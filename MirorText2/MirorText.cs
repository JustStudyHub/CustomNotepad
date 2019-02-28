using PluginsContracts;
using System;
using System.ComponentModel.Composition;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MirorText2
{
    [Export(typeof(IPlugin))]
    public class MirorText2 : ITextEditor
    {
        public string Name => "MirorText2";        

        public void EditText(Window window, RichTextBox richTextBox)
        {
            TextRange tr = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            string text = ChangeText(tr.Text);
            richTextBox.SelectAll();
            richTextBox.Selection.Text = "";
            richTextBox.AppendText(text);
        }
        private string ChangeText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            StringBuilder resSb = new StringBuilder();
            string[] sentence = text.ToString().Split('.');
            foreach (var s in sentence)
            {
                string reverseSentence = ReverseSentence(s);
                resSb.Append(reverseSentence);
            }
            return resSb.ToString();
        }

        private string ReverseSentence(string sentence)
        {
            if (string.IsNullOrEmpty(sentence))
                return string.Empty;
            if (sentence == "\r\n")
                return string.Empty;

            StringBuilder sb = new StringBuilder();
            string[] words = sentence.Split();
            char[] array;
            for (int i = words.Length - 1; i > 0; --i)
            {
                array = words[i].ToCharArray();
                Array.Reverse(array);
                sb.AppendFormat("{0} ", new string(array));
            }

            array = words[0].ToCharArray();
            Array.Reverse(array);
            sb.AppendFormat("{0}. ", new string(array));
            return sb.ToString();
        }
    }
}
