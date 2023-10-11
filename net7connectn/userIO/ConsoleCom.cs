using Java;
using System.Diagnostics;

namespace UserIO
{
    public class ConsoleCom
    {
        public static readonly char NO_CHAR_INPUT = ' '; //default for character input
        private Scanner input;
        /// <summary>
        /// default constructor
        /// </summary>
        public ConsoleCom()
        {
            input = new Scanner(System.@in);
        }

        /// <summary>
        /// helper method to contain printing of messages to the console
        /// </summary>
        /// <param name="message">the message to print</param>
        public virtual void Print(string message)
        {
            Console.Write(message);
        }

        /// <summary>
        /// helper method to contain printing of messages to the console with a new line
        /// </summary>
        /// <param name="message">the message to print</param>
        public virtual void Println(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Displays a message and waits to read a line of text from the user
        /// </summary>
        /// <param name="message">the message to display</param>
        /// <returns>the line inputed by the user</returns>
        public virtual string GetInputString(string message)
        {
            Print(message);
            return input.NextLine().Trim();
        }

        /// <summary>
        /// Displays a message and waits to read an integer from the user
        /// </summary>
        /// <param name="message">the message to display</param>
        /// <returns>the integer inputed by the user</returns>
        public virtual int GetInputInt(string message)
        {
            int userInput = 0;
            bool exit = true;
            Print(message);
            do
            {
                try
                {
                    userInput = input.NextInt();
                    input.NextLine();
                }
                catch (Exception ex)
                {
                    Print("Not a number, please enter again: ");
                    ClearInputLine();
                    exit = false;
                }
            }
            while (!exit);
            return userInput;
        }

        /// <summary>
        /// Displays a message and waits to read an integer from the user within the given range
        /// </summary>
        /// <param name="message">the message to display</param>
        /// <param name="low">the low end of the range (inclusive)</param>
        /// <param name="high">the high end of the range (inclusive)</param>
        /// <returns>the integer inputed by the user ( and verified to be within range )</returns>
        public virtual int GetInputInRangeInt(string message, int low, int high)
        {
            int userInput = 0;
            bool exit = false;
            do
            {
                userInput = GetInputInt(message);
                if (userInput < low || userInput > high)
                    Print("Not within proper range (" + low + "-" + high + "), please enter again: ");
                else
                    exit = true;
            }
            while (!exit);
            return userInput;
        }

        /// <summary>
        /// clears one line from the input console
        /// </summary>
        public virtual void ClearInputLine()
        {
            input.NextLine();
        }

        /// <summary>
        /// reads one character from the input
        /// </summary>
        /// <param name="message">the message to display</param>
        /// <returns>the entered character or NO_CHAR_INPUT if no input</returns>
        public virtual char GetInputChar(string message)
        {
            char result = NO_CHAR_INPUT;
            Print(message);
            string inputLine = input.NextLine().Trim().ToLowerCase();
            if (inputLine.Length() > 0)
                result = inputLine.CharAt(0);
            return result;
        }

        /// <summary>
        ///  reads one character from the input and validates that it is a character in validChars
        /// </summary>
        /// <param name="message">the message to display</param>
        /// <param name="validChars">a string that contains all valid chars for input by the user</param>
        /// <returns>the entered character validated</returns>
        public virtual char GetInputCharValidate(string message, string validChars)
        {
            char userInput = NO_CHAR_INPUT;
            bool exit = false;
            string valid = validChars.ToUpperCase();
            do
            {
                userInput = Character.ToUpperCase(GetInputChar(message));
                if (valid.IndexOf(userInput) == -1)
                    Println("Invalid choice ( must be one of " + validChars + "), please enter again: ");
                else
                    exit = true;
            }
            while (!exit);
            return userInput;
        }

        /// <summary>
        /// Asks the user a yes no answer and returns the result
        /// </summary>
        /// <param name="message">the message to display to the user ( y/n) will be tacked on</param>
        /// <returns>true if the user entered 'y' false otherwise</returns>
        public virtual bool GetInputYesNo(string message)
        {
            bool result = false;
            char input = Character.ToUpperCase(GetInputChar(message + " (y,n)"));
            if (input == 'Y')
                result = true;
            return result;
        }

        /// <summary>
        /// pauses until the user hits enter to continue
        /// </summary>
        public virtual void PauseUntilHitEnter()
        {
            GetInputString("<hit enter to continue>");
        }
    }
}