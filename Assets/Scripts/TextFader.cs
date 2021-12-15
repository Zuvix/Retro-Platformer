using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TextFader : MonoBehaviour
{
    [Header("Fading")]
    [SerializeField] bool fadingUsed = false;
    [SerializeField] float minAlpha=0.8f;
    [SerializeField] float speed=0.75f;
    TMP_Text text;
    private void Awake()
    {
        text=GetComponent<TMP_Text>();
    }
    void Start()
    {
        if (fadingUsed)
        {
            FadeOut();
        }
        
    }
    public void FadeIn()
    {
        text.DOFade(1f, speed).OnComplete(FadeOut);
    }
    public void FadeOut()
    {
        text.DOFade(minAlpha, speed).OnComplete(FadeIn);
    }
}
