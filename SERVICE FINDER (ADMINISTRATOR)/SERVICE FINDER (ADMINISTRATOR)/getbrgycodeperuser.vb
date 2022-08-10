Imports MySql.Data.MySqlClient

Public Class getbrgycodeperuser
    Dim conn As New MySqlConnection
    Dim cnstr As String = "data source =  localhost; user id = root; database = sfa;"
    Dim cmd As MySqlCommand
    Dim READER As MySqlDataReader

    Private Sub getbrgycodeperuser_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Dim code(100) As String
    Dim i As Integer
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        conn = New MySqlConnection(cnstr)
        conn.Open()
        Dim sql As String = "SELECT * FROM useraddress"
        cmd = New MySqlCommand(sql, conn)
        READER = cmd.ExecuteReader
        i = 0
        While i <> READER.Read
            For c = 0 To Val(READER.Read)
                If code(c) = READER.GetString("brgyCode") Then

                ElseIf code(c) <> READER.GetString("brgyCode") Then
                    code(i) = READER.GetString("brgyCode")
                End If
            Next
            i = i + 1
        End While
        conn.Close()
    End Sub
End Class