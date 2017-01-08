// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// ConstantUtilityOption.cs is part of Crystal AI.
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
  ///   This option always returns the same utility irrespective of the context.
  ///   <seealso cref="T:Crystal.Option"/>
  /// </summary>
  public sealed class ConstantUtilityOption : Option {
    /// <summary>
    ///   Calculates the utility given the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>The utility.</returns>
    public override void Consider(IContext context) {
    }

    public override IConsideration Clone() {
      return new ConstantUtilityOption(this);
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ConstantUtilityOption"/> class.
    /// </summary>
    public ConstantUtilityOption() {
      Weight = 1.0f;
      DefaultUtility = new Utility(0.0f, Weight);
    }

    ConstantUtilityOption(ConstantUtilityOption other) : base(other) {
      Weight = other.Weight;
      DefaultUtility = other.DefaultUtility;
    }

    public ConstantUtilityOption(string nameId, IOptionCollection collection) : base(nameId, collection) {
    }
  }

}