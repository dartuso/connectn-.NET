using Java.Util;
using System.Diagnostics;

namespace UserIO
{
    public class Menu
    {
        private ConsoleCom comm;
        private List<MenuOption> menuChoices;
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Menu(ConsoleCom comm)
        {
            this.comm = comm;
            menuChoices = new List<MenuOption>();
        }

        public Menu()
        {
            comm = new ConsoleCom();
            menuChoices = new List<MenuOption>();
        }

        /// <summary>
        /// Adds a menu option that the user can select. This method does not
        /// check for duplicate choices/options
        /// </summary>
        /// <param name="option">the option object to add to the possible choices</param>
        public virtual void AddMenuOption(MenuOption option)
        {
            menuChoices.Add(option);
        }

        /// <summary>
        /// Prints all the choices to the user and gets a valid choice from the user.
        /// </summary>
        /// <returns>the selected option (based upon the options that have been given to the user)</returns>
        public virtual MenuOption GetUserChoice()
        {
            string selection = "";
            MenuOption selectedOption = null;
            bool end = false;

            //keep asking for the choice until they enter to correct value
            do
            {
                comm.Println("Menu: ");

                //print out all options
                foreach (MenuOption option in menuChoices)
                    comm.Println(option.ToString());

                //get user choice
                selection = comm.GetInputString("Enter choice: ");
                selectedOption = IsValidChoice(selection);
                if (selectedOption == null)
                {
                    comm.Println("Error: invalid choice");
                }
                else
                    end = true;
            }
            while (!end);
            return selectedOption;
        }

        /// <summary>
        /// Determines if the given choice is valid based upon the current options in the menu
        /// </summary>
        /// <param name="choice">the choice made by the user</param>
        /// <returns>The selected menu option object if it is a valid choice, null otherwise</returns>
        private MenuOption IsValidChoice(string choice)
        {
            MenuOption matchFound = null;
            foreach (MenuOption option in menuChoices)
            {
                if (option.IsAMatch(choice))
                {
                    matchFound = option;
                    break;
                }
            }

            return matchFound;
        }

        public virtual void Clear()
        {
            menuChoices.Clear();
        }
    }
}