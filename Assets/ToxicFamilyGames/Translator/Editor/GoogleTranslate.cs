#if UNITY_EDITOR
namespace ToxicFamilyGames.Translator.Editor {
    using UnityEngine;
    #pragma warning disable
    public class GoogleTranslate
    { 
        public static string Translate(string text, string from, string to)
        {
            string[] lines = text.Split('\n');
            string result = "";
            for (int i = 0; i < lines.Length; i++) {
                string url = "https://translate.googleapis.com/translate_a/single?client=gtx&sl=" + from + "&tl=" + to + "&dt=t&q=" + WWW.EscapeURL(lines[i]);
                string line;
                WWW www = new WWW(url);
                while(!www.isDone) {
                
                }
                line = string.IsNullOrEmpty(www.error) ? www.text : null;
                line = line.Split("\",")[0];
                line = line.Substring(4, line.Length-4);
                line = line.Replace("\\\"", "\"");
                result += ((i != 0) ? "\n" : "") + line;
            }
            return result;
        }
    
        public static string Translate(string text, SystemLanguage from, SystemLanguage to) {
            return Translate(text, Language.ConvertToISO(from), Language.ConvertToISO(to));
        }
    }
}
#endif
