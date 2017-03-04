// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// OptionCollection.cs is part of Crystal AI.
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
  /// A collection of AI <see cref="T:Crystal.IOption"/>s.
  /// </summary>
  /// <seealso cref="T:Crystal.IOptionCollection" />
  public class OptionCollection : IOptionCollection {
    Dictionary<string, IOption> _optionsMap;

    /// <summary>
    /// The <see cref="T:Crystal.IActionCollection" /> used to construct this
    /// <see cref="T:Crystal.IOptionCollection." />.
    /// </summary>
    public IActionCollection Actions { get; private set; }

    /// <summary>
    /// The <see cref="T:Crystal.IConsiderationCollection" /> used to construct this
    /// <see cref="T:Crystal.IOptionCollection." />.
    /// </summary>
    public IConsiderationCollection Considerations { get; private set; }

    /// <summary>
    /// Adds the specified option to this collection.
    /// </summary>
    /// <param name="option">The option.</param>
    /// <returns>
    /// <c>true</c> if the <see cref="T:Crystal.IOption" /> was successfully added to this collection;
    /// otherwise, <c>false</c>.
    /// </returns>
    public bool Add(IOption option) {
      if(option == null)
        return false;
      if(string.IsNullOrEmpty(option.NameId))
        return false;
      if(_optionsMap.ContainsKey(option.NameId))
        return false;

      _optionsMap.Add(option.NameId, option);
      return true;
    }
    
    /// <summary>
    /// Determines whether this collection contains the option associated with the
    /// specified identifier.
    /// </summary>
    /// <param name="optionId">The option identifier.</param>
    /// <returns>
    /// <c>true</c> if this collection contains an option with the specified identifier;
    /// otherwise, <c>false</c>.
    /// </returns>
    public bool Contains(string optionId) {
      return _optionsMap.ContainsKey(optionId);
    }

    /// <summary>
    /// Clears all <see cref="T:Crystal.IOption" />s from this collection.
    /// </summary>
    public void Clear() {
      _optionsMap.Clear();
    }

    /// <summary>
    /// Clears all <see cref="T:Crystal.IOption" />s from this collection. Additionally all
    /// considerations are cleared from <see cref="P:Crystal.IOptionCollection.Considerations" /> and
    /// all actions are cleared from <see cref="P:Crystal.IOptionCollection.Actions" />.
    /// </summary>
    public void ClearAll() {
      _optionsMap.Clear();
      Actions.Clear();
      Considerations.Clear();
    }

    /// <summary>
    /// Creates an instance of the option that is associated with the given identifier. If such an option
    /// does not exist <c>null</c> is returned.f
    /// </summary>
    /// <param name="optionId">The option identifier.</param>
    /// <returns></returns>
    public IOption Create(string optionId) {
      return _optionsMap.ContainsKey(optionId) ? _optionsMap[optionId].Clone() as IOption : null;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OptionCollection"/> class.
    /// </summary>
    /// <param name="actionCollection">The action collection.</param>
    /// <param name="considerationCollection">The consideration collection.</param>
    /// <exception cref="T:Crystal.OptionCollection.ActionCollectionNullException"></exception>
    /// <exception cref="T:Crystal.OptionCollection.ConsiderationCollectionNullException"></exception>
    public OptionCollection(IActionCollection actionCollection, IConsiderationCollection considerationCollection) {
      if(actionCollection == null)
        throw new ActionCollectionNullException();
      if(considerationCollection == null)
        throw new ConsiderationCollectionNullException();

      _optionsMap = new Dictionary<string, IOption>();
      Actions = actionCollection;
      Considerations = considerationCollection;
    }

    internal class ActionCollectionNullException : Exception {
    }

    internal class ConsiderationCollectionNullException : Exception {
    }
  }

}