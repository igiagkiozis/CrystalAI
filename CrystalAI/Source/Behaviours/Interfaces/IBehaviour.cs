// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// IBehaviour.cs is part of Crystal AI.
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
  ///   Interface to Behaviours.
  /// </summary>
  /// <seealso cref="Crystal.ICompositeConsideration"/>
  public interface IBehaviour : ICompositeConsideration {
    /// <summary>
    ///   <see cref="T:Crystal.ISelector"/>
    /// </summary>
    /// <value>
    ///   The selector used in this behaviour. Note this cannot be null and because of this
    ///   if a null value set is attempted this is simply ignored and the previous value is
    ///   retained.
    /// </value>
    ISelector Selector { get; set; }

    /// <summary>
    ///   Adds the option associated with optionId, if one exists.
    /// </summary>
    /// <param name="optionId">The option identifier.</param>
    /// <returns>Returns true if the option was successfully added, false otherwise.</returns>
    bool AddOption(string optionId);

    /// <summary>
    ///   Selects an <see cref="T:Crystal.IOption"/> from this behaviour and returns its associated
    ///   action.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>The action associated with the selected option.</returns>
    IAction Select(IContext context);
  }

}