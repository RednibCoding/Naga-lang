


# Naga-lang <img src="https://github.com/RednibCoding/Naga-lang/blob/master/res/naga_icon.png" width="60">
Naga is a tiny but practical programming language written in C# and .NET Core 3.1 .

## Status
- Lexer: working
- Parser: working
- error checking: working
- Evaluator: wip
- Compiler: wip
- Standard library: wip

## Design Principles
Naga is designed to be very small: it should have as few features as possible
while still being useful. The goal behind Naga is to build a language that is easy
to write Lexers/Parsers/Compilers/Transpilers for.

For example, the fact that language features such as "if" are provided as functions written in Naga itself instead of
special keywords means there is little code needed to write the language, and little learning needed to understand it.

Everything in Naga is an expression. You can for example pass functions as arguments,
return a function from functions or declare functions inside functions and return them from functions.
So you can pass functions around like variables. All that makes the language very powerfull while still beeing minimalistic.

## Features
- Numbers (floating-point)
- Strings
- Functions
- Multiline Comments
- A special "None" value (wip)

Operators:
```html
+ - * /
```

That's about it.

## Building a language ontop of Naga with Naga
Naga does not provide special syntax for things like lists, maps and
objects, but they can be built up using the features of Naga functions.
It is planned that the Naga library contains functions like pair that makes a simple data
structure from two values.

A tutorial on how you can build things that behave like objects in languages like Java and
C++ out of Naga functions is comming soon.

## Example
	square = :(x){ x * x;};

	num1 = 3;
	num2 = square( num1 );

	if( equals( num1, num2 ),
		{
			print( "num1 equals num2." );
		},
		{
			print( "num1 does not equal num2." );
		}
	);
This prints:

	num1 does not equal num2.

The Parser produces the following abstract syntax tree:
```html
[assignment] =
 [symbol] square
 [function_decl]
  [function_params] 1
   [symbol] x
  [function_body] 1
   [operation] *
    [symbol] x
    [symbol] x

[assignment] =
 [symbol] num1
 [number] 3

[assignment] =
 [symbol] num2
 [function_call]
  [symbol] square
  [function_args] 1
   [symbol] num1

[function_call]
 [symbol] if
 [function_args] 3
  [function_call]
   [symbol] equals
   [function_args] 2
    [symbol] num1
    [symbol] num2
  [function_decl]
   [function_params] 0
   [function_body] 1
    [function_call]
     [symbol] print
     [function_args] 1
  [function_decl]
   [function_params] 0
   [function_body] 1
    [function_call]
     [symbol] print
     [function_args] 1
```

## Language
Comments
```html
# This is a comment #
```
Multiline Comments
```html
# As you have guessed,
  there is no such thing
  as single line comments
  in Naga. The above comment
  is also a multiline comment #
```
Declaring a variable
```html
num1 = 3;
```
Declaring a function
```html
square = :(x) {x * x;};
```
Calling a function
```html
num2 = square( num1 );
```
Declaring an anonymous function and calling it
```html
# 
 Anonymous function without parameters.
 You can omit the ":()" when no parameters are given
#
{
	num = 10;
	x = 5 + num * 2; # Last expression is what gets returned #
}(); # Calling the function #
```
Declaring an anonymous function with parameters and calling it
```html
# 
 Anonymous function with parameters.
 Introduce comming parameters with colon ":"
 And wrapping them in parethesize seperated by commas.
#
:(x, y){
	num = 10;
	x + y + num * 2; # Last expression is what gets returned #
}(5, 8); # Calling the function with arguments #
```
Declaring and returning a function from a function
```html
# Declaring a function "func" #
func = :(x)
{
	#
	 This anonymous function get's returned from "func".
	 The last expression is what get's returned from a function.
	 There is no "return" keyword in Naga.
	 So this anonymous function gets returned:
	#
	{
		#
		 This gets returned from the anonymous function
		 "x" can be accessed from here (Closure)
		#
		x*x;
	}
}

func2 = func(8)

squared_8 = func2(); # "squared_8" is 64 #

#
 As you can see, functions keep their variable values.
 This fact let's you write pretty interesting constructs.
 E.g. you can mimic arrays, lists, pairs and even objects because
 objects are pretty similar to closures.
#
```
You can pass a function declaration as argument to other functions:
```html
myFunc(:(x){x*x;}(5), y);
```
You can also omit the parameters if there aren't any
```html
myFunc({x*x;}(), y);
```
You can also pass a non-anonymous function as argument
```html
func = :(x)
{
	x * 2; # This gets returned #
}

myFunc(func(5), y)
```
You can also pass the function itself as argument so it can be called inside the other function
```html
func = :(x)
{
	x * 2; # This gets returned #
}

myFunc = :(x, y)
{
	x() * y; # This gets returned #
}

myFunc(func, 5)

```
From the above example you could also declare the function anonymously
```html
myFunc = :(x, y)
{
	x() * y; # This gets returned #
}
myFunc(:(x){x*2}, 5)
```