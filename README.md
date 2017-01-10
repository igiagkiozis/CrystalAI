[![license](https://img.shields.io/badge/license-GPL-brightgreen.svg?style=flat)](https://github.com/ThelDoctor/CrystalAI/blob/master/LICENSE)
[![Build Status](https://travis-ci.org/ThelDoctor/CrystalAI.svg?branch=master)](https://travis-ci.org/ThelDoctor/CrystalAI)
[![Build status](https://ci.appveyor.com/api/projects/status/rw0tma0eucs45fi5/branch/master?svg=true)](https://ci.appveyor.com/project/ThelDoctor/crystalai/branch/master)
![Tests Status](http://flauschig.ch/batch.php?type=tests&account=ThelDoctor&slug=CrystalAI)

# Crystal AI Utility Theory Based AI for C# and Unity

Crystal is a general purpose decision making AI for C# that is based on concepts in utility theory. Utility theory has 
its roots in economics and game theory. Loosely, it can be defined as a set of mathematical recipes that 
when used with discipline enable you to objectively evaluate a set of alternative courses of action. Characters
in games have plenty of choices to make and a utility based framework, at least to me, is the most intuitive one when compared 
with the alternatives (e.g. Behaviour Trees, or Finite State Machines and others), hence Crystal AI. That is not to say
that BTs and FSMs don't have their uses, or that they are in any way inferior to utility based AIs, its just a personal 
preference that's all. 

If you've never heard of Utility AI and you would like to know more about it these resources should help get you started
- [Behavioural Mathematics for Game AI by David Mark](https://www.amazon.com/Behavioral-Mathematics-Game-AI-Applied/dp/1584506849)
- [Introducing GAIA: A Reusable, Extensible Architecture for AI Behavior, by Kevin Dill](https://www.sisostds.org/DesktopModules/Bring2mind/DMX/Download.aspx?Command=Core_Download&EntryId=35466&PortalId=0&TabId=105)
- [Improving AI Decision Modeling Through Utility Theory by Dave Mark and Kevin Dill (Talk, GDC2010)](http://www.gdcvault.com/play/1012410/Improving-AI-Decision-Modeling-Through)
- [Embracing the Dark Art of Mathematical Modeling in Game AI by Dave Mark and Kevin Dill (Talk, GDC 2012)](http://www.gdcvault.com/play/1015683/Embracing-the-Dark-Art-of)

This obiously is not an exhaustive list, and it wasn't meant to be. However, if I've missed your favourite Utility AI resource 
let me know.

## Getting Started
Keep an eye on the [Wiki](https://github.com/ThelDoctor/CrystalAI/wiki) for upcoming documentation, tutorials and examples, also have a look at the [Crystal AI class documentation](https://theldoctor.github.io/CrystalAI/doxydoc/index.html).

### Prerequisites
Crystal AI has no external dependencies. If you intend to use it with [Unity](https://unity3d.com/) make sure that the API Compatiblity 
Level is set to .NET 2.0 (this almost equivalent to .NET 3.5) and not .NET 2.0 Subset. 

### Installing 
A [NuGet](https://www.nuget.org/) package will soon be available, until then, simply drop the CrystalAI.dll into your project directory and link 
to it in your favourite IDE.

### Installing in Unity
Compile the Debug or Release version of Crystal AI and drop in the dll in a folder named Plugins in your Unity directory. 

## Running the Tests
The Crystal AI test suite [CrystalAI.Tests](CrystalAI.Tests) depends on NUnit 3.5.0. If you are on Visual Studio you can use 
the [NuGet Package Manager](https://marketplace.visualstudio.com/items?itemName=NuGetTeam.NuGetPackageManagerforVisualStudio2015)
to install download it. Otherwise, you can directly download NUnit 3.5.0 from [their NuGet page](https://www.nuget.org/packages/NUnit/). 
You can find documentation on NUnit [here](https://www.nunit.org/). 


### Command Line Mono
To run the unit tests using [Mono](http://www.mono-project.com/) cd into the directory you have downloaded Crystal AI into and 
execute the following commands
```
nuget restore CrystalAI.sln
nuget install NUnit.Runners -Version 3.5.0 -OutputDirectory testrunner
xbuild /p:Configuration=Release CrystalAI.sln
mono ./testrunner/NUnit.ConsoleRunner.3.5.0/tools/nunit3-console.exe ./CrystalAI.Tests/bin/Release/CrystalAI.Tests.dll
```

### MonoDevelop Version Bundled with Unity
The MonoDevelop version bundled with Unity, although has its problems, is fairly good and obviously has the added benefit of 
seamless integration with Unity. The good part is that Unity has, in their managed folder, NUnit. However, I have not attempted 
to run the unit tests with the Unity version of NUnit. The reason main reason for this is that it is somewhat outdated. That 
shouldn't create difficulties, simply download NUnit 3.5.0, link to it (in the CrystalAI.Tests project) and you 
all the tests should run.

### Visual Studio 2015
To run the unit tests from within Visual Studio, apart from NUnit 3.5.0 you will need to 
install [NUnit3TestAdapter v3.6.0](https://www.nuget.org/packages/NUnit3TestAdapter/).

## Feature Requests and Bugs
If you find any bugs in Crystal AI, or simply have an awsome suggestion, we'd love to hear about it. After all, squashing bugs is fun!
The best way to report a bug or a feature request is via the GitHub Issue system [here](https://github.com/ThelDoctor/CrystalAI/issues). For discussing anything Crystal you can [visit our forums](http://www.bismur.co.uk/forums/index.php).

## History
- v0.7.2 Changed a portion of the API to make it more intuitive. Added a basic example project showcasing the AI. 
- v0.7.1 First public version containing only the library and the test suite.

## License
GPL v3 License
 
Copyright (c) 2016-2017 Bismur Studios Ltd.

Copyright (c) 2016-2017 Ioannis Giagkiozis
 
Crystal AI is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.
  
Crystal AI is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.
