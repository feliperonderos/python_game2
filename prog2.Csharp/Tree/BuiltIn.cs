// BuiltIn -- the data structure for built-in functions

// Class BuiltIn is used for representing the value of built-in functions
// such as +.  Populate the initial environment with
// (name, new BuiltIn(name)) pairs.

// The object-oriented style for implementing built-in functions would be
// to include the C# methods for implementing a Scheme built-in in the
// BuiltIn object.  This could be done by writing one subclass of class
// BuiltIn for each built-in function and implementing the method apply
// appropriately.  This requires a large number of classes, though.
// Another alternative is to program BuiltIn.apply() in a functional
// style by writing a large if-then-else chain that tests the name of
// the function symbol.

using System;
using Parse;

namespace Tree
{
    public class BuiltIn : Node
    {
        private string symbol;            // the Ident for the built-in function

        public BuiltIn(string s)		{ symbol = s; }

        //public Node getSymbol()		{ return symbol; }

        // TODO: The method isProcedure() should be defined in
        // class Node to return false.
        public override bool isProcedure()	{ return true; }

        public override void print(int n)
        {
            // there got to be a more efficient way to print n spaces
            for (int i = 0; i < n; i++)
                Console.Write(' ');
            Console.Write("#{Built-in Procedure ");
            //if (symbol != null)
            //    symbol.print(-Math.Abs(n));
            Console.Write('}');
            if (n >= 0)
                Console.WriteLine();
        }

        // TODO: Implement method apply()
        // It should be overridden only in classes BuiltIn and Closure
        public override Node apply(Node args, Environment env)
        {
            string s = symbol;
            Node arg1 = new Node();
            Node arg2 = new Node();
            Node arg1s = new Node();
            Node arg2s = new Node();
            if (!args.isNull()) { 
                arg1 = args.getCar();
                arg2 = (!args.getCdr().isNull()) ? (args.getCdr().getCar()) : null;
                arg1s = (arg1 is Cons) ? ((Node)arg1.eval(env)) : arg1;
                arg2s = (arg2 is Cons) ? ((Node)arg2.eval(env)) : arg2;
                while (arg1s is Ident)
                {
                    arg1s = (Node)arg1s.eval(env);
                }
                while (arg2s is Ident)
                {
                    arg2s = (Node)arg2s.eval(env);
                }
            }
            switch (s)
            {
                case "+":
                    return new IntLit((int)arg1s.eval(env) + (int)arg2s.eval(env));
                    break;
                case "-":
                    return new IntLit((int)arg1s.eval(env) - (int)arg2s.eval(env));
                    break;
                case "*":
                    return new IntLit((int)arg1s.eval(env) * (int)arg2s.eval(env));
                    break;
                case "/":
                    return new IntLit((int)arg1s.eval(env) / (int)arg2s.eval(env));
                    break;
                case "=":
                    return new BoolLit((int)arg1s.eval(env) == (int)arg2s.eval(env));
                    break;
                case ">":
                    return new BoolLit((int)arg1s.eval(env) > (int)arg2s.eval(env));
                    break;
                case ">=":
                    return new BoolLit((int)arg1s.eval(env) >= (int)arg2s.eval(env));
                    break;
                case "<=":
                    return new BoolLit((int)arg1s.eval(env) <= (int)arg2s.eval(env));
                    break;
                case "<":
                    return new BoolLit((int)arg1s.eval(env) < (int)arg2s.eval(env));
                    break;

                case "symbol?":
                    return new BoolLit(arg1.isSymbol());
                    break;
                case "number?":
                    return new BoolLit(arg1.isNumber());
                    break;
                case "procedure?":
                    return new BoolLit(arg1s.isProcedure());
                    break;
                case "car":
                    return arg1.getCar();
                    break;
                case "cdr":
                    return arg1.getCdr();
                    break;
                case "cons":
                    return new Cons((Node)arg1,arg2);
                    break;
                case "set-car!":
                    return new Cons(arg2, arg1.getCdr());
                    break;
                case "set-cdr!":
                    return new Cons(arg1.getCar(),arg2);
                    break;
                case "null?":
                    return new BoolLit(arg1.isNull());
                    break;
                case "pair?":
                    return new BoolLit(arg1.isPair());
                    break;
                case "eq?":
                    if ((arg1 is Ident) && (arg2 is Ident))
                    {
                        while (arg1 is Ident) {
                            arg1 = env.find(arg1.getName());
                        }
                        while (arg2 is Ident)
                        {
                            arg2 = env.find(arg2.getName());
                        }
                        return new BoolLit(arg1 == arg2);
                    }
                    else
                        return new BoolLit(arg1.eval(env) == arg2.eval(env));
                    break;

                case "read":
                    Scanner scanner = new Scanner(Console.In);
                    Parser parser = new Parser(scanner);
                    return parser.parseExp();
                    break;
                case "write":
                    return arg1;
                    break;
                case "display":
                    return arg1;
                    // call pretty printer on arg1, subtly different in that strings and characters are printed without any notation
                    break;
                case "newline":
                    Console.Write("\n");
                    return new Node();
                    break;
                case "interaction-environment":
                    return env;
                    break;
                default:
                    return null;
                /*
                 
                 */
            }


            //return new StringLit("Error: BuiltIn.apply not yet implemented");

        }
    }    
}

