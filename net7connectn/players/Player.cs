using System.Diagnostics;

namespace Players
{
    /// <summary>
    /// Enumeration used to represent a marker for a player in the game
    /// </summary>
    /// <remarks>
    /// @authorJKidney
    /// @version1
    /// 
    ///      Created: Oct 24, 2013
    /// Last Updated: Oct 24, 2013 - creation (jkidney)
    /// </remarks>
    public enum Player
    {
        PLAYER_ONE,
        PLAYER_TWO,
        NEITHER // no one won
    ,
        WINMARKER // used to print the win combo
    ,
        ERROR //used to indicate that an error occurred from a player while playing the game 
    }
}