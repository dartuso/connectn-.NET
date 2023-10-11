using System.Diagnostics;

namespace UserIO
{
    /// <summary>
    /// Represents a single menu option that can be printed and selected by the user
    /// </summary>
    /// <remarks>
    /// @author jkidney
    /// @version1.1
    /// Created on: March 11, 2013
    /// Last Modified: Sept 24, 2013 - JKidney added getOption method
    /// </remarks>
    public class MenuOption
    {
        private string option;
        private string description;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="option">the option/character the user will select</param>
        /// <param name="description">the description for this option</param>
        public MenuOption(string option, string description)
        {
            this.option = option.ToLowerCase();
            this.description = description;
        }

        /// <summary>
        /// Determines if the given character matches the current menu option object
        /// </summary>
        /// <param name="usersChoice">the char given by the user</param>
        /// <returns>true for a match, false otherwise</returns>
        public virtual bool IsAMatch(string usersChoice)
        {
            return (option.CompareToIgnoreCase(usersChoice) == 0);
        }

        /// <summary>
        ///  Formated string for use when displaying all options for the menu
        /// </summary>
        public virtual string ToString()
        {
            return String.Format("%4s: %-20s", option, description);
        }

        public virtual string GetOption()
        {
            return option;
        }
    }
}