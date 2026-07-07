#if UNITY_EDITOR
namespace ToxicFamilyGames.Translator.Editor {
    using UnityEngine;
    using UnityEditor;
    using System.Xml;
    using TMPro;

    [CanEditMultipleObjects]
    [CustomEditor(typeof(TranslatedText))]
    public class TranslatedTextEditor : Editor
    {
        SerializedProperty id;
        SerializedProperty text;
#if TMPRO_TRANSLATE
        private TMPro.TMP_Text tmp;
#else
        private UnityEngine.UI.Text tmp;
#endif
        private TranslateSettings ts;
        private void OnEnable()
        {
            id = serializedObject.FindProperty("id");
            text = serializedObject.FindProperty("_text");
            #if TMPRO_TRANSLATE
            tmp = ((TranslatedText)serializedObject.targetObject).gameObject.GetComponent<TMPro.TMP_Text>();
            #else
            tmp = ((TranslatedText)serializedObject.targetObject).gameObject.GetComponent<UnityEngine.UI.Text>();
            #endif
            ts = Resources.Load<TranslateSettings>("TranslateSettings");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(id);
            bool idIsReady = ts.FindId(id.stringValue); 
            if (tmp == null && !idIsReady) {
                EditorGUILayout.PropertyField(text);
            }
            if (!Application.isPlaying) {
                ts.SelectedLanguage = ts.startLanguage.ToString();
            }
            if (!idIsReady && GUILayout.Button("Create new id"))
            {
                int index = 0;
                float delay = 300f;
                long time = NowMilliseconds();
                while (index < ts.languages.Length) {
                    if (NowMilliseconds() - time >= delay) {
                        AddKeyToLanguage(ts, index, id.stringValue, tmp == null ? text.stringValue : tmp.text);
                        index++;
                        time = NowMilliseconds();
                    }
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
        public static void AddKeyToLanguage(TranslateSettings ts, int languageIndex, string key, string value) 
        {
            XmlDocument xmlDoc = ts.GetDocument(ts.languages[languageIndex].ToString());
            XmlElement texts = xmlDoc.DocumentElement;
            XmlElement text = xmlDoc.CreateElement("text");
            XmlAttribute idAttribute = xmlDoc.CreateAttribute("id");
            XmlText idText = xmlDoc.CreateTextNode(key);
            XmlText innerText;
            if (ts.languages[languageIndex] == ts.startLanguage) {
                innerText = xmlDoc.CreateTextNode(value);
            } else {
                innerText = xmlDoc.CreateTextNode(GoogleTranslate.Translate(value, ts.startLanguage, ts.languages[languageIndex]));
            }
            idAttribute.AppendChild(idText);
            text.AppendChild(innerText);
            text.Attributes.Append(idAttribute);
            texts.AppendChild(text);
            xmlDoc.Save("Assets/ToxicFamilyGames/Translator/Resources/Languages/" + Language.ConvertToISO(ts.languages[languageIndex]) + ".xml");
        }
        public static long NowMilliseconds() 
        {
            return System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
        }
    }
}
#endif
