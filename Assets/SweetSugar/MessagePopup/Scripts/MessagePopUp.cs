using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessagePopUp : MonoBehaviour
{
    public TMP_Text text;
    public Animator animator;

    public static MessagePopUp Instance;

    private void Awake()
    {
        Instance = this;
    }


    public void EnablePopup(string text, float timeOut = 6)
    {
        Debug.Log("anim = " + animator.GetBool("active"));

        this.text.text = text;
        animator.SetBool("active", true);
        StartCoroutine(DisableCoroutine(timeOut));
    }

    public void DisablePopup()
    {
        StopAllCoroutines();
        animator.SetBool("active", false);
    }

    private IEnumerator DisableCoroutine(float timeOut)
    {
        yield return new WaitForSeconds(timeOut);
        DisablePopup();
    }
}
