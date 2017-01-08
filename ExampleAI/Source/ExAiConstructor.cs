// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// ExAiConstructor.cs is part of Crystal AI.
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

  internal class ExAiConstructor : AiConstructor {
    protected override void DefineActions() {
      A = new DrinkAction(Actions);
      A = new EatAction(Actions);
      A = new ShowerAction(Actions);
      A = new ToiletAction(Actions);
    }

    protected override void DefineConsiderations() {
      C = new BladderConsideration(Considerations);
      C = new CleanlinessConsideration(Considerations);
      C = new HungerConsideration(Considerations);
      C = new ThirstConsideration(Considerations);

      C = new PrimaryNeedsBehaviourConsideration(Considerations);
      C = new HigherObjectivesBehaviourConsideration(Considerations);
    }

    protected override void DefineOptions() {
    }

    protected override void DefineBehaviours() {
    }

    protected override void ConfigureAi() {
    }

    public ExAiConstructor(IAiCollection collection) : base(collection) {
    }
  }

}