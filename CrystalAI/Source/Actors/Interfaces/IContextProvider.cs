// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// IContextProvider.cs is part of Crystal AI.
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
  ///   Interface for context provider responsible for supplying the <see cref="T:Crystal.IContext"/>
  ///   implementing context instances for e.g. AI clients.
  /// </summary>
  public interface IContextProvider {
    /// <summary>
    ///   Retrieves the context instance. This can be a simple getter or a factory method.
    /// </summary>
    /// <returns>The concrete context instance for use by the requester.</returns>
    IContext Context();
  }

}