
Public MustInherit Class PawnClass
    Inherits PictureBox
    Enum life
        dead
        alive
    End Enum
    Enum State
        jumping
        walking
        crouching
        flying
        still
    End Enum
    Friend Collision As Rectangle = New Rectangle()
    Friend CurrentState As State
    Friend CurrentLife As life
    Friend WithEvents Ticker As Timer = New Timer()
    Public Sub New()
        Ticker.Interval = 100
        Me.CurrentState = State.still
        Me.CurrentLife = life.alive
    End Sub
    Public ReadOnly Property getState() As State
        Get
            Return Me.CurrentState
        End Get
    End Property
    Public Property getLife() As State
        Get
            Return Me.CurrentLife
        End Get
        Set(value As State)
            Me.CurrentLife = value
        End Set
    End Property
    MustOverride Sub tickTock() Handles Ticker.Tick
    MustOverride Sub ShowPawn()
    MustOverride Sub UpdatePawn()
End Class
