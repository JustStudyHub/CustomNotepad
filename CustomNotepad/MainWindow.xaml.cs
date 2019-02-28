using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Microsoft.Win32;
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
                if (interfaces.Contains(typeof(ITextEditor)))
                {
                    menuItem.Click += OnTextEdit;
                }
                if (interfaces.Contains(typeof(IUIChanger)))
                {
                    menuItem.Click += OnChangerUI;
                }
                MI_Plugins.Items.Add(menuItem);
                menuPlugins.Add(t.Name, p);
            }
        }

        private void OnTextEdit(object sender, RoutedEventArgs e)
        {
            string menuHeader = ((MenuItem)sender).Header.ToString();
            if (menuPlugins.ContainsKey(menuHeader))
            {
                IPlugin plugin = menuPlugins[menuHeader];
                ((ITextEditor)plugin).EditText(this, RTB_Text);
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

        private void OnChangerUI(object sender, RoutedEventArgs e)
        {
            string menuHeader = ((MenuItem)sender).Header.ToString();
            if (menuPlugins.ContainsKey(menuHeader))
            {
                IUIChanger plugin = (IUIChanger)menuPlugins[menuHeader];
                plugin.ChangeUI(this);
                plugin.AddItem();
                plugin.ChangeMainMenu(this, MainMenu);
            }
        }

        private void OnSaveFileClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt";

            if (saveFileDialog.ShowDialog() == true)
            {
                TextRange tr = new TextRange(RTB_Text.Document.ContentStart, RTB_Text.Document.ContentEnd);
                File.WriteAllText(saveFileDialog.FileName, tr.Text);
            }
        }

        private void OnOpenFileClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == true)
            {
                RTB_Text.SelectAll();
                RTB_Text.Selection.Text = "";
                RTB_Text.AppendText(File.ReadAllText(openFileDialog.FileName));
            }
        }
    }
}
