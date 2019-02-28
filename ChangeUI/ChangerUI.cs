using PluginsContracts;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel.Composition;

namespace ChangerUI
{
    [Export (typeof(IPlugin))]
    public class ChangerUI : IUIChanger
    {
        public string Name => "ChangerUI";

        public void AddItem()
        {
        }

        public void ChangeMainMenu(Window window, Menu menu)
        {
        }        

        public void ChangeUI(Window window)
        {
            
        }
    }
}
