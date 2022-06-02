Imports System.Data.OleDb
Imports System.Data
Module Module1
    Public connnectionstring As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MUDB.accdb"
    Public con As New OleDbConnection(connnectionstring)
    Public cmd As New OleDbCommand
    Public da As New OleDbDataAdapter
    Public dr As OleDbDataReader
    Public ds As New DataSet

    Public qry As String = ""

    Public Function FetchData(ByVal qry As String) As DataSet
        If con.State = 1 Then con.Close()
        con.Open()
        da = New OleDbDataAdapter(qry, con)
        ds = New DataSet
        da.Fill(ds)
        Return ds
        con.Close()
    End Function
End Module
