using System;
using System.Collections.Generic;

namespace UglyTrivia
{
    public class Game
    {
        private readonly List<Player> _players = new List<Player>();


        private readonly Dictionary<int, Category> _questions = new Dictionary<int, Category>();
        private bool _isGettingOutOfPenaltyBox;

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
                RockCategory.Questions.Push(CreateRockQuestion(i));
            }
        }

        public Player CurrentPlayer => _players[_currentPlayerIndex];

        public Category PopCategory { get; }
        public Category ScienceCategory { get; }
        public Category SportsCategory { get; }
        public Category RockCategory { get; }

        private int _currentPlayerIndex;

        private Category CreateCategory(string name, params int[] places)
        {
            var popCategory = new Category {Name = name, Places = places};
            foreach (var place in popCategory.Places) _questions.Add(place, popCategory);
            return popCategory;
        }

        private Category CurrentCategory =>_questions[CurrentPlayer.Place];

        private string CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool isPlayable()
        {
            return howManyPlayers() >= 2;
        }

        public bool add(string playerName)
        {
            _players.Add(new Player(playerName));
            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + _players.Count);
            return true;
        }

        public int howManyPlayers()
        {
            return _players.Count;
        }

        public void Roll(int dice)
        {
            Console.WriteLine($"{CurrentPlayer.Name} is the current player");
            Console.WriteLine($"They have rolled a {dice}");

            if (CurrentPlayer.InPenaltyBox)
            {
                if (IsGettigOutPenalityBox(dice))
                {
                    CurrentPlayer.MovePlayer(dice);
                    AskQuestion();
                }
            }
            else
            {
                CurrentPlayer.MovePlayer(dice);
                AskQuestion();
            }
        }

        private bool IsGettigOutPenalityBox(int dice)
        {
            if (IsOdd(dice))
            {
                _isGettingOutOfPenaltyBox = true;
                Console.WriteLine($"{CurrentPlayer.Name} is getting out of the penalty box");
            }
            else
            {
                Console.WriteLine($"{CurrentPlayer.Name} is not getting out of the penalty box");
                _isGettingOutOfPenaltyBox = false;
            }

            return _isGettingOutOfPenaltyBox;
        }

        private static bool IsOdd(int dice)
        {
            return dice % 2 != 0;
        }

        private void PrintCategory()
        {
            Console.WriteLine($"The category is {CurrentCategory.Name}");
        }

        private void AskQuestion()
        {
            PrintCategory();
            Console.WriteLine(CurrentCategory.Questions.Pop());
        }

        public bool WasCorrectlyAnswered()
        {
            if (CurrentPlayer.InPenaltyBox)
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    return ChangePlayer(_isGettingOutOfPenaltyBox); ;
                }
                return ChangePlayer(_isGettingOutOfPenaltyBox);
            }
            return ChangePlayer(false);
        }

        private bool ChangePlayer(bool isInPenalityBox)
        {
            bool winner = true;
            if (!isInPenalityBox)
            {
                CurrentPlayer.IncreasePurse();
                winner = CurrentPlayer.IsWinner();
            }
            _currentPlayerIndex++;
            if (_currentPlayerIndex == _players.Count) _currentPlayerIndex = 0;
            return winner;
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine($"{CurrentPlayer.Name} was sent to the penalty box");
            CurrentPlayer.InPenaltyBox = true;
            return ChangePlayer(true);
        }
    }
}