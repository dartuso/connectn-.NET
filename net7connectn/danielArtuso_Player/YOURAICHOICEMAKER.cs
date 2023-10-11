using Game;
using Players;

namespace yourname_Player;

public class YOURAICHOICEMAKER : ConnectNChoiceMaker
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="board"></param>
    /// <param name="whichPlayerAmI"></param>
    /// <param name="otherPlayer"></param>
    /// <param name="winLength"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public int PlayTurn(ConnectNBoard board, Player whichPlayerAmI, Player otherPlayer, int winLength)
    {
        throw new NotImplementedException();
    }
/// <summary>
///
/// </summary>
    public void Reset()
    {
        //You can add any logic in here
    }
/// <summary>
///
/// </summary>
/// <returns>
///
/// </returns>
    public string GetName()
    {
        //TODO: enter your team name
        return "team name!";
    }
}