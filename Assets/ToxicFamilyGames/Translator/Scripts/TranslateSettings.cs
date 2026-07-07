#define TOXICFAMILYGAMES_TRANSLATOR

namespace ToxicFamilyGames.Translator
{
#if UNITY_EDITOR
    using System.Text;
    using System.IO;
    using ToxicFamilyGames.Translator.Editor;
#endif
    using System.Xml;
    using UnityEngine;

    public class TranslateSettings : ScriptableObject
    {
        private string _postfix = "";
        public string postfix
        {
            get => _postfix;
            set {
                _postfix = value;
                Init();
                SwitchLanguage(SelectedLanguage);
            }
        }
        public string SelectedLanguage
        {
            get { return PlayerPrefs.GetString("selectedLanguage", startLanguage.ToString()); }
            set { PlayerPrefs.SetString("selectedLanguage", value); }
        }
        public bool startLanguageLikeSystem;
        public SystemLanguage startLanguage = SystemLanguage.Russian;
        public SystemLanguage[] languages = {SystemLanguage.Russian, SystemLanguage.English, SystemLanguage.Turkish};

        public void SwitchLanguage(int index)
        {
            SwitchLanguage(languages[index].ToString());
        }

        public void SwitchLanguage(string lang) {
            SelectedLanguage = lang;
            TranslatedText[] texts = FindObjectsByType<TranslatedText>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
            foreach (TranslatedText text in texts)
            {
                text.Init();
            }
        }

        public bool FindInnerText(string atributeValue, out string innerText)
        {
            XmlDocument doc = GetDocument();
            XmlElement texts = doc.DocumentElement;
            if (texts != null)
            {
                foreach (XmlElement text in texts)
                {
                    XmlNode id = text.Attributes.GetNamedItem("id");
                    if (id.Value == atributeValue)
                    {
                        innerText = text.InnerText;
                        return true;
                    }
                }
#if UNITY_EDITOR
                TranslatedTextEditor.AddKeyToLanguage(this, 0, atributeValue, atributeValue);
                innerText = atributeValue;
                return true;
#else
                innerText = atributeValue + " in " + SelectedLanguage + " not found!";
                return false;
#endif
            }
            throw new UnityException(Language.ConvertToISO(SelectedLanguage) + ".xml not found!");
        }

        public int GetIndex(string lang)
        {
            for (int i = 0; i < languages.Length; i++)
            {
                if (Language.ConvertToISO(languages[i]).Equals(Language.ConvertToISO(lang)))
                    return i;
            }
            Debug.LogError("Language " + lang + " in TranslateSettings not found, return 0!");
            return 0;
        }

        private XmlDocument[] documents;
        private void Init()
        {
            if (startLanguageLikeSystem) {
                SelectedLanguage = Application.systemLanguage.ToString();
            }
            documents = new XmlDocument[languages.Length];
            for (int i = 0; i < languages.Length; i++)
            {
                string iso = Language.ConvertToISO(languages[i]) + postfix;
                documents[i] = new XmlDocument();
#if !UNITY_EDITOR
                TextAsset file = Resources.Load<TextAsset>("Languages/" + iso);
                if (file == null)
                {
                    throw new UnityException("File Resources/Languages/" + iso + ".xml not found!");
                }
                documents[i].LoadXml(file.text);
#else
                string path = "Assets/ToxicFamilyGames/Translator/Resources/Languages/" + iso + ".xml";
                Directory.CreateDirectory("Assets/ToxicFamilyGames/Translator/Resources/Languages");
                if (!File.Exists(path)) {
                    using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate)) {
                        byte[] buffer = Encoding.Default.GetBytes("<text>\n</text>");
                        fs.Write(buffer, 0, buffer.Length);
                    }
                    Editor.TranslateSettingsEditor.CopyKeysToDocument.Add(languages[i]);
                }
                documents[i].Load(path);
#endif
            }
        }

        internal bool FindId(string value)
        {
            XmlDocument doc = GetDocument();
            XmlElement texts = doc.DocumentElement;
            if (texts != null)
            {
                foreach (XmlElement text in texts)
                {
                    XmlNode id = text.Attributes.GetNamedItem("id");
                    if (id.Value == value)
                    {
                        return true;
                    }
                }
                return false;
            }
            throw new UnityException(SelectedLanguage + ".xml not found!");
        }

        internal XmlDocument GetDocument(string lang)
        {
            return GetDocument(GetIndex(lang));
        }

        internal XmlDocument GetDocument(int id)
        {
            if (documents == null || id >= documents.Length) Init();
            return documents[id];
        }

        internal XmlDocument GetDocument()
        {
            return GetDocument(SelectedLanguage);
        }
    }
}