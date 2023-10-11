using Players;
using System.Diagnostics;

namespace Game
{
    public class GameToken
    {
        private Player whichPlayer; // which player placed the token 
        private int row;
        private int col;
        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="p">the player that placed the game token</param>
        /// <param name="row">the row the token is located at</param>
        /// <param name="col">the col the token is located at</param>
        public GameToken(Player p, int row, int col)
        {
            whichPlayer = p;
            this.row = row;
            this.col = col;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="gt">the object to copy information from</param>
        public GameToken(GameToken gt)
        {
            if (gt != null)
            {
                whichPlayer = gt.whichPlayer;
                this.row = gt.row;
                this.col = gt.col;
            }
        }

        public virtual Player GetWhichPlayer()
        {
            return whichPlayer;
        }

        public virtual int GetRow()
        {
            return row;
        }

        public virtual int GetCol()
        {
            return col;
        }

        public virtual void SetWhichPlayer(Player whichPlayer)
        {
            this.whichPlayer = whichPlayer;
        }

        public virtual string ToString()
        {
            return "(row=" + row + ", col=" + col + ")";
        }

        /// <summary>
        /// Compares to see if the token matches anothe rgame token
        /// </summary>
        public virtual bool Equals(object obj)
        {
            bool result = false;
            if (obj is GameToken)
            {
                if (obj != null)
                {
                    GameToken comp = (GameToken)obj;
                    result = (comp.col == col) && (comp.row == row) && (comp.whichPlayer == whichPlayer);
                }
            }

            return result;
        }
    }
}