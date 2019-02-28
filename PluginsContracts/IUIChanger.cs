
using System.Windows;
using System.Windows.Controls;

namespace PluginsContracts
{
    public interface IUIChanger : IPlugin
    {
        void ChangeUI(Window window);
        void ChangeMainMenu(Window window, Menu menu);
        void AddItem();
    }
}
