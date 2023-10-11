using Players;
using Game;
using System.Diagnostics;

namespace DanielArtuso_Player
{
    public class AIChoiceMaker : ConnectNChoiceMaker
    {
        /// <summary>
        /// This method will return what column to drop the GameToken in for the game of
        /// ConnectN. The choice must be a valid column, otherwise the players turn is
        /// forfeit. The method is given a duplicate copy of the current game board.
        /// </summary>
        /// <param name="board">
        ///            copy of the current game board</param>
        /// <param name="whichPlayerAmI">
        ///            indicator of which player in the game you are</param>
        /// <param name="otherPlayer">
        ///            indicator of which player the other entity is in the game</param>
        /// <param name="winLength">
        ///            the win length for a combo</param>
        /// <returns>the column to place the players GameToken in</returns>
        public virtual int PlayTurn(ConnectNBoard board, Player whichPlayerAmI, Player otherPlayer, int winLength)
        {
            int depth = 2;
            int bestValue = Integer.MIN_VALUE;
            int col = 0;
            for (int i = 0; i < board.NumCols(); i++)
            {
                if (!board.IsColFull(i))
                {
                    ConnectNBoard minimaxBoard = new ConnectNBoard(board);
                    minimaxBoard.AddGameToken(whichPlayerAmI, i);

                    //				switch comments to used minimax
                    //				int v = minimax(minimaxBoard, whichPlayerAmI, otherPlayer, winLength, false,
                    //				 depth);
                    int v = Alphabeta(minimaxBoard, whichPlayerAmI, otherPlayer, winLength, false, depth, Integer.MIN_VALUE, Integer.MAX_VALUE);
                    bestValue = Math.Max(bestValue, v);
                    if (bestValue == v)
                    {
                        col = i;
                    }
                }
            }

            return col;
        }

        /// <summary>
        /// This method will return what column to drop the GameToken in for the game of
        /// ConnectN. The choice must be a valid column, otherwise the players turn is
        /// forfeit. The method is given a duplicate copy of the current game board.
        /// </summary>
        /// <param name="board">
        ///            copy of the current game board</param>
        /// <param name="whichPlayerAmI">
        ///            indicator of which player in the game you are</param>
        /// <param name="otherPlayer">
        ///            indicator of which player the other entity is in the game</param>
        /// <param name="winLength">
        ///            the win length for a combo</param>
        /// <returns>the column to place the players GameToken in</returns>
        //				switch comments to used minimax
        //				int v = minimax(minimaxBoard, whichPlayerAmI, otherPlayer, winLength, false,
        //				 depth);
        /* Alphabeta pruning algorithm currently in use with eval function*/
        public virtual int Alphabeta(ConnectNBoard board, Player me, Player other, int winLength, bool isMaximizingPlayer, int depth, int alpha, int beta)
        {
            GameTokenComboFinder finder = new GameTokenComboFinder(board, winLength);
            if (depth == 0 || finder.DidPlayerWin(me, winLength) || finder.DidPlayerWin(other, winLength) || finder.DidPlayerWin(Player.NEITHER, winLength))
            {
                return Eval(board, winLength, me, other);
            }

            if (isMaximizingPlayer)
            {
                int v = Integer.MIN_VALUE;
                for (int i = 0; i < board.NumCols(); i++)
                {
                    if (!board.IsColFull(i))
                    {
                        ConnectNBoard copyBoard = new ConnectNBoard(board);
                        copyBoard.AddGameToken(me, i);
                        v = Math.Max(v, Alphabeta(copyBoard, me, other, winLength, false, depth - 1, alpha, beta));
                        alpha = Math.Max(alpha, v);
                        if (beta <= alpha)
                        {
                            break;
                        }
                    }
                }

                return v;
            }
            else
            {
                int v = Integer.MAX_VALUE;
                for (int i = 0; i < board.NumCols(); i++)
                {
                    if (!board.IsColFull(i))
                    {
                        ConnectNBoard copyBoard = new ConnectNBoard(board);
                        copyBoard.AddGameToken(other, i);
                        v = Math.Min(v, Alphabeta(copyBoard, me, other, winLength, true, depth - 1, alpha, beta));
                        beta = Math.Min(beta, v);
                        if (beta <= alpha)
                        {
                            break;
                        }
                    }
                }

                return v;
            }
        }

        /// <summary>
        /// This method will return what column to drop the GameToken in for the game of
        /// ConnectN. The choice must be a valid column, otherwise the players turn is
        /// forfeit. The method is given a duplicate copy of the current game board.
        /// </summary>
        /// <param name="board">
        ///            copy of the current game board</param>
        /// <param name="whichPlayerAmI">
        ///            indicator of which player in the game you are</param>
        /// <param name="otherPlayer">
        ///            indicator of which player the other entity is in the game</param>
        /// <param name="winLength">
        ///            the win length for a combo</param>
        /// <returns>the column to place the players GameToken in</returns>
        //				switch comments to used minimax
        //				int v = minimax(minimaxBoard, whichPlayerAmI, otherPlayer, winLength, false,
        //				 depth);
        /* Alphabeta pruning algorithm currently in use with eval function*/
        /* minimax search algorithm, not in use but included for posterity */
        public virtual int Minimax(ConnectNBoard board, Player me, Player other, int winLength, bool isMaximizingPlayer, int depth)
        {
            GameTokenComboFinder finder = new GameTokenComboFinder(board, winLength);
            if (depth == 0 || finder.DidPlayerWin(me, winLength) || finder.DidPlayerWin(other, winLength) || finder.DidPlayerWin(Player.NEITHER, winLength))
            {
                return Eval(board, winLength, me, other);
            }

            if (isMaximizingPlayer)
            {
                int bestValue = Integer.MIN_VALUE;
                for (int i = 0; i < board.NumCols(); i++)
                {
                    if (!board.IsColFull(i))
                    {
                        ConnectNBoard copyBoard = new ConnectNBoard(board);
                        copyBoard.AddGameToken(me, i);
                        int v = Minimax(copyBoard, me, other, winLength, false, depth - 1);
                        bestValue = Math.Max(bestValue, v);
                    }
                }

                return bestValue;
            }
            else
            {
                int bestValue = Integer.MAX_VALUE;
                for (int i = 0; i < board.NumCols(); i++)
                {
                    if (!board.IsColFull(i))
                    {
                        ConnectNBoard copyBoard = new ConnectNBoard(board);
                        copyBoard.AddGameToken(other, i);
                        int v = Minimax(copyBoard, me, other, winLength, true, depth - 1);
                        bestValue = Math.Min(bestValue, v);
                    }
                }

                return bestValue;
            }
        }

        /// <summary>
        /// This method will return what column to drop the GameToken in for the game of
        /// ConnectN. The choice must be a valid column, otherwise the players turn is
        /// forfeit. The method is given a duplicate copy of the current game board.
        /// </summary>
        /// <param name="board">
        ///            copy of the current game board</param>
        /// <param name="whichPlayerAmI">
        ///            indicator of which player in the game you are</param>
        /// <param name="otherPlayer">
        ///            indicator of which player the other entity is in the game</param>
        /// <param name="winLength">
        ///            the win length for a combo</param>
        /// <returns>the column to place the players GameToken in</returns>
        //				switch comments to used minimax
        //				int v = minimax(minimaxBoard, whichPlayerAmI, otherPlayer, winLength, false,
        //				 depth);
        /* Alphabeta pruning algorithm currently in use with eval function*/
        /* minimax search algorithm, not in use but included for posterity */
        /* first iteration eval, currently in use */
        public virtual int Eval(ConnectNBoard board, int winLength, Player me, Player other)
        {
            GameTokenComboFinder finder = new GameTokenComboFinder(board, winLength);
            if (finder.DidPlayerWin(me, winLength))
            {
                return Integer.MAX_VALUE;
            }
            else if (finder.DidPlayerWin(other, winLength))
            {
                return Integer.MIN_VALUE;
            }


            //Get largest amounts of combos and return the largest amount - with positive or negative based if it is mine or not  
            if (me == Player.PLAYER_ONE)
            {
                if (finder.GetPlayer1Combos().Count > finder.GetPlayer2Combos().Count)
                {
                    return finder.GetPlayer1Combos().Count;
                }
                else
                {
                    return -finder.GetPlayer2Combos().Count;
                }
            }
            else
            {
                if (finder.GetPlayer1Combos().Count > finder.GetPlayer2Combos().Count)
                {
                    return -finder.GetPlayer1Combos().Count;
                }
                else
                {
                    return finder.GetPlayer2Combos().Count;
                }
            }
        }

        /// <summary>
        /// This method will return what column to drop the GameToken in for the game of
        /// ConnectN. The choice must be a valid column, otherwise the players turn is
        /// forfeit. The method is given a duplicate copy of the current game board.
        /// </summary>
        /// <param name="board">
        ///            copy of the current game board</param>
        /// <param name="whichPlayerAmI">
        ///            indicator of which player in the game you are</param>
        /// <param name="otherPlayer">
        ///            indicator of which player the other entity is in the game</param>
        /// <param name="winLength">
        ///            the win length for a combo</param>
        /// <returns>the column to place the players GameToken in</returns>
        //				switch comments to used minimax
        //				int v = minimax(minimaxBoard, whichPlayerAmI, otherPlayer, winLength, false,
        //				 depth);
        /* Alphabeta pruning algorithm currently in use with eval function*/
        /* minimax search algorithm, not in use but included for posterity */
        /* first iteration eval, currently in use */
        //Get largest amounts of combos and return the largest amount - with positive or negative based if it is mine or not  
        /* second iteration included for posterity */
        public virtual int UnusedEval(ConnectNBoard board, int winLength, Player me, Player other)
        {
            GameTokenComboFinder finder = new GameTokenComboFinder(board, winLength);
            if (finder.DidPlayerWin(me, winLength))
            {
                return Integer.MAX_VALUE;
            }

            if (finder.DidPlayerWin(other, winLength))
            {
                return Integer.MIN_VALUE;
            }

            int p1score = 0;
            int p2score = 0;
            foreach (GameTokenCombo combo in finder.GetPlayer1Combos())
            {
                p1score = (int)(p1score + (Math.Pow(10, combo.Count)));
            }

            foreach (GameTokenCombo combo in finder.GetPlayer2Combos())
            {
                p2score = p2score + (int)(Math.Pow(10, combo.Count));
            }

            if (me == Player.PLAYER_ONE)
            {
                return (p1score - p2score);
            }
            else
            {
                return (p2score - p1score);
            }
        }

        /// <summary>
        /// This method will return what column to drop the GameToken in for the game of
        /// ConnectN. The choice must be a valid column, otherwise the players turn is
        /// forfeit. The method is given a duplicate copy of the current game board.
        /// </summary>
        /// <param name="board">
        ///            copy of the current game board</param>
        /// <param name="whichPlayerAmI">
        ///            indicator of which player in the game you are</param>
        /// <param name="otherPlayer">
        ///            indicator of which player the other entity is in the game</param>
        /// <param name="winLength">
        ///            the win length for a combo</param>
        /// <returns>the column to place the players GameToken in</returns>
        //				switch comments to used minimax
        //				int v = minimax(minimaxBoard, whichPlayerAmI, otherPlayer, winLength, false,
        //				 depth);
        /* Alphabeta pruning algorithm currently in use with eval function*/
        /* minimax search algorithm, not in use but included for posterity */
        /* first iteration eval, currently in use */
        //Get largest amounts of combos and return the largest amount - with positive or negative based if it is mine or not  
        /* second iteration included for posterity */
        /// <summary>
        /// Called if multiple games are being played and values should be reset
        /// </summary>
        public virtual void Reset()
        {
        }

        /// <summary>
        /// This method will return what column to drop the GameToken in for the game of
        /// ConnectN. The choice must be a valid column, otherwise the players turn is
        /// forfeit. The method is given a duplicate copy of the current game board.
        /// </summary>
        /// <param name="board">
        ///            copy of the current game board</param>
        /// <param name="whichPlayerAmI">
        ///            indicator of which player in the game you are</param>
        /// <param name="otherPlayer">
        ///            indicator of which player the other entity is in the game</param>
        /// <param name="winLength">
        ///            the win length for a combo</param>
        /// <returns>the column to place the players GameToken in</returns>
        //				switch comments to used minimax
        //				int v = minimax(minimaxBoard, whichPlayerAmI, otherPlayer, winLength, false,
        //				 depth);
        /* Alphabeta pruning algorithm currently in use with eval function*/
        /* minimax search algorithm, not in use but included for posterity */
        /* first iteration eval, currently in use */
        //Get largest amounts of combos and return the largest amount - with positive or negative based if it is mine or not  
        /* second iteration included for posterity */
        /// <summary>
        /// Called if multiple games are being played and values should be reset
        /// </summary>
        /// <summary>
        /// Gives the name used when displaying information about this specific choice
        /// maker
        /// </summary>
        /// <returns></returns>
        public virtual string GetName()
        {
            return "Daniel Artuso";
        }
    }
}