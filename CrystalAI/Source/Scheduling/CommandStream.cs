// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// CommandStream.cs is part of Crystal AI.
//  
// Crystal AI is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//  
// Crystal AI is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Crystal AI.  If not, see <http://www.gnu.org/licenses/>.
using System.Diagnostics;


namespace Crystal {

  /// <summary>
  ///   The CommandStream processes a given fraction of the AI events on every loop.
  /// </summary>
  public sealed class CommandStream : ICommandStream {
    float _extraTimeNeeded;
    float _frameBeginTime;

    // A game running at 60fps takes approximately 16ms per update, hence it seems reasonable to have the default AI 
    // update cycle to be one eighth of that as a default.
    double _maxProcessingTime = 2.0;
    float _minimumDelay = 1e-6f;
    QueuedCommand _nextCommand;
    int _processedCommandsCounter;
    IPriorityQueue<QueuedCommand, float> _queue;
    Stopwatch _watch;

    /// <summary>
    ///   The maximum Time in milliseconds the Process() is allowed to take.
    /// </summary>
    /// <value>The max process Time.</value>
    public double MaxProcessingTime {
      get { return _maxProcessingTime; }
      set { _maxProcessingTime = value.ClampToLowerBound(0.1); }
    }

    /// <summary>
    ///   Gets the accumulated number of seconds the updates were overdue this frame, i.e. sum of
    ///   all updates.
    /// </summary>
    public float ExtraTimeNeeded { get; private set; }

    /// <summary>
    ///   Total milliseconds used on the last Process().
    /// </summary>
    public double TotalMilliseconds { get; private set; }

    /// <summary>
    ///   The number of items in the queue that were processed during the previous cycle.
    /// </summary>
    public int ProcessedCount { get; private set; }

    /// <summary>
    ///   The number of scheduled commands in the queue.
    /// </summary>
    public int CommandsCount {
      get { return _queue.Count; }
    }

    public IDeferredCommandHandle Add(IDeferredCommand cmd) {
      float time = CrTime.Time;
      var scheduledCommand = new QueuedCommand {
        Queue = this,
        Command = cmd,
        LastExecution = time,
        NextExecution = time + cmd.InitExecutionDelay
      };

      _queue.Enqueue(scheduledCommand, scheduledCommand.NextExecution);
      return scheduledCommand;
    }

    /// <summary>
    ///   Processes as many items as possible withing the given Time limits.
    /// </summary>
    public void Process() {
      ResetVariables();
      UpdateFrameTimeAndStartClock();

      do {
        if(CanGetNextCommand()) {
          CalculateOverdueUpdates();
          ExecuteNextCommand();
          RescheduleOrRemoveExecutedCommand();
        } else
          break;
      } while(StillHaveTime());

      UpdatePerformanceMeasurements();
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="Crystal.CommandStream"/> class.
    /// </summary>
    /// <param name="initialQueueSize">Initial queue size.</param>
    public CommandStream(int initialQueueSize) {
      _watch = new Stopwatch();
      _queue = new PriorityQueue<QueuedCommand, float>(initialQueueSize);
    }

    void ResetVariables() {
      _nextCommand = null;
      _extraTimeNeeded = 0.0f;
      _processedCommandsCounter = 0;
    }

    void UpdateFrameTimeAndStartClock() {
      _watch.Reset();
      _watch.Start();
      _frameBeginTime = CrTime.Time;
    }

    bool CanGetNextCommand() {
      if(_queue.HasNext == false)
        return false;

      _nextCommand = _queue.Peek();
      return _nextCommand.NextExecution <= _frameBeginTime;
    }

    bool StillHaveTime() {
      return _watch.Elapsed.TotalMilliseconds < MaxProcessingTime;
    }

    void CalculateOverdueUpdates() {
      float timeSinceLastUpdate = _frameBeginTime - _nextCommand.LastExecution;
      _extraTimeNeeded += timeSinceLastUpdate - _nextCommand.Command.ExecutionDelay;
    }

    void ExecuteNextCommand() {
      _nextCommand.Command.Execute();
      _processedCommandsCounter++;
    }

    void RescheduleOrRemoveExecutedCommand() {
      if(_nextCommand.Command.IsRepeating)
        UpdateScheduledItem();
      else
        _nextCommand.IsActive = false;
    }

    void UpdateScheduledItem() {
      _nextCommand.LastExecution = _frameBeginTime;
      _nextCommand.NextExecution = _frameBeginTime + _nextCommand.Command.ExecutionDelay; // + MinimumDelay;
//      _queue.UpdatePriority(_nextCommand, _nextCommand.NextExecution);
      _queue.Dequeue();
      _queue.Enqueue(_nextCommand, _nextCommand.NextExecution);
    }

    void UpdatePerformanceMeasurements() {
      ProcessedCount = _processedCommandsCounter;
      ExtraTimeNeeded = _extraTimeNeeded;
      TotalMilliseconds = _watch.Elapsed.TotalMilliseconds;
    }

    void Add(QueuedCommand item, float priority) {
      _queue.Enqueue(item, priority);
    }

    void Remove(QueuedCommand item) {
      _queue.Remove(item);
    }

    QueuedCommand Peek() {
      return _queue.Peek();
    }

    QueuedCommand Dequeue() {
      return _queue.Dequeue();
    }

    void UpdatePriority(QueuedCommand item, float priority) {
      _queue.UpdatePriority(item, priority);
    }

    class QueuedCommand : IDeferredCommandHandle {
      bool _isActive = true;

      /// <summary>
      ///   The scheduled command this handle refers to.
      /// </summary>
      /// <value>The command.</value>
      public IDeferredCommand Command { get; set; }

      /// <summary>
      ///   If true the associated command is still being executed.
      /// </summary>
      public bool IsActive {
        get { return _isActive; }
        set {
          if(_isActive != value) {
            if(_isActive)
              RemoveSelfFromQueue();
            else
              AddSelfToQueue();
            _isActive = value;
          }
        }
      }

      public void Pause() {
        if(_isActive == false)
          return;

        float time = CrTime.Time;
        float num = NextExecution - time;
        LastExecution = num;
        NextExecution = float.PositiveInfinity;
        Queue.UpdatePriority(this, NextExecution);
      }

      public void Resume() {
        if(_isActive == false)
          return;

        float time = CrTime.Time;
        LastExecution = time;
        NextExecution = time + Command.ExecutionDelay;
        Queue.UpdatePriority(this, NextExecution);
      }

      void AddSelfToQueue() {
        float time = CrTime.Time;
        LastExecution = time;
        NextExecution = time + Command.ExecutionDelay;
        Queue.Add(this, NextExecution);
      }

      void RemoveSelfFromQueue() {
        if(Queue.Peek() == this)
          Queue.Dequeue();
        else
          Queue.Remove(this);
      }

      internal CommandStream Queue;
      internal float LastExecution;
      internal float NextExecution;
    }
  }

}