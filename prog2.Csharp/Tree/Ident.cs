// Ident -- Parse tree node class for representing identifiers

using System;

namespace Tree
{
    public class Ident : Node
    {
        private string name;

        public Ident(string n)
        {
            name = n;
        }

        public override object eval(Environment e)
        {
            if ((!(e.find(name).isNull())) && (e.find(name).isProcedure())) {
                return e.find(name);
            }
            return e.find(name);
        }


        public override bool isSymbol() { return true; }  // Ident

        public override void print(int n)
        {
            printSpaces(n, name);
        }

        public override string getName()
        {
            return name; 
        }
    }
}

