Imports System.Data.OleDb
Imports System.Data
Imports System.IO
Public Class Form7
    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label6.Text = Form1.TextBox1.Text
        LoadDatainGrid()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Label1.Text = "BOOKS DETAILS"
        BookDataGridView.Visible = True
        DataGridView1.Visible = False
        DataGridView2.Visible = False
        DataGridView3.Visible = False
        DataGridView4.Visible = False
        LoadDatainGrid()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Label1.Text = "BORROWS"
        DataGridView1.Visible = True
        BookDataGridView.Visible = False
        DataGridView2.Visible = False
        DataGridView3.Visible = False
        DataGridView4.Visible = False
        LoadDatainGrid1()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Label1.Text = "RETURNS"
        DataGridView2.Visible = True
        BookDataGridView.Visible = False
        DataGridView1.Visible = False
        DataGridView3.Visible = False
        DataGridView4.Visible = False
        LoadDatainGrid2()
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Label1.Text = "ISSUES"
        DataGridView3.Visible = True
        BookDataGridView.Visible = False
        DataGridView1.Visible = False
        DataGridView2.Visible = False
        DataGridView4.Visible = False
        LoadDatainGrid3()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Label1.Text = "USERS DETAILS"
        DataGridView4.Visible = True
        BookDataGridView.Visible = False
        DataGridView1.Visible = False
        DataGridView2.Visible = False
        DataGridView3.Visible = False
        LoadDatainGrid4()
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
    Public Sub LoadDatainGrid1()
        If con.State = 1 Then con.Close()
        ds.Clear()
        DataGridView1.DataSource = Nothing
        qry = "SELECT ProcessNo, ISBN, UserID, BorrowDate, DueDate FROM Borrow"
        ds = FetchData(qry)
        If ds.Tables(0).Rows.Count > 0 Then
            DataGridView1.DataSource = ds.Tables(0)
        Else
            MessageBox.Show("NOT FOUND!")
        End If
    End Sub
    Public Sub LoadDatainGrid2()
        If con.State = 1 Then con.Close()
        ds.Clear()
        DataGridView2.DataSource = Nothing
        qry = "SELECT ProcessNum, ISBN, UserID, BorrowDate, ReturnDate, DueDate FROM Return"
        ds = FetchData(qry)
        If ds.Tables(0).Rows.Count > 0 Then
            DataGridView2.DataSource = ds.Tables(0)
        Else
            MessageBox.Show("NOT FOUND!")
        End If
    End Sub
    Public Sub LoadDatainGrid3()
        If con.State = 1 Then con.Close()
        ds.Clear()
        DataGridView3.DataSource = Nothing
        qry = "SELECT ProcessNum, ISBN, UserID, BorrowDate, DueDate, LostDate, IssueType FROM Issue"
        ds = FetchData(qry)
        If ds.Tables(0).Rows.Count > 0 Then
            DataGridView3.DataSource = ds.Tables(0)
        Else
            MessageBox.Show("NOT FOUND!")
        End If
    End Sub
    Public Sub LoadDatainGrid4()
        If con.State = 1 Then con.Close()
        ds.Clear()
        DataGridView4.DataSource = Nothing
        qry = "SELECT UserID, Username, Phone, Address, UserType FROM Users"
        ds = FetchData(qry)
        If ds.Tables(0).Rows.Count > 0 Then
            DataGridView4.DataSource = ds.Tables(0)
        Else
            MessageBox.Show("NOT FOUND!")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If BookDataGridView.Visible = True Then
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
        ElseIf DataGridView1.Visible = True Then
            DataGridView1.DataSource = Nothing
            If TextBox1.Text <> "" Then
                If con.State = 1 Then con.Close()
                ds.Clear()
                DataGridView1.DataSource = Nothing
                qry = "SELECT * FROM Borrow WHERE ProcessNo LIKE'%" & TextBox1.Text & "%' or ISBN LIKE '%" & TextBox1.Text & "%' or UserID LIKE '%" & TextBox1.Text & "%' or BorrowDate LIKE '%" & TextBox1.Text & "%' or DueDate LIKE '%" & TextBox1.Text & "%'"
                ds = FetchData(qry)
                If ds.Tables(0).Rows.Count > 0 Then
                    DataGridView1.DataSource = ds.Tables(0)
                Else
                    MessageBox.Show("NOT FOUND!")
                    Exit Sub
                End If
            ElseIf TextBox1.Text = "" Then
                LoadDatainGrid1()
            End If
        ElseIf DataGridView2.Visible = True Then
            DataGridView2.DataSource = Nothing
            If TextBox1.Text <> "" Then
                If con.State = 1 Then con.Close()
                ds.Clear()
                DataGridView2.DataSource = Nothing
                qry = "SELECT * FROM Return WHERE ProcessNum LIKE'%" & TextBox1.Text & "%' or ISBN LIKE '%" & TextBox1.Text & "%' or UserID LIKE '%" & TextBox1.Text & "%' or BorrowDate LIKE '%" & TextBox1.Text & "%' or ReturnDate LIKE '%" & TextBox1.Text & "%' or DueDate LIKE '%" & TextBox1.Text & "%'"
                ds = FetchData(qry)
                If ds.Tables(0).Rows.Count > 0 Then
                    DataGridView2.DataSource = ds.Tables(0)
                Else
                    MessageBox.Show("NOT FOUND!")
                    Exit Sub
                End If
            ElseIf TextBox1.Text = "" Then
                LoadDatainGrid2()
            End If
        ElseIf DataGridView3.Visible = True Then
            DataGridView3.DataSource = Nothing
            If TextBox1.Text <> "" Then
                If con.State = 1 Then con.Close()
                ds.Clear()
                DataGridView3.DataSource = Nothing
                qry = "SELECT * FROM Issue WHERE ProcessNum LIKE'%" & TextBox1.Text & "%' or ISBN LIKE '%" & TextBox1.Text & "%' or UserID LIKE '%" & TextBox1.Text & "%' or BorrowDate LIKE '%" & TextBox1.Text & "%' or LostDate LIKE '%" & TextBox1.Text & "%' or DueDate LIKE '%" & TextBox1.Text & "%' or IssueType LIKE '%" & TextBox1.Text & "%'"
                ds = FetchData(qry)
                If ds.Tables(0).Rows.Count > 0 Then
                    DataGridView3.DataSource = ds.Tables(0)
                Else
                    MessageBox.Show("NOT FOUND!")
                    Exit Sub
                End If
            ElseIf TextBox1.Text = "" Then
                LoadDatainGrid3()
            End If
        ElseIf DataGridView4.Visible = True Then
            DataGridView4.DataSource = Nothing
            If TextBox1.Text <> "" Then
                If con.State = 1 Then con.Close()
                ds.Clear()
                DataGridView4.DataSource = Nothing
                qry = "SELECT * FROM Users WHERE UserID LIKE'%" & TextBox1.Text & "%' or Username LIKE '%" & TextBox1.Text & "%' or Phone LIKE '%" & TextBox1.Text & "%' or Address LIKE '%" & TextBox1.Text & "%' or UserType LIKE '%" & TextBox1.Text & "%'"
                ds = FetchData(qry)
                If ds.Tables(0).Rows.Count > 0 Then
                    DataGridView4.DataSource = ds.Tables(0)
                Else
                    MessageBox.Show("NOT FOUND!")
                    Exit Sub
                End If
            ElseIf TextBox1.Text = "" Then
                LoadDatainGrid4()
            End If
        End If
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Form1.Show()
        Me.Close()
    End Sub
    Private Sub BookDataGridView_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles BookDataGridView.CellDoubleClick
        Dim SelectedISBN As String = BookDataGridView.GetClipboardContent().GetText()
        If SelectedISBN.Length = 9 Then
            Label7.Visible = True
            Clipboard.SetText(SelectedISBN)
            Label7.ForeColor = Color.White
            Label7.Text = "Copied ISBN: " & SelectedISBN
        Else
            Label7.Visible = True
            Label7.ForeColor = Color.Black
            Label7.Text = "Double-Click an ISBN Only!"
        End If
    End Sub
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Form8.Show()
    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Form9.Show()
    End Sub

End Class