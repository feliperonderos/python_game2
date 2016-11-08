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
            Node arg1 = args.getCar();
            Node arg2 = (!args.getCdr().isNull()) ? (args.getCdr().getCar()) : null;
            switch (s)
            {
                case "+":
                    return new IntLit((int)arg1.eval(env) + (int)arg2.eval(env));
                    break;
                case "-":
                    return new IntLit((int)arg1.eval(env) - (int)arg2.eval(env));
                    break;
                case "*":
                    return new IntLit((int)arg1.eval(env) * (int)arg2.eval(env));
                    break;
                case "/":
                    return new IntLit((int)arg1.eval(env) / (int)arg2.eval(env));
                    break;
                case "=":
                    return new BoolLit((int)arg1.eval(env) == (int)arg2.eval(env));
                    break;
                case ">":
                    return new BoolLit((int)arg1.eval(env) > (int)arg2.eval(env));
                    break;
                case ">=":
                    return new BoolLit((int)arg1.eval(env) >= (int)arg2.eval(env));
                    break;
                case "<=":
                    return new BoolLit((int)arg1.eval(env) <= (int)arg2.eval(env));
                    break;
                case "<":
                    return new BoolLit((int)arg1.eval(env) < (int)arg2.eval(env));
                    break;

                case "symbol?":
                    return new BoolLit(arg1.isSymbol());
                    break;
                case "number?":
                    return new BoolLit(arg1.isNumber());
                    break;
                case "procedure?":
                    return new BoolLit(arg1.isProcedure());
                    break;
                case "car":
                    return arg1;
                    break;
                case "cdr":
                    return args.getCdr();
                    break;
                case "cons":
                    return new Cons((Node)args.GetCar(),args.getCdr());
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
                    return new BoolLit(arg1.eval(env) == arg2.eval(env));
                    break;

                case "read":
                    Scanner scanner = new Scanner(Console.In);
                    Parser parser = new Parser(scanner);
                    return parser.parseExp();
                    break;
                case "write":
                    // call pretty printer on arg1
                    break;
                case "display":
                    // call pretty printer on arg1, subtly different in that strings and characters are printed without any notation
                    break;
                case "newline":
                    return new NewLine();
                    break;
                case "interaction-environment":
                    return env.asNode();
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

