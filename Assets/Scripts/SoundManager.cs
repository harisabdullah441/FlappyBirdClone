using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager 
{
    public enum Sound
    {
        BirdJump,
        Loose,
        ButtonOver,
        ButtonClick,
        Score,
    }

    public static void PlaySound(Sound sound)
    {
        GameObject gameObject = new GameObject("Sound", typeof(AudioSource));
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundClipManager audioClip in GameAssets.GetInstance().soundmanagerclipArray)
        {
            if(audioClip.sound == sound)
            {
                return audioClip.audioClip;
            }
        }
        Debug.LogError("Sound "+sound +"not found");
        return null;

    }

}
