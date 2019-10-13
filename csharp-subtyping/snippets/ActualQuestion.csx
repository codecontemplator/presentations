class Base
{
}

class Derived : Base
{
}

List<List<Base>> b;
List<List<Derived>> d;

//d = b;

// List<List<Base>> never relates to List<List<Derived>>
// in any way since List<A> is not variant with / related to List<B> even though A : B or B : A. 

IEnumerable<IEnumerable<Base>> eeb = new [] { new [] { new Derived(), new Derived() }, new [] { new Derived() } };
IEnumerable<IEnumerable<Derived>> eed;
eed = eeb as IEnumerable<IEnumerable<Derived>>;
if (eed != null)
{
    eed.Dump();
}
else
{
    Console.WriteLine("Cast failed");
}


eed = new [] { new [] { new Derived() } };
eeb = eed;
eed.Dump();