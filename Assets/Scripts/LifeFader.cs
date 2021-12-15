using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class LifeFader : MonoBehaviour
{
    Image lifeImg;
    [SerializeField]
    Sprite fullLife;
    [SerializeField]
    Sprite mediumLife;
    [SerializeField]
    Sprite lowLife;
    // Start is called before the first frame update
    int life = 0;
    float fadeSpeed = 0.75f;
    float minFade = 0.8f;
    Tween activeTween;
    private void Awake()
    {
        fadeSpeed = 0.75f;
        minFade = 0.8f;
        lifeImg = GetComponent<Image>();
        life = PlayerPrefs.GetInt("Life", 3);
        switch (life)
        {
            case 3: lifeImg.sprite = fullLife; break;
            case 2: lifeImg.sprite = mediumLife; break;
            case 1: lifeImg.sprite = lowLife; break;
            case 0: life = 3; break;
        }
    }
    void Start()
    {
        FadeLife();
    }
    public void FadeLife()
    {
        lifeImg.DOFade(minFade, fadeSpeed).OnComplete(UnFadeLife);
    }
    public void UnFadeLife()
    {
        lifeImg.DOFade(1f, fadeSpeed).OnComplete(FadeLife);
    }
    public void LifeLost()
    {
        //Shatter Life
    }
}
