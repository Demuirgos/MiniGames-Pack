Public Class Game
    Private WithEvents ParentForm As Form1
    Public player As Dinosaur
    Private enemies As List(Of Enemy)
    Public score As Integer
    Public collisionChecker As Timer = New Timer()
    Public enemySpawner As Timer = New Timer()
    Public PlayerOrganiser As Timer = New Timer()
    Public Sub New(ByRef parent As Form1)
        Me.ParentForm = parent
        Me.collisionChecker.Interval = 2
        Me.enemySpawner.Interval = 5000
        Me.PlayerOrganiser.Interval = 100
        Me.player = New Dinosaur()
        Me.enemies = New List(Of Enemy)
        Me.score = 0
        Me.ParentForm.Controls.Add(player)
        AddHandler collisionChecker.Tick, AddressOf tickTockColl
        AddHandler enemySpawner.Tick, AddressOf tickTockEnem
        AddHandler enemySpawner.Tick, AddressOf tickTockSpawn
    End Sub
    Public Sub start()
        Me.collisionChecker.Start()
        Me.enemySpawner.Start()
        Me.PlayerOrganiser.Start()
        For Each enemyPawn In Me.enemies
            enemyPawn.Ticker.Start()
        Next
    End Sub

    Public Sub restart()
        clearEnemies()
        Me.ParentForm.PauseToolStripMenuItem.Text = "start"
    End Sub
    Public Sub Pause()
        Me.collisionChecker.Stop()
        Me.enemySpawner.Stop()
        Me.PlayerOrganiser.Stop()
        For Each enemyPawn In Me.enemies
            enemyPawn.Ticker.Stop()
        Next
    End Sub
    Public Sub clearEnemies()
        For Each enemy In enemies.ToList()
            Me.ParentForm.Controls.Remove(enemy)
            enemies.Remove(enemy)
            enemy.Dispose()
        Next
    End Sub
    Public Sub showDeathMsg()
        Dim result As DialogResult = MessageBox.Show("You Died " & vbNewLine & " Your Final Score is :" & Me.ParentForm.Score.Text, "End Game", MessageBoxButtons.OK)
    End Sub
    Private Sub checkEndGame()
        For Each enemyPawn In Me.enemies.ToList()
            If Me.player.Collision.IntersectsWith(enemyPawn.Collision) Then
                Me.player.CurrentLife = PawnClass.life.dead
                Me.Pause()
                showDeathMsg()
                Me.restart()
            End If
        Next
    End Sub
    Private Sub updatePlayers()
        Dim toBeDisposedOf = From enemy In enemies Where enemy.CurrentLife = PawnClass.life.dead
        For Each enemyPawn In toBeDisposedOf.ToList()
            Me.enemies.Remove(enemyPawn)
            Me.ParentForm.Controls.Remove(enemyPawn)
            enemyPawn.Dispose()
        Next
    End Sub
    Private Sub SpawnPlayers()
        Dim r As Random = New Random()
        If r.Next(1, 7) = 4 Then
            Me.enemies.Add(New Birds())
        Else
            Me.enemies.Add(New Bushes())
        End If
    End Sub
    Public Sub tickTockColl()
        checkEndGame()
    End Sub
    Public Sub tickTockEnem()
        updatePlayers()
    End Sub
    Public Sub tickTockSpawn()
        Me.score += 10
        SpawnPlayers()
        show()
    End Sub
    Public Sub show()
        For Each enemy In Me.enemies
            If Not Me.ParentForm.Controls.Contains(enemy) Then
                Me.ParentForm.Controls.Add(enemy)
            End If
        Next
        Me.ParentForm.Score.Text = Me.score.ToString()
    End Sub
End Class
