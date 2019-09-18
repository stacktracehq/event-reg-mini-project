# Starting to think about "Dependencies"

## So far...

Most of the code we have written has been ignorant of any other
systems, integrations, or processes that can effect how it works.
We've mostly been able to stay in a nice little logical world that
we have constructed for ourselves.

We have seen one area where the "real world" does interfere though.
Our usage of the `DateTime` struct for modelling things like
dates of birth (and event times etc in the project), along with
getting the _current_ date/time using `DateTime.Now` does
represent a coupling between our code and some externality, in
this case our machine's clock.

I've taken the Person class we've been working on and updated
the `GetAgeInYears` function to take the month and day into account.
That is, you should be considered a year older only if it is currently
your birthday, or your birthday has already passed in the current
calendar year.

I've also written a simple test suite which checks the age of
someone born yesterday, today, and tomorrow. These tests all
use `DateTime.Now`.

## Time moves on...

If you run these tests today (16th September) they should all pass.

Now update your machines sytem clock, setting the "current" date
back to yesterday.

Uh oh.

## This is a dependency

We can say that what _our_ code does _depends_ on what some other
code/service/_thing_ does. This idea comes up quite a lot in
a "real world" code base and accurately identifying and decoupling
dependencies is probably more art than science. But you will definitely
here it talked about a lot.

## The Task

Let's refactor our code so that our tests don't have this direct
coupling to our machine's clock.

This means we'll need to introduce some sort of "abstraction", i.e.
a way to program agains the idea of time, instead of the concrete
time provided by the machines clock.

## But How?

Well, that's up to you. We have mentioned interfaces before. This
would definitley be a standard approach.

Some interesting design questions?

* What actually depends on the time?
* How does having an abstraction help with testing?

## Links

* [C# Interfaces](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/interfaces/)


