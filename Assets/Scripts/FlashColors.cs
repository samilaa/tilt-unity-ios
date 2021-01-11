using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashColors : MonoBehaviour
{
    public Image img;
    public Text text;

    public Color color1;
    public Color color2;

    public Color textColor1;
    public Color textColor2;

    public float interval;
    public float textInterval;
    private float startInterval;

    private bool colChooser;

    private void Update()
    {
        if (img != null)
        {
            if (startInterval >= interval)
            {
                if (colChooser)
                {
                    img.color = color1;

                    startInterval = 0f;

                    colChooser = false;
                }
                else
                {
                    img.color = color2;

                    startInterval = 0f;

                    colChooser = true;
                }
            }
            else
            {
                startInterval += Time.deltaTime;
            }
        }

        if (text != null)
        {
            if (startInterval >= textInterval)
            {
                if (colChooser)
                {
                    text.color = textColor1;

                    startInterval = 0f;

                    colChooser = false;
                }
                else
                {
                    text.color = textColor2;

                    startInterval = 0f;

                    colChooser = true;
                }
            }
            else
            {
                startInterval += Time.deltaTime;
            }
        }
    }
}
