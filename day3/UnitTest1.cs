namespace day3;

public class UnitTest1
{
    [Fact]
    public void RucksackHasTwoCompartmentsOfEqualSize()
    {
        var input = "abcd";
        var sack = new Rucksack(input);
        Assert.Equal(2, sack.Compartments.Count);
        Assert.Equal(sack.Compartments[0].Length, sack.Compartments[1].Length);
    }

    [Theory]
    [MemberData(nameof(TestExamples.LetterPriority), MemberType = typeof(TestExamples))]
    public void LetterHasPriority(int prio, char letter)
    {
        Assert.Equal(prio, GetLetterPriority(letter));
    }

    [Theory]
    [MemberData(nameof(TestExamples.ExampleRucksacks), MemberType = typeof(TestExamples))]
    public void SingleItemShouldBeInBothSacks(string input, char expected)
    {
        var sack = new Rucksack(input);
        Assert.Equal(expected, sack.GetSharedComponent());
    }

    [Fact]
    public void ListOfSacksShouldHaveTotal()
    {
        var total = SumPrios(File.ReadLines("c:\\Repos\\advent\\day3\\input.txt"));
        Assert.Equal(8243, total);
    }

    [Fact]
    public void SacksShouldBeGroupedIntoThrees(){
        var lines = File.ReadLines("c:\\Repos\\advent\\day3\\input.txt");
        var groups = GroupIntoThrees(lines);
        Assert.True(groups.All(g => g.Count() == 3));
    }

    [Fact]
    public void EachGroupShouldHaveCommonItem(){
        var lines = new Rucksack[] { new Rucksack("vJrwpWtwJgWrhcsFMMfFFhFp"), new Rucksack("jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL"), new Rucksack("PmmdzqPrVvPwwTWBwg") };
        var result = GetCommonItem(lines);
        Assert.Equal('r', result);
    }

    [Fact]
    public void CommonItemOfGroupsShouldHaveATotalPrio()
    {
        var lines = File.ReadLines("c:\\Repos\\advent\\day3\\input2.txt");
        var groups = GroupIntoThrees(lines);
        var result = groups.Sum(g => GetLetterPriority(GetCommonItem(g)));
        Assert.Equal(2631, result);
    }

    private char GetCommonItem(Rucksack[] lines)
    {
        //intersection of 3 lines
        var first = lines[0].Everything.Intersect(lines[1].Everything);
        return first.Intersect(lines[2].Everything).First();
    }

    private IEnumerable<Rucksack[]> GroupIntoThrees(IEnumerable<string> lines)
    {
        var sacks = lines.Select(l => new Rucksack(l));
        while(sacks.Any())
        {
            var group = sacks.Take(3).ToArray();
            yield return group;
            sacks = sacks.Skip(3);
        }
    }

    private int SumPrios(IEnumerable<string> readLines)
    {
        var total = 0;
        foreach (var line in readLines)
        {
            var sack = new Rucksack(line);
            total += GetLetterPriority(sack.GetSharedComponent());
        }
        return total;
    }

    public int GetLetterPriority(char letter)
    {
        //list of all lowercase letters and then uppercase letters
        var alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return alphabet.IndexOf(letter) + 1;
    }
}

public class TestExamples
{
    public static IEnumerable<object[]> LetterPriority()
    {
        yield return new object[] { 1, 'a' };
        yield return new object[] { 2, 'b' };
        yield return new object[] { 16, 'p' };
        yield return new object[] { 38, 'L' };
        yield return new object[] { 42, 'P' };
        yield return new object[] { 22, 'v' };
        yield return new object[] { 20, 't' };
    }

    public static IEnumerable<object[]> ExampleRucksacks()
    {
        yield return new object[] { "vJrwpWtwJgWrhcsFMMfFFhFp", 'p' };
        yield return new object[] { "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", 'L' };
        yield return new object[] { "PmmdzqPrVvPwwTWBwg", 'P' };
        yield return new object[] { "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn", 'v' };
        yield return new object[] { "ttgJtRGJQctTZtZT", 't' };
        yield return new object[] { "CrZsJsPPZsGzwwsLwLmpwMDw", 's' };
    }
}