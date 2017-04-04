// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// ActionBase.cs is part of Crystal AI.
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
using System;


namespace Crystal {

  /// <summary>
  ///   Base class for non-generic <see cref="T:Crystal.IAction"/>s. All actions should derive either from
  ///   this class or its generic version <see cref="T:Crystal.ActionBase`1"/>.
  /// </summary>
  /// <seealso cref="T:Crystal.IAction"/>
  public class ActionBase : IAction {
    IActionCollection _collection;
    float _cooldown;
    float _startedTime;

    /// <summary>
    ///   A string alias for ID.
    /// </summary>
    public string NameId { get; set; }

    /// <summary>
    ///   The Time that this action has been running for since it has been started. If the task is currently
    ///   on pause, this will return the difference pauseTime - startTime.
    /// </summary>
    /// <value>The elapsed Time.</value>
    public float ElapsedTime {
      get {
        if(ActionStatus == ActionStatus.Running)
          return CrTime.TotalSeconds - _startedTime;

        return 0f;
      }
    }

    /// <summary>
    ///   The required cool-down time, in seconds, needed before this action executes again.
    /// </summary>
    public float Cooldown {
      get { return _cooldown; }
      set { _cooldown = value.ClampToLowerBound(0.0f); }
    }

    /// <summary>
    ///   This returns true if the cool-down time for this action has not yet elapsed.
    /// </summary>
    public bool InCooldown {
      get {
        if(ActionStatus == ActionStatus.Running ||
           ActionStatus == ActionStatus.Idle)
          return false;

        return CrTime.TotalSeconds - _startedTime < _cooldown;
      }
    }

    /// <summary>
    ///   Gets the action status.
    /// </summary>
    /// <value>The action status.</value>
    public ActionStatus ActionStatus { get; protected set; } = ActionStatus.Idle;

    /// <summary>Executes the action.</summary>
    /// <param name="context">The context.</param>
    public void Execute(IContext context) {
      if(CanExecute() == false)
        return;

      if(TryUpdate(context) == false) {
        _startedTime = CrTime.TotalSeconds;
        ActionStatus = ActionStatus.Running;
        OnExecute(context);
      }
    }

    /// <summary>
    ///   Creates a new instance of the implementing class. Note that the semantics here
    ///   are somewhat vague, however, by convention the "Prototype Pattern" uses a "Clone"
    ///   function. Note that this may have very different semantics when compared with either
    ///   shallow or deep cloning. When implementing this remember to include only the defining
    ///   characteristics of the class and not its state!
    /// </summary>
    /// <returns></returns>
    public virtual IAction Clone() {
      return new ActionBase(this);
    }

    /// <summary>
    ///   Ends the action and sets its status to <see cref="F:Crystal.ActionStatus.Success"/>.
    /// </summary>
    /// <param name="context">The context.</param>
    protected void EndInSuccess(IContext context) {
      if(ActionStatus != ActionStatus.Running)
        return;

      ActionStatus = ActionStatus.Success;
      FinalizeAction(context);
    }

    /// <summary>
    ///   Ends the action and sets its status to <see cref="F:Crystal.ActionStatus.Failure"/>.
    /// </summary>
    /// <param name="context">The context.</param>
    protected void EndInFailure(IContext context) {
      if(ActionStatus != ActionStatus.Running)
        return;

      ActionStatus = ActionStatus.Failure;
      FinalizeAction(context);
    }

    /// <summary>
    ///   Executes once when the action starts.
    /// </summary>
    /// <param name="context">Context.</param>
    protected virtual void OnExecute(IContext context) {
      EndInSuccess(context);
    }

    /// <summary>
    ///   Executes on every action update, until <see cref="ActionBase.EndInSuccess"/> or
    ///   <see cref="ActionBase.EndInFailure"/> is called.
    /// </summary>
    /// <param name="context">Context.</param>
    protected virtual void OnUpdate(IContext context) {
    }

    /// <summary>
    ///   This can be used for cleanup. It executes after <see cref="ActionBase.EndInSuccess"/> or
    ///   <see cref="ActionBase.EndInFailure"/> is called.
    /// </summary>
    /// <param name="context">Context.</param>
    protected virtual void OnStop(IContext context) {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ActionBase"/> class.
    /// </summary>
    public ActionBase() {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ActionBase"/> class.
    /// </summary>
    /// <param name="other">The other.</param>
    protected ActionBase(ActionBase other) {
      NameId = other.NameId;
      _collection = other._collection;
      Cooldown = other.Cooldown;     
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ActionBase"/> class.
    /// </summary>
    /// <param name="nameId">The name identifier.</param>
    /// <param name="collection">The collection.</param>
    /// <exception cref="Crystal.ActionBase.NameIdEmptyOrNullException"></exception>
    /// <exception cref="Crystal.ActionBase.ActionCollectionNullException"></exception>
    public ActionBase(string nameId, IActionCollection collection) {
      if(string.IsNullOrEmpty(nameId))
        throw new NameIdEmptyOrNullException();
      if(collection == null)
        throw new ActionCollectionNullException();

      NameId = nameId;
      _collection = collection;
      AddSelfToCollection();
    }

    bool CanExecute() {
      if(InCooldown) {
        ActionStatus = ActionStatus.Failure;
        return false;
      }

      return true;
    }

    bool TryUpdate(IContext context) {
      if(ActionStatus == ActionStatus.Running) {
        OnUpdate(context);
        return true;
      }

      return false;
    }

    void FinalizeAction(IContext context) {
      OnStop(context);
    }

    void AddSelfToCollection() {
      if(_collection.Add(this) == false)
        throw new ActionAlreadyExistsInCollectionException(NameId);
    }

    internal class NameIdEmptyOrNullException : Exception {
    }

    internal class ActionCollectionNullException : Exception {
    }

    internal class ActionAlreadyExistsInCollectionException : Exception {
      string _message;

      public override string Message {
        get { return _message; }
      }

      public ActionAlreadyExistsInCollectionException(string nameId) {
        _message = string.Format("Error: {0} already exists in the actions collection.", nameId);
      }
    }
  }

}