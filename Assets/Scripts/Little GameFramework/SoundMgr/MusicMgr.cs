using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// “Ù¿÷π‹¿Ì∆˜
/// </summary>
public class MusicMgr : Singleton<MusicMgr>
{
    private MusicMgr() { }

    //±≥æ∞“Ù¿÷◊Èº˛
    private AudioSource BGM = null;
    //±≥æ∞“Ù¿÷“Ù¡ø
    private float BGMvolume = 0.5f;

    /// <summary>
    /// ≤•∑≈±≥æ∞“Ù¿÷
    /// </summary>
    /// <param name="name">“Ù¿÷√˚≥∆</param>
    public void PlayBGM(string name)
    {
        if (BGM == null)
        {
            GameObject obj = new GameObject("BGM");
            GameObject.DontDestroyOnLoad(obj);
            BGM = obj.AddComponent<AudioSource>();
        }

        ResourcesMgr.Instance.LoadAsync<AudioClip>("Music/" + name, (clip) =>
        {
            BGM.clip = clip;
            BGM.loop = true;
            BGM.volume = BGMvolume;
            BGM.Play();
        });
    }

    /// <summary>
    /// Õ£÷π±≥æ∞“Ù¿÷
    /// </summary>
    public void StopBGM()
    { 
        if (BGM != null)
        {
            BGM.Stop();
        }
    }

    /// <summary>
    /// ‘›Õ£±≥æ∞“Ù¿÷
    /// </summary>
    public void PauseBGM()
    {
        if (BGM != null)
        {
            BGM.Pause();
        }
    }

    /// <summary>
    /// …Ë÷√±≥æ∞“Ù¿÷“Ù¡ø
    /// </summary>
    /// <param name="volume">“Ù¡øµƒ÷µ</param>
    public void SetBGMVolume(float volume)
    {
        BGMvolume = volume;
        if (BGM != null)
        {
            BGM.volume = volume;
        }
    }
}
