using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFluent;

namespace Trivia
{
    using NUnit.Framework;

    using UglyTrivia;

    class GameShould
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        public void Increment_the_current_player_place_to_the_roll_when_user_roll(int roll)
        {
            Game aGame = new Game();
            aGame.add("Chet");
            aGame.roll(roll);
            Check.That(aGame.places[aGame.currentPlayer]).Equals(roll);
        }

        [Test]
        public void Go_back_to_when_0_When_roll_12()
        {
            Game aGame = new Game();
            aGame.add("Chet");
            aGame.roll(12);
            Check.That(aGame.places[aGame.currentPlayer]).Equals(0);
        }

        [Test]
        public void Decrease_popQuestions_list_When_current_player_reach_pop_category()
        {
            var rolls = new[] {0, 4, 4};
            Game aGame = new Game();
            int initialValue = aGame.popQuestions.Count;
            aGame.add("Chet");
            foreach (var roll in rolls)
            {
                aGame.roll(roll);
                Check.That(aGame.popQuestions.Count).IsEqualTo(--initialValue);
            }
        }

        [Test]
        public void Decrease_scienceQuestions_list_When_current_player_reach_pop_category()
        {
            var rolls = new[] { 1, 4, 4 };
            Game aGame = new Game();
            int initialValue = aGame.scienceQuestions.Count;
            aGame.add("Chet");
            foreach (var roll in rolls)
            {
                aGame.roll(roll);
                Check.That(aGame.scienceQuestions.Count).IsEqualTo(--initialValue);
            }
        }

        [Test]
        public void Decrease_sportsQuestions_list_When_current_player_reach_pop_category()
        {
            var rolls = new[] { 2, 4, 4 };
            Game aGame = new Game();
            int initialValue = aGame.sportsQuestions.Count;
            aGame.add("Chet");
            foreach (var roll in rolls)
            {
                aGame.roll(roll);
                Check.That(aGame.sportsQuestions.Count).IsEqualTo(--initialValue);
            }
        }

        [Test]
        public void Decrease_rockQuestions_list_When_current_player_reach_pop_category()
        {
            var rolls = new[] { 11 };
            Game aGame = new Game();
            int initialValue = aGame.rockQuestions.Count;
            aGame.add("Chet");
            foreach (var roll in rolls)
            {
                aGame.roll(roll);
                Check.That(aGame.rockQuestions.Count).IsEqualTo(--initialValue);
            }
        }

        [Test]
        public void Increase_purse_When_anwser_correctly()
        {
            Game aGame = new Game();
            int purse = 0;
            aGame.add("Chet");
            for (int i = 0; i < 3; i++)
            {
                aGame.roll(1);
                aGame.wasCorrectlyAnswered();
                Check.That(aGame.purses[aGame.currentPlayer]).IsEqualTo(++purse);
            }
        }

        [Test]
        public void Go_to_penality_box_the_same_purse_When_anwser_is_wrong()
        {
            Game aGame = new Game();
            int purse = 0;
            aGame.add("Chet");
            aGame.roll(1);
            aGame.wrongAnswer();
            Check.That(aGame.inPenaltyBox[aGame.currentPlayer]).IsEqualTo(true);
        }

        [Test]
        public void Remain_the_same_purse_When_anwser_correctly()
        {
            Game aGame = new Game();
            aGame.add("Chet");
            for (int i = 0; i < 3; i++)
            {
                aGame.roll(1);
                aGame.wrongAnswer();
                Check.That(aGame.purses[aGame.currentPlayer]).IsEqualTo(0);
            }
        }

        [Test]
        public void Reamin_in_penality_box_When_was_in_penality_box_and_answer_correctly()
        {
            Game aGame = new Game();
            aGame.add("Chet");
            aGame.wrongAnswer();
            for (int i = 0; i < 3; i++)
            {
                aGame.roll(1);
                aGame.wasCorrectlyAnswered();
                Check.That(aGame.inPenaltyBox[aGame.currentPlayer]).IsEqualTo(true);
            }
        }
    }
}
