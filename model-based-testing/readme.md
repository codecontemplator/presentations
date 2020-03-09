# Model Based Testing

Testing components with state is many times a non-trivial task. Model based testing is a elegant approach to this problem and can be a handy tool is such situations. We will look at an example coming from a real world problem and see how model based testing can be applied. FsCheck is leveraged as a test framework.

Quoting the FsCheck web site
> FsCheck is a tool for testing .NET programs automatically. The programmer provides a specification of the program, in the form of properties which functions, methods or objects should satisfy, and FsCheck then tests that the properties hold in a large number of randomly generated cases. .... FsCheck's generator combinators can be used in any testing framework to easily generate a number of random values for many types, and FsCheck itself integrates nicely with existing unit testing frameworks such as NUnit, xUnit, MSTest and MbUnit.

