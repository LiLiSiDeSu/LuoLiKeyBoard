using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class C
{
    public delegate bool WNDENUMPROC(IntPtr hwnd, uint lParam);
    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool EnumWindows(WNDENUMPROC lpEnumFunc, uint lParam);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr GetParent(IntPtr hWnd);
    [DllImport("user32.dll")]
    public static extern uint GetWindowThreadProcessId(IntPtr hWnd, ref uint lpdwProcessId);

    [DllImport("kernel32.dll")]
    public static extern void SetLastError(uint dwErrCode);

    public static IntPtr GetProcessWnd()
    {
        IntPtr ptrWnd = IntPtr.Zero;
        uint pid = (uint)Process.GetCurrentProcess().Id;  // ��ǰ���� ID  

        bool bResult = EnumWindows(new WNDENUMPROC(delegate (IntPtr hwnd, uint lParam)
        {
            uint id = 0;
            if (GetParent(hwnd) == IntPtr.Zero)
            {
                GetWindowThreadProcessId(hwnd, ref id);
                if (id == lParam)    // �ҵ����̶�Ӧ�������ھ��  
                {
                    ptrWnd = hwnd;   // �Ѿ����������  
                    SetLastError(0);    // �����޴���  
                    return false;   // ���� false ����ֹö�ٴ���  
                }
            }

            return true;

        }), pid);

        return (!bResult && Marshal.GetLastWin32Error() == 0) ? ptrWnd : IntPtr.Zero;
    }
}