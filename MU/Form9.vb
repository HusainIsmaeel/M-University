Imports System.Data
Imports System.Data.OleDb
Public Class Form9
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim constr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MUDB.accdb"
        Using con As OleDbConnection = New OleDbConnection(constr)
            Using cmd As OleDbCommand = New OleDbCommand("SELECT Title, Category, Author, Copies FROM Book WHERE ISBN ='" & TextBox1.Text & "'")
                cmd.CommandType = CommandType.Text
                cmd.Connection = con
                con.Open()
                Using sdr As OleDbDataReader = cmd.ExecuteReader()
                    sdr.Read()
                    TextBox2.Text = sdr("Title").ToString()
                    TextBox3.Text = sdr("Category").ToString()
                    TextBox4.Text = sdr("Author").ToString()
                    TextBox5.Text = sdr("Copies").ToString()
                End Using
                con.Close()
            End Using
        End Using
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\MU\MU\MUDB.accdb")
        Dim cmd As OleDbCommand = New OleDbCommand("DELETE * FROM Book WHERE ISBN ='" & TextBox1.Text & "'", con)
        con.Open()
        cmd.ExecuteNonQuery()
        con.Close()
        MsgBox("BOOK HAS BEEN DELETED SUCCESSFULLY!", MessageBoxIcon.Information)
        Me.Close()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Form9_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class