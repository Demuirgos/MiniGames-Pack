Module Module1
    Class game
        Class cell
            Public Sub New()
                v = -1
                u.Add("e", 0)
                u.Add("p", 0)
            End Sub
            Private v As Integer
            Public u As New Dictionary(Of String, Integer)
            Public Property Fill(Optional str As String = Nothing)
                Get
                    Return v
                End Get
                Set(value)
                    Dim str2 As String
                    If str = "e" Then
                        str2 = "p"
                    Else str2 = "e"
                    End If
                    v = value
                    u(str) = -1
                    u(str2) = -2
                End Set
            End Property
            Public Sub set_priority(Str As String, n As Integer)
                u(Str) = n
            End Sub
        End Class
        Class grid
            Public Sub New()
                For i = 0 To 2
                    For j = 0 To 2
                        get_cell(i, j) = New cell()
                    Next
                Next
                First_game.Add("e", 0)
                First_game.Add("p", 0)
            End Sub
            Public get_cell(2, 2) As cell
            Public First_game As New Dictionary(Of String, Integer)
            Public Sub set_move(str As String, i As Integer, j As Integer)
                str = str.ToLower
                If str = "e" Then
                    get_cell(i, j).Fill("e") = 0
                Else
                    get_cell(i, j).Fill("p") = 1
                End If
                priority_set(str, i, j)
                First_game(str) = 1
                count += 1
            End Sub
            Private Sub priority_set(str As String, i As Integer, j As Integer)
                'to be optimised
                Dim chack_end = Sub()
                                    Dim a() As Integer = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
                                    For i = 0 To 2
                                        'If get_cell(i)
                                    Next
                                End Sub
                Dim set_lines = Sub()
                                    For k = 0 To 2
                                        If k = i Then
                                            Continue For
                                        Else
                                            If get_cell(k, j).Fill = -1 Then
                                                get_cell(k, j).set_priority(str, 1)
                                            End If
                                        End If
                                    Next
                                    For k = 0 To 2
                                        If k = j Then
                                            Continue For
                                        Else
                                            If get_cell(i, k).Fill = -1 Then
                                                get_cell(i, k).set_priority(str, 1)
                                            End If
                                        End If
                                    Next
                                End Sub
                Dim set_diagonals = Sub()
                                        If i = j Then
                                            For k = 0 To 2
                                                If i = k Then
                                                    Continue For
                                                Else
                                                    If get_cell(k, k).Fill = -1 Then
                                                        get_cell(k, k).set_priority(str, 1)
                                                    End If
                                                End If
                                            Next
                                        ElseIf i <> j Then
                                            For k = 0 To 2
                                                If i = k And j = 2 - k Then
                                                    Continue For
                                                Else
                                                    If get_cell(k, 2 - k).Fill = -1 Then
                                                        get_cell(k, 2 - k).set_priority(str, 1)
                                                    End If
                                                End If
                                            Next
                                        End If
                                    End Sub
                Dim set_grid = Sub()
                                   For i = 0 To 2
                                       For j = 0 To 2
                                           If get_cell(i, j).Fill = -1 Then
                                               get_cell(i, j).set_priority(str, 1)
                                           End If
                                       Next
                                   Next
                               End Sub
                Dim check_endgame = Sub()
                                        For k = 0 To 2
                                            For i = 0 To 1
                                                For j = i + 1 To 2
                                                    Dim list As New List(Of Integer)(New Integer() {0, 1, 2})
                                                    If (First_game("e") <> 0 And First_game("p") <> 0) And get_cell(k, i).u(str) = get_cell(k, j).u(str) And get_cell(k, i).Fill <> -1 Then
                                                        Dim user As New List(Of String)(New String() {"e", "p"})
                                                        list.Remove(i)
                                                        list.Remove(j)
                                                        user.Remove(str)
                                                        If list.Count <> 0 AndAlso get_cell(k, list.Item(0)).Fill = -1 Then
                                                            get_cell(k, list.Item(0)).u(str) = 2
                                                            get_cell(k, list.Item(0)).u(user.Item(0)) = 3
                                                            'ElseIf list.Count = 0 Then
                                                            '   Console.SetCursorPosition(41, 4)
                                                            '  Console.Write("the winner is " & user.Item(0))
                                                        End If
                                                    End If
                                                Next
                                            Next
                                            For i = 0 To 1
                                                For j = i + 1 To 2
                                                    Dim list As New List(Of Integer)(New Integer() {0, 1, 2})
                                                    If (First_game("e") <> 0 And First_game("p") <> 0) And get_cell(i, k).u(str) = get_cell(j, k).u(str) And get_cell(k, i).Fill <> -1 Then
                                                        Dim user As New List(Of String)(New String() {"e", "p"})
                                                        list.Remove(i)
                                                        list.Remove(j)
                                                        user.Remove(str)
                                                        If list.Count <> 0 AndAlso get_cell(list.Item(0), k).Fill = -1 Then
                                                            get_cell(list.Item(0), k).u(str) = 2
                                                            get_cell(list.Item(0), k).u(user.Item(0)) = 3
                                                            'ElseIf list.Count = 0 Then
                                                            '   Console.SetCursorPosition(41, 4)
                                                            '  Console.Write("the winner is " & user.Item(0))
                                                        End If
                                                    End If
                                                Next
                                            Next
                                        Next
                                        For k = 0 To 1
                                            Dim list As New List(Of Integer)(New Integer() {0, 1, 2})
                                            For i = k + 1 To 2
                                                If (First_game("e") <> 0 And First_game("p") <> 0) And get_cell(k, k).u(str) = get_cell(i, i).u(str) And get_cell(k, k).Fill <> -1 Then
                                                    Dim user As New List(Of String)(New String() {"e", "p"})
                                                    list.Remove(k)
                                                    list.Remove(i)
                                                    user.Remove(str)
                                                    'to be fixed remove cond_1 and adjut others
                                                    If list.Count <> 0 AndAlso get_cell(list.Item(0), list.Item(0)).Fill = -1 Then
                                                        get_cell(list.Item(0), list.Item(0)).u(str) = 2
                                                        get_cell(list.Item(0), list.Item(0)).u(user.Item(0)) = 3
                                                        'ElseIf list.Count = 0 Then
                                                        '   Console.SetCursorPosition(41, 4)
                                                        '  Console.Write("the winner is " & user.Item(0))
                                                    End If
                                                End If
                                            Next
                                        Next
                                        For k = 0 To 1
                                            Dim list As New List(Of Integer)(New Integer() {0, 1, 2})
                                            For i = k + 1 To 2
                                                If (First_game("e") <> 0 And First_game("p") <> 0) And get_cell(k, 2 - k).u(str) = get_cell(i, 2 - i).u(str) And get_cell(k, 2 - k).Fill <> -1 Then
                                                    Dim user As New List(Of String)(New String() {"e", "p"})
                                                    list.Remove(k)
                                                    list.Remove(i)
                                                    user.Remove(str)
                                                    'to be fixed remove cond_1 and adjut others
                                                    If list.Count <> 0 AndAlso get_cell(list.Item(0), 2 - list.Item(0)).Fill = -1 Then
                                                        get_cell(list.Item(0), 2 - list.Item(0)).u(str) = 2
                                                        get_cell(list.Item(0), 2 - list.Item(0)).u(user.Item(0)) = 3
                                                        'ElseIf list.Count = 0 Then
                                                        '   Console.SetCursorPosition(41, 4)
                                                        '  Console.Write("the winner is " & user.Item(0))
                                                    End If
                                                End If
                                            Next
                                        Next
                                    End Sub

                If i Mod 2 = 0 And j Mod 2 = 0 Then
                    set_lines()
                    set_diagonals()
                ElseIf i = 1 And j = 1 Then
                    set_grid()

                Else
                    set_lines()
                End If
                If First_game("e") And First_game("p") > 0 Then
                    check_endgame()
                End If
                'check_killer_move()
            End Sub

            Public count As Integer = 0
        End Class
        Public Sub New()
            web = New grid
        End Sub
        Public web As grid
        Public Sub solve(str As String)
            str.ToLower()
            Dim i, j, c As Integer
            Dim str2 As String
            If str = "e" Then
                str2 = "p"
            Else str2 = "e"
            End If
            Dim a As New Random
            If web.First_game(str) = 0 And web.First_game(str2) = 0 Then
                c = a.Next(0, 5)
                If c = 0 Then
                    i = a.Next(0, 3)
                    j = a.Next(0, 3)
                Else
                    i = 1
                    j = 1
                End If
                web.set_move(str, i, j)
            ElseIf web.First_game(str) = 0 And web.First_game(str2) <> 0 Then
                Do
                    If web.get_cell(1, 1).Fill = -1 Then
                        c = a.Next(0, 5)
                    End If
                    If c = 0 Then
                        i = a.Next(0, 3)
                        j = a.Next(0, 3)
                    Else
                        i = 1
                        j = 1
                        Exit Do
                    End If
                Loop While web.get_cell(i, j).Fill <> -1
                web.set_move(str, i, j)
            Else
                Dim min() As Integer = {0, 0}
                For i = 0 To 2
                    For j = 0 To 2
                        If web.get_cell(i, j).u(str) > web.get_cell(min(0), min(1)).u(str) Then
                            min(0) = i
                            min(1) = j
                        End If
                    Next
                Next
                If web.get_cell(min(0), min(1)).Fill = -1 Then
                    web.set_move(str, min(0), min(1))
                End If
            End If
        End Sub
        Public Sub show()
            Dim square = Sub()
                             For i = 0 To 22
                                 For j = 0 To 36
                                     If i Mod 7 = 0 Then
                                         Console.Write("=")
                                     ElseIf i Mod 2 <> 0 And (j Mod 12 = 0) Then
                                         Console.Write("|")
                                     Else Console.Write(" ")
                                     End If
                                 Next
                                 Console.WriteLine()
                             Next
                         End Sub
            Dim fill_square = Sub(i As Integer, j As Integer, v As Integer)
                                  If v = 1 Then
                                      For k = 0 To 5
                                          Console.SetCursorPosition(j * 12 + 4 + k, i * 7 + 1 + k)
                                          Console.Write("*" & vbNewLine)
                                      Next
                                      For k = 0 To 5
                                          Console.SetCursorPosition(j * 12 + 8 - k, i * 7 + 1 + k)
                                          Console.Write("*" & vbNewLine)
                                      Next
                                  ElseIf v = 0 Then
                                      For k = 0 To 5
                                          Console.SetCursorPosition(j * 12 + 4 + 0, i * 7 + 1 + k)
                                          If k = 0 Or k = 5 Then
                                              Console.Write("  **  ")
                                          ElseIf k = 1 Or k = 4 Then
                                              Console.Write(" *  * ")
                                          Else
                                              Console.Write("*    *")
                                          End If
                                      Next
                                  ElseIf v = -1 Then
                                  Else
                                      Console.Clear()
                                      Console.Write("input invalid")
                                  End If
                              End Sub
            If web.First_game("e") = 0 And web.First_game("p") = 0 Then
                square()
            End If
            For i = 0 To 2
                For j = 0 To 2
                    fill_square(i, j, web.get_cell(i, j).Fill)
                Next
            Next

        End Sub
        Public Sub get_move(str As String)
            Dim i, j As Integer
            While True
                Console.SetCursorPosition(41, 0)
                Console.Write("please enter the H_line number   ")
                Console.SetCursorPosition(72, 0)
                i = Console.ReadLine()
                Console.SetCursorPosition(41, 1)
                Console.Write("please enter the V_line number   ")
                Console.SetCursorPosition(72, 1)
                j = Console.ReadLine()
                Console.SetCursorPosition(41, 2)
                Console.Write("                                ")
                If (i < 3 And j < 3) AndAlso web.get_cell(i, j).Fill = -1 Then
                    web.set_move(str, i, j)
                    Exit Sub
                Else
                    Console.SetCursorPosition(41, 2)
                    Console.Write("Illegal move")
                    Beep()
                End If

            End While


        End Sub
        Public Sub diag()

            For i = 0 To 2
                For j = 0 To 2
                    Console.SetCursorPosition(42, 6 + 6 * i + j)
                    Console.Write("i : " & i & " j : " & j & " user : e priority" & web.get_cell(i, j).u("e") & " user : p priority" & web.get_cell(i, j).u("p") & vbNewLine)
                Next
            Next
        End Sub
    End Class
    Sub delay()
        Dim i = 0
        While i < 1000000000
            i += 1
        End While
    End Sub
    Sub Main()
        If Console.ReadLine() = 0 Then
            Console.Clear()
            While True
                Dim game As New game
                While game.web.count < 9
                    game.show()
                    game.get_move("p")
                    game.show()
                    game.solve("e")
                End While
                Console.Read()
                Console.SetCursorPosition(0, 0)
            End While
        ElseIf Console.ReadLine = 1 Then
            Console.Clear()
            While True
                Dim game As New game
                While game.web.count < 9
                    game.show()
                    game.get_move("p")
                    game.show()
                    game.get_move("e")
                End While
                Console.Read()
                Console.SetCursorPosition(0, 0)
            End While
        Else
            Console.Clear()
            While True
                    Dim game As New game
                While game.web.count < 9
                    game.show()
                    game.solve("p")
                    delay()
                    game.show()
                    game.solve("e")
                    delay()
                End While
                    Console.Read()
                    Console.SetCursorPosition(0, 0)
            End While
        End If

        Console.Read()
    End Sub
End Module
