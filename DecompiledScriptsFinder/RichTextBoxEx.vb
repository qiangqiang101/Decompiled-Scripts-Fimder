Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports System.Drawing.Drawing2D

<ToolboxBitmap(GetType(RichTextBox))>
Public Class RichTextBoxEx
    Inherits RichTextBox

    Private charFormat As User32.CHARFORMAT
    Private lParam1 As IntPtr
    Private _savedScrollLine As Integer
    Private _savedSelectionStart As Integer
    Private _savedSelectionEnd As Integer
    Private _borderPen As Pen
    Private _stringDrawingFormat As System.Drawing.StringFormat
    Private alg As System.Security.Cryptography.HashAlgorithm

    Public Sub New()
        charFormat = New User32.CHARFORMAT() With {
            .cbSize = Marshal.SizeOf(GetType(User32.CHARFORMAT)),
            .szFaceName = New Char(31) {}
        }
        lParam1 = Marshal.AllocCoTaskMem(charFormat.cbSize)
        NumberFont = New System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte((0))))
        NumberColor = Color.FromName("DarkGray")
        NumberLineCounting = LineCounting.CRLF
        NumberAlignment = StringAlignment.Center
        NumberBorder = SystemColors.ControlDark
        NumberBorderThickness = 1
        NumberPadding = 2
        NumberBackground1 = SystemColors.ControlLight
        NumberBackground2 = SystemColors.Window
        SetStringDrawingFormat()
        alg = System.Security.Cryptography.SHA1.Create()
    End Sub

    Protected Overrides Sub Finalize()
        Marshal.FreeCoTaskMem(lParam1)
    End Sub

    Private Sub SetStringDrawingFormat()
        _stringDrawingFormat = New System.Drawing.StringFormat With {
            .Alignment = StringAlignment.Center,
            .LineAlignment = NumberAlignment,
            .Trimming = StringTrimming.None
        }
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        NeedRecomputeOfLineNumbers()
        MyBase.OnTextChanged(e)
    End Sub

    Public Sub BeginUpdate()
        User32.SendMessage(Me.Handle, CInt(User32.Msgs.WM_SETREDRAW), 0, IntPtr.Zero)
    End Sub

    Public Sub EndUpdate()
        User32.SendMessage(Me.Handle, CInt(User32.Msgs.WM_SETREDRAW), 1, IntPtr.Zero)
    End Sub

    Public Function BeginUpdateAndSuspendEvents() As IntPtr
        User32.SendMessage(Me.Handle, CInt(User32.Msgs.WM_SETREDRAW), 0, IntPtr.Zero)
        Dim eventMask As IntPtr = User32.SendMessage(Me.Handle, User32.Msgs.EM_GETEVENTMASK, 0, IntPtr.Zero)
        Return eventMask
    End Function

    Public Sub EndUpdateAndResumeEvents(ByVal eventMask As IntPtr)
        User32.SendMessage(Me.Handle, User32.Msgs.EM_SETEVENTMASK, 0, eventMask)
        User32.SendMessage(Me.Handle, User32.Msgs.WM_SETREDRAW, 1, IntPtr.Zero)
        NeedRecomputeOfLineNumbers()
        Me.Invalidate()
    End Sub

    Public Sub GetSelection(<Out> ByRef start As Integer, <Out> ByRef [end] As Integer)
        User32.SendMessageRef(Me.Handle, CInt(User32.Msgs.EM_GETSEL), start, [end])
    End Sub

    Public Sub SetSelection(ByVal start As Integer, ByVal [end] As Integer)
        User32.SendMessage(Me.Handle, CInt(User32.Msgs.EM_SETSEL), start, [end])
    End Sub

    Public Sub BeginUpdateAndSaveState()
        User32.SendMessage(Me.Handle, CInt(User32.Msgs.WM_SETREDRAW), 0, IntPtr.Zero)
        _savedScrollLine = FirstVisibleDisplayLine
        GetSelection(_savedSelectionStart, _savedSelectionEnd)
    End Sub

    Public Sub EndUpdateAndRestoreState()
        Dim Line1 As Integer = FirstVisibleDisplayLine
        Scroll(_savedScrollLine - Line1)
        SetSelection(_savedSelectionStart, _savedSelectionEnd)
        User32.SendMessage(Me.Handle, CInt(User32.Msgs.WM_SETREDRAW), 1, IntPtr.Zero)
        Refresh()
    End Sub

    Private _sformat As String
    Private _ndigits As Integer
    Private _lnw As Integer = -1

    Private ReadOnly Property LineNumberWidth As Integer
        Get
            If _lnw > 0 Then Return _lnw

            If NumberLineCounting = LineCounting.CRLF Then
                _ndigits = If((CharIndexForTextLine.Length = 0), 1, CInt((1 + Math.Log(CDbl(CharIndexForTextLine.Length), 10))))
            Else
                Dim n As Integer = GetDisplayLineCount()
                _ndigits = If((n = 0), 1, CInt((1 + Math.Log(CDbl(n), 10))))
            End If

            Dim s = New String("0"c, _ndigits)
            Dim b = New Bitmap(400, 400)
            Dim g = Graphics.FromImage(b)
            Dim size As SizeF = g.MeasureString(s, NumberFont)
            g.Dispose()
            _lnw = NumberPadding * 2 + 4 + CInt((size.Width + 0.5 + NumberBorderThickness))
            _sformat = "{0:D" & _ndigits & "}"
            Return _lnw
        End Get
    End Property

    Public _lineNumbers As Boolean

    Public Property ShowLineNumbers As Boolean
        Get
            Return _lineNumbers
        End Get
        Set(ByVal value As Boolean)
            If value = _lineNumbers Then Return
            SetLeftMargin(If(value, LineNumberWidth + Margin.Left, Margin.Left))
            _lineNumbers = value
            User32.SendMessage(Me.Handle, User32.Msgs.WM_PAINT, 0, 0)
        End Set
    End Property

    Private Sub NeedRecomputeOfLineNumbers()
        _CharIndexForTextLine = Nothing
        _Text2 = Nothing
        _lnw = -1
        If _paintingDisabled Then Return
        User32.SendMessage(Me.Handle, User32.Msgs.WM_PAINT, 0, 0)
    End Sub

    Private _NumberFont As Font

    Public Property NumberFont As Font
        Get
            Return _NumberFont
        End Get
        Set(ByVal value As Font)
            If _NumberFont Is value Then Return
            _lnw = -1
            _NumberFont = value
            User32.SendMessage(Me.Handle, User32.Msgs.WM_PAINT, 0, 0)
        End Set
    End Property

    Private _NumberLineCounting As LineCounting

    Public Property NumberLineCounting As LineCounting
        Get
            Return _NumberLineCounting
        End Get
        Set(ByVal value As LineCounting)
            If _NumberLineCounting = value Then Return
            _lnw = -1
            _NumberLineCounting = value
            User32.SendMessage(Me.Handle, User32.Msgs.WM_PAINT, 0, 0)
        End Set
    End Property

    Private _NumberAlignment As StringAlignment

    Public Property NumberAlignment As StringAlignment
        Get
            Return _NumberAlignment
        End Get
        Set(ByVal value As StringAlignment)
            If _NumberAlignment = value Then Return
            _NumberAlignment = value
            SetStringDrawingFormat()
            User32.SendMessage(Me.Handle, User32.Msgs.WM_PAINT, 0, 0)
        End Set
    End Property

    Private _NumberColor As Color

    Public Property NumberColor As Color
        Get
            Return _NumberColor
        End Get
        Set(ByVal value As Color)
            If _NumberColor.ToArgb() = value.ToArgb() Then Return
            _NumberColor = value
            User32.SendMessage(Me.Handle, User32.Msgs.WM_PAINT, 0, 0)
        End Set
    End Property

    Private _NumberLeadingZeroes As Boolean

    Public Property NumberLeadingZeroes As Boolean
        Get
            Return _NumberLeadingZeroes
        End Get
        Set(ByVal value As Boolean)
            If _NumberLeadingZeroes = value Then Return
            _NumberLeadingZeroes = value
            User32.SendMessage(Me.Handle, User32.Msgs.WM_PAINT, 0, 0)
        End Set
    End Property

    Private _NumberBorder As Color

    Public Property NumberBorder As Color
        Get
            Return _NumberBorder
        End Get
        Set(ByVal value As Color)
            If _NumberBorder.ToArgb() = value.ToArgb() Then Return
            _NumberBorder = value
            NewBorderPen()
            User32.SendMessage(Me.Handle, User32.Msgs.WM_PAINT, 0, 0)
        End Set
    End Property

    Private _NumberPadding As Integer

    Public Property NumberPadding As Integer
        Get
            Return _NumberPadding
        End Get
        Set(ByVal value As Integer)
            If _NumberPadding = value Then Return
            _lnw = -1
            _NumberPadding = value
            User32.SendMessage(Me.Handle, User32.Msgs.WM_PAINT, 0, 0)
        End Set
    End Property

    Public _NumberBorderThickness As Single

    Public Property NumberBorderThickness As Single
        Get
            Return _NumberBorderThickness
        End Get
        Set(ByVal value As Single)
            If _NumberBorderThickness = value Then Return
            _lnw = -1
            _NumberBorderThickness = value
            NewBorderPen()
            User32.SendMessage(Me.Handle, User32.Msgs.WM_PAINT, 0, 0)
        End Set
    End Property

    Private _NumberBackground1 As Color

    Public Property NumberBackground1 As Color
        Get
            Return _NumberBackground1
        End Get
        Set(ByVal value As Color)
            If _NumberBackground1.ToArgb() = value.ToArgb() Then Return
            _NumberBackground1 = value
            User32.SendMessage(Me.Handle, User32.Msgs.WM_PAINT, 0, 0)
        End Set
    End Property

    Private _NumberBackground2 As Color

    Public Property NumberBackground2 As Color
        Get
            Return _NumberBackground2
        End Get
        Set(ByVal value As Color)
            If _NumberBackground2.ToArgb() = value.ToArgb() Then Return
            _NumberBackground2 = value
            User32.SendMessage(Me.Handle, User32.Msgs.WM_PAINT, 0, 0)
        End Set
    End Property

    Private _paintingDisabled As Boolean

    Public Sub SuspendLineNumberPainting()
        _paintingDisabled = True
    End Sub

    Public Sub ResumeLineNumberPainting()
        _paintingDisabled = False
    End Sub

    Private Sub NewBorderPen()
        _borderPen = New Pen(NumberBorder)
        _borderPen.Width = NumberBorderThickness
        _borderPen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round)
    End Sub

    Private _lastMsgRecd As DateTime = New DateTime(1901, 1, 1)

    Protected Overrides Sub WndProc(ByRef m As Message)
        Dim handled As Boolean = False

        Select Case m.Msg
            Case CInt(User32.Msgs.WM_PAINT)
                If _paintingDisabled Then Return

                If _lineNumbers Then
                    MyBase.WndProc(m)
                    Me.PaintLineNumbers()
                    handled = True
                End If

            Case CInt(User32.Msgs.WM_CHAR)
                NeedRecomputeOfLineNumbers()
        End Select

        If Not handled Then MyBase.WndProc(m)
    End Sub

    Private _lastWidth As Integer = 0

    Private Sub PaintLineNumbers()
        If _paintingDisabled Then Return
        Dim w As Integer = LineNumberWidth

        If w <> _lastWidth Then
            SetLeftMargin(w + Margin.Left)
            _lastWidth = w
            User32.SendMessage(Me.Handle, User32.Msgs.WM_PAINT, 0, 0)
            Return
        End If

        Dim buffer As Bitmap = New Bitmap(w, Me.Bounds.Height)
        Dim g As Graphics = Graphics.FromImage(buffer)
        Dim forebrush As Brush = New SolidBrush(NumberColor)
        Dim rect = New Rectangle(0, 0, w, Me.Bounds.Height)
        Dim wantDivider As Boolean = NumberBackground1.ToArgb() = NumberBackground2.ToArgb()
        Dim backBrush As Brush = If((wantDivider), CType(New SolidBrush(NumberBackground2), Brush), SystemBrushes.Window)
        g.FillRectangle(backBrush, rect)
        Dim n As Integer = If((NumberLineCounting = LineCounting.CRLF), NumberOfVisibleTextLines, NumberOfVisibleDisplayLines)
        Dim first As Integer = If((NumberLineCounting = LineCounting.CRLF), FirstVisibleTextLine, FirstVisibleDisplayLine + 1)
        Dim py As Integer = 0
        Dim w2 As Integer = w - 2 - CInt(NumberBorderThickness)
        Dim brush As LinearGradientBrush
        Dim dividerPen As Pen = New Pen(NumberColor)

        For i As Integer = 0 To n
            Dim ix As Integer = first + i
            Dim c As Integer = If((NumberLineCounting = LineCounting.CRLF), GetCharIndexForTextLine(ix), GetCharIndexForDisplayLine(ix) - 1)
            Dim p = GetPosFromCharIndex(c + 1)
            Dim r4 As Rectangle = Rectangle.Empty

            If i = n Then
                If Me.Bounds.Height <= py Then Continue For
                r4 = New Rectangle(1, py, w2, Me.Bounds.Height - py)
            Else
                If p.Y <= py Then Continue For
                r4 = New Rectangle(1, py, w2, p.Y - py)
            End If

            If wantDivider Then
                If i <> n Then g.DrawLine(dividerPen, 1, p.Y + 1, w2, p.Y + 1)
            Else
                brush = New LinearGradientBrush(r4, NumberBackground1, NumberBackground2, LinearGradientMode.Vertical)
                g.FillRectangle(brush, r4)
            End If

            If NumberLineCounting = LineCounting.CRLF Then ix += 1
            If NumberAlignment = StringAlignment.Near Then rect.Offset(0, 3)
            Dim s = If((NumberLeadingZeroes), String.Format(_sformat, ix), ix.ToString())
            g.DrawString(s, NumberFont, forebrush, r4, _stringDrawingFormat)
            py = p.Y
        Next

        If NumberBorderThickness <> 0.0 Then
            Dim t As Integer = CInt((w - (NumberBorderThickness + 0.5) / 2)) - 1
            g.DrawLine(_borderPen, t, 0, t, Me.Bounds.Height)
        End If

        Dim g1 As Graphics = Me.CreateGraphics()
        g1.DrawImage(buffer, New Point(0, 0))
        g1.Dispose()
        g.Dispose()
    End Sub

    Private Function GetCharIndexFromPos(ByVal x As Integer, ByVal y As Integer) As Integer
        Dim p = New User32.POINTL With {
            .X = x,
            .Y = y
        }
        Dim rawSize As Integer = Marshal.SizeOf(GetType(User32.POINTL))
        Dim lParam As IntPtr = Marshal.AllocHGlobal(rawSize)
        Marshal.StructureToPtr(p, lParam, False)
        Dim r As Integer = User32.SendMessage(Me.Handle, CInt(User32.Msgs.EM_CHARFROMPOS), 0, lParam)
        Marshal.FreeHGlobal(lParam)
        Return r
    End Function

    Private Function GetPosFromCharIndex(ByVal ix As Integer) As Point
        Dim rawSize As Integer = Marshal.SizeOf(GetType(User32.POINTL))
        Dim wParam As IntPtr = Marshal.AllocHGlobal(rawSize)
        Dim r As Integer = User32.SendMessage(Me.Handle, CInt(User32.Msgs.EM_POSFROMCHAR), CInt(wParam), ix)
        Dim p1 As User32.POINTL = CType(Marshal.PtrToStructure(wParam, GetType(User32.POINTL)), User32.POINTL)
        Marshal.FreeHGlobal(wParam)
        Dim p = New Point With {
            .X = p1.X,
            .Y = p1.Y
        }
        Return p
    End Function

    Private Function GetLengthOfLineContainingChar(ByVal charIndex As Integer) As Integer
        Dim r As Integer = User32.SendMessage(Me.Handle, CInt(User32.Msgs.EM_LINELENGTH), 0, 0)
        Return r
    End Function

    Private Function GetLineFromChar(ByVal charIndex As Integer) As Integer
        Return User32.SendMessage(Me.Handle, CInt(User32.Msgs.EM_LINEFROMCHAR), charIndex, 0)
    End Function

    Private Function GetCharIndexForDisplayLine(ByVal line As Integer) As Integer
        Return User32.SendMessage(Me.Handle, CInt(User32.Msgs.EM_LINEINDEX), line, 0)
    End Function

    Private Function GetDisplayLineCount() As Integer
        Return User32.SendMessage(Me.Handle, CInt(User32.Msgs.EM_GETLINECOUNT), 0, 0)
    End Function

    Public Sub SetSelectionColor(ByVal start As Integer, ByVal [end] As Integer, ByVal color As System.Drawing.Color)
        User32.SendMessage(Me.Handle, CInt(User32.Msgs.EM_SETSEL), start, [end])
        charFormat.dwMask = &H40000000
        charFormat.dwEffects = 0
        charFormat.crTextColor = System.Drawing.ColorTranslator.ToWin32(color)
        Marshal.StructureToPtr(charFormat, lParam1, False)
        User32.SendMessage(Me.Handle, CInt(User32.Msgs.EM_SETCHARFORMAT), User32.SCF_SELECTION, lParam1)
    End Sub

    Private Sub SetLeftMargin(ByVal widthInPixels As Integer)
        User32.SendMessage(Me.Handle, CInt(User32.Msgs.EM_SETMARGINS), User32.EC_LEFTMARGIN, widthInPixels)
    End Sub

    'Got Error

    Public Sub Scroll(ByVal delta As Integer)
        User32.SendMessage(Me.Handle, CInt(User32.Msgs.EM_LINESCROLL), 0, delta)
    End Sub

    Private Property FirstVisibleDisplayLine As Integer
        Get
            Return User32.SendMessage(Me.Handle, CInt(User32.Msgs.EM_GETFIRSTVISIBLELINE), 0, 0)
        End Get
        Set(ByVal value As Integer)
            Dim current As Integer = FirstVisibleDisplayLine
            Dim delta As Integer = value - current
            User32.SendMessage(Me.Handle, CInt(User32.Msgs.EM_LINESCROLL), 0, delta)
        End Set
    End Property

    Private ReadOnly Property NumberOfVisibleDisplayLines As Integer
        Get
            Dim topIndex As Integer = Me.GetCharIndexFromPosition(New System.Drawing.Point(1, 1))
            Dim bottomIndex As Integer = Me.GetCharIndexFromPosition(New System.Drawing.Point(1, Me.Height - 1))
            Dim topLine As Integer = Me.GetLineFromCharIndex(topIndex)
            Dim bottomLine As Integer = Me.GetLineFromCharIndex(bottomIndex)
            Dim n As Integer = bottomLine - topLine + 1
            Return n
        End Get
    End Property

    Private ReadOnly Property FirstVisibleTextLine As Integer
        Get
            Dim c As Integer = GetCharIndexFromPos(1, 1)

            For i As Integer = 0 To CharIndexForTextLine.Length - 1
                If c < CharIndexForTextLine(i) Then Return i
            Next

            Return CharIndexForTextLine.Length
        End Get
    End Property

    Private ReadOnly Property LastVisibleTextLine As Integer
        Get
            Dim c As Integer = GetCharIndexFromPos(1, Me.Bounds.Y + Me.Bounds.Height)

            For i As Integer = 0 To CharIndexForTextLine.Length - 1
                If c < CharIndexForTextLine(i) Then Return i
            Next

            Return CharIndexForTextLine.Length
        End Get
    End Property

    Private ReadOnly Property NumberOfVisibleTextLines As Integer
        Get
            Return LastVisibleTextLine - FirstVisibleTextLine
        End Get
    End Property

    Public ReadOnly Property FirstVisibleLine As Integer
        Get

            If Me.NumberLineCounting = LineCounting.CRLF Then
                Return FirstVisibleTextLine
            Else
                Return FirstVisibleDisplayLine
            End If
        End Get
    End Property

    Public ReadOnly Property NumberOfVisibleLines As Integer
        Get

            If Me.NumberLineCounting = LineCounting.CRLF Then
                Return NumberOfVisibleTextLines
            Else
                Return NumberOfVisibleDisplayLines
            End If
        End Get
    End Property

    Private Function GetCharIndexForTextLine(ByVal ix As Integer) As Integer
        If ix >= CharIndexForTextLine.Length Then Return 0
        If ix < 0 Then Return 0
        Return CharIndexForTextLine(ix)
    End Function

    Private _CharIndexForTextLine As Integer()

    Private ReadOnly Property CharIndexForTextLine As Integer()
        Get

            If _CharIndexForTextLine Is Nothing Then
                Dim list = New List(Of Integer)()
                Dim ix As Integer = 0

                For Each c In Text2
                    If c = vbLf Then list.Add(ix)
                    ix += 1
                Next

                _CharIndexForTextLine = list.ToArray()
            End If

            Return _CharIndexForTextLine
        End Get
    End Property

    Private _Text2 As String

    Private ReadOnly Property Text2 As String
        Get
            If _Text2 Is Nothing Then _Text2 = Me.Text
            Return _Text2
        End Get
    End Property

    Private Function CompareHashes(ByVal a As Byte(), ByVal b As Byte()) As Boolean
        If a.Length <> b.Length Then Return False

        For i As Integer = 0 To a.Length - 1
            If a(i) <> b(i) Then Return False
        Next

        Return True
    End Function

    Public Enum LineCounting
        CRLF
        AsDisplayed
    End Enum
End Class

Module Tuple
    Function [New](Of T1, T2)(ByVal v1 As T1, ByVal v2 As T2) As Tuple(Of T1, T2)
        Return New Tuple(Of T1, T2)(v1, v2)
    End Function
End Module

'Public Class Tuple(Of T1, T2)
'    Public Sub New(ByVal v1 As T1, ByVal v2 As T2)
'        v1 = v1
'        v2 = v2
'    End Sub

'    Public Property V1 As T1
'    Public Property V2 As T2
'End Class
