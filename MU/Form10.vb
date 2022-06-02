Imports System.Data.OleDb
Public Class Form10
    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label6.Text = Form1.TextBox1.Text
    End Sub
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress
        If Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub AddBook_Click(sender As Object, e As EventArgs) Handles AddBook.Click
        If TextBox1.TextLength = 9 And TextBox5.TextLength = 1 And TextBox2.TextLength > 5 And TextBox3.TextLength > 5 And TextBox4.TextLength > 5 Then
            Dim dr As DialogResult = MessageBox.Show("ARE YOU SURE TO ADD THIS BOOK?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If dr = DialogResult.Yes Then
                Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Hsvi\source\repos\MU\MU\MUDB.accdb")
                Dim cmd As OleDbCommand = New OleDbCommand("INSERT into Book values('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "')", con)
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
                MsgBox("BOOK HAS BEEN ADDED SUCCESSFULLY!", MessageBoxIcon.Information, "Yes")
            ElseIf dr = DialogResult.No Then
                MsgBox("BOOK WAS NOT ADDED!", MessageBoxIcon.Information, "No")
            End If
        Else
            MsgBox("SOMETHING IS WRONG!", MessageBoxIcon.Error, "Invalid Value")
        End If
    End Sub
    Private Sub ClearAll_Click(sender As Object, e As EventArgs) Handles ClearAll.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form1.Show()
        Me.Close()
    End Sub
End Class