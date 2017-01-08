// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// CharacterContext.cs is part of Crystal AI.
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
using Crystal;


namespace ExampleAI {

  public class CharacterContext : IContext {
    float _bladder;
    float _thirst;
    float _hunger;
    float _cleanliness;
    float _sleepiness;

    public float Wealth;

    public float Bladder {
      get { return _bladder; }
      set { _bladder = value.Clamp(0f, 100f); }
    }

    public float Thirst {
      get { return _thirst; }
      set { _thirst = value.Clamp(0f, 100); }
    }

    public float Hunger {
      get { return _hunger; }
      set { _hunger = value.Clamp(0f, 100); }
    }

    public float Cleanliness {
      get { return _cleanliness; }
      set { _cleanliness = value.Clamp(0f, 100f); }
    }

    public float Sleepiness {
      get { return _sleepiness; }
      set { _sleepiness = value.Clamp(0f, 100f); }
    }

    public CharacterContext() {
      _bladder = 0f;
      _thirst = 0f;
      _hunger = 0f;
      _sleepiness = 0f;
      _cleanliness = 100f;
      Wealth = 1000f;
    }
  }

}