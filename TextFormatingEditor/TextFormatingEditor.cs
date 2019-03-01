using PluginsContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace TextFormatingEditor
{
    [Export(typeof(IPlugin))]
    public class TextFormatingEditor : ITextEditor
    {
        Window _window;
        RichTextBox _richTextBox;
        public string Name => "TextFormatingEditor";

        public void EditText(Window window, RichTextBox richTextBox)
        {
            _window = window;
            _richTextBox = richTextBox;
            InitInterface();
        }
        
        private void InitInterface()
        {
            Menu editorMenu = new Menu();
            Thickness menuMargin = new Thickness();
            editorMenu.Height = 19;
            menuMargin.Top = 19;
            editorMenu.VerticalAlignment = VerticalAlignment.Top;
            editorMenu.Margin = menuMargin;

            MenuItem mi_bold = new MenuItem();
            mi_bold.Header = "Bold";
            mi_bold.Click += OnClick;

            MenuItem mi_italic = new MenuItem();
            mi_italic.Header = "Italic";
            mi_italic.Click += OnClick;

            MenuItem mi_relief = new MenuItem();
            mi_relief.Header = "Relief";
            mi_relief.Click += OnClick;

            MenuItem mi_normal = new MenuItem();
            mi_normal.Header = "Normal";
            mi_normal.Click += OnClick;

            Thickness rtbMargin = _richTextBox.Margin;
            rtbMargin.Top += menuMargin.Top;
            _richTextBox.Margin = rtbMargin;

            editorMenu.Items.Add(mi_normal);
            editorMenu.Items.Add(mi_bold);
            editorMenu.Items.Add(mi_italic);
            editorMenu.Items.Add(mi_relief);

            Grid grid = (Grid)_window.Content;
            grid.Children.Add(editorMenu);
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            string operationName = ((MenuItem)sender).Header.ToString();
            ChangeSelected(operationName);
        }
        private void ChangeSelected(string changeType)
        {
            TextRange selection = _richTextBox.Selection;
            switch (changeType)
            {
                case "Bold":
                    selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                    break;
                case "Italic":
                    selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
                    break;
                case "Relief":
                    selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Oblique);
                    break;
                case "Normal":
                    selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
                    selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
                    break;
            }
        }
    }
}
