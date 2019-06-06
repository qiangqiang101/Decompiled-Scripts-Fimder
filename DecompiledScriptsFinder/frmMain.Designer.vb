<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.txtDir = New System.Windows.Forms.TextBox()
        Me.txtStr = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.lvResult = New System.Windows.Forms.ListView()
        Me.chLine = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chFile = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chPreview = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnFind = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.pbProgress = New System.Windows.Forms.ToolStripProgressBar()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.rtbText = New DecompiledScriptsFinder.RichTextBoxEx()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtDir
        '
        Me.txtDir.Location = New System.Drawing.Point(87, 3)
        Me.txtDir.Name = "txtDir"
        Me.txtDir.Size = New System.Drawing.Size(393, 23)
        Me.txtDir.TabIndex = 0
        '
        'txtStr
        '
        Me.txtStr.Location = New System.Drawing.Point(87, 32)
        Me.txtStr.Name = "txtStr"
        Me.txtStr.Size = New System.Drawing.Size(425, 23)
        Me.txtStr.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 15)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Directory"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 15)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Search String"
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(486, 3)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(26, 23)
        Me.btnBrowse.TabIndex = 1
        Me.btnBrowse.Text = "..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'lvResult
        '
        Me.lvResult.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvResult.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chLine, Me.chFile, Me.chPreview})
        Me.lvResult.FullRowSelect = True
        Me.lvResult.GridLines = True
        Me.lvResult.Location = New System.Drawing.Point(3, 61)
        Me.lvResult.Name = "lvResult"
        Me.lvResult.Size = New System.Drawing.Size(603, 521)
        Me.lvResult.TabIndex = 5
        Me.lvResult.UseCompatibleStateImageBehavior = False
        Me.lvResult.View = System.Windows.Forms.View.Details
        '
        'chLine
        '
        Me.chLine.Text = "Line"
        Me.chLine.Width = 80
        '
        'chFile
        '
        Me.chFile.Text = "File"
        Me.chFile.Width = 150
        '
        'chPreview
        '
        Me.chPreview.Text = "Preview"
        Me.chPreview.Width = 350
        '
        'btnFind
        '
        Me.btnFind.Location = New System.Drawing.Point(518, 3)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(89, 52)
        Me.btnFind.TabIndex = 3
        Me.btnFind.Text = "Find"
        Me.btnFind.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.pbProgress, Me.lblStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 585)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1048, 22)
        Me.StatusStrip1.TabIndex = 7
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'pbProgress
        '
        Me.pbProgress.Name = "pbProgress"
        Me.pbProgress.Size = New System.Drawing.Size(100, 16)
        Me.pbProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(42, 17)
        Me.lblStatus.Text = "Ready."
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDir)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnFind)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtStr)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lvResult)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnBrowse)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rtbText)
        Me.SplitContainer1.Size = New System.Drawing.Size(1048, 585)
        Me.SplitContainer1.SplitterDistance = 609
        Me.SplitContainer1.TabIndex = 8
        '
        'rtbText
        '
        Me.rtbText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.rtbText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbText.Location = New System.Drawing.Point(0, 0)
        Me.rtbText.Name = "rtbText"
        Me.rtbText.NumberAlignment = System.Drawing.StringAlignment.Center
        Me.rtbText.NumberBackground1 = System.Drawing.SystemColors.ControlLight
        Me.rtbText.NumberBackground2 = System.Drawing.SystemColors.Window
        Me.rtbText.NumberBorder = System.Drawing.SystemColors.ControlDark
        Me.rtbText.NumberBorderThickness = 1.0!
        Me.rtbText.NumberColor = System.Drawing.Color.DarkGray
        Me.rtbText.NumberFont = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbText.NumberLeadingZeroes = False
        Me.rtbText.NumberLineCounting = DecompiledScriptsFinder.RichTextBoxEx.LineCounting.CRLF
        Me.rtbText.NumberPadding = 2
        Me.rtbText.ReadOnly = True
        Me.rtbText.ShowLineNumbers = True
        Me.rtbText.Size = New System.Drawing.Size(435, 585)
        Me.rtbText.TabIndex = 0
        Me.rtbText.Text = ""
        '
        'frmMain
        '
        Me.AcceptButton = Me.btnFind
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1048, 607)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Decompiled Scripts Finder"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtDir As TextBox
    Friend WithEvents txtStr As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btnBrowse As Button
    Friend WithEvents lvResult As ListView
    Friend WithEvents chLine As ColumnHeader
    Friend WithEvents chFile As ColumnHeader
    Friend WithEvents chPreview As ColumnHeader
    Friend WithEvents btnFind As Button
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents pbProgress As ToolStripProgressBar
    Friend WithEvents lblStatus As ToolStripStatusLabel
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents rtbText As RichTextBoxEx
End Class
