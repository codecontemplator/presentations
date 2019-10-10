// Are we done? Lets explore functions

class Base
{
}

class Derived : Base
{
    public int GetInt() => 1;    
}

Func<Derived> ad = () => new Derived();

// Works because ad returns something that is always compatible with Base (that is a Derived)
Func<Base> ab = ad;

class MoreDerived : Derived
{
}

Func<Derived, int> f = (d) => d.GetInt();

// Does not work! Base does not have GetInt()
// Func<Base, int> f2 = f; 

Func<MoreDerived, int> f2 = f;  

// Works, because MoreDervived is a Derived (and has GetInt())

// public delegate TResult Func<out TResult>();
// public delegate TResult Func<in T,out TResult>(T arg);

// Covariant / in -> Contravariant out

