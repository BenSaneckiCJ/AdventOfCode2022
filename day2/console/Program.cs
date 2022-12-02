// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, Elves!");



// Read file input.txt into string
var input = File.ReadAllText("input.txt");

//parse into list of rounds
var rounds = input.Split(Environment.NewLine).Select(r => r.Split(" ")).Select(r => new Round(r[0], r[1])).ToList();

//create players
var playerA = new Player();
var playerB = new Player();

foreach (var round in rounds)
{
    Round.TallyPoints(playerA, playerB, round);
}
System.Console.WriteLine($"Player A: {playerA.Score}");
System.Console.WriteLine($"Player B: {playerB.Score}");
