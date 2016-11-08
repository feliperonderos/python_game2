// Regular -- Parse tree node strategy for printing regular lists

using System;

namespace Tree
{
    public class Regular : Special
    {
        public Regular() { }

        /*
         * called with 4, true
         * call car with 4, false
         *      called with false
         *      print ( call car with true, -1)
         * call cdr with 4, true
         */
        public override void print(Node t, int n, int indentation, bool p, int state)
        /*STATES 
         * 1 print on one line single spaced. no newline. Pass 1 to descendants
         * 2 used by if... do not print newline + spaces pass 1 to car 3 to cdr.
         * 3 used by if, cond... print newline + spaces before. pass 1 to car, 3 to cdr 
         * */
        {
            int nCAR, nCDR;
            nCAR = 0;
            nCDR = 1;
            int cars = 1;
            int cdrs = 1;
            if (state == 1)
            {
                nCAR = 1;
                cars = 1;
                cdrs = 1;
            }
            else if (state == 4) {
                nCAR = 1;
                cars = 4;
                cdrs = 4;
            }
            else if (state == 2)
            {
                nCAR = 1;
                cars = 1;
                cdrs = 3;
            }
            else if (state == 3)
            {
                cars = 1;
                cdrs = 3;
                Console.WriteLine("");
                printSpaces(indentation * 4);
            }
            if (!p) {
                printSpaces(n);
                Console.Write("(");
                nCAR = 0;
                t.getCar().print(nCAR, indentation, false, cars);
                t.getCdr().print(nCDR, indentation, true,cdrs);
            }
            else
            {
                //  if (state == 2) Console.Write(" ");
                t.getCar().print(nCAR, indentation, false, cars);
                t.getCdr().print(nCDR, indentation, true, cdrs);
            }

        }
    }
}


