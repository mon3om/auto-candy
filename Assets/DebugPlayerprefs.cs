using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SweetSugar.Scripts.Core;

public class DebugPlayerprefs : MonoBehaviour
{
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        text.text = "GotExtraLife = " + PlayerPrefs.GetInt("GotExtraLife") + "\nLifes = " + InitScript.Instance.GetLife();
    }
}
