public static class Shape
{
    public static IShape Parse(string input)
    {
        switch (input)
        {
            case "A":
                return new Rock();
            case "B":
                return new Paper();
            case "C":
                return new Scissors();
            default:
                throw new ArgumentException("Invalid input");
        }
    }

    public static int GetBThrowPoints(IShape aThrow, string winLoseOrDraw){
        if(winLoseOrDraw == "X")
            return aThrow.Beats.Points;
        else if(winLoseOrDraw == "Y")
            return aThrow.Points;
        else
            return aThrow.BeatBy.Points;
    }
}

public interface IShape
{
    int Points { get; }
    IShape Beats { get; }
    IShape BeatBy { get; }
}

public class Rock :IShape
{
    public int Points => 1;
    public IShape BeatBy => new Paper();
    public IShape Beats => new Scissors();
}
public class Paper :IShape{
    public int Points => 2;
    public IShape BeatBy => new Scissors();
    public IShape Beats => new Rock();
}
public class Scissors :IShape{
    public int Points => 3;
    public IShape BeatBy => new Rock();
    public IShape Beats => new Paper();
}