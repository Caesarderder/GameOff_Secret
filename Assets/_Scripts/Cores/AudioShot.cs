using UnityEngine;

public class AudioShot : MonoBehaviour
{
    public int AudioClipIndex;
    
    public bool PlayOnAwake=false;
    public bool Loop=false;
    public bool StopWhenDisable=true;
    bool _isPlayed;

    AudioSource audioSource;
    AudioClip audioClip;

    private void Awake()
    {
        if(PlayOnAwake)
        {
            Play();
        }
    }

    public void ChangeAudioClipIndex(int index)
    {
        Stop();
        audioClip = Manager<AudioManager>.Inst.GetAudioClipByIndex(AudioClipIndex);
    }


    public void Play()
    {
        _isPlayed = true;
        if(audioSource==null)
        {
            audioSource=Manager<AudioManager>.Inst.GetAvailableSoundEffectSource();
            audioClip=Manager<AudioManager>.Inst.GetAudioClipByIndex(AudioClipIndex);
        }

        if(!Loop)
        {
            audioSource.PlayOneShot(audioClip);
        }
        else
        {
            audioSource.clip = audioClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    private void OnDisable()
    {
        if(StopWhenDisable)
            Stop();
    }
    public void Pause()
    {
        if ( !_isPlayed )
        {
            return;
        }
        audioSource.Pause();
    }
    public void Continue()
    {
        if ( !_isPlayed )
        {
            return;
        }
         audioSource.UnPause();
    }

    public void Stop()
    {
        if ( !_isPlayed )
        {
            return;
        }
         audioSource.Stop();
    }



}
