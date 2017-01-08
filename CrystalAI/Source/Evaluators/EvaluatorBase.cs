// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// EvaluatorBase.cs is part of Crystal AI.
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

  public abstract class EvaluatorBase : IEvaluator, IComparable<IEvaluator> {
    /// <summary>
    ///   The first point in terms of x!
    /// </summary>
    public Pointf PtA {
      get { return new Pointf(Xa, Ya); }
    }

    /// <summary>
    ///   The second point in terms of x!
    /// </summary>
    public Pointf PtB {
      get { return new Pointf(Xb, Yb); }
    }

    /// <summary>
    ///   Gets the minimum x.
    /// </summary>
    /// <value>The minimum x.</value>
    public float MinX {
      get { return Xa; }
    }

    /// <summary>
    ///   Gets the max x.
    /// </summary>
    /// <value>The max x.</value>
    public float MaxX {
      get { return Xb; }
    }

    /// <summary>
    ///   Gets the minimum y.
    /// </summary>
    /// <value>The minimum y.</value>
    public float MinY {
      get { return Math.Min(Ya, Yb); }
    }

    /// <summary>
    ///   Gets the max y.
    /// </summary>
    /// <value>The max y.</value>
    public float MaxY {
      get { return Math.Max(Ya, Yb); }
    }

    /// <summary>
    ///   Gets the X interval.
    /// </summary>
    /// <value>The X interval.</value>
    public Interval<float> XInterval {
      get { return new Interval<float>(MinX, MaxX); }
    }

    /// <summary>
    ///   Gets the Y interval.
    /// </summary>
    /// <value>The Y interval.</value>
    public Interval<float> YInterval {
      get { return new Interval<float>(MinY, MaxY); }
    }

    public int CompareTo(IEvaluator other) {
      return XInterval.CompareTo(other.XInterval);
    }

    /// <summary>
    ///   Returns the utility for the specified value x.
    /// </summary>
    /// <param name="x">The x value.</param>
    public abstract float Evaluate(float x);

    protected EvaluatorBase() {
      Initialize(0.0f, 0.0f, 1.0f, 1.0f);
    }

    protected EvaluatorBase(Pointf ptA, Pointf ptB) {
      Initialize(ptA.X, ptA.Y, ptB.X, ptB.Y);
    }

    void Initialize(float xA, float yA, float xB, float yB) {
      if(CrMath.AeqB(xA, xB))
        throw new EvaluatorDxZeroException();
      if(xA > xB)
        throw new EvaluatorXaGreaterThanXbException();

      Xa = xA;
      Xb = xB;
      Ya = yA.Clamp01();
      Yb = yB.Clamp01();
    }

    protected float Xa;
    protected float Xb;
    protected float Ya;
    protected float Yb;

    internal class EvaluatorDxZeroException : Exception {
    }

    internal class EvaluatorXaGreaterThanXbException : Exception {
    }
  }

}