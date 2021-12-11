using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameSound
{
    [SerializeField]
    SoundName soundName;
    [SerializeField]
    AudioClip audioClip;
    [SerializeField]
    [Range(0f, 1f)]
    int volume;
}
public enum SoundName
{
    player_jump, 
    player_walk, 
    player_die,
    player_kill
}
public class AudioPlayer : Singleton<AudioPlayer>
{
    //public List<>
}
