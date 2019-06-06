Imports System.Runtime.InteropServices

Public Module User32

    Public Enum Styles : uint
        WS_OVERLAPPED = &H00000000
        WS_POPUP = &H80000000
        WS_CHILD = &H40000000
        WS_MINIMIZE = &H20000000
        WS_VISIBLE = &H10000000
        WS_DISABLED = &H08000000
        WS_CLIPSIBLINGS = &H04000000
        WS_CLIPCHILDREN = &H02000000
        WS_MAXIMIZE = &H01000000
        WS_CAPTION = &H00C00000
        WS_BORDER = &H00800000
        WS_DLGFRAME = &H00400000
        WS_VSCROLL = &H00200000
        WS_HSCROLL = &H00100000
        WS_SYSMENU = &H00080000
        WS_THICKFRAME = &H00040000
        WS_GROUP = &H00020000
        WS_TABSTOP = &H00010000
        GWL_STYLE = &HFFFFFFF0
    End Enum

    Public Enum Msgs
        ' GetWindow
        GW_HWNDFIRST = 0
        GW_HWNDLAST = 1
        GW_HWNDNEXT = 2
        GW_HWNDPREV = 3
        GW_OWNER = 4
        GW_CHILD = 5

        ' Window messages - WinUser.h
        WM_NULL = &H0000
        WM_CREATE = &H0001
        WM_DESTROY = &H0002
        WM_MOVE = &H0003
        WM_SIZE = &H0005
        WM_KILLFOCUS = &H0008
        WM_SETREDRAW = &H000B
        WM_GETTEXT = &H000D
        WM_GETTEXTLENGTH = &H000E
        WM_PAINT = &H000F
        WM_ERASEBKGND = &H0014
        WM_SHOWWINDOW = &H0018

        WM_FONTCHANGE = &H001D
        WM_SETCURSOR = &H0020
        WM_MOUSEACTIVATE = &H0021
        WM_CHILDACTIVATE = &H0022

        WM_DRAWITEM = &H002B
        WM_MEASUREITEM = &H002C
        WM_DELETEITEM = &H002D
        WM_VKEYTOITEM = &H002E
        WM_CHARTOITEM = &H002F

        WM_SETFONT = &H0030
        WM_COMPAREITEM = &H0039
        WM_WINDOWPOSCHANGING = &H0046
        WM_WINDOWPOSCHANGED = &H0047
        WM_NOTIFY = &H004E
        WM_NOTIFYFORMAT = &H0055
        WM_STYLECHANGING = &H007C
        WM_STYLECHANGED = &H007D
        WM_NCMOUSEMOVE = &H00A0
        WM_NCLBUTTONDOWN = &H00A1

        WM_NCCREATE = &H0081
        WM_NCDESTROY = &H0082
        WM_NCCALCSIZE = &H0083
        WM_NCHITTEST = &H0084
        WM_NCPAINT = &H0085
        WM_GETDLGCODE = &H0087

        ' from WinUser.h And RichEdit.h
        EM_GETSEL = &H00B0
        EM_SETSEL = &H00B1
        EM_GETRECT = &H00B2
        EM_SETRECT = &H00B3
        EM_SETRECTNP = &H00B4
        EM_SCROLL = &H00B5
        EM_LINESCROLL = &H00B6
        'EM_SCROLLCARET       = &H00B7
        EM_GETMODIFY = &H00B8
        EM_SETMODIFY = &H00B9
        EM_GETLINECOUNT = &H00BA
        EM_LINEINDEX = &H00BB
        EM_SETHANDLE = &H00BC
        EM_GETHANDLE = &H00BD
        EM_GETTHUMB = &H00BE
        EM_LINELENGTH = &H00C1
        EM_LINEFROMCHAR = &H00C9
        EM_GETFIRSTVISIBLELINE = &H00CE
        EM_SETMARGINS = &H00D3
        EM_GETMARGINS = &H00D4
        EM_POSFROMCHAR = &H00D6
        EM_CHARFROMPOS = &H00D7

        WM_KEYFIRST = &H0100
        WM_KEYDOWN = &H0100
        WM_KEYUP = &H0101
        WM_CHAR = &H0102
        WM_DEADCHAR = &H0103
        WM_SYSKEYDOWN = &H0104
        WM_SYSKEYUP = &H0105
        WM_SYSCHAR = &H0106
        WM_SYSDEADCHAR = &H0107

        WM_COMMAND = &H0111
        WM_SYSCOMMAND = &H0112
        WM_TIMER = &H0113
        WM_HSCROLL = &H0114
        WM_VSCROLL = &H0115
        WM_UPDATEUISTATE = &H0128
        WM_QUERYUISTATE = &H0129
        WM_MOUSEFIRST = &H0200
        WM_MOUSEMOVE = &H0200
        WM_LBUTTONDOWN = &H0201
        WM_LBUTTONUP = &H0202
        WM_PARENTNOTIFY = &H0210

        WM_NEXTMENU = &H0213
        WM_SIZING = &H0214
        WM_CAPTURECHANGED = &H0215
        WM_MOVING = &H0216

        WM_IME_SETCONTEXT = &H0281
        WM_IME_NOTIFY = &H0282
        WM_IME_CONTROL = &H0283
        WM_IME_COMPOSITIONFULL = &H0284
        WM_IME_SELECT = &H0285
        WM_IME_CHAR = &H0286
        WM_IME_REQUEST = &H0288
        WM_IME_KEYDOWN = &H0290
        WM_IME_KEYUP = &H0291
        WM_NCMOUSEHOVER = &H02A0
        WM_NCMOUSELEAVE = &H02A2
        WM_MOUSEHOVER = &H02A1
        WM_MOUSELEAVE = &H02A3

        WM_CUT = &H0300
        WM_COPY = &H0301
        WM_PASTE = &H0302
        WM_CLEAR = &H0303
        WM_UNDO = &H0304
        WM_RENDERFORMAT = &H0305
        WM_RENDERALLFORMATS = &H0306
        WM_DESTROYCLIPBOARD = &H0307
        WM_DRAWCLIPBOARD = &H0308
        WM_PAINTCLIPBOARD = &H0309
        WM_VSCROLLCLIPBOARD = &H030A
        WM_SIZECLIPBOARD = &H030B
        WM_ASKCBFORMATNAME = &H030C
        WM_CHANGECBCHAIN = &H030D
        WM_HSCROLLCLIPBOARD = &H030E
        WM_QUERYNEWPALETTE = &H030F
        WM_PALETTEISCHANGING = &H0310
        WM_PALETTECHANGED = &H0311
        WM_HOTKEY = &H0312

        WM_USER = &H0400
        EM_SCROLLCARET = (WM_USER + 49)

        EM_CANPASTE = (WM_USER + 50)
        EM_DISPLAYBAND = (WM_USER + 51)
        EM_EXGETSEL = (WM_USER + 52)
        EM_EXLIMITTEXT = (WM_USER + 53)
        EM_EXLINEFROMCHAR = (WM_USER + 54)
        EM_EXSETSEL = (WM_USER + 55)
        EM_FINDTEXT = (WM_USER + 56)
        EM_FORMATRANGE = (WM_USER + 57)
        EM_GETCHARFORMAT = (WM_USER + 58)
        EM_GETEVENTMASK = (WM_USER + 59)
        EM_GETOLEINTERFACE = (WM_USER + 60)
        EM_GETPARAFORMAT = (WM_USER + 61)
        EM_GETSELTEXT = (WM_USER + 62)
        EM_HIDESELECTION = (WM_USER + 63)
        EM_PASTESPECIAL = (WM_USER + 64)
        EM_REQUESTRESIZE = (WM_USER + 65)
        EM_SELECTIONTYPE = (WM_USER + 66)
        EM_SETBKGNDCOLOR = (WM_USER + 67)
        EM_SETCHARFORMAT = (WM_USER + 68)
        EM_SETEVENTMASK = (WM_USER + 69)
        EM_SETOLECALLBACK = (WM_USER + 70)
        EM_SETPARAFORMAT = (WM_USER + 71)
        EM_SETTARGETDEVICE = (WM_USER + 72)
        EM_STREAMIN = (WM_USER + 73)
        EM_STREAMOUT = (WM_USER + 74)
        EM_GETTEXTRANGE = (WM_USER + 75)
        EM_FINDWORDBREAK = (WM_USER + 76)
        EM_SETOPTIONS = (WM_USER + 77)
        EM_GETOPTIONS = (WM_USER + 78)
        EM_FINDTEXTEX = (WM_USER + 79)

        ' Tab Control Messages - CommCtrl.h
        TCM_DELETEITEM = &H1308
        TCM_INSERTITEM = &H133E
        TCM_GETITEMRECT = &H130A
        TCM_GETCURSEL = &H130B
        TCM_SETCURSEL = &H130C
        TCM_ADJUSTRECT = &H1328
        TCM_SETITEMSIZE = &H1329
        TCM_SETPADDING = &H132B

        ' olectl.h
        OCM__BASE = (WM_USER + &H1C00)
        OCM_COMMAND = (OCM__BASE + WM_COMMAND)
        OCM_DRAWITEM = (OCM__BASE + WM_DRAWITEM)
        OCM_MEASUREITEM = (OCM__BASE + WM_MEASUREITEM)
        OCM_DELETEITEM = (OCM__BASE + WM_DELETEITEM)
        OCM_VKEYTOITEM = (OCM__BASE + WM_VKEYTOITEM)
        OCM_CHARTOITEM = (OCM__BASE + WM_CHARTOITEM)
        OCM_COMPAREITEM = (OCM__BASE + WM_COMPAREITEM)
        OCM_HSCROLL = (OCM__BASE + WM_HSCROLL)
        OCM_VSCROLL = (OCM__BASE + WM_VSCROLL)
        OCM_PARENTNOTIFY = (OCM__BASE + WM_PARENTNOTIFY)
        OCM_NOTIFY = (OCM__BASE + WM_NOTIFY)
    End Enum

    Public Const SCF_SELECTION As Integer = &H0001
    Public Const EC_LEFTMARGIN As Integer = &H0001
    Public Const EC_RIGHTMARGIN As Integer = &H0002

    <Flags>
    Public Enum Flags
        ' SetWindowPos Flags - WinUser.h
        SWP_NOSIZE = &H0001
        SWP_NOMOVE = &H0002
        SWP_NOZORDER = &H0004
        SWP_NOREDRAW = &H0008
        SWP_NOACTIVATE = &H0010
        SWP_FRAMECHANGED = &H0020
        SWP_SHOWWINDOW = &H0040
        SWP_HIDEWINDOW = &H0080
        SWP_NOCOPYBITS = &H0100
        SWP_NOOWNERZORDER = &H0200
        SWP_NOSENDCHANGING = &H0400
    End Enum

    Private tmsgs As Type = GetType(Msgs)

    Public Function Mnemonic(ByVal z As Integer) As String
        For Each ix As Integer In [Enum].GetValues(tmsgs)
            If z = ix Then Return [Enum].GetName(tmsgs, ix)
        Next

        Return z.ToString("X4")
    End Function

    <StructLayout(LayoutKind.Sequential)>
    Public Structure WINDOWPOS
        Public hwnd, hwndInsertAfter As IntPtr
        Public x, y, cx, cy, flags As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)>
    Public Structure STYLESTRUCT
        Public styleOld As Integer
        Public styleNew As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)>
    Public Structure CREATESTRUCT
        Public lpCreateParams As IntPtr
        Public hInstance As IntPtr
        Public hMenu As IntPtr
        Public hwndParent As IntPtr
        Public cy As Integer
        Public cx As Integer
        Public y As Integer
        Public x As Integer
        Public style As Integer
        Public lpszName As String
        Public lpszClass As String
        Public dwExStyle As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Public Structure CHARFORMAT
        Public cbSize As Integer
        Public dwMask As UInt32
        Public dwEffects As UInt32
        Public yHeight As Int32
        Public yOffset As Int32
        Public crTextColor As Int32
        Public bCharSet As Byte
        Public bPitchAndFamily As Byte
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)>
        Public szFaceName As Char()
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Public Structure POINTL
        Public X As Int32
        Public Y As Int32
    End Structure

    Public Sub BeginUpdate(ByVal hWnd As IntPtr)
        SendMessage(hWnd, CInt(Msgs.WM_SETREDRAW), 0, IntPtr.Zero)
    End Sub

    Public Sub EndUpdate(ByVal hWnd As IntPtr)
        SendMessage(hWnd, CInt(Msgs.WM_SETREDRAW), 1, IntPtr.Zero)
    End Sub

    <DllImport("User32.dll", EntryPoint:="SendMessage", CharSet:=CharSet.Auto)>
    Public Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    End Function
    <DllImport("User32.dll", EntryPoint:="SendMessage", CharSet:=CharSet.Auto)>
    Public Function SendMessage(ByVal hWnd As IntPtr,
    <MarshalAs(UnmanagedType.I4)> ByVal msg As Msgs, ByVal wParam As Integer, ByVal lParam As IntPtr) As IntPtr
    End Function
    <DllImport("User32.dll", EntryPoint:="SendMessage", CharSet:=CharSet.Auto)>
    Public Function SendMessage(ByVal hWnd As IntPtr,
    <MarshalAs(UnmanagedType.I4)> ByVal msg As Msgs, ByVal wParam As Integer, ByVal lParam As Integer) As IntPtr
    End Function
    <DllImport("User32.dll", EntryPoint:="SendMessage", CharSet:=CharSet.Auto)>
    Public Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wparam As Integer, ByVal lparam As IntPtr) As Integer
    End Function
    <DllImport("User32.dll", EntryPoint:="SendMessage", CharSet:=CharSet.Auto)>
    Public Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wparam As Integer, ByVal lparam As Integer) As Integer
    End Function
    <DllImport("User32.dll", EntryPoint:="SendMessage", CharSet:=CharSet.Auto)>
    Public Function SendMessageRef(ByVal hWnd As IntPtr, ByVal msg As Integer, <Out> ByRef wparam As Integer, <Out> ByRef lparam As Integer) As Integer
    End Function
    <DllImport("User32.dll", CharSet:=CharSet.Auto)>
    Public Function GetWindow(ByVal hWnd As IntPtr, ByVal uCmd As Integer) As IntPtr
    End Function
    <DllImport("User32.dll", CharSet:=CharSet.Auto)>
    Public Function GetClassName(ByVal hWnd As IntPtr, ByVal className As Char(), ByVal maxCount As Integer) As Integer
    End Function
    <DllImport("user32.dll")>
    Public Function SetWindowPos(ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As UInteger) As Integer
    End Function
    <DllImport("user32.dll", SetLastError:=True)>
    Public Function GetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As UInteger) As UInteger
    End Function

    <DllImport("user32.dll")>
    Public Function SetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As UInteger, ByVal dwNewLong As UInteger) As Integer
    End Function


End Module
