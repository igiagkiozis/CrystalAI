// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// Scheduler.cs is part of Crystal AI.
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
  ///   AI Scheduler.
  /// </summary>
  public sealed class Scheduler : IScheduler {
    /// <summary>
    ///   The Think cycle.
    /// </summary>
    public ICommandStream ThinkStream { get; private set; }

    /// <summary>
    ///   The Update cycle.
    /// </summary>
    /// <value>The update queue.</value>
    public ICommandStream UpdateStream { get; private set; }

    /// <summary>
    ///   Tick this instance.
    /// </summary>
    public void Tick() {
      ThinkStream.Process();
      UpdateStream.Process();
    }

    public Scheduler() {
      ThinkStream = new CommandStream(128) {
        MaxProcessingTime = 1
      };
      UpdateStream = new CommandStream(128) {
        MaxProcessingTime = 3
      };
    }
  }

}