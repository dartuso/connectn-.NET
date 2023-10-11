using Game;
using Java.Util;
using System.Diagnostics;

namespace Players
{
    /// <summary>
    /// Picks a random col each time it is called.
    /// </summary>
    /// <remarks>
    /// @authorJKidney
    /// @version1
    /// 
    ///      Created: Oct 24, 2013
    /// Last Updated: Oct 24, 2013 - creation (jkidney)
    /// </remarks>
    public class RandomChoiceMaker : ConnectNChoiceMaker
    {
        private Random rng = new Random();
        /// <summary>
        /// Makes a random choice
        /// </summary>
        /// <remarks>@seeConnectNChoiceMaker#playTurn(ConnectNBoard, Player)</remarks>
        public virtual int PlayTurn(ConnectNBoard board, Player whichPlayerAmI, Player otherPlayer, int winLength)
        {
            int choice = rng.NextInt(board.NumCols());
            return choice;
        }

        /// <summary>
        /// Makes a random choice
        /// </summary>
        /// <remarks>@seeConnectNChoiceMaker#playTurn(ConnectNBoard, Player)</remarks>
        public virtual void Reset()
        {
            rng = new Random();
        }

        /// <summary>
        /// Makes a random choice
        /// </summary>
        /// <remarks>@seeConnectNChoiceMaker#playTurn(ConnectNBoard, Player)</remarks>
        /// <summary>
        /// Gives the name used when displaying information about this specific choice maker
        /// </summary>
        /// <returns></returns>
        public virtual string GetName()
        {
            return "RANDOM";
        }
    }
}