using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAnimation : MonoBehaviour
{
    public int speed = 2;

    private int score = 4365;
    private int preScore = 0;

    public TMPro.TMP_Text text;

    public void SetScore(int score)
    {
        this.score = score;
    }

    void FixedUpdate()
    {
        if (preScore == score) return;

        // if (preScore + speed / 49 > score)
        //     preScore += score - preScore;
        // else
        //     preScore += speed / 49;

        preScore = (int)Mathf.Lerp(preScore, score, speed * Time.deltaTime);

        text.text = preScore + "";
    }
}
