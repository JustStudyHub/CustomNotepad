using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PluginsContracts;

namespace CustomNotepad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, IPlugin> menuPlugins;
        public MainWindow()
        {
            InitializeComponent();
            RTB_Text.Height = G_Main.Height;
            PluginsLoader<IPlugin> loader = new PluginsLoader<IPlugin>("..\\..\\..\\Plugins");
            IEnumerable<IPlugin> plugins = loader.Plugins;
            menuPlugins = new Dictionary<string, IPlugin>();
            foreach(var p in plugins)
            {
                Type t = p.GetType();
                Type[] interfaces = t.GetInterfaces();

                MenuItem menuItem = new MenuItem();
                menuItem.Header = t.Name;
                if (interfaces.Contains(typeof(ITextChanger)))
                {
                    menuItem.Click += OnTextChanger;
                }
                MI_Plugins.Items.Add(menuItem);
                menuPlugins.Add(t.Name, p);
            }
        }

        private void OnTextChanger(object sender, RoutedEventArgs e)
        {
            string menuHeader = ((MenuItem)sender).Header.ToString();
            if (menuPlugins.ContainsKey(menuHeader))
            {
                IPlugin plugin = menuPlugins[menuHeader];
                TextRange tr = new TextRange(RTB_Text.Document.ContentStart, RTB_Text.Document.ContentEnd);
                string text = ((ITextChanger)plugin).ChangeText(tr.Text);
                RTB_Text.SelectAll();
                RTB_Text.Selection.Text = "";
                RTB_Text.AppendText(text);
            }

        }
    }
}
