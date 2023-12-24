using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class MgrData : InstanceBaseAuto_Mono<MgrData>
{
    public string filePath = "D:/AudioClip";

    public List<string> ListRegisterKey = new()
    {
        "W", "S", "A", "D"
    };
    public Dictionary<KeyCode, AudioSource> DicAudioSource = new();

    public void Init()
    {
        foreach (var strkey in ListRegisterKey)
        {
            AudioClip clip = Hot.MgrAudioSource_.BytesToClip(File.ReadAllBytes(filePath + "/" + strkey + ".wav"));
            KeyCode key = (KeyCode)Enum.Parse(typeof(KeyCode), strkey);

            GameObject obj = new(strkey);
            AudioSource audioSource = obj.AddComponent<AudioSource>();
            audioSource.pitch = 2f;
            audioSource.clip = clip;

            DicAudioSource.Add(key, audioSource);
        }
    }
}
