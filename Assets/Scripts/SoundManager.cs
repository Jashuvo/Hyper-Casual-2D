using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    public AudioSource source;
    [SerializeField]
    AudioClip jumpSound,deathSound;
    public static SoundManager instance = null;
    public void Start(){
        if(instance == null){
            instance = this;
        }
    }
    public void DeathSound(){
        source.clip = null;
        source.clip = deathSound;
        source.Play();
    }

    public void JumpSound(){
        source.clip = null;
        source.clip = jumpSound;
        source.Play();
    }
}
