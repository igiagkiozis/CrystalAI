// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// RandomHelpers.cs is part of Crystal AI.
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
using System.IO;


namespace Crystal.UtilitiesTests {

  public static class RandomHelpers {
    public static List<uint> ReadPcgOutput(int seed) {
      var dir = Path.GetDirectoryName(typeof(RandomHelpers).Assembly.Location);
      var fileName = Path.Combine(dir, Path.Combine("Data", string.Format("pcg32_seed_{0}.txt", seed)));
      return ReadPcgValuesFile(fileName);
    }

    public static List<uint> ReadPcgExtendedOutput(int seed, int tablePow2, int advancePow2) {
      var dir = Path.GetDirectoryName(typeof(RandomHelpers).Assembly.Location);
      var dataFile = string.Format("pcg32_k_table_pow2_{0}_advance_pow2_{1}_seed_{2}.txt", tablePow2, advancePow2, seed);
      var fileName = Path.Combine(dir, Path.Combine("Data", dataFile));
      return ReadPcgValuesFile(fileName);
    }

    static List<uint> ReadPcgValuesFile(string fileName) {
      var list = new List<uint>();
      try {
        using(TextReader reader = File.OpenText(fileName))
          do {
            var uintString = reader.ReadLine();
            if(string.IsNullOrEmpty(uintString))
              break;

            var x = uint.Parse(uintString);
            list.Add(x);
          } while(true);
      } catch {
        Console.WriteLine("File {0}, does not exist!", fileName);
      }

      return list;
    }
  }

}