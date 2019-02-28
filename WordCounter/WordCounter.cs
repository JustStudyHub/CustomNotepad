using PluginsContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCounter
{
    public class WordCounter : IWordCounter
    {
        public string Name => "WordCounter";

        public int CountWord(StringBuilder text)
        {
            throw new NotImplementedException();
        }

        public int CountWord(string text)
        {
            throw new NotImplementedException();
        }

        public int CountWord(StringBuilder text, string word)
        {
            throw new NotImplementedException();
        }

        public int CountWord(string text, string word)
        {
            throw new NotImplementedException();
        }
    }
}
