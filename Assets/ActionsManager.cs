using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsManager : MonoBehaviour
{

    public static ActionsManager Instance;
    public delegate void EndEvent();
    public EndEvent endEvent;

    private void Awake()
    {
        Instance = this;
    }

    public void SelectVideo()
    {
        VideoManager.Instance.Activate(endEvent);
        gameObject.SetActive(false);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
