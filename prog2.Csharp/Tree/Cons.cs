// Cons -- Parse tree node class for representing a Cons node

using System;

namespace Tree
{
    public class Cons : Node
    {
        private Node car;
        private Node cdr;
        private Special form;
    
        public Cons(Node a, Node d)
        {
            car = a;
            cdr = d;
            parseList();
        }
        public override bool isPair() { return true; }  // Cons

        // parseList() `parses' special forms, constructs an appropriate
        // object of a subclass of Special, and stores a pointer to that
        // object in variable form.  It would be possible to fully parse
        // special forms at this point.  Since this causes complications
        // when using (incorrect) programs as data, it is easiest to let
        // parseList only look at the car for selecting the appropriate
        // object from the Special hierarchy and to leave the rest of
        // parsing up to the interpreter.
        private void parseList()
        {
            if (car.isSymbol())
            {
                Ident special = (Ident)car;
                switch (special.getName())
                {
                    case "quote":
                        form = new Quote();
                        break;
                    case "lambda":
                        form = new Lambda();
                        break;
                    case "begin":
                        form = new Begin();
                        break;
                    case "if":
                        form = new If();
                        break;
                    case "let":
                        form = new Let();
                        break;
                    case "cond":
                        form = new Cond();
                        break;
                    case "define":
                        form = new Define();
                        break;
                    case "set!":
                        form = new Set();
                        break;
                    default:
                        form = new Regular();
                        break;
                }
            }
            else form = new Regular();
        }
 
        public override void print(int n)
        {
            form.print(this, n, 0, false,1);
        }
        
        public override void print(int n, int indentation, bool p, int state)
        {
            form.print(this, n, indentation, p, state);
        }
        
        public override Node getCar()
        {
            return car;
        }

        public override Node getCdr()
        {
            return cdr;
        }
        public override object eval(Environment e)
        {
            if (form is If)
            {
                if ((Boolean)cdr.getCar().eval(e))
                {
                    return cdr.getCdr().getCar().eval(e);
                }
                else
                {
                    return cdr.getCdr().getCdr().getCar().eval(e);
                }
            }
            else if (form is Cond) {
                Node nextClause = cdr;
                while ((nextClause.getCar().getCar().getName() != "else") && (!((Boolean)nextClause.getCar().getCar().eval(e)))) {
                    nextClause = nextClause.getCdr();
                }
                return nextClause.getCar().getCdr().getCar().eval(e);
            }
            //TODO: Closures
            else if (form is Define)
            {
                e.define(cdr.getCar().getName(), cdr.getCdr().getCar());
            }
            else if (form is Lambda) {
                ;
            }
            else if (form is Let)
            {
                ;
            }
            else if (form is Quote)
            {
                return cdr;
            }
            //END TODO
            else if (form is Begin)
            {
                Node expression;
                while (!cdr.isNull())
                {
                    expression = cdr.getCar();
                    if (!cdr.getCdr().isNull())
                    {
                        expression.eval(e);
                        cdr = cdr.getCdr();
                    }
                    else
                    {
                        return expression.eval(e);
                    }
                }
            }
            else if (form is Set)
            {
                e.change(cdr.getCar().getName(), cdr.getCdr().getCar());
            }
            else if ((car.eval(e) is Node) && (((Node)car.eval(e)).isProcedure()))
            {
                return ((Node)car.eval(e)).apply(cdr, e).eval(e);
            }
            return null;
        }
        // After setCar, a Cons cell needs to be `parsed' again using parseList.
        public override void setCar(Node a)
        {
            car = a;
            parseList();
        }
        public override void setCdr(Node d)
        {
            cdr = d;
        }
    }
}

