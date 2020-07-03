using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace U._2
{
    class Program
    {
        static void Main(string[] args)
        {
            
            char terminate = ' ';
            int money = 500;
            Welcome();
            do
            {
                Console.Clear();
                string betChoice = BetChoice();
                int[] betOnNumbers = BetTypeCalculation(betChoice);
                Console.Clear();
                int betAmount = BetAmount(money, betChoice);
                Console.Clear();
                bool betMore = BetMore(money, betAmount);
                int winningNumber = WinningNumber();
                bool win = CheckForWin(betOnNumbers, winningNumber);
                money = MoneyCalcualtion(win, betMore, betAmount, money, betChoice);
                Console.Clear();
                Console.Write("Your current balance is: {0}", money);
                if (money < 0)
                {
                    ThreatenAndBeat();
                    return;
                }

                Console.Clear();
                Console.WriteLine("Do you want to play again? [y/n]");
                terminate = Console.ReadKey().KeyChar;

            } while (terminate != 'n');

           
            return;
        }

        static void Welcome()
        {
            Console.CursorVisible = false;
            Console.Write("Hello and welcome to the Malling Casino!\n" +
                "Press any key to continue");
            Console.ReadKey();
            Console.Clear();
            Console.Write("You have chosen to play roulette! Well done!\n" +
                "Press any key to continue");
            Console.ReadKey();
            Console.Clear();
            Console.CursorVisible = true;
        }

        static string BetChoice()
        {
            /*This Method finds out what kind if bet you want to make*/
            Console.CursorVisible = true;
            string betCategory = "";
            bool correctInput = false;
            string betChoice = "";
            do
            {
                Console.Clear();
                Console.WriteLine("Do you want to place an inside or an outside bet? Type \"explain\" if you do" +
                    " not know what these are");
                betCategory = Console.ReadLine();
                betCategory = betCategory.ToLower();
                if (betCategory == "inside")
                {
                    betChoice = InsideBet();
                    correctInput = true;
                }
                else if (betCategory == "outside")
                {
                    betChoice = OutsideBet();
                    correctInput = true;
                }
                else if (betCategory == "explain")
                {
                    Console.Clear();
                    Explain();
                    Console.ReadKey();
                }
                else
                {
                    Console.Clear();
                    Console.Write("Wrong! try writing either inside or outside");
                    Console.ReadKey();
                }
            } while (correctInput == false);


            
            return betChoice;
                
        }
        
        static void Explain()
        {
            //This method runs another method to get a description of what the different bets are
            Console.Clear();
            for (int i = 0; i < 16; i++)
            {
                
                string betExplain = BetTypes(i);
                Console.WriteLine(betExplain);
            }
        }

        static string BetTypes(int i)
        {
            //This is a list of bet types, their description and the odds
            Console.CursorVisible = false;
            string[] betTypes = new string[16];
            betTypes[0] = "Inside bets:";
            betTypes[1] = "Straight: Bet on a single number. 35 to 1";
            betTypes[2] = "Split: Bet on 2 adjacent numbers. 17 to 1";
            betTypes[3] = "Triple: Bet on a row. 11 to 1";
            betTypes[4] = "Square: Bet on a 2x2 square of numbers. 8 to 1";
            betTypes[5] = "Five: Bet on numbers; 0, 1, 2, 3, 4. 6 to 1";
            betTypes[6] = "Six: Bet on 2 adjacent rows ";
            betTypes[7] = "Outside bets:";
            betTypes[8] = "Red: Bet on that the number will be red. 1 to 1";
            betTypes[9] = "Black: Bet on that the number will be black. 1 to 1 ";
            betTypes[10] = "Even: Bet on that the number will be even. 1 to 1";
            betTypes[11] = "Odd: Bet on that the number will be odd. 1 to 1";
            betTypes[12] = "Low: Number will be between 1-18. 1 to 1";
            betTypes[13] = "High: Number will be between 19-36. 1 to 1";
            betTypes[14] = "Dozen: Bet on; 1-12, 13-24 or 25-36. 2 to 1";
            betTypes[15] = "Column: Bet on a column. 2 to 1";
            Console.CursorVisible = true;
            return betTypes[i];
        }

        static string QuickBetTypesInside(int i)
        {
            //A list of the names of inside bets
            string[] insideBetTypes = new string[6];
            insideBetTypes[0] = "Straight";
            insideBetTypes[1] = "Split";
            insideBetTypes[2] = "Triple";
            insideBetTypes[3] = "Square";
            insideBetTypes[4] = "Five";
            insideBetTypes[5] = "Six";
           
            return insideBetTypes[i]; 
        }

        static string QuickBetTypesOutside(int i)
        {
            //An array with the types of outside bets
            string[] outsideBetTypes = new string[8];
            outsideBetTypes[0] = "Red";
            outsideBetTypes[1] = "Black";
            outsideBetTypes[2] = "Even";
            outsideBetTypes[3] = "Odd";
            outsideBetTypes[4] = "Low";
            outsideBetTypes[5] = "High";
            outsideBetTypes[6] = "Dozen";
            outsideBetTypes[7] = "Column";
            return outsideBetTypes[i];
            
           
        }

        static int WinningNumber()
        {
            //This is a randomly generated number between 0-36
            Console.CursorVisible = false;
            Console.Clear();
            Console.Write("The croupier lets go of the ball...");
            Console.ReadKey();
            Random winningNumber = new Random();
            int winNumber = winningNumber.Next(37);
            Console.Clear();
            Console.Write("And the ball stops on {0}", winNumber);
            Console.ReadKey();
            Console.Clear();
            return winNumber;

        }

        static bool CheckForWin(int[] betOnNumbers, int winningNumber)
        {
            int i = 0;
            bool win = false;
            do
            {
                if (betOnNumbers[i] == winningNumber)
                {
                    win = true;

                }
                else
                {
                    win = false;
                }
                i++;
            } while (win != true && i < betOnNumbers.Length);

            return win;
        }

        static int MoneyCalcualtion(bool win, bool betMore, int betAmount, int money, string betChoice)
        {
            Console.CursorVisible = false;
            int cash = money;
            int lossAmount = 0;
            string odds = Odds(betChoice);
            string[] arrayOdds = odds.Split(' ');
            int oddsTimes = Convert.ToInt32(arrayOdds[0]);
            int backToHouse;
            int wonAmount = 0;
            if (win == true)
            {
                if(betMore == false)
                {
                    wonAmount = betAmount * oddsTimes;
                    cash = wonAmount + cash;
                    Console.WriteLine("Congratulations! you've won {0} Euro!", wonAmount);
                }
                else if (betMore == true)
                {
                    wonAmount = betAmount * oddsTimes;
                     backToHouse = wonAmount / 2;
                    cash = wonAmount + cash - backToHouse;
                    Console.WriteLine("Congratulations! you've won {0} Euro!", wonAmount);
                }
               

            }
            else if(win == false)
            {
                if(betMore == false)
                {
                    lossAmount = betAmount;
                    cash = cash - betAmount;
                    Console.WriteLine("Lady luck wasn't with you this time I'm afraid. You've lost {0} Euro.", lossAmount);
                }
                else if(betMore == true)
                {
                    lossAmount = betAmount;
                    cash = cash - betAmount;
                    Console.Write("You've gone and fucked up mate");
                }
                
            }
            Console.ReadKey();
            return cash;
        }

        static string InsideBet()
        {
            /*Lets you chose what kind of inside bet you want to make, I do that by bringing up a list of the
             * types of inside bets and then a different method to write its odds*/
            string choice = "";
            bool terminate = false;
            
            

            do
            {
                Console.Clear();
                Console.WriteLine("What kind of inside bet do you want to make? type \"explain\" if you want more information");
                for (int i = 0; i < 6; i++)
                {
                    Console.WriteLine("{0} -> {1} ", QuickBetTypesInside(i), i+1);
                }
                choice = Console.ReadLine();
                if (choice == "explain")
                {
                    Explain();
                    Console.ReadKey();
                }
                else 
                {
                    terminate = int.TryParse(choice, out int testInt); /*I found this method of checking if it's a number
                    online at https://stackoverflow.com/questions/894263/identify-if-a-string-is-a-number */

                    if (terminate == false)
                    {
                        Console.Clear();
                        Console.Write("Please input a valid response");
                        Console.ReadKey();
                    }
                    if ((terminate == true) && (Convert.ToInt32(choice) > 6))
                    {
                        Console.Clear();
                        Console.Write("Please input a valid response");
                        Console.ReadKey();
                        terminate = false;
                    }
                }
                

            } while (terminate != true);
                                 
            int numericInsideChoice = Convert.ToInt32(choice);
            return QuickBetTypesInside(numericInsideChoice-1);
        }
        
        static string OutsideBet()
        {
            //This lets you chose what outside bet you wanna do.
            string choice = "";
            bool terminate = false;
            int testInt;


            do
            {
                Console.Clear();
                Console.WriteLine("What kind of outside bet do you want to make? type \"explain\" if you want more information");
                for (int i = 0; i < 8; i++)
                {
                    Console.WriteLine("{0} -> {1}", QuickBetTypesOutside(i), i+1);
                }
                choice = Console.ReadLine();
                if (choice == "explain")
                {
                    Explain();
                    Console.ReadKey();
                }
                else
                {
                    terminate = int.TryParse(choice, out testInt); /*I found this method of checking if it's a number
                    online at https://stackoverflow.com/questions/894263/identify-if-a-string-is-a-number */
                    if (terminate == false)
                    {
                        Console.Clear();
                        Console.Write("Please input a valid response");
                        Console.ReadKey();
                    }
                    if ((terminate == true) && (Convert.ToInt32(choice) > 8))
                    {
                        Console.Clear();
                        Console.Write("Please input a valid response");
                        Console.ReadKey();
                        terminate = false;
                    }
                }


            } while (terminate != true);

            int numericOutsideChoice = Convert.ToInt32(choice);
            return QuickBetTypesOutside(numericOutsideChoice-1);
        }
        
        static int BetAmount(int money, string betChoice)
        {
            //Finds out how much you're gonna bet
            
            int betAmount = 0;
            char check = ' ';
            do
            {
                Console.Clear();
                Console.WriteLine("The odds for {0} are currently {1}", betChoice, Odds(betChoice));
                Console.WriteLine("How much do you want to bet? Your current balance is {0}", money);
                betAmount = Convert.ToInt32(Console.ReadLine());
                if (betAmount > money)
                {
                    Console.Clear();
                    Console.WriteLine("You can bet more than you have but there will be consequences if you lose and the house\n" +
                        "will keep half of what you earn if you win");
                    Console.ReadKey();
                }
                Console.Clear();
                Console.Write("You are about to bet {0} euro. Are you sure [y/n]", betAmount);
                check = Console.ReadKey().KeyChar;

            } while (check != 'y');

            return betAmount;

        }

        static string Odds(string betChoice)
        {
            //Odds for the different types of bets
            string checkOdds = betChoice.ToLower();
            string odds = "";

            if (checkOdds == "straight")
            {
                odds = "35 to 1";
            }
            else if (checkOdds == "split")
            {
                odds = "17 to 1";
            }
            else if(checkOdds == "triple")
            {
                odds = "11 to 1";
            }
            else if(checkOdds == "square")
            {
                odds = "8 to 1";
            }
            else if(checkOdds == "five")
            {
                odds = "6 to 1";
            }
            else if(checkOdds == "six")
            {
                odds = "6 to 1";
            }
            else if(checkOdds == "red")
            {
                odds = "1 to 1";
            }
            else if(checkOdds == "black")
            {
                odds = "1 to 1";
            }
            else if(checkOdds == "even")
            {
                odds = "1 to 1";
            }
            else if(checkOdds == "odd")
            {
                odds = "1 to 1";
            }
            else if(checkOdds == "low")
            {
                odds = "1 to 1";
            }
            else if(checkOdds == "high")
            {
                odds = "1 to 1";
            }
            else if(checkOdds == "dozen")
            {
                odds = "2 to 1";
            }
            else if(checkOdds == "column")
            {
                odds = "2 to 1";
            }
            return odds;
        }

        static bool BetMore(int money, int betAmount)
        {
            //This checks if the user has bet more money than he has.
            bool betMore = false;
            if (betAmount > money)
            {
                betMore = true;
            }
            return betMore;
        }

        static int[] BetTypeCalculation(string betChoice)
        {
            int[] betNumber = { 1 };
            string betCheck = betChoice.ToLower();
            if (betCheck[0] == 's')
            {
                if (betCheck == "straight")
                {
                    betNumber = Straight();
                }
                else if (betCheck == "split")
                {
                    betNumber = Split();
                }
                else if (betCheck == "six")
                {
                    betNumber = Six();
                }
                else if (betCheck == "square")
                {
                    betNumber = Square();
                }
            }
            else if (betCheck[0] == 't')
            {
                betNumber = Triple();
            }
            else if(betCheck[0] == 'f')
            {
                betNumber = Five();
            }
            else if(betCheck[0] == 'r')
            {
                betNumber = Red();
            }
            else if(betCheck[0] == 'b')
            {
                betNumber = Black();
            }
            else if(betCheck[0] == 'e')
            {
                betNumber = Even();
            }
            else if(betCheck[0] == 'o')
            {
                betNumber = Odd();
            }
            else if (betCheck[0] == 'l')
            {
                betNumber = Low();
            }
            else if (betCheck[0] == 'h')
            {
                betNumber = High();
            }
            else if (betCheck[0] == 'd')
            {
                betNumber = Dozen();
            }
            else if (betCheck[0] == 'c')
            {
                betNumber = Column();
            }
            return betNumber;
        }

        static int[] Straight()
        {
            bool terminate = false;
            int[] betNumber = new int[1];
            do
            {
                Console.Clear();
                //lets you chose the number/s you bet on
                
                Console.Write("Chose a number between 0 and 36 including the 2 extremes -> ");
                betNumber[0] = Convert.ToInt32(Console.ReadLine());
                if (betNumber[0] > -1 && betNumber[0] < 37)
                {
                    terminate = true;
                }
                else
                {
                    Console.Clear();
                    Console.Write("Please input a valid number");
                    Console.ReadKey();
                }
            } while (terminate != true);
            
            return betNumber;
        }

        static int[] Split()
        {
            Console.Clear();
            //lets you chose the number/s you bet on
            bool numberInDomain;
            bool terminate = false;
            int[] betNumber = new int[2];
            do
            {
                Console.Write("Chose a number between 0 and 36 including the 2 extremes -> ");
                betNumber[0] = Convert.ToInt32(Console.ReadLine());
                if (betNumber[0] < 0 || betNumber[0] > 36)
                {
                    Console.Clear();
                    Console.Write("Please input a valid number");
                    Console.ReadKey();
                    numberInDomain = false;
                }
                else
                {
                    numberInDomain = true;
                }
            } while (numberInDomain != true);
            
            
            if (betNumber[0] != 0 && betNumber[0] != 36)
            {
                do
                {
                    Console.Clear();
                    Console.Write("Chose either {0} or {1} -> ", betNumber[0] - 1, betNumber[0] + 1);
                    betNumber[1] = Convert.ToInt32(Console.ReadLine());
                    if (betNumber[1] == betNumber[0]+1 || betNumber[1] == betNumber[0]-1)
                    {
                        terminate = true;
                    }
                    else if(betNumber[1] != betNumber[0] - 1)
                    {
                        Console.CursorVisible = false;
                        Console.Clear();
                        Console.Write("You can only chose between the given numbers");
                        Console.ReadKey();
                        Console.CursorVisible = true;
                    }
                    else if(betNumber[1] != betNumber[0] + 1)
                    {
                        Console.CursorVisible = false;
                        Console.Clear();
                        Console.Write("You can only chose between the given numbers");
                        Console.ReadKey();
                        Console.CursorVisible = true;
                    }
                } while (terminate != true);
                
            }
            else if(betNumber[0] == 0 || betNumber[0] == 36)
            {
                if (betNumber[0] == 0)
                {
                    Console.Clear();
                    betNumber[1] = 1;
                    Console.Write("You have chosen {0} and, by the nature of the bet, {1}", betNumber[0], betNumber[1]);
                }
                else
                {
                    Console.Clear();
                    betNumber[1] = 35;
                    Console.Write("You have chosen {0} and, by the nature of the bet, {1}", betNumber[0], betNumber[1]);
                }
            }
            return betNumber;
        }

        static int[] Triple()
        {
            //lets you chose the number/s you bet on
            bool numberInDomain = false;
            int[] betNumber = new int[3];
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("Chose between:\n" +
                    "1 - 3 -> 1\n" +
                    "4 - 6 -> 2\n" +
                    "7 - 9 -> 3\n" +
                    "10 - 12 -> 4\n" +
                    "13 - 15 -> 5\n" +
                    "16 - 18 -> 6\n" +
                    "19 - 21 -> 7\n" +
                    "22 - 24 -> 8\n" +
                    "25 - 27 -> 9\n" +
                    "28 - 30 -> 10\n" +
                    "31 - 33 -> 11\n" +
                    "34 - 36 -> 12");

                choice = Convert.ToInt32(Console.ReadLine());
                if (choice < 1 || choice > 12)
                {
                    Console.Clear();
                    Console.Write("Please input a valid number");
                    Console.ReadKey();
                }
                else
                {
                    numberInDomain = true;
                }

            } while (numberInDomain != true);
            
            if (choice == 1)
            {
                betNumber[0] = 1;
                betNumber[1] = 2;
                betNumber[2] = 3; 
            }
            else if (choice == 2)
            {
                betNumber[0] = 4;
                betNumber[1] = 5;
                betNumber[2] = 6;
            }
            else if (choice == 3)
            {
                betNumber[0] = 7;
                betNumber[1] = 8;
                betNumber[2] = 9;
            }
            else if (choice == 4)
            {
                betNumber[0] = 10;
                betNumber[1] = 11;
                betNumber[2] = 12;
            }
            else if (choice == 5)
            {
                betNumber[0] = 13;
                betNumber[1] = 14;
                betNumber[2] = 15;
            }
            else if (choice == 6)
            {
                betNumber[0] = 16;
                betNumber[1] = 17;
                betNumber[2] = 18;
            }
            else if (choice == 7)
            {
                betNumber[0] = 19;
                betNumber[1] = 20;
                betNumber[2] = 21;
            }
            else if (choice == 8)
            {
                betNumber[0] = 22;
                betNumber[1] = 23;
                betNumber[2] = 24;
            }
            else if (choice == 9)
            {
                betNumber[0] = 25;
                betNumber[1] = 26;
                betNumber[2] = 27;
            }
            else if (choice == 10)
            {
                betNumber[0] = 28;
                betNumber[1] = 29;
                betNumber[2] = 30;
            }
            else if (choice == 11)
            {
                betNumber[0] = 31;
                betNumber[1] = 32;
                betNumber[2] = 33;
            }
            else if (choice == 12)
            {
                betNumber[0] = 34;
                betNumber[1] = 35;
                betNumber[2] = 36;
            }
            return betNumber;
        }

        static int[] Square()
        {
            //lets you chose the number/s you bet on
            int choice;
            bool numberInDomain = false;
            int[] betNumber = new int[4];
            do
            {
                Console.Clear();
                Console.WriteLine("Chose between:\n" +
                    "(1, 4, 2, 5) -> 1\n" +
                    "(2, 5, 3, 6) -> 2\n" +
                    "(4, 7, 5, 8) -> 3\n" +
                    "(5, 8, 6, 9) -> 4\n" +
                    "(7, 10, 8, 11) -> 5\n" +
                    "(8, 11, 9, 12) -> 6\n" +
                    "(10, 13, 11, 14) -> 7\n" +
                    "(11, 14, 12, 15) -> 8\n" +
                    "(13, 16, 14, 17) -> 9\n" +
                    "(14, 17, 15, 18) -> 10\n" +
                    "(16, 19, 17, 20) -> 11\n" +
                    "(17, 20, 18, 21) -> 12\n" +
                    "(19, 22, 20, 23) -> 13\n" +
                    "(20, 23, 21, 24) -> 14\n" +
                    "(22, 25, 23, 26) -> 15\n" +
                    "(23, 26, 24, 27) -> 16\n" +
                    "(25, 28, 26, 29) -> 17\n" +
                    "(26, 29, 27, 30) -> 18\n" +
                    "(28, 31, 29, 32) -> 19\n" +
                    "(29, 32, 30, 33) -> 20\n" +
                    "(31, 34, 32, 35) -> 21\n" +
                    "(32, 35, 33, 36) -> 22");


                choice = Convert.ToInt32(Console.ReadLine());
                if (choice < 1 || choice > 22)
                {
                    Console.Clear();
                    Console.Write("Please input a valid number");
                    Console.ReadKey();
                }
                else
                {
                    numberInDomain = true;
                }
            } while (numberInDomain != true);
            
            if (choice == 1)
            {
                betNumber[0] = 1;
                betNumber[1] = 4;
                betNumber[2] = 2;
                betNumber[3] = 5;
            }
            else if (choice == 2)
            {
                betNumber[0] = 2;
                betNumber[1] = 5;
                betNumber[2] = 3;
                betNumber[3] = 6;
            }
            else if (choice == 3)
            {
                betNumber[0] = 4;
                betNumber[1] = 7;
                betNumber[2] = 5;
                betNumber[3] = 8;
            }
            else if (choice == 4)
            {
                betNumber[0] = 5;
                betNumber[1] = 8;
                betNumber[2] = 6;
                betNumber[3] = 9;
            }
            else if (choice == 5)
            {
                betNumber[0] = 7;
                betNumber[1] = 10;
                betNumber[2] = 8;
                betNumber[3] = 11;
            }
            else if (choice == 6)
            {
                betNumber[0] = 8;
                betNumber[1] = 11;
                betNumber[2] = 9;
                betNumber[3] = 12;
            }
            else if (choice == 7)
            {
                betNumber[0] = 10;
                betNumber[1] = 13;
                betNumber[2] = 11;
                betNumber[3] = 14;
            }
            else if (choice == 8)
            {
                betNumber[0] = 11;
                betNumber[1] = 14;
                betNumber[2] = 12;
                betNumber[3] = 15;
            }
            else if (choice == 9)
            {
                betNumber[0] = 13;
                betNumber[1] = 16;
                betNumber[2] = 14;
                betNumber[3] = 17;
            }
            else if (choice == 10)
            {
                betNumber[0] = 14;
                betNumber[1] = 17;
                betNumber[2] = 15;
                betNumber[3] = 18;
            }
            else if (choice == 11)
            {
                betNumber[0] = 16;
                betNumber[1] = 19;
                betNumber[2] = 17;
                betNumber[3] = 20;
            }
            else if (choice == 12)
            {
                betNumber[0] = 17;
                betNumber[1] = 20;
                betNumber[2] = 18;
                betNumber[3] = 21;
            }
            else if (choice == 13)
            {
                betNumber[0] = 19;
                betNumber[1] = 22;
                betNumber[2] = 20;
                betNumber[3] = 23;
            }
            else if (choice == 14)
            {
                betNumber[0] = 20;
                betNumber[1] = 23;
                betNumber[2] = 21;
                betNumber[3] = 24;
            }
            else if (choice == 15)
            {
                betNumber[0] = 22;
                betNumber[1] = 25;
                betNumber[2] = 23;
                betNumber[3] = 26;
            }
            else if (choice == 16)
            {
                betNumber[0] = 23;
                betNumber[1] = 26;
                betNumber[2] = 24;
                betNumber[3] = 27;
            }
            else if (choice == 17)
            {
                betNumber[0] = 25;
                betNumber[1] = 28;
                betNumber[2] = 26;
                betNumber[3] = 29;

            }
            else if (choice == 18)
            {
                betNumber[0] = 26;
                betNumber[1] = 29;
                betNumber[2] = 27;
                betNumber[3] = 30;
            }
            else if (choice == 19)
            {
                betNumber[0] = 28;
                betNumber[1] = 31;
                betNumber[2] = 29;
                betNumber[3] = 32;
            }
            else if (choice == 20)
            {
                betNumber[0] = 29;
                betNumber[1] = 32;
                betNumber[2] = 30;
                betNumber[3] = 33;
            }
            else if (choice == 21)
            {
                betNumber[0] = 31;
                betNumber[1] = 34;
                betNumber[2] = 32;
                betNumber[3] = 35;
            }
            else if (choice == 22)
            {
                betNumber[0] = 32;
                betNumber[1] = 35;
                betNumber[2] = 33;
                betNumber[3] = 36;
            }

            return betNumber;
        }

        static int[] Five()
        {
            //Informs of your winning conditions
            Console.Clear();
            Console.WriteLine("If any of the numbers; 0, 1, 2, 3, 4 is the winning one, you win");
            Console.ReadKey();
            int[] betNumber = { 0, 1, 2, 3, 4 };
            return betNumber;
        }

        static int[] Six()
        {
            //Lets you chose the number/s you want to bet on
            int[] betNumber = new int[6];
            int numbers = 0;
            int counter = 0;
            int choice;
            bool numberInDomain = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Chose between the following combinations:\n" +
                    "numbers: 1 - 6 -> 1\n" +
                    "numbers: 4 - 9 -> 2\n" +
                    "numbers: 7 - 12 -> 3\n" +
                    "numbers: 10 - 15 -> 4\n" +
                    "numbers: 13 - 18 -> 5\n" +
                    "numbers: 16 - 21 -> 6\n" +
                    "numbers: 19 - 24 -> 7\n" +
                    "numbers: 22 - 27 -> 8\n" +
                    "numbers: 25 - 30 -> 9\n" +
                    "numbers: 28 - 33 -> 10\n" +
                    "numbers: 31 - 36-> 11");
                choice = Convert.ToInt32(Console.ReadLine());

                if (choice < 1 || choice > 11)
                {
                    Console.Clear();
                    Console.Write("Please input a valid number");
                    Console.ReadKey();
                }
                else
                {
                    numberInDomain = true;
                }
            
            } while (numberInDomain != true);
            

            if (choice == 1)
            {
                for(counter = 0; counter < 6; counter++)
                {
                    betNumber[counter] = numbers + 1;
                    numbers++;
                }
            }
            else if (choice == 2)
            {
                numbers = 3;
                for (counter = 0; counter < 6; counter++)
                {
                    betNumber[counter] = numbers + 1;
                    numbers++;
                }
            }
            else if (choice == 3)
            {
                numbers = 6;
                for (counter = 0; counter < 6; counter++)
                {
                    betNumber[counter] = numbers + 1;
                    numbers++;
                }
            }
            else if (choice == 4)
            {
                numbers = 9;
                for (counter = 0; counter < 6; counter++)
                {
                    betNumber[counter] = numbers + 1;
                    numbers++;
                }
            }
            else if (choice == 5)
            {
                numbers = 12;
                for (counter = 0; counter < 6; counter++)
                {
                    betNumber[counter] = numbers + 1;
                    numbers++;
                }
            }
            else if (choice == 6)
            {
                numbers = 15;
                for (counter = 0; counter < 6; counter++)
                {
                    betNumber[counter] = numbers + 1;
                    numbers++;
                }
            }
            else if (choice == 7)
            {
                numbers = 18;
                for (counter = 0; counter < 6; counter++)
                {
                    betNumber[counter] = numbers + 1;
                    numbers++;
                }
            }
            else if (choice == 8)
            {
                numbers = 21;
                for (counter = 0; counter < 6; counter++)
                {
                    betNumber[counter] = numbers + 1;
                    numbers++;
                }
            }
            else if (choice == 9)
            {
                numbers = 24;
                for (counter = 0; counter < 6; counter++)
                {
                    betNumber[counter] = numbers + 1;
                    numbers++;
                }
            }
            else if (choice == 10)
            {
                numbers = 27;
                for (counter = 0; counter < 6; counter++)
                {
                    betNumber[counter] = numbers + 1;
                    numbers++;
                }
            }
            else if (choice == 11)
            {
                numbers = 30;
                for (counter = 0; counter < 6; counter++)
                {
                    betNumber[counter] = numbers + 1;
                    numbers++;
                }
            }

            return betNumber;
        }

        static int[] Red()
        {
            //Informs you of your winning conditions
            Console.CursorVisible = false;
            int[] betNumber = { 1, 7, 16, 19, 25, 34, 5, 14, 23, 32, 3, 9, 12, 18, 21, 27, 30, 36};
            Console.Clear();
            Console.WriteLine("You have chosen to be on \"Red\" so if any of the following numbers is the winning one then you win!");
            for (int i = 0; i < betNumber.Length; i++)
            {
                Console.Write("{0}  ", betNumber[i]);
            }
            Console.ReadKey();
            Console.CursorVisible = true;
            return betNumber;
        }

        static int[] Black()
        {
           //Informs you of your winning conditions

           
            Console.Clear();
            Console.CursorVisible = false;
            int[] betNumber = {4, 10, 13, 22, 28, 31, 2, 8, 11, 17, 20, 26, 29, 35, 6, 15, 24, 33};
            Console.Clear();
            Console.WriteLine("You have chosen tp bet on \"black\" so if any of the following numbers is the winning one then you win!");
            for (int i = 0; i < betNumber.Length; i++)
            {
                Console.Write("{0}  ", betNumber[i]);
            }
            Console.ReadKey();
            Console.CursorVisible = true;
            return betNumber;
        }

        static int[] Even()
        {
            Console.CursorVisible = false;
            Console.Clear();
            int[] betNumber = new int[18];
            Console.WriteLine("You have chosen to bet on \"even\", that means that if any of the following numbers is the winning one then you win:");
            for(int i = 0; i < betNumber.Length; i++)
            {
                betNumber[i] = (i + 1) * 2;
                Console.Write("{0}  ", betNumber[i]);
            }
            Console.ReadKey();
            Console.CursorVisible = true;
            return betNumber;
        }

        static int[] Odd()
        {
            Console.Clear();
            Console.CursorVisible = false;
            int[] betNumber = new int[18];
            Console.WriteLine("You have chosen to bet on odd, that means that if any of the following numbers is the winning one then you win:");
            for (int i = 0; i < betNumber.Length; i++)
            {
                betNumber[i] = i * 2 + 1;

                Console.Write("{0}  ", betNumber[i]);
            }
            Console.ReadKey();
            Console.CursorVisible = true;
            return betNumber;
        }

        static int[] Low()
        {
            Console.CursorVisible = false;
            Console.Clear();
            Console.WriteLine("You have chosen to bet on \"low\". If any of the following numbers is the winning one, then you win!");
            int[] betNumber = new int[18];
            for (int i = 0; i < 18; i++)
            {
                betNumber[i] = i + 1;
                Console.Write("{0} ", betNumber[i]);
            }
            Console.ReadKey();
            Console.CursorVisible = true;
            return betNumber;
        }

        static int[] High()
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.WriteLine("You have chosen to bet on \"high\". If any of the following numbers is the winning one, then you win!");
            int[] betNumber = new int[18];
            for (int i = 0; i < 18; i++)
            {
                betNumber[i] = i + 19;
                Console.Write("{0} ", betNumber[i]);
            }
            Console.ReadKey();
            Console.CursorVisible = false;
            return betNumber;
        }

        static int[] Dozen()
        {
            int[] betNumber = new int[12];
            bool numberInDomain = false;
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("You have chosen to bet on \"dozen\". Chose between the following options:\n" +
                    "Numbers 1 - 12 -> 1\n" +
                    "Numbers 13 - 24 -> 2\n" +
                    "Numbers 25 - 36 -> 3");

                choice = Convert.ToInt32(Console.ReadLine());

                if (choice < 1 || choice > 3)
                {
                    Console.Clear();
                    Console.Write("Please input a valid number");
                    Console.ReadKey();
                }
                else
                {
                    numberInDomain = true;
                }

            } while (numberInDomain != true);
            
            
            if (choice == 1)
            {
                for (int i = 0; i < betNumber.Length; i++)
                {
                    betNumber[i] = i + 1;
                }
            }
            else if (choice == 2)
            {
                for (int i = 0; i < betNumber.Length; i++)
                {
                    betNumber[i] = i + 13;
                }
            }
            else if (choice == 3)
            {
                for (int i = 0; i < betNumber.Length; i++)
                {
                    betNumber[i] = i + 25;
                }
            }
            return betNumber;
        }

        static int[] Column()
        {
            int[] betNumber = new int[12];
            bool numberInDomain = false;
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("You have chosen to bet on \"column\". Chose which column you want to bet on:\n" +
                    "Column 1 - Numbers: 1, 4, 7, 10, 13, 16, 19, 22, 25, 28, 31, 34 -> 1\n" +
                    "Column 2 - Numbers: 2, 5, 8, 11, 14, 17, 20, 23, 26, 29, 32, 35 -> 2\n" +
                    "Column 3 - Numbers: 3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36 -> 3");
                choice = Convert.ToInt32(Console.ReadLine());

                if (choice < 1 || choice > 3)
                {
                    Console.Clear();
                    Console.Write("Please input a valid number");
                    Console.ReadKey();
                }
                else
                {
                    numberInDomain = true;
                }

            } while (numberInDomain != true);
            

            if(choice == 1)
            {
                int[] column1 = { 1, 4, 7, 10, 13, 16, 19, 22, 25, 28, 31, 34 };
                betNumber = column1;
                
            }
            else if (choice == 2)
            {
                int[] column2 = { 2, 5, 8, 11, 14, 17, 20, 23, 26, 29, 32, 35 };
                betNumber = column2;
            }
            else if (choice == 3)
            {
                int[] column3 = { 3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36 };
                betNumber = column3;
            }
            return betNumber;
        }

        static void ThreatenAndBeat()
        {
            Console.CursorVisible = false;
            Console.Clear();
            Console.WriteLine("Just after you lose the bet you go to the bathroom to splash some water in your head, just\n" +
                "to clear you mind, suddenly two men in suits grab you, one by each elbow, and drag you out a back door");

            Console.ReadKey();
            Console.Clear();
            Console.Write("You're standing in alley pressed hard up against the side of the building when one of the\n" +
                "men in suits leans closer, you can feel his breath on your face, and starts explaining how business\n" +
                "is handled here at the Malling Casino");
               
            Console.ReadKey();
            Console.Clear();
            Console.Write("After a severe case of violence the two men leave you broken and bleeding in the alley, but not\n" +
                "before going through your pockets and taking photos of all ID and familly numbers. Before they leave, one of\n" +
                "them whispers; You're gonna be back here with double then amount you owe us, and soon, if you're not we'll\n" +
                "be back");
               
            Console.ReadKey();
            return;


        }
        
    }
}
