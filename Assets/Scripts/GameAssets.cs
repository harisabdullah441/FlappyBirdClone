using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets instance;

    public static GameAssets GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    public Transform pfPipeHead;
    public Transform pfPipeBody;
    public Transform scoreCollider;
    public Transform pfGround;
  public SoundClipManager[] soundmanagerclipArray;
    
      [Serializable]
public class SoundClipManager
    {
       public SoundManager.Sound sound;
       public AudioClip audioClip;
    }

}
