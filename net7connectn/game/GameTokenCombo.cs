using Java.Util;
using System.Diagnostics;

namespace Game
{
    /// <summary>
    /// Represents a single combo of connected game tokens
    /// </summary>
    /// <remarks>
    /// @authorJKidney
    /// @version1
    /// 
    ///      Created: Oct 24, 2013
    /// Last Updated: Oct 24, 2013 - creation (jkidney)
    /// </remarks>
    public class GameTokenCombo
    {
        private List<GameToken> combo = new List<GameToken>();
        public virtual void Add(GameToken token)
        {
            combo.Add(token);
        }

        public virtual int Size()
        {
            return combo.Count;
        }

        public virtual void Clear()
        {
            combo.Clear();
        }

        public virtual GameToken Get(int index)
        {
            return combo[index];
        }

        public virtual bool Contains(GameToken cont)
        {
            return combo.Contains(cont);
        }

        public virtual bool ContainsSubCombo(GameTokenCombo c2)
        {
            bool result = false;
            int matchCount = 0;
            if (Size() >= c2.Count)
            {
                for (int i = 0; i < Size(); i++)
                {
                    if (c2.Contains(Get(i)))
                        matchCount++;
                }

                if (matchCount == c2.Count)
                    result = true;
            }

            return result;
        }

        public virtual string ToString()
        {
            return "[combo=" + combo + "]";
        }
    }
}