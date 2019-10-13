class Base
{
}

class Derived : Base
{
}

// Relationship?
List<Derived> ld;
List<Base> lb;

// Does not work...
//lb = ld;

// Since lb would allow...
class AnotherDerived : Base
{
}

lb.Add(new AnotherDerived());

// Which would cause runtime errors when using ld
