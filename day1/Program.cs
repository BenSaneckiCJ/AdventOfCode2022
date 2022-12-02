namespace advent
{
    internal class Program
    {
        // private static void Main(string[] args)
        // {
        //     var input = File.ReadAllText("input.txt");
        //     var result = Solve(input);
        //     Console.WriteLine(result);
        // }

        public static string ReadAllText(string inputFileName = "input.txt")
        {
            return File.ReadAllText($"c:\\Repos\\advent\\{inputFileName}");
        }

        public static int Solve(string input)
        {
            return GetHighestThree(input);
        }

        public static int GetHighestSum(string input)
        {
            List<Elf> group = GroupIntoElves(input);

            return group.Max(e => e.Sum);
        }

        public static int GetHighestThree(string input){
            List<Elf> group = GroupIntoElves(input);

            return group.OrderByDescending(e => e.Sum).Take(3).Sum(e => e.Sum);
        }

        private static List<Elf> GroupIntoElves(string input)
        {
            var split = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            var group = new List<Elf> { };
            group.Add(new Elf());
            foreach (var line in split)
            {
                if (int.TryParse(line, out var cal))
                {
                    group.Last().Calories.Add(cal);
                }
                else
                {
                    group.Add(new Elf());
                }
            }

            return group;
        }
    }

    public class Elf
    {
        public List<int> Calories { get; set; } = new List<int>();

        public int Sum => Calories?.Sum() ?? 0;
    }
}