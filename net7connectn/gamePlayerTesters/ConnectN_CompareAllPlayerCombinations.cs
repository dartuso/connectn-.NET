using DanielArtuso_Player;
using Players;
using System.Diagnostics;

namespace GamePlayerTesters
{
    public class ConnectN_CompareAllPlayerCombinations
    {
        public static void Main(String[] args)
        {
            ConnectN_CompareAll tester = new ConnectN_CompareAll();
            tester.SetNumGamesPerTest(10);
            tester.SetNumBoardRows(6);
            tester.SetNumBoardCols(7);
            tester.SetWinLength(5);
            tester.SetAllowPlayAgainstSelf(true);
            tester.AddChoiceMaker(new LeftToRightLinearPickerChoiceMaker());
            tester.AddChoiceMaker(new RightToLeftLinearPickerChoiceMaker());
            tester.AddChoiceMaker(new RandomChoiceMaker());
            tester.AddChoiceMaker(new AIChoiceMaker());
            tester.RunAllTests();
        }
    }
}