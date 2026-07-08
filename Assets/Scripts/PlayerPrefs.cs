using UnityEngine;
using MirraGames.SDK;

[CreateAssetMenu(fileName = "MirraPrefs")]
public class PlayerPrefs : ScriptableObject
{
    public static bool GetBool(string key, bool defaultValue = false)
    {
        return MirraSDK.Data.GetBool(key, defaultValue);
    }

    public static void SetBool(string key, bool value)
    {
        MirraSDK.Data.SetBool(key, value);
    }

    public static int GetInt(string key, int defaultValue = 0)
    {
        return MirraSDK.Data.GetInt(key, defaultValue);
    }

    public static void SetInt(string key, int value)
    {
        MirraSDK.Data.SetInt(key, value);
    }

    public static float GetFloat(string key, float value = 0f)
    {
        return MirraSDK.Data.GetFloat(key, value);
    }

    public static void SetFloat(string key, float value)
    {
        MirraSDK.Data.SetFloat(key, value);
    }

    public static string GetString(string key, string defaultValue = "")
    {
        return MirraSDK.Data.GetString(key, defaultValue);
    }

    public static void SetString(string key, string value)
    {
        MirraSDK.Data.SetString(key, value);
    }

    public static T GetObject<T>(string key)
    {
        return MirraSDK.Data.GetObject<T>(key);
    }

    public static T GetObject<T>(string key, T defaultValue)
    {
        return MirraSDK.Data.GetObject(key, defaultValue);
    }

    public static void SetObject<T>(string key, T value)
    {
        MirraSDK.Data.SetObject(key, value);
    }

    public static void Save()
    {
        MirraSDK.Data.Save();
    }

    public static bool HasKey(string key)
    {
        return MirraSDK.Data.HasKey(key);
    }

    public static void DeleteKey(string key)
    {
        MirraSDK.Data.DeleteKey(key);
    }

    public static void DeleteAll()
    {
        MirraSDK.Data.DeleteAll();
    }
}