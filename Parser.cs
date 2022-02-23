using System;


/// <summary>
/// Assignment 3: creating a Parser
/// </summary>
namespace CSC446_Assignment_3_Nguyen
{
    class Parser
    {
        static int count = 0;
        public static void Parse()
        {
            //Printing out the token list
            for (int i = 0; i < Lexie.counting; i++)
            {
                Console.WriteLine(Lexie.MatchTokens[i]);
            }


            CallProg();
        }
        static void CallProg()
        {
            bool isComplete = false;

            while (isComplete == false)
            {
                //GetNextToken
                if (Lexie.MatchTokens[count] == "intt" | Lexie.MatchTokens[count] == "floatt" | Lexie.MatchTokens[count] == "chart")
                {
                    count++;
                    if (Lexie.MatchTokens[count] == "idt")
                    {
                        count++;
                        CallRest();
                    }

                    else
                    {
                        Console.WriteLine("::ERROR:: Expected MatchTokens: idt. Got:" + Lexie.MatchTokens[count]);
                        Environment.Exit(1);
                    }
                }




                else if (Lexie.MatchTokens[count] == "eoftt")
                {
                    isComplete = true;
                }

                else
                {
                    Console.WriteLine("::ERROR:: Expected MatchTokens: intt, floatt, or chart. Got:" + Lexie.MatchTokens[count]);
                    isComplete = true;
                    Environment.Exit(1);
                }
            }
        }

        static void CallRest()
        {
            if (Lexie.MatchTokens[count] == "lparent")
            {
                count++;
                CallParamlist();

                if (Lexie.MatchTokens[count] == "rparent")
                {
                    count++;
                    CallCompound();
                }

            }

            else if (Lexie.MatchTokens[count] == "commat" || Lexie.MatchTokens[count] == "semit")
            {
                CallIDTail();
                if (Lexie.MatchTokens[count] == "semit")
                {
                    count++;
                }

                else
                {
                    Console.WriteLine("::ERROR:: Expected MatchTokens: semit. Got: " + Lexie.MatchTokens[count]);
                    Environment.Exit(1);
                }
            }

            else
            {
                Console.WriteLine("::ERROR:: Expected MatchTokens: lparent, commat, or semit. Got: " + Lexie.MatchTokens[count]);
                Environment.Exit(1);
            }
        }

        static void CallParamlist()
        {
            if (Lexie.MatchTokens[count] == "intt" | Lexie.MatchTokens[count] == "floatt" | Lexie.MatchTokens[count] == "chart")
            {
                count++;
                if (Lexie.MatchTokens[count] == "idt")
                {
                    count++;
                    CallParamTail();
                }

                else
                {
                    Console.WriteLine("::ERROR:: Expected MatchTokens: idt. Got: " + Lexie.MatchTokens[count]);
                    Environment.Exit(1);
                }
            }

            else if (Lexie.MatchTokens[count] == "rparent")
            {

            }

            else
            {
                Console.WriteLine("::ERROR:: Expected MatchTokens: rparent. Got: " + Lexie.MatchTokens[count]);
                Environment.Exit(1);

            }
        }

        static void CallParamTail()
        {
            if (Lexie.MatchTokens[count] == "commat")
            {
                count++;
                if (Lexie.MatchTokens[count] == "intt" | Lexie.MatchTokens[count] == "floatt" | Lexie.MatchTokens[count] == "chart")
                {
                    count++;
                    if (Lexie.MatchTokens[count] == "idt")
                    {
                        count++;
                        CallParamTail();
                    }

                    else
                    {
                        Console.WriteLine("::ERROR:: Expected MatchTokens: idt. Got: " + Lexie.MatchTokens[count]);
                        Environment.Exit(1);

                    }
                }

                else
                {
                    Console.WriteLine("::ERROR:: Expected MatchTokens: intt, floatt, or chart. Got:" + Lexie.MatchTokens[count]);
                    Environment.Exit(1);

                }

            }

            else if (Lexie.MatchTokens[count] == "rparent")
            {

            }

            else
            {
                Console.WriteLine("::ERROR:: Expected MatchTokens: rparent. Got: " + Lexie.MatchTokens[count]);
                Environment.Exit(1);

            }
        }

        static void CallCompound()
        {
            if (Lexie.MatchTokens[count] == "openCurlyParent")
            {
                count++;
                CallDecl();
                if (Lexie.MatchTokens[count] == "closeCurlyParent")
                {
                    count++;
                    CallProg();
                    if (Lexie.MatchTokens[count] == "eoftt")
                    {

                    }
                }

                else
                {
                    Console.WriteLine("::ERROR:: Expected MatchTokens: closeCurlyParent. Got: " + Lexie.MatchTokens[count]);
                    Environment.Exit(1);

                }
            }

            else
            {
                Console.WriteLine("::ERROR:: Expected MatchTokens: openCurlyParent. Got: " + Lexie.MatchTokens[count]);
                Environment.Exit(1);

            }
        }

        static void CallDecl()
        {
            if (Lexie.MatchTokens[count] == "intt" | Lexie.MatchTokens[count] == "floatt" | Lexie.MatchTokens[count] == "chart")
            {
                count++;
                CallIDList();
            }

            else if (Lexie.MatchTokens[count] == "closeCurlyParent")
            {

            }

            else
            {
                Console.WriteLine("::ERROR:: Expected MatchTokens: intt, floatt, or chart. Got:" + Lexie.MatchTokens[count]);
                Environment.Exit(1);

            }
        }

        static void CallIDList()
        {
            if (Lexie.MatchTokens[count] == "idt")
            {
                count++;
                CallIDTail();
                if (Lexie.MatchTokens[count] == "semit")
                {
                    count++;
                    CallDecl();
                }

                else
                {
                    Console.WriteLine("::ERROR:: Expected MatchTokens: semit. Got: " + Lexie.MatchTokens[count]);
                    Environment.Exit(1);

                }
            }

            else
            {
                Console.WriteLine("::ERROR:: Expected MatchTokens: idt. Got: " + Lexie.MatchTokens[count]);
                Environment.Exit(1);

            }
        }

        static void CallIDTail()
        {
            if (Lexie.MatchTokens[count] == "commat")
            {
                count++;
                if (Lexie.MatchTokens[count] == "idt")
                {
                    count++;
                    CallIDTail();
                }

                else
                {
                    Console.WriteLine("::ERROR:: Expected MatchTokens: idt. Got: " + Lexie.MatchTokens[count]);
                    Environment.Exit(1);

                }

            }

            else if (Lexie.MatchTokens[count] == "semit")
            {

            }

            else
            {
                Console.WriteLine("::ERROR:: Expected MatchTokens: semit. Got: " + Lexie.MatchTokens[count]);
                Environment.Exit(1);

            }
        }
    }
}
