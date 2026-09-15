using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 音效管理器
/// </summary>
public class SoundMgr : Singleton<SoundMgr>
{
    private SoundMgr()
    {
        MonoMgr.Instance.AddUpdateListener(Update);
    }

    //所有音效组件的容器
    private List<AudioSource> soundList = new List<AudioSource>();
    //所有音效音量
    private float soundvolume = 0.5f;

    //是否在暂停状态
    private bool isPause = false;

    /// <summary>
    /// 自动移除音效组件
    /// </summary>
    private void Update()
    {
        if (!isPause)
        {
            for (int i = soundList.Count - 1; i >= 0; i--)
            {
                if (!soundList[i].isPlaying)
                {
                    soundList[i].clip = null;
                    PoolMgr.Instance.PushObject("Prefab/SoundObj", soundList[i].gameObject);
                    soundList.RemoveAt(i);
                }
            }
        }
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="name">音效名称</param>
    /// <param name="isLoop">是否循环播放</param>
    /// <param name="callBack">回调函数</param>
    public void PlaySound(string name, bool isLoop = false, UnityAction<AudioSource> callBack = null)
    {
        ResourcesMgr.Instance.LoadAsync<AudioClip>("Sound/" + name, (clip) =>
        {
            AudioSource source = PoolMgr.Instance.GetObject("Prefab/SoundObj").GetComponent<AudioSource>();
            source.Stop();

            source.clip = clip;
            source.loop = isLoop;
            source.volume = soundvolume;
            source.Play();
            if(!soundList.Contains(source))
            {
                soundList.Add(source);
            }
            
            callBack?.Invoke(source);
        });
    }

    /// <summary>
    /// 停止循环音效
    /// </summary>
    /// <param name="source">音效组件</param>
    public void StopSound(AudioSource source)
    {
        if(soundList.Contains(source))
        {
            source.Stop();
            source.clip = null;
            PoolMgr.Instance.PushObject("Prefab/SoundObj", source.gameObject);
            soundList.Remove(source);
        }
    }

    /// <summary>
    /// 设置所有音效音量
    /// </summary>
    /// <param name="volume">音量的值</param>
    public void SetSoundsVolume(float volume)
    {
        soundvolume = volume;
        for(int i = 0; i < soundList.Count; i++)
        {
            soundList[i].volume = volume;
        }
    }

    /// <summary>
    /// 暂停播放所有音效
    /// </summary>
    public void PauseSounds()
    {
        isPause = true;
        for (int i = 0; i < soundList.Count; i++)
        {
            soundList[i].Pause();
        }
    }

    /// <summary>
    /// 继续播放所有音效
    /// </summary>
    public void ContinueSounds()
    {
        isPause = false;
        for (int i = 0; i < soundList.Count; i++)
        {
            soundList[i].Play();
        }
    }

    /// <summary>
    /// 清除所有音效
    /// 在清空缓存池之前调用
    /// </summary>
    public void ClearSounds()
    {
        for (int i = 0; i < soundList.Count; i++)
        {
            soundList[i].Stop();
            soundList[i].clip = null;
            PoolMgr.Instance.PushObject("Prefab/SoundObj", soundList[i].gameObject);
        }
        soundList.Clear();
    }
}
