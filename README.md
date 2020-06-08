


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

Everything in Naga is an expression. Thats where the power comes from. You can for example pass functions as arguments,
return a function from functions or declare functions inside functions and return them from functions.
So you can pass functions around like variables. All that makes the language very powerfull while still beeing minimalistic.

## Features
- Numbers (floating-point)
- Strings
- Functions
- Multiline Comments
- A special "None" value (wip)
That's about it.

Operators:
```html
+ - * /
```

## Building a language ontop of Naga with Naga
Naga does not provide special syntax for things like lists, maps and
objects, but they can be built up using the features of Naga functions.
It is planned that the Naga library contains functions like pair that makes a simple data
structure from two values.

A tutorial on how you can build things that behave like objects in languages like Java and
C++ out of Naga functions is comming soon.

## Examples
#### Code:
Comments
	# This is a comment #
Multiline Comments
	# As you have guessed,
	  there is no such thing
	  as single line comments
	  in Naga. The above comment
	  is also a multiline comment #
Declaring a variable
	num1 = 3;
Declaring a function
	square = :(x) {x * x;};
Calling a function
	num2 = square( num1 );
Declaring an anonymous function and calling it
	{
		# Anonymous function
		num = 10;
		x = x + 10 * 2;
	}(); # The open and closing parenthesize calling the function
Anonymous function with arguments
	:(x){
		# Anonymous function
		num = 10;
		x = x + 10 * 2;
	}(5);
Declaring and returning a function from a function
	# declaring a function
	func = :(x)
	{
		# this anonymous function get's returned
		  the last expreassion is what get's returned
		  from a function.
		  There is no "return" keyword in Naga #
		:()
		{
			# this gets returned from the anonymous function
			x*x;
		}
	}
	
	func2 = func(8)
	
	# Variable "squared_8" is 64
	squared_8 = func2();


 
