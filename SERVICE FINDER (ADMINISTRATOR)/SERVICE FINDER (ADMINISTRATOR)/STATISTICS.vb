Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Net
Public Class STATISTICS
    Dim conn As New MySqlConnection
    Dim cnstr As String = "data source =  localhost; user id = root; database = sfa;"
    Dim cmd As MySqlCommand
    Dim READER As MySqlDataReader

    Dim regiondesc As String 'for region description
    Dim provincedesc As String 'for province description
    Dim citydesc As String 'for city description
    Dim brgydesc As String 'for barangay description
    Dim regcode As String 'for region code
    Dim provcode As String 'for province code
    Dim citycode As String 'for city code
    Dim brgycode As String 'for barangay code
   
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            conn = New MySqlConnection(cnstr)
            conn.Open()
            MsgBox("connected to database")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'QUERY FOR RETRIEVING REGION DESCRIPTION
    Public Sub QueryForRegion()
        conn = New MySqlConnection(cnstr)
        conn.Open()
        Dim sql As String = "SELECT * FROM refregion"
        cmd = New MySqlCommand(sql, conn)
        READER = cmd.ExecuteReader
        While READER.Read
            regiondesc = READER.GetString("regDesc")
            ComboBox1.Items.Add(regiondesc)
        End While
        conn.Close()
    End Sub
    'QUERY FOR RETRIEVING PROVINCE DESCRIPTION
    Public Sub QueryForProvince()
        conn = New MySqlConnection(cnstr)
        conn.Open()
        Dim sql As String = "SELECT * FROM refprovince WHERE regCode = '" + regioncode + "'"
        cmd = New MySqlCommand(sql, conn)
        READER = cmd.ExecuteReader
        While READER.Read
            provincedesc = READER.GetString("provDesc")
            ComboBox2.Items.Add(provincedesc)
        End While
        conn.Close()
    End Sub
    'QUERY FOR RETRIEVING CITY DESCRIPTION
    Dim a As Integer = 0
    Public Sub QueryForCity()
        ComboBox3.Items.Clear()
        conn = New MySqlConnection(cnstr)
        conn.Open()
        Dim sql As String = "SELECT * FROM refcitymun where provCode = '" + provcode + "'"
        cmd = New MySqlCommand(sql, conn)
        READER = cmd.ExecuteReader
        While READER.Read
            citydesc = READER.GetString("citymunDesc")
            ComboBox3.Items.Add(citydesc)

        End While
        conn.Close()

    End Sub
    Dim one, two, three As String
    Public Sub allcombobox()


        'province
        conn = New MySqlConnection(cnstr)
        conn.Open()
        Dim sql As String = "SELECT provDesc FROM refprovince"
        cmd = New MySqlCommand(sql, conn)
        READER = cmd.ExecuteReader
        While READER.Read
            one = READER.GetString("provDesc")
            ComboBox2.Items.Add(one)
        End While
        conn.Close()

        'city
        conn = New MySqlConnection(cnstr)
        conn.Open()
        sql = "SELECT citymunDesc FROM refcitymun "
        cmd = New MySqlCommand(sql, conn)
        READER = cmd.ExecuteReader
        While READER.Read
            two = READER.GetString("citymunDesc")
            ComboBox3.Items.Add(two)
        End While
        conn.Close()


        'barangay
        conn = New MySqlConnection(cnstr)
        conn.Open()
        sql = "SELECT brgyDesc FROM refbrgy "
        cmd = New MySqlCommand(sql, conn)
        READER = cmd.ExecuteReader
        While READER.Read
            three = READER.GetString("brgyDesc")
            ComboBox4.Items.Add(three)
        End While
        conn.Close()
    End Sub

    'QUERY FOR RETRIEVING Barangay DESCRIPTION
    Public Sub QueryForbarangay()
        conn = New MySqlConnection(cnstr)
        conn.Open()
        Dim sql As String = "SELECT * FROM refbrgy where citymunCode = '" + citycode + "'"
        cmd = New MySqlCommand(sql, conn)
        READER = cmd.ExecuteReader
        While READER.Read
            brgydesc = READER.GetString("brgyDesc")
            ComboBox4.Items.Add(brgydesc)
        End While
        conn.Close()
    End Sub





    'PARA TO SA PAG LAGAY NG STATISTICS
    Dim regioncode As String 'sa pag kuha ng REGION CODE
    'chart for region filter'
    Public Sub QueryForChartregion()
        Chart1.Series("NUMBER OF CLIENTS").Points.Clear()
        conn = New MySqlConnection(cnstr)
        conn.Open()
        Dim sql As String = "SELECT * FROM refprovince where  regCode = '" + regioncode + "' "
        cmd = New MySqlCommand(sql, conn)
        READER = cmd.ExecuteReader
        While READER.Read
            Chart1.Series("NUMBER OF CLIENTS").Points.AddXY(READER.GetString("provDesc"), user)
        End While
        conn.Close()

    End Sub

    'chart for province filter'
    Public Sub QueryForChartProvince()
        Chart1.Series("NUMBER OF CLIENTS").Points.Clear()
        conn = New MySqlConnection(cnstr)
        conn.Open()
        Dim sql As String = "SELECT * FROM refcitymun where  provCode = '" + provcode + "' "
        cmd = New MySqlCommand(sql, conn)
        READER = cmd.ExecuteReader
        While READER.Read
            Chart1.Series("NUMBER OF CLIENTS").Points.AddXY(READER.GetString("citymunDesc"), user)
        End While
        conn.Close()
    End Sub

    'chart for city filter'
    Public Sub QueryForChartCity()
        Chart1.Series("NUMBER OF CLIENTS").Points.Clear()
        conn = New MySqlConnection(cnstr)
        conn.Open()
        Dim sql As String = "SELECT * FROM refbrgy where  citymunCode = '" + citycode + "' "
        cmd = New MySqlCommand(sql, conn)
        READER = cmd.ExecuteReader
        While READER.Read
            Chart1.Series("NUMBER OF CLIENTS").Points.AddXY(READER.GetString("brgyDesc"), user)
        End While
        conn.Close()
    End Sub
    Dim user As Integer = 100
    'chart for Barangay filter'
    Public Sub QueryForChartBarangay()
        Chart1.Series("NUMBER OF CLIENTS").Points.Clear()
        conn = New MySqlConnection(cnstr)
        conn.Open()
        Dim sql As String = "SELECT * FROM useraddress"
        cmd = New MySqlCommand(sql, conn)
        READER = cmd.ExecuteReader
        While READER.Read
            Chart1.Series("NUMBER OF CLIENTS").Points.AddXY(READER.GetString("address"), user)
        End While
        conn.Close()
    End Sub

    Public Sub iadd(i)

    End Sub
    Dim brgycodes(4000) As String


    Public Sub loadbrgycode()
        conn = New MySqlConnection(cnstr)
        conn.Open()
        Dim sql As String = "SELECT brgyCode FROM refbrgy"
        cmd = New MySqlCommand(sql, conn)
        READER = cmd.ExecuteReader


        While READER.Read
            TextBox1.Text = READER.GetString("brgyCode")
        End While
        MessageBox.Show("total barangay code read is: " + i.ToString)
    End Sub
    Dim i As Integer
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim i As New Integer
        brgycodes(i) = TextBox1.Text
        i = i + 1
    End Sub
    Private Sub STATISTICS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call QueryForRegion()
    End Sub



    'pag pili ng region'
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        conn = New MySqlConnection(cnstr)
        conn.Open()
        Dim sql As String = "SELECT regCode FROM refregion WHERE regDesc = '" + ComboBox1.SelectedItem + "'"
        cmd = New MySqlCommand(sql, conn)
        READER = cmd.ExecuteReader

        While READER.Read
            regioncode = READER.GetString("regCode")
        End While
        ComboBox2.Items.Clear()
        ComboBox2.ResetText()
        Call QueryForProvince()
        Call QueryForChartregion()
    End Sub
    'pag pili ng province'
    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        conn = New MySqlConnection(cnstr)
        conn.Open()
        Dim sql As String = "SELECT provCode FROM refprovince WHERE provDesc = '" + ComboBox2.SelectedItem + "'"
        cmd = New MySqlCommand(sql, conn)
        READER = cmd.ExecuteReader

        While READER.Read
            provcode = READER.GetString("provCode")
        End While
        ComboBox3.Items.Clear()
        ComboBox3.ResetText()
        Call QueryForCity()
        Call QueryForChartProvince()
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        conn = New MySqlConnection(cnstr)
        conn.Open()
        Dim sql As String = "SELECT citymunCode FROM refcitymun WHERE citymunDesc = '" + ComboBox3.SelectedItem + "'"
        cmd = New MySqlCommand(sql, conn)
        READER = cmd.ExecuteReader

        While READER.Read
            citycode = READER.GetString("citymunCode")
        End While
        ComboBox4.Items.Clear()
        ComboBox4.ResetText()
        Call QueryForbarangay()
        Call QueryForChartCity()
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        conn = New MySqlConnection(cnstr)
        conn.Open()
        Dim sql As String = "SELECT brgyCode FROM refbrgy WHERE brgyDesc = '" + ComboBox4.SelectedItem + "'"
        cmd = New MySqlCommand(sql, conn)
        READER = cmd.ExecuteReader

        While READER.Read
            brgycode = READER.GetString("brgyCode")
        End While

        Call QueryForCity()

        Call QueryForChartBarangay()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Call loadbrgycode()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Hide()
        PROCESS_VERIFICATION.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Hide()
    End Sub
End Class
