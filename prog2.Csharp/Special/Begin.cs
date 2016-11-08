// Begin -- Parse tree node strategy for printing the special form begin

using System;

namespace Tree
{
    public class Begin : Special
    {
        public Begin() { }

        public override void print(Node t, int n, int indentation, bool p, int state)
        {
            printSpaces(n);
            Console.Write("(");
            t.getCar().print(0);
            Console.Write("");
            if (state != 4)
                t.getCdr().print(0, indentation + 1, true, 3);
            else
                t.getCdr().print(0, 0, true, 4);
           
        }
    }
}
