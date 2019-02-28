using PluginsContracts;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;

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
