namespace advent;

public class UnitTest1
{
    private string _input = string.Empty;
    
[Fact]
public void ReadTestFileIntoInputString(){
    _input = Program.ReadAllText();
    Assert.True(_input.Length > 0);
}

    [Fact]
    public void Test1()
    {
        var input = "";
        var result = Program.Solve(input);
        Assert.Equal(0, result);
    }

    [Fact]
    public void ReturnsOneWhenInputIsOne()
    {
        var input = "1";
        var result = Program.Solve(input);
        Assert.Equal(1, result);
    }

    [Fact]
    public void ReturnsTwoWhenInputIsTwo()
    {
        var input = "2";
        var result = Program.Solve(input);
        Assert.Equal(2, result);
    }

    [Fact]
    public void ReturnsSumOfTwoNumbers()
    {
        var input = @"1
        2";
        var result = Program.Solve(input);
        Assert.Equal(3, result);
    }

    [Fact]
    public void ReturnsSumOfThreeNumbers()
    {
        var input = @"1
        2
        3";
        var result = Program.Solve(input);
        Assert.Equal(6, result);
    }

    [Fact]
    public void ReturnsSumOfThreeNumbersWithNewLineAtEnd()
    {
        var result = Program.Solve(Program.ReadAllText("onetwothreeline.txt"));
        Assert.Equal(6, result);
    }

    [Fact]
    public void ReturnsHighestBetweenGroups(){
        var input = Program.ReadAllText("onetwothreelinefour.txt");
        var result = Program.GetHighestSum(input);
        Assert.Equal(6, result);
    }

    [Fact]
    public void ReturnsHighestForTheInput(){
        var input = Program.ReadAllText();
        var result = Program.GetHighestSum(input);
        Assert.Equal(69912, result);
    }

    [Fact]
    public void ReturnsTotalForHighestThreeCombined(){
        var input = Program.ReadAllText();
        var result = Program.Solve(input);
        
        Assert.Equal(1, result);
    }
}