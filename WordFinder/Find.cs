using PluginsContracts;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WordFinder
{
    [Export(typeof(IPlugin))]
    public class Find : ITextEditor
    {
        Window _window;
        RichTextBox _richTextBox;
        static TextPointer endFindPos;
        public string Name => "Find";

        public void EditText(Window window, RichTextBox richTextBox)
        {
            _window = window;
            _richTextBox = richTextBox;
            InitFindWindow();

        }

        private void InitFindWindow()
        {
            Window findWindow = new Window();
            findWindow.Height = 70;
            findWindow.Width = 150;
            findWindow.Owner = _window;
            findWindow.Left = _window.Left + _window.Width - findWindow.Width;
            findWindow.Top = _window.Top + 30;

            TextBox textBox = new TextBox();
            textBox.Name = "tb_find";
            textBox.Height = 18;

            Button button = new Button();
            button.Height = 20;
            button.Width = 60;
            button.Click += OnButtonClick;


            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            Grid.SetRow(textBox, 0);
            Grid.SetRow(button, 0);
            Grid.SetColumn(textBox, 0);
            Grid.SetColumn(button, 1);
            grid.Children.Add(textBox);
            grid.Children.Add(button);

            button.Content = "Find";
            findWindow.Content = grid;
            findWindow.Show();

        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            var grid = ((Button)sender).Parent;
            var gridElem = ((Grid)grid).Children;
            foreach(var ge in gridElem)
            {
                if(ge.GetType() == typeof(TextBox))
                {
                    FindWord(((TextBox)ge).Text);
                }
            }
        }

        private void FindWord(string word)
        {
            bool isFound = false;
            TextRange text = new TextRange(_richTextBox.Document.ContentStart, _richTextBox.Document.ContentEnd);
            TextPointer current;
            if(endFindPos == null)
            {
                current = text.Start.GetInsertionPosition(LogicalDirection.Forward);
            }
            else
            {
                current = endFindPos;
            }
            while (current != null && !isFound)
            {
                string textInRun = current.GetTextInRun(LogicalDirection.Forward);
                if (!string.IsNullOrWhiteSpace(textInRun))
                {
                    int index = textInRun.IndexOf(word);
                    if (index != -1)
                    {
                        TextPointer selectionStart = current.GetPositionAtOffset(index, LogicalDirection.Forward);
                        TextPointer selectionEnd = selectionStart.GetPositionAtOffset(word.Length, LogicalDirection.Forward);
                        endFindPos = selectionEnd;
                        TextRange selection = new TextRange(selectionStart, selectionEnd);
                        //selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                        FrameworkContentElement e = selectionStart.Parent as FrameworkContentElement;
                        if (e != null)
                            e.BringIntoView();
                        _richTextBox.Selection.Select(selection.Start, selection.End);
                        _richTextBox.Focus();
                        isFound = true;
                    }
                }
                current = current.GetNextContextPosition(LogicalDirection.Forward);
                if (current == null)
                {
                    current = text.Start.GetInsertionPosition(LogicalDirection.Forward);
                }
            }
            if (current == null)
            {
                endFindPos = null;
            }
        }
    }
}
