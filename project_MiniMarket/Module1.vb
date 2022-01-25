Imports System.Data.OleDb
Module Module1
    Public CONN As OleDbConnection
    Public DA As OleDbDataAdapter
    Public DS As DataSet
    Public RD As OleDbDataReader
    Public CMD As OleDbCommand
    Public LokasiData As String
    Public Sub Koneksi()
        LokasiData = "provider=microsoft.jet.oledb.4.0;data source=DBMiniMarket.mdb"
        CONN = New OleDbConnection(LokasiData)
        If CONN.State = ConnectionState.Closed Then CONN.Open()
    End Sub
End Module
