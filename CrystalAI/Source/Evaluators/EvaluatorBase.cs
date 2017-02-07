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

  /// <summary>
  ///   EvaluatorBase serves as a base class for all evaluators, i.e. functions with arbitrary domain
  ///   of definition and a range equal to any sub-interval of [0,1].
  /// </summary>
  /// <seealso cref="T:Crystal.IEvaluator"/>
  /// <seealso cref="T:System.IComparable`1"/>
  public class EvaluatorBase : IEvaluator, IComparable<IEvaluator> {

    /// <summary>
    /// The x-coordinate of point A. Note, this can have any value in the real interval (-inf, +inf).
    /// </summary>
    protected float Xa;
    /// <summary>
    /// The x-coordinate of point B. Note, this can have any value in the real interval (-inf, +inf).
    /// </summary>
    protected float Xb;
    /// <summary>
    /// The y-coordinate of point A. Note, this can have a value within the interval [0, 1].
    /// </summary>
    protected float Ya;
    /// <summary>
    /// The y-coordinate of point B. Note, this can have a value within the interval [0, 1].
    /// </summary>
    protected float Yb;

    /// <summary>
    ///   The first point of the evaluator. The x-coordinate of this point will always
    ///   be strictly smaller than that of PtB.
    /// </summary>
    public Pointf PtA {
      get { return new Pointf(Xa, Ya); }
    }

    /// <summary>
    ///   The second point of the evaluator. The x-coordinate of this point will always be
    ///   strictly larger than that of PtA.
    /// </summary>
    public Pointf PtB {
      get { return new Pointf(Xb, Yb); }
    }

    /// <summary>
    ///   The lower bound of the x-coordinate interval.
    /// </summary>
    public float MinX {
      get { return Xa; }
    }

    /// <summary>
    ///   The upper bound of the x-coordinate interval.
    /// </summary>
    public float MaxX {
      get { return Xb; }
    }

    /// <summary>
    ///   The lower bound of the y-coordinate interval.
    /// </summary>
    public float MinY {
      get { return Math.Min(Ya, Yb); }
    }

    /// <summary>
    ///   The upper bound of the y-coordinate interval.
    /// </summary>
    public float MaxY {
      get { return Math.Max(Ya, Yb); }
    }

    /// <summary>
    ///   The x-coordinate interval represents the domain of definition of this evaluator.
    /// </summary>
    public Interval<float> XInterval {
      get { return new Interval<float>(MinX, MaxX); }
    }

    /// <summary>
    ///   The y-coordinate interval represents the range of this evaluator. Note that this must
    ///   be a sub-interval (or the entire interval) [0,1].
    /// </summary>
    public Interval<float> YInterval {
      get { return new Interval<float>(MinY, MaxY); }
    }

    /// <summary>
    ///   When true, the output of the Evaluate method is transformed to 1.0f - (normal output).
    /// </summary>
    public bool IsInverted { get; set; }

    /// <summary>
    ///   Compares the current object with another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   A 32-bit signed integer that indicates the relative order of the objects being compared.
    ///   The return value has the following meanings: Value Meaning Less than zero This object is
    ///   less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>.
    ///   Greater than zero This object is greater than <paramref name="other"/>.
    /// </returns>
    public int CompareTo(IEvaluator other) {
      return XInterval.CompareTo(other.XInterval);
    }

    /// <summary>
    ///   Returns the value for the specified x.
    /// </summary>
    public virtual float Evaluate(float x) {
      return 0f;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="T:Crystal.EvaluatorBase"/> class.
    /// </summary>
    protected EvaluatorBase() {
      Initialize(0.0f, 0.0f, 1.0f, 1.0f);
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="T:Crystal.EvaluatorBase"/> class.
    /// </summary>
    /// <param name="ptA">The pt a.</param>
    /// <param name="ptB">The pt b.</param>
    protected EvaluatorBase(Pointf ptA, Pointf ptB) {
      Initialize(ptA.X, ptA.Y, ptB.X, ptB.Y);
    }

    /// <summary>
    ///   Returns the value for the specified x.
    /// </summary>
    float IEvaluator.Evaluate(float x) {
      return IsInverted ? 1f - Evaluate(x) : Evaluate(x);
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

    internal class EvaluatorDxZeroException : Exception {
    }

    internal class EvaluatorXaGreaterThanXbException : Exception {
    }
  }

}