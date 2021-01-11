using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetCharacterContent : MonoBehaviour
{
    private CharacterManager cm;

    private GameObject currentImage;

    public GameObject sampleImage;
    public GameObject lockImage;

    public bool useLockImages;

    public bool includeDefaultBall = true;

    private void Awake()
    {
        cm = GameObject.FindWithTag("CharacterManager").GetComponent<CharacterManager>();

        if (includeDefaultBall)
        {
            for (int i = 0; i < cm.characterImages.Length; i++)
            {
                currentImage = Instantiate(sampleImage, Vector3.zero + Vector3.right * 640f * i, Quaternion.identity, transform);
                currentImage.GetComponent<Image>().sprite = cm.characterImages[i];

                if (cm.isLocked[i] && useLockImages)
                    Instantiate(lockImage, currentImage.transform);
            }
        }
        else
        {
            for (int i = 1; i < cm.characterImages.Length; i++)
            {
                currentImage = Instantiate(sampleImage, Vector3.zero + Vector3.right * 640f * i, Quaternion.identity, transform);
                currentImage.GetComponent<Image>().sprite = cm.characterImages[i];

                if (cm.isLocked[i] && useLockImages)
                    Instantiate(lockImage, currentImage.transform);
            }
        }
    }
}
