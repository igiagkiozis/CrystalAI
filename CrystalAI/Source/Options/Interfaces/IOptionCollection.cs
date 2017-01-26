// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// IOptionCollection.cs is part of Crystal AI.
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
namespace Crystal {

  /// <summary>
  ///   Interface to a collection containing <see cref="T:Crystal.IOption"/>s.
  /// </summary>
  public interface IOptionCollection {
    /// <summary>
    ///   The <see cref="T:Crystal.IActionCollection"/> used to construct this
    ///   <see cref="T:Crystal.IOptionCollection."/>.
    /// </summary>
    IActionCollection Actions { get; }

    /// <summary>
    ///   The <see cref="T:Crystal.IConsiderationCollection"/> used to construct this
    ///   <see cref="T:Crystal.IOptionCollection."/>.
    /// </summary>
    IConsiderationCollection Considerations { get; }

    /// <summary>
    ///   Adds the specified option to this collection.
    /// </summary>
    /// <param name="option">The option.</param>
    /// <returns>
    ///   <c>true</c> if the <see cref="T:Crystal.IOption"/> was successfully added to this collection;
    ///   otherwise, <c>false</c>.
    /// </returns>
    bool Add(IOption option);

    /// <summary>
    ///   Determines whether this collection contains the option associated with the
    ///   specified identifier.
    /// </summary>
    /// <param name="optionId">The option identifier.</param>
    /// <returns>
    ///   <c>true</c> if this collection contains an option with the specified identifier;
    ///   otherwise, <c>false</c>.
    /// </returns>
    bool Contains(string optionId);

    /// <summary>
    ///   Clears all <see cref="T:Crystal.IOption"/>s from this collection.
    /// </summary>
    void Clear();

    /// <summary>
    ///   Clears all <see cref="T:Crystal.IOption"/>s from this collection. Additionally all
    ///   considerations are cleared from <see cref="P:Crystal.IOptionCollection.Considerations"/> and
    ///   all actions are cleared from <see cref="P:Crystal.IOptionCollection.Actions"/>.
    /// </summary>
    void ClearAll();

    /// <summary>
    ///   Creates an instance of the option that is associated with the given identifier. If such an option
    ///   does not exist <c>null</c> is returned.f
    /// </summary>
    /// <param name="optionId">The option identifier.</param>
    IOption Create(string optionId);
  }

}