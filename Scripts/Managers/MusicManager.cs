using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    

    [SerializeField]
    private AudioClip[] audioClips;

    private AudioSource audioSource;
 
    private float audioTimer; 
    private float timerOffset; 


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
        if (audioTimer + timerOffset < Time.time)
        { 

            audioSource.clip = audioClips[Random.Range(0, audioClips.Length)]; //find the correct audio clip to play
            timerOffset = audioSource.clip.length;
            audioSource.Play(); 
            audioTimer = Time.time;
        }
    }

    
}
