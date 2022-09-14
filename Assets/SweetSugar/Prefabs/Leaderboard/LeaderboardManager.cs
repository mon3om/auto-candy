using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    private User[] users;
    public GameObject usersContainer;
    public GameObject userRow;

    public GameObject closeButton;
    [Space]
    public GameObject content;
    public GameObject loading;
    [Space]
    [Header("Continue Button")]
    public Button continueButton;

    public static LeaderboardManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        if (transform.GetChild(0).gameObject.activeSelf)
            StartCoroutine(NetworkingNew.GetAllUsers(FillUsers));
    }

    private void FillUsers(User[] _users)
    {
        foreach (Transform child in usersContainer.transform)
            Destroy(child.gameObject);

        foreach (User user in _users)
        {
            if (PlayerPrefs.GetString("office_name") != user.office_name)
            {

                GameObject row = Instantiate(userRow, usersContainer.transform);
                row.transform.GetChild(0).Find("TMP_Rank").GetComponent<TMP_Text>().text = user.rank;
                row.transform.GetChild(1).GetComponent<TMP_Text>().text = user.office_name;
                row.transform.GetChild(2).GetComponent<TMP_Text>().text = user.score == "" ? "0" : user.score;

                row.transform.GetChild(0).Find("Indicator").gameObject.SetActive(false);
            }
            else
            {
                GameObject row = Instantiate(userRow, usersContainer.transform);
                row.transform.GetChild(0).Find("TMP_Rank").GetComponent<TMP_Text>().text = user.rank;
                row.transform.GetChild(1).GetComponent<TMP_Text>().text = user.office_name;
                row.transform.GetChild(2).GetComponent<TMP_Text>().text = user.score == "" ? "0" : user.score;

                Color color = new Color(78 / 255f, 175 / 255f, 50 / 255f, 1);

                row.transform.GetChild(0).Find("TMP_Rank").GetComponent<TMP_Text>().color = color;
                row.transform.GetChild(1).GetComponent<TMP_Text>().color = color;
                row.transform.GetChild(2).GetComponent<TMP_Text>().color = color;

                row.transform.GetChild(0).Find("Indicator").gameObject.SetActive(true);
                row.transform.GetChild(0).Find("Indicator").GetComponent<Image>().color = color;

                row.transform.SetSiblingIndex(0);
            }
        }

        content.SetActive(true);
        loading.SetActive(false);
    }

    public void ShowLeaderboard(NextButtonCallback callback = null)
    {
        GameObject childGO = transform.GetChild(0).gameObject;
        childGO.SetActive(!childGO.activeSelf);
        if (childGO.activeSelf)
        {
            content.SetActive(false);
            loading.SetActive(true);
            StartCoroutine(NetworkingNew.GetAllUsers(FillUsers));
        }

        closeButton.SetActive(true);
        continueButton.gameObject.SetActive(false);

        if (callback != null)
        {
            continueButton.gameObject.SetActive(true);
            closeButton.SetActive(false);
            continueButton.onClick.AddListener(() => callback());
        }
    }

    public delegate void NextButtonCallback();

    public void HideLeaderboard() => gameObject.SetActive(false);


    public void ShowHideCloseButton(bool show)
    {
        closeButton.gameObject.SetActive(show);
    }
}
