// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// AIConstructor.cs is part of Crystal AI.
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
using System.Diagnostics;
using System.Text;


namespace Crystal {

  /// <summary>
  ///   A convenience class used to streamline the process of creating a utility AI.
  /// </summary>
  public abstract class AiConstructor {
    /// <summary>
    ///   Convenience variable for <see cref="T:Crystal.IAction"/>s. This should only be used
    ///   as a temporary variable and nothing more.
    /// </summary>
    protected IAction A;
    /// <summary>
    ///   Convenience variable for <see cref="T:Crystal.IConsideration"/>s. This should only be used
    ///   as a temporary variable and nothing more.
    /// </summary>
    protected IConsideration C;
    /// <summary>
    ///   Convenience variable for <see cref="T:Crystal.ICompositeConsideration"/>s. This should only be used
    ///   as a temporary variable and nothing more.
    /// </summary>
    protected ICompositeConsideration Cc;
    /// <summary>
    ///   Convenience variable for <see cref="T:Crystal.IOption"/>s. This should only be used
    ///   as a temporary variable and nothing more.
    /// </summary>
    protected IOption O;
    /// <summary>
    ///   Convenience variable for <see cref="T:Crystal.IBehaviour"/>s. This should only be used
    ///   as a temporary variable and nothing more.
    /// </summary>
    protected IBehaviour B;
    /// <summary>
    ///   Convenience variable for <see cref="T:Crystal.IUtilityAI"/>s. This should only be used
    ///   as a temporary variable and nothing more.
    /// </summary>
    protected IUtilityAi Ai;

    /// <summary>
    ///   A collection that contains all available <see cref="T:Crystal.IAction"/>s to this
    ///   instance.
    /// </summary>
    public IActionCollection Actions {
      get { return AIs.Actions; }
    }

    /// <summary>
    ///   A collection that contains all available <see cref="T:Crystal.IConsideration"/>s to
    ///   this instance.
    /// </summary>
    public IConsiderationCollection Considerations {
      get { return AIs.Considerations; }
    }

    /// <summary>
    ///   A collection that contains all available <see cref="T:Crystal.IOption"/>s to
    ///   this instance.
    /// </summary>
    public IOptionCollection Options {
      get { return AIs.Options; }
    }

    /// <summary>
    ///   A collection that contains all available <see cref="T:Crystal.IBehaviour"/>s to
    ///   this instance.
    /// </summary>
    public IBehaviourCollection Behaviours {
      get { return AIs.Behaviours; }
    }

    /// <summary>
    ///   A collection that contains all available <see cref="T:Crystal.IUtilityAi"/>s to
    ///   this instance.
    /// </summary>
    public IAiCollection AIs { get; protected set; }


    /// <summary>
    ///   Creates the AI associated with the given identifier; if such an AI does not
    ///   exist this returns <c>null</c>.
    /// </summary>
    /// <param name="aiId">The AI identifier.</param>
    public IUtilityAi Create(string aiId) {
      return AIs.Create(aiId);
    }
    
    /// <summary>
    ///   Determines whether the specified expression is okay.
    /// </summary>
    /// <param name="expression">if set to <c>true</c> [expression].</param>
    /// <exception cref="AiConfigurationxception"></exception>
    protected void IsOkay(bool expression) {
      if(expression == false) {
        // This extracts information from the stack about the calling Class, method and 
        // line number at which initialization failed. This should help locate the offending 
        // code quickly.
        var stack = new StackTrace(true);
        StackFrame frame = stack.GetFrame(1);
        var fileLineNumber = frame.GetFileLineNumber();
        var method = frame.GetMethod();
        var type = method.DeclaringType;
        var name = method.Name;
        var errorMessage = string.Format("{0}.{1}() Line : {2} - Failed to initialize!", type, name, fileLineNumber.ToString());
        Console.WriteLine(errorMessage);
        throw new AiConfigurationxception(errorMessage);
      }        
    }

    /// <summary>
    ///   Instantiate all <see cref="T:Crystal.IAction"/>s used in your AI here.
    ///   Use <see cref="P:Crystal.AiConstructor.Actions"/> to register the newly
    ///   instantiated <see cref="T:Crystal.IAction"/>s to the <see cref="T:Crystal.IActionCollection"/>
    ///   of this <see cref="T:Crystal.AiConsturctor"/>.
    /// </summary>
    protected abstract void DefineActions();

    /// <summary>
    ///   Instantiate all <see cref="T:Crystal.IConsideration"/>s and
    ///   <see cref="T:Crystal.ICompositeConsideration"/>s used in your AI here.
    ///   Use <see cref="P:Crystal.AiConstructor.Considerations"/> to register the newly
    ///   instantiated <see cref="T:Crystal.IConsideration"/>s (or <see cref="T:Crystal.ICompositeConsideration"/>s)
    ///   to the <see cref="T:Crystal.IConsiderationCollection"/> of this <see cref="T:Crystal.AiConsturctor"/>.
    /// </summary>
    protected abstract void DefineConsiderations();

    /// <summary>
    ///   Instantiate all <see cref="T:Crystal.IOption"/>s used in your AI here.
    ///   Use <see cref="P:Crystal.AiConstructor.Options"/> to register the newly
    ///   instantiated <see cref="T:Crystal.IOption"/>s to the <see cref="T:Crystal.IOptionCollection"/>
    ///   of this <see cref="T:Crystal.AiConsturctor"/>.
    /// </summary>
    protected abstract void DefineOptions();

    /// <summary>
    ///   Instantiate all <see cref="T:Crystal.IBehaviour"/>s used in your AI here.
    ///   Use <see cref="P:Crystal.AiConstructor.Behaviours"/> to register the newly
    ///   instantiated <see cref="T:Crystal.IBehaviour"/>s to the <see cref="T:Crystal.IBehaviourCollection"/>
    ///   of this <see cref="T:Crystal.AiConsturctor"/>.
    /// </summary>
    protected abstract void DefineBehaviours();

    /// <summary>
    ///   Instantiate all <see cref="T:Crystal.IUtilityAI"/>s here.
    ///   Use <see cref="P:Crystal.AiConstructor.AIs"/> to register the newly
    ///   instantiated <see cref="T:Crystal.IUtilityAI"/>s to the <see cref="T:Crystal.IAiCollection"/>
    ///   of this <see cref="T:Crystal.AiConsturctor"/>.
    /// </summary>
    protected abstract void ConfigureAi();

    /// <summary>
    ///   Initializes a new instance of the <see cref="AiConstructor"/> class.
    /// </summary>
    /// <param name="collection">The collection.</param>
    /// <exception cref="Crystal.AiConstructor.AiCollectionNullException"></exception>
    protected AiConstructor(IAiCollection collection) {
      if(collection == null)
        throw new AiCollectionNullException();

      AIs = collection;
      Initialize();
    }

    void Initialize() {
      DefineActions();
      DefineConsiderations();
      DefineOptions();
      DefineBehaviours();
      ConfigureAi();
    }

    internal class AiCollectionNullException : Exception {
    }

    internal class AiConfigurationxception : Exception {
      public AiConfigurationxception(string message) : base(message) {
      }
    }
  }

}
