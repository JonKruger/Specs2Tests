## Overview

Specs2Tests is a simple library that will help you take textual given/when/then scenarios and transform them into unit tests in your favorite testing framework.  

Why bother with this?  Because I believe that if you can't write out what you're trying to build in given/when/then format, you don't really know you're building.  But at the same time, I don't like having to type out all of the underscores, braces, attributes and all that.  It's much easier to type out the given/when/thens in text format, review it with your BAs and QA team, and then have it generate nice code for you where you can just fill in the blanks.

Here's how it works:

1. Type out your scenarios		
2. Copy the scenario text (you can copy multiple scenarios if you want)
3. Run Specs2Tests.  It will pull the scenario off the clipboard, generate test code, and put it on the clipboard for you to paste into Visual Studio.

Scenarios need to look more or less like this:

	Scenario: Deposit
		Given a bank account
		When I deposit money into the account
		Then the balance should increase by the amount of the deposit

Currently Specs2Tests will generate C# code for the following test frameworks:

- NUnit (default)
- MSTest

If you edit the config file, you can specify which framework you want to use.  Use a different framework?  Fork the code and implement it and send me a pull request!

## Installation

You can get Specs2Tests in one of two ways:

- Get the source and build it
- Download it from the <a href="https://github.com/JonKruger/Specs2Tests/downloads">downloads</a> page

## Caveats

I wrote this utility originally for myself.  I hope that it can be useful for others, but I wrote it to match the style of testing that I use.  If you want it to generate code to match your style or if you want it to generate VB.NET code, modify/replace the CSharpCodeWriter class.  Honestly it's not that hard.

## Enjoy!

Let me know if you have any questions or problems.  