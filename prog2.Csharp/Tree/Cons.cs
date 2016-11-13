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
                if ((Boolean)((Node)cdr.getCar().eval(e)).eval(e))
                {
                    if (cdr.getCdr().getCar() is Cons) return cdr.getCdr().getCar().eval(e);
                    return cdr.getCdr().getCar();
                }
                else
                {
                    if (cdr.getCdr().getCdr().getCar() is Cons) return cdr.getCdr().getCdr().getCar().eval(e);
                    return cdr.getCdr().getCdr().getCar();
                }
            }
            else if (form is Cond) {
                Node nextClause = cdr;
                Node o = (Node) nextClause.getCar().getCar().eval(e);
                while ((nextClause.getCar().getCar().getName() != "else") && (!(Boolean)o.eval(e))) {
                    nextClause = nextClause.getCdr();
                }
                Node p = (nextClause.getCar().getCdr().getCar());
                while (p is Cons) {
                    p = (Node)p.eval(e);
                }
                return p;
            }
            //TODO: Closures
            else if (form is Define)
            {
                if (cdr.GetCar() is Cons) {      
                    e.define(cdr.getCar().getCar().getName(), new Closure(new Cons(cdr.getCar().getCdr(), cdr.getCdr()), e));
                }
                else
                e.define(cdr.getCar().getName(), cdr.getCdr().getCar());
            }
            else if (form is Lambda) {
                return new Closure(cdr,e);
            }
            else if (form is Let)
            {
                Environment env = new Environment(e);
                Node assign = (Node)cdr.GetCar();
                while (!assign.isNull()){
                    if (assign.getCar().getCdr().getCar() is Cons)
                    {
                        env.define(assign.getCar().getCar().getName(), (Node)assign.getCar().getCdr().getCar().eval(env));
                    }
                    else {
                        env.define(assign.getCar().getCar().getName(), (Node)assign.getCar().getCdr().getCar());
                    }
                    assign = assign.getCdr();
                }
                return cdr.getCdr().getCar().eval(env);
            }
            else if (form is Quote)//buggy
            {
                ;
            }
            else if (form is Begin)//buggy
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
                  else{
                         return expression.eval(e);
                  }
                }
            }
            //END TODO
            else if (form is Set)
            {
                e.change(cdr.getCar().getName(), cdr.getCdr().getCar());
            }
            else if ((car.eval(e) is Node) && (((Node)car.eval(e)).isProcedure()))
            {
                object pp = ((Node)car.eval(e));
                object p = ((Node)car.eval(e)).apply(cdr, new Environment(e));
                return ((Node)p);
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

