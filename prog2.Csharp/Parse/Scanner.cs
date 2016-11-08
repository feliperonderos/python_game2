// Scanner -- The lexical analyzer for the Scheme printer and interpreter

using System;
using System.IO;
using Tokens;

namespace Parse
{
    public class Scanner
    {
        private TextReader In;

        // maximum length of strings and identifier
        private const int BUFSIZE = 1000;
        private char[] buf = new char[BUFSIZE];

        public Scanner(TextReader i) { In = i; }
  
        // TODO: Add any other methods you need

        public Token getNextToken()
        {
            int ch;
            var abc = "$!%&*+-./:<=>?@^_~";

            try
            {
                // It would be more efficient if we'd maintain our own
                // input buffer and read characters out of that
                // buffer, but reading individual characters from the
                // input stream is easier.

                ch = In.Read();
                

                // TODO: skip white space and comments

                while ((ch == '\n') || (ch == '\r')||(ch ==' ')||(ch == '\t')|| (ch == '\f')) {
                    ch = In.Read();
                }
                if (ch == -1)
                    return null;
                else if (ch == ';')
                {
                    while ((ch = In.Read()) != '\n')
                    {
                        ;
                    }
                    return getNextToken();
                }
                // Special characters
                else if (ch == '\'')
                    return new Token(TokenType.QUOTE);
                else if (ch == '(')
                    return new Token(TokenType.LPAREN);
                else if (ch == ')')
                    return new Token(TokenType.RPAREN);
                else if (ch == '.')
                    // We ignore the special identifier `...'.
                    return new Token(TokenType.DOT);
                
                // Boolean constants
                else if (ch == '#')
                {
                    ch = In.Read();

                    if (ch == 't')
                        return new Token(TokenType.TRUE);
                    else if (ch == 'f')
                        return new Token(TokenType.FALSE);
                    else if (ch == -1)
                    {
                        Console.Error.WriteLine("Unexpected EOF following #");
                        return null;
                    }
                    else
                    {
                        Console.Error.WriteLine("Illegal character '" +
                                                (char)ch + "' following #");
                        return getNextToken();
                    }
                }

                // String constants
                else if (ch == '"')
                {
                    buf = new char[BUFSIZE];
                    int u = 0;
                    while ((In.Peek() != '"')&& (In.Peek() != -1)) {
                        ch = In.Read();
                        buf[u] = (char)ch;
                        u++;
                    }
                    ch = In.Read();
                    
                    return new StringToken(new String(buf, 0, u));
                }

    
                // Integer constants
                else if (ch >= '0' && ch <= '9')
                {
                    int i = ch - '0';
                    while (In.Peek() >= '0' && In.Peek()<= '9') {
                        i = i * 10 + (In.Read() - '0');
                    }
                        // TODO: scan the number and convert it to an integer

                        // make sure that the character following the integer
                        // is not removed from the input stream
                    return new IntToken(i);
                }

                // Identifiers
                
                else if ((ch >= 'A' && ch <= 'Z')
                    || (ch >= 'a' && ch <= 'z')
                    || (abc.ToLower().IndexOf((char)ch) != -1)

                         // or ch is some other valid first character
                         // for an identifier
                         ) {
                   
                
                    // TODO: scan an identifier into the buffer
                    int u = 0;
                    buf = new char[BUFSIZE];
                    buf[u++] = (char)ch;
                    while ((In.Peek() >= 'A' && In.Peek() <= 'Z')
                        || (In.Peek() >= 'a' && In.Peek() <= 'z')
                        || (abc.ToLower().IndexOf((char)In.Peek()) != -1)
                        || (In.Peek() >= '0' && In.Peek() <= '9'))
                    {
                        buf[u++] = (char)In.Read();

                    }

                    // make sure that the character following the integer
                    // is not removed from the input stream

                    return new IdentToken(new String(buf, 0, u).ToLower());
                }
    
                // Illegal character
                else
                {
                    Console.Error.WriteLine("Illegal input character '"
                                            + (char)ch + '\'');
                    return getNextToken();
                }
            }
            catch (IOException e)
            {
                Console.Error.WriteLine("IOException: " + e.Message);
                return null;
            }
        }
    }

}

