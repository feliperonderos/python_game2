// Quote -- Parse tree node strategy for printing the special form quote

using System;

namespace Tree
{
    public class Quote : Special
    {
        public Quote() { }

        public override void print(Node t, int n, int indentation, bool p, int state)
        {
            Console.Write("'");
            if (t.getCdr() != null)
                t.getCdr().print(0, 0, false, 4);
        }
    }
}

