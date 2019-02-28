
using System.Windows;
using System.Windows.Controls;

namespace PluginsContracts
{
    public interface ITextEditor : IPlugin
    {
        void EditText(Window window, RichTextBox richTextBox);
    }
}
