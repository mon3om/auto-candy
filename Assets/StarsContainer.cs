using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsContainer : MonoBehaviour
{
    public Sprite coloredStar, nonColoredStar;
    private Image[] childrenImages;
    // Start is called before the first frame update
    void Start()
    {
        childrenImages = transform.GetComponentsInChildren<Image>();
    }

    public void ColorateStars(int endIndex)
    {
        for (int i = 0; i <= endIndex; i++)
            childrenImages[i].sprite = coloredStar;
    }

    public void UncolorStars()
    {
        foreach (var im in childrenImages)
            im.sprite = nonColoredStar;
    }

    public void OpenRatingPage()
    {
        Application.OpenURL("https://google.com");
    }
}
