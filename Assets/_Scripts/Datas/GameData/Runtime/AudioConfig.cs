using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioConfig", menuName = "Scriptable Objects/AudioConfig")]
[Serializable]
public class AudioConfig : ScriptableObject
{
    public List<SAudioMap> list=new();
    
    public Dictionary<EAudioEffectIndex, AudioClip> Dics = new();

    public void Init()
    {
        Dics.Clear();
        for ( int i = 0; i < list.Count; i++ )
        {
            Dics.Add(list[i].index, list[i].ac);
        }
    }

}

[Serializable]
public struct SAudioMap
{
   public EAudioEffectIndex index
        ;
    public AudioClip ac;
}
public enum EAudioEffectIndex
{
    test1=1, 
    test2=2, 
    test3=3,
}

