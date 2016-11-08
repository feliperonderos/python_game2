// BoolLit -- Parse tree node class for representing boolean literals

using System;

namespace Tree
{
    public class BoolLit : Node
    {
        private bool boolVal;

        private static BoolLit trueInstance = new BoolLit(true);
        private static BoolLit falseInstance = new BoolLit(false);
  
        public BoolLit(bool b)
        {
            boolVal = b;
        }

        public override object eval(Environment e)
        {
            return boolVal;
        }


        public static BoolLit getInstance(bool val)
        {
            if (val)
                return trueInstance;
            else
                return falseInstance;
        }

        public override void print(int n)
        {
            if (boolVal)
                printSpaces(n,"#t");
            else
                printSpaces(n,"#f");
        }

        public override bool isBool()
        {
            return true;
        }

    }
}
