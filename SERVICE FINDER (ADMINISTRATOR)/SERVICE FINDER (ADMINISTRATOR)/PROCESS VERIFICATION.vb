Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Drawing.Imaging
Public Class PROCESS_VERIFICATION
    Dim conn As MySqlConnection
    Dim cnstr As String = "data source =  localhost; user id = root; database = sfa;"
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim itemcol(99) As String
    Dim msg As String
    Dim res As String = "FALSE"
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        'table for requesting for verification
        If ComboBox1.Text = "FOR VERIFICATION" Then
            Button1.Enabled = True
            Button2.Enabled = True
            ListView1.Items.Clear()
            conn = New MySqlConnection(cnstr)
            conn.Open()
            Dim sql As String = "SELECT * FROM userverification Where verified = 'FALSE' "
            cmd = New MySqlCommand(sql, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet
            da.Fill(ds, "tables")
            For r = 0 To ds.Tables(0).Rows.Count - 1
                For c = 0 To ds.Tables(0).Columns.Count - 1
                    itemcol(c) = ds.Tables(0).Rows(r)(c).ToString
                Next
                Dim lvitems As New ListViewItem(itemcol)
                ListView1.Items.Add(lvitems)
            Next
            'table for VERIFIED
        ElseIf ComboBox1.Text = "VERIFIED" Then
            ListView1.Items.Clear()
            Button1.Enabled = False
            Button2.Enabled = False
            conn = New MySqlConnection(cnstr)
            conn.Open()
            Dim sql As String = "SELECT * FROM userverification Where verified = 'TRUE' "
            cmd = New MySqlCommand(sql, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet
            da.Fill(ds, "tables")
            For r = 0 To ds.Tables(0).Rows.Count - 1
                For c = 0 To ds.Tables(0).Columns.Count - 1
                    itemcol(c) = ds.Tables(0).Rows(r)(c).ToString
                Next
                Dim lvitems As New ListViewItem(itemcol)
                ListView1.Items.Add(lvitems)
            Next

        ElseIf ComboBox1.Text = "ALL REQUEST RECORDS" Then
            Button1.Enabled = False
            Button2.Enabled = False
            ListView1.Items.Clear()
            conn = New MySqlConnection(cnstr)
            conn.Open()
            Dim sql As String = "SELECT * FROM userverification"
            cmd = New MySqlCommand(sql, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet
            da.Fill(ds, "tables")
            For r = 0 To ds.Tables(0).Rows.Count - 1
                For c = 0 To ds.Tables(0).Columns.Count - 1
                    itemcol(c) = ds.Tables(0).Rows(r)(c).ToString
                Next
                Dim lvitems As New ListViewItem(itemcol)
                ListView1.Items.Add(lvitems)
            Next

        ElseIf ComboBox1.Text = "DENIED" Then

            ListView1.Items.Clear()
            conn = New MySqlConnection(cnstr)
            conn.Open()
            Dim sql As String = "SELECT * FROM userverification Where verified = 'DENIED'"
            cmd = New MySqlCommand(sql, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet
            da.Fill(ds, "tables")
            For r = 0 To ds.Tables(0).Rows.Count - 1
                For c = 0 To ds.Tables(0).Columns.Count - 1
                    itemcol(c) = ds.Tables(0).Rows(r)(c).ToString
                Next
                Dim lvitems As New ListViewItem(itemcol)
                ListView1.Items.Add(lvitems)
            Next
        End If
    End Sub

    Private Sub PROCESS_VERIFICATION_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Dim tempId As String
    Dim tempuservid As String
    Dim id1, id2 As String
    Dim txtsourcecode As String
    Dim tempverified As String
    Dim tempprofpic As String
    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        Button1.Enabled = True
        If ListView1.SelectedItems.Count > 0 Then
            tempuservid = ListView1.Items(ListView1.SelectedIndices(0)).Text
            tempId = ListView1.Items(ListView1.SelectedIndices(0)).SubItems(1).Text
            conn = New MySqlConnection(cnstr)
            conn.Open()
            Dim sql As String = "SELECT lastName,firstName,middleName,contactNumber,picture from users where userID = '" & tempId & "'  "
            cmd = New MySqlCommand(sql, conn)
            Dim myreader As MySqlDataReader = cmd.ExecuteReader
            While myreader.Read
                lname.Text = myreader.GetValue(0).ToString
                fname.Text = myreader.GetValue(1).ToString
                mname.Text = myreader.GetValue(2).ToString
                cnumber.Text = myreader.GetValue(3).ToString
                tempprofpic = myreader.GetValue(4)
            End While
            PictureBox3.Image = Base64ToImage(tempprofpic.Replace("data:image/png;base64,", ""))
            conn.Close()

            conn = New MySqlConnection(cnstr)
            conn.Open()
            sql = "SELECT id1,id2 FROM userverification where userVerificationID = '" + tempuservid + "'"
            cmd = New MySqlCommand(sql, conn)
            Dim reader1 As MySqlDataReader = cmd.ExecuteReader
            While reader1.Read
                id1 = reader1.GetValue(0)
                id2 = reader1.GetValue(1)
            End While
            PictureBox1.Image = Base64ToImage(id1.Replace("data:image/jpeg;base64,", ""))
            PictureBox2.Image = Base64ToImage(id2.Replace("data:image/jpeg;base64,", ""))
            conn.Close()

            conn = New MySqlConnection(cnstr)
            conn.Open()
            sql = "SELECT verified from userverification where userID= '" & tempId & "'"
            cmd = New MySqlCommand(sql, conn)
            myreader = cmd.ExecuteReader

            While myreader.Read
                tempverified = myreader.GetValue(0).ToString
            End While
            If tempverified = "TRUE" Then
                Button1.Enabled = False
                Button2.Enabled = False
            ElseIf tempverified = "FALSE" Then
                Button1.Enabled = True
                Button2.Enabled = True
            ElseIf tempverified = "DENIED" Then
                Button1.Enabled = False
                Button2.Enabled = False
            End If
            conn.Close()
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        popupimage.BringToFront()
        popupimage.Visible = True
        BACK.Visible = True
        popupimage.Image = PictureBox1.Image
    End Sub

    Private Sub BACK_Click(sender As Object, e As EventArgs) Handles BACK.Click
        popupimage.SendToBack()
        popupimage.Image = Nothing
        popupimage.Visible = False
        BACK.Visible = False
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        popupimage.BringToFront()
        popupimage.Visible = True
        BACK.Visible = True
        popupimage.Image = PictureBox2.Image
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim result As DialogResult = MessageBox.Show("CONFIRM VERIFICATION?", "ADMIN", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            Try
                conn = New MySqlConnection(cnstr)
                conn.Open()
                Dim sql As String = "UPDATE userverification  SET verified = 'TRUE' WHERE userID = '" + tempId + "' "
                cmd = New MySqlCommand(sql, conn)
                Dim i As Integer = cmd.ExecuteNonQuery
                If i > 0 Then
                    MessageBox.Show("USER HAS BEEN VERIFIED")

                    'CONNECTING TO SMS MODULE

                    ' With SerialPort1
                    ''Serial Port Configuration
                    '.PortName = "COM4" 'THIS PORT IS CHANGABLE. YOU MUST TO CHECK YOURS!
                    '.BaudRate = 9600
                    '.ReadTimeout = 1000
                    '.ReadBufferSize = 1000
                    '.WriteTimeout = 1000
                    '.WriteBufferSize = 1000
                    '.Parity = Parity.None
                    '.StopBits = StopBits.One
                    '.DataBits = 8
                    '.Handshake = Handshake.None
                    ''.RtsEnable = True
                    '.Open()
                    'End With
                    'BackgroundWorker1.RunWorkerAsync()
                    msg = "VERIFIED"
                Else

                End If

            Catch ex As Exception

            End Try

        ElseIf result = DialogResult.No Then


        End If

    End Sub
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        'Message Processing
        'Chr(34) is equivalent to (") and Chr(13) is equivalent to Carriage-Return
        '      SerialPort1.Write("AT+CMGS=" & Chr(34) & cnumber.Text & Chr(34) & Chr(13))
        'Sleep 2 milliseconds before execute the second line
        '     Thread.Sleep(200)
        '    SerialPort1.Write("YOUR VERIFICATION REQUEST IS " + msg & Chr(26))
        '    SerialPort1.Close()
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        'Prompt when the execution is completed
        '   MsgBox("Message Sent!", vbInformation, "Message")
        '  SerialPort1.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        'CONNECTING TO SMS MODULE

        ' With SerialPort1
        ''Serial Port Configuration
        '.PortName = "COM4" 'THIS PORT IS CHANGABLE. YOU MUST TO CHECK YOURS!
        '.BaudRate = 9600
        '.ReadTimeout = 1000
        '.ReadBufferSize = 1000
        '.WriteTimeout = 1000
        '.WriteBufferSize = 1000
        '.Parity = Parity.None
        '.StopBits = StopBits.One
        '.DataBits = 8
        '.Handshake = Handshake.None
        ''.RtsEnable = True
        '.Open()
        'End With
        'BackgroundWorker1.RunWorkerAsync()
        msg = "DENIED"
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub cnumber_TextChanged(sender As Object, e As EventArgs) Handles cnumber.TextChanged

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
        Call STATISTICS.allcombobox()
        STATISTICS.Show()
    End Sub



    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Hide()
    End Sub

    Function Base64ToImage(ByVal base64string As String) As System.Drawing.Image
        'Setup image and get data stream together
        Dim img As System.Drawing.Image
        Dim MS As System.IO.MemoryStream = New System.IO.MemoryStream
        Dim b64 As String = base64string.Replace(" ", "+")
        Dim b() As Byte

        'Converts the base64 encoded msg to image data
        b = Convert.FromBase64String(b64)
        MS = New System.IO.MemoryStream(b)

        'creates image
        img = System.Drawing.Image.FromStream(MS)

        Return img
    End Function



End Class