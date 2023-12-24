using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class MgrInstance : InstanceBase_Mono<MgrInstance>
{
    public GameObject Gaming;

    protected override void Awake()
    {
        base.Awake();

        #region ��������ü̳�BaseAutoInstance_Mono�ű���GetInstance����

        Gaming = new("Gaming");
        transform.parent = Gaming.transform;
        GameObject Other = new("Other");
        Other.transform.SetParent(transform);

        //����һ��Ҫ�ȼ��س���
        MgrData.GetInstance().transform.SetParent(Other.transform);
        MgrAudioSource.GetInstance().transform.SetParent(Other.transform);
        //--
        PoolBuffer.GetInstance().transform.SetParent(transform);
        PoolEsc.GetInstance().transform.SetParent(transform);
        //--
        PoolNowPanel.GetInstance().transform.SetParent(Other.transform);
        CenterEvent.GetInstance().transform.SetParent(Other.transform);
        MgrInput.GetInstance().transform.SetParent(Other.transform);
        MgrJson.GetInstance().transform.SetParent(Other.transform);
        MgrRes.GetInstance().transform.SetParent(Other.transform);
        MgrUI.GetInstance().transform.SetParent(Other.transform);
        //StartPanelһ��Ҫ��UIMgr������� ��Ȼһ��ʼ��ʾ��Panel���ᱻ���õ�Canvas����
        Start.GetInstance().transform.SetParent(Other.transform);

        #endregion
    }
}
