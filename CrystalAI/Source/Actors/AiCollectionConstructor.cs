// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// AiCollectionConstructor.cs is part of Crystal AI.
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

  public static class AiCollectionConstructor {
    /// <summary>
    ///   Creates all necessary collections for the AI. Note that the collections created by this
    ///   will in most instances be unique. If however, for some reason, there is the need for different
    ///   AI systems, then these should have separate collections.
    /// </summary>
    /// <returns></returns>
    public static IAiCollection Create() {
      var a = new ActionCollection();
      var c = new ConsiderationCollection();
      var o = new OptionCollection(a, c);
      var b = new BehaviourCollection(o);
      return new AiCollection(b);
    }
  }

}