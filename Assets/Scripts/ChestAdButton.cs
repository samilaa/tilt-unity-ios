using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class ChestAdButton : MonoBehaviour, IUnityAdsListener
{

#if UNITY_IOS
    private string gameId = "1486551";
#elif UNITY_ANDROID
    private string gameId = "1486550";
#endif

    Button myButton;
    public string myPlacementId = "rewardedVideo";

    public int mode; // 0 is double level coins

    private int adsWatched;

    void Start()
    {
        adsWatched = 0;

        myButton = GetComponent<Button>();

        // Set interactivity to be dependent on the Placement’s status:
        myButton.interactable = Advertisement.IsReady(myPlacementId);

        // Map the ShowRewardedVideo function to the button’s click listener:
        if (myButton) myButton.onClick.AddListener(ShowRewardedVideo);

        // Initialize the Ads listener and service:

        Advertisement.AddListener(this);

        if (!Advertisement.isInitialized)
        {
            Advertisement.Initialize(gameId, true);
        }
    }

    // Implement a function for showing a rewarded video ad:
    void ShowRewardedVideo()
    {
        Advertisement.Show(myPlacementId);

        Debug.Log("Show video ad");
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == myPlacementId)
        {
            if (adsWatched < 1)
                myButton.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
            Wallet.Transaction (100);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
            Debug.Log("Ad skipped!");
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("Ad did not finish due to an error");
        }

        myButton.interactable = false;
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
        Debug.LogWarning("Ad did not finish due to an error");
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.

        adsWatched += 1;
    }

    public void OnDestroy()
    {
        Debug.Log("DestroyAdController");
        myButton.onClick.RemoveListener(ShowRewardedVideo);
        Advertisement.RemoveListener(this);
    }
}