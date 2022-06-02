Imports System.Data
Imports System.Data.OleDb
Public Class Form8
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
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form7.Show()
        Me.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\MU\MU\MUDB.accdb")
            Dim ds1 As New DataSet

            Dim da1 As New OleDbDataAdapter("SELECT * FROM Book WHERE ISBN='" & Trim(TextBox1.Text) & "'", con)

            If da1.Fill(ds1) Then

                Dim ra As Integer
                Dim cb As OleDbCommand
                con.Open()
                cb = New OleDbCommand("Update Book Set [Title]='" & TextBox2.Text & "', [Category]='" & TextBox3.Text & "', [Author]='" & TextBox4.Text & "', [Copies]='" & TextBox5.Text & "' WHERE ISBN='" & TextBox1.Text & "'", con)
                ra = cb.ExecuteNonQuery()
                MessageBox.Show("Book has been updated successfully!")
                con.Close()
                Form7.Show()
                Me.Close()
            Else
                MsgBox(MsgBoxStyle.Critical, "Invalid ISBN!")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Me.Close()
    End Sub

    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class