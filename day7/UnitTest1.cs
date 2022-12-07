namespace day7;

public class UnitTest1
{
    public string[] _testInput;
    public Program _program;
    public UnitTest1()
    {
        _testInput = File.ReadAllLines("../../../test.txt");
        _program = new Program();
    }

    [Fact]
    public void ParseFileIntoInput()
    {
        Assert.NotEmpty(_testInput);
    }

    [Fact]
    public void SetsDirectory()
    {

        _program.ParseLine("$ cd /");
        Assert.Equal("/", _program.currentTree.Peek());
    }

    [Fact]
    public void SetsTwoDirectories()
    {

        _program.ParseLine("$ cd /");
        _program.ParseLine("$ cd b");
        Assert.Equal("b", _program.currentTree.Peek());
        Assert.Equal(2, _program.currentTree.Count());
    }

    [Fact]
    public void AddsSizeToOneLevelOfTree()
    {
        _program.ParseLine("$ cd /");
        _program.ParseLine("10 abdsr");
        Assert.Equal(10, _program.directorySizes["/"]);
    }

    [Fact]
    public void AddsSizeToTwoLevelsOfTree(){
        _program.ParseLine("$ cd /");
        _program.ParseLine("10 abdsr");
        _program.ParseLine("$ cd b");
        _program.ParseLine("20 abdsr");
        Assert.Equal(30, _program.directorySizes["/"]);
        Assert.Equal(20, _program.directorySizes["b"]);
    }

    [Fact]
    public void TraversesBackUpTree(){
        _program.ParseLine("$ cd /");
        _program.ParseLine("10 abdsr");
        _program.ParseLine("$ cd b");
        _program.ParseLine("20 abdsr");
        _program.ParseLine("$ cd ..");
        _program.ParseLine("30 abdsr");
        Assert.Equal(60, _program.directorySizes["/"]);
        Assert.Equal(20, _program.directorySizes["b"]);
    }

    [Fact]
    public void ReturnsDirectoriesOver10000()
    {
        _program.ParseLine("$ cd /");
        _program.ParseLine("10 abdsr");
        _program.ParseLine("$ cd b");
        _program.ParseLine("20 abdsr");
        _program.ParseLine("$ cd ..");
        _program.ParseLine("30 abdsr");
        _program.ParseLine("$ cd c");
        _program.ParseLine("1000001 abdsr");
        _program.ParseLine("$ cd ..");
        _program.ParseLine("1000000 abdsr");
        Assert.Single(GetSmallDirectories());
        Assert.Equal(20, GetSmallDirectories().Sum(x => x.Value));
    }

    [Fact]
    public void SumOfBigDirectories()
    {
        _program.ReadAllLines("../../../test.txt");
        Assert.Equal(2, GetSmallDirectories().Count());
        Assert.Equal(94853, _program.directorySizes["a"]);
        Assert.Equal(584, _program.directorySizes["e"]);
        Assert.Equal(95437, GetSmallDirectories().Sum(x => x.Value));
    }

    [Fact]
    public void SumsOnRealFile(){
        _program.ReadAllLines("../../../input.txt");
        Assert.Equal(1598125, GetSmallDirectories().Sum(x => x.Value));
    }

    private IEnumerable<KeyValuePair<string, long>> GetSmallDirectories()
    {
        return _program.directorySizes.Where(x => x.Value <= 100000);
        
    }
}