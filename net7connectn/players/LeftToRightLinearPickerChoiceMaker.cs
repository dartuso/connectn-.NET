using Game;
using System.Diagnostics;

namespace Players
{
    /// <summary>
    /// Cycles through picking each line
    /// </summary>
    /// <remarks>
    /// @authorJKidney
    /// @version1
    /// 
    ///      Created: Oct 24, 2013
    /// Last Updated: Oct 24, 2013 - creation (jkidney)
    /// </remarks>
    public class LeftToRightLinearPickerChoiceMaker : ConnectNChoiceMaker
    {
        private int count = 0;
        /// <summary>
        ///  Just steps through picking each col each turn
        /// </summary>
        /// <remarks>@seeConnectNChoiceMaker#playTurn(ConnectNBoard, Player)</remarks>
        public virtual int PlayTurn(ConnectNBoard board, Player whichPlayerAmI, Player otherPlayer, int winLength)
        {
            int choice = count;
            count = (++count) % board.NumCols();
            return choice;
        }

        /// <summary>
        ///  Just steps through picking each col each turn
        /// </summary>
        /// <remarks>@seeConnectNChoiceMaker#playTurn(ConnectNBoard, Player)</remarks>
        public virtual void Reset()
        {
            count = 0;
        }

        /// <summary>
        ///  Just steps through picking each col each turn
        /// </summary>
        /// <remarks>@seeConnectNChoiceMaker#playTurn(ConnectNBoard, Player)</remarks>
        /// <summary>
        /// Gives the name used when displaying information about this specific choice maker
        /// </summary>
        /// <returns></returns>
        public virtual string GetName()
        {
            return "LR-LINEAR";
        }
    }
}