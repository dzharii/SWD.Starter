SWD.Starter
===========

``` csharp
  var testAutomation = new TestAutomation();
  testAutomation.Create();
  testAutomation.Run();
  
  if (testAutomation.FoundBugs)
  {
    BugTracker.AddBugs(testAutomation.Bugs);
  }
```
