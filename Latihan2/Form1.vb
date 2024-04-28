Imports System.Data
Imports System.Data.Common
Imports System.Data.Odbc

Public Class Form1
    Dim Mydatabase As String
    Dim ds As DataSet
    Dim Da As OdbcDataAdapter
    Dim conn As OdbcConnection
    Dim cmd As OdbcCommand
    Dim dr As OdbcDataReader

    Sub connection()
        Try
            Mydatabase = "Driver={MySQL ODBC 8.3 ANSI Driver};Database=latihan2;Server=localhost;uid=root;"
            conn = New OdbcConnection(Mydatabase)
            conn.Open()
            ' MsgBox("data berhasil")
        Catch ex As Exception
            MsgBox("Data tidak berhasil ")
        End Try
    End Sub

    Sub tampilkanData()
        Da = New OdbcDataAdapter("select * from login", conn)
        ds = New DataSet
        Da.Fill(ds, "login")
        DataGridView1.DataSource = ds.Tables(0)
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call connection()
        Call tampilkanData()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Call tampilkanData()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("harap isi")
        Else
            Call connection()
            cmd = New OdbcCommand("select * from login where id='" & TextBox1.Text & "' ", conn)
            dr = cmd.ExecuteReader
            dr.Read()

            If Not dr.HasRows Then
                Call connection()
                Dim simpan As String
                simpan = "insert into login (name,pasword) value ('" & TextBox1.Text & "','" & TextBox2.Text & "')"
                cmd = New OdbcCommand(simpan, conn)
                cmd.ExecuteNonQuery()
                Dim form2 As New Form2
                form2.Show()
                Me.Hide()
            Else
                MsgBox("isi kembali")
            End If
            Call tampilkanData()
        End If
    End Sub
End Class
