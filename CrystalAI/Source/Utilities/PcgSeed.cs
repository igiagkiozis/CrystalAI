// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// PcgSeed.cs is part of Crystal AI.
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

  public static class PcgSeed {
    /// <summary>
    ///   Provides a Time-dependent seed value, matching the default behavior of System.Random.
    /// </summary>
    public static ulong TimeBasedSeed() {
      return (ulong)Environment.TickCount;
    }

    /// <summary>
    ///   Provides a seed based on Time and unique GUIDs.
    /// </summary>
    public static ulong GuidBasedSeed() {
      ulong upper = (ulong)(Environment.TickCount ^ Guid.NewGuid().GetHashCode()) << 32;
      ulong lower = (ulong)(Environment.TickCount ^ Guid.NewGuid().GetHashCode());
      return upper | lower;
    }
  }

}