Public Class Form1
    Public currentGame As Game
    Private Sub keyPressed(ByVal o As [Object], ByVal e As KeyPressEventArgs) Handles Me.KeyPress
        If currentGame IsNot Nothing AndAlso e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Space) Then
            currentGame.player.changeState(PawnClass.State.jumping)
        End If
    End Sub

    Private Sub PauseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PauseToolStripMenuItem.Click
        If currentGame Is Nothing Then
            currentGame = New Game(Me)
        End If
        Dim senderCasted = CType(sender, ToolStripMenuItem)
        If senderCasted.Text = "Start" Then
            senderCasted.Text = "Pause"
            Me.currentGame.start()
        Else
            senderCasted.Text = "Start"
            Me.currentGame.Pause()
        End If
    End Sub

    Private Sub RestartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartToolStripMenuItem.Click
        If currentGame Is Nothing Then
            currentGame = New Game(Me)
        Else
            currentGame.restart()
        End If
    End Sub

    Private Sub Form1_ControlAdded(sender As Object, e As ControlEventArgs) Handles MyBase.ControlAdded
        Me.Ground.BringToFront()
        Me.theBigPicture.SendToBack()
    End Sub
End Class
