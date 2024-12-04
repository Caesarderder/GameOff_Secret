using UnityEngine;
using System;
using System.Collections.Generic;

public struct SAudioClipSettings
{
    public AudioClip Ac;
    public bool Loop;
    public Vector3 Pos;
}


public class AudioManager
{
    // 背景音乐的音频源
    public AudioSource backgroundMusicSource;
    // 音效的音频源池
    private List<AudioSource> soundEffectSources = new List<AudioSource>();
    // 音效预设列表
    public Dictionary<EAudioEffectIndex,AudioClip> soundEffects;

    GameObject go_asPool;

    // 背景音乐和音效的音量
    [Range(0f, 1f)] public float backgroundMusicVolume = 0.2f;
    [Range(0f, 1f)] public float soundEffectVolume = 0.2f;

    private bool isMuted = false;  // 是否静音

    public void Init()
    {
        // 初始化音效音量
        foreach ( var source in soundEffectSources )
        {
            source.volume = soundEffectVolume;
        }
        go_asPool=ResManager.MonoManager.AudioPool.gameObject;
        soundEffects = Manager<TableManager>.Inst.AudioConfig.Dics;

        // 初始化背景音乐音量
        backgroundMusicSource= go_asPool.AddComponent<AudioSource>();
        backgroundMusicSource.loop = true;
        backgroundMusicSource.volume = backgroundMusicVolume;

    }

    // 播放背景音乐
    public void PlayBackgroundMusic(AudioClip musicClip)
    {
        // 如果正在播放的背景音乐和新播放的音乐一样，则不做任何处理
        if ( backgroundMusicSource.isPlaying && backgroundMusicSource.clip == musicClip ) return;

        backgroundMusicSource.clip = musicClip;
        backgroundMusicSource.Play();
    }

    // 切换背景音乐
    public void SwitchBackgroundMusic(AudioClip musicClip)
    {
        backgroundMusicSource.Stop();
        backgroundMusicSource.clip = musicClip;
        backgroundMusicSource.Play();
    }

    // 停止背景音乐
    public void StopBackgroundMusic()
    {
        backgroundMusicSource.Stop();
    }

    // 播放音效
    public void PlaySoundEffect(EAudioEffectIndex effectIndex)
    {
        // 确保索引有效
            AudioSource soundEffectSource = GetAvailableSoundEffectSource();
            soundEffectSource.clip = soundEffects[effectIndex];
            soundEffectSource.Play();
    }

    // 获取可用的音效音源
    private AudioSource GetAvailableSoundEffectSource()
    {
        foreach ( AudioSource source in soundEffectSources )
        {
            if ( !source.isPlaying )
                return source;
        }

        // 如果没有空闲音效源，则创建一个新的音效源
        AudioSource newSource = go_asPool.AddComponent<AudioSource>();
        newSource.volume = soundEffectVolume;
        newSource.loop = false;
        soundEffectSources.Add(newSource);
        return newSource;
    }

    // 暂停所有音乐
    public void PauseAllMusic()
    {
        backgroundMusicSource.Pause();
        foreach ( var source in soundEffectSources )
        {
            source.Pause();
        }
    }

    // 恢复所有音乐
    public void ResumeAllMusic()
    {
        backgroundMusicSource.UnPause();
        foreach ( var source in soundEffectSources )
        {
            source.UnPause();
        }
    }

    // 静音或恢复音效
    public void ToggleMute()
    {
        isMuted = !isMuted;
        backgroundMusicSource.mute = isMuted;
        foreach ( var source in soundEffectSources )
        {
            source.mute = isMuted;
        }
    }

    // 设置背景音乐音量
    public void SetBackgroundMusicVolume(float volume)
    {
        backgroundMusicVolume = volume;
        backgroundMusicSource.volume = volume;
    }

    // 设置音效音量
    public void SetSoundEffectVolume(float volume)
    {
        soundEffectVolume = volume;
        foreach ( var source in soundEffectSources )
        {
            source.volume = volume;
        }
    }





}