using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour
{
    public bool openAllLevels = true;
    public static bool FreePlay = true;

    private void Awake()
    {
        // To-Do
        // PlayerPrefs.SetInt("FirstTime", 1);
        // PlayerPrefs.SetString("office_name", "Alberta");
        // for (int i = 0; i < 100; i++)
        //     if (openAllLevels) PlayerPrefs.SetInt(GetLevelKey(i), 3);
        //     else PlayerPrefs.DeleteKey(GetLevelKey(i));
    }



    public static string GetLevelKey(int number)
    {
        return string.Format("Level.{0:000}.StarsCount", number);
    }
}
