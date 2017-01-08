using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crystal;


namespace ExampleAI {

  public class ReadAction : ActionBase<CharacterContext> {
    public static readonly string Name = "Read";

    public override IAction Clone() {
      return new ReadAction(this);
    }

    protected override void OnExecute(CharacterContext context) {
      context.Character.Report(Name);
      context.Energy -= 1f;
      context.Wealth -= 10f;
      context.Fitness -= 1f;
      EndInSuccess(context);
    }

    protected override void OnUpdate(CharacterContext context) {
    }

    public ReadAction() {
    }

    ReadAction(ReadAction other) : base(other) {
    }

    public ReadAction(IActionCollection collection) : base(Name, collection) {
    }    

  }
}
