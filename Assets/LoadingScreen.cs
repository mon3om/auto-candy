using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public TMP_Text tmp_loading, tmp_quote, tmp_quote_writer;
    public float blinkingSpeed = 0.5f;
    [Space]
    [Header("Quotes")]
    public Quote[] quotes;

    private string blinkingDots = "";

    // Start is called before the first frame update
    void Start()
    {
        int i = Random.Range(0, quotes.Length);
        tmp_quote.text = quotes[i].quote;
        tmp_quote_writer.text = quotes[i].writer;

        StartCoroutine(BlinkCorou());
    }

    private IEnumerator BlinkCorou()
    {
        blinkingDots += ".";
        if (blinkingDots.Length > 2)
            blinkingDots = "";

        tmp_loading.text = "Loading" + blinkingDots;
        yield return new WaitForSeconds(blinkingSpeed);
        StartCoroutine(BlinkCorou());
    }
}

[System.Serializable]
public class Quote
{
    public string quote;
    public string writer;
}
