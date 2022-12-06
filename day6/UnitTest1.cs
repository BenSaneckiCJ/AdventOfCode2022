namespace day6;

public class UnitTest1
{
    [Theory]
    [MemberData(nameof(TestExamples.Examples), MemberType = typeof(TestExamples))]
    public void ShouldGetFirstStringOfFourUniqueLetters(string input, int expected)
    {
        var result = GetFirstStringOfNUniqueLetters(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldWorkOnRealInput()
    {
        var input = File.ReadAllText("c:\\Repos\\advent\\day6\\input.txt");
        var result = GetFirstStringOfNUniqueLetters(input);
        Assert.Equal(1210, result);
    }

    [Theory]
    [MemberData(nameof(TestExamples.Part2), MemberType = typeof(TestExamples))]
    public void ShouldLookForStartOfMessage(string input, int expected)
    {
        var result = GetFirstStringOfNUniqueLetters(input, 14);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldLookForStartOfMessageWithRealInput()
    {
        var input = File.ReadAllText("c:\\Repos\\advent\\day6\\input.txt");
        var result = GetFirstStringOfNUniqueLetters(input, 14);
        Assert.Equal(3476, result);
    }

    private int GetFirstStringOfNUniqueLetters(string input, int numToSearch =4)
    {
        List<string> lastFour = new List<string>();
        foreach (var letter in input.Select((value, index) => new { value, index }))
        {
            lastFour.Add(letter.value.ToString());
            if (lastFour.Distinct().Count() == numToSearch)
                return letter.index + 1;
            else if (lastFour.Count >= numToSearch)
                lastFour.RemoveAt(0);
        }
        throw new Exception("No string found");
    }
}
public static class TestExamples
{
    public static IEnumerable<object[]> Examples()
    {
        yield return new object[] { "bvwbjplbgvbhsrlpgdmjqwftvncz", 5 };
        yield return new object[] { "nppdvjthqldpwncqszvftbrmjlhg", 6 };
        yield return new object[] { "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10 };
        yield return new object[] { "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11 };
    }

    public static IEnumerable<object[]> Part2()
    {
        yield return new object[] { "mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19 };
        yield return new object[] { "bvwbjplbgvbhsrlpgdmjqwftvncz", 23 };
        yield return new object[] { "nppdvjthqldpwncqszvftbrmjlhg", 23 };
        yield return new object[] { "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29 };
        yield return new object[] { "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26 };
    }
}
