Imports System.Data
Imports System.Data.OleDb
Public Class Form3
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Idno.Text = Form1.TextBox1.Text
        Dim constr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MUDB.accdb"
        Using con As OleDbConnection = New OleDbConnection(constr)
            Using cmd As OleDbCommand = New OleDbCommand("SELECT ISBN, BorrowDate, DueDate FROM Borrow WHERE UserID ='" & Idno.Text & "'")
                cmd.CommandType = CommandType.Text
                cmd.Connection = con
                con.Open()
                Using sdr As OleDbDataReader = cmd.ExecuteReader()
                    sdr.Read()
                    TextBox1.Text = sdr("ISBN").ToString()
                    brwdt.Text = sdr("BorrowDate").ToShortDateString()
                    duedt.Text = sdr("DueDate").ToShortDateString()
                End Using
                con.Close()
            End Using
        End Using
    End Sub
    Private Sub Label16_TextChanged(sender As Object, e As EventArgs) Handles Label16.TextChanged
        Dim rn As Random
        Dim num As Integer
        rn = New Random
        num = rn.Next(700000, 999999)
        Label16.Text = num.ToString
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, v As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim TotalFine As Decimal
        If ComboBox1.SelectedItem = "Lost" Then
            TotalFine = 20
        ElseIf ComboBox1.SelectedItem = "Renew" Then
            TotalFine = 2
        End If

        If ComboBox1.SelectedItem = "Lost" Then
            Label15.Text = "Lost Date:"
        ElseIf ComboBox1.SelectedItem = "Renew" Then
            Label15.Text = "Renew Date:"
        End If
        Label15.Visible = True

        If ComboBox1.SelectedItem = "Lost" Then
            Dim mytimestampL As String = DateTime.Now.ToString("d")
            lostdate.Text = mytimestampL
        ElseIf ComboBox1.SelectedItem = "Renew" Then
            Dim mytimestampR As String = DateTime.Now.AddDays(14).ToString("d")
            lostdate.Text = mytimestampR
        End If
        lostdate.Visible = True

        Label11.Visible = True
        If Label11.Visible = True Then
            Label13.Visible = True
            Label11.Text = TotalFine
        End If
    End Sub
    Private Sub Label6_TextChanged(sender As Object, e As EventArgs)
        Label6.Text = Form1.TextBox1.Text
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form2.Show()
        Me.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MUDB.accdb")

        If ComboBox1.SelectedItem = "Renew" Then
            Dim OleDb = "Select DueDate From Borrow Where ISBN = @ISBN"
            Using cmd As New OleDbCommand(OleDb, con)
                cmd.Parameters.Add("@ISBN", OleDbType.Decimal).Value = TextBox1.Text
                cmd.Connection.Open()
                cmd.CommandText = "UPDATE Borrow SET DueDate='" & lostdate.Text & "' WHERE ISBN=@ISBN"
                cmd.ExecuteNonQuery()
                Try
                    Dim ds1 As New DataSet
                    Dim da1 As New OleDbDataAdapter("SELECT * FROM Borrow", con)
                    If da1.Fill(ds1) Then
                        Dim cb As OleDbCommand
                        cb = New OleDbCommand("INSERT into Issue (ProcessNum, ISBN, UserID, BorrowDate, LostDate, DueDate, IssueType) values ('" & Label16.Text & "','" & TextBox1.Text & "','" & Idno.Text & "','" & brwdt.Text & "','" & lostdate.Text & "','" & duedt.Text & "','" & ComboBox1.Text & "')", con)
                        cb.ExecuteNonQuery()
                    Else
                        MsgBox(MsgBoxStyle.Critical, "Oledb Error")
                    End If
                Catch ex As Exception
                End Try
                MsgBox("BORROW HAS BEEN RENEWED SUCCESSFULLY!" + vbCrLf + "FEES WILL BE ADDED TO YOUR COURSE FEES.", MessageBoxIcon.Information)
            End Using
        ElseIf ComboBox1.SelectedItem = "Lost" Then
            Dim cb1 As OleDbCommand
            Dim cb2 As OleDbCommand
            con.Open()
            cb1 = New OleDbCommand("INSERT into Issue (ProcessNum, ISBN, UserID, BorrowDate, LostDate, DueDate, IssueType) values ('" & Label16.Text & "','" & TextBox1.Text & "','" & Idno.Text & "','" & brwdt.Text & "','" & lostdate.Text & "','" & duedt.Text & "','" & ComboBox1.Text & "')", con)
            cb1.ExecuteNonQuery()
            cb2 = New OleDbCommand("DELETE * FROM Borrow WHERE ISBN ='" & TextBox1.Text & "'", con)
            cb2.ExecuteNonQuery()
            con.Close()
            MsgBox("BOOK HAS BEEN MARKED AS LOST!" + vbCrLf + "FEES WILL BE ADDED TO YOUR COURSE FEES.", MessageBoxIcon.Information)
        End If
    End Sub
End Class