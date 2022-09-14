using System.Collections;
using System.Collections.Generic;
using SweetSugar.Scripts.GUI.Boost;
using UnityEngine;

public class BoostersManager : MonoBehaviour
{
    public Transform boosterContainer;
    public List<BoostType> selectedBoosters = new List<BoostType>();
    public GameObject btnContinue;

    public static BoostersManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        btnContinue.SetActive(false);
        for (int i = 0; i < boosterContainer.childCount; i++)
            boosterContainer.GetChild(i).GetComponent<BoostIcon>().Check(false);
    }

    public void SelectFirstBooster()
    {
        BoostIcon boostIcon = boosterContainer.GetChild(0).GetComponent<BoostIcon>();

        if (selectedBoosters.Count > 0)
            return;

        selectedBoosters.Add(boostIcon.type);
        boostIcon.Check(true);
        btnContinue.SetActive(true);
    }

    public void SelectBooster(GameObject booster)
    {
        BoostIcon boostIcon = booster.GetComponent<BoostIcon>();
        if (selectedBoosters.IndexOf(boostIcon.type) < 0)
        {
            selectedBoosters.Add(boostIcon.type);
            boostIcon.Check(true);
        }
        else
        {
            selectedBoosters.Remove(boostIcon.type);
            boostIcon.Check(false);

        }

        if (selectedBoosters.Count > 0) btnContinue.SetActive(true);
        else btnContinue.SetActive(false);
    }


}
