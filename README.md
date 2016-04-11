Welcome to Applying Functional Principles in C#
=====================

This is the source code for the [Applying Functional Principles in C# Pluralsight course][L7].

There are two versions of the code base: the [old version][L5] that doesn't comply with the functional principles and the [new one][L6] which adheres to the functional principles described in the course. The last module of the course shows a step-by-step guide of how to refactor the old version into the new one.

How to Get Started
--------------

Both versions are fully functional and covered with auto-tests. In order to run the tests, you need to create a database ([new version][L21], [old version][L22]) and change the connection string ([new version][L31], [old version][L32]).

Licence
--------------
[Apache 2 License][L1]

[L21]: New/DBCreationScriptRefactored.txt
[L22]: New/DBCreationScriptNonRefactored.txt
[L31]: New/CustomerManagement.Tests/Integration/Tests.cs
[L32]: Old/CustomerManagement.Tests/Integration/Tests.cs
[L1]: http://www.apache.org/licenses/LICENSE-2.0
[L5]: Old
[L6]: New
[L7]: http://pluralsight.com/courses/csharp-applying-functional-principles
