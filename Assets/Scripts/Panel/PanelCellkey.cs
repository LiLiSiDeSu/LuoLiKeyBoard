using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCellkey : PanelBase
{
    public string key;
    public AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();

        audioSource = transform.GetComponentInChildren<AudioSource>();
    }

    protected override void Button_OnClick(string controlname)
    {
        base.Button_OnClick(controlname);

        switch (controlname)
        {
            case "BtnkeyDown":
                audioSource.Play();
                break;
        }
    }
}
