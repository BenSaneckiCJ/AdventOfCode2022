public class Round
{
    public Round(string playerAThrow, string playerBThrow)
    {
        aThrow = playerAThrow;
        bThrow = playerBThrow;
    }
    public string aThrow { get; }
    public string bThrow { get; }
    public bool Draw => bThrow == "Y";

    public bool BWins => bThrow == "Z";

    public static void TallyPoints(Player playerA, Player playerB, Round round)
{
    if (round.Draw)
    {
        playerA.Score += 3;
        playerB.Score += 3;
    }
    else if (round.BWins)
    {
        playerB.Score += 6;
    }
    else
    {
        playerA.Score += 6;
    }
    var aThrow = Shape.Parse(round.aThrow);
    playerA.Score += aThrow.Points;
    playerB.Score += Shape.GetBThrowPoints(aThrow, round.bThrow);
}
}
