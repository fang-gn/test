using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForAICodeReview
{
    public static class Test
    {
        public static bool PrintListItems()
        {
            var list = new List<string> { "1", "2" };

            if (list?.Count == 0)
            {
                return false;
            }

            for (var i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }
         
            return false;
        }

    }
}
