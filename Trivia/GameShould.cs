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
            Check.That(aGame.players[aGame.currentPlayer].Place).Equals(roll);
        }

        [Test]
        public void Go_back_to_when_0_When_roll_12()
        {
            Game aGame = new Game();
            aGame.add("Chet");
            aGame.roll(12);
            Check.That(aGame.players[aGame.currentPlayer].Place).Equals(0);
        }

        [Test]
        public void Decrease_popQuestions_list_When_current_player_reach_pop_category()
        {
            var rolls = new[] {0, 4, 4};
            Game aGame = new Game();
            int initialValue = aGame.PopCategory.Questions.Count;
            aGame.add("Chet");
            foreach (var roll in rolls)
            {
                aGame.roll(roll);
                Check.That(aGame.PopCategory.Questions.Count).IsEqualTo(--initialValue);
            }
        }

        [Test]
        public void Decrease_scienceQuestions_list_When_current_player_reach_pop_category()
        {
            var rolls = new[] { 1, 4, 4 };
            Game aGame = new Game();
            int initialValue = aGame.ScienceCategory.Questions.Count;
            aGame.add("Chet");
            foreach (var roll in rolls)
            {
                aGame.roll(roll);
                Check.That(aGame.ScienceCategory.Questions.Count).IsEqualTo(--initialValue);
            }
        }

        [Test]
        public void Decrease_sportsQuestions_list_When_current_player_reach_pop_category()
        {
            var rolls = new[] { 2, 4, 4 };
            Game aGame = new Game();
            int initialValue = aGame.SportsCategory.Questions.Count;
            aGame.add("Chet");
            foreach (var roll in rolls)
            {
                aGame.roll(roll);
                Check.That(aGame.SportsCategory.Questions.Count).IsEqualTo(--initialValue);
            }
        }

        [Test]
        public void Decrease_rockQuestions_list_When_current_player_reach_pop_category()
        {
            var rolls = new[] { 11 };
            Game aGame = new Game();
            int initialValue = aGame.RockCategory.Questions.Count;
            aGame.add("Chet");
            foreach (var roll in rolls)
            {
                aGame.roll(roll);
                Check.That(aGame.RockCategory.Questions.Count).IsEqualTo(--initialValue);
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
                Check.That(aGame.players[aGame.currentPlayer].Purse).IsEqualTo(++purse);
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
            Check.That(aGame.players[aGame.currentPlayer].InPenaltyBox).IsEqualTo(true);
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
                Check.That(aGame.players[aGame.currentPlayer].Purse).IsEqualTo(0);
            }
        }

        [Test]
        public void Remain_in_penality_box_When_was_in_penality_box_and_answer_correctly()
        {
            Game aGame = new Game();
            aGame.add("Chet");
            aGame.wrongAnswer();
            for (int i = 0; i < 3; i++)
            {
                aGame.roll(1);
                aGame.wasCorrectlyAnswered();
                Check.That(aGame.players[aGame.currentPlayer].InPenaltyBox).IsEqualTo(true);
            }
        }

        [Test]
        public void Remain_penality_box_When_roll_2_and_was_in_penality_box_and_answer_correctly()
        {
            Game aGame = new Game();
            aGame.add("Chet");
            aGame.wrongAnswer();
            aGame.roll(2);
            aGame.wasCorrectlyAnswered();
            Check.That(aGame.players[aGame.currentPlayer].InPenaltyBox).IsEqualTo(true);
        }

        [Test]
        public void Win_When_answer_6_question_correctly()
        {
            Game aGame = new Game();
            aGame.add("Chet");
            aGame.roll(1);
            Check.That(aGame.wasCorrectlyAnswered()).IsEqualTo(true);
            aGame.roll(1);
            Check.That(aGame.wasCorrectlyAnswered()).IsEqualTo(true);
            aGame.roll(1);
            Check.That(aGame.wasCorrectlyAnswered()).IsEqualTo(true);
            aGame.roll(1);
            Check.That(aGame.wasCorrectlyAnswered()).IsEqualTo(true);
            aGame.roll(1);
            Check.That(aGame.wasCorrectlyAnswered()).IsEqualTo(true);
            aGame.roll(1);
            Check.That(aGame.wasCorrectlyAnswered()).IsEqualTo(false);
        }
    }
}
