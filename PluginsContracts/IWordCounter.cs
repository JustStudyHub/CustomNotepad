using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginsContracts
{
    public interface IWordCounter : IPlugin
    {
        int CountWord(StringBuilder text);
        int CountWord(string text);
        int CountWord(StringBuilder text, string word);
        int CountWord(string text, string word);
    }
}
