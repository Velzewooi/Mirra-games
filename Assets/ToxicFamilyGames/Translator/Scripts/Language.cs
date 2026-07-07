namespace ToxicFamilyGames.Translator
{
    using UnityEngine;
    public class Language : MonoBehaviour
    {
        public static string ConvertToISO(string language)
        {
            if (language.Length == 2) {
                return language;
            }

            return language switch
            {
                "Afrikaans" => "af",
                "Arabic" => "ar",
                "Basque" => "eu",
                "Belarusian" => "be",
                "Bulgarian" => "bg",
                "Catalan" => "ca",
                "Chinese" => "zh",
                "Czech" => "cs",
                "Danish" => "da",
                "Dutch" => "nl",
                "English" => "en",
                "Estonian" => "et",
                "Faroese" => "fo",
                "Finnish" => "fi",
                "French" => "fr",
                "German" => "de",
                "Greek" => "el",
                "Hebrew" => "he",
                "Hindi" => "hi",
                "Hungarian" => "hu",
                "Icelandic" => "is",
                "Indonesian" => "id",
                "Italian" => "it",
                "Japanese" => "ja",
                "Korean" => "ko",
                "Latvian" => "lv",
                "Lithuanian" => "lt",
                "Norwegian" => "no",
                "Polish" => "pl",
                "Portuguese" => "pt",
                "Romanian" => "ro",
                "Russian" => "ru",
                "SerboCroatian" => "sr",
                "Slovak" => "sk",
                "Slovenian" => "sl",
                "Spanish" => "es",
                "Swedish" => "sv",
                "Thai" => "th",
                "Turkish" => "tr",
                "Ukrainian" => "uk",
                "Vietnamese" => "vi",
                _ => throw new UnityException("ISO Language not found!")
            };
        }
        public static string ConvertToISO(SystemLanguage systemLanguage) {
            return ConvertToISO(systemLanguage.ToString());
        }
    }
}