// Node -- Base class for parse tree node objects

namespace Tree
{
    public abstract class INode { }

    public class Node : INode
    {
        // The argument of print(int) is the number of characters to indent.
        // Every subclass of Node must implement print(int).
        public virtual void print(int n) { }

        // The first argument of print(int, bool) is the number of characters
        // to indent.  It is interpreted the same as for print(int).
        // The second argument is only useful for lists (nodes of classes
        // Cons or Nil).  For all other subclasses of Node, the boolean
        // argument is ignored.  Therefore, print(n,p) defaults to print(n)
        // for all classes other than Cons and Nil.
        // For classes Cons and Nil, print(n,TRUE) means that the open
        // parenthesis was printed already by the caller.
        // Only classes Cons and Nil override print(int, bool).
        // For correctly indenting special forms, you might need to pass
        // additional information to print.  What additional information
        // you pass is up to you.  If you only need one more bit, you can
        // encode that in the sign bit of n. If you need additional parameters,
        // make sure that you define the method print in all the appropriate
        // subclasses of Node as well.
        public virtual void print(int n, int indentation, bool p, int state)
        { 
            print(n);
        }

        // For parsing Cons nodes, for printing trees, and later for
        // evaluating them, we need some helper functions that test
        // the type of a node and that extract some information.

        // DONE: implement these in the appropriate subclasses to return true.
        public virtual bool isBool()   { return false; }  // BoolLit
        public virtual bool isNumber() { return false; }  // IntLit
        public virtual bool isString() { return false; }  // StringLit
        public virtual bool isSymbol() { return false; }  // Ident
        public virtual bool isNull()   { return false; }  // Nil
        public virtual bool isPair()   { return false; }  // Cons
        public virtual bool isProcedure() { return false; } // BuiltIn, Closure

        // public virtual INode getInstance() { return null; } // Nil

        // method apply() should be overridden only in classes
        // BuiltIn and Closure.
        public virtual Node apply(Node args, Environment env)
        {
            System.Console.Error.WriteLine("Called apply() from non BuiltIn or Closure node");
            return null;
        }

        public virtual object eval(Environment e) {
            return null;
        }
        // Since C# does not have covariant override, it is not possible
        // for the getCar and getCdr methods to implement the interface
        // methods from INode directly.
        public INode GetCar() { return getCar(); }
        public INode GetCdr() { return getCdr(); }
         
        // Doneish: Report an error in these default methods and implement them
        // in class Cons.  After setCar, a Cons cell needs to be `parsed' again
        // using parseList.
        public virtual Node getCar() {
            //System.Console.Error.WriteLine("Error: argument of car is not a pair");
            return null;
        }

        public virtual Node getCdr() {
            System.Console.Error.WriteLine("Error: argument of cdr is not a pair");
            return null;
        }

        public virtual void setCar(Node a) {
            System.Console.Error.WriteLine("Error: argument of set-car() is not a pair");
        }

        public virtual void setCdr(Node d) {
            System.Console.Error.WriteLine("Error: argument of set-cdr! is not a pair");
        }

        public virtual string getName()
        {
            return "";
        }

        public static void printSpaces(int n, string content) {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(' ', n);
            System.Console.Write(sb.ToString());
            // for (int i = 0; i < n; i++) {
            //     System.Console.Write(" ");
            // }
            System.Console.Write(content);
        }

    }
}

