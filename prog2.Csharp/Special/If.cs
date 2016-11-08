// If -- Parse tree node strategy for printing the special form if

using System;

namespace Tree
{
    public class If : Special
    {
        public If() { }
        /*STATES 
            * 1 print on one line single spaced. no newline. Pass 1 to descendants
            * 2 used by if... do not print newline + spaces pass 1 to car 3 to cdr.
            * 3 used by if, cond... print newline + spaces before. pass 1 to car, 3 to cdr 
        */
        public override void print(Node t, int n, int indentation, bool p, int state)
        {
            printSpaces(n);
            Console.Write("(");
            t.getCar().print(0);
            Console.Write("");
            if (state != 4)
                t.getCdr().print(0, indentation + 1, true, 2);
            else
                t.getCdr().print(0, 0, true, 4);
            
        }
    }
}

