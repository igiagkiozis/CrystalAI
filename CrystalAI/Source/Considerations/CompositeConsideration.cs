// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// CompositeConsideration.cs is part of Crystal AI.
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
using System.Collections.Generic;
using System.Linq;


namespace Crystal {

  public class CompositeConsideration : ICompositeConsideration {
    IConsiderationCollection _collection;
    List<IConsideration> _considerations;
    List<Utility> _considerationUtilities;

    Utility _defaultUtility = new Utility(0.0f, 1.0f);
    IMeasure _measure;
    float _weight = 1.0f;

    /// <summary>
    ///   A string alias for ID.
    /// </summary>
    public string NameId { get; set; }

    /// <summary>
    ///   The measure to be used to evaluate the utility of this consideration.
    /// </summary>
    public IMeasure Measure {
      get { return _measure; }
      set { _measure = value ?? _measure; }
    }

    /// <summary>
    ///   Gets or sets the default utility.
    /// </summary>
    /// <value>The default utility.</value>
    public Utility DefaultUtility {
      get { return _defaultUtility; }
      set {
        _defaultUtility = value;
        Utility = value;
      }
    }

    /// <summary>
    ///   Returns the combined utility for this consideration.
    /// </summary>
    /// <value>The utility.</value>
    public Utility Utility { get; protected set; }

    /// <summary>
    ///   Gets the weight of this consideration.
    /// </summary>
    public float Weight {
      get { return _weight; }
      set { _weight = value.Clamp01(); }
    }

    public bool AddConsideration(IConsideration consideration) {
      if(consideration == null)
        return false;
      if(_considerations.Contains(consideration))
        return false;
      if(_considerations.Any(c => string.Equals(c.NameId, consideration.NameId)))
        return false;

      InternalAddConsideration(consideration);
      return true;
    }

    public bool AddConsideration(string considerationId) {
      if(_collection == null)
        return false;
      if(string.IsNullOrEmpty(considerationId))
        return false;
      if(_considerations.Any(c => string.Equals(c.NameId, considerationId)))
        return false;
      if(_collection.Contains(considerationId) == false)
        return false;

      InternalAddConsideration(considerationId);
      return true;
    }

    /// <summary>
    ///   Calculates the utility for this option given the provided context.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>The utility of this option.</returns>
    public virtual void Consider(IContext context) {
      if(_considerations.Count == 0)
        return;

      UpdateConsiderationUtilities(context);
      var mValue = Measure.Calculate(_considerationUtilities);
      Utility = new Utility(mValue, Weight);
    }

    public virtual IConsideration Clone() {
      return new CompositeConsideration(this);
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="Crystal.CompositeConsideration"/> class.
    /// </summary>
    public CompositeConsideration() {
      Initialize();
    }

    protected CompositeConsideration(CompositeConsideration other) {
      CreateLists();
      NameId = other.NameId;
      _collection = other._collection;
      _measure = other._measure.Clone();
      _defaultUtility = other._defaultUtility;
      Utility = other.Utility;
      Weight = other.Weight;

      for(int i = 0; i < other._considerations.Count; i++) {
        _considerations.Add(other._considerations[i].Clone());
        _considerationUtilities.Add(other._considerationUtilities[i]);
      }
    }

    protected CompositeConsideration(IConsiderationCollection collection) {
      if(collection == null)
        throw new ConsiderationCollectionNullException();

      _collection = collection;
      Initialize();
    }

    public CompositeConsideration(string nameId, IConsiderationCollection collection) {
      if(string.IsNullOrEmpty(nameId))
        throw new NameIdIsNullOrEmptyException();
      if(collection == null)
        throw new ConsiderationCollectionNullException();

      NameId = nameId;
      _collection = collection;
      Initialize();
      if(_collection.Add(this) == false)
        throw new NameIdAlreadyExistsInCollectionException(nameId);
    }

    void Initialize() {
      Weight = 1.0f;
      _measure = new WeightedMetrics();
      CreateLists();
    }

    void CreateLists() {
      _considerations = new List<IConsideration>();
      _considerationUtilities = new List<Utility>();
    }

    void UpdateConsiderationUtilities(IContext context) {
      for(int i = 0; i < _considerations.Count; i++) {
        _considerations[i].Consider(context);
        _considerationUtilities[i] = _considerations[i].Utility;
      }
    }

    void InternalAddConsideration(IConsideration c) {
      _considerations.Add(c);
      _considerationUtilities.Add(new Utility(0.0f, 0.0f));
    }

    void InternalAddConsideration(string nameId) {
      _considerations.Add(_collection.Create(nameId));
      _considerationUtilities.Add(new Utility(0.0f, 0.0f));
    }

    internal class NameIdIsNullOrEmptyException : Exception {
    }

    internal class ConsiderationCollectionNullException : Exception {
    }

    internal class NameIdAlreadyExistsInCollectionException : Exception {
      string _message;

      public override string Message {
        get { return _message; }
      }

      public NameIdAlreadyExistsInCollectionException(string msg) {
        _message = string.Format("{0} already exists in the consideration collection", msg);
      }
    }
  }

}