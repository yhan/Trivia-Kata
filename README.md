# Trivia-Kata


J. B. Rainsberger uses this Kata for his “Legacy Code Retreat” events, which are a whole day, so there is enough to do in this Kata for several dojo meetings. The first thing to do is to get some test coverage in place, since if you refactor without it, you won’t necessarily notice if you make a mistake and break the functionality of the code. Once you have some tests to lean on, you can practice refactoring techniques.  

Add unit tests without changing any of the code  
This should be quite difficult. You will learn about what makes code hard to unit test. You’ll probably end up with tests that are not really “unit” tests since they test larger chunks of code.
You could try the technique “subclass to test”, in order to get the “Game” class under test.
