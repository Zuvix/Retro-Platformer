using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField]
    private TMP_Text scoreTxt;
    [SerializeField]
    private TMP_Text addTxt;
    int addition;
    int totalScore;
    float timePassed = 0f;
    [SerializeField]
    float maxTime=3f;
    [SerializeField]
    CanvasGroup addTxtGroup;
    [SerializeField]
    ScorePopup scorePopup;
    public void AddScore(int score, Vector2 position) 
    {
        addition += score;
        timePassed = 0;
        addTxtGroup.alpha = 0.9f;
        addTxt.text = "+" + addition;
        ScorePopup popup =Instantiate(scorePopup);
        popup.SetPopup(score, position);
        AddScoreToTotal(score);
    }
    public void AddScoreToTotal(int score)
    {
        totalScore += score;
        scoreTxt.text= totalScore.ToString("00000");
    }
    public void ResetAddition()
    {
        addition = 0;
    }
    private void Update()
    {
        if (addition > 0)
        {
            timePassed += Time.deltaTime;
            addTxtGroup.alpha = Mathf.Clamp(0.9f - (timePassed / maxTime),0f,1f);
        }
        if (timePassed >= maxTime)
        {
            timePassed = 0f;
            ResetAddition();
        }
    }

}
