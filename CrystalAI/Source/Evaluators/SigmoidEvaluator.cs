// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// SigmoidEvaluator.cs is part of Crystal AI.
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

  /// <summary>
  ///   The SigmoidEvaluator returns a normalized utility based on a sigmoid function that has
  ///   effectively 1 parameter ( -0.99999f leq k leq 0.99999f ) bounded by the box defined by PtA and PtB with
  ///   PtA.x being strictly less than PtB.x!
  ///   <see href="https://www.desmos.com/calculator/rf8mrgolws">Parametrised Sigmoid</see> for an interactive
  ///   plot.
  /// </summary>
  public class SigmoidEvaluator : EvaluatorBase {
    float _dyOverTwo;
    float _k;
    float _oneMinusK;
    float _twoOverDx;
    float _xMean;
    float _yMean;

    /// <summary>
    ///   Returns the utility value for the specified value x.
    /// </summary>
    /// <param name="x">The x value.</param>
    public override float Evaluate(float x) {
      var cxMinusXMean = x.Clamp<float>(Xa, Xb) - _xMean;
      var num = _twoOverDx * cxMinusXMean * _oneMinusK;
      var den = _k * (1 - 2 * Math.Abs(_twoOverDx * cxMinusXMean)) + 1;
      var val = _dyOverTwo * (num / den) + _yMean;
      return val;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="Crystal.SigmoidEvaluator"/> class.
    ///   <see href="https://www.desmos.com/calculator/rf8mrgolws">Parametrised Sigmoid</see> for an interactive
    ///   plot.
    /// </summary>
    public SigmoidEvaluator() {
      _k = -0.6f;
      Initialize();
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="Crystal.SigmoidEvaluator"/> class.
    ///   <see href="https://www.desmos.com/calculator/rf8mrgolws">Parametrised Sigmoid</see> for an interactive
    ///   plot.
    /// </summary>
    /// <param name="ptA">Point a.</param>
    /// <param name="ptB">Point b.</param>
    public SigmoidEvaluator(Pointf ptA, Pointf ptB) : base(ptA, ptB) {
      _k = -0.6f;
      Initialize();
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="Crystal.SigmoidEvaluator"/> class.
    ///   <see href="https://www.desmos.com/calculator/rf8mrgolws">Parametrised Sigmoid</see> for an interactive
    ///   plot.
    /// </summary>
    /// <param name="ptA">Point a.</param>
    /// <param name="ptB">Point b.</param>
    /// <param name="k">K.</param>
    public SigmoidEvaluator(Pointf ptA, Pointf ptB, float k) : base(ptA, ptB) {
      _k = k.Clamp<float>(MinK, MaxK);
      Initialize();
    }

    void Initialize() {
      _twoOverDx = Math.Abs(2.0f / (Xb - Xa));
      _xMean = (Xa + Xb) / 2.0f;
      _yMean = (Ya + Yb) / 2.0f;
      _dyOverTwo = (Yb - Ya) / 2.0f;
      _oneMinusK = 1.0f - _k;
    }

    const float MinK = -0.99999f;
    const float MaxK = 0.99999f;
  }

}