using Game;
using Java.Util;
using System.Diagnostics;

namespace Players
{
    /// <summary>
    /// Gets a players choice from the console, it will confirm
    /// that it is a legal move.
    /// </summary>
    /// <remarks>
    /// @authorJKidney
    /// @version1
    /// 
    ///      Created: Oct 24, 2013
    /// Last Updated: Oct 24, 2013 - creation (jkidney)
    /// </remarks>
    public class ConsoleChoiceMaker : ConnectNChoiceMaker
    {
        private Scanner input = new Scanner(System.@in);
        /// <summary>
        /// Gets the players choice from the console
        /// </summary>
        /// <remarks>@seeConnectNChoiceMaker#playTurn(ConnectNBoard)</remarks>
        public virtual int PlayTurn(ConnectNBoard board, Player whichPlayerAmI, Player otherPlayer, int winLength)
        {
            int choice = -1;
            bool end = false;
            while (!end)
            {
                try
                {
                    Console.Write("Enter your choice (1 - " + board.NumCols() + "): ");
                    choice = input.NextInt() - 1;
                    if (choice < 0 || choice >= board.NumCols())
                    {
                        Console.WriteLine("ERROR: Incorrect input, choice out of range");
                    }
                    else
                    {
                        if (board.IsColFull(choice))
                            Console.WriteLine("ERROR: Incorrect input, choice destination is full");
                        else
                            end = true;
                    }
                }
                catch (InputMismatchException ime)
                {
                    Console.WriteLine("ERROR: Incorrect input");
                    input.Next();
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR: " + e.GetMessage());
                    end = true;
                }
            }

            return choice;
        }

        /// <summary>
        /// Gets the players choice from the console
        /// </summary>
        /// <remarks>@seeConnectNChoiceMaker#playTurn(ConnectNBoard)</remarks>
        public virtual void Reset()
        {
        }

        /// <summary>
        /// Gets the players choice from the console
        /// </summary>
        /// <remarks>@seeConnectNChoiceMaker#playTurn(ConnectNBoard)</remarks>
        /// <summary>
        /// Gives the name used when displaying information about this specific choice maker
        /// </summary>
        /// <returns></returns>
        public virtual string GetName()
        {
            return "CONSOLE";
        }
    }
}