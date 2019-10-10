class Base
{
}

class Derived : Base
{
}

Derived[] da = new Derived[] { new Derived() };
Base[] ba = da;

class AnotherDerived : Base
{
}

ba[0] = new AnotherDerived();  // Breaks at runtime! Bad!

