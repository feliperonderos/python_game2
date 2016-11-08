// StringLit -- Parse tree node class for representing string literals

using System;

namespace Tree
{
    public class StringLit : Node
    {
        private string stringVal;

        public StringLit(string s)
        {
            stringVal = s;
        }


        public override object eval(Environment e)
        {
            return stringVal;
        }
        public override bool isString() { return true; }  // StringLit

        public override void print(int n)
        {
            printSpaces(n, "\"" + stringVal + "\"");
        }
    }
}

