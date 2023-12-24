using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class GlobalInput : MonoBehaviour
{
    // 安装钩子
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    // 卸载钩子
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    // 向下传递钩子
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    // 获取程序集模块的句柄
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    // 全局钩子键盘为13
    private const int WH_KEYBOARD_LL = 13;

    // 按下键
    private const int WM_KEYDOWN = 0x0100;

    private static LowLevelKeyboardProc _proc = HookCallback;
    private static IntPtr _hookID = IntPtr.Zero;

    void Start()
    {
        _hookID = SetHook(_proc);
    }

    void OnApplicationQuit()
    {
        UnhookWindowsHookEx(_hookID);
    }

    // 安装Hook,用于截获键盘。
    private static IntPtr SetHook(LowLevelKeyboardProc proc)
    {
        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule curModule = curProcess.MainModule)
        {
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
        }
    }

    private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        // nCode>0表示此消息已由Hook函数处理了,不会交给Windows窗口过程处理了
        // nCode=0则表示消息继续传递给Window消息处理函数
        if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
        {
            int vkCode = Marshal.ReadInt32(lParam);
            if (vkCode == 87)
            {
                Hot.MgrData_.DicAudioSource[KeyCode.W].Play();
            }
            if (vkCode == 65)
            {
                Hot.MgrData_.DicAudioSource[KeyCode.A].Play();
            }
            if (vkCode == 83)
            {
                Hot.MgrData_.DicAudioSource[KeyCode.S].Play();
            }
            if (vkCode == 68)
            {
                Hot.MgrData_.DicAudioSource[KeyCode.D].Play();
            }
        }

        return CallNextHookEx(_hookID, nCode, wParam, lParam); // 传给下一个钩子
    }
}
