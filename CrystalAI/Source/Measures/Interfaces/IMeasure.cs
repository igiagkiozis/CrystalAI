// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// IMeasure.cs is part of Crystal AI.
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
using System.Collections.Generic;


namespace Crystal {

  /// <summary>
  ///   The measure interface. Note this refers to the mathematical measure
  ///   <see cref="https://en.wikipedia.org/wiki/Measure_(mathematics)"/>
  /// </summary>
  public interface IMeasure : IAiPrototype<IMeasure> {
    /// <summary>
    ///   Calculate the measure for the given set of elements.
    /// </summary>
    float Calculate(ICollection<Utility> elements);
  }

}