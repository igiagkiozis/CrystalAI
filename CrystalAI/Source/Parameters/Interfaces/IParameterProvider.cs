// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// IParameterProvider.cs is part of Crystal AI.
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
  ///   Provides parameters to <see cref="T:Crystal.IOption"/>, <see cref="T:Crystal.IAction"/>, and <see cref="T:Crystal.IConsideration"/>.
  ///   These can be used to provide additional hints in addition to the <see cref="T:Crystal.IContext"/>.
  /// </summary>
  public interface IParameterProvider : IAiPrototype<IParameterProvider> {
  }

}