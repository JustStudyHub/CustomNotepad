using System;
using System.Linq;
using System.Text;
using PluginsContracts;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace MirorText 
{
    [Export(typeof(IPlugin))]
    public class MirorText : ITextChanger
    {
        public string Name => "MirorText";

        public StringBuilder ChangeText(StringBuilder text)
        {
            if (string.IsNullOrEmpty(text.ToString()))
                return new StringBuilder();
            StringBuilder resSb = new StringBuilder();
            string[] sentence = text.ToString().Split('.');
            foreach(var s in sentence)
            {
                string reverseSentence = ReverseSentence(s);
                resSb.Append(reverseSentence);
            }
            return resSb;
        }

        public string ChangeText(string text)
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

        string ReverseSentence(string sentence)
        {
            if (string.IsNullOrEmpty(sentence))
                return string.Empty;

            StringBuilder sb = new StringBuilder();
            string[] words = sentence.Split();
            for(int i = words.Length -1; i >= 0; --i)
            {
                char[] array = words[i].ToCharArray();
                Array.Reverse(array);
                sb.AppendFormat(" {0}", new string(array));
            }
            //sb.Remove(sb.Length - 1, 1);
            sb.Append('.');
            return sb.ToString();
        }
    }
}
