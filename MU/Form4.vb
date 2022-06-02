Imports System.Data
Imports System.Data.OleDb
Public Class Form4
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If NewPassword.TextLength < 5 Then
            MsgBox("Your new password should contain 5 or more characters")
            NewPassword.Text = ""
            NewPassword2.Text = ""
        ElseIf CurrentPassword.Text = NewPassword.Text Then
            MsgBox("The New Password is Same As Old Password")
            NewPassword.Text = ""
            NewPassword.Focus()
        ElseIf (NewPassword.Text = NewPassword2.Text) Then

            Try
                Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MUDB.accdb")
                Dim ds1 As New DataSet

                Dim da1 As New OleDbDataAdapter("select * from Users where UserID='" & Trim(UserID.Text) & "'and Password='" & Trim(CurrentPassword.Text) & "'", con)

                If da1.Fill(ds1) Then

                    Dim ra As Integer
                    Dim cb As OleDbCommand
                    con.Open()
                    cb = New OleDbCommand("Update Users Set [Password]='" & NewPassword.Text & "' where UserID='" & UserID.Text & "'", con)
                    ra = cb.ExecuteNonQuery()
                    MessageBox.Show("Password Changed Successfully!")
                    con.Close()
                    Form1.Show()
                    Me.Hide()
                Else
                    MsgBox(MsgBoxStyle.Critical, "Invalid ID or Password!")

                End If

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class