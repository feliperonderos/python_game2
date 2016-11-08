// Nil -- Parse tree node class for representing the empty list

using System;

namespace Tree
{
    public class Nil : Node
    {
        private static Nil instance = new Nil();

        public Nil() { }

        public static Nil getInstance()
        {
            return instance;
        }


        public override object eval(Environment e)
        {
            return null;
        }

        public override void print(int n)
        {
            print(n, 0, false,1);
        }

        /*STATES 
            * 1 print on one line single spaced. no newline. Pass 1 to descendants
            * 2 used by if... do not print newline + spaces pass 1 to car 3 to cdr.
            * 3 used by if, cond... print newline + spaces before. pass 1 to car, 3 to cdr 
            * */
        public override void print(int n, int indentation, bool p, int state)
        {
            /* if (state == 1)
             {

             }
             else if (state == 2)
             {

             }
             else if (state == 3) {

             }*/
            if (state == 3)
            {
                Console.WriteLine("");
                printSpaces((indentation*4)-4, ")");
            }
            else
            {
                if (p)
                    printSpaces(0, ")");
                else
                    printSpaces(n, "()");
            }
        }

        public override bool isNull() { return true; }

    }
}
