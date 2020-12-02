Public Class Dinosaur
    Inherits PawnClass
    Private WithEvents updateThread As Timer = New Timer()
    Private counter As Integer = 0
    Public Sub New()
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        Me.Location = New Point(75, 215)
        Me.Size = New System.Drawing.Size(100, 235)
        Me.CurrentState = State.walking
        Me.BackColor = Color.Transparent
        Me.SizeMode = PictureBoxSizeMode.StretchImage
        Me.Collision.Size = New System.Drawing.Size(100, 50)
        Me.ShowPawn()
        updateThread.Start()
    End Sub
    Public Sub updateFrame() Handles updateThread.Tick
        UpdatePawn()
    End Sub
    Public Overrides Sub tickTock() Handles Ticker.Tick
        If counter < 65 Then
            counter += 1
        Else
            Me.changeState(State.walking)
            Me.ShowPawn()
            counter = 0
            Me.Ticker.Stop()
        End If
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
        'handle your keys here
    End Function
    Public Sub changeState(ByVal newState As State)
        If newState = State.jumping AndAlso Me.CurrentState = State.walking Or Me.CurrentState = State.still Then
            Me.CurrentState = State.jumping
        Else
            Me.CurrentState = State.walking
        End If
        If Me.CurrentState = State.jumping Then
            Dim newSize As Size = New System.Drawing.Size(100, 235)
            Me.Size = newSize
            Me.Ticker.Start()
        Else
            Dim newSize As Size = New System.Drawing.Size(100, 50)
            Me.Size = newSize
        End If
        ShowPawn()
    End Sub

    Public Overrides Sub ShowPawn()
        Me.Controls.Clear()
        If Me.CurrentState = State.walking Then
            Me.Image = My.Resources.Dinosaur
        ElseIf Me.CurrentState = State.jumping Then
            Me.Image = My.Resources.DinoJump
        End If
        If Me.CurrentState = State.jumping Then
            Me.Location = New Point(75, 65)
            Dim newSize As Size = New System.Drawing.Size(100, 235)
            Me.Size = newSize
        Else
            Me.Location = New Point(75, 235)
            Dim newSize As Size = New System.Drawing.Size(100, 50)
            Me.Size = newSize
        End If
        Me.SizeMode = PictureBoxSizeMode.StretchImage
        Me.BackColor = Color.Transparent
    End Sub

    Public Overrides Sub UpdatePawn()
        Me.Collision.Location = Me.Location
    End Sub
End Class
