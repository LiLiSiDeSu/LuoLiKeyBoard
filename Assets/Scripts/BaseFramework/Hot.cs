using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hot
{
    public static MgrUI MgrUI_
    {
        get { return MgrUI.GetInstance(); }
    }
    public static PoolBuffer PoolBuffer_
    {
        get { return PoolBuffer.GetInstance(); }
    }
    public static MgrInput MgrInput_
    {
        get { return MgrInput.GetInstance(); }
    }
    public static MgrJson MgrJson_
    {
        get { return MgrJson.GetInstance(); }
    }
    public static CenterEvent CenterEvent_
    {
        get { return CenterEvent.GetInstance(); }
    }
    public static MgrRes MgrRes_
    {
        get { return MgrRes.GetInstance(); }
    }
    public static PoolNowPanel PoolNowPanel_
    {
        get { return PoolNowPanel.GetInstance(); }
    }
    public static MgrData MgrData_
    {
        get { return MgrData.GetInstance(); }
    }
    public static MgrAudioSource MgrAudioSource_
    {
        get { return MgrAudioSource.GetInstance(); }
    }
    public static PanelKeyBoard PanelKeyBoard_
    {
        get { return MgrUI_.GetPanel<PanelKeyBoard>("PanelKeyBoard"); }
    }
}
