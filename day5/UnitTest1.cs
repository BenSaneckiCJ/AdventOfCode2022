namespace day5;

public class UnitTest1
{
    private readonly string input;
    public UnitTest1()
    {
        input = File.ReadAllText("C:\\Repos\\advent\\day5\\test.txt");
    }
    [Fact]
    public void ShouldBreakInputIntoInitialAndCommands()
    {
        var result = BreakIntoInitialAndCommands(input);
        Assert.NotEmpty(result.Item1);
        Assert.Equal(4, result.Item1.Count);
        Assert.NotEmpty(result.Item2);
        Assert.Equal(4, result.Item2.Count);
    }

    [Fact]
    public void ShouldGetNumberOfStacks()
    {
        var broken = BreakIntoInitialAndCommands(input);
        var result = GetStacks(broken.Item1);
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public void ShouldParseInitialConditionsIntoStacks()
    {
        var result = BreakIntoInitialAndCommands(input);
        var stacks = ParseInitialConditionsIntoStacks(result.Item1);
        Assert.Equal(3, stacks.Count());
        Assert.Equal(2, stacks[0].Count);
        Assert.Equal(3, stacks[1].Count);
        Assert.Equal(1, stacks[2].Count);
    }

    [Fact]
    public void ShouldParseMoveCommands()
    {
        var result = BreakIntoInitialAndCommands(input);
        var stacks = ParseInitialConditionsIntoStacks(result.Item1);

        var commands = ParseMoveCommands(result.Item2).ToList();
        Assert.Equal(1, commands[0].Item1);
        Assert.Equal(2, commands[0].Item2);
        Assert.Equal(1, commands[0].Item3);
        Assert.Equal(3, commands[1].Item1);
        Assert.Equal(1, commands[1].Item2);
        Assert.Equal(3, commands[1].Item3);
    }

    [Fact]
    public void ShouldExecuteMoveCommands()
    {
        var result = BreakIntoInitialAndCommands(input);
        var stacks = ParseInitialConditionsIntoStacks(result.Item1);

        var commands = ParseMoveCommands(result.Item2).ToList();
        ExecuteMoveCommands9000(commands, stacks);
        Assert.Equal(1, stacks[0].Count);
        Assert.Equal(1, stacks[1].Count);
        Assert.Equal(4, stacks[2].Count);
    }


    [Fact]
    public void ShouldRunAndReturnTopOfEachStack()
    {
        var result = BreakIntoInitialAndCommands(input);
        var stacks = ParseInitialConditionsIntoStacks(result.Item1);

        var commands = ParseMoveCommands(result.Item2).ToList();
        ExecuteMoveCommands9000(commands, stacks);
        var output = TopOfEachStack(stacks);
        Assert.Equal("CMZ", output);
    }

    [Fact]
    public void ShouldDoAllThisOnTheRealInput(){
        var input = File.ReadAllText("C:\\Repos\\advent\\day5\\input.txt");
        var result = BreakIntoInitialAndCommands(input);
        var stacks = ParseInitialConditionsIntoStacks(result.Item1);

        var commands = ParseMoveCommands(result.Item2).ToList();
        ExecuteMoveCommands9000(commands, stacks);
        var output = TopOfEachStack(stacks);
        Assert.Equal("BWNCQRMDB", output);
    }

    [Fact]
    public void ShouldExectureMoveCommands9001(){
        var result = BreakIntoInitialAndCommands(File.ReadAllText("C:\\Repos\\advent\\day5\\input.txt"));
        var stacks = ParseInitialConditionsIntoStacks(result.Item1);

        var commands = ParseMoveCommands(result.Item2).ToList();
        ExecuteMoveCommands9001(commands, stacks);
        var output = TopOfEachStack(stacks);
        Assert.Equal("CMZ", output);
    }

    private string TopOfEachStack(List<Stack<string>> stacks)
    {
        var output = "";
        foreach (var stack in stacks)
        {
            if (stack.Count > 0)
                output += stack.Peek();
        }
        return output;
    }

    private void ExecuteMoveCommands9000(List<Tuple<int, int, int>> commands, List<Stack<string>> stacks)
    {
        foreach (var command in commands)
        {
            var count = command.Item1;
            var from = command.Item2;
            var to = command.Item3;
            for (var i = 0; i < count; i++)
            {
                stacks[to - 1].Push(stacks[from - 1].Pop());
            }
        }
    }
    private void ExecuteMoveCommands9001(List<Tuple<int, int, int>> commands, List<Stack<string>> stacks)
    {
        foreach (var command in commands)
        {
            var count = command.Item1;
            var from = command.Item2;
            var to = command.Item3;
            
            var toPush = new Stack<string>();
            for (var i = 0; i < count; i++)
            {
                toPush.Push(stacks[from-1].Pop());
            }
            
            foreach (var item in toPush)
            {
                stacks[to - 1].Push(item);
            }
        }
    }

    private IEnumerable<Tuple<int, int, int>> ParseMoveCommands(List<string> item2)
    {
        foreach (var row in item2)
        {
            var split = row.Split(" ");
            var count = int.Parse(split[1]);
            var from = int.Parse(split[3]);
            var to = int.Parse(split[5]);
            yield return new Tuple<int, int, int>(count, from, to);
        }
    }

    private List<Stack<string>> ParseInitialConditionsIntoStacks(List<string> startingStacks)
    {
        var stacks = GetStacks(startingStacks).ToList();
        startingStacks.Reverse();
        foreach (var row in startingStacks.Skip(1))
        {
            for (var i = 0; i < stacks.Count(); i++)
            {
                var toPush = row.Skip(3*(i)+i).Take(3).ToArray()[1].ToString();
                if (!string.IsNullOrWhiteSpace(toPush))
                    stacks[i].Push(toPush);
            }
        }
        return stacks;
    }

    private static IEnumerable<Stack<string>> GetStacks(List<string> startingStacks)
    {
        var lastStack = startingStacks.Last().Split(" ");
        foreach (var stack in lastStack)
        {
            if (int.TryParse(stack, out var result))
                yield return new Stack<string>();
        }
    }

    private (List<string>, List<string>) BreakIntoInitialAndCommands(string input)
    {
        var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        var splitPoint = lines.ToList().IndexOf("");
        var initial = lines.Take(splitPoint).ToList();
        var commands = lines.Skip(splitPoint + 1).ToList();
        return (initial, commands);
    }
}
