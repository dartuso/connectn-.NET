using Players;
using DanielArtuso_Player;
using Game;
using System.Diagnostics;

namespace GameIndividualPlay
{
    /// <summary>
    /// Plays the connectN game of a AI vs AI
    /// </summary>
    /// <remarks>@authorJKidney</remarks>
    public class ConnectN_AvA
    {
        /// <summary>
        /// </summary>
        /// <param name="args"></param>
        public static void Main(String[] args)
        {
            ConnectNChoiceMaker player2 = new AIChoiceMaker();
            ConnectNChoiceMaker player1 = new AIChoiceMaker();
            ConnectNGame game = new ConnectNGame(player1, player2, 6, 7);
            game.SetWinLength(5);
            game.SetDelay(200);
            Player winner = game.PlayGame();
            Console.WriteLine("WINNER: " + winner);
        }
    }
}