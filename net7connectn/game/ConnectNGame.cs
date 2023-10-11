using Java.Util;
using Players;
using System.Diagnostics;

namespace Game
{
    public class ConnectNGame
    {
        private ConnectNChoiceMaker player1 = null;
        private ConnectNChoiceMaker player2 = null;
        private ConnectNBoard board = null;
        /// <summary>
        /// Delay in milliseconds between players choices, zero means no delay
        /// </summary>
        private int delay = 0;
        //** Used to control if anything is to be displayed to the screen or not 
        private bool display = true;
        /// <summary>
        /// The combo length needed to win
        /// </summary>
        private int winLength = 4;
        /// <summary>
        /// toggles the current state of the display field
        /// </summary>
        public virtual void ToggleDisplay()
        {
            display = !display;
        }

        // Debug control methods
        public static void DebugOff()
        {
            ConnectNBoard.DebugOff();
        }

        public static void DebugOn()
        {
            ConnectNBoard.DebugOn();
        }

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="player1">player one choice maker in the game</param>
        /// <param name="player2">player two choice maker in the game</param>
        /// <param name="rows">the number of rows to have in the game</param>
        /// <param name="cols">the number of columns to have in the board</param>
        public ConnectNGame(ConnectNChoiceMaker player1, ConnectNChoiceMaker player2, int rows, int cols)
        {
            this.player1 = player1;
            this.player2 = player2;
            board = new ConnectNBoard(rows, cols); // set up the basic connect game board size
        }

        /// <summary>
        /// Plays a single turn for the given player
        /// </summary>
        /// <param name="player">the choice make for the current player</param>
        /// <param name="marker">the marker to be used for the current player</param>
        /// <returns>true if the player has won the game false otherwise</returns>
        private bool PlayTurn(ConnectNChoiceMaker player, Player marker, Player otherPlayer)
        {
            bool result = false;
            ConnectNBoard copyBoard = new ConnectNBoard(board);
            if (board.AllColsFull())
                throw new BoardFullException("Board is full");
            Print("Turn: " + marker);
            Print("Win@: " + winLength);
            Print(board.ToString());
            int col = player.PlayTurn(copyBoard, marker, otherPlayer, winLength);
            Print("Choice: " + (col + 1));
            if (col >= 0 && col < board.NumCols())
            {
                if (!board.IsColFull(col))
                {
                    board.AddGameToken(marker, col);
                    GameTokenComboFinder finder = new GameTokenComboFinder(board, winLength);
                    if (finder.DidPlayerWin(marker, winLength))
                        result = true;
                }
                else
                    Print("Choice: '" + (col + 1) + "' is full");
            }
            else
                Print("Choice: '" + (col + 1) + "' is out of range");
            return result;
        }

        /// <summary>
        ///  Plays the full game of connect 4
        /// </summary>
        /// <returns>the player that won the game</returns>
        public virtual Player PlayGame()
        {
            bool end = false;
            bool win = false;
            Player winner = Player.NEITHER;
            try
            {
                while (!end)
                {
                    if (delay > 0)
                    {
                        try
                        {
                            Thread.Sleep(delay);
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    win = false;

                    //Player 1's turn
                    win = PlayTurn(player1, Player.PLAYER_ONE, Player.PLAYER_TWO);
                    if (!win)
                    {

                        //Player 2's turn
                        win = PlayTurn(player2, Player.PLAYER_TWO, Player.PLAYER_ONE);
                        if (win)
                        {
                            winner = Player.PLAYER_TWO;
                            break;
                        }
                    }
                    else
                    {
                        winner = Player.PLAYER_ONE;
                        break;
                    }
                }
            }
            catch (BoardFullException fullError)
            {
                Print("THE BOARD IS FULL");
                winner = Player.NEITHER;
            }
            catch (Exception e)
            {
                Print("ERROR: " + e.GetMessage());
                e.PrintStackTrace();
                winner = Player.ERROR;
            }

            Print("Final Result Original Board");
            Print(board.ToString());
            GameTokenComboFinder finder = new GameTokenComboFinder(board, winLength);
            List<GameTokenCombo> combos = null;
            int numPlace = -1;
            if (finder.DidPlayerWin(Player.PLAYER_ONE, winLength))
            {
                combos = finder.GetPlayer1Combos();
                numPlace = board.NumMovesByPlayer1();
            }
            else
            {
                combos = finder.GetPlayer2Combos();
                numPlace = board.NumMovesByPlayer1();
            }

            foreach (GameTokenCombo curr in combos)
                if (curr.Count >= winLength)
                {
                    MarkWinCombo(curr);
                }

            if (winner != Player.NEITHER && winner != Player.WINMARKER)
            {
                Print("Final Result Win Combo");
                Print(board.ToString());
                Print("Num tokens placed by winner: " + numPlace);
            }
            else
                Print("No Win Combo found");
            return winner;
        }

        private void MarkWinCombo(GameTokenCombo winCombo)
        {
            for (int i = 0; i < winCombo.Count; i++)
            {
                GameToken token = winCombo[i];
                token.SetWhichPlayer(Player.WINMARKER);
            }
        }

        /// <summary>
        /// helper method to control output from the game
        /// </summary>
        /// <param name="s">the string to output</param>
        private void Print(string s)
        {
            if (display)
                Console.WriteLine("ConnectN: " + s);
        }

        public virtual int GetDelay()
        {
            return delay;
        }

        public virtual void SetDelay(int delay)
        {
            this.delay = delay;
        }

        public virtual int GetWinLength()
        {
            return winLength;
        }

        public virtual void SetWinLength(int winLength)
        {
            this.winLength = winLength;
        }
    }
}