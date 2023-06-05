using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public static void PlayMusicByName(string name)
    {
        string path = "Sounds/" + name;
        AudioClip clip = Resources.Load<AudioClip>(path);
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }
}
