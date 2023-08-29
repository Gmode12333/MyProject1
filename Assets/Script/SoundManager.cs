using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : GlobalReference<SoundManager>
{

    [SerializeField] private AudioSource musicSource, sfxSource;

    public void PlaySound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
