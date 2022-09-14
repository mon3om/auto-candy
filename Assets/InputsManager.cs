using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputsManager : MonoBehaviour
{
    public TMP_InputField office, email, website, instagram;
    private User user;
    public GameObject inputsPage1, inputsPage2, page1NextBtn, page2NextBtn;

    public void SaveInformation()
    {
        user = new User(website.text, office.text, email.text, "0", instagram.text);
        User.SaveUser(user);
        StartCoroutine(NetworkingNew.CreatUser(user, UserCreatedCallback));
    }

    public void NextPage()
    {
        inputsPage1.SetActive(false);
        inputsPage2.SetActive(true);
    }

    public void PreviousPage()
    {
        inputsPage1.SetActive(true);
        inputsPage2.SetActive(false);
    }

    private void Update()
    {
        if (office.text.Length > 3)
            page1NextBtn.SetActive(true);
        else
            page1NextBtn.SetActive(false);

        if (office.text.Length > 0 || email.text.Length > 0 || website.text.Length > 0)
            page2NextBtn.SetActive(true);
        else
            page2NextBtn.SetActive(false);
    }

    private void UserCreatedCallback()
    {
        PlayerPrefs.SetInt("FirstTime", 1);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        MessagePopUp.Instance.EnablePopup("Welcome " + PlayerPrefs.GetString("first_name"));
    }

    //
    // Dropdown and search management
    //

    [Space]
    public CustomDropdown dropDownGO;
    private IEnumerator coroutine = null;
    private bool changedFromScript = false;

    public void OnOfficeNameChange()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        if (changedFromScript)
        {
            changedFromScript = false;
            return;
        }

        if (office.text.Length < 2)
        {
            dropDownGO.ShowHideDropdown(false);
            return;
        }

        coroutine = NetworkingNew.GetSearchResult(office.text, SearchCallback);
        StartCoroutine(coroutine);
    }

    private void SearchCallback(SearchResult[] results)
    {
        List<DropdownItem> dropdowns = new List<DropdownItem>();
        foreach (SearchResult item in results)
        {
            dropdowns.Add(new DropdownItem(item.result, () =>
            {
                changedFromScript = true;
                office.text = item.result;
                dropDownGO.ShowHideDropdown(false);
            }));
        }

        dropDownGO.UpdateList(dropdowns);

        if (results.Length > 0)
            dropDownGO.ShowHideDropdown(true);

        else
            dropDownGO.ShowHideDropdown(false);
    }
}


