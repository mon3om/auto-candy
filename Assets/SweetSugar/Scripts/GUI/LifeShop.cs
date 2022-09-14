using SweetSugar.Scripts.Core;
using TMPro;
using UnityEngine;

namespace SweetSugar.Scripts.GUI
{
    /// <summary>
    /// Life shop popup
    /// </summary>
    public class LifeShop : MonoBehaviour
    {
        public int CostIfRefill = 12;
        public GameObject btnVideo, gotExtraReplacement;

        // Use this for initialization
        void OnEnable()
        {
            int gotExtraLife = PlayerPrefs.GetInt("GotExtraLife");
            if (gotExtraLife == 1 && InitScript.Instance.GetLife() == 0)
            {
                btnVideo.SetActive(false);
                gotExtraReplacement.SetActive(true);
            }
            else
            {
                btnVideo.SetActive(true);
                gotExtraReplacement.SetActive(false);
            }

        }

    }
}
