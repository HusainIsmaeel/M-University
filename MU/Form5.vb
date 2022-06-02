Imports System.Data
Imports System.Data.OleDb
Public Class Form5
    Private Sub Form5_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mytimestamp As String = DateTime.Now.ToString("d")
        Dim mytimestamp2 As String = DateTime.Now.AddDays(14).ToString("d")
        brwdate.Text = mytimestamp
        duedate.Text = mytimestamp2
        Idno.Text = Form1.TextBox1.Text
        TextBox1.Text = My.Computer.Clipboard.GetText()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MUDB.accdb")

        Dim OleDb = "Select Copies From Book Where ISBN = @ISBN"
        Dim AvailableCopies As Integer
        Using cmd As New OleDbCommand(OleDb, con)
            cmd.Parameters.Add("@ISBN", OleDbType.Decimal).Value = TextBox1.Text
            cmd.Connection.Open()
            AvailableCopies = CInt(cmd.ExecuteScalar)
            If AvailableCopies > 0 Then
                cmd.CommandText = "UPDATE Book SET Copies=Copies-1 WHERE ISBN=@ISBN"
                cmd.ExecuteNonQuery()
                Try
                    Dim ds1 As New DataSet
                    Dim da1 As New OleDbDataAdapter("SELECT * FROM Borrow", con)
                    If da1.Fill(ds1) Then
                        Dim cb As OleDbCommand
                        cb = New OleDbCommand("INSERT into Borrow values('" & Label3.Text & "','" & TextBox1.Text & "','" & Idno.Text & "','" & brwdate.Text & "','" & duedate.Text & "')", con)
                        cb.ExecuteNonQuery()
                    Else
                        MsgBox(MsgBoxStyle.Critical, "Oledb Error")
                    End If
                Catch ex As Exception
                End Try
                MsgBox("BOOK BORROWED SUCCESSFULLY!", MessageBoxIcon.Information)
            Else
                MsgBox("NO COPIES AVAILABLE!", MessageBoxIcon.Error)
            End If
        End Using
        Me.Close()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form2.Show()
        Me.Close()
    End Sub
    Private Sub Label3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.TextChanged
        Dim rn As Random
        Dim num As Integer
        rn = New Random
        num = rn.Next(100000, 400000)
        Label3.Text = num.ToString
    End Sub
End Class