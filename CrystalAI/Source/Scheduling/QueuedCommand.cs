// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// QueuedCommand.cs is part of Crystal AI.
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
using System.Collections.Generic;


namespace Crystal {

  internal class QueuedCommandComparer : IComparer<QueuedCommand> {
    public int Compare(QueuedCommand x, QueuedCommand y) {
      return x.NextExecution.CompareTo(y.NextExecution);
    }
  }

  internal class QueuedCommand : IDeferredCommandHandle, IHeapItem<QueuedCommand> {
    bool _isActive = true;

    CommandStream _stream;
    public float LastExecution;
    public float NextExecution;

    /// <summary>
    ///   The scheduled command this handle refers to.
    /// </summary>
    /// <value>The command.</value>
    public DeferredCommand Command { get; set; }

    public IPriorityQueueHandle<QueuedCommand> Handle { get; set; }

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

      float time = CrTime.TotalSeconds;
      float num = NextExecution - time;
      LastExecution = num;
      NextExecution = float.PositiveInfinity;
      _stream.Queue.UpdatePriority(this);
    }

    public void Resume() {
      if(_isActive == false)
        return;

      float time = CrTime.TotalSeconds;
      LastExecution = time;
      NextExecution = time + Command.ExecutionDelay;
      _stream.Queue.UpdatePriority(this);
    }

    public QueuedCommand(CommandStream stream) {
      _stream = stream;
    }

    void AddSelfToQueue() {
      float time = CrTime.TotalSeconds;
      LastExecution = time;
      NextExecution = time + Command.ExecutionDelay;
      _stream.Queue.Enqueue(this);
    }

    void RemoveSelfFromQueue() {
      if(_stream.Queue.Peek() == this)
        _stream.Queue.Dequeue();
      else
        _stream.Queue.Remove(this);
    }
    
  }

}