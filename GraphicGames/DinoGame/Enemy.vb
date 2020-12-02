Public MustInherit Class Enemy
    Inherits PawnClass
    Public Overrides Sub tickTock() Handles Ticker.Tick
        UpdatePawn()
    End Sub
    Public Sub New()
        Me.SizeMode = PictureBoxSizeMode.StretchImage
        Me.Ticker.Interval = 2
    End Sub
    Public MustOverride Overrides Sub ShowPawn()

    Public Overrides Sub UpdatePawn()
        Me.Location = New Point(Me.Location.X - 2.5, Me.Location.Y)
        Me.Collision.Location = Me.Location
        If Me.Location.X < -10 Then
            Me.CurrentLife = life.dead
        End If
    End Sub
End Class
Public Class Bushes
    Inherits Enemy
    Private innerSize As thicc
    Enum thicc
        big
        small
    End Enum
    Public Sub New()
        Dim r As Random = New Random()
        If r.Next(0, 10) = 7 Then
            Me.innerSize = thicc.big
        Else
            Me.innerSize = thicc.small
        End If
        If Me.innerSize = thicc.small Then
            Me.Location = New Point(755, 250)
            Me.Size = New Size(40, 40)
            Me.Collision.Size = New Size(35, 35)
        Else
            Me.Location = New Point(755, 230)
            Me.Size = New Size(100, 75)
            Me.Collision.Size = New Size(90, 70)
        End If
        Me.Collision.Size = Me.Size
        Me.Collision.Location = Me.Location
        ShowPawn()
        Me.Ticker.Start()
    End Sub
    Public Overrides Sub ShowPawn()
        If Me.innerSize = thicc.small Then
            Me.Image = My.Resources.BushSmall
        Else
            Me.Image = My.Resources.BigBush
        End If
        Me.BackColor = Color.Transparent
        Me.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub
End Class
Public Class Birds
    Inherits Enemy
    Private innerLevel As level
    Enum level
        high
        low
    End Enum
    Public Sub New()
        Dim r As Random = New Random()
        Me.Collision.Size = New Size(70, 30)
        Me.Size = New Size(85, 70)
        Me.Ticker.Interval = 1
        If r.Next(0, 2) = 1 Then
            Me.innerLevel = level.high
        Else
            Me.innerLevel = level.low
        End If
        If Me.innerLevel = level.low Then
            Me.Location = New Point(755, 230)
        Else
            Me.Location = New Point(755, 185)
        End If
        Me.Collision.Location = Me.Location
        ShowPawn()
        Me.Ticker.Start()
    End Sub
    Public Overrides Sub ShowPawn()
        Dim dummyPic As PictureBox = New PictureBox()
        Me.Image = My.Resources.Bird
        Me.BackColor = Color.Transparent
        Me.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub
End Class
