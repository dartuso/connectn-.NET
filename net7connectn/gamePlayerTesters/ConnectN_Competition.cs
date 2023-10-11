using Players;
using System.Diagnostics;

namespace GamePlayerTesters
{
    public class ConnectN_Competition
    {
        public static void Main(String[] args)
        {
            ConnectN_CompareAll tester = new ConnectN_CompareAll();
            tester.SetNumGamesPerTest(10);
            tester.SetNumBoardRows(7);
            tester.SetNumBoardCols(8);
            tester.SetWinLength(5);
            tester.SetAllowPlayAgainstSelf(false);
            tester.AddChoiceMaker(new LeftToRightLinearPickerChoiceMaker());
            tester.AddChoiceMaker(new RightToLeftLinearPickerChoiceMaker());
            tester.AddChoiceMaker(new RandomChoiceMaker());
            tester.AddChoiceMaker(new AIChoiceMaker());
            tester.RunAllTests();
        }
    }
}