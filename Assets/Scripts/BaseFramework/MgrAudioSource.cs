using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MgrAudioSource : InstanceBaseAuto_Mono<MgrAudioSource>
{
    public AudioClip BytesToClip(byte[] rawData)
    {
        float[] samples = new float[rawData.Length / 2];
        float rescaleFactor = 32767;
        short st = 0;
        float ft = 0;

        for (int i = 0; i < rawData.Length; i += 2)
        {
            st = BitConverter.ToInt16(rawData, i);
            ft = st / rescaleFactor;
            samples[i / 2] = ft;
        }

        AudioClip audioClip = AudioClip.Create("mySound", samples.Length, 1, 44100, false, false);
        audioClip.SetData(samples, 0);

        return audioClip;
    }
}
