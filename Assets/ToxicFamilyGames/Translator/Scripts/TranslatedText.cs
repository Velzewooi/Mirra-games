
using UnityEngine.UI;

namespace ToxicFamilyGames.Translator
{
    using System.Linq;
    using TMPro;
    using UnityEngine;

    public class TranslatedText : MonoBehaviour
    {
        [SerializeField]
        public string id;
        [SerializeField] private string _text = "";
        public string text {
            get {
                Init();
                return value == null ? _text : string.Format(_text, value);
            }
        }
#if TMPRO_TRANSLATE
        private TMP_Text _uiText;
        private TMP_Text uiText => _uiText == null ? _uiText = GetComponent<TMP_Text>() : _uiText;
#else
        private Text _uiText;
        private Text uiText => _uiText == null ? _uiText = GetComponent<Text>() : _uiText;
#endif
        private TranslateSettings settings => Resources.Load<TranslateSettings>("TranslateSettings");

        private void OnEnable()
        {
            Init();
        }

        private object[] value = new string[] {"", "", ""};
        public void SetValue(params object[] value)
        {
            this.value = value;
            if (uiText != null && uiText.text != null) {
                uiText.text = value == null ? _text : string.Format(_text, value);
            }  
        } 
        public void SetValue(object text) => SetValue(new object[] {text});
        public void SetValue(int value) => SetValue((object) value);
        public void SetValue(float value) => SetValue((object) value);
        public void SetValue(string value) => SetValue((object) value);

        public void Init()
        {
            if (settings.FindInnerText(id, out _text))
            {
                SetValue(value);
            }
        }
    }
}
