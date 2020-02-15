
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    //PlatformID
    private string playStoreID = "";
    private string appStoreID = "";

    private string interstitialAd = "video";
    private string rewardedVideoAd = "rewardedVideo";

    [Header("Ads Setting for Platform")]
    [Tooltip("Clicking any of these to active that platform")]
    public bool PLAYSTORE = false;
    public bool APPSTORE = false;
    public bool isTestMode;


    private void Start()
    {
        InitAdSystem();
    }

    private void InitAdSystem()
    {
        if (APPSTORE)
        {
            Advertisement.Initialize(appStoreID, isTestMode);

        }
        else if (PLAYSTORE)
        {
            Advertisement.Initialize(playStoreID, isTestMode);
        }
        else
        {
            return;
        }


    }
    public void PlayVideoAdvertisement()
    {
        if (!Advertisement.IsReady(interstitialAd))
        {
            return;
        }

        Advertisement.Show(interstitialAd);
    }
    public void PlayRewaredVideoAdvertisement()
    {
        if (!Advertisement.IsReady(rewardedVideoAd))
        {
            return;
        }

        Advertisement.Show(rewardedVideoAd);
    }

}
