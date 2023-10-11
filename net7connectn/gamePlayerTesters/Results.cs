using System.Diagnostics;

namespace GamePlayerTesters
{
    /// <summary>
    /// Used to keep track of game results from multiple game plays
    /// </summary>
    /// <remarks>
    /// @authorJKidney
    /// @version1
    /// 
    ///      Created: Oct 24, 2013
    /// Last Updated: Oct 24, 2013 - creation (jkidney)
    /// </remarks>
    public class Results : Comparable<Results>
    {
        private int wins = 0;
        private int loss = 0;
        private int tie = 0;
        private string name;
        public Results(string name)
        {
            this.name = name;
        }

        public virtual void IncWin()
        {
            wins++;
        }

        public virtual void IncLoss()
        {
            loss++;
        }

        public virtual void IncTie()
        {
            tie++;
        }

        public virtual string ToString()
        {
            return String.Format("results [wins=%3d, loss=%3d, tie=%3d] : %s", wins, loss, tie, name);
        }

        public virtual int CompareTo(Results other)
        {
            int result = 0;
            if (wins < other.wins)
                result = 1;
            else if (wins > other.wins)
                result = -1;
            else
            {
                if (tie < other.tie)
                    result = 1;
                else if (tie > other.tie)
                    result = -1;
                else
                {
                    if (loss > other.loss)
                        result = 1;
                    else if (loss < other.loss)
                        result = -1;
                }
            }

            return result;
        }
    }
}