using System;

namespace Tree
{
    class NewLine : Node
    {
        public NewLine() { }

        public override void print(int n)
        {
            printSpaces(n, "\n");
        }
    }
}
