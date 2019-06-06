﻿Imports System.ComponentModel
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading

Public Class frmMain

    Dim findThread As New Thread(AddressOf FindWorkThread)

    Public Declare Unicode Function SetWindowTheme Lib "uxtheme.dll" (ByVal hWnd As IntPtr, ByVal pszSubAppName As String, ByVal pszSubIdList As String) As Integer

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        If btnFind.Text = "Find" Then
            If txtDir.Text = Nothing Then
                txtDir.Text = Application.StartupPath
            ElseIf txtStr.Text = Nothing Then
                txtStr.Text = "I'm Not MentaL"
            Else
                findThread = New Thread(AddressOf FindWorkThread)
                findThread.Start()
            End If
        Else
            findThread.Abort()
        End If
    End Sub

    Private Sub FindWorkThread()
        Dim abort As Boolean = False
        Dim fileNum As Integer = 0
        Dim curFile As Integer = 0, withMatch As Integer = 0, woMatch As Integer = 0, matches As Integer = 0

        Try
            ControlsEnabler(False)
            btnFind.Text = "Cancel"
            lvResult.Items.Clear()

            Dim files As String() = Directory.GetFiles(txtDir.Text)
            fileNum = files.Length
            pbProgress.Maximum = fileNum

            For Each file As String In files
                curFile += 1
                Dim lineNum As Integer = 0
                Dim flag As Boolean = False

                Dim lines As String() = IO.File.ReadAllLines(file)
                For Each line As String In lines
                    lineNum += 1

                    If line.ToLower.Contains(txtStr.Text.ToLower) Then
                        flag = True
                        matches += 1

                        Dim num6 As Integer = 0
                        Dim lvi As New ListViewItem(lineNum)
                        With lvi
                            .SubItems.Add(Path.GetFileName(file))
                            .SubItems.Add(line)
                        End With
                        lvResult.Items.Add(lvi)
                    End If
                Next

                If flag Then
                    withMatch += 1
                Else
                    woMatch += 1
                End If

                pbProgress.Value = curFile
                lblStatus.Text = $"Processing {curFile} of {fileNum} files. Last file: {Path.GetFileName(file)}"
            Next
        Catch tae As ThreadAbortException
            Thread.ResetAbort()
            abort = True
        Catch ex As Exception
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        Finally
            If Not abort Then
                Dim sb As New StringBuilder()
                sb.AppendLine("Files: ")
                sb.AppendLine($"- Total: {fileNum}")
                sb.AppendLine($"- Processed: {curFile}")
                sb.AppendLine($"- With Matches: {withMatch}")
                sb.AppendLine($"- Without Matches: {woMatch}")
                sb.AppendLine(vbNewLine)
                sb.AppendLine($"Matches Found: {matches}")

                MsgBox($"{sb.ToString()}", MsgBoxStyle.Exclamation, "Task Completed")
            End If
            ControlsEnabler(True)
            btnFind.Text = "Find"
        End Try
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        SetWindowTheme(lvResult.Handle, "explorer", Nothing)
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Try
            Dim fsd = New FolderSelectDialog()
            fsd.Title = "Select Directory..."
            fsd.InitialDirectory = Application.StartupPath

            If fsd.ShowDialog(IntPtr.Zero) Then
                txtDir.Text = fsd.FileName
            End If
        Catch ex As Exception
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Public Sub ControlsEnabler(enable As Boolean)
        txtDir.Enabled = enable
        txtStr.Enabled = enable
        btnBrowse.Enabled = enable
    End Sub

    Private Sub lvResult_DoubleClick(sender As Object, e As EventArgs) Handles lvResult.DoubleClick
        Try
            Dim file As String = Path.Combine(txtDir.Text, lvResult.SelectedItems(0).SubItems(1).Text)
            Process.Start(file)
        Catch ex As Exception
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub frmMain_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If findThread.IsAlive Then
            Dim result As DialogResult = MessageBox.Show("Task is Running, do you want to Abort?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Stop)
            If result = DialogResult.Yes Then
                findThread.Abort()
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub lvResult_Click(sender As Object, e As EventArgs) Handles lvResult.Click
        Try
            Dim line As Integer = CInt(lvResult.SelectedItems(0).SubItems(0).Text) - 1
            Dim file As String = Path.Combine(txtDir.Text, lvResult.SelectedItems(0).SubItems(1).Text)
            Dim text As String = lvResult.SelectedItems(0).SubItems(2).Text

            rtbText.Clear()
            rtbText.Text = IO.File.ReadAllText(file)

            rtbText.SelectionStart = rtbText.Find(rtbText.Lines(line), 0, RichTextBoxFinds.None)
            rtbText.Scroll(line)
            rtbText.SelectionBackColor = Color.LightBlue
            rtbText.Focus()
        Catch ex As Exception
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
End Class
