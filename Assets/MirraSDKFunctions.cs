using System.Collections.Generic;
using UnityEngine;
using MirraGames.SDK;
using MirraGames.SDK.Common;
using ToxicFamilyGames.Translator;

[CreateAssetMenu(fileName="MirraSDKFunctions")]
public class MirraSDKFunctions : ScriptableObject
{
    public static void OnStart()
    {
        MirraSDK.WaitForProviders(static () =>
        {
            print("IsAdsAvailable: " + MirraSDK.Ads.IsAdsAvailable);
            print("IsBannerAvailable: " + MirraSDK.Ads.IsBannerAvailable);
            print("IsInterstitialAvailable: " + MirraSDK.Ads.IsInterstitialAvailable);
            print("IsRewardedAvailable: " + MirraSDK.Ads.IsRewardedAvailable);
            print("IsGameReadyAvailable: " + MirraSDK.Analytics.IsGameplayReporterAvailable);
            print("Language: " + language);
            print("IsMobile: " + isMobile);
            print("Payments: " + MirraSDK.Payments.IsPaymentsAvailable);
            print("PlatformType: " + MirraSDK.Platform.Current);
            print("DeploymentType: " + MirraSDK.Platform.Deployment);
            print("PlatformAppId: " + MirraSDK.Platform.AppId);
            print("PlayerDisplayName: " + MirraSDK.Player.DisplayName);
            print("PlayerFirstName: " + MirraSDK.Player.FirstName);
            print("PlayerLastName: " + MirraSDK.Player.LastName);
            print("PlayerUsername: " + MirraSDK.Player.Username);
            print("PlayerID: " + MirraSDK.Player.UniqueId);
            print("PlayerIsLogin: " + MirraSDK.Player.IsLoggedIn);
            
            Resources.Load<TranslateSettings>(nameof(TranslateSettings)).SwitchLanguage(language.ToString());
            MirraSDK.Payments.RestorePurchases(restoreData =>
            {
                string[] allPurchases = restoreData.AllPurchases;
                print($"Игрок совершил '{allPurchases.Length}' успешных покупок");

                string[] pendingProducts = restoreData.PendingProducts;
                print($"Игрок не получил '{pendingProducts.Length}' разных товаров: [{string.Join(", ", pendingProducts)}]");
                
                foreach(string productTag in pendingProducts) {
                    restoreData.RestoreProduct(productTag, onProductRestore: () => {
                        print($"Товар '{productTag}' восстановлен");
                    });
                }
            });
        });
    }

    public static float audioVolume
    {
        get => MirraSDK.Audio.Volume;
        set => MirraSDK.Audio.Volume = Mathf.Clamp01(value);
    }

    public static bool audioPause
    {
        get => MirraSDK.Audio.Pause;
        set => MirraSDK.Audio.Pause = value;
    }

    public static LanguageType language
    {
        get => MirraSDK.Language.Current;
    }

    public static bool isMobile
    {
        get => MirraSDK.Device.IsMobile;
    }

    public static SystemType systemType
    {
        get => MirraSDK.Device.SystemType;
    }

    public static bool cursorVisible
    {
        get => MirraSDK.Device.CursorVisible;
        set => MirraSDK.Device.CursorVisible = value;
    }

    public static CursorLockMode cursorLockMode
    {
        get => MirraSDK.Device.CursorLock;
        set => MirraSDK.Device.CursorLock = value;
    }

    public static void ShareGame(string message)
    {
        MirraSDK.Platform.ShareGame(message);
    }

    public static void RateGame()
    {
        MirraSDK.Platform.RateGame();
    }

    public static void Purchase(string tag)
    {
        MirraSDK.Payments.Purchase(
            productTag: tag,
            onSuccess: () => OnPurchaseSuccess(tag),
            onError: () => OnPurchaseError(tag)
        );
    }

    public static void OpenURL(string url)
    {
        MirraSDK.Device.OpenUrl(url);
    }

    public static void GameIsReady()
    {
        MirraSDK.Analytics.GameIsReady();
    }

    public static void GameReadyStart(int level)
    {
        MirraSDK.Analytics.GameplayStart(level);
    }

    public static void GameReadyRestart(int level)
    {
        MirraSDK.Analytics.GameplayRestart(level);
    }

    public static void GameReadyStop(int level)
    {
        MirraSDK.Analytics.GameplayStop(level);
    }

    public static void AnalyticsReport(string eventName, string value = "", Dictionary<string, object> eventParameters = null)
    {
        if (value == "" && eventParameters == null)
        {
            MirraSDK.Analytics.Report(eventName);
        }

        if (value != "")
        {
            MirraSDK.Analytics.Report(eventName, value);
        }

        if (eventParameters != null)
        {
            MirraSDK.Analytics.Report(eventName, eventParameters);
        }
    }
    
    public static void InvokeInterstitial()
    {
        MirraSDK.Ads.InvokeInterstitial(
            onOpen: OnInterstitialOpen,
            onClose: OnInterstitialClose
        );
    }

    public static void InvokeRewarded(string tag)
    {
        MirraSDK.Ads.InvokeRewarded(
            onOpen: () => OnRewardedOpen(tag),
            onClose: granted => OnRewardedClose(tag, granted)
        );
    }

    public static void InvokeBanner()
    {
        MirraSDK.Ads.InvokeBanner();
    }

    public static void RefreshBanner()
    {
        MirraSDK.Ads.RefreshBanner();
    }

    public static void DisableBanner()
    {
        MirraSDK.Ads.DisableBanner();
    }

    public static void InvokeLogin()
    {
        MirraSDK.Player.InvokeLogin(
            OnLoginSuccess,
            OnLoginFailed
        );
    }

    private static void OnLoginSuccess()
    {
        print("LoginSuccess");
    }

    private static void OnLoginFailed()
    {
        print("LoginFailed");
    }
    
    private static void OnPurchaseSuccess(string tag)
    {
        print("PurchaseSuccess: " + tag);
    }

    private static void OnPurchaseError(string tag)
    {
        print("PurchaseError: " + tag);
    }
    
    private static void OnInterstitialOpen()
    {
        print("InterstitialOpen");
    }

    private static void OnInterstitialClose(bool isError)
    {
        print("IntersisialClose: error=" + isError);
    }

    private static void OnRewardedOpen(string tag)
    {
        print($"RewardOpen: tag={tag}");
    }

    private static void OnRewardedClose(string tag, bool rewardGranted)
    {
        print($"RewardClose: tag={tag}, isRewardGranted={rewardGranted}");
    }

    private static void print(string text)
    {
        Debug.Log("MirraSDKFunctions: " + text);
    }
}