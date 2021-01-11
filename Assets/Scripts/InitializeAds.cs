using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAds : MonoBehaviour
{
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
    }
}
