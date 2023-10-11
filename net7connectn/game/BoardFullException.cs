using System.Diagnostics;

namespace Game
{
    public class BoardFullException : Exception
    {
        public BoardFullException(string arg0) : base(arg0)
        {
        }
    }
}