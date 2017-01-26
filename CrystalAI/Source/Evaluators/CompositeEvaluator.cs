// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// CompositeEvaluator.cs is part of Crystal AI.
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
  ///   The CompositeEvaluator class allows the composition, or combination, of one or
  ///   more <see cref="T:Crystal.IEvaluator"/>s whose domain of definition does not overlap!
  ///   Note that addition of an <see cref="T:Crystal.IEvaluator"/> that has a domain of definition
  ///   that is covered (even in part) by another <see cref="T:Crystal.IEvaluator"/> already
  ///   added to the CompositeEvaluator, will not be added and will have no effect in the
  ///   evaluation process.
  /// </summary>
  /// <seealso cref="Crystal.EvaluatorBase"/>
  public class CompositeEvaluator : EvaluatorBase {
    /// <summary>
    ///   Returns the value for the specified x. Note that if the given x value does not fall
    ///   within the domain of definition of any of the <see cref="T:Crystal.IEvaluator"/>s of this
    ///   <see cref="T:Crystal.CompositeEvaluator"/> then one of two things will happen depending
    ///   on the value x itself.
    ///   1. If x is within the extended interval defined by the evaluator's lower and upper bounds,
    ///   then the return value will be linearly interpolated using the values of the <see cref="T:Crystal.IEvaluator"/>
    ///   whose domain is to the "left" (i.e. all values of the domain are smaller than x) of the
    ///   value x and the <see cref="T:Crystal.IEvaluator"/> to the "right" of x.
    ///   2. If x is outside the extended interval defined by the evaluator's lower and upper bounds,
    ///   then the Evaluate method will derive its output from the <see cref="T:Crystal.IEvaluator"/>
    ///   whose lower or upper bound is closest to the value x.
    /// </summary>
    public override float Evaluate(float x) {
      var ev = FindEvaluator(x);
      // if ev is null then there is a "hole" in the XInterval, in which case linear 
      // interpolation is used.
      return ev != null ? ev.Evaluate(x) : LinearHoleInterpolator(x);
    }

    /// <summary>
    ///   Adds the specified <see cref="T:Crystal.IEvaluator"/> to this <see cref="T:Crystal.CompositeEvaluator"/>
    ///   if the new evaluator has a domain that does not overlap the domains of definition of the
    ///   <see cref="T:Crystal.IEvaluator"/> already added to this <see cref="T:Crystal.CompositeEvaluator"/>.
    /// </summary>
    /// <param name="ev">Ev.</param>
    public void Add(IEvaluator ev) {
      if(DoesNotOverlapWithAnyEvaluator(ev))
        Evaluators.Add(ev);

      Evaluators.Sort((e1, e2) => e1.XInterval.CompareTo(e2.XInterval));
      UpdateXyPoints();
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="CompositeEvaluator"/> class.
    /// </summary>
    public CompositeEvaluator() {
      Evaluators = new List<IEvaluator>();
    }

    bool DoesNotOverlapWithAnyEvaluator(IEvaluator ev) {
      foreach(var cev in Evaluators)
        if(ev.XInterval.Overlaps(cev.XInterval))
          if(ev.XInterval.Adjacent(cev.XInterval))
            continue;
          else
            return false;

      return true;
    }

    void UpdateXyPoints() {
      var count = Evaluators.Count;
      if(count == 1)
        SingleEvaluatorXyPointsUpdate();
      else
        MultiEvaluatorXyPointsUpdate();
    }

    void SingleEvaluatorXyPointsUpdate() {
      Xa = Evaluators[0].PtA.X;
      Ya = Evaluators[0].PtA.Y;
      Xb = Evaluators[0].PtB.X;
      Yb = Evaluators[0].PtB.Y;
    }

    void MultiEvaluatorXyPointsUpdate() {
      foreach(var ev in Evaluators) {
        if(Xa >= ev.MinX) {
          Xa = ev.MinX;
          Ya = ev.PtA.Y;
        }
        if(Xb <= ev.MaxX) {
          Xb = ev.MaxX;
          Yb = ev.PtB.Y;
        }
      }
    }

    /// <summary>
    ///   Attempts to find an evaluator. The only Time this will return null is when
    ///   the value x is within the XInterval of the composite evaluator but there is
    ///   no evaluator within the interval that contains x.
    /// </summary>
    IEvaluator FindEvaluator(float x) {
      int evCount = Evaluators.Count;
      if(x.InInterval(XInterval))
        return FindInternalEvaluator(x);

      if(x.AboveInterval(XInterval))
        return Evaluators[evCount - 1];

      return x.BelowInterval(XInterval) ? Evaluators[0] : null;
    }

    IEvaluator FindInternalEvaluator(float x) {
      int evCount = Evaluators.Count;
      for(int i = 0; i < evCount; i++)
        if(x.InInterval(Evaluators[i].XInterval))
          return Evaluators[i];
      // x is in a "hole"
      return null;
    }

    float LinearHoleInterpolator(float x) {
      var lrev = FindLeftAndRightInterpolators(x);
      var xl = lrev.Key.MaxX;
      var yl = lrev.Key.Evaluate(xl);
      var xr = lrev.Value.MinX;
      var yr = lrev.Value.Evaluate(xr);
      var alpha = (x - xl) / (xr - xl);
      return yl + alpha * (yr - yl);
    }

    KeyValuePair<IEvaluator, IEvaluator> FindLeftAndRightInterpolators(float x) {
      int evCount = Evaluators.Count;
      IEvaluator lev = null;
      IEvaluator rev = null;
      for(int i = 0; i < evCount - 1; i++) {
        lev = Evaluators[i];
        rev = Evaluators[i + 1];
        if(x.AboveInterval(lev.XInterval) &&
           x.BelowInterval(rev.XInterval))
          break;
      }

      return new KeyValuePair<IEvaluator, IEvaluator>(lev, rev);
    }

    internal List<IEvaluator> Evaluators;
  }

}