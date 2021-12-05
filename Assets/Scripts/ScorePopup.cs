using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class ScorePopup : MonoBehaviour
{
    [SerializeField]
    TMP_Text popupTxt;
    [SerializeField]
    float maxTime = 1.5f;
    [SerializeField]
    float yOffset = 0.5f;
    public void SetPopup(int score, Vector2 position)
    {
        this.transform.position = position+Vector2.up* yOffset;
        popupTxt.text = "+" + score;
        popupTxt.DOFade(0, maxTime);
        transform.DOMoveY(transform.position.y + 0.5f, maxTime);
        Destroy(this.gameObject, maxTime + 0.2f);
    }

}
