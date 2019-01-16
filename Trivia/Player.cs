using System;

namespace UglyTrivia
{
    public class Player
    {
        public Player(string name)
        {
            Name = name;
        }
        public int Place { get; set; }
        public int Purse { get; set; }
        public string Name { get; set; }
        public bool InPenaltyBox { get; set; }

        public bool IsGettingOutPenalityBox { get; set; }

        public void MovePlayer(int roll)
        {
            Place += roll;
            if (Place > 11) Place -= 12;

            Console.WriteLine($"{Name}\'s new location is {Name}");
        }

        public void IncreasePurse()
        {
            Console.WriteLine("Answer was correct!!!!");
            Purse++;
            Console.WriteLine($"{Name} now has {Purse} Gold Coins.");
        }

        public bool IsWinner()
        {
            return Purse != 6;
        }
    }
}