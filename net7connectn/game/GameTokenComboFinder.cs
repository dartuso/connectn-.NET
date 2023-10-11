using Java;
using Players;
using System.Diagnostics;

namespace Game
{
    public class GameTokenComboFinder
    {
        private List<GameTokenCombo> player1 = new List<GameTokenCombo>();
        private List<GameTokenCombo> player2 = new List<GameTokenCombo>();
        private ConnectNBoard board = null;
        private int comboLength;
        public GameTokenComboFinder(ConnectNBoard board, int comboLength)
        {
            this.board = board;
            this.comboLength = comboLength;
            player1 = GetAllCombosFromEachToken(Player.PLAYER_ONE);
            player2 = GetAllCombosFromEachToken(Player.PLAYER_TWO);
        }

        public virtual List<GameTokenCombo> GetPlayer1Combos()
        {
            return player1;
        }

        public virtual List<GameTokenCombo> GetPlayer2Combos()
        {
            return player2;
        }

        /// <summary>
        /// Checks to see if the given player has won the game. A win is determined by
        /// having at least one combo of length comboLength
        /// </summary>
        /// <param name="whichPlayer">the player to check the win for</param>
        /// <param name="comboLength">the length of a winning combo</param>
        /// <returns>true if the player has won false otherwise</returns>
        public virtual bool DidPlayerWin(Player whichPlayer, int comboLength)
        {
            bool result = false;
            List<GameTokenCombo> checkList = null;
            checkList = (whichPlayer == Player.PLAYER_ONE) ? player1 : player2;
            foreach (GameTokenCombo combo in checkList)
                if (combo.Count >= comboLength)
                {
                    result = true;
                    break;
                }

            return result;
        }

        /// <summary>
        /// Returns an array list that contains all possible combos for the given player
        /// </summary>
        /// <returns></returns>
        private List<GameTokenCombo> GetAllCombosFromEachToken(Player whichPlayer)
        {
            List<GameTokenCombo> combos = new List<GameTokenCombo>();
            List<GameToken> playerTokens = null;
            if (whichPlayer == Player.PLAYER_ONE)
                playerTokens = board.GetPlayer1Tokens();
            else
                playerTokens = board.GetPlayer2Tokens();
            foreach (GameToken current in playerTokens)
                combos.AddAll(CheckForPossibleCombosAt(current));
            return FilterCombos(combos);
        }

        /// <summary>
        /// Generates all the offset values to use when checking for possible combos
        /// </summary>
        /// <returns></returns>
        private int[][][] GenerateOffSets()
        {
            int[][][] offsets = new int[8, comboLength, 2]; // [# paths to check][# points to check][#row col offset]
            for (int i = 1; i <= comboLength; i++)
            {
                offsets[0][i - 1][0] = i; // strait above current position
                // strait above current position
                offsets[0][i - 1][1] = 0; // strait above current position
                // strait above current position
                offsets[1][i - 1][0] = (-1 * i); // strait below current position
                // strait below current position
                offsets[1][i - 1][1] = 0; // strait below current position
                // strait below current position
                offsets[2][i - 1][0] = 0; // strait to the right of the current position
                // strait to the right of the current position
                offsets[2][i - 1][1] = i; // strait to the right of the current position
                // strait to the right of the current position
                offsets[3][i - 1][0] = 0; // strait to the left of the current position
                // strait to the left of the current position
                offsets[3][i - 1][1] = (-1 * i); // strait to the left of the current position
                // strait to the left of the current position

                //Possible diagonal paths
                offsets[4][i - 1][0] = i;
                offsets[4][i - 1][1] = i;
                offsets[5][i - 1][0] = (-1 * i);
                offsets[5][i - 1][1] = (-1 * i);
                offsets[6][i - 1][0] = (-1 * i);
                offsets[6][i - 1][1] = i;
                offsets[7][i - 1][0] = i;
                offsets[7][i - 1][1] = (-1 * i);
            }

            return offsets;
        }

        /// <summary>
        /// checks for all possible combos from the given location
        /// </summary>
        /// <param name="location">the location to check</param>
        /// <returns>the list of found combos</returns>
        private List<GameTokenCombo> CheckForPossibleCombosAt(GameToken location)
        {
            List<GameTokenCombo> combos = new List<GameTokenCombo>();

            //offsets to check for combos
            int[][][] offsets = GenerateOffSets();
            foreach (int[][] offsetPath in offsets)
            {
                GameTokenCombo result = CheckPath(location, offsetPath);
                if (result != null)
                    combos.Add(result);
            }

            return combos;
        }

        /// <summary>
        /// checks for a combo along a given plat of offsets
        /// </summary>
        /// <param name="location">the location to check from</param>
        /// <param name="offsets">the set of offsets to check at</param>
        /// <returns>any combo that was found</returns>
        private GameTokenCombo CheckPath(GameToken location, int[][] offsets)
        {
            GameTokenCombo result = new GameTokenCombo();
            result.Add(location);
            foreach (int[] offset in offsets)
            {
                int nrow = location.GetRow() + offset[0];
                int ncol = location.GetCol() + offset[1];
                GameToken checkLoc = board.GetTokenAt(nrow, ncol);
                if (checkLoc != null)
                {

                    //make sure the token is the current players token
                    if (checkLoc.GetWhichPlayer() == location.GetWhichPlayer())
                    {
                        result.Add(checkLoc);
                    }
                    else

                    // Token from different player found so exit loop
                    {
                        break;
                    }
                }
                else

                    // no token at location so stop
                    break;
            }


            //if result list is only size one then return null, otherwise return the
            //combo found.
            if (result.Count == 1)
            {
                result.Clear();
                result = null;
            }

            return result;
        }

        /// <summary>
        /// filters out duplicate combos
        /// </summary>
        /// <param name="allCombos">al combos that were found</param>
        /// <returns>a new filtered list of combos</returns>
        private List<GameTokenCombo> FilterCombos(List<GameTokenCombo> allCombos)
        {
            List<GameTokenCombo> duplicateList = (List<GameTokenCombo>)allCombos.Clone();
            for (int i = 0; i < allCombos.Count - 1; i++)
            {
                GameTokenCombo c1 = allCombos[i];
                for (int j = i + 1; j < allCombos.Count; j++)
                {
                    GameTokenCombo c2 = allCombos[j];
                    if (c1.ContainsSubCombo(c2))
                        duplicateList.Remove(c2);
                }
            }

            allCombos.Clear();
            return duplicateList;
        }

        public virtual string ToString()
        {
            string nl = System.GetProperty("line.separator");
            string result = "";
            result = "All Combos" + nl + "{" + nl;
            result += nl + "\tPlayer 1:" + nl;
            foreach (GameTokenCombo combo in player1)
                result += "\t\t" + combo + nl;
            result += nl + "\tPlayer 2:" + nl;
            foreach (GameTokenCombo combo in player2)
                result += "\t\t" + combo + nl;
            result += nl + "}" + nl;
            return result;
        }
    }
}