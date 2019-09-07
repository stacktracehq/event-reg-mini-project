# C# Class Design Exercises

## Readling List

These resources may prove useful/interesting.

* [DateTime struct](https://docs.microsoft.com/en-us/dotnet/api/system.datetime?view=netcore-2.2).
* [Structs v Classes]()
* [The `Math` class](https://docs.microsoft.com/en-us/dotnet/api/system.math?view=netcore-2.2)
* [Primitive Obsession](https://medium.com/@arpitjain.iec/primitive-obsession-code-smell-that-hurt-people-the-most-5cbdd70496e9)

## Exercise 1

### Task

* Provide an implementation for the `Person` class
* Construction/Data: A person stores a name (`string`) and date of birth (`DateTime`).
* Behaviour: `GetAgeInYears()` which uses the date of birth to
calculate that persons _current_ age.
* Get all the `PersonTests` passing.

### Running the tests

```
> dotnet test --filter PersonTests
```

### Reflective Questions

* You may have noticed that I didn't write a test that covered the date of
birth being null. Why didn't we need this test?
* the value `GetAgeInYears()` returns depends on the "current" time. Can
you see any issues with this?

## Exercise 2

### Task

* Provide an implementation for the `Book` class
* Construction/Data: you need a title, author, and number of pages to construct a
book.
* Behaviour:
    * `CurrentPage`: getter for the current page, which starts at 1 and is updated by calling `SetBookmark`.
    * `SetBookmark(int pageNumber)`: page number must be in range 1-number of pages
    * `ClearBookmark()`: clears the bookmark
    * `EstimateRemainingReadingMinutes(double readingRateInPagesPerMinute)`:
    provides a estimate of how many minutes will be required to read the rest of
    the book, given the `CurrentPage` and the reading rate, passed as an
    argument. Should round values `< 1` up to `1` and
    round all other values to the closest integer.

### Running the tests

```
> dotnet test --filter BookTests
```

### Reflective Questions

* Could we have modelled page numbers differently to avoid having to be so
defensive about 0 and negative page numbers?
* How is the validation logic we are writing feeling? Repitious? What could we
do?
* Is `new Book("Stephen King", "Pet Cemetary", 230);` a legal way
to construct a book? Is this a problem that could be avoided?
* What happens when we test the equality of two books? e.g.

```charp
var bookOne = new Book("Stephen King", "Pet Cemetary", 230);
var bookTwo = new Book("Stephen King", "Pet Cemetary", 230);
Assert.Equal(bookOne, bookTwo); // what do we expect here?
```

