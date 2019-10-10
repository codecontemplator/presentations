class Base
{
}

class Derived : Base
{
}

List<List<Base>> b;
List<List<Derived>> d;

d = b;

// List<List<Base>> never relates to List<List<Derived>>
// in any way since List<A> is not variant with / related to List<B> even though A : B or B : A. 

