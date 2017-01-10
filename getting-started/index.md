---
layout: default
title: Getting Started
navigation_weight: 2
---

## Getting Started

* [Running Tests](#running-tests)
  * [Command Line Mono](#command-line-mono)
  * [MonoDevelop Version Bundled with Unity](#monodevelop-version-bundled-with-unity)
  * [Visual Studio 2015](#visual-studio-2015)
* [Quick Start](#quick-start)
  * [Context](#context)
  * [Considerations and Evaluators](#considerations-and-evaluators)
  * [Actions](#actions)
  * [AiConstructor](#aiconstructor)
  * [Options and Measures](#options-and-measures)
  * [Behaviours](#behaviours)
* [Resources](#resources)
* [Feature Requests and Bugs](#feature-requests-and-bugs)

### Prerequisites
Crystal AI has no external dependencies. If you intend to use it with [Unity](https://unity3d.com/){:target="_blank"} make sure that the API Compatiblity 
Level is set to .NET 2.0 (this almost equivalent to .NET 3.5) and not .NET 2.0 Subset. 

### Installing 
A [NuGet](https://www.nuget.org/){:target="_blank"} package will soon be available, until then, simply drop the CrystalAI.dll into your project directory and link 
to it in your favourite IDE.

### Installing in Unity
Compile the Debug or Release version of Crystal AI and drop in the dll in a folder named Plugins in your Unity directory. 

## Running the Tests
The Crystal AI test suite [CrystalAI.Tests](CrystalAI.Tests){:target="_blank"} depends on NUnit 3.5.0. If you are on Visual Studio you can use 
the [NuGet Package Manager](https://marketplace.visualstudio.com/items?itemName=NuGetTeam.NuGetPackageManagerforVisualStudio2015){:target="_blank"}
to install download it. Otherwise, you can directly download NUnit 3.5.0 from [their NuGet page](https://www.nuget.org/packages/NUnit/){:target="_blank"}. 
You can find documentation on NUnit [here](https://www.nunit.org/){:target="_blank"}. 


### Command Line Mono
To run the unit tests using [Mono](http://www.mono-project.com/){:target="_blank"} cd into the directory you have downloaded Crystal AI into and 
execute the following commands
{% highlight ruby %}
nuget restore CrystalAI.sln
nuget install NUnit.Runners -Version 3.5.0 -OutputDirectory testrunner
xbuild /p:Configuration=Release CrystalAI.sln
mono ./testrunner/NUnit.ConsoleRunner.3.5.0/tools/nunit3-console.exe ./CrystalAI.Tests/bin/Release/CrystalAI.Tests.dll
{% endhighlight %}

### MonoDevelop Version Bundled with Unity
The MonoDevelop version bundled with Unity, although has its problems, is fairly good and obviously has the added benefit of 
seamless integration with Unity. The good part is that Unity has, in their managed folder, NUnit. However, I have not attempted 
to run the unit tests with the Unity version of NUnit. The reason main reason for this is that it is somewhat outdated. That 
shouldn't create difficulties, simply download NUnit 3.5.0, link to it (in the CrystalAI.Tests project) and you 
all the tests should run.

### Visual Studio 2015
To run the unit tests from within Visual Studio, apart from NUnit 3.5.0 you will need to 
install [NUnit3TestAdapter v3.6.0](https://www.nuget.org/packages/NUnit3TestAdapter/){:target="_blank"}.

{% assign IDecisionMaker = "[IDecisionMaker](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Actors/Interfaces/IDecisionMaker.cs)" %}
{% assign IUtilityAI = "[IUtilityAI](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Actors/Interfaces/IUtilityAI.cs)" %}
{% assign IBehaviour = "[IBehaviour](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Behaviours/Interfaces/IBehaviour.cs)" %}
{% assign IOption = "[IOption](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Options/Interfaces/IOption.cs)" %}
{% assign IConsideration = "[IConsideration](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Considerations/Interfaces/IConsideration.cs)" %}
{% assign IAction = "[IAction](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Actions/Interfaces/IAction.cs)" %}
{% assign IContext = "[IContext](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Actors/Interfaces/IContext.cs)" %}
{% assign IContextProvider = "[IContextProvider](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Actors/Interfaces/IContextProvider.cs)" %}
{% assign ConsiderationBase = "[ConsiderationBase](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Considerations/GenericConsiderationBase.cs)" %}
{% assign IEvaluator = "[IEvaluator](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Evaluators/Interfaces/IEvaluator.cs)" %}
{% assign CompositeEvaluator = "[CompositeEvaluator](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Evaluators/CompositeEvaluator.cs)" %}
{% assign LinearEvaluator = "[LinearEvaluator](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Evaluators/LinearEvaluator.cs)" %}
{% assign PowerEvaluator = "[PowerEvaluator](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Evaluators/PowerEvaluator.cs)" %}
{% assign SigmoidEvaluator = "[SigmoidEvaluator](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Evaluators/SigmoidEvaluator.cs)" %}
{% assign Utility = "[Utility](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/General/Utility.cs)" %}
{% assign ActionBase = "[ActionBase](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Actions/GenericActionBase.cs)" %}
{% assign AiConstructor = "[AiConstructor](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/General/AIConstructor.cs)" %}
{% assign IActionCollection = "[IActionCollection](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Actions/Interfaces/IActionCollection.cs)" %}
{% assign IConsiderationCollection = "[IConsiderationCollection](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Considerations/Interfaces/IConsiderationCollection.cs)" %}
{% assign IOptionCollection = "[IOptionCollection](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Options/Interfaces/IOptionCollection.cs)" %}
{% assign IBehaviourCollection = "[IBehaviourCollection](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Behaviours/Interfaces/IBehaviourCollection.cs)" %}
{% assign IAiCollection = "[IAiCollection](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Actors/Interfaces/IAICollection.cs)" %}
{% assign OptionCollection = "[OptionCollection](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Options/OptionCollection.cs)" %}
{% assign BehaviourCollection = "[BehaviourCollection](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Behaviours/BehaviourCollection.cs)" %}
{% assign AiCollection = "[AiCollection](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Actors/AiCollection.cs)" %}
{% assign Option = "[Option](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Options/Option.cs)" %}
{% assign CompositeConsideration = "[CompositeConsideration](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Considerations/CompositeConsideration.cs)" %}
{% assign IMeasure = "[IMeasure](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Measures/Interfaces/IMeasure.cs)" %}
{% assign Chebyshev = "[Chebyshev](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Measures/Chebyshev.cs)" %}
{% assign WeightedMetrics = "[WeightedMetrics](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Measures/WeightedMetrics.cs)" %}
{% assign ConstrainedChebyshev = "[ConstrainedChebyshev](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Measures/ConstrainedChebyshev.cs)" %}
{% assign ConstrainedWeightedMetrics = "[ConstrainedWeightedMetrics](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Measures/ConstrainedWeightedMetrics.cs)" %}
{% assign MultiplicativePseudoMeasure = "[MultiplicativePseudoMeasure](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Measures/MultiplicativePseudoMeasure.cs)" %}
{% assign Scheduler = "[Scheduler](https://github.com/ThelDoctor/CrystalAI/blob/master/CrystalAI/Source/Scheduling/Scheduler.cs)" %}


## Quick Start
The main set of interfaces in Crystal AI are associated as follows. 
A {{ IDecisionMaker }} has a {{ IUtilityAI }} to perform its function. In turn a {{ IUtilityAI }} contains 
a set of {{ IBehaviour }}s, these {{ IBehaviour }}s contain {{ IOption }}s and those {{ IOption }}s have one or more 
{{ IConsideration }}s. When the {{ IConsideration }}s that belong to an {{ IOption }} "win" over other {{ IOption }}s {{ IConsideration }}s, then the {{ IAction }} associated with that {{ IOption }} is executed. 

Another important interface is {{ IContext }}. Implementations of this interface are project dependent and are meant to contain all necessary information for the AI to make decisions. The AI then obtains this context from classes that implement the {{ IContextProvider }} interface. 

### Context
Create a new console project in your favourite IDE, add the CrystalAI.dll and add a link to it.

First we'll need a context for our AI. Add the following class to the project 

{% highlight csharp %}
using Crystal;

public class FooContext : IContext {
  public string Name;
  float _hunger;
  public float Hunger { 
    get { return _hunger; } 
    set { _hunger = value.Clamp(0f, 100f); } 
  }
  float _thirst;
  public float Thirst { 
    get { return _thirst; } 
    set { _thirst = value.Clamp(0f, 100f); } 
  }
  float _bladder;
  public float Bladder { 
    get { return _bladder; } 
    set { _bladder = value.Clamp(0f, 100f); } 
  }

  public FooContext() {
    // Just assign some random starting values to mix things up.
    _hunger = Pcg.Default.NextFloat(0f, 100f);
    _thirst = Pcg.Default.NextFloat(0f, 100f);
    _bladder = Pcg.Default.NextFloat(0f, 100f);
  }
}
{% endhighlight %}

For this example this will also hold all information for the AI controlled objects, our "Toons" in this case. Next, we need a {{ IContextProvider }} 
implementing class
{% highlight csharp %}
using Crystal; 

public class Toon : IContextProvider { 
  // All information related to our toon is stored here. 
  // This need not necessarily be the case in your implementation. 
  // A rule of thumb is for the context to be given access only to
  // information that is required by the AI to make decisions. 
  // More on that later. 
  FooContext _context;

  // IContextProvider implementation
  public IContext Context() {
    return _context;
  }

  public Toon(string name) {
    _context = new FooContext();
    _context.Name = name;
  }
}
{% endhighlight %}

With these classes out of the way, it is time to implementing the AI itself. 

### Considerations and Evaluators

The values within FooContext, in and of themselves, are meaningless. Namely, what does it tell you that a Toon has Hunger of 32, and how does this compare to Thirst and Bladder? To enable comparison between these values we implement the {{ IConsideration }} interface, or more conveniently we can derive from {{ ConsiderationBase }}. 

{% highlight csharp %}
using Crystal;

// This extends the generic version of ConsiderationBase which has the added 
// convenience that the Consider override "knows" about our custom context
// since we've passed it in as a template parameter. The cost is increased 
// level of indirection. But this is a simple example so we're aiming at 
// simplicity, not performance.
public class BladderConsideration : ConsiderationBase<FooContext> {
  IEvaluator _evaluator;
  // This is used as a type Id. Could use reflection, but its ugly... to
  // each his own I suppose. 
  public static readonly string Name = "BladderConsideration";

  public override void Consider(FooContext context) {
    Utility = new Utility(_evaluator.Evaluate(context.Bladder), Weight);
  }

  // This override is essential since this is how different AIs get their
  // own copies of this consideration. I'm not entirely satisfied with the
  // name this has, since this will usually be a *selective* clone of the 
  // original. I haven't a found better name so far, any suggestions would be 
  // welcome.
  public override IConsideration Clone() {
    return new BladderConsideration(this);
  }

  public BladderConsideration() {
    Initialize();
  }

  // A copy constructor must be present in every consideration.
  BladderConsideration(BladderConsideration other) : base(other) {
    Initialize();
  }

  public BladderConsideration(IConsiderationCollection collection)
    : base(Name, collection) {
    Initialize();
  }

  void Initialize() {
    // Point "a" in the interactive plots below.
    var ptA = new Pointf(0f, 0f);
    // Point "b" in the plots below.
    var ptB = new Pointf(100f, 1f);
    // This says that as the value of the Bladder property approaches 100, it 
    // becomes increasingly more important to do something about it. If this 
    // was a LinearEvaluator, that would ignore the sense of urgency, that is
    // quite familiar to everyone with a bladder, to take action when their 
    // bladder is nearly full.
    _evaluator = new PowerEvaluator(ptA, ptB, 3f);
  }
}
{% endhighlight %}

Considerations rely on {{ IEvaluator }}s to translate a given value from the context to the range [0,1]. {{ IEvaluator }}s are essentially functions that have an arbitrary [compact](https://en.wikipedia.org/wiki/Compact_space) interval for their domain and their range is any subinterval of the interval [0,1]. How this conversion is performed exactly is subjective and depends on the application. Crystal AI has three concrete {{ IEvaluator }}s, which are flexible enough and can also be combined to create piecewise functions, see {{ CompositeEvaluator }}. Currently the available evaluators are the following 

* {{ LinearEvaluator }} - [Linear evaluator interactive plot](https://www.desmos.com/calculator/cp2d8hwfju){:target="_blank"}
* {{ PowerEvaluator }} - [Power evaluator interactive plot](https://www.desmos.com/calculator/jpuvzy8pe6){:target="_blank"}
* {{ SigmoidEvaluator }} - [Sigmoid evaluator interactive plot](https://www.desmos.com/calculator/nlnoh2qeq5){:target="_blank"}

Before reading any further it would be good if you experiment with a few different values in the above interactive plots to get a general sense on how the three main evaluators in Crystal AI work. Note that for these plots the domain is limited in the interval [0,1] only to simplify the plot, that of course is not the case for evaluators in Crystal. The points labelled as "a" and "b" in the plots are exactly the same points used to initialise the evaluators, as we did in the BladderConsideration above.

The most important override in a consideration is 

{% highlight csharp %}
public override void Consider(FooContext context) {
  Utility = new Utility(_evaluator.Evaluate(context.Bladder), Weight);
}
{% endhighlight %}

The Consider method uses the appropriate variable (or variables) from the context and with the help of its {{ IEvaluator }} calculates a {{ Utility }}. {{ Utility }} is a struct that has a Value and a Weight and these are constrained to be in the interval [0, 1]. The Value is often evaluated directly using an evaluator. Given that every consideration has a Weight of its own, in most situations it makes sense to pass it to the {{ Utility }} directly as seen above.

We'll need two more considerations for our AI, a HungerConsideration and a ThirstConsideration, their implementations can be found in the [Crystal AI quick start project.](https://github.com/ThelDoctor/CrystalAIQuickStart)


### Actions  

Now that we have a way to transform information from the context to utilities next are {{ IAction }}s. All actions should extend {{ ActionBase }}. Actions implement the problem dependent logic for the game. Once an action is selected the first method that is executed is ```OnExecute(IContext context)```, if there is need to perform additional operations then these can be performed in ```OnUpdate(IContext context)```. For an action to stop execution, either ```EndInSuccess(IContext context)``` or ```EndInFailure(IContext context)``` must be called. Once either of these functions is called ```OnStop(IContext context)``` is executed where any final clean up actions are executed.  

Now lets look at the implementation of the ToiletAction
{% highlight csharp %}
public class ToiletAction : ActionBase<FooContext> {
  public static readonly string Name = "Toilet";

  public override IAction Clone() {
    return new ToiletAction(this);
  }

  protected override void OnExecute(FooContext context) {
    context.Report(Name);
    // As we get older this value gets smaller until such time when we
    // exercise vigorously by the increased frequency of our trips to the loo ;) 
    context.Bladder -= 90f;
    context.Hunger += 25f;
    // If the action ends here we must call EndInSucces or EndInFailure. 
    EndInSuccess(context);
  }

  public ToiletAction() {
  }

  ToiletAction(ToiletAction other) : base(other) {
  }

  public ToiletAction(IActionCollection collection) : base(Name, collection) {
  }
}
{% endhighlight %}
There are three more actions defined for this example ```IdleAction, EatAction and DrinkAction```, all of which are similar enough so they are not reproduced here. You can find these [here](https://github.com/ThelDoctor/CrystalAIQuickStart/tree/master/CrystalQuickStart/Source/Actions).


### AiConstructor  

Now that all considerations and actions for our example are in place it is time for all the pieces to come together into an AI. A convenience class created for this purpose is {{ AiConstructor }}. {{ AiConstructor }} has five protected abstract functions that are executed in order initializing the AI building blocks. Once actions, considerations, options, behaviours and AIs are instantiated in a concrete {{ AiConstructor }} there is no longer the need instantiate directly any of the AI components. All components (i.e. actions, considerations etc.) are named and that name should be unique. The {{ AiConstructor }} has some convenience temporary variables (e.g. A for actions, C for considerations etc.) and a checking function ```IsOkay(bool expression)``` whose use is not required but is highly advisable since it throw an exception if the initialization process fails at some point which is useful for debugging.  

First we create an instance of all actions and considerations. These are then stored in a {{ IActionCollection }} and {{ IConsiderationCollection }} respectively. Actions, considerations, options, behaviours and AIs all have their individual collections. These are created by the ```AiCollectionConstructor.Create()``` method in ```QsAiConstructor``` (see below). These collections are "weaved" together to create the {{ AiCollection }}. Namely, the {{ OptionCollection }} requires for its construction an {{ ActionCollection }} and  a {{ ConsiderationCollection }}. The {{ BehaviourCollection }} needs an {{ OptionCollection }} and the {{ AiCollection }} needs a {{ BehaviourCollection }}. In the abstract class {{ AiConstructor }} these collections are assigned to the following protected fields

* ```Actions``` - {{ IActionCollection }}
* ```Considerations``` - {{ IConsiderationCollection }}
* ```Options``` - {{ IOptionCollection }}
* ```Behaviours``` - {{ IBehaviourCollection }}
* ```AIs``` - {{ IAiCollection }}

By passing these collections to the actions, considerations (etc.) constructors a prototype of the newly instantiated object is passed to the appropriate collection. For actions and considerations in our example this is accomplished as follows

{% highlight csharp %}
public class QsAiConstructor : AiConstructor {
  protected override void DefineActions() {
    A = new DrinkAction(Actions);
    A = new EatAction(Actions);
    A = new ToiletAction(Actions);
    A = new IdleAction(Actions);
  }

  protected override void DefineConsiderations() {
    C = new BladderConsideration(Considerations);
    C = new HungerConsideration(Considerations);
    C = new ThirstConsideration(Considerations);
  }
{% endhighlight %}


### Options and Measures

Next in line are {{ IOption }}s. As mentioned options can have one or more considerations and at least one action. If you have a look at the implementation of {{ Option }} you will notice that there is a property named ```Measure```. This returns an {{ IMeasure }} which is similar to the normal mathematical definition of a measure ([see Wikipedia](https://en.wikipedia.org/wiki/Measure_(mathematics))). However, to allow for flexibility not all implementing classes of {{ IMeasure }} are measures in the strict mathematical sense but have their use. So what is it that implementing classes of {{ IMeasure }} do? As was mentioned, {{ Option }}s have one or more considerations, and each of these considerations has a {{ Utility }}. Now, if there is more than of these considerations, how do we decide which {{ Option }}s is best? That's the job of these {{ IMeasure }}s. That is, they accept a vector of utilities and return a single floating point value. That value is assigned to the {{ Utility }}.Value of the given {{ Option }}, thus enabling comparison between options. At this point a warning is in order, some implementations of utility AIs by game developers, use what is called in Crystal AI the {{ MultiplicativePseudoMeasure }} to perform this operation. From a mathematical point of view that doesn't make sense. That, in and of itself, doesn't mean that this pseudo-measure shouldn't be used or that it is somehow inherently a bad practice. However, due to the fact that this pseudo-measure multiplies the elements of the vector of utilities, it exhibits some peculiar behaviour for vectors of different length. For example, ignoring the weight in utilities for the time being, let's say that you have a vector of utilities that is ```(0.9, 0.9)``` and another that belongs to a different option that is ```(0.92, 0.92, 0.92)```. Using the {{ MultiplicativePseudoMeasure }} on these two vectors will result in a final value for the first equal to ```0.81``` and ```0.77``` for the second. This means that the first option will be selected ignoring the fact that each of its considerations individually have lower utility compared with the second option. This is counter intuitive, and because of this, personally I would avoid this pseudo-measure. Nevertheless, it appears to be in common use so  an implementation is available for completeness. If you have insights on the reasons behind the use of this particular pseudo-measure in the game development industry I would love to hear them!

The {{ IMeasure }} implementations available in Crystal AI are the following 

* ```Chebyshev``` - See {{ Chebyshev }} 
* ```WeightedMetrics``` - See {{ WeightedMetrics }}
* ```ConstrainedChebyshev``` - See {{ ConstrainedChebyshev }}
* ```ConstrainedWeightedMetrics``` - See {{ ConstrainedWeightedMetrics }}
* ```MultiplicativePseudoMeasure``` - See {{ MultiplicativePseudoMeasure }}

The default for {{ Option }}s is {{ WeightedMetrics }} which can be changed as ```O.Measure = new SomeMeasure()```. A note on the constrained versions of the Chebyshev and the weighted metrics measures. The constraint in these is that for them to return a non-zero result all the utilities in passed to the measure should be above a certain threshold, otherwise they behave exactly like {{ Chebyshev }} and {{ WeightedMetrics }}.

{% highlight csharp %}
  protected override void DefineOptions() {
    O = new Option("Drink", Options);
    IsOkay(O.SetAction(DrinkAction.Name));
    IsOkay(O.AddConsideration(ThirstConsideration.Name));

    O = new Option("Eat", Options);
    IsOkay(O.SetAction(EatAction.Name));
    IsOkay(O.AddConsideration(HungerConsideration.Name));

    O = new Option("Toilet", Options);
    IsOkay(O.SetAction(ToiletAction.Name));
    IsOkay(O.AddConsideration(BladderConsideration.Name));

    O = new ConstantUtilityOption("Idle", Options);
    IsOkay(O.SetAction(IdleAction.Name));
    O.DefaultUtility = new Utility(0.01f, 1f);
  }
{% endhighlight %}  


### Behaviours

A behaviour in Crystal is a collection of options. This can be useful when characters in a game cannot, or does not make sense, to perform a particular set of actions in every context. For example, let's say our character enters a pub, the ```pub``` may give the character a new behaviour with the following options ```Take part in quiz night, Play a table game, Have a pint(!) etc.```, once our character exits the pub it makes little sense to keep evaluating those options. 

{% highlight csharp %}
  protected override void DefineBehaviours() {
    B = new Behaviour("DefaultBehaviour", Behaviours);
    IsOkay(B.AddOption("Drink"));
    IsOkay(B.AddOption("Eat"));
    IsOkay(B.AddOption("Toilet"));
    IsOkay(B.AddOption("Idle"));
  }

  protected override void ConfigureAi() {
    Ai = new UtilityAi("QuickStartAi", AIs);
    IsOkay(Ai.AddBehaviour("DefaultBehaviour"));
  }

  public QsAiConstructor() : base(AiCollectionConstructor.Create()) {
  }
}
{% endhighlight %}

Right, now that we have our AI, let's put it to work! Mostly everything in the ```Main()``` method should be straightforward. The new thing here is the {{ Scheduler }}. This is internally has two priority queues, one the think cycle and one for the update cycle. Decisions are made during the think cycle and in the update cycle any ```OnUpdate(...)``` methods of actions are executed. The {{ Scheduler }}s job is to balance the workload on the given thread and limit it to a given number of milliseconds. So for example you could create ```400000``` characters ("toons") and the {{ Scheduler }} will do just fine, just don't forget to ```bool verbose = false;``` is you set ```N``` to such a high value. A warning! This doesn't mean that the AI is processing all ```400000``` AIs per update, but that it processes as many as it can (or just the ones that need updating) in the allotted time. That said, Crystal can actually handle a fairly large number of AIs. In the upcoming weeks I'll share some benchmarking results.


{% highlight csharp %}
    public static void Main() {
      // Try with some higher values. Just, if you set this to higher than say 100-200,
      // don't forget to set the verbose variable to false, otherwise Console.Write(..)
      // will slow down things to a halt. 
      int N = 4; // 400000;
      bool verbose = true;

      var toons = new List<Toon>();
      var decisionMakers = new List<IDecisionMaker>();
      var aiConstructor = new QsAiConstructor();
      var scheduler = new Scheduler();
      var tStream = scheduler.ThinkStream as CommandStream;
      var uStream = scheduler.UpdateStream as CommandStream;
      tStream.MaxProcessingTime = 4.0;
      uStream.MaxProcessingTime = 0.01;

      // Toon creation loop
      for(int i = 0; i < N; i++) {
        var toon = new Toon(string.Format("Toon {0}", i));
        var dm = new DecisionMaker(aiConstructor.Create("QuickStartAi"), toon, scheduler) {
          // Every AI will be updated 4-5 times per second with these settings. 
          // If you need this to me more or less often modify the delays accordingly. 
          ThinkDelayMin = 0.2f,
          ThinkDelayMax = 0.25f
        };


        toons.Add(toon);
        decisionMakers.Add(dm);
        dm.Start();
      }

      // Simulation loop
      Console.WriteLine("Entering simulation loop");
      float factor = 1.0f / 0.017f;
      var procPerSecMa = new MovingAverage(590);
      Stopwatch w = new Stopwatch();
      long itCount = 0;
      while(true) {
        var sb = new StringBuilder();
        w.Reset();
        w.Start();
        scheduler.Tick();
        itCount++;
        w.Stop();

        if(verbose)
          for(int i = 0; i < N; i++)
            sb.AppendLine(toons[i].Context().ToString());

        procPerSecMa.Enqueue(tStream.ProcessedCount);
        var stats = string.Format("Frame {0}, total time in milliseconds {1:0.00}, processed # {2}, proc/sec {3}",
                                 itCount, w.Elapsed.TotalMilliseconds, 
                                 tStream.ProcessedCount, 
                                 procPerSecMa.Mean * factor );
        sb.AppendLine(stats);
        Console.SetCursorPosition(0, 0);
        Console.Write(sb);

        // Just a crude way to simulate a game engine update loop at ~58.8 fps. (In case you're wondering 
        // why such a strange number, this just so that we have to deal only with ints below)
        if(w.ElapsedMilliseconds >= 17)
          continue;

        int dt = 17 - (int)w.ElapsedMilliseconds;
        Thread.Sleep(dt);
      }

    }
{% endhighlight %}

All the files for this example can be [downloaded from here.](https://github.com/ThelDoctor/CrystalAIQuickStart). As Crystal moves closer to version 1.0 the in code documentation will increase accordingly. After version 1.0 we'll be adopting semantic versioning. 

## Resources

If you've never heard of Utility AI and you would like to know more about it these resources should help get you started

* [Behavioural Mathematics for Game AI by David Mark](https://www.amazon.com/Behavioral-Mathematics-Game-AI-Applied/dp/1584506849){:target="_blank"}
* [Introducing GAIA: A Reusable, Extensible Architecture for AI Behavior, by Kevin Dill](https://www.sisostds.org/DesktopModules/Bring2mind/DMX/Download.aspx?Command=Core_Download&EntryId=35466&PortalId=0&TabId=105){:target="_blank"}
* [Improving AI Decision Modeling Through Utility Theory by Dave Mark and Kevin Dill (Talk, GDC2010)](http://www.gdcvault.com/play/1012410/Improving-AI-Decision-Modeling-Through){:target="_blank"}
* [Embracing the Dark Art of Mathematical Modeling in Game AI by Dave Mark and Kevin Dill (Talk, GDC 2012)](http://www.gdcvault.com/play/1015683/Embracing-the-Dark-Art-of){:target="_blank"}

This obiously is not an exhaustive list, and it wasn't meant to be. However, if I've missed your favourite Utility AI resource 
let me know.


## Feature Requests and Bugs
If you find any bugs in Crystal AI, or simply have an awesome suggestion, we'd love to hear about it. After all, squashing bugs is fun!
The best way to report a bug or a feature request is via the GitHub Issue system [here](https://github.com/ThelDoctor/CrystalAI/issues){:target="_blank"}. For discussing anything Crystal you can [visit our forums](http://www.bismur.co.uk/forums/index.php){:target="_blank"}.