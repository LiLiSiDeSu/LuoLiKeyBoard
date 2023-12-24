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

        #region 在这里调用继承BaseAutoInstance_Mono脚本的GetInstance方法

        Gaming = new("Gaming");
        transform.parent = Gaming.transform;
        GameObject Other = new("Other");
        Other.transform.SetParent(transform);

        //数据一定要先加载出来
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
        //StartPanel一定要在UIMgr后面加载 不然一开始显示的Panel不会被设置到Canvas下面
        Start.GetInstance().transform.SetParent(Other.transform);

        #endregion
    }
}
