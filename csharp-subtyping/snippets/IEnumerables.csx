class Base
{
}

class Derived : Base
{
}

// Relationship?
List<Derived> ld;
IEnumerable<Base> eb;

// Works!
eb = ld;

// IEnumberable is readonly so the underlying
// collection cannot be messed up

// How does this work?
// https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1?view=netframework-4.8
// https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=netframework-4.8
//public class List<T> ...
//public interface IEnumerable<out T> ...

// out mean that IEnumerable is covariant with T
// so
//
// IEnumerable<Derived> : IEnumerable<Base>
// since
// Derived : Base
//
// And List<Derived> : IEnumerable<Derived>

