Imports System.Data.OleDb
Public Class Form1
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim cmd As OleDbCommand = New OleDbCommand("SELECT * FROM Users WHERE UserID='" & TextBox1.Text & "' AND Password='" & TextBox2.Text & "'", con)
        If TextBox1.Text.StartsWith("100") Then
            con.Open()
            Try
                Dim sdr As OleDbDataReader = cmd.ExecuteReader()

                If (sdr.Read() = True) Then
                    Form7.Label6.Text = ""
                    Form7.Show()
                    Me.Hide()
                Else
                    MessageBox.Show("Invalid ID or Password!")
                End If

            Catch ex As Exception

                MsgBox(ex.Message, MsgBoxStyle.Critical, "Oledb Error")

            End Try
            con.Close()
        ElseIf TextBox1.Text.StartsWith("200") Then
            con.Open()
            Try
                Dim sdr As OleDbDataReader = cmd.ExecuteReader()

                If (sdr.Read() = True) Then
                    Form10.Label6.Text = ""
                    Form10.Show()
                    Me.Hide()
                Else
                    MessageBox.Show("Invalid ID or Password!")
                End If

            Catch ex As Exception

                MsgBox(ex.Message, MsgBoxStyle.Critical, "Oledb Error")

            End Try
            con.Close()
        Else
            con.Open()
            Try
                Dim sdr As OleDbDataReader = cmd.ExecuteReader()

                If (sdr.Read() = True) Then
                    Form2.Label6.Text = ""
                    Form2.Show()
                    Me.Hide()
                Else
                    MessageBox.Show("Invalid ID or Password!")
                End If

            Catch ex As Exception

                MsgBox(ex.Message, MsgBoxStyle.Critical, "Oledb Error")

            End Try
            con.Close()
        End If
    End Sub
    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Form4.Show()
        Me.Hide()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        TextBox2.UseSystemPasswordChar = Not CheckBox1.Checked
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
