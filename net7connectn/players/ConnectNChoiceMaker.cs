using Game;
using System.Diagnostics;

namespace Players
{
    /// <summary>
    /// Interface to be implemented by any class That will be used to represent
    /// A player in the game of ConnectN
    /// </summary>
    /// <remarks>
    /// @authorJKidney
    /// @version1
    /// 
    ///      Created: Oct 24, 2013
    /// Last Updated: Oct 24, 2013 - creation (jkidney)
    /// </remarks>
    public interface ConnectNChoiceMaker
    {
        /// <summary>
        /// This method will return what column to drop the GameToken in
        /// for the game of ConnectN. The choice must be a valid column,
        /// otherwise the players turn is forfeit. The method is given
        /// a duplicate copy of the current game board.
        /// </summary>
        /// <param name="board">copy of the current game board</param>
        /// <param name="whichPlayerAmI">indicator of which player in the game you are</param>
        /// <param name="otherPlayer">indicator of which player the other entity is in the game</param>
        /// <param name="winLength">the win length for a combo</param>
        /// <returns>the column to place the players GameToken in</returns>
        int PlayTurn(ConnectNBoard board, Player whichPlayerAmI, Player otherPlayer, int winLength);
        /// <summary>
        /// Called if multiple games are being played and values should be reset
        /// </summary>
        void Reset();
        /// <summary>
        /// Gives the name used when displaying information about this specific choice maker
        /// </summary>
        /// <returns></returns>
        string GetName();
    }
}