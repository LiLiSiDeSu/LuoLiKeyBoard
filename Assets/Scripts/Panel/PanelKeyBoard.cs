using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelKeyBoard : PanelBase
{
    public Toggle TogIsRepeat;

    protected override void Awake()
    {
        base.Awake();

        TogIsRepeat = transform.FindSonSonSon("TogIsRepeat").GetComponent<Toggle>();
        TogIsRepeat.isOn = Hot.MgrData_.config.IsRepeat;
    }

    protected override void Toggle_OnValueChange(string controlname, bool EventParam)
    {
        base.Toggle_OnValueChange(controlname, EventParam);

        switch (controlname)
        {
            case "TogIsRepeat":
                Hot.MgrData_.config.IsRepeat = TogIsRepeat.isOn;
                Hot.MgrJson_.Save(Hot.MgrData_.config, "", "/Config");
                break;
        }
    }
}
