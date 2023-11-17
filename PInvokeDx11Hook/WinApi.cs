﻿using System.Runtime.InteropServices;

namespace PInvokeHook;


public enum WM : int
{
    NULL = 0x0000,
    CREATE = 0x0001,
    DESTROY = 0x0002,
    MOVE = 0x0003,
    SIZE = 0x0005,
    ACTIVATE = 0x0006,
    SETFOCUS = 0x0007,
    KILLFOCUS = 0x0008,
    ENABLE = 0x000A,
    SETREDRAW = 0x000B,
    SETTEXT = 0x000C,
    GETTEXT = 0x000D,
    GETTEXTLENGTH = 0x000E,
    PAINT = 0x000F,
    CLOSE = 0x0010,
    QUERYENDSESSION = 0x0011,
    QUERYOPEN = 0x0013,
    ENDSESSION = 0x0016,
    QUIT = 0x0012,
    ERASEBKGND = 0x0014,
    SYSCOLORCHANGE = 0x0015,
    SHOWWINDOW = 0x0018,
    WININICHANGE = 0x001A,
    SETTINGCHANGE = WININICHANGE,
    DEVMODECHANGE = 0x001B,
    ACTIVATEAPP = 0x001C,
    FONTCHANGE = 0x001D,
    TIMECHANGE = 0x001E,
    CANCELMODE = 0x001F,
    SETCURSOR = 0x0020,
    MOUSEACTIVATE = 0x0021,
    CHILDACTIVATE = 0x0022,
    QUEUESYNC = 0x0023,
    GETMINMAXINFO = 0x0024,
    PAINTICON = 0x0026,
    ICONERASEBKGND = 0x0027,
    NEXTDLGCTL = 0x0028,
    SPOOLERSTATUS = 0x002A,
    DRAWITEM = 0x002B,
    MEASUREITEM = 0x002C,
    DELETEITEM = 0x002D,
    VKEYTOITEM = 0x002E,
    CHARTOITEM = 0x002F,
    SETFONT = 0x0030,
    GETFONT = 0x0031,
    SETHOTKEY = 0x0032,
    GETHOTKEY = 0x0033,
    QUERYDRAGICON = 0x0037,
    COMPAREITEM = 0x0039,
    GETOBJECT = 0x003D,
    COMPACTING = 0x0041,
    COMMNOTIFY = 0x0044,
    WINDOWPOSCHANGING = 0x0046,
    WINDOWPOSCHANGED = 0x0047,
    POWER = 0x0048,
    COPYDATA = 0x004A,
    CANCELJOURNAL = 0x004B,
    NOTIFY = 0x004E,
    INPUTLANGCHANGEREQUEST = 0x0050,
    INPUTLANGCHANGE = 0x0051,
    TCARD = 0x0052,
    HELP = 0x0053,
    USERCHANGED = 0x0054,
    NOTIFYFORMAT = 0x0055,
    CONTEXTMENU = 0x007B,
    STYLECHANGING = 0x007C,
    STYLECHANGED = 0x007D,
    DISPLAYCHANGE = 0x007E,
    GETICON = 0x007F,
    SETICON = 0x0080,
    NCCREATE = 0x0081,
    NCDESTROY = 0x0082,
    NCCALCSIZE = 0x0083,
    NCHITTEST = 0x0084,
    NCPAINT = 0x0085,
    NCACTIVATE = 0x0086,
    GETDLGCODE = 0x0087,
    SYNCPAINT = 0x0088,


    NCMOUSEMOVE = 0x00A0,
    NCLBUTTONDOWN = 0x00A1,
    NCLBUTTONUP = 0x00A2,
    NCLBUTTONDBLCLK = 0x00A3,
    NCRBUTTONDOWN = 0x00A4,
    NCRBUTTONUP = 0x00A5,
    NCRBUTTONDBLCLK = 0x00A6,
    NCMBUTTONDOWN = 0x00A7,
    NCMBUTTONUP = 0x00A8,
    NCMBUTTONDBLCLK = 0x00A9,
    NCXBUTTONDOWN = 0x00AB,
    NCXBUTTONUP = 0x00AC,
    NCXBUTTONDBLCLK = 0x00AD,

    INPUT_DEVICE_CHANGE = 0x00FE,
    INPUT = 0x00FF,

    KEYFIRST = 0x0100,
    KEYDOWN = 0x0100,
    KEYUP = 0x0101,
    CHAR = 0x0102,
    DEADCHAR = 0x0103,
    SYSKEYDOWN = 0x0104,
    SYSKEYUP = 0x0105,
    SYSCHAR = 0x0106,
    SYSDEADCHAR = 0x0107,
    UNICHAR = 0x0109,
    KEYLAST = 0x0109,

    IME_STARTCOMPOSITION = 0x010D,
    IME_ENDCOMPOSITION = 0x010E,
    IME_COMPOSITION = 0x010F,
    IME_KEYLAST = 0x010F,

    INITDIALOG = 0x0110,
    COMMAND = 0x0111,
    SYSCOMMAND = 0x0112,
    TIMER = 0x0113,
    HSCROLL = 0x0114,
    VSCROLL = 0x0115,
    INITMENU = 0x0116,
    INITMENUPOPUP = 0x0117,
    MENUSELECT = 0x011F,
    MENUCHAR = 0x0120,
    ENTERIDLE = 0x0121,
    MENURBUTTONUP = 0x0122,
    MENUDRAG = 0x0123,
    MENUGETOBJECT = 0x0124,
    UNINITMENUPOPUP = 0x0125,
    MENUCOMMAND = 0x0126,

    CHANGEUISTATE = 0x0127,
    UPDATEUISTATE = 0x0128,
    QUERYUISTATE = 0x0129,

    CTLCOLORMSGBOX = 0x0132,
    CTLCOLOREDIT = 0x0133,
    CTLCOLORLISTBOX = 0x0134,
    CTLCOLORBTN = 0x0135,
    CTLCOLORDLG = 0x0136,
    CTLCOLORSCROLLBAR = 0x0137,
    CTLCOLORSTATIC = 0x0138,
    MN_GETHMENU = 0x01E1,

    MOUSEMOVE = 0x0200,
    MOUSEFIRST = 0x0200,
    LBUTTONDOWN = 0x0201,
    LBUTTONUP = 0x0202,
    LBUTTONDBLCLK = 0x0203,
    RBUTTONDOWN = 0x0204,
    RBUTTONUP = 0x0205,
    RBUTTONDBLCLK = 0x0206,
    MBUTTONDOWN = 0x0207,
    MBUTTONUP = 0x0208,
    MBUTTONDBLCLK = 0x0209,
    MOUSEWHEEL = 0x020A,
    XBUTTONDOWN = 0x020B,
    XBUTTONUP = 0x020C,
    XBUTTONDBLCLK = 0x020D,
    MOUSEHWHEEL = 0x020E,

    PARENTNOTIFY = 0x0210,
    ENTERMENULOOP = 0x0211,
    EXITMENULOOP = 0x0212,

    NEXTMENU = 0x0213,
    SIZING = 0x0214,
    CAPTURECHANGED = 0x0215,
    MOVING = 0x0216,

    POWERBROADCAST = 0x0218,

    DEVICECHANGE = 0x0219,

    MDICREATE = 0x0220,
    MDIDESTROY = 0x0221,
    MDIACTIVATE = 0x0222,
    MDIRESTORE = 0x0223,
    MDINEXT = 0x0224,
    MDIMAXIMIZE = 0x0225,
    MDITILE = 0x0226,
    MDICASCADE = 0x0227,
    MDIICONARRANGE = 0x0228,
    MDIGETACTIVE = 0x0229,


    MDISETMENU = 0x0230,
    ENTERSIZEMOVE = 0x0231,
    EXITSIZEMOVE = 0x0232,
    DROPFILES = 0x0233,
    MDIREFRESHMENU = 0x0234,

    IME_SETCONTEXT = 0x0281,
    IME_NOTIFY = 0x0282,
    IME_CONTROL = 0x0283,
    IME_COMPOSITIONFULL = 0x0284,
    IME_SELECT = 0x0285,
    IME_CHAR = 0x0286,
    IME_REQUEST = 0x0288,
    IME_KEYDOWN = 0x0290,
    IME_KEYUP = 0x0291,

    MOUSEHOVER = 0x02A1,
    MOUSELEAVE = 0x02A3,
    NCMOUSEHOVER = 0x02A0,
    NCMOUSELEAVE = 0x02A2,

    WTSSESSION_CHANGE = 0x02B1,

    TABLET_FIRST = 0x02c0,
    TABLET_LAST = 0x02df,

    CUT = 0x0300,
    COPY = 0x0301,
    PASTE = 0x0302,
    CLEAR = 0x0303,
    UNDO = 0x0304,
    RENDERFORMAT = 0x0305,
    RENDERALLFORMATS = 0x0306,
    DESTROYCLIPBOARD = 0x0307,
    DRAWCLIPBOARD = 0x0308,
    PAINTCLIPBOARD = 0x0309,
    VSCROLLCLIPBOARD = 0x030A,
    SIZECLIPBOARD = 0x030B,
    ASKCBFORMATNAME = 0x030C,
    CHANGECBCHAIN = 0x030D,
    HSCROLLCLIPBOARD = 0x030E,
    QUERYNEWPALETTE = 0x030F,
    PALETTEISCHANGING = 0x0310,
    PALETTECHANGED = 0x0311,
    HOTKEY = 0x0312,

    PRINT = 0x0317,
    PRINTCLIENT = 0x0318,

    APPCOMMAND = 0x0319,

    THEMECHANGED = 0x031A,

    CLIPBOARDUPDATE = 0x031D,

    DWMCOMPOSITIONCHANGED = 0x031E,
    DWMNCRENDERINGCHANGED = 0x031F,
    DWMCOLORIZATIONCOLORCHANGED = 0x0320,
    DWMWINDOWMAXIMIZEDCHANGE = 0x0321,

    GETTITLEBARINFOEX = 0x033F,

    HANDHELDFIRST = 0x0358,
    HANDHELDLAST = 0x035F,

    AFXFIRST = 0x0360,
    AFXLAST = 0x037F,

    PENWINFIRST = 0x0380,
    PENWINLAST = 0x038F,

    APP = 0x8000,

    USER = 0x0400,

    REFLECT = USER + 0x1C00,
}
public static class WinApi
{

    [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CallingConvention = CallingConvention.StdCall)] public static extern int AllocConsole();

    [DllImport("kernel32.dll")] public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)] public static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("user32.dll", EntryPoint = "GetWindowLong")] public static extern IntPtr GetWindowLongPtr32(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")] public static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

    public static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
    {
        if (IntPtr.Size != 4)
            return GetWindowLongPtr64(hWnd, nIndex);
        else
            return GetWindowLongPtr32(hWnd, nIndex);
    }

    [DllImport("user32.dll", EntryPoint = "SetWindowLong")] public static extern IntPtr SetWindowLong32(IntPtr hWnd, int nIndex, IntPtr newValue);

    [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")] public static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr newValue);
    public static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr newValue)
    {
        if (IntPtr.Size != 4)
        {
            return SetWindowLongPtr64(hWnd, nIndex, newValue);
        }

        return SetWindowLong32(hWnd, nIndex, newValue);
    }

    [DllImport("user32.dll")] public static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

    public enum GWL
    {
        GWL_WNDPROC = -4,
        GWL_HINSTANCE = -6,
        GWL_HWNDPARENT = -8,
        GWL_STYLE = -16,
        GWL_EXSTYLE = -20,
        GWL_USERDATA = -21,
        GWL_ID = -12
    }

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Ansi)] public static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);

}

