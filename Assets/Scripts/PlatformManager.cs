using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    private void Awake()
    {
        if (iOSHapticFeedback.Instance.IsSupported())
            iOSHapticFeedback.Instance.enabled = true;
        else
        {
            iOSHapticFeedback.Instance.enabled = false;
        }

        DontDestroyOnLoad(this);
    }
}
