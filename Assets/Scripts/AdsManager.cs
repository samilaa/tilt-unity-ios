using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    public string placementId = "bannerPlacement";

#if UNITY_IOS
    private string gameId = "3516536";
#elif UNITY_ANDROID
    private string gameId = "3516537";
#endif

    bool testMode = true;

    void Start()
    {
        if (!Advertisement.isInitialized)
            Advertisement.Initialize(gameId, testMode);

        StartCoroutine(ShowBannerWhenReady());

        DontDestroyOnLoad(this);
    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(placementId))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show(placementId);
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
    }

    public void PlayAd ()
    {
        PlayerPrefs.SetInt("PlayCountAds", PlayerPrefs.GetInt("PlayCountAds", 0) + 1);

        if (PlayerPrefs.GetInt("PlayCountAds", 0) >= 20)
        {
            Advertisement.Show();

            PlayerPrefs.SetInt("PlayCountAds", 0);
        }
    }
}
