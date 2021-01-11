using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    public Color[] skyboxes;
    public Material[] platforms;
    public Material[] walls;

    public int GetThemeID ()
    {
        int themeCount = skyboxes.Length;
        int level = PlayerPrefs.GetInt("Level", 0);

        if (themeCount != 0)
        {
            while (level >= themeCount)
            {
                level -= (themeCount - 1);
            }

            return level;
        }

        else
        {
            return 0;
        }
    }

    public void LoadTheme (int id)
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("BasicBlock");
        GameObject[] plats = GameObject.FindGameObjectsWithTag("Platform");

        foreach (GameObject block in blocks)
        {
            Renderer blockRenderer = block.GetComponent<Renderer>();
            blockRenderer.material = walls[id];
        }

        foreach (GameObject plat in plats)
        {
            Renderer platRenderer = plat.GetComponent<Renderer>();
            platRenderer.material = platforms[id];
        }

        //Camera.main.backgroundColor = skyboxes[id];
        StartCoroutine(SmoothSkybox(Camera.main.backgroundColor, skyboxes[id], 1f));
    }

    IEnumerator SmoothSkybox (Color col, Color targetCol, float speed)
    {
        float f = 0f;

        Color startCol = col;

        while (f < 1f)
        {
            f += Time.deltaTime * speed;

            col = Color.Lerp(col, targetCol, f);
            Camera.main.backgroundColor = col;

            yield return null;
        }

        col = targetCol;
        Camera.main.backgroundColor = col;
    }
}
