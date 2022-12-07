using System.Diagnostics;

public class Program
{
    public Stack<string> currentTree = new Stack<string>();
    public Dictionary<string, long> directorySizes = new Dictionary<string, long>();

    public void ReadAllLines(string fileName = "../../../input.txt")
    {
        var input = File.ReadAllLines(fileName);
        foreach(var line in input)
        {
            ParseLine(line);
            Console.WriteLine(string.Join("/", currentTree));
        }
    }

    public void ParseLine(string line)
    {
        var lineContents = line.Split(" ");
        if (lineContents[0] == "$")
            GetCommand(lineContents);
        else if (long.TryParse(lineContents[0], out long size)){
            AddSizeToCurrentDirectory(size);
        }
    }

    public void AddSizeToCurrentDirectory(long size)
    {
        foreach(var directory in currentTree)
        {
            if (directorySizes.ContainsKey(directory))
            {
                directorySizes[directory] += size;
            }
            else
            {
                directorySizes.Add(directory, size);
            }
        }
    }

    public void GetCommand(string[] lineContents)
    {
        if(lineContents[1] == "cd")
        {
            SetCurrentDirectory(lineContents[2]);
        }
    }

    private void SetCurrentDirectory(string dir)
    {
        if (dir == "..")
        {
            currentTree.Pop();
        }
        else
        {
            currentTree.Push(dir);
        }
    }
}