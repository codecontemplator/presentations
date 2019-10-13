// Explicit casts (might fail or lose data)

Int64 i64 = 124321;
Int32 i32 = (Int32)i64;

class Base 
{
}

class Derived : Base
{
}

Base b = new Derived();
Derived d = (Derived)b;

class AnotherDerived : Base
{
}

b = new AnotherDerived();
d = (Derived)b;
