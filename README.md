# Sperse

This is an experimental project trying to find itself...

Work in progress...


### Current demo:

Run the SampleApi project. It exposes the following endpoints:

- `GET /demo/execute/:x`

Takes an int parameter called x and returns an int. By default, this simply returns the input.
e.g.: `Get /demo/execute/2`

- `GET /demo/update/:newFunc`

Allows you to replace the implementation of the `execute` function with arbitrary code. You
provide the source code for the replacement function
.e.g: `GET /demo/update/Add(x, System.Math.Max(x, 10))`

The syntax for the source code is a simple expression that consists
of a function call:
- `expression` : `identifier` | `intLiteral` | `identifier` `(` `argsList` `)`
- `argsList`: *empty* | `expression` `argsList`

Commas can optionally be used as separators between expression arguments.


There are some built-in functions like `int Add(int a, int b)`
and `int Mult(int a, int b)`. You can also call any method accessibly in your
program (from any of the imported assemblies) by providing it's fully qualified name.
It should be a static method that takes 0 or more int arguments and returns an int
e.g. `int System.Math.Max(int, int)`

Your function will be called with one int argument called `x`.

Examples:
- `x` : identity function
- `Add(x 2)` ->  x + 2
- `Mul(x, 2)` -> x * 2
- `Mul(x, System.Math.Min(x, 100))` -> x * Min(x, 100)