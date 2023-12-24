﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public enum E_InputKeyEvent
{
    KeyDown,
    KeyUp,
    KeyHold,
}

public class MgrInput : InstanceBaseAuto_Mono<MgrInput>
{
    private void Update()
    {
        if (!IsOpen)
            return;
    }

    /// <summary>
    /// 是否输入检测
    /// </summary>
    private bool IsOpen = true;

    /// <summary>
    /// 是否开启或关闭输入检测
    /// </summary>
    /// <param name="isOpen">---</param>
    public void OpenOrCloseCheck(bool isOpen)
    {
        IsOpen = isOpen;
    }

    /// <summary>
    /// 用来检测按键抬起按下 分发事件
    /// </summary>
    /// <param name="key"></param>
    private void CheckKeyCode(KeyCode key)
    {
        //可以添加其他的输入事件

        //事件中心模块 统一触发抬起事件
        if (Input.GetKeyUp(key))
            CenterEvent.GetInstance().EventTrigger<KeyCode>(E_InputKeyEvent.KeyUp.ToString(), key);
        //事件中心模块 统一触发按下事件
        if (Input.GetKeyDown(key))
            CenterEvent.GetInstance().EventTrigger<KeyCode>(E_InputKeyEvent.KeyDown.ToString(), key);
        //事件中心模块 统一触发按住事件
        if (Input.GetKey(key))
            CenterEvent.GetInstance().EventTrigger<KeyCode>(E_InputKeyEvent.KeyHold.ToString(), key);
    }
}
