using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

public class SceneLevelManager : Singleton<SceneLevelManager>
{
    
    [SerializeField]
    CameraFollow cameraFollow;
    [SerializeField]
    Image enclosingBcg;
    [SerializeField]
    Image enclosingCircle;
    [SerializeField]
    Image fillBlack;
    [SerializeField]
    UnityEvent gameOver;

    public void PlayerDiedReload()
    {
        cameraFollow.PlayerDied();
        enclosingCircle.gameObject.SetActive(true);
        enclosingBcg.DOFade(1f, 0f);
        enclosingCircle.rectTransform.DOSizeDelta(new Vector2(120f,120f), 1.285f).OnComplete(ScheduleReload);
        SubstractLife();
        gameOver.Invoke();
    }
    public void PlayerDroppedReload()
    {
        enclosingCircle.gameObject.SetActive(true);
        fillBlack.gameObject.SetActive(true);
        fillBlack.DOFade(1f, 1f).OnComplete(ReloadLevel);
        SubstractLife();
        gameOver.Invoke();
    }
    void SubstractLife()
    {
        int life = PlayerPrefs.GetInt("Life", 3);
        life--;
        PlayerPrefs.SetInt("Life", life);
    }
    public void ScheduleReload()
    {

        Invoke("ReloadLevel", 0.25f);
    }
    public void ReloadLevel()
    {
        DOTween.Clear(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
