using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class MgrData : InstanceBaseAuto_Mono<MgrData>
{
    public Config config = new();
    public string filePath = "D:/LuoLiKeyBoardData/AudioClip";

    //  Q 81, W 87, E 69, R 82, T 84, Y 89, U 85, I 73, O 79, P 80
    //  A 65, S 83, D 68, F 70, G 71, H 72, J 74, K 75, L 76
    //  Z 90, X 88, C 67, V 86, B 66, N 78, M 77
    public Dictionary<int, string> DicRegisterKey = new()
    {
        { 81, "Q" }, { 87, "W" }, { 69, "E" }, { 82, "R"}, { 84, "T" }, { 89, "Y" }, { 85, "U" }, { 73, "I" }, { 79, "O" }, { 80, "P" },
        { 65, "A" }, { 83, "S" }, { 68, "D" }, { 70, "F" }, { 71, "G" }, { 72, "H" }, { 74, "J"   }, { 75, "K" }, { 76, "L" },
        { 90, "Z" }, { 88, "X" }, { 67, "C" }, { 86, "V" }, { 66, "B" }, { 78, "N" }, { 77, "M" },
    };
    public Dictionary<string, AudioSource> DicAudioSource = new();
    public List<AudioSource> ListTempAudioSource = new();

    private void Awake()
    {
        config = Hot.MgrJson_.Load<Config>("", "/Config");
    }

    public void Init()
    {
        foreach (int key in DicRegisterKey.Keys)
        {
            if (File.Exists(filePath + "/" + DicRegisterKey[key] + ".wav"))
            {
                AudioClip clip = Hot.MgrAudioSource_.BytesToClip(File.ReadAllBytes(filePath + "/" + DicRegisterKey[key] + ".wav"));

                GameObject obj = new(DicRegisterKey[key]);
                AudioSource audioSource = obj.AddComponent<AudioSource>();
                audioSource.pitch = 2f;
                audioSource.clip = clip;

                DicAudioSource.Add(DicRegisterKey[key], audioSource);
            }
        }
    }
}
