using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginsContracts
{
    public interface ITextParser : IPlugin
    {
        StringBuilder ChangeText(StringBuilder text);
        StringBuilder ChangeText(string text);
    }
}
