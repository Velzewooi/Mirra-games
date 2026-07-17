using System;
using UnityEngine;
using MirraGames.SDK;
using MirraGames.SDK.Common;

[CreateAssetMenu(fileName = "MirraTime")]
public class Time : ScriptableObject
{
    public static float time => UnityEngine.Time.time;
    
    public static float timeScale
    {
        get => MirraSDK.Time.Scale;
        set => MirraSDK.Time.Scale = value;
    }

    public static float deltaTime
    {
        get => UnityEngine.Time.deltaTime;
    }

    public static float unscaledTime
    {
        get => UnityEngine.Time.unscaledTime;
    }

    public static float realtimeSinceStartup
    {
        get => UnityEngine.Time.realtimeSinceStartup;
    }

    public static float unscaledDeltaTime
    {
        get => UnityEngine.Time.unscaledDeltaTime;
    }

    public static DateTime currentDate
    {
        get => MirraSDK.Time.CurrentDate;
    }

    public static HolidayType holydayType
    {
        get => MirraSDK.Time.CurrentHoliday;
    }
}