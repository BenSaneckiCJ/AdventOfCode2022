namespace day3
{
    internal class Rucksack
    {
        public Rucksack(string input)
        {
            Everything = input.ToCharArray();
            var splitAt = input.Length / 2;
            var firstHalf = input.Substring(0, splitAt).ToCharArray();
            var secondHalf = input.Substring(splitAt).ToCharArray();
            Compartments = new List<char[]> { firstHalf, secondHalf };
        }

        public List<char[]> Compartments {get;set;}

        public char[] Everything {get;set;}
        internal char GetSharedComponent()
        {
            var firstHalf = Compartments[0];
            var secondHalf = Compartments[1];
            var sharedComponent = firstHalf.Intersect(secondHalf).First();
            return sharedComponent;
        }
    }
}