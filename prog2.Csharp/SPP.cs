// SPP -- The main program of the Scheme pretty printer.

using Tree;
using System;
using Parse;
using Tokens;


public class SPP
{
    public static int Main(string[] args)
    {
        // Create scanner that reads from standard input
        Scanner scanner = new Scanner(Console.In);
        /*
        if (args.Length > 1 ||
            (args.Length == 1 && ! args[0].Equals("-d")))
        {
            Console.Error.WriteLine("Usage: mono SPP [-d]");
            return 1;
        }
        
        // If command line option -d is provided, debug the scanner.
        if (args.Length == 1 && args[0].Equals("-d"))
        {
            // Console.Write("Scheme 4101> ");
            Token tok = scanner.getNextToken();
            while (tok != null)
            {
                TokenType tt = tok.getType();

                Console.Write(tt);
                if (tt == TokenType.INT)
                    Console.WriteLine(", intVal = " + tok.getIntVal());
                else if (tt == TokenType.STRING)
                    Console.WriteLine(", stringVal = " + tok.getStringVal());
                else if (tt == TokenType.IDENT)
                    Console.WriteLine(", name = " + tok.getName());
                else
                    Console.WriteLine();

                // Console.Write("Scheme 4101> ");
                tok = scanner.getNextToken();
            }
            return 0;
        }
        */
        // Create parser
        Parser parser = new Parser(scanner);
        Node root;
        // TODO: Create and populate the built-in environment and
        // create the top-level environment

        // Read-eval-print loop

        // TODO: print prompt and evaluate the expression
        TreeBuilder builder = new TreeBuilder();
        Tree.Environment env = new Tree.Environment();

        Console.Write("Scheme 4101> ");
        root = parser.parseExp();
        while (root != null)
        {
            
            //root = (Node)builder.buildCons(root, null);
            Object o = root.eval(env);
            if (o is Node)
            {
                ((Node)root.eval(env)).print(0);
                //System.Console.WriteLine(((Node)root.eval(env)).eval(env));
            }
            else if (o == null)
            {
                ;
            }
            else {
                root.print(0);
            }
            Console.WriteLine();
            Console.Write("Scheme 4101> ");
            root = parser.parseExp();
        }
        
        

        return 0;
    }
}
