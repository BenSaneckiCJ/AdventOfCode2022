namespace day2;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var input = "A";
        var score = Shape.Parse(input);
        Assert.IsType<Rock>(score);
    }

    [Fact]
    public void Test2()
    {
        var input = "B";
        var score = Shape.Parse(input);
        Assert.IsType<Paper>(score);
    }

    [Fact]
    public void Test3()
    {
        var input = "C";
        var score = Shape.Parse(input);
        Assert.IsType<Scissors>(score);
    }

    [Fact]
    public void GetBThrowPointsForWinningOverRock()
    {
        var aThrow = new Rock();
        var bThrow = "Z";
        var score = Shape.GetBThrowPoints(aThrow, bThrow);
        Assert.Equal(2, score);
    }

    [Fact]
    public void ThreePointsForLosingToRock()
    {
        var playerA = new Player();
        var playerB = new Player();

        var round = new Round("A", "X");
        Round.TallyPoints(playerA, playerB, round);
        Assert.Equal(7, playerA.Score);
        Assert.Equal(3, playerB.Score);
        Assert.False(round.Draw);
        Assert.False(round.BWins);
    }

    [Fact]
    public void ShouldBeDraw(){
        var playerA = new Player();
        var playerB = new Player();
        
        var round = new Round("A", "Y");
        Round.TallyPoints(playerA, playerB, round);
        Assert.True(round.Draw);
        Assert.Equal(4, playerA.Score);
        Assert.Equal(4, playerB.Score);
    }
}
