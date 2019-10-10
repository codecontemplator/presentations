// Cast = Type conversion

// Implicit (safe) casts 
Int32 i32 = 3957;
Int64 i64 = i32;

class Base 
{
}

class Derived : Base
{
}

Derived d = new Derived();
Base b = d;


