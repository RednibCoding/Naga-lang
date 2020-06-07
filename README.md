


# Naga-lang <img src="https://github.com/RednibCoding/Naga-lang/blob/master/res/naga_icon.png" width="60"> 
Naga is a minimal and easy to use general purpose programming language written in C# .netcore 3.1

Everything is written by hand and no lexer generators or parser generators have been used.

It's planned that Naga has it's own interpreter and also a compiler/transpiler for different backends.


## Status
- Lexer: working
- Parser: working
- error checking: working
- Evaluator: wip
- Compiler: wip

## Syntax

Data types:
- Number (int and floats)
- String ("this is a string")
- Bool (true/false)

planned:
- list
- array

Keywords
- if
- then
- else
- lambda
- true
- false

planned:
- struct
- while
- elseif

Operators:
```html
+ - * / % = == != < > >= <= && ||
```
## Examples
#### Code:
	a = 10;
	squared = lambda(x) x*x;

	a_squared = squared(a);
#### AST
```html
<Root>
 <AssignNode> '='
  <SymbolNode> 'a'
  <NumberNode> '10'
 <AssignNode> '='
  <SymbolNode> 'squared'
  <LambdaNode>
   <Params>
    <SymbolNode> 'x'
   <Body>
    <BinaryNode> '*'
     <SymbolNode> 'x'
     <SymbolNode> 'x'
 <AssignNode> '='
  <SymbolNode> 'a_squared'
  <FunCallNode>
   <SymbolNode> 'squared'
   <Args>
    <SymbolNode> 'a'
```
#### Code:
    fib = lambda (n) if n < 2 then n else fib(n - 1) + fib(n - 2);
#### AST:
```html
<Root>
 <AssignNode> '='
  <SymbolNode> 'fib'
  <LambdaNode>
   <Params>
    <SymbolNode> 'n'
   <Body>
    <IfNode>
     <Condition>
      <BinaryNode> '<'
       <SymbolNode> 'n'
       <NumberNode> '2'
     <Then-Block>
      <SymbolNode> 'n'
     <Else-Block>
      <BinaryNode> '+'
       <FunCallNode>
        <SymbolNode> 'fib'
        <Args>
         <BinaryNode> '-'
          <SymbolNode> 'n'
          <NumberNode> '1'
       <FunCallNode>
        <SymbolNode> 'fib'
        <Args>
         <BinaryNode> '-'
          <SymbolNode> 'n'
          <NumberNode> '2'
 ```
#### Code:
    a = if foo() then bar() else baz();
#### AST:
 ```html
<Root>
  <AssignNode> '='
   <SymbolNode> 'a'
   <IfNode>
    <Condition>
     <FunCallNode>
      <SymbolNode> 'foo'
    <Then-Block>
     <FunCallNode>
      <SymbolNode> 'bar'
    <Else-Block>
     <FunCallNode>
      <SymbolNode> 'baz'
```
 
