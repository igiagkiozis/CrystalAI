// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// IDeferredCommandHandle.cs is part of Crystal AI.
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
  ///   Interface to represent items added to the load balancer, which allows a number of
  ///   operations to be called.
  /// </summary>
  public interface IDeferredCommandHandle {
    /// <summary>
    ///   The scheduled command this handle refers to.
    /// </summary>
    IDeferredCommand Command { get; }

    /// <summary>
    ///   If true the associated command is still being executed.
    /// </summary>
    bool IsActive { get; set; }

    /// <summary>
    ///   Pause execution of command.
    /// </summary>
    void Pause();

    /// <summary>
    ///   Resume execution of this command.
    /// </summary>
    void Resume();
  }

}