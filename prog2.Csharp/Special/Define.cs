// Define -- Parse tree node strategy for printing the special form define

using System;

namespace Tree
{
    public class Define : Special
    {
        public Define() { }

        public override void print(Node t, int n, int indentation, bool p, int state)
        {
            printSpaces(n);
            Console.Write("(");
            t.getCar().print(0);
            Console.Write("");
            if (state != 4)
                if ((!(t.getCdr().isNull()))
               && (t.getCdr().getCar().isPair()))
                    t.getCdr().print(0, indentation + 1, true, 2);
                else t.getCdr().print(0, indentation + 1, true, 1);
            else
                t.getCdr().print(0, 0, true, 4);
        }
    }
}
