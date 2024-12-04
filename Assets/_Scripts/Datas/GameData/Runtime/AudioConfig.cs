using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioConfig", menuName = "Scriptable Objects/AudioConfig")]
[Serializable]
public class AudioConfig : ScriptableObject
{
    public List<AudioClip> list=new();
    
    public Dictionary<int, AudioClip> Dics = new();

    public void Init()
    {
        Dics.Clear();
        for ( int i = 0; i < list.Count; i++ )
        {
            Dics.Add(i+1, list[i]);
        }
    }

}

[Serializable]
public struct SAudioMap
{
   public int index
        ;
    public AudioClip ac;
}
//public int EAudioEffectIndex
//{
//}

