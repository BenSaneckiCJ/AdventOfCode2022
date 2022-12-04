namespace day4;

public class UnitTest1
{
    [Fact]
    public void ShouldParseAssignmentIntoNumbers()
    {
        var input = "2-4";
        Assert.Equal(new[] {2,3,4}, ParseAssignment(input));
    }

    [Fact]
    public void ShouldBreakLineToAssignments(){
        var input="2-4,5-6";
        var result = SeparateAndParse(input);
        Assert.Equal(2, result.Count());
        Assert.Equal(new[] {2,3,4}, result.First());
        Assert.Equal(new[] {5,6}, result.Last());
    }

    [Fact]
    public void ShouldFindIfAssignmentsAreTotallyContained(){
        var input="2-5,3-4";
        var assignemnts = SeparateAndParse(input);
        Assert.True(AreTotallyContained(assignemnts));
    }

    [Fact]
    public void ShouldHaveTotalNumberOfContainments(){
        var file = File.ReadAllText("C:\\Repos\\advent\\day4\\input1.txt");
        var result = file.Split(Environment.NewLine).Select(SeparateAndParse).Count(AreTotallyContained);
        Assert.Equal(542,result);
    }

    [Theory]
    [InlineData("2-5,3-4", true)]
    [InlineData("2-5,6-7", false)]
    [InlineData("2-5,1-7", true)]
    [InlineData("5-7,7-9", true)]
    public void ShouldMeasureOverlap(string input, bool expected){
        var assignemnts = SeparateAndParse(input);
        Assert.Equal(expected, AreOverlapping(assignemnts));
    }

    [Fact]
    public void ShouldMeasureNumberOfOverlaps(){
        var file = File.ReadAllText("C:\\Repos\\advent\\day4\\input1.txt");
        var result = file.Split(Environment.NewLine).Select(SeparateAndParse).Count(AreOverlapping);
        Assert.Equal(900,result);
    }

    private bool AreOverlapping(IEnumerable<IEnumerable<int>> assignemnts)
    {
        var first = assignemnts.First();
        var second = assignemnts.Last();
        return first.Any(x => second.Contains(x)) || second.Any(x => first.Contains(x));
    }

    private bool AreTotallyContained(IEnumerable<IEnumerable<int>> assignemnts)
    {
        var first = assignemnts.First();
        var second = assignemnts.Last();
        return second.All(x => first.Contains(x)) || first.All(x => second.Contains(x));
    }

    private IEnumerable<IEnumerable<int>> SeparateAndParse(string input)
    {
        var assignments = input.Split(',');
        return assignments.Select(ParseAssignment);
    }

    private IEnumerable<int> ParseAssignment(string input)
    {
        var rangeEnds = input.Split('-').Select(int.Parse).ToArray();
        return Enumerable.Range(rangeEnds.First(), rangeEnds.Last() - rangeEnds.First() + 1);
    }
}