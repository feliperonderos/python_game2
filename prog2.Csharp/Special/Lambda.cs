// Lambda -- Parse tree node strategy for printing the special form lambda

using System;

namespace Tree
{
    public class Lambda : Special
    {
        public Lambda() { }

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
