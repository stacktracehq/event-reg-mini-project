# Tiny Types and Value Equality

We've talked about 'tiny types' as a pattern where we wrap primitive types, e.g.
`string` or `int` into a more domain-relevant custom type, e.g.
`CustomerNumber`, or `PageCount`.

We've also talked about structs and classes in C#, briefly discussing the high
level difference, i.e. **value type** vs **reference type**. It's useful to
read [Microsoft's guidelines](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/choosing-between-class-and-struct)
for when to use structs.

In these exercises we're going to add some tiny type goodness to
the `Person` and `Book` implementations we did in the
[C# Classes Exercises](../csharp_classes).

Try doing these for yourself:

```
CurrentPage : TinyType<int>
Title : TinyType<string>
Author : TinyType<string>
PageCount : TinyType<int>
```
