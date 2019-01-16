using System.Collections.Generic;

namespace UglyTrivia
{
    public class Category
    {
        public string Name { get; set; }

        public Stack<string> Questions { get; } = new Stack<string>();

        public int[] Places { get; set; }
    }
}