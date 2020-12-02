<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.GameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PauseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.Cloud1 = New System.Windows.Forms.PictureBox()
        Me.Ground = New System.Windows.Forms.PictureBox()
        Me.theBigPicture = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Score = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Cloud1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Ground, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.theBigPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.theBigPicture.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GameToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(784, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'GameToolStripMenuItem
        '
        Me.GameToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RestartToolStripMenuItem, Me.PauseToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.GameToolStripMenuItem.Name = "GameToolStripMenuItem"
        Me.GameToolStripMenuItem.Size = New System.Drawing.Size(50, 20)
        Me.GameToolStripMenuItem.Text = "Game"
        '
        'RestartToolStripMenuItem
        '
        Me.RestartToolStripMenuItem.Name = "RestartToolStripMenuItem"
        Me.RestartToolStripMenuItem.Size = New System.Drawing.Size(110, 22)
        Me.RestartToolStripMenuItem.Text = "Restart"
        '
        'PauseToolStripMenuItem
        '
        Me.PauseToolStripMenuItem.Name = "PauseToolStripMenuItem"
        Me.PauseToolStripMenuItem.Size = New System.Drawing.Size(110, 22)
        Me.PauseToolStripMenuItem.Text = "Start"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(110, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = Global.DinoGame.My.Resources.Resources.Clouds
        Me.PictureBox4.Location = New System.Drawing.Point(571, 16)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(190, 190)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox4.TabIndex = 17
        Me.PictureBox4.TabStop = False
        '
        'PictureBox5
        '
        Me.PictureBox5.Image = Global.DinoGame.My.Resources.Resources.Clouds
        Me.PictureBox5.Location = New System.Drawing.Point(381, 16)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(190, 190)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox5.TabIndex = 16
        Me.PictureBox5.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox3.Image = Global.DinoGame.My.Resources.Resources.Clouds
        Me.PictureBox3.Location = New System.Drawing.Point(191, 16)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(190, 190)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 15
        Me.PictureBox3.TabStop = False
        '
        'Cloud1
        '
        Me.Cloud1.BackColor = System.Drawing.Color.Transparent
        Me.Cloud1.Image = Global.DinoGame.My.Resources.Resources.Clouds
        Me.Cloud1.Location = New System.Drawing.Point(1, 16)
        Me.Cloud1.Name = "Cloud1"
        Me.Cloud1.Size = New System.Drawing.Size(190, 190)
        Me.Cloud1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Cloud1.TabIndex = 14
        Me.Cloud1.TabStop = False
        '
        'Ground
        '
        Me.Ground.Image = CType(resources.GetObject("Ground.Image"), System.Drawing.Image)
        Me.Ground.Location = New System.Drawing.Point(12, 281)
        Me.Ground.Name = "Ground"
        Me.Ground.Size = New System.Drawing.Size(760, 50)
        Me.Ground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Ground.TabIndex = 13
        Me.Ground.TabStop = False
        '
        'theBigPicture
        '
        Me.theBigPicture.BackColor = System.Drawing.Color.Transparent
        Me.theBigPicture.Controls.Add(Me.Cloud1)
        Me.theBigPicture.Controls.Add(Me.PictureBox3)
        Me.theBigPicture.Controls.Add(Me.PictureBox5)
        Me.theBigPicture.Controls.Add(Me.PictureBox4)
        Me.theBigPicture.Location = New System.Drawing.Point(12, 27)
        Me.theBigPicture.Name = "theBigPicture"
        Me.theBigPicture.Size = New System.Drawing.Size(760, 304)
        Me.theBigPicture.TabIndex = 19
        Me.theBigPicture.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(655, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Score : "
        '
        'Score
        '
        Me.Score.AutoSize = True
        Me.Score.Location = New System.Drawing.Point(695, 11)
        Me.Score.Name = "Score"
        Me.Score.Size = New System.Drawing.Size(31, 13)
        Me.Score.TabIndex = 21
        Me.Score.Text = "0000"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(784, 346)
        Me.Controls.Add(Me.Score)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Ground)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.theBigPicture)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(800, 385)
        Me.MinimumSize = New System.Drawing.Size(800, 385)
        Me.Name = "Form1"
        Me.Text = "DinoOffline"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Cloud1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Ground, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.theBigPicture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.theBigPicture.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents GameToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RestartToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PauseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents PictureBox5 As PictureBox
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents Cloud1 As PictureBox
    Friend WithEvents Ground As PictureBox
    Friend WithEvents theBigPicture As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Score As Label
End Class
