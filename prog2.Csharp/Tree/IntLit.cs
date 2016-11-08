// IntLit -- Parse tree node class for representing integer literals

using System;

namespace Tree
{
    public class IntLit : Node
    {
        private int intVal;

        public IntLit(int i)
        {
            intVal = i;
        }


        public override object eval(Environment e)
        {
            return intVal;
        }

        public override void print(int n)
        {
            printSpaces(n,intVal.ToString());
        }

        public override bool isNumber() { return true; }  // IntLit

    }
}
