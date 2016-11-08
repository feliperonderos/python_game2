// Set -- Parse tree node strategy for printing the special form set!

using System;

namespace Tree
{
    public class Set : Special
    {
        public Set() { }
	
        public override void print(Node t, int n,int indentation, bool p, int state)
        {
            if (!p)
            {
                printSpaces(n);
                Console.Write("(");
                t.getCar().print(0, indentation, false, 1);
                t.getCdr().print(1, indentation, true, 1);
            }
            else
            {
                
                t.getCar().print(1, indentation, false, 1);
                t.getCdr().print(1, indentation, true, 1);
            }
        }
    }
}

