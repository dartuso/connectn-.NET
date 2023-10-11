using Game;
using Java;
using Players;
using System.Diagnostics;

namespace GamePlayerTesters
{
    public class ConnectN_CompareAll
    {
        /// <summary>
        /// the number of games to run per test
        /// </summary>
        private int numGamesPerTest = 5;
        private int numBoardRows = 6;
        private int numBoardCols = 7;
        private int winLength = 5;
        private bool allowPlayAgainstSelf = true;
        private List<ConnectNChoiceMaker> choiceMakers = new List<ConnectNChoiceMaker>();
        private Hashtable<string, Results> OverAllWins = new Hashtable<string, Results>();
        /// <summary>
        /// Add a new Choice maker to be included when all tests are run
        /// </summary>
        /// <param name="player">the player to add</param>
        public virtual void AddChoiceMaker(ConnectNChoiceMaker player)
        {
            choiceMakers.Add(player);
            OverAllWins.Put(player.GetType().GetName(), new Results(player.GetName() + "(" + player.GetType().GetName() + ")"));
        }

        public virtual void SetNumGamesPerTest(int numGamesPerTest)
        {
            this.numGamesPerTest = numGamesPerTest;
        }

        public virtual void SetNumBoardRows(int numBoardRows)
        {
            this.numBoardRows = numBoardRows;
        }

        public virtual void SetNumBoardCols(int numBoardCols)
        {
            this.numBoardCols = numBoardCols;
        }

        public virtual void SetWinLength(int winLength)
        {
            this.winLength = winLength;
        }

        public virtual void SetAllowPlayAgainstSelf(bool allowPlayAgainstSelf)
        {
            this.allowPlayAgainstSelf = allowPlayAgainstSelf;
        }

        /// <summary>
        /// Prints the current players in the test to the console
        /// </summary>
        /// <returns>the longest name length</returns>
        private int PrintPlayers()
        {
            int length = Integer.MIN_VALUE;
            Console.WriteLine("Players: ");
            int count = 1;
            foreach (ConnectNChoiceMaker player1 in choiceMakers)
            {
                Console.WriteLine(String.Format("(%2d) " + player1.GetType().GetName(), count));
                length = Math.Max(player1.GetName().Length(), length);
                count++;
            }

            return length;
        }

        /// <summary>
        /// Prints out the final test results to the console
        /// </summary>
        private void PrintFinalTestResults()
        {
            Console.WriteLine("Final Overall Results: ");
            List<Results> allRes = new List<Results>();
            allRes.AddAll(OverAllWins.Values());
            Collections.Sort(allRes);
            foreach (Results res in allRes)
            {
                Console.WriteLine(res);
            }
        }

        /// <summary>
        /// runs test of all players against all other players
        /// </summary>
        public virtual void RunAllTests()
        {
            int length = PrintPlayers();
            string frmt = "\tTest: %" + length + "s vs %" + length + "s : ";
            Console.WriteLine("Num games per test: " + numGamesPerTest);
            foreach (ConnectNChoiceMaker player1 in choiceMakers)
            {
                foreach (ConnectNChoiceMaker player2 in choiceMakers)
                {
                    if (player1 == player2 && !allowPlayAgainstSelf)
                        continue;
                    Console.Write(String.Format(frmt, player1.GetName(), player2.GetName()));
                    RunTest(player1, player2);
                }
            }

            PrintFinalTestResults();
        }

        /// <summary>
        /// Runs an individual round of tests for the given combination of players
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        private void RunTest(ConnectNChoiceMaker player1, ConnectNChoiceMaker player2)
        {
            int player1Win = 0;
            int player2Win = 0;
            int tie = 0;
            int error = 0;
            string player1Name = player1.GetType().GetName();
            string player2Name = player2.GetType().GetName();
            string winnerName = "";
            for (int i = 1; i <= numGamesPerTest; i++)
            {
                player1.Reset();
                player2.Reset();
                ConnectNGame game = new ConnectNGame(player1, player2, numBoardRows, numBoardCols);
                game.ToggleDisplay();
                game.SetWinLength(winLength);
                Player winner = game.PlayGame();
                if (winner == Player.PLAYER_ONE)
                    player1Win++;
                else if (winner == Player.PLAYER_TWO)
                    player2Win++;
                else if (winner == Player.NEITHER)
                    tie++;
                else
                    error++;
            }

            if (player1Win > player2Win)
            {
                winnerName = player1Name;
                OverAllWins[player1Name].IncWin();
                OverAllWins[player2Name].IncLoss();
            }
            else if (player1Win < player2Win)
            {
                winnerName = player2Name;
                OverAllWins[player2Name].IncWin();
                OverAllWins[player1Name].IncLoss();
            }
            else
            {
                winnerName = "TIE";
                OverAllWins[player1Name].IncTie();
                OverAllWins[player2Name].IncTie();
            }

            Console.WriteLine(String.Format("Result: ( P1 Wins %3d, P2 Wins %3d, Ties %3d, Errors %3d) Winner = %s", player1Win, player2Win, tie, error, winnerName));
        }
    }
}