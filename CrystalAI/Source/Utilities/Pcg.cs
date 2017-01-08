// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// Pcg.cs is part of Crystal AI.
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


// Note, you will see that API is duplicated in PCG and PCGExtended, this is not by accident or 
// neglect. It would be "cleaner" to have a base class and just implement the core of the generator
// in derived classes, however, this has a performance cost which for random number generators, in my
// view, is too high. That said, there is still some room for performance optimisation.


namespace Crystal {

  /// <summary>
  ///   PCG (Permuted Congruential Generator) is a C# port from C the base PCG generator
  ///   presented in "PCG: A Family of Simple Fast Space-Efficient Statistically Good
  ///   Algorithms for Random Number Generation" by Melissa E. O'Neill. The code follows closely the one
  ///   made available by O'Neill at her site: http://www.pcg-random.org/download.html
  ///   To understand how exactly this generator works read this:
  ///   http://www.pcg-random.org/pdf/toms-oneill-pcg-family-v1.02.pdf
  /// </summary>
  public class Pcg {
    // This attribute ensures that every thread will get its own instance of PCG.
    // An alternative, since PCG supports streams, is to use a different stream per
    // thread. 
    [ThreadStatic] static Pcg _defaultInstance;
    ulong _increment = 1442695040888963407ul;
    ulong _state;

    /// <summary>
    ///   Default instance.
    /// </summary>
    public static Pcg Default => _defaultInstance ?? (_defaultInstance = new Pcg(PcgSeed.GuidBasedSeed()));

    public virtual int Next() {
      uint result = NextUInt();
      return (int)(result >> 1);
    }

    public virtual int Next(int maxExclusive) {
      if(maxExclusive <= 0)
        throw new ArgumentException("Max Exclusive must be positive");

      uint uMaxExclusive = (uint)maxExclusive;
      uint threshold = (uint)-uMaxExclusive % uMaxExclusive;

      while(true) {
        uint result = NextUInt();
        if(result >= threshold)
          return (int)(result % uMaxExclusive);
      }
    }

    public virtual int Next(int minInclusive, int maxExclusive) {
      if(maxExclusive <= minInclusive)
        throw new ArgumentException("MaxExclusive must be larger than MinInclusive");

      uint uMaxExclusive = unchecked((uint)(maxExclusive - minInclusive));
      uint threshold = (uint)-uMaxExclusive % uMaxExclusive;

      while(true) {
        uint result = NextUInt();
        if(result >= threshold)
          return (int)unchecked(result % uMaxExclusive + minInclusive);
      }
    }

    public int[] NextInts(int count) {
      if(count <= 0)
        throw new ArgumentException("Zero count");

      var resultA = new int[count];
      for(var i = 0; i < count; i++)
        resultA[i] = Next();

      return resultA;
    }

    public int[] NextInts(int count, int maxExclusive) {
      if(count <= 0)
        throw new ArgumentException("Zero count");

      var resultA = new int[count];
      for(int i = 0; i < count; i++)
        resultA[i] = Next(maxExclusive);

      return resultA;
    }

    public int[] NextInts(int count, int minInclusive, int maxExclusive) {
      if(count <= 0)
        throw new ArgumentException("Zero count");

      var resultA = new int[count];
      for(int i = 0; i < count; i++)
        resultA[i] = Next(minInclusive, maxExclusive);

      return resultA;
    }

    public uint NextUInt() {
      ulong oldState = _state;
      _state = unchecked(oldState * Multiplier + _increment);
      uint xorShifted = (uint)(((oldState >> 18) ^ oldState) >> 27);
      int rot = (int)(oldState >> 59);
      uint result = (xorShifted >> rot) | (xorShifted << (-rot & 31));
      return result;
    }

    public uint NextUInt(uint maxExclusive) {
      uint threshold = (uint)-maxExclusive % maxExclusive;

      while(true) {
        uint result = NextUInt();
        if(result >= threshold)
          return result % maxExclusive;
      }
    }

    public uint NextUInt(uint minInclusive, uint maxExclusive) {
      if(maxExclusive <= minInclusive)
        throw new ArgumentException();

      uint diff = maxExclusive - minInclusive;
      uint threshold = (uint)-diff % diff;

      while(true) {
        uint result = NextUInt();
        if(result >= threshold)
          return result % diff + minInclusive;
      }
    }

    public uint[] NextUInts(int count) {
      if(count <= 0)
        throw new ArgumentException("Zero count");

      var resultA = new uint[count];
      for(int i = 0; i < count; i++)
        resultA[i] = NextUInt();

      return resultA;
    }

    public uint[] NextUInts(int count, uint maxExclusive) {
      if(count <= 0)
        throw new ArgumentException("Zero count");

      var resultA = new uint[count];
      for(int i = 0; i < count; i++)
        resultA[i] = NextUInt(maxExclusive);

      return resultA;
    }

    public uint[] NextUInts(int count, uint minInclusive, uint maxExclusive) {
      if(count <= 0)
        throw new ArgumentException("Zero count");

      var resultA = new uint[count];
      for(int i = 0; i < count; i++)
        resultA[i] = NextUInt(minInclusive, maxExclusive);

      return resultA;
    }

    public virtual float NextFloat() {
      return (float)(NextUInt() * ToDouble01);
    }

    public virtual float NextFloat(float maxInclusive) {
      if(maxInclusive <= 0)
        throw new ArgumentException("MaxInclusive must be larger than 0");

      return (float)(NextUInt() * ToDouble01) * maxInclusive;
    }

    public virtual float NextFloat(float minInclusive, float maxInclusive) {
      if(maxInclusive <= minInclusive)
        throw new ArgumentException("Max must be larger than min");

      return (float)(NextUInt() * ToDouble01) * (maxInclusive - minInclusive) + minInclusive;
    }

    public float[] NextFloats(int count) {
      if(count <= 0)
        throw new ArgumentException("Zero count");

      var resultA = new float[count];
      for(int i = 0; i < count; i++)
        resultA[i] = NextFloat();

      return resultA;
    }

    public float[] NextFloats(int count, float maxInclusive) {
      if(count <= 0)
        throw new ArgumentException("Zero count");

      var resultA = new float[count];
      for(int i = 0; i < count; i++)
        resultA[i] = NextFloat(maxInclusive);

      return resultA;
    }

    public float[] NextFloats(int count, float minInclusive, float maxInclusive) {
      if(count <= 0)
        throw new ArgumentException("Zero count");

      var resultA = new float[count];
      for(int i = 0; i < count; i++)
        resultA[i] = NextFloat(minInclusive, maxInclusive);

      return resultA;
    }

    public virtual double NextDouble() {
      return NextUInt() * ToDouble01;
    }

    public virtual double NextDouble(double maxInclusive) {
      if(maxInclusive <= 0)
        throw new ArgumentException("Max must be larger than 0");

      return NextUInt() * ToDouble01 * maxInclusive;
    }

    public virtual double NextDouble(double minInclusive, double maxInclusive) {
      if(maxInclusive <= minInclusive)
        throw new ArgumentException("Max must be larger than min");

      return NextUInt() * ToDouble01 * (maxInclusive - minInclusive) + minInclusive;
    }

    public double[] NextDoubles(int count) {
      if(count <= 0)
        throw new ArgumentException("Zero count");

      var resultA = new double[count];
      for(int i = 0; i < count; i++)
        resultA[i] = NextDouble();

      return resultA;
    }

    public double[] NextDoubles(int count, double maxInclusive) {
      if(count <= 0)
        throw new ArgumentException("Zero count");

      var resultA = new double[count];
      for(int i = 0; i < count; i++)
        resultA[i] = NextDouble(maxInclusive);

      return resultA;
    }

    public double[] NextDoubles(int count, double minInclusive, double maxInclusive) {
      if(count <= 0)
        throw new ArgumentException("Zero count");

      var resultA = new double[count];
      for(int i = 0; i < count; i++)
        resultA[i] = NextDouble(minInclusive, maxInclusive);

      return resultA;
    }

    public byte NextByte() {
      var result = NextUInt();
      return (byte)(result % 256);
    }

    public byte[] NextBytes(int count) {
      if(count <= 0)
        throw new ArgumentException("Zero count");

      var resultA = new byte[count];
      for(var i = 0; i < count; i++)
        resultA[i] = NextByte();

      return resultA;
    }

    public bool NextBool() {
      var result = NextUInt();
      return result % 2 == 1;
    }

    public bool[] NextBools(int count) {
      if(count <= 0)
        throw new ArgumentException("Zero count");

      var resultA = new bool[count];
      for(var i = 0; i < count; i++)
        resultA[i] = NextBool();

      return resultA;
    }

    public int PeriodPow2() {
      return 64;
    }

    public void SetStream(int sequence) {
      SetStream((ulong)sequence);
    }

    public void SetStream(ulong sequence) {
      _increment = (sequence << 1) | 1;
    }

    public ulong CurrentStream() {
      return _increment >> 1;
    }

    public Pcg() : this(PcgSeed.GuidBasedSeed()) {
    }

    public Pcg(int seed) : this((ulong)seed) {
    }

    public Pcg(int seed, int sequence) : this((ulong)seed, (ulong)sequence) {
    }

    public Pcg(ulong seed, ulong sequence = ShiftedIncrement) {
      Initialize(seed, sequence);
    }

    void Initialize(ulong seed, ulong initseq) {
      _state = 0ul;
      SetStream(initseq);
      NextUInt();
      _state += seed;
      NextUInt();
    }

    // This shifted to the left and or'ed with 1ul results in the default increment.
    const ulong ShiftedIncrement = 721347520444481703ul;
    const ulong Multiplier = 6364136223846793005ul;
    const double ToDouble01 = 1.0 / 4294967296.0;
  }

}