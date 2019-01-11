using System;
using System.Collections.Generic;
using System.Linq;

namespace UglyTrivia
{
    public class Game
    {
        private bool isGettingOutOfPenaltyBox;
        

        public Dictionary<int, Category> questions = new Dictionary<int, Category>();

        public readonly List<Player> players = new List<Player>();
        
        public Game()
        {
            PopCategory = CreateCategory("Pop", 0, 4, 8);
            ScienceCategory = CreateCategory("Science", 1, 5, 9);
            SportsCategory = CreateCategory("Sports", 2, 6, 10);
            RockCategory = CreateCategory("Rock", 3, 7, 11);
            for (var i = 0; i < 50; i++)
            {
                PopCategory.Questions.Push("Pop Question " + i);
                ScienceCategory.Questions.Push("Science Question " + i);
                SportsCategory.Questions.Push("Sports Question " + i);
                RockCategory.Questions.Push(createRockQuestion(i));
            }
        }

        private Category CreateCategory(string name, params int[] places)
        {
            var popCategory = new Category {Name = name, Places = places};
            foreach (var place in popCategory.Places)
            {
                questions.Add(place, popCategory);
            }
            return popCategory;
        }

        public string currentCategory()
        {
            var player = players[currentPlayer];
            return questions[player.Place].Name;
        }

        public Category PopCategory { get; } 
        public Category ScienceCategory { get; } 
        public Category SportsCategory { get; }
        public Category RockCategory { get; } 

        public int currentPlayer { get; set; }

        public string createRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool isPlayable()
        {
            return howManyPlayers() >= 2;
        }

        public bool add(string playerName)
        {
            players.Add(new Player(playerName));
            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
            return true;
        }

        public int howManyPlayers()
        {
            return players.Count;
        }

        public void roll(int roll)
        {
            var player = players[currentPlayer];
            Console.WriteLine(player.Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (player.InPenaltyBox)
            {
                if (roll % 2 != 0)
                {
                    isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(player.Name + " is getting out of the penalty box");
                    MovePlayer(player, roll);
                    askQuestion();
                }
                else
                {
                    Console.WriteLine(player.Name + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                MovePlayer(player, roll);
                askQuestion();
            }
        }

        private void MovePlayer(Player player, int roll)
        {
            player.Place += roll;
            if (player.Place > 11) player.Place -= 12;

            Console.WriteLine(player.Name
                              + "'s new location is "
                              + player.Name);
        }

        private void PrintCategory()
        {
            Console.WriteLine("The category is " + currentCategory());
        }

        private void askQuestion()
        {
            PrintCategory();
            var player = players[currentPlayer];
            var question = questions[player.Place].Questions;
            Console.WriteLine(question.Pop());
        }

        public bool wasCorrectlyAnswered()
        {
            var player = players[currentPlayer];
            if (player.InPenaltyBox)
            {
                if (isGettingOutOfPenaltyBox)
                {
                    IncreasePurse();
                    var winner = didPlayerWin();
                    ChangePlayer();

                    return winner;
                }
                ChangePlayer();
                return true;
            }
            IncreasePurse();
            var isWinner = didPlayerWin();
            ChangePlayer();

            return isWinner;
        }

        private void IncreasePurse()
        {
            var player = players[currentPlayer];
            Console.WriteLine("Answer was correct!!!!");
            player.Purse++;
            Console.WriteLine(player.Name
                              + " now has "
                              + player.Purse
                              + " Gold Coins.");
        }

        private void ChangePlayer()
        {
            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
        }

        public bool wrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            var player = players[currentPlayer];
            Console.WriteLine(player.Name + " was sent to the penalty box");
            player.InPenaltyBox = true;
            ChangePlayer();
            return true;
        }


        private bool didPlayerWin()
        {
            var player = players[currentPlayer];
            return player.Purse != 6;
        }
    }

    public class Category
    {
        public string Name { get; set; }

        public Stack<string> Questions { get; } = new Stack<string>();

        public int[] Places { get; set; }
    }

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
    }
}