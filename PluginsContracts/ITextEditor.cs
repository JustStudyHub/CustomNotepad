using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PluginsContracts
{
    public interface ITextEditor : IPlugin
    {
        void EditText(Window window, RichTextBox richTextBox);
    }
}
