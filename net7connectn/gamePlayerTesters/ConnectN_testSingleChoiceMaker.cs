using Java.Util;
using DanielArtuso_Player;
using Game;
using Players;
using UserIO;
using System.Diagnostics;

namespace GamePlayerTesters
{
    /// <summary>
    /// Runs tests on a single choice maker
    /// </summary>
    /// <remarks>
    /// @authorJKidney
    /// @version1
    /// 
    ///      Created: Oct 24, 2013
    /// Last Updated: Oct 24, 2013 - creation (jkidney)
    /// </remarks>
    public class ConnectN_testSingleChoiceMaker
    {
        private ConnectNChoiceMaker choiceMaker = null;
        public ConnectN_testSingleChoiceMaker(ConnectNChoiceMaker maker)
        {
            choiceMaker = maker;
        }

        /// <summary>
        /// Runs a single test to find the time it took the AI to complete a choice
        /// </summary>
        /// <param name="nRows">the number of rows for the board</param>
        /// <param name="nCols">the number of cols for the board</param>
        /// <param name="winLength">the win length required</param>
        public virtual void RunSingleChoiceTimeTest(int nRows, int nCols, int winLength)
        {
            try
            {
                Random rng = new Random();
                ConnectNBoard board = new ConnectNBoard(nRows, nCols);
                choiceMaker.Reset();
                for (int count = 0; count < 8; count++)
                {
                    if ((count % 2) == 0)
                        board.AddGameToken(Player.PLAYER_ONE, rng.NextInt(nCols));
                    else
                        board.AddGameToken(Player.PLAYER_TWO, rng.NextInt(nCols));
                }

                long start = System.CurrentTimeMillis();
                choiceMaker.PlayTurn(board, Player.PLAYER_ONE, Player.PLAYER_TWO, winLength);
                long end = System.CurrentTimeMillis();
                long time = end - start;
                Console.WriteLine(String.Format("\tTime: R=%2d C=%2d W=%2d Time=" + time, nRows, nCols, winLength));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test Fail");
                ex.PrintStackTrace();
            }
        }

        /// <summary>
        /// Runs a single game test where the tested choice maker is player 1 against the given choice maker
        /// </summary>
        /// <param name="nRows">the number of rows for the board</param>
        /// <param name="nCols">the number of cols for the board</param>
        /// <param name="vs">that versus choice maker</param>
        /// <param name="winLength">the win length required</param>
        public virtual void RunsingleGameTest_Player1(int nRows, int nCols, int winLength, ConnectNChoiceMaker vs)
        {
            try
            {
                choiceMaker.Reset();
                ConnectNChoiceMaker player1 = choiceMaker;
                ConnectNChoiceMaker player2 = vs;
                ConnectNGame game = new ConnectNGame(player1, player2, nRows, nCols);
                game.SetWinLength(winLength);
                game.SetDelay(100);
                long start = System.CurrentTimeMillis();
                Player winner = game.PlayGame();
                long end = System.CurrentTimeMillis();
                long time = end - start;
                Console.WriteLine("WINNER: " + winner);
                Console.WriteLine(String.Format("\tTime: R=%2d C=%2d W=%2d Time=" + time, nRows, nCols, winLength));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test Fail");
                ex.PrintStackTrace();
            }
        }

        public static void Main(String[] args)
        {
            ConnectN_testSingleChoiceMaker tester = new ConnectN_testSingleChoiceMaker(new AIChoiceMaker());
            ConsoleCom com = new ConsoleCom();
            Console.WriteLine("Testing Time ( cols 6  to 20 )");
            tester.RunSingleChoiceTimeTest(9, 6, 5);
            tester.RunSingleChoiceTimeTest(9, 10, 5);
            tester.RunSingleChoiceTimeTest(9, 15, 5);
            tester.RunSingleChoiceTimeTest(9, 20, 5);
            com.PauseUntilHitEnter();
            int[][] testGames = new[]
            {
                new[]
                {
                    6,
                    7,
                    5
                },
                new[]
                {
                    3,
                    4,
                    3
                },
                new[]
                {
                    8,
                    3,
                    4
                },
                new[]
                {
                    20,
                    5,
                    4
                },
                new[]
                {
                    8,
                    8,
                    6
                },
                new[]
                {
                    15,
                    15,
                    4
                }
            };
            for (int test = 0; test < testGames.length; test++)
            {
                Console.WriteLine("=======================================================================");
                Console.WriteLine(String.Format("Next Test: R=%2d C=%2d W=%2d", testGames[test][0], testGames[test][1], testGames[test][2]));
                com.PauseUntilHitEnter();
                tester.RunsingleGameTest_Player1(testGames[test][0], testGames[test][1], testGames[test][2], new LeftToRightLinearPickerChoiceMaker());
            }
        }
    }
}