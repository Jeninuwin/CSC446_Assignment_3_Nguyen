namespace CSC446_Assignment_3_Nguyen
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="Lexie" />.
    /// </summary>
    internal class Lexie
    {
        /// <summary>
        /// Defines the nameFile.
        /// </summary>
        internal static string nameFile;

        /// <summary>
        /// Defines the ch.
        /// </summary>
        internal static char ch;

        /// <summary>
        /// Defines the reader.
        /// </summary>
        internal static StreamReader reader;

        /// <summary>
        /// Defines the Symbols.
        /// </summary>
        public enum Symbols
        {
            thent, ift, elset, whilet, floatt, intt, chart, breakt, continuet, voidt, lparent, rparent, unknownt, eoftt, blanks,
            literalt, relopt, assignopt, addopt, mulopt, idt, integert, nott, whitespace, semit, quotet, colont, commat, closeParent,
            openParent, periodt, openCurlyParent, closeCurlyParent, openSquareParent, closeSquareParent, numt, 
        }

        /// <summary>
        /// Defines the Token.
        /// </summary>
        public static Symbols Token;

        /// <summary>
        /// Defines the Lexeme.
        /// </summary>
        public static string Lexeme;

        /// <summary>
        /// Defines the LineNo.
        /// </summary>
        public static int LineNo;

        // static int Value; //integer
        /// <summary>
        /// Defines the ValueR.
        /// </summary>
        public static double ValueR;//real

        /// <summary>
        /// Defines the ValueL.
        /// </summary>
        public static string ValueL;//literal

        /// <summary>
        /// Defines the MatchTokens.
        /// </summary>
        public static List<string> MatchTokens = new List<string>();

        /// <summary>
        /// Defines the counting.
        /// </summary>
        public static int counting;

        /// <summary>
        /// The main lexical analyzer that reads the file and calls the process token
        /// </summary>
        /// <param name="args">The args<see cref="string[]"/>.</param>
        public static void LexicalAnalyzer(string[] args)
        {

        Redo:

            string file_dir = Directory.GetCurrentDirectory() + "\\";

            if (args.Length == 0)
            {
                do
                {
                    Console.WriteLine("Enter your c file name: ");
                    nameFile = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(nameFile));
            }

            if (args.Length > 0 && File.Exists(file_dir + args[0]))
                nameFile = file_dir + args[0];
            else if (File.Exists(file_dir + nameFile))
                nameFile = file_dir + nameFile;
            else
            {
                Console.WriteLine("ERROR: File not found.");
                goto Redo; //i know it's not stable but this is temporary 
            }

            readFile();

            while (ch != 65535 || Token != Symbols.eoftt || ch != '\uffff')
            {
                ProcessToken();
                //DisplayToken();
            }
        }

        /// <summary>
        /// The readFile will read within the file name given and insert it into ch, a character that acts like a list.
        /// </summary>
        public static void readFile()
        {
            reader = new StreamReader(nameFile);
            ch = (char)reader.Read();
        }

        /// <summary>
        /// The ProcessToken will sort out what lexeme or data is and call the appropriate process token.
        /// </summary>
        public static void ProcessToken()
        {
            Lexeme = ch.ToString();
            GetNextChar(); //Look ahead

            //if the lexeme has any letters, go to process word token
            if (Lexeme[0] >= 'A' && Lexeme[0] <= 'Z' || Lexeme[0] >= 'a' && Lexeme[0] <= 'z')
            {
                ProcessWordToken();
            }
            //if the Lexeme has any numbers, go to process num token
            else if (Lexeme[0] >= '0' && Lexeme[0] <= '9')
            {
                ProcessNumToken();
            }
            //if the Lexeme has / AND the character has * to create a comment i.e /* then go to process comment token
            else if (Lexeme[0] == '/' && ch == '*')
            {
                ProcessCommentToken();
                GetNextToken();
            }
            //if the Lexeme has a quotation then go to process literal token. This means it's a string of words
            else if (Lexeme[0] == '"')
            {
                ProcessLiteralToken();
            }
            //if Lexeme has any of the symbols before then go to either process double token or single token 
            else if (Lexeme[0] == '<' || Lexeme[0] == '>' || Lexeme[0] == ':' || Lexeme[0] == '!' || Lexeme[0] == '&' || Lexeme[0] == '|')
            {
                if (ch == '=' || ch == '&' || ch == '|')
                    ProcessDoubleToken();
                else
                    ProcessSingleToken();
            }
            //if Lexeme has any of the below, assign it as whitespace
            else if (Lexeme[0] == ' ' || Lexeme[0] == '\r' || Lexeme[0] == '\t' || Lexeme[0] == '\n')
            {
                Token = Symbols.whitespace;
            }
            else
            {
                ProcessSingleToken();
            }

            if (Lexeme.Length > 27)
            {
                Console.WriteLine("ERROR: Invalid Token. The Length cannot be more than 27.");
                Token = Symbols.unknownt;
                MatchTokens.Add("unknownt");
                counting++;
            }
        }

        /// <summary>
        /// The ProcessCommentToken will convert the comment to be whitespace and will be ultimately ignored.
        /// </summary>
        public static void ProcessCommentToken()
        {
            while (Lexeme[0] != '*' && ch != '/') //until it reached */ of the comment, it will continue to turn everything to whitespace
            {
                GetNextChar();
                if (Lexeme[0] == '*' && ch == '/' || ch == '\uffff')
                {
                    Console.WriteLine("ERROR: Invalid Comment. Comment does not end with a '*/'.");
                    Token = Symbols.unknownt;
                    MatchTokens.Add("unknowt");
                    counting++;
                    break;
                }
                else
                {
                    continue;
                }
            }

            Token = Symbols.whitespace;

            GetNextChar();
        }

        /// <summary>
        /// The ProcessWordToken this will take the word and combine it togehter until there is a whitespace. Then it will assign the token to the correct symbol.
        /// </summary>
        public static void ProcessWordToken()
        {
            int length = 1;

            while (ch >= 'A' && ch <= 'Z' || ch >= 'a' && ch <= 'z' || ch >= '0' && ch <= '9' || ch == '_')
            {
                length++;
                Lexeme += ch.ToString();
                GetNextChar();
            }

            //assign Lexeme tokens to their symbols
            switch (Lexeme.ToLower())
            {
                case "break":
                    MatchTokens.Add("breakt");
                    Token = Symbols.breakt;
                    counting++;

                    break;
                case "char":
                    MatchTokens.Add("chart");
                    Token = Symbols.chart;
                    counting++;

                    break;
                case "continue":
                    MatchTokens.Add("continuet");
                    Token = Symbols.continuet;
                    counting++;

                    break;
                case "else":
                    MatchTokens.Add("elset");
                    Token = Symbols.elset;
                    counting++;

                    break;
                case "float":
                    MatchTokens.Add("floatt");
                    Token = Symbols.floatt;
                    counting++;

                    break;
                case "if":
                    MatchTokens.Add("ift");
                    Token = Symbols.ift;
                    counting++;

                    break;
                case "int":
                    MatchTokens.Add("intt");
                    Token = Symbols.intt;
                    counting++;

                    break;
                case "void":
                    MatchTokens.Add("voidt");
                    Token = Symbols.voidt;
                    counting++;

                    break;
                case "while":
                    MatchTokens.Add("whilet");
                    Token = Symbols.whilet;
                    counting++;

                    break;
                default:
                    MatchTokens.Add("idt");
                    Token = Symbols.idt;
                    counting++;
                    break;
            }
        }

        /// <summary>
        /// The ProcessNumToken will process the numbers in the token. This will also sort out the decimals to be assigned as floats and the other numbers will be integers.
        /// </summary>
        public static void ProcessNumToken()
        {
            int Decimals = 0;

            while (ch >= '0' && ch <= '9' || (ch == '.' && Decimals < 1))
            {
                if (ch == '.')
                {
                    Decimals += 1;
                }

                Lexeme += ch;
                GetNextChar();
            }

            if (Lexeme[Lexeme.Length - 1] == '.')
            {
                Token = Symbols.unknownt;
                MatchTokens.Add("unknownt");
                Console.WriteLine("ERROR: On line" + LineNo + "contains an error");
                return;
            }
            else if (Decimals == 1)
            {
                ValueR = System.Convert.ToDouble(Lexeme);
                Token = Symbols.numt;
                MatchTokens.Add("numt");
            }
            else
            {
                Value = System.Convert.ToInt32(Lexeme);
                Token = Symbols.numt;
                MatchTokens.Add("numt");
            }
        }

        /// <summary>
        /// The GetNextChar takes a look at the next character and stores it into the character ch to be used later.
        /// </summary>
        public static void GetNextChar()
        {
            if (ch == 10)
                LineNo++;
            ch = (char)reader.Read();
        }

        /// <summary>
        /// The ProcessLiteralToken this will process the strings that are surrounded in the quotations and make sure that it is a string, if it does not end with an ending quotation, it will throw an error.
        /// </summary>
        public static void ProcessLiteralToken()
        {
            bool hasEnding = false;
            ValueL = "";

            while (ch != 10 && !reader.EndOfStream && ch != '"')
            {
                //if the lexeme is shorter than 20 characters, keep adding onto the lexeme
                if (Lexeme.Length < 20)
                {
                    Lexeme += ch;
                }

                //add the same characters to the literal value, which will be displayed in Display
                ValueL += ch;
                GetNextChar();

                //if we reach the end of the literal, set isClosed to true and exit this function
                if (ch == '"')
                {
                    hasEnding = true;

                    GetNextChar();

                    break;
                }
            }

            if (!hasEnding)
            {
                MatchTokens.Add("unknownt");
                Token = Symbols.unknownt;
                counting++;

                Console.WriteLine("::ERROR:: No closing quotation!");
            }
            else
            {
                MatchTokens.Add("literalt");
                Token = Symbols.literalt;
                counting++;

            }
        }

        /// <summary>
        /// The GetNextToken.
        /// </summary>
        internal static void GetNextToken()
        {
            //if it's not a space, keep crunching chars
            while (ch <= 32)
            {
                GetNextChar();
            }

            //if it's not the end of the file, process the token
            if (!reader.EndOfStream)
            {
                ProcessToken();
            }

            //otherwise, set the token to eoftt since it's the end of the stream
            else
            {
                MatchTokens.Add("eoftt");
                Token = Symbols.eoftt;
                counting++;
            }
        }

        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        public static int? Value { get; set; } = null;//this resets the value not sure if this is needed but can remove?

        /// <summary>
        /// The DisplayToken will display on the console what the results are.
        /// </summary>
        //public static void DisplayToken()
        //{

        //    if (Token == Symbols.whitespace)
        //    {
        //        return;
        //    }

        //    if (Token == Symbols.eoftt)
        //    {
        //        Lexeme = "eoft";
        //    }

        //    Console.Write(Token.ToString().PadRight(22, ' ') + Lexeme.ToString().PadRight(20, ' '));

        //    if (Token == Symbols.numt)
        //    {
        //        if (Lexeme.Contains('.'))
        //            Console.Write("|| REAL NUM VALUE: " + ValueR);
        //        else
        //            Console.Write("|| INT NUM VALUE: " + Value);
        //    }

        //    else if (Token == Symbols.literalt)
        //        Console.Write("|| LITERAL VALUE: " + "\"" + ValueL + "\"");

        //    else if (Token == Symbols.unknownt)
        //        Console.Write("ERROR: Token is unknown");

        //    Console.Write("\n");
        //}

        /// <summary>
        /// The ProcessSingleToken will process any alone symbols and assign them the correct symbol.
        /// </summary>
        public static void ProcessSingleToken()
        {
            switch (Lexeme)
            {
                case "<":
                case ">":
                case "=":
                    {
                        Token = Symbols.relopt;
                        MatchTokens.Add("relopt");
                        counting++;
                        break;
                    }
                case ".":
                    {
                        Token = Symbols.periodt;
                        MatchTokens.Add("periodt");
                        counting++;
                        break;
                    }
                case "(":
                    {
                        Token = Symbols.lparent;
                        MatchTokens.Add("lparent");
                        counting++;
                        break;
                    }
                case ")":
                    {
                        Token = Symbols.rparent;
                        MatchTokens.Add("rparent");
                        counting++;
                        break;
                    }
                case "{":
                    {
                        Token = Symbols.openCurlyParent;
                        MatchTokens.Add("openCurlyParent");
                        counting++;
                        break;
                    }
                case "}":
                    {
                        Token = Symbols.closeCurlyParent;
                        MatchTokens.Add("closeCurlyParent");
                        counting++;
                        break;
                    }
                case "[":
                    {
                        Token = Symbols.openSquareParent;
                        MatchTokens.Add("openSquareParent");
                        counting++;
                        break;
                    }
                case "]":
                    {
                        Token = Symbols.closeSquareParent;
                        MatchTokens.Add("closeSquareParent");
                        counting++;
                        break;
                    }
                case ",":
                    {
                        Token = Symbols.commat;
                        MatchTokens.Add("commat");
                        counting++;
                        break;
                    }
                case "+":
                case "-":
                case "|":
                    {
                        Token = Symbols.addopt;
                        MatchTokens.Add("addopt");
                        counting++;
                        break;
                    }
                case ":":
                    {
                        Token = Symbols.colont;
                        MatchTokens.Add("colont");
                        counting++;
                        break;
                    }
                case ";":
                    {
                        Token = Symbols.semit;
                        MatchTokens.Add("semit");
                        counting++;
                        break;
                    }
                case "\"\"":
                    {
                        Token = Symbols.quotet;
                        MatchTokens.Add("quotet");
                        counting++;
                        break;
                    }
                case "&":
                case "%":
                case "*":
                case "/":
                    {
                        Token = Symbols.mulopt;
                        MatchTokens.Add("mulopt");
                        counting++;
                        break;
                    }

            };

            if(Lexeme[0] == 65535)
            {
                Token = Symbols.eoftt;
                MatchTokens.Add("eoftt");
                counting++;
            }
            if (Lexeme[0] == '=')
            {
                Token = Symbols.assignopt;
                MatchTokens.Add("assignopt");
                counting++;
            }
        }

        /// <summary>
        /// The ProcessDoubleToken will take any symbols that belong together and assign them a symbol.
        /// </summary>
        public static void ProcessDoubleToken()
        {
            Lexeme += ch;

            switch (Lexeme)
            {
                case "<=":
                    {
                        Lexeme = Lexeme[0].ToString() + ch.ToString();

                        Token = Symbols.relopt;
                        MatchTokens.Add("relopt");
                        counting++;
                        GetNextChar();
                        break;
                    }
                case ">=":
                    {
                        Lexeme = Lexeme[0].ToString() + ch.ToString();
                        Token = Symbols.relopt;
                        MatchTokens.Add("relopt");
                        counting++;
                        GetNextChar();
                        break;
                    }
                case "==":
                    {
                        Lexeme = Lexeme[0].ToString() + ch.ToString();
                        Token = Symbols.relopt;
                        MatchTokens.Add("relopt");
                        counting++;
                        GetNextChar();
                        break;
                    }
                case "!=":
                    {
                        Lexeme = Lexeme[0].ToString() + ch.ToString();
                        Token = Symbols.relopt;
                        MatchTokens.Add("relopt");
                        counting++;
                        GetNextChar();
                        break;
                    }
                case "||":
                    {
                        Lexeme = Lexeme[0].ToString() + ch.ToString();
                        Token = Symbols.addopt;
                        MatchTokens.Add("relopt");
                        counting++;
                        GetNextChar();
                        break;
                    }
                case "&&":
                    {
                        Lexeme = Lexeme[0].ToString() + ch.ToString();
                        Token = Symbols.mulopt;
                        MatchTokens.Add("relopt");
                        counting++;
                        GetNextChar();
                        break;
                    }

            };
            if (Lexeme[0] == 65535) //if this reaches the end of the file set it to eoft
            {
                Token = Symbols.eoftt;
                MatchTokens.Add("eoftt");
                counting++;
            }

        }
    }
}
