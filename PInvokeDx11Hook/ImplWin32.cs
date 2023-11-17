using System.Runtime.InteropServices;

namespace PInvokeDx11Hook
{
    public class ImplWin32
    {
        [DllImport("cimgui", EntryPoint = "ImGui_ImplWin32_Init", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Init(IntPtr device);

        [DllImport("cimgui", EntryPoint = "ImGui_ImplWin32_Shutdown", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Shutdown();

        [DllImport("cimgui", EntryPoint = "ImGui_ImplWin32_NewFrame", CallingConvention = CallingConvention.Cdecl)]
        public static extern void NewFrame();

        [DllImport("cimgui", EntryPoint = "ImGui_ImplWin32_WndProcHandler", CallingConvention = CallingConvention.Cdecl)]
        public static extern long WndProcHandler(IntPtr hWnd, uint Msg, long wParam, uint lParam);
    }
}
