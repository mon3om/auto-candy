using System.Collections;
using System.Collections.Generic;
using SweetSugar.Scripts.GUI;
using UnityEngine;

public class MainScreen : MonoBehaviour
{
    public GameObject loading;
    public GUIEvents gUI;
    public static bool DirectFreePlay = false;

    private void Awake()
    {
        if (DirectFreePlay)
        {
            gUI.FreePlay();
            loading.SetActive(true);
            DirectFreePlay = false;
        }
    }
}
