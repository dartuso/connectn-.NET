using Players;
using DanielArtuso_Player;
using Game;
using System.Diagnostics;

namespace GameIndividualPlay
{
    /// <summary>
    /// Plays the connectN game of a user vs random chooser
    /// </summary>
    /// <remarks>@authorJKidney</remarks>
    public class ConnectN_UvA
    {
        /// <summary>
        /// </summary>
        /// <param name="args"></param>
        public static void Main(String[] args)
        {
            ConnectNChoiceMaker player1 = new ConsoleChoiceMaker();
            ConnectNChoiceMaker player2 = new AIChoiceMaker();
            ConnectNGame game = new ConnectNGame(player1, player2, 6, 7);
            game.SetWinLength(5);
            Player winner = game.PlayGame();
            Console.WriteLine("WINNER: " + winner);
        }
    }
}