namespace Appodeal;

using System;
using Godot;
using Godot.Collections;

[Flags]
public enum AppodealAdType
{
    Interstitial = 1,
    Banner = 2,
    RewardedVideo = 4,
    MREC = 8,
}

[Flags]
public enum ShowStyle
{
    Interstitial = 1,
    BannerBottom = 2,
    BannerTop = 4,
    BannerLeft = 8,
    BannerRight = 16,
    RewardedVideo = 32,
}

public enum LogLevel
{
    Verbose,
    Debug,
    None,
}

public enum PlayStorePurchaseType
{
    InApp,
    Subs,
}

public enum AppStorePurchaseType
{
    Consumable,
    NonConsumable,
    AutoRenewableSubscription,
    NonRenewingSubscription,
}

public static class ViewPosition
{
    public const int HORIZONTALSMART = -1;
    public const int HORIZONTALCENTER = -2;
    public const int HORIZONTALRIGHT = -3;
    public const int HORIZONTALLEFT = -4;

    public const int VERTICALBOTTOM = -1;
    public const int VERTICALTOP = -2;
}

public static class AppodealNetwork
{
    public const string A4G = "a4g";
    public const string AdColony = "adcolony";
    public const string AdMob = "admob";
    public const string AdMobMediation = "admob_mediation";
    public const string AppLovin = "applovin";
    public const string BidMachine = "bidmachine";
    public const string BidOn = "bidon";
    public const string BigoAds = "bigo_ads";
    public const string DTExchange = "dt_exchange";
    public const string GAM = "gam";
    public const string Meta = "facebook";
    public const string MIntegral = "mintegral";
    public const string MRAID = "mraid";
    public const string MyTarget = "my_target";
    public const string Notsy = "notsy";
    public const string InMobi = "inmobi";
    public const string Ironsource = "ironsource";
    public const string UnityAds = "unity_ads";
    public const string Vast = "vast";
    public const string Vungle = "vungle";
    public const string Yandex = "yandex";
}

public static class Appodeal
{
    private static bool initialized, unavailable;

    private static GodotObject singleton;
    private static Node signals;
    // TODO: Cache stringNames
    public static bool Init(SceneTree tree, Node signalsNode)
    {
        if (initialized) return true;
        if (unavailable) return false;

        singleton = tree.Root.GetNode("Appodeal");
        signals = signalsNode;

        if (singleton == null)
        {
            GD.PushError("Appodeal not found in engine!");
            unavailable = true;
            return false;
        }

        try
        {
            if (!signals.IsConnected("initialization_finished", Callable.From<string>(OnInitializationFinished)))
                signals.Connect("initialization_finished", Callable.From<string>(OnInitializationFinished));
            if (!signals.IsConnected("ad_revenue_received", Callable.From<Dictionary>(OnAdRevenueReceived)))
                signals.Connect("ad_revenue_received", Callable.From<Dictionary>(OnAdRevenueReceived));
            if (!signals.IsConnected("inapp_purchase_validation_succeded", Callable.From<string>(OnInappPurchaseValidationSuccess)))
                signals.Connect("inapp_purchase_validation_succeded", Callable.From<string>(OnInappPurchaseValidationSuccess));
            if (!signals.IsConnected("inapp_purchase_validation_failed", Callable.From<string>(OnInappPurchaseValidationFail)))
                signals.Connect("inapp_purchase_validation_failed", Callable.From<string>(OnInappPurchaseValidationFail));
            if (!signals.IsConnected("mrec_loaded", Callable.From<bool>(OnMRECLoaded)))
                signals.Connect("mrec_loaded", Callable.From<bool>(OnMRECLoaded));
            if (!signals.IsConnected("mrec_failed_to_load", Callable.From(OnMRECFailedToLoad)))
                signals.Connect("mrec_failed_to_load", Callable.From(OnMRECFailedToLoad));
            if (!signals.IsConnected("mrec_shown", Callable.From(OnMRECShown)))
                signals.Connect("mrec_shown", Callable.From(OnMRECShown));
            if (!signals.IsConnected("mrec_show_failed", Callable.From(OnMRECShowFailed)))
                signals.Connect("mrec_show_failed", Callable.From(OnMRECShowFailed));
            if (!signals.IsConnected("mrec_clicked", Callable.From(OnMRECClicked)))
                signals.Connect("mrec_clicked", Callable.From(OnMRECClicked));
            if (!signals.IsConnected("mrec_expired", Callable.From(OnMRECExpired)))
                signals.Connect("mrec_expired", Callable.From(OnMRECExpired));
            if (!signals.IsConnected("banner_loaded", Callable.From<int, bool>(OnBannerLoaded)))
                signals.Connect("banner_loaded", Callable.From<int, bool>(OnBannerLoaded));
            if (!signals.IsConnected("banner_failed_to_load", Callable.From(OnBannerFailedToLoad)))
                signals.Connect("banner_failed_to_load", Callable.From(OnBannerFailedToLoad));
            if (!signals.IsConnected("banner_shown", Callable.From(OnBannerShown)))
                signals.Connect("banner_shown", Callable.From(OnBannerShown));
            if (!signals.IsConnected("banner_show_failed", Callable.From(OnBannerShowFailed)))
                signals.Connect("banner_show_failed", Callable.From(OnBannerShowFailed));
            if (!signals.IsConnected("banner_clicked", Callable.From(OnBannerClicked)))
                signals.Connect("banner_clicked", Callable.From(OnBannerClicked));
            if (!signals.IsConnected("banner_expired", Callable.From(OnBannerExpired)))
                signals.Connect("banner_expired", Callable.From(OnBannerExpired));
            if (!signals.IsConnected("interstitial_loaded", Callable.From<bool>(OnInterstitialLoaded)))
                signals.Connect("interstitial_loaded", Callable.From<bool>(OnInterstitialLoaded));
            if (!signals.IsConnected("interstitial_failed_to_load", Callable.From(OnInterstitialFailedToLoad)))
                signals.Connect("interstitial_failed_to_load", Callable.From(OnInterstitialFailedToLoad));
            if (!signals.IsConnected("interstitial_shown", Callable.From(OnInterstitialShown)))
                signals.Connect("interstitial_shown", Callable.From(OnInterstitialShown));
            if (!signals.IsConnected("interstitial_show_failed", Callable.From(OnInterstitialShowFailed)))
                signals.Connect("interstitial_show_failed", Callable.From(OnInterstitialShowFailed));
            if (!signals.IsConnected("interstitial_clicked", Callable.From(OnInterstitialClicked)))
                signals.Connect("interstitial_clicked", Callable.From(OnInterstitialClicked));
            if (!signals.IsConnected("interstitial_closed", Callable.From(OnInterstitialClosed)))
                signals.Connect("interstitial_closed", Callable.From(OnInterstitialClosed));
            if (!signals.IsConnected("interstitial_expired", Callable.From(OnInterstitialExpired)))
                signals.Connect("interstitial_expired", Callable.From(OnInterstitialExpired));
            if (!signals.IsConnected("rewarded_video_loaded", Callable.From<bool>(OnRewardedVideoLoaded)))
                signals.Connect("rewarded_video_loaded", Callable.From<bool>(OnRewardedVideoLoaded));
            if (!signals.IsConnected("rewarded_video_failed_to_load", Callable.From(OnRewardedVideoFailedToLoad)))
                signals.Connect("rewarded_video_failed_to_load", Callable.From(OnRewardedVideoFailedToLoad));
            if (!signals.IsConnected("rewarded_video_shown", Callable.From(OnRewardedVideoShown)))
                signals.Connect("rewarded_video_shown", Callable.From(OnRewardedVideoShown));
            if (!signals.IsConnected("rewarded_video_show_failed", Callable.From(OnRewardedVideoShowFailed)))
                signals.Connect("rewarded_video_show_failed", Callable.From(OnRewardedVideoShowFailed));
            if (!signals.IsConnected("rewarded_video_clicked", Callable.From(OnRewardedVideoClicked)))
                signals.Connect("rewarded_video_clicked", Callable.From(OnRewardedVideoClicked));
            if (!signals.IsConnected("rewarded_video_finished", Callable.From<string, string>(OnRewardedVideoFinished)))
                signals.Connect("rewarded_video_finished", Callable.From<string, string>(OnRewardedVideoFinished));
            if (!signals.IsConnected("rewarded_video_closed", Callable.From<bool>(OnRewardedVideoClosed)))
                signals.Connect("rewarded_video_closed", Callable.From<bool>(OnRewardedVideoClosed));
            if (!signals.IsConnected("rewarded_video_expired", Callable.From(OnRewardedVideoExpired)))
                signals.Connect("rewarded_video_expired", Callable.From(OnRewardedVideoExpired));

            GD.Print("Appodeal initialized.");
        }
        catch (Exception)
        {
            return false;
        }

        initialized = true;
        return initialized;
    }

    private static void OnInitializationFinished(string errors) => InitializationFinished?.Invoke(errors);
    public static event Action<string> InitializationFinished;

    private static void OnAdRevenueReceived(Dictionary @params) => AdRevenueReceived?.Invoke(@params);
    public static event Action<Dictionary> AdRevenueReceived;

    private static void OnInappPurchaseValidationSuccess(string json) => InappPurchaseValidationSuccess?.Invoke(json);
    public static event Action<string> InappPurchaseValidationSuccess;

    private static void OnInappPurchaseValidationFail(string reason) => InappPurchaseValidationFail?.Invoke(reason);
    public static event Action<string> InappPurchaseValidationFail;

    private static void OnMRECLoaded(bool isPrecache) => MRECLoaded?.Invoke(isPrecache);
    public static event Action<bool> MRECLoaded;

    private static void OnMRECFailedToLoad() => MRECFailedToLoad?.Invoke();
    public static event Action MRECFailedToLoad;

    private static void OnMRECShown() => MRECShown?.Invoke();
    public static event Action MRECShown;

    private static void OnMRECShowFailed() => MRECShowFailed?.Invoke();
    public static event Action MRECShowFailed;

    private static void OnMRECClicked() => MRECClicked?.Invoke();
    public static event Action MRECClicked;

    private static void OnMRECExpired() => MRECExpired?.Invoke();
    public static event Action MRECExpired;

    private static void OnBannerLoaded(int height, bool isPrecache) => BannerLoaded?.Invoke(height, isPrecache);
    public static event Action<int, bool> BannerLoaded;

    private static void OnBannerFailedToLoad() => BannerFailedToLoad?.Invoke();
    public static event Action BannerFailedToLoad;

    private static void OnBannerShown() => BannerShown?.Invoke();
    public static event Action BannerShown;

    private static void OnBannerShowFailed() => BannerShowFailed?.Invoke();
    public static event Action BannerShowFailed;

    private static void OnBannerClicked() => BannerClicked?.Invoke();
    public static event Action BannerClicked;

    private static void OnBannerExpired() => BannerExpired?.Invoke();
    public static event Action BannerExpired;

    private static void OnInterstitialLoaded(bool isPrecache) => InterstitialLoaded?.Invoke(isPrecache);
    public static event Action<bool> InterstitialLoaded;

    private static void OnInterstitialFailedToLoad() => InterstitialFailedToLoad?.Invoke();
    public static event Action InterstitialFailedToLoad;

    private static void OnInterstitialShown() => InterstitialShown?.Invoke();
    public static event Action InterstitialShown;

    private static void OnInterstitialShowFailed() => InterstitialShowFailed?.Invoke();
    public static event Action InterstitialShowFailed;

    private static void OnInterstitialClicked() => InterstitialClicked?.Invoke();
    public static event Action InterstitialClicked;

    private static void OnInterstitialClosed() => InterstitialClosed?.Invoke();
    public static event Action InterstitialClosed;

    private static void OnInterstitialExpired() => InterstitialExpired?.Invoke();
    public static event Action InterstitialExpired;

    private static void OnRewardedVideoLoaded(bool isPrecache) => RewardedVideoLoaded?.Invoke(isPrecache);
    public static event Action<bool> RewardedVideoLoaded;

    private static void OnRewardedVideoFailedToLoad() => RewardedVideoFailedToLoad?.Invoke();
    public static event Action RewardedVideoFailedToLoad;

    private static void OnRewardedVideoShown() => RewardedVideoShown?.Invoke();
    public static event Action RewardedVideoShown;

    private static void OnRewardedVideoShowFailed() => RewardedVideoShowFailed?.Invoke();
    public static event Action RewardedVideoShowFailed;

    private static void OnRewardedVideoClicked() => RewardedVideoClicked?.Invoke();
    public static event Action RewardedVideoClicked;

    private static void OnRewardedVideoFinished(string amount, string name) => RewardedVideoFinished?.Invoke(amount, name);
    public static event Action<string, string> RewardedVideoFinished;

    private static void OnRewardedVideoClosed(bool finished) => RewardedVideoClosed?.Invoke(finished);
    public static event Action<bool> RewardedVideoClosed;

    private static void OnRewardedVideoExpired() => RewardedVideoExpired?.Invoke();
    public static event Action RewardedVideoExpired;

    public static void Initialize(string appKey, AppodealAdType adTypes) => singleton.Call("initialize", appKey, (int)adTypes);
    public static bool IsInitialized(AppodealAdType adType) => singleton.Call("is_initialized", (int)adType).AsBool();
    public static bool IsAutoCacheEnabled(AppodealAdType adType) => singleton.Call("is_auto_cache_enabled", (int)adType).AsBool();
    public static void Cache(AppodealAdType adTypes) => singleton.Call("cache", (int)adTypes);
    public static bool Show(ShowStyle showStyle) => singleton.Call("show", (int)showStyle).AsBool();
    public static bool ShowForPlacement(ShowStyle showStyle, string placement) => singleton.Call("show_for_placement", (int)showStyle, placement).AsBool();
    public static bool ShowBannerView(int xAxis, int yAxis, string placement) => singleton.Call("show_banner_view", xAxis, yAxis, placement).AsBool();
    public static bool ShowMRECView(int xAxis, int yAxis, string placement) => singleton.Call("show_m_r_e_c_view", xAxis, yAxis, placement).AsBool();
    public static void HideBanner() => singleton.Call("hide_banner");
    public static void HideBannerView() => singleton.Call("hide_banner_view");
    public static void HideMRECView() => singleton.Call("hide_m_r_e_c_view");
    public static void SetAutoCache(AppodealAdType adTypes, bool autoCache) => singleton.Call("set_auto_cache", (int)adTypes, autoCache);
    public static bool IsLoaded(AppodealAdType adTypes) => singleton.Call("is_loaded", (int)adTypes).AsBool();
    public static bool IsPrecache(AppodealAdType adType) => singleton.Call("is_precache", (int)adType).AsBool();
    public static void SetSmartBanners(bool enabled) => singleton.Call("set_smart_banners", enabled);
    public static bool IsSmartBannersEnabled() => singleton.Call("is_smart_banners_enabled").AsBool();
    public static void Set728x90Banners(bool enabled) => singleton.Call("set_728x90_banners", enabled);
    public static void SetBannerAnimation(bool animate) => singleton.Call("set_banner_animation", animate);
    public static void SetBannerRotation(int leftBannerRotation, int rightBannerRotation) => singleton.Call("set_banner_roatation", leftBannerRotation, rightBannerRotation);
    public static void SetUseSafeArea(bool useSafeArea) => singleton.Call("set_use_safe_area", useSafeArea);
    public static void TrackInappPurchase(float amount, string currency) => singleton.Call("track_inapp_purchase", amount, currency);
    public static Godot.Collections.Array GetNetworks(int adType) => singleton.Call("get_networks", adType).AsGodotArray();
    public static void DisableNetwork(string network) => singleton.Call("disable_network", network);
    public static void DisableNetworkForAdTypes(string network, AppodealAdType adTypes) => singleton.Call("disable_network_for_ad_types", network, (int)adTypes);
    public static void SetUserId(string userId) => singleton.Call("set_user_id", userId);
    public static string GetUserId() => singleton.Call("get_user_id").AsString();
    public static string GetVersion() => singleton.Call("get_version").AsString();
    public static string GetPluginVersion() => singleton.Call("get_plugin_version").AsString();
    public static long GetSegmentId() => singleton.Call("get_segment_id").AsInt64();
    public static void SetTesting(bool testMode) => singleton.Call("set_testing", testMode);
    public static void SetLogLevel(LogLevel logLevel) => singleton.Call("set_log_level", (int)logLevel);
    public static void SetCustomFilterBool(string name, bool value) => singleton.Call("set_custom_filter_bool", name, value);
    public static void SetCustomFilterInt(string name, int value) => singleton.Call("set_custom_filter_int", name, value);
    public static void SetCustomFilterFloat(string name, float value) => singleton.Call("set_custom_filter_float", name, value);
    public static void SetCustomFilterString(string name, string value) => singleton.Call("set_custom_filter_string", name, value);
    public static void ResetCustomFilter(string name) => singleton.Call("reset_custom_filter", name);
    public static bool CanShowForDefaultPlacement(AppodealAdType adType) => singleton.Call("can_show_for_default_placement", (int)adType).AsBool();
    public static bool CanShowForPlacement(AppodealAdType adType, string placementName) => singleton.Call("can_show_for_placement", (int)adType, placementName).AsBool();
    public static double GetRewardAmount(string placementName) => singleton.Call("get_reward_amount", placementName).AsDouble();
    public static string GetRewardCurrency(string placementName) => singleton.Call("get_reward_currency", placementName).AsString();
    public static void MuteVideosIfCallsMuted(bool isMuted) => singleton.Call("mute_videos_if_calls_muted", isMuted);
    public static void StartTestActivity() => singleton.Call("start_test_activity");
    public static void SetChildDirectedTreatment(bool value) => singleton.Call("set_child_directed_treatment", value);
    public static void Destroy(AppodealAdType adTypes) => singleton.Call("destroy", (int)adTypes);
    public static void SetExtraDataBool(string key, bool value) => singleton.Call("set_extra_data_bool", key, value);
    public static void SetExtraDataInt(string key, int value) => singleton.Call("set_extra_data_int", key, value);
    public static void SetExtraDataFloat(string key, float value) => singleton.Call("set_extra_data_float", key, value);
    public static void SetExtraDataString(string key, string value) => singleton.Call("set_extra_data_string", key, value);
    public static void ResetExtraData(string key) => singleton.Call("reset_extra_data", key);
    public static double GetPredictedEcpm(int adType) => singleton.Call("get_predicted_ecpm", adType).AsDouble();
    public static void LogEvent(string eventName, Dictionary @params) => singleton.Call("log_event", eventName, @params);
    public static void ValidatePlayStoreInappPurchase(Dictionary payload) => singleton.Call("validate_play_store_inapp_purchase", payload);
    public static void ValidateAppStoreInappPurchase(Dictionary payload) => singleton.Call("validate_app_store_inapp_purchase", payload);
}
