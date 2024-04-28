Imports System
Imports System.Data.Odbc

Public Class Form2
    Dim ds As DataSet
    Dim conn As OdbcConnection
    Dim dr As OdbcDataReader
    Dim da As OdbcDataAdapter
    Dim cmd As OdbcCommand
    Dim Mydatabase As String
    Sub connection()
        Try
            Mydatabase = "Driver={MySQL ODBC 8.3 ANSI Driver};Database=latihan2;Server=localhost;uid=root;"
            conn = New OdbcConnection(Mydatabase)
            conn.Open()
            MsgBox("data berhasil")
        Catch ex As Exception
            MsgBox("Data tidak berhasil")
        End Try
    End Sub

    Sub tampilkanData()
        da = New OdbcDataAdapter("select * from inventory", conn)
        ds = New DataSet
        da.Fill(ds, "inventory")
        DataGridView1.DataSource = ds.Tables(0)
    End Sub

    Sub clear()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
    End Sub

    Sub nyalakanform()
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
    End Sub

    Sub matikanform()
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
    End Sub
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call connection()
        Call tampilkanData()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call matikanform()
        Call clear()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Call clear()
        Call nyalakanform()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            Call connection()
            Dim delete As String
            delete = "delete inventory where id='" & TextBox4.Text & "'"
            MsgBox("Data berhasil dihapus")
        Catch ex As Exception
            MsgBox("Data  tidak berhasil dihapus")
        End Try
        Call clear()
        Call tampilkanData()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call nyalakanform()
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Harap isi data")
        Else
            'Call connection()
            cmd = New OdbcCommand("select * from inventory where id='" & TextBox1.Text & "' ", conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If Not dr.HasRows Then
                'Call connection()
                Dim simpan As String
                simpan = "insert into inventory (name,jenis,stock) value('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "')"
                cmd = New OdbcCommand(simpan, conn)
                cmd.ExecuteNonQuery()
                MsgBox("data berhasil input")
            Else
                MsgBox("data tidak berhasil di input")

            End If
            Call tampilkanData()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Harap isi data")
        Else
            'Call connection()
            cmd = New OdbcCommand("select * from inventory where id='" & TextBox1.Text & "' ", conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If Not dr.HasRows Then
                'Call connection()
                Dim update As String
                update = "update inventory set name='" & TextBox1.Text & "',jenis='" & TextBox2.Text & "',stock='" & TextBox3.Text & "' where id='" & TextBox4.Text & "' "
                cmd = New OdbcCommand(update, conn)
                cmd.ExecuteNonQuery()
                MsgBox("data berhasil update")
            Else
                MsgBox("data tidak berhasil")
            End If
            Call clear()
            Call tampilkanData()
        End If
    End Sub
End Class