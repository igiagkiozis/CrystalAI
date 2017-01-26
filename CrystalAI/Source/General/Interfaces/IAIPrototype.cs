// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// IAIPrototype.cs is part of Crystal AI.
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
  /// Interface to all AiPrototypes.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public interface IAiPrototype<T> {
    /// <summary>
    /// Creates a new instance of the implementing class. Note that the semantics here
    /// are somewhat vague, however, by convention the "Prototype Pattern" uses a "Clone"
    /// function. Note that this may have very different semantics when compared with either
    /// shallow or deep cloning. When implementing this remember to include only the defining
    /// characteristics of the class and not its state!
    /// </summary>
    T Clone();
  }

}