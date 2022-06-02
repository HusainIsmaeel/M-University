Imports System.Data.OleDb
Imports System.Data
Imports System.IO
Public Class Form2
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label6.Text = Form1.TextBox1.Text
        LoadDatainGrid()
    End Sub
    Public Sub LoadDatainGrid()
        If con.State = 1 Then con.Close()
        ds.Clear()
        BookDataGridView.DataSource = Nothing
        qry = "SELECT ISBN, Title, Category, Author, Copies FROM Book"
        ds = FetchData(qry)
        If ds.Tables(0).Rows.Count > 0 Then
            BookDataGridView.DataSource = ds.Tables(0)
        Else
            MessageBox.Show("NOT FOUND!")
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        BookDataGridView.DataSource = Nothing
        If TextBox1.Text <> "" Then
            If con.State = 1 Then con.Close()
            ds.Clear()
            BookDataGridView.DataSource = Nothing
            qry = "SELECT * FROM Book WHERE Title LIKE'%" & TextBox1.Text & "%' or Category LIKE '%" & TextBox1.Text & "%' or Author LIKE '%" & TextBox1.Text & "%' or ISBN LIKE '%" & TextBox1.Text & "%'"
            ds = FetchData(qry)
            If ds.Tables(0).Rows.Count > 0 Then
                BookDataGridView.DataSource = ds.Tables(0)
            Else
                MessageBox.Show("NOT FOUND!")
                Exit Sub
            End If
        ElseIf TextBox1.Text = "" Then
            LoadDatainGrid()
        End If
    End Sub
    Private Sub BookDataGridView_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles BookDataGridView.CellDoubleClick
        Dim SelectedISBN As String = BookDataGridView.GetClipboardContent().GetText()
        If SelectedISBN.Length = 9 Then
            Label7.Visible = True
            Clipboard.SetText(SelectedISBN)
            Label7.ForeColor = Color.White
            Label7.Text = "ISBN Copied: " & SelectedISBN
        Else
            Label7.Visible = True
            Label7.ForeColor = Color.Black
            Label7.Text = "Double-Click an ISBN Only!"
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form1.Show()
        Me.Close()
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MUDB.accdb")

        Dim OleDb = "Select * From Borrow Where UserID = @UserID"
        Dim BorrowedBook As Integer
        Using cmd As New OleDbCommand(OleDb, con)
            cmd.Parameters.Add("@UserID", OleDbType.Decimal).Value = Label6.Text
            cmd.Connection.Open()
            BorrowedBook = CInt(cmd.ExecuteScalar)
            If BorrowedBook = 0 Then
                MsgBox("YOU DONT HAVE A BORROWED BOOK!", MessageBoxIcon.Error)
            Else
                Form3.Show()
            End If
        End Using
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MUDB.accdb")

        Dim OleDb = "Select * From Borrow Where UserID = @UserID"
        Dim BorrowedBook As Integer
        Using cmd As New OleDbCommand(OleDb, con)
            cmd.Parameters.Add("@UserID", OleDbType.Decimal).Value = Label6.Text
            cmd.Connection.Open()
            BorrowedBook = CInt(cmd.ExecuteScalar)
            If BorrowedBook > 0 Then
                MsgBox("YOU ALREADY HAVE BORROWED A BOOK!", MessageBoxIcon.Error)
            Else
                Form5.Show()
            End If
        End Using
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MUDB.accdb")

        Dim OleDb = "Select * From Borrow Where UserID = @UserID"
        Dim BorrowedBook As Integer
        Using cmd As New OleDbCommand(OleDb, con)
            cmd.Parameters.Add("@UserID", OleDbType.Decimal).Value = Label6.Text
            cmd.Connection.Open()
            BorrowedBook = CInt(cmd.ExecuteScalar)
            If BorrowedBook = 0 Then
                MsgBox("YOU DONT HAVE A BORROWED BOOK!", MessageBoxIcon.Error)
            Else
                Form6.Show()
            End If
        End Using
    End Sub
End Class