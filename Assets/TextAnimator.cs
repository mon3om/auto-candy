using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAnimator : MonoBehaviour
{
    public float blinkingSpeed = 0.5f;
    private TMP_Text text;
    private string blinkingDots = "";

    void Start()
    {
        text = GetComponent<TMP_Text>();
        StartCoroutine(BlinkCorou());
    }


    private IEnumerator BlinkCorou()
    {
        blinkingDots += ".";
        if (blinkingDots.Length > 2)
            blinkingDots = "";

        text.text = "Loading" + blinkingDots;
        yield return new WaitForSeconds(blinkingSpeed);
        StartCoroutine(BlinkCorou());
    }
}
