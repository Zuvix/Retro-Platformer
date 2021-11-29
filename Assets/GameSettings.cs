using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField]
    private int targetFramerate = 80;
    private void Awake()
    {
        Application.targetFrameRate = targetFramerate;
    }

}
