using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip collectBlockSound, fireSound, jumpSound, deathSound, foundBlockSound, enterLevelSound, winningSound;
    private static AudioSource audiosource;
    public static AudioManager audioManager;


     private void Start()
    {
        audioManager = GetComponent<AudioManager>();
        collectBlockSound = Resources.Load<AudioClip>("Coin");
        fireSound = Resources.Load<AudioClip>("Fire");
        jumpSound = Resources.Load<AudioClip>("Jump");
        deathSound = Resources.Load<AudioClip>("Death");
        foundBlockSound = Resources.Load <AudioClip>("FoundBlock");
        enterLevelSound = Resources.Load<AudioClip>("EnterLevel");
        winningSound = Resources.Load<AudioClip>("Winning");
        audiosource = GetComponent<AudioSource>();
        
    }

    public static void playSound(String clip)
    {
        switch (clip)
        {
            case "Fire":
                audiosource.PlayOneShot(fireSound);
                break;

            case "Coin":
                audiosource.PlayOneShot(collectBlockSound);
                break;

            case "Jump":
                audiosource.PlayOneShot(jumpSound);
                break;

            case "Death":
                audiosource.PlayOneShot(deathSound);
                break;

            case "FoundBlock":
                audiosource.PlayOneShot(foundBlockSound);
                break;

            case "EnterLevel":
                audiosource.PlayOneShot(enterLevelSound);
                break;

            case "Winning":
                audiosource.PlayOneShot(winningSound);
                break;
        }
    }
}
