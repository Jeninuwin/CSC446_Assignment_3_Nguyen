using System;
using System.Collections.Generic;
using System.IO;
using CSC446_Assignment_3_Nguyen;

/// <summary>
/// Assignment 1: Creating a Lexical Analyzer
/// </summary>
namespace CSC446_Assignment_3_Nguyen
{
    class LexicalAnalyzer
    {
        static string nameFile;
        static char ch;
        static StreamReader reader;

        public static List<string> reservedWords;

        public LexicalAnalyzer(string filename)
        {
            reader = new StreamReader(nameFile);
            ch = (char)reader.Read();
            reservedWords = new List<string> { "if", "else", "while", "float", "int", "char", "break", "continue", "float", "void" };
            
        }
        /// <summary>
        /// The ProcessToken will sort out what lexeme or data is and call the appropriate process token 
        /// </summary>
        internal static void ProcessToken()
        {
            Program.Lexeme = ch.ToString();
            GetNextChar(); //Look ahead

            //if the lexeme has any letters, go to process word token
            if (Program.Lexeme[0] >= 'A' && Program.Lexeme[0] <= 'Z' || Program.Lexeme[0] >= 'a' && Program.Lexeme[0] <= 'z')
            {
                ProcessWordToken();
            }
            //if the Program.Lexeme has any numbers, go to process num token
            else if (Program.Lexeme[0] >= '0' && Program.Lexeme[0] <= '9')
            {
                ProcessNumToken();
            }
            //if the Program.Lexeme has / AND the character has * to create a comment i.e /* then go to process comment token
            else if (Program.Lexeme[0] == '/' && ch == '*')
            {
                ProcessCommentToken();
            }
            //if the Program.Lexeme has a quotation then go to process literal token. This means it's a string of words
            else if (Program.Lexeme[0] == '"')
            {
                ProcessLiteralToken();
            }
            //if Program.Lexeme has any of the symbols before then go to either process double token or single token 
            else if (Program.Lexeme[0] == '<' || Program.Lexeme[0] == '>' || Program.Lexeme[0] == ':' || Program.Lexeme[0] == '!' || Program.Lexeme[0] == '&' || Program.Lexeme[0] == '|')
            {
                if (ch == '=' || ch == '&' || ch == '|')
                    ProcessDoubleToken();
                else
                    ProcessSingleToken();
            }
            //if Program.Lexeme has any of the below, assign it as whitespace
            else if (Program.Lexeme[0] == ' ' || Program.Lexeme[0] == '\r' || Program.Lexeme[0] == '\t' || Program.Lexeme[0] == '\n')
            {
                Token = Symbols.whitespace;
            }
            else
            {
                ProcessSingleToken();
            }

            if (Program.Lexeme.Length > 27)
            {
                Console.WriteLine("ERROR: Invalid Token. The Length cannot be more than 27.");
                Token = Symbols.unknownt;
            }
        }

        /// <summary>
        /// The ProcessCommentToken will convert the comment to be whitespace and will be ultimately ignored
        /// </summary>
        internal static void ProcessCommentToken()
        {
            while (Program.Lexeme[0] != '*' && ch != '/') //until it reached */ of the comment, it will continue to turn everything to whitespace
            {
                GetNextChar();
            }

            Token = Symbols.whitespace;

            GetNextChar();
        }

        /// <summary>
        /// The ProcessWordToken this will take the word and combine it togehter until there is a whitespace. Then it will assign the token to the correct symbol.
        /// </summary>
        internal static void ProcessWordToken()
        {
            int length = 1;

            while (ch >= 'A' && ch <= 'Z' || ch >= 'a' && ch <= 'z' || ch >= '0' && ch <= '9' || ch == '_')
            {
                length++;
                Program.Lexeme += ch.ToString();
                GetNextChar();
            }

            //assign Program.Lexeme tokens to their symbols
            switch (Program.Lexeme.ToLower())
            {
                case "if":
                    Token = Symbols.ift;
                    break;
                case "else":
                    Token = Symbols.elset;
                    break;
                case "while":
                    Token = Symbols.whilet;
                    break;
                case "float":
                    Token = Symbols.floatt;
                    break;
                case "int":
                    Token = Symbols.intt;
                    break;
                case "char":
                    Token = Symbols.chart;
                    break;
                case "break":
                    Token = Symbols.breakt;
                    break;
                case "continue":
                    Token = Symbols.continuet;
                    break;
                case "void":
                    Token = Symbols.voidt;
                    break;
                default:
                    Token = Symbols.idt;
                    break;

            }
        }

        /// <summary>
        /// The ProcessNumToken will process the numbers in the token. This will also sort out the decimals to be assigned as floats and the other numbers will be integers 
        /// </summary>
        internal static void ProcessNumToken()
        {
            int Decimals = 0;

            while (ch >= '0' && ch <= '9' || (ch == '.' && Decimals < 1))
            {
                if (ch == '.')
                {
                    Decimals += 1;
                }

                Program.Lexeme += ch;
                GetNextChar();
            }

            if (Program.Lexeme[Program.Lexeme.Length - 1] == '.')
            {
                Token = Symbols.unknownt;
                Console.WriteLine("ERROR: On line" + LineNo + "contains an error");
            }
            else if (Decimals == 1)
            {
                ValueR = System.Convert.ToDouble(Program.Lexeme);
                Token = Symbols.numt;
            }
            else
            {
                Value = System.Convert.ToInt32(Program.Lexeme);
                Token = Symbols.numt;
            }
        }

        /// <summary>
        /// The GetNextChar takes a look at the next character and stores it into the character ch to be used later
        /// </summary>
        internal static void GetNextChar()
        {
            if (ch == 10)
                LineNo++;
            ch = (char)reader.Read();
        }

        /// <summary>
        /// The ProcessLiteralToken this will process the strings that are surrounded in the quotations and make sure that it is a string, if it does not end with an ending quotation, it will throw an error
        /// </summary>
        internal static void ProcessLiteralToken()
        {
            bool hasEnding = false;
            ValueL = "";

            while (ch != 10 && !reader.EndOfStream && ch != '"')
            {
                if (Program.Lexeme.Length < 17)
                    Program.Lexeme += ch;

                ValueL += ch;
                GetNextChar();

                if (ch == '"')
                {
                    hasEnding = true;
                    GetNextChar();
                    break;
                }
            }
            if (!hasEnding)
            {
                Token = Symbols.unknownt;
                Console.WriteLine("ERROR: On the line " + LineNo + " it is an incomplete literal");
            }
            else
            {
                Token = Symbols.literal;
            }
        }

        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        public static int? Value { get; set; } = null;//this resets the value not sure if this is needed but can remove?

        /// <summary>
        /// The DisplayToken will display on the console what the results are
        /// </summary>
        internal static void DisplayToken()
        {

            if (Token == Symbols.whitespace)
            {
                return;
            }

            if (Token == Symbols.eoftt)
            {
                Program.Lexeme = "eoft";
            }

            Console.Write(Token.ToString().PadRight(22, ' ') + Program.Lexeme.ToString().PadRight(20, ' '));

            if (Token == Symbols.numt)
            {
                if (Program.Lexeme.Contains('.'))
                    Console.Write("|| REAL NUM VALUE: " + ValueR);
                else
                    Console.Write("|| INT NUM VALUE: " + Value);
            }

            else if (Token == Symbols.literal)
                Console.Write("|| LITERAL VALUE: " + "\"" + ValueL + "\"");

            else if (Token == Symbols.unknownt)
                Console.Write("ERROR: Token is unknown");

            Console.Write("\n");
        }

        /// <summary>
        /// The ProcessSingleToken will process any alone symbols and assign them the correct symbol
        /// </summary>
        internal static void ProcessSingleToken()
        {
            switch (Program.Lexeme)
            {
                case "<":
                case ">":
                case "=":
                    {
                        Token = Symbols.relopt;
                        break;
                    }
                case ".":
                    {
                        Token = Symbols.periodt;
                        break;
                    }
                case "(":
                    {
                        Token = Symbols.openParent;
                        break;
                    }
                case ")":
                    {
                        Token = Symbols.closeParent;
                        break;
                    }
                case "{":
                    {
                        Token = Symbols.openCurlyParent;
                        break;
                    }
                case "}":
                    {
                        Token = Symbols.closeCurlyParent;
                        break;
                    }
                case "[":
                    {
                        Token = Symbols.openSquareParent;
                        break;
                    }
                case "]":
                    {
                        Token = Symbols.closeSquareParent;
                        break;
                    }
                case ",":
                    {
                        Token = Symbols.commat;
                        break;
                    }
                case "+":
                case "-":
                case "|":
                    {
                        Token = Symbols.addopt;
                        break;
                    }
                case ":":
                    {
                        Token = Symbols.colont;
                        break;
                    }
                case ";":
                    {
                        Token = Symbols.semit;
                        break;
                    }
                case "\"\"":
                    {
                        Token = Symbols.quotet;
                        break;
                    }
                case "&":
                case "%":
                case "*":
                case "/":
                    {
                        Token = Symbols.mulopt;
                        break;
                    }
                default:
                    {
                        Token = Symbols.unknownt;
                        break;
                    }

            };

            if (Program.Lexeme[0] == '=')
            {
                Token = Symbols.assignopt;
            }
        }

        /// <summary>
        /// The ProcessDoubleToken will take any symbols that belong together and assign them a symbol
        /// </summary>
        internal static void ProcessDoubleToken()
        {
            Program.Lexeme += ch;

            switch (Program.Lexeme)
            {
                case "<=":
                    {
                        Program.Lexeme = Program.Lexeme[0].ToString() + ch.ToString();

                        Token = Symbols.relopt;
                        GetNextChar();
                        break;
                    }
                case ">=":
                    {
                        Program.Lexeme = Program.Lexeme[0].ToString() + ch.ToString();
                        Token = Symbols.relopt;
                        GetNextChar();
                        break;
                    }
                case "==":
                    {
                        Program.Lexeme = Program.Lexeme[0].ToString() + ch.ToString();
                        Token = Symbols.relopt;
                        GetNextChar();
                        break;
                    }
                case "!=":
                    {
                        Program.Lexeme = Program.Lexeme[0].ToString() + ch.ToString();
                        Token = Symbols.relopt;
                        GetNextChar();
                        break;
                    }
                case "||":
                    {
                        Program.Lexeme = Program.Lexeme[0].ToString() + ch.ToString();
                        Token = Symbols.addopt;
                        GetNextChar();
                        break;
                    }
                case "&&":
                    {
                        Program.Lexeme = Program.Lexeme[0].ToString() + ch.ToString();
                        Token = Symbols.mulopt;
                        GetNextChar();
                        break;
                    }
            };

        }
    }
}
