Imports System.Data.OleDb
Public Class MenuUtama
    Private Sub KeluarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KeluarToolStripMenuItem.Click
        End
    End Sub

    Sub Terkunci()
        LoginToolStripMenuItem.Enabled = True
        LogoutToolStripMenuItem.Enabled = False
        MasterToolStripMenuItem.Enabled = False
        TransaksiToolStripMenuItem.Enabled = False
        LaporanToolStripMenuItem.Enabled = False
        ResetToolStripMenuItem.Visible = False
    End Sub

    Sub KondisiAwal()
        STLabel2.Text = ""
        STLabel4.Text = ""
        STLabel6.Text = ""
    End Sub

    Private Sub MenuUtama_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Terkunci()
        STLabel10.Text = Today
        'Login.ShowDialog()
    End Sub

    Private Sub LoginToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoginToolStripMenuItem.Click
        Login.TextBox1.Focus()
        Login.ShowDialog()
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogoutToolStripMenuItem.Click
        Call Terkunci()
        KondisiAwal()
        Login.ShowDialog()
    End Sub

    Private Sub AdminToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdminToolStripMenuItem.Click
        MasterAdmin.ShowDialog()
    End Sub

    Private Sub SupplierToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupplierToolStripMenuItem.Click
        MasterSupplier.ShowDialog()
    End Sub

    Private Sub KategoriBarangToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KategoriBarangToolStripMenuItem.Click
        MasterKategoriBarang.ShowDialog()
    End Sub

    Private Sub BarangToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarangToolStripMenuItem.Click
        MasterBarang.ShowDialog()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        STLabel8.Text = TimeOfDay
    End Sub

    Private Sub PenerimaanBarangToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PenerimaanBarangToolStripMenuItem.Click
        TransTerimaBarang.ShowDialog()
    End Sub

    Private Sub PenjualanBarangToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PenjualanBarangToolStripMenuItem.Click
        TransPenjualanBarang.ShowDialog()
    End Sub

    'Private Sub DetailTerimaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DetailTerimaToolStripMenuItem.Click
    '    AxCrystalReport1.ReportFileName = "DetailTerima.rpt"
    '    AxCrystalReport1.WindowState = Crystal.WindowStateConstants.crptMaximized
    '    AxCrystalReport1.RetrieveDataFiles()
    '    AxCrystalReport1.Action = 1
    'End Sub

    'Private Sub DetailJualToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DetailJualToolStripMenuItem.Click
    '    AxCrystalReport2.ReportFileName = "DetailJual.rpt"
    '    AxCrystalReport2.WindowState = Crystal.WindowStateConstants.crptMaximized
    '    AxCrystalReport2.RetrieveDataFiles()
    '    AxCrystalReport2.Action = 1
    'End Sub

    Private Sub ResetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResetToolStripMenuItem.Click
        If MessageBox.Show("Yakin Ingin Mereset Ulang ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
            Call Koneksi()
            Dim EditData As String = "Delete From TBL_DetailJual"
            CMD = New OleDbCommand(EditData, CONN)
            CMD.ExecuteNonQuery()
            Dim EditData2 As String = "Delete From TBL_DetailTerima"
            CMD = New OleDbCommand(EditData2, CONN)
            CMD.ExecuteNonQuery()
            Dim EditData3 As String = "Delete From TBL_Jual"
            CMD = New OleDbCommand(EditData3, CONN)
            CMD.ExecuteNonQuery()
            Dim EditData4 As String = "Delete From TBL_Terima"
            CMD = New OleDbCommand(EditData4, CONN)
            CMD.ExecuteNonQuery()
            'Dim EditData5 As String = "Update From TBL_Barang Set JumlahBarang='" & "0" & "'"
            'CMD = New OleDbCommand(EditData5, CONN)
            'CMD.ExecuteNonQuery()
            CMD = New OleDbCommand("Select * From TBL_Barang ", CONN)
            RD = CMD.ExecuteReader
            RD.Read()
            Dim KurangiStok As String = "Update TBL_Barang Set JumlahBarang='" & "0" & "'"
            CMD = New OleDbCommand(KurangiStok, CONN)
            CMD.ExecuteNonQuery()
            'Call KondisiAwal()
            MsgBox("Data Berhasil Di Reset ...! ")
        End If
    End Sub

    Private Sub LaporanToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LaporanToolStripMenuItem.Click
        Laporan.ShowDialog()
    End Sub
End Class
