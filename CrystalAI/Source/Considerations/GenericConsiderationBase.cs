// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// GenericConsiderationBase.cs is part of Crystal AI.
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
  ///   Base class for contextual scorers used by <see cref="T:Crystal.ICompositeConsideration"/>s
  /// </summary>
  /// <typeparam name="TContext">The type of the context.</typeparam>
  /// <seealso cref="T:Crystal.IConsideration"/>
  public abstract class ConsiderationBase<TContext> : IConsideration
    where TContext : class, IContext {
    IConsiderationCollection _collection;

    float _weight = 1.0f;

    /// <summary>
    ///   A string alias for ID.
    /// </summary>
    public string NameId { get; set; }

    /// <summary>
    ///   Gets or sets the default utility.
    /// </summary>
    public Utility DefaultUtility { get; set; }

    /// <summary>
    ///   Returns the utility for this consideration.
    /// </summary>
    /// <value>The utility.</value>
    public Utility Utility { get; protected set; }

    /// <summary>
    ///   The weight of this consideration.
    /// </summary>
    public float Weight {
      get { return _weight; }
      set { _weight = value.Clamp01(); }
    }

    /// <summary>Calculates the utility given the specified context.</summary>
    /// <param name="context">The context.</param>
    public abstract void Consider(TContext context);

    public abstract IConsideration Clone();

    protected ConsiderationBase() {
    }

    protected ConsiderationBase(ConsiderationBase<TContext> other) {
      _collection = other._collection;
      NameId = other.NameId;
      DefaultUtility = other.DefaultUtility;
      Utility = other.Utility;
      Weight = other.Weight;
    }

    protected ConsiderationBase(string nameId, IConsiderationCollection collection) {
      if(collection == null)
        throw new ConsiderationCollectionNullException();

      NameId = nameId;
      _collection = collection;
      if(_collection.Add(this) == false)
        throw new ConsiderationAlreadyExistsInCollectionException(nameId);
    }

    void IConsideration.Consider(IContext context) {
      Consider((TContext)context);
    }

    internal class ConsiderationCollectionNullException : Exception {
    }

    internal class ConsiderationAlreadyExistsInCollectionException : Exception {
      string _message;

      public override string Message {
        get { return _message; }
      }

      public ConsiderationAlreadyExistsInCollectionException(string msg) {
        _message = string.Format("{0} already exists in the consideration collection", msg);
      }
    }
  }

}