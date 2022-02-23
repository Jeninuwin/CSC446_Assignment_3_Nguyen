/// <summary>
/// 
/// </summary>

using System;
using System.Collections.Generic;
using System.IO;

namespace CSC446_Assignment_3_Nguyen
{
    
    /// <summary>
    /// Defines the <see cref="Program" />.
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            Lexie.LexicalAnalyzer(args);
            Console.WriteLine("Lexical Analyzer completed. Commencing Parser.");
            Parser.Parse();
            Console.WriteLine("Parser completed completed.");

            string continueProgram;

        cp:
            Console.WriteLine("\nDo you want to enter another file? Enter Y for yes and N for to exit the program");
            continueProgram = Console.ReadLine();

            if (continueProgram.ToLower() == "n")
            {
                System.Environment.Exit(0);
            }
            else if (continueProgram.ToLower() == "y")
            {
                Lexie.LexicalAnalyzer(args);
            }
            else
            {
                Console.WriteLine("Invalid Response.");
                goto cp;
            }


        }
        //static string nameFile;
        //static char ch;
        //static StreamReader reader;
        //public enum Symbols
        //{
        //    thent, ift, elset, whilet, floatt, intt, chart, breakt, continuet, voidt, lparent, rparent, unknownt, eoftt, blanks,
        //    literal, relopt, assignopt, addopt, mulopt, idt, integert, nott, whitespace, semit, quotet, colont, commat, closeParent,
        //    openParent, periodt, openCurlyParent, closeCurlyParent, openSquareParent, closeSquareParent, numt,
        //}
        //public static Symbols Token;
        //public static string Lexeme;
        //public static int LineNo;
        //// static int Value; //integer 
        //public static double ValueR; //real
        //public static string ValueL; //literal

        //// public static LexicalAnalyzer Lexie;

        ////public static List<string> reservedWords;

        ///// <summary>
        ///// The Main.
        ///// </summary>
        ///// <param name="args">The args<see cref="string[]"/>.</param>
        //public static void Main(string[] args)
        //{

        //Redo:

        //    string file_dir = Directory.GetCurrentDirectory() + "\\";

        //    if (args.Length == 0)
        //    {
        //        do
        //        {
        //            Console.WriteLine("Enter your c file name: ");
        //            nameFile = Console.ReadLine();
        //        } while (string.IsNullOrWhiteSpace(nameFile));
        //    }

        //    if (args.Length > 0 && File.Exists(file_dir + args[0]))
        //        nameFile = file_dir + args[0];
        //    else if (File.Exists(file_dir + nameFile))
        //        nameFile = file_dir + nameFile;
        //    else
        //    {
        //        Console.WriteLine("ERROR: File not found.");
        //        goto Redo; //i know it's not stable but this is temporary 
        //    }

        //    readFile();
        //    string empty = "";

        //    Console.WriteLine("TOKEN: " + empty.PadRight(9, ' ') + "||  LEXEME: " + empty.PadRight(15, ' ') + "|| ATTRIBUTES: ");

        //    while (ch != 65535 || ch != '\uffff')
        //    {
        //        LexicalAnalyzer.Lexie(nameFile);
        //        Parser.Match();
        //    }

        //    string continueProgram;

        //cp:
        //    Console.WriteLine("\nDo you want to enter another file? Enter Y for yes and N for to exit the program");
        //    continueProgram = Console.ReadLine();

        //    if (continueProgram.ToLower() == "n")
        //    {
        //        System.Environment.Exit(0);
        //    }
        //    else if (continueProgram.ToLower() == "y")
        //    {
        //        goto Redo;
        //    }
        //    else
        //    {
        //        Console.WriteLine("Invalid Response.");
        //        goto cp;
        //    }
        //}

        ///// <summary>
        ///// The readFile will read within the file name given and insert it into ch, a character that acts like a list
        ///// </summary>
        //public static void readFile()
        //{
        //    reader = new StreamReader(nameFile);
        //    ch = (char)reader.Read();
        //}


        ///// <summary>
        ///// The ProcessToken will sort out what lexeme or data is and call the appropriate process token 
        ///// </summary>
        ////public static void ProcessToken()
        ////{
        ////    Lexeme = ch.ToString();
        ////    GetNextChar(); //Look ahead

        ////    if the lexeme has any letters, go to process word token
        ////    if (Lexeme[0] >= 'A' && Lexeme[0] <= 'Z' || Lexeme[0] >= 'a' && Lexeme[0] <= 'z')
        ////    {
        ////        ProcessWordToken();
        ////    }
        ////    if the lexeme has any numbers, go to process num token
        ////    else if (Lexeme[0] >= '0' && Lexeme[0] <= '9')
        ////    {
        ////        ProcessNumToken();
        ////    }
        ////    if the lexeme has / AND the character has* to create a comment i.e /* then go to process comment token
        ////    else if (Lexeme[0] == '/' && ch == '*')
        ////    {
        ////        ProcessCommentToken();
        ////    }
        ////    if the lexeme has a quotation then go to process literal token. This means it's a string of words
        ////    else if (Lexeme[0] == '"')
        ////    {
        ////        ProcessLiteralToken();
        ////    }
        ////    if lexeme has any of the symbols before then go to either process double token or single token 
        ////    else if (Lexeme[0] == '<' || Lexeme[0] == '>' || Lexeme[0] == ':' || Lexeme[0] == '!' || Lexeme[0] == '&' || Lexeme[0] == '|')
        ////    {
        ////        if (ch == '=' || ch == '&' || ch == '|')
        ////            ProcessDoubleToken();
        ////        else
        ////            ProcessSingleToken();
        ////    }
        ////    if lexeme has any of the below, assign it as whitespace
        ////    else if (Lexeme[0] == ' ' || Lexeme[0] == '\r' || Lexeme[0] == '\t' || Lexeme[0] == '\n')
        ////    {
        ////        Token = Symbols.whitespace;
        ////    }
        ////    else
        ////    {
        ////        ProcessSingleToken();
        ////    }

        ////    if (Lexeme.Length > 27)
        ////    {
        ////        Console.WriteLine("ERROR: Invalid Token. The Length cannot be more than 27.");
        ////        Token = Symbols.unknownt;
        ////    }
        ////}

        ///// <summary>
        ///// The ProcessCommentToken will convert the comment to be whitespace and will be ultimately ignored
        ///// </summary>
        ////public static void ProcessCommentToken()
        ////{

        ////    while (Lexeme[0] != '*' && ch != '/') //until it reached */ of the comment, it will continue to turn everything to whitespace
        ////    {
        ////        GetNextChar();
        ////        if (Lexeme[0] == '*' && ch == '/' || ch == '\uffff')
        ////        {
        ////            Console.WriteLine("ERROR: Invalid Comment. Comment does not end with a '*/'.");
        ////            Token = Symbols.unknownt;
        ////            break;
        ////        }
        ////        else
        ////        {
        ////            continue;
        ////        }
        ////    }

        ////    if (Lexeme[0] == '*' && ch != '/')
        ////    {
        ////        Console.WriteLine("ERROR: Invalid Comment. Comment does not end with a '*/'.");
        ////    }
        ////    else
        ////    {

        ////    }
        ////    if (ch == ' ')
        ////    {
        ////        (Lexeme[0] != '*' && ch != '/')
        ////        Console.WriteLine("ERROR: Invalid Comment. Comment does not end with a '*/'.");
        ////    }

        ////    Token = Symbols.whitespace;

        ////    GetNextChar();


        ////}

        ///// <summary>
        ///// The ProcessWordToken this will take the word and combine it togehter until there is a whitespace.Then it will assign the token to the correct symbol.
        ///// </summary>
        ////public static void ProcessWordToken()
        ////{
        ////    int length = 1;

        ////    while (ch >= 'A' && ch <= 'Z' || ch >= 'a' && ch <= 'z' || ch >= '0' && ch <= '9' || ch == '_')
        ////    {
        ////        length++;
        ////        Lexeme += ch.ToString();
        ////        GetNextChar();
        ////    }

        ////    assign lexeme tokens to their symbols
        ////    switch (Lexeme.ToLower())
        ////    {
        ////        case "if":
        ////            Token = Symbols.ift;
        ////            break;
        ////        case "else":
        ////            Token = Symbols.elset;
        ////            break;
        ////        case "while":
        ////            Token = Symbols.whilet;
        ////            break;
        ////        case "float":
        ////            Token = Symbols.floatt;
        ////            break;
        ////        case "int":
        ////            Token = Symbols.intt;
        ////            break;
        ////        case "char":
        ////            Token = Symbols.chart;
        ////            break;
        ////        case "break":
        ////            Token = Symbols.breakt;
        ////            break;
        ////        case "continue":
        ////            Token = Symbols.continuet;
        ////            break;
        ////        case "void":
        ////            Token = Symbols.voidt;
        ////            break;
        ////        default:
        ////            Token = Symbols.idt;
        ////            break;

        ////    }
        ////}

        ///// <summary>
        ///// The ProcessNumToken will process the numbers in the token.This will also sort out the decimals to be assigned as floats and the other numbers will be integers 
        ///// </summary>
        ////public static void ProcessNumToken()
        ////{
        ////    int Decimals = 0;

        ////    while (ch >= '0' && ch <= '9' || (ch == '.' && Decimals < 1))
        ////    {
        ////        if (ch == '.')
        ////        {
        ////            Decimals += 1;
        ////        }

        ////        Lexeme += ch;
        ////        GetNextChar();
        ////    }

        ////    if (Lexeme[Lexeme.Length - 1] == '.')
        ////    {
        ////        Token = Symbols.unknownt;
        ////        Console.WriteLine("ERROR: On line" + LineNo + "contains an error");
        ////    }
        ////    else if (Decimals == 1)
        ////    {
        ////        ValueR = System.Convert.ToDouble(Lexeme);
        ////        Token = Symbols.numt;
        ////    }
        ////    else
        ////    {
        ////        Value = System.Convert.ToInt32(Lexeme);
        ////        Token = Symbols.numt;
        ////    }
        ////}

        ///// <summary>
        ///// The GetNextChar takes a look at the next character and stores it into the character ch to be used later
        ///// </summary>
        ////public static void GetNextChar()
        ////{
        ////    if (ch == 10)
        ////        LineNo++;
        ////    ch = (char)reader.Read();
        ////}

        ///// <summary>
        ///// The ProcessLiteralToken this will process the strings that are surrounded in the quotations and make sure that it is a string, if it does not end with an ending quotation, it will throw an error
        ///// </summary>
        ////public static void ProcessLiteralToken()
        ////{
        ////    bool hasEnding = false;
        ////    ValueL = "";

        ////    while (ch != 10 && !reader.EndOfStream && ch != '"')
        ////    {
        ////        if (Lexeme.Length < 17)
        ////            Lexeme += ch;

        ////        ValueL += ch;
        ////        GetNextChar();

        ////        if (ch == '"')
        ////        {
        ////            hasEnding = true;
        ////            GetNextChar();
        ////            break;
        ////        }
        ////    }
        ////    if (!hasEnding)
        ////    {
        ////        Token = Symbols.unknownt;
        ////        Console.WriteLine("ERROR: On the line " + LineNo + " it is an incomplete literal");
        ////    }
        ////    else
        ////    {
        ////        Token = Symbols.literal;
        ////    }
        ////}

        ///// <summary>
        ///// Gets or sets the Value.
        ///// </summary>
        ////public static int? Value { get; set; } = null;//this resets the value not sure if this is needed but can remove?

        ///// <summary>
        ///// The DisplayToken will display on the console what the results are
        ///// </summary>
        ////public static void DisplayToken()
        ////{

        ////    if (Token == Symbols.whitespace)
        ////    {
        ////        return;
        ////    }

        ////    if (Token == Symbols.eoftt)
        ////    {
        ////        Lexeme = "eoft";
        ////    }

        ////    Console.Write(Token.ToString().PadRight(22, ' ') + Lexeme.ToString().PadRight(20, ' '));

        ////    if (Token == Symbols.numt)
        ////    {
        ////        if (Lexeme.Contains('.'))
        ////            Console.Write("|| REAL NUM VALUE: " + ValueR);
        ////        else
        ////            Console.Write("|| INT NUM VALUE: " + Value);
        ////    }

        ////    else if (Token == Symbols.literal)
        ////        Console.Write("|| LITERAL VALUE: " + "\"" + ValueL + "\"");

        ////    else if (Token == Symbols.unknownt)
        ////        Console.Write("ERROR: Token is unknown");

        ////    Console.Write("\n");
        ////}

        ///// <summary>
        ///// The ProcessSingleToken will process any alone symbols and assign them the correct symbol
        ///// </summary>
        ////public static void ProcessSingleToken()
        ////{
        ////    switch (Lexeme)
        ////    {
        ////        case "<":
        ////        case ">":
        ////        case "=":
        ////            {
        ////                Token = Symbols.relopt;
        ////                break;
        ////            }
        ////        case ".":
        ////            {
        ////                Token = Symbols.periodt;
        ////                break;
        ////            }
        ////        case "(":
        ////            {
        ////                Token = Symbols.openParent;
        ////                break;
        ////            }
        ////        case ")":
        ////            {
        ////                Token = Symbols.closeParent;
        ////                break;
        ////            }
        ////        case "{":
        ////            {
        ////                Token = Symbols.openCurlyParent;
        ////                break;
        ////            }
        ////        case "}":
        ////            {
        ////                Token = Symbols.closeCurlyParent;
        ////                break;
        ////            }
        ////        case "[":
        ////            {
        ////                Token = Symbols.openSquareParent;
        ////                break;
        ////            }
        ////        case "]":
        ////            {
        ////                Token = Symbols.closeSquareParent;
        ////                break;
        ////            }
        ////        case ",":
        ////            {
        ////                Token = Symbols.commat;
        ////                break;
        ////            }
        ////        case "+":
        ////        case "-":
        ////        case "|":
        ////            {
        ////                Token = Symbols.addopt;
        ////                break;
        ////            }
        ////        case ":":
        ////            {
        ////                Token = Symbols.colont;
        ////                break;
        ////            }
        ////        case ";":
        ////            {
        ////                Token = Symbols.semit;
        ////                break;
        ////            }
        ////        case "\"\"":
        ////            {
        ////                Token = Symbols.quotet;
        ////                break;
        ////            }
        ////        case "&":
        ////        case "%":
        ////        case "*":
        ////        case "/":
        ////            {
        ////                Token = Symbols.mulopt;
        ////                break;
        ////            }
        ////        default:
        ////            {
        ////                Token = Symbols.unknownt;
        ////                break;
        ////            }

        ////    };

        ////    if (Lexeme[0] == '=')
        ////    {
        ////        Token = Symbols.assignopt;
        ////    }
        ////}

        ///// <summary>
        ///// The ProcessDoubleToken will take any symbols that belong together and assign them a symbol
        ///// </summary>
        ////public static void ProcessDoubleToken()
        ////{
        ////    Lexeme += ch;

        ////    switch (Lexeme)
        ////    {
        ////        case "<=":
        ////            {
        ////                Lexeme = Lexeme[0].ToString() + ch.ToString();

        ////                Token = Symbols.relopt;
        ////                GetNextChar();
        ////                break;
        ////            }
        ////        case ">=":
        ////            {
        ////                Lexeme = Lexeme[0].ToString() + ch.ToString();
        ////                Token = Symbols.relopt;
        ////                GetNextChar();
        ////                break;
        ////            }
        ////        case "==":
        ////            {
        ////                Lexeme = Lexeme[0].ToString() + ch.ToString();
        ////                Token = Symbols.relopt;
        ////                GetNextChar();
        ////                break;
        ////            }
        ////        case "!=":
        ////            {
        ////                Lexeme = Lexeme[0].ToString() + ch.ToString();
        ////                Token = Symbols.relopt;
        ////                GetNextChar();
        ////                break;
        ////            }
        ////        case "||":
        ////            {
        ////                Lexeme = Lexeme[0].ToString() + ch.ToString();
        ////                Token = Symbols.addopt;
        ////                GetNextChar();
        ////                break;
        ////            }
        ////        case "&&":
        ////            {
        ////                Lexeme = Lexeme[0].ToString() + ch.ToString();
        ////                Token = Symbols.mulopt;
        ////                GetNextChar();
        ////                break;
        ////            }
        ////    };

        ////}
    }
}
