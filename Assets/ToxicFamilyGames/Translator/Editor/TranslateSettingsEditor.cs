#if UNITY_EDITOR
namespace ToxicFamilyGames.Translator.Editor
{
    using UnityEditor;
    using UnityEngine;
    using System.Collections.Generic;
    using System.Xml;
    using static TranslatedTextEditor;
    
    [CustomEditor(typeof(TranslateSettings))]
    public class TranslateSettingsEditor : Editor
    {
        public static List<SystemLanguage> CopyKeysToDocument = new List<SystemLanguage>();
    
        private TranslateSettings ts;
        private void OnEnable()
        {
            ts = (TranslateSettings)serializedObject.targetObject;
            
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Init languages")) {
                for (int i = 0; i < ts.languages.Length; i++) {
                    ts.GetDocument(i);
                    CopyKeys();
                }
                Debug.Log("Languages initialized!");
            }
        }
    
        private void CopyKeys() {
            if (CopyKeysToDocument.Count != 0) {
                XmlDocument fromDoc = ts.GetDocument(ts.startLanguage.ToString());
                XmlElement texts = fromDoc.DocumentElement;
                int toIndex = ts.GetIndex(CopyKeysToDocument[0].ToString());
                float delay = 300f;
                long time = NowMilliseconds();
                float delta = 0;
                SystemLanguage bufferLanguage = ts.startLanguage;
                foreach (XmlElement text in texts) {
                    while (delta < delay) {
                        delta = NowMilliseconds() - time;
                    }
                    XmlNode id = text.Attributes.GetNamedItem("id");
                    AddKeyToLanguage(ts, toIndex, id.Value, text.InnerText);
                    time = NowMilliseconds();
                }
                ts.startLanguage = bufferLanguage;
                CopyKeysToDocument.RemoveAt(0);
            }
        }
    }
}
#endif