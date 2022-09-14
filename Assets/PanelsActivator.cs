using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SweetSugar.Scripts.Core;
using SweetSugar.Scripts.MapScripts;
using SweetSugar.Scripts.GUI;

public class PanelsActivator : MonoBehaviour
{
    public List<int> earnLevels = new List<int>();
    public AnimationEventManager animationEventManager;


    private void Start()
    {
        if (Startup.FreePlay)
        {
            int level = Random.Range(2, 100);
            PlayerPrefs.SetInt(Startup.GetLevelKey(level), 3);
            LevelsMap.OnLevelSelected(level);
            return;
        }

        if (PlayerPrefs.GetInt("FirstTime", 0) == 0)
        {
            StartCoroutine(StartGameCorou());
        }
        else
        {
            int level = GetRandomLevel(earnLevels);
            LevelsMap.OnLevelSelected(level);
        }

    }

    private int GetRandomLevel(List<int> availableLevels, List<int> excludes = null)
    {
        int level = availableLevels[Random.Range(0, availableLevels.Count)];
        return level;

        // if (excludes != null)
        // {

        // }
    }

    private IEnumerator StartGameCorou()
    {
        yield return new WaitForSeconds(0.01f);
        LevelsMap.OnLevelSelected(1);
        animationEventManager.Play();
    }
}
