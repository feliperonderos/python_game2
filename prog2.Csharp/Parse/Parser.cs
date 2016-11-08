// Parser -- the parser for the Scheme printer and interpreter
//
// Defines
//
//   class Parser;
//
// Parses the language
//
//   exp  ->  ( rest
//         |  #f
//         |  #t
//         |  ' exp
//         |  integer_constant
//         |  string_constant
//         |  identifier
//    rest -> )
//         |  exp+ [. exp] )
//
// and builds a parse tree.  Lists of the form (rest) are further
// `parsed' into regular lists and special forms in the constructor
// for the parse tree node class Cons.  See Cons.parseList() for
// more information.
//
// The parser is implemented as an LL(0) recursive descent parser.
// I.e., parseExp() expects that the first token of an exp has not
// been read yet.  If parseRest() reads the first token of an exp
// before calling parseExp(), that token must be put back so that
// it can be reread by parseExp() or an alternative version of
// parseExp() must be called.
//
// If EOF is reached (i.e., if the scanner returns a NULL) token,
// the parser returns a NULL tree.  In case of a parse error, the
// parser discards the offending token (which probably was a DOT
// or an RPAREN) and attempts to continue parsing with the next token.

using System;
using Tokens;
using Tree;

namespace Parse
{
    public class Parser {
	
        private Scanner scanner;
        private Nil nil;
        private BoolLit tr;
        private BoolLit fa;

        public Parser(Scanner s) {
            scanner = s;
            nil = new Nil();
            tr = new BoolLit(true);
            fa = new BoolLit(false);

        }
        public Node parseExp() {
            return parseExp(scanner.getNextToken());
        }

        public Node parseExp(Token t)
        {
            if (t.getType() == TokenType.LPAREN)
            {
                return parseRest();
                //(rest
                //retur node with parserest
            }
            else if (t.getType() == TokenType.FALSE)
            {
                return fa;
                //return boolnode with false;
            }
            else if (t.getType() == TokenType.TRUE)
            {
                return tr;
                //return boolnode with true;
            }
            else if ((t.getType() == TokenType.QUOTE)|| ((t.getType() == TokenType.IDENT) && (t.getName() == "quote")))
            {
                return new Cons(new Ident("quote"),parseExp());
                //return quote node parseExp() node;
            }
            else if (t.getType() == TokenType.INT)
            {
                return new IntLit(t.getIntVal());
                //return intnode
            }
            else if (t.getType() == TokenType.STRING)
            {
                return new StringLit(t.getStringVal());
                //stringnode
            }
            else if (t.getType() == TokenType.IDENT)
            {
                return new Ident(t.getName());
                //return id Node
            }
            else {
                
            }
            return null;
        }
  
        protected Node parseRest()
        {
            Token t = scanner.getNextToken();
            if (t.getType() == TokenType.RPAREN)
            {
                return nil;
            }
            else if (t.getType() == TokenType.DOT)
            {
                return new Cons(parseExp(t), parseRest());
            }
            else {
                return new Cons(parseExp(t), parseRest());
            }
           
            //return null;
        }

        // TODO: Add any additional methods you might need.
    }
}

