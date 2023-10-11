using Java;
using Players;
using System.Diagnostics;

namespace Game
{
    /// <summary>
    /// Represents the ConnectN game board
    /// </summary>
    /// <remarks>
    /// @authorJKidney
    /// @version1
    /// 
    ///      Created: Oct 24, 2013
    /// Last Updated: Oct 24, 2013 - creation (jkidney)
    /// </remarks>
    public class ConnectNBoard
    {
        /// <summary>
        /// used to indicate if debug info should be printed out to the console
        /// </summary>
        private static bool debug = false;
        /// <summary>
        /// all tokens placed by player 1
        /// </summary>
        private List<GameToken> player1Tokens = new List<GameToken>();
        /// <summary>
        /// all tokens placed by player 2
        /// </summary>
        private List<GameToken> player2Tokens = new List<GameToken>();
        /// <summary>
        /// 2d array to represent the entire game board
        /// The board is represented turned upside down, the bottom of the board is row zero and the top
        /// of the board t=is the last row. Game tokens grow in cols from the bottom to the top
        /// ( index zero to the number of rows. )
        /// </summary>
        private GameToken[][] board = null;
        /// <summary>
        /// array to keep track of top location to place game tokens
        /// </summary>
        private int[] top = null;
        /// <summary>
        /// the number of rows in the game board
        /// </summary>
        private int rows = 0;
        /// <summary>
        /// the number of columns in the game board
        /// </summary>
        private int cols = 0;
        /// <summary>
        /// Base constructor to set up the empty game board
        /// </summary>
        /// <param name="rows">The number of rows to have in the game board</param>
        /// <param name="cols">The number of columns to have in the game board</param>
        public ConnectNBoard(int rows, int cols)
        {
            Init(rows, cols);
        }

        /// <summary>
        /// Copy Constructor. Creates an exact copy of the given board
        /// </summary>
        /// <param name="board">the board to duplicate</param>
        public ConnectNBoard(ConnectNBoard c4board)
        {
            Init(c4board.rows, c4board.cols);
            System.Arraycopy(c4board.top, 0, top, 0, c4board.top.length);
            for (int i = 0; i < c4board.rows; i++)
            {
                for (int j = 0; j < c4board.cols; j++)
                {
                    if (c4board.board[i][j] != null)
                    {
                        board[i][j] = new GameToken(c4board.board[i][j]);
                        if (board[i][j].GetWhichPlayer() == Player.PLAYER_ONE)
                            player1Tokens.Add(board[i][j]);
                        else
                            player2Tokens.Add(board[i][j]);
                    }
                    else
                        board[i][j] = null;
                }
            }
        }

        /// <summary>
        /// Sets up the empty game board
        /// </summary>
        /// <param name="rows">The number of rows to have in the game board</param>
        /// <param name="cols">The number of columns to have in the game board</param>
        public virtual void Init(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            top = new int[cols];
            board = new GameToken[rows];
            for (int i = 0; i < rows; i++)
                board[i] = new GameToken[cols];
            for (int i = 0; i < cols; i++)
                top[i] = 0;
        }

        /// <summary>
        /// Determines if a specific col is full and no more game tokens can be placed
        /// </summary>
        /// <param name="col"></param>
        /// <returns>true if it is full false otherwise</returns>
        public virtual bool IsColFull(int col)
        {
            bool result = false;
            if (GetTop(col) == rows)
                result = true;
            return result;
        }

        /// <summary>
        /// Determines if all columns in the board are full
        /// </summary>
        /// <returns>true if all are full false otherwise</returns>
        public virtual bool AllColsFull()
        {
            bool result = true;
            for (int i = 0; i < cols; i++)
                if (!IsColFull(i))
                {
                    result = false;
                    break;
                }

            return result;
        }

        /// <summary>
        /// returns the top location of where the token is in the specified column
        /// </summary>
        /// <param name="col">the col to get the top for</param>
        /// <returns></returns>
        private int GetTop(int col)
        {
            return top[col];
        }

        private void IncTop(int col)
        {
            top[col]++;
        }

        public virtual int NumRows()
        {
            return rows;
        }

        public virtual int NumCols()
        {
            return cols;
        }

        public static void DebugOn()
        {
            debug = true;
        }

        public static void DebugOff()
        {
            debug = false;
        }

        public virtual List<GameToken> GetPlayer1Tokens()
        {
            return player1Tokens;
        }

        public virtual List<GameToken> GetPlayer2Tokens()
        {
            return player2Tokens;
        }

        /// <summary>
        /// returns the specific token at the given location
        /// </summary>
        /// <param name="row">row location of the token</param>
        /// <param name="col">column location of the token</param>
        /// <returns>the token if found, null if not found</returns>
        public virtual GameToken GetTokenAt(int row, int col)
        {
            GameToken result = null;

            // make sure value is within the game board
            if (((row >= 0) && (row < rows)) && ((col >= 0) && (col < cols)))
            {
                if (row < GetTop(col))
                    result = board[row][col];
            }

            return result;
        }

        /// <summary>
        /// Adds a game token to the board for the given user
        /// </summary>
        /// <param name="whichPlayer">which player is adding the game token</param>
        /// <param name="col">the col to add the token to</param>
        /// <returns>true of the token was added false if the col was full</returns>
        public virtual bool AddGameToken(Player whichPlayer, int col)
        {
            bool result = false;
            if (!IsColFull(col))
            {
                GameToken gt = new GameToken(whichPlayer, GetTop(col), col);
                board[GetTop(col)][col] = gt;
                IncTop(col);
                if (whichPlayer == Player.PLAYER_ONE)
                    player1Tokens.Add(gt);
                else
                    player2Tokens.Add(gt);
            }

            return result;
        }

        /// <summary>
        /// returns the current number of tokens that have been placed by player 1
        /// </summary>
        /// <returns>the number of tokens</returns>
        public virtual int NumMovesByPlayer1()
        {
            return player1Tokens.Count;
        }

        /// <summary>
        /// returns the current number of tokens that have been placed by player 2
        /// </summary>
        /// <returns>the number of tokens</returns>
        public virtual int NumMovesByPlayer2()
        {
            return player2Tokens.Count;
        }

        public virtual string ToString()
        {
            string nl = System.GetProperty("line.separator");
            string result = nl + "\tGame Board: " + nl;
            string space = "";
            string dash = "";
            if (debug)
            {
                result += "\tPlayer1: " + player1Tokens + nl;
                result += "\tPlayer2: " + player2Tokens + nl;
                result += "\t   Rows: " + rows + nl;
                result += "\t   Cols: " + cols + nl;
                result += "\t   Top: ";
                foreach (int t in top)
                    result += "[" + t + "]";
                result += nl + nl;
            }

            if (cols >= 10)
            {
                space = " ";
                dash = "_";
            }

            for (int i = rows - 1; i >= 0; i--)
            {
                result += "\t";
                for (int j = 0; j < cols; j++)
                {
                    if (board[i][j] != null)
                    {
                        string player = "";
                        switch (board[i][j].GetWhichPlayer())
                        {
                            case PLAYER_ONE:
                                player = "1";
                                break;
                            case PLAYER_TWO:
                                player = "2";
                                break;
                            case WINMARKER:
                                player = "W";
                                break;
                            default:
                                player = "E";
                                break;
                        }

                        result += "[" + space + player + "]";
                    }
                    else
                        result += "[" + space + " ]";
                }

                result += nl;
            }

            result += "\t";
            for (int j = 0; j < cols; j++)
                result += dash + "___";
            result += nl + "\t";
            for (int j = 1; j <= cols; j++)
            {
                if (j < 10)
                    result += "(" + space + j + ")";
                else
                    result += "(" + j + ")";
            }

            return result + nl;
        }
    }
}