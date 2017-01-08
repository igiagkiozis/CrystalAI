// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// CrTime.cs is part of Crystal AI.
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
using System.Diagnostics;


namespace Crystal {

  /// <summary>
  ///   A wrapper for the UnityEngine.Time class. This is necessary for unit tests
  ///   without the Unity runtime.
  /// </summary>
  public static class CrTime {
    static readonly Stopwatch Clock;

    /// <summary>
    ///   The Time at the beginning of this frame (Read Only). This is the Time in seconds since the
    ///   start of the game.
    /// </summary>
    public static float Time {
      get { return (float)Clock.Elapsed.TotalSeconds; }
    }

    static CrTime() {
      Clock = new Stopwatch();
      Clock.Start();
    }
  }

}