using System.Collections;
using System.Linq;
using SweetSugar.Scripts.Core;
using SweetSugar.Scripts.Level;
using SweetSugar.Scripts.TargetScripts.TargetSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SweetSugar.Scripts.GUI
{
    /// <summary>
    /// various GUi counters
    /// </summary>
    public class Counter_ : MonoBehaviour
    {
        TextMeshProUGUI txt;
        private float lastTime;
        bool alert;

        // Use this for initialization
        void Awake()
        {
            // txt = GetComponent<Text>();
            txt = GetComponent<TextMeshProUGUI>();
        }

        void OnEnable()
        {
            lastTime = 0;
            UpdateText();
            alert = false;
            StartCoroutine(UpdateRare());
            if (name == "Limit") StartCoroutine(TimeTick());
        }

        // Update is called once per frame
        IEnumerator UpdateRare()
        {
            while (true)
            {
                if (txt == null) continue;
                // Debug.Break();
                UpdateText();
                yield return new WaitForSeconds(0.5f);
            }
        }

        private void UpdateText()
        {
            if (name == "Score")
            {
                txt.text = "" + LevelManager.Score;
            }

            if (name == "BestScore")
            {
                txt.text = "Best score:" + PlayerPrefs.GetInt("Score" + PlayerPrefs.GetInt("OpenLevel"));
            }


            if (name == "Limit" && LevelManager.THIS.levelData != null)
            {

                if (LevelManager.THIS.levelData.limitType == LIMIT.MOVES)
                {
                    txt.text = "" + Mathf.Clamp(LevelManager.THIS.levelData.limit, 0, LevelManager.THIS.levelData.limit);
                    txt.transform.localScale = Vector3.one;
                    if (LevelManager.THIS.levelData.limit <= 5)
                    {
                        // txt.color = new Color(255f / 255f, 132f / 255, 222f / 255);
                        // txt.outlineColor = Color.white;
                        if (!alert)
                        {
                            alert = true;
                            //                            SoundBase.Instance.PlayOneShot(SoundBase.Instance.alert);
                        }
                    }
                    else
                    {
                        alert = false;
                        txt.color = Color.white;
                        // txt.GetComponent<Outline>().effectColor = new Color(148f / 255f, 61f / 255f, 95f / 255f);
                    }
                }
                else
                {
                    var minutes = Mathf.FloorToInt(LevelManager.THIS.levelData.limit / 60F);
                    var seconds = Mathf.FloorToInt(LevelManager.THIS.levelData.limit - minutes * 60);
                    txt.text = "" + $"{minutes:00}:{seconds:00}";
                    txt.transform.localScale = Vector3.one * 0.68f;
                    txt.fontSize = 80;
                    if (LevelManager.THIS.levelData.limit <= 5 && LevelManager.THIS.gameStatus == GameState.Playing)
                    {
                        // txt.color = new Color(216f / 255f, 0, 0);
                        // txt.outlineColor = Color.white;
                        if (lastTime + 5 < Time.time)
                        {
                            lastTime = Time.time;
                            SoundBase.Instance.PlayOneShot(SoundBase.Instance.timeOut);
                        }
                    }
                    else
                    {
                        txt.color = Color.white;
                        txt.outlineColor = new Color(148f / 255f, 61f / 255f, 95f / 255f);
                    }
                }
            }

            if (name == "Lifes")
            {
                txt.text = "" + InitScript.Instance?.GetLife();
            }

            if (name == "FailedCount")
            {
                if (LevelManager.THIS.levelData.limitType == LIMIT.MOVES)
                    txt.text = "+" + LevelManager.THIS.ExtraFailedMoves;
                else
                    txt.text = "+" + LevelManager.THIS.ExtraFailedSecs;
            }

            if (name == "FailedPrice")
            {
                txt.text = "" + LevelManager.THIS.FailedCost;
            }

            if (name == "FailedDescription")
            {
                txt.text = "" + LevelData.THIS.GetTargetCounters().First(i => !i.IsTotalTargetReached()).targetLevel.GetFailedDescription();
            }


            if (name == "Gems")
            {
                txt.text = "" + InitScript.Gems;
            }

            if (name == "TargetScore")
            {
                txt.text = "" + LevelManager.THIS.levelData.star1;
            }

            if (name == "Level")
            {
                txt.text = "" + PlayerPrefs.GetInt("OpenLevel");
            }

            // if (name == "TargetDescription1")
            // {
            //     txt.text = "" + LevelData.THIS.GetTargetContainersForUI().First().targetLevel.GetDescription();
            // }
        }

        IEnumerator TimeTick()
        {
            while (true)
            {
                if (LevelManager.THIS.gameStatus == GameState.Playing)
                {
                    if (LevelManager.THIS.levelData.limitType == LIMIT.TIME)
                    {
                        LevelManager.THIS.levelData.limit--;
                        if (!LevelManager.THIS.DragBlocked)
                            LevelManager.THIS.CheckWinLose();
                    }
                }
                if (LevelManager.THIS.gameStatus == GameState.Map)
                    yield break;
                yield return new WaitForSeconds(1);
            }
        }
    }
}
