using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LevelStartedFade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().DOFade(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
