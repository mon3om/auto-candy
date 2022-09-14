using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomDropdown : MonoBehaviour
{
    public Transform itemsContainer;
    public GameObject dropdownItem;
    public RectTransform anchorEl;
    public bool asynchrone = false;

    [Space]
    public List<DropdownItem> items;

    [Space]
    [Header("Anchoring")]
    public Vector2 anchorOrigin = Vector2.zero;

    private void Start()
    {

        if (asynchrone)
        {
            ShowHideDropdown(false);
            return;
        }

        // Clear list
        foreach (Transform child in itemsContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in items)
        {
            GameObject instance = Instantiate(dropdownItem, itemsContainer).gameObject;
            instance.transform.Find("Text").GetComponent<TMPro.TMP_Text>().text = item.text;
            if (item.onClickEvent != null)
                instance.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(item.onClickEvent);
            else
                instance.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(item.OnEvent.Invoke);

        }


        // SetAnchorOrigin();
    }

    private void Update()
    {
        // itemsContainer.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1);
        // itemsContainer.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
        // itemsContainer.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
    }

    public void ShowHideDropdown(bool active)
    {
        gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(active);
    }

    public void ShowHideDropdown()
    {
        gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeSelf);
    }

    public void UpdateList(List<DropdownItem> list)
    {
        // Clear list
        foreach (Transform child in itemsContainer)
            Destroy(child.gameObject);

        foreach (var item in list)
        {
            var instance = Instantiate(dropdownItem, itemsContainer);
            instance.transform.Find("Text").GetComponent<TMPro.TMP_Text>().text = item.text;
            instance.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(item.onClickEvent);
        }
    }

    // private void SetAnchorOrigin()
    // {
    //     if (anchorOrigin != Vector2.zero)
    //     {
    //         Vector2 position = (Vector2)anchorEl.position + anchorOrigin * anchorEl.rect.height / 2 + anchorOrigin * GetComponent<RectTransform>().rect.height / 2;

    //         Debug.Log(position);
    //         Debug.Log(anchorEl.rect.height);
    //         Debug.Log(GetComponent<RectTransform>().rect.height);
    //         // Debug.Log(anchorEl.);

    //         GetComponent<RectTransform>().position = anchorEl.position;
    //     }
    // }
}

[System.Serializable]
public class DropdownItem
{
    public string text;
    public delegate void OnClickEvent();
    public OnClickEvent editorEvent;

    public UnityEngine.Events.UnityAction onClickEvent;

    [Serializable]
    public class MyEventType : UnityEvent { }

    public MyEventType OnEvent;


    public DropdownItem(string text, UnityEngine.Events.UnityAction onClickEvent)
    {
        this.text = text;
        this.onClickEvent = onClickEvent;
    }

    public DropdownItem() { }
}
