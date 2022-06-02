Imports System.Data
Imports System.Data.OleDb
Public Class Form6
    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim mytimestamp As String = DateTime.Now.ToString("d")
        returndate.Text = mytimestamp
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
                    borwdate.Text = sdr("BorrowDate").ToShortDateString()
                    due_date.Text = sdr("DueDate").ToShortDateString()
                End Using
                con.Close()
            End Using
        End Using
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MUDB.accdb")
        Dim OleDb = "Select Copies From Book Where ISBN = @ISBN"
        Using cmd As New OleDbCommand(OleDb, con)
            cmd.Parameters.Add("@ISBN", OleDbType.Decimal).Value = TextBox1.Text
            cmd.Connection.Open()
            cmd.CommandText = "UPDATE Book SET Copies=Copies+1 WHERE ISBN=@ISBN"
            cmd.ExecuteNonQuery()
            Try
                Dim ds1 As New DataSet
                Dim da1 As New OleDbDataAdapter("SELECT * FROM Borrow", con)
                If da1.Fill(ds1) Then
                    Dim cb As OleDbCommand
                    cb = New OleDbCommand("INSERT into Return (ProcessNum, ISBN, UserID, BorrowDate, ReturnDate, DueDate) values ('" & Label3.Text & "','" & TextBox1.Text & "','" & Idno.Text & "','" & borwdate.Text & "','" & returndate.Text & "','" & due_date.Text & "')", con)
                    cb.ExecuteNonQuery()
                    Dim cmd1 As OleDbCommand = New OleDbCommand("DELETE * FROM Borrow WHERE ISBN ='" & TextBox1.Text & "'", con)
                    cmd1.ExecuteNonQuery()
                Else
                    MsgBox(MsgBoxStyle.Critical, "Oledb Error")
                End If
            Catch ex As Exception
            End Try
            MsgBox("BOOK RETURNED SUCCESSFULLY!", MessageBoxIcon.Information)
        End Using
        Me.Close()
    End Sub
    Private Sub Label3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.TextChanged
        Dim rn As Random
        Dim num As Integer
        rn = New Random
        num = rn.Next(400000, 700000)
        Label3.Text = num.ToString
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form2.Show()
        Me.Close()
    End Sub
End Class