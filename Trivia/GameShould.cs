using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trivia
{
    using NUnit.Framework;

    using UglyTrivia;

    class GameShould
    {
        [Test]
        public void testcase()
        {
            Game aGame = new Game();
            aGame.add("Chet");
            aGame.roll(1);

        }
    }
}
