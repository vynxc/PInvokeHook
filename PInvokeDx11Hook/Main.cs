

using PInvokeHook;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ImGuiNET;
using System.Reflection;

namespace PInvokeDx11Hook
{
    public class Main
    {
        private static void LoadEmbeddedDll(string resourceName)
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            string fullResourceName = $"{currentAssembly.GetName().Name}.{resourceName}";

            using (Stream resourceStream = currentAssembly.GetManifestResourceStream(fullResourceName))
            {
                if (resourceStream == null)
                {
                    throw new Exception($"Resource '{fullResourceName}' not found.");
                }

                string tempPath = Path.Combine(Path.GetTempPath(), resourceName);

                using (FileStream fileStream = new FileStream(tempPath, FileMode.Create))
                {
                    resourceStream.CopyTo(fileStream);
                }

                LoadLibrary(tempPath);
            }
        }

        #region PInvoke

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("PInvokeHooker.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetPresent(CallbackDelegate callback);

        [DllImport("PInvokeHooker.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetRenderDraw(CallbackNothingDelegate callback);

        [DllImport("PInvokeHooker.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Init();

        [DllImport("PInvokeHooker.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetHWND();

        [DllImport("PInvokeHooker.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetDevice();

        [DllImport("PInvokeHooker.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetContext();

        [DllImport("PInvokeHooker.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetWndProcHandler(WndProcDelegate callback);

        #endregion
        #region Delegates
        public delegate long WndProcDelegate(IntPtr hWnd, uint Msg, long wParam, uint lParam);

        public delegate void CallbackDelegate(IntPtr swapChainPtr, int syncInterval, int flags);

        public delegate void CallbackNothingDelegate();

        #endregion

        static bool isInit = false;

        public static void HkPresent(IntPtr swapChainPtr, int syncInterval, int flags)
        {
            Console.WriteLine("HkPresent called");
            if (!isInit)
            {
                Console.WriteLine("init start");
                var hwnd = GetHWND();
                var device = GetDevice();
                var context = GetContext();

                Console.WriteLine("device " + device);
                Console.WriteLine("hwnd " + hwnd);
                ImGui.CreateContext();
                ImplWin32.Init(hwnd);
                ImplDX11.Init(device, context);
                isInit = true;
                Console.WriteLine("init finished");
            }

            Console.WriteLine("post init render");
            ImplDX11.NewFrame();
            ImplWin32.NewFrame();
            ImGui.NewFrame();
            ImGui.Begin("vynxc is cool");
            ImGui.End();
            ImGui.Render();

        }

        public static void Render()
        {
            Console.WriteLine("Render called");
            ImplDX11.RenderDrawData(ImGui.GetDrawData());
        }

        public static long WndProcHandler(IntPtr hWnd, uint Msg, long wParam, uint lParam)
        {
            Console.WriteLine("WndProcHandler called");
            Console.WriteLine("Hwnd: " + hWnd + " Msg: " + Msg + " wParam " + wParam + " lParam " + lParam);
            //TODO: sus
            var data = ImplWin32.WndProcHandler(hWnd, Msg, wParam, lParam);
            Console.WriteLine("WndProcHandler: " + data);
            return data;
        }


        public static void CreateHook()
        {
            SetWndProcHandler(WndProcHandler);
            SetPresent(HkPresent);
            SetRenderDraw(Render);
            Init();
        }




        [UnmanagedCallersOnly(EntryPoint = "DllMain", CallConvs = new[] { typeof(CallConvStdcall) })]
        private static bool DllMain(IntPtr hModule, uint ulReasonForCall, IntPtr lpReserved)
        {
            switch (ulReasonForCall)
            {
                case 1:
                    LoadEmbeddedDll("PInvokeHooker.dll");
                    LoadEmbeddedDll("cimgui.dll");
                    WinApi.AllocConsole();
                    Task.Run(WorkerThread);
                    break;
                default:
                    break;
            }

            return true;
        }

        private static void WorkerThread()
        {
            CreateHook();
            Console.WriteLine("Running");
            Console.ReadLine();
        }
    }
}