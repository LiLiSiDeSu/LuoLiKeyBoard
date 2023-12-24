using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class WindowActive : MonoBehaviour
{
    [DllImport("User32.dll")]
    extern static bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("User32.dll")]
    extern static bool ShowWindow(IntPtr hWnd, short State);

    [DllImport("user32.dll ")]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    const UInt32 SWP_NOSIZE = 0x0001;
    const UInt32 SWP_NOMOVE = 0x0002;
    IntPtr hWnd;

    //public float Wait = 0;//延迟执行
    //public float Rate = 1;//更新频率
    public bool KeepForeground = true;//保持最前

    void Start()
    {
        hWnd = C.GetProcessWnd();
        Active();
        //InvokeRepeating("Active", Wait, Rate);
    }

    /// <summary>
    /// 激活窗口
    /// </summary>
    void Active()
    {
        if (KeepForeground)
        {
            ShowWindow(hWnd, 1);
            SetForegroundWindow(hWnd);
            SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE);
        }
    }
}