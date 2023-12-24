using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using UnityEngine;

public class GlobalInput : MonoBehaviour
{
    // ��װ����
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    // ж�ع���
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    // ���´��ݹ���
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    // ��ȡ����ģ��ľ��
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    // ȫ�ֹ��Ӽ���Ϊ13
    private const int WH_KEYBOARD_LL = 13;

    // ���¼�
    private const int WM_KEYDOWN = 0x0100;

    private static LowLevelKeyboardProc _proc = HookCallback;
    private static IntPtr _hookID = IntPtr.Zero;

    private void Start()
    {
        _hookID = SetHook(_proc);
    }

    private void OnApplicationQuit()
    {
        UnhookWindowsHookEx(_hookID);
    }

    // ��װHook,���ڽػ���̡�
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
        // nCode>0��ʾ����Ϣ����Hook����������,���ύ��Windows���ڹ��̴�����
        // nCode=0���ʾ��Ϣ�������ݸ�Window��Ϣ������
        if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
        {
            int vkCode = Marshal.ReadInt32(lParam);

            if (Hot.MgrData_.DicRegisterKey.ContainsKey(vkCode))
            {
                string key = Hot.MgrData_.DicRegisterKey[vkCode];

                if (Hot.MgrData_.DicAudioSource.ContainsKey(key))
                {
                    if (Hot.PanelKeyBoard_.TogIsRepeat.isOn && Hot.MgrData_.DicAudioSource[key].isPlaying)
                    {
                        GameObject obj = Instantiate(Hot.MgrData_.DicAudioSource[key].gameObject);
                        obj.GetComponent<AudioSource>().Play();
                        Hot.MgrData_.ListTempAudioSource.Add(obj.GetComponent<AudioSource>());
                        UnityEngine.Debug.Log(Hot.MgrData_.ListTempAudioSource.Count);
                    }
                    else
                    {
                        Hot.MgrData_.DicAudioSource[key].Play();
                    }
                }
            }
        }

        if (Hot.MgrData_.ListTempAudioSource.Count >= 10)
        {
            bool isQuit = false;

            for (int i = 1; i <= 10; i++)
            {
                if (Hot.MgrData_.ListTempAudioSource[^i].isPlaying)
                {
                    isQuit = true;
                    break;
                }
            }

            if (!isQuit)
            {
                foreach (var item in Hot.MgrData_.ListTempAudioSource)
                {
                    DestroyImmediate(item.gameObject);
                }

                Hot.MgrData_.ListTempAudioSource.Clear();
                UnityEngine.Debug.Log("Clear");
            }
        }

        // ������һ������
        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }
}
