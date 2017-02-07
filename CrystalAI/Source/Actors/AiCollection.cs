// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// AiCollection.cs is part of Crystal AI.
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
using System.Collections.Generic;


namespace Crystal {


  /// <summary>
  /// An AiCollection is a container of all AI building blocks necessary for the construction
  /// of utility AI instances.
  /// </summary>
  /// <seealso cref="T:Crystal.IAiCollection" />
  public class AiCollection : IAiCollection {
    Dictionary<string, IUtilityAi> _aiMap;

    /// <summary>
    ///   All behaviours available to this AiCollection.
    /// </summary>
    public IBehaviourCollection Behaviours { get; private set; }
    
    /// <summary>
    ///   All options available to this AiCollection.
    /// </summary>
    public IOptionCollection Options {
      get { return Behaviours.Options; }
    }

    /// <summary>
    ///   All considerations available to this AiCollection.
    /// </summary>
    public IConsiderationCollection Considerations {
      get { return Behaviours.Options.Considerations; }
    }

    /// <summary>
    ///   All actions available to this AiCollection.
    /// </summary>
    public IActionCollection Actions {
      get { return Behaviours.Options.Actions; }
    }

    /// <summary>
    ///   Adds the specified AI.
    /// </summary>
    /// <param name="ai">The AI.</param>
    /// <returns></returns>
    public bool Add(IUtilityAi ai) {
      if(ai == null)
        return false;
      if(string.IsNullOrEmpty(ai.NameId))
        return false;
      if(_aiMap.ContainsKey(ai.NameId))
        return false;

      _aiMap.Add(ai.NameId, ai);
      return true;
    }

    /// <summary>
    ///   Determines whether [contains] [the specified name identifier].
    /// </summary>
    /// <param name="aiId">The name identifier.</param>
    /// <returns>
    ///   <c>true</c> if [contains] [the specified name identifier]; otherwise, <c>false</c>.
    /// </returns>
    public bool Contains(string aiId) {
      return _aiMap.ContainsKey(aiId);
    }

    /// <summary>
    ///   Gets the AI.
    /// </summary>
    /// <param name="aiId">The name identifier.</param>
    /// <returns></returns>
    public IUtilityAi GetAi(string aiId) {
      return _aiMap.ContainsKey(aiId) ? _aiMap[aiId] : null;
    }

    /// <summary>
    ///   Creates a new utility AI instance if the requested AI named aiId exists within
    ///   the AiCollection, otherwise this returns null.
    /// </summary>
    /// <param name="aiId"></param>
    /// <returns></returns>
    public IUtilityAi Create(string aiId) {
      return _aiMap.ContainsKey(aiId) ? _aiMap[aiId].Clone() : null;
    }

    /// <summary>
    ///   Removes all AIs in this collection.
    /// </summary>
    public void Clear() {
      _aiMap.Clear();
    }

    /// <summary>
    ///   Clears everything, i.e. all actions, considerations, options, behaviours and AIs.
    /// </summary>
    public void ClearAll() {
      _aiMap.Clear();
      Behaviours.Clear();
      Options.Clear();
      Considerations.Clear();
      Actions.Clear();
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="AiCollection"/> class.
    /// </summary>
    /// <param name="behaviourCollection">The behaviour collection.</param>
    /// <exception cref="Crystal.AiCollection.BehaviourCollectionNullException"></exception>
    public AiCollection(IBehaviourCollection behaviourCollection) {
      if(behaviourCollection == null)
        throw new BehaviourCollectionNullException();

      _aiMap = new Dictionary<string, IUtilityAi>();
      Behaviours = behaviourCollection;
    }

    internal class BehaviourCollectionNullException : Exception {
    }
  }

}