Imports System.Data.OleDb
Public Class TransPenjualanBarang
    Sub KondisiAwal()
        Label9.Text = "Rp"
        Label16.Text = ""
        LBLNamaBarang.Text = ""
        LBLHarga.Text = ""
        TxtBayar.Text = ""
        TxtKembali.Text = ""
        LBLTanggal.Text = Today
        LBLAdmin.Text = MenuUtama.STLabel4.Text
        TextBox1.Focus()
        NomorOtomatis()
        BuatKolom()
        AturGrid()
        TextBox2.Enabled = False
        TxtKembali.Enabled = False
        Button4.Enabled = True

    End Sub

    Sub AturGrid()
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.RowsDefaultCellStyle.BackColor = Color.Cyan
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
    End Sub

    Sub NomorOtomatis()
        Call Koneksi()
        CMD = New OleDbCommand("Select * From TBL_Jual Where NoJual in (Select max(NoJual) From TBL_Jual)", CONN)
        Dim UrutKode As String
        Dim Hitung As Long
        RD = CMD.ExecuteReader
        RD.Read()
        If Not RD.HasRows Then
            UrutKode = "J" + Format(Now, "yyMMdd") + "001"
        Else
            Hitung = Microsoft.VisualBasic.Right(RD.GetString(0), 9) + 1
            UrutKode = "J" + Format(Now, "yyMMdd") + Microsoft.VisualBasic.Right("000" & Hitung, 3)
        End If
        LBLNoJual.Text = UrutKode
    End Sub

    Sub BuatKolom()
        DataGridView1.Columns.Clear()
        DataGridView1.Columns.Add("Kode", "Kode")
        DataGridView1.Columns.Add("Nama", "Nama Barang")
        DataGridView1.Columns.Add("Harga", "Harga")
        DataGridView1.Columns.Add("Jumlah", "Jumlah")
        DataGridView1.Columns.Add("Subtotal", "Subtotal")
    End Sub

    Sub RumusSubtotal()
        Dim Hitung As Integer = 0
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            Hitung = Hitung + DataGridView1.Rows(i).Cells(4).Value
            Label9.Text = Hitung
            Label9.Text = Format(Val(Label9.Text), "Rp, ###,###")
        Next

    End Sub

    Sub RumusCariItem()
        Dim HitungItem As Integer = 0
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            HitungItem = HitungItem + DataGridView1.Rows(i).Cells(3).Value
            Label16.Text = HitungItem
        Next
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        LBLJam.Text = TimeOfDay
    End Sub


    Private Sub TransPenjualanBarang_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        KondisiAwal()
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If LBLNamaBarang.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Silahkan Masukan Kode Barang dan Tekan Enter...!")
        Else
            CMD = New OleDbCommand("Select * From TBL_Barang Where KodeBarang='" & TextBox1.Text & "'", CONN)
            RD = CMD.ExecuteReader
            RD.Read()
            If Val(TextBox2.Text) > RD.Item("JumlahBarang") Then
                MsgBox("Stok/Barang Habis, Tersisa ==> " & RD.Item("JumlahBarang"))
            Else
                LBLHarga.Text = Format(Val(LBLHarga.Text), "Rp, ###,###")
                DataGridView1.Rows.Add(New String() {TextBox1.Text, LBLNamaBarang.Text, LBLHarga.Text, TextBox2.Text, Format(Val(LBLHarga.Text * TextBox2.Text), "Rp, ###,###")})
                RumusSubtotal()
                TextBox1.Text = ""
                LBLNamaBarang.Text = ""
                LBLHarga.Text = ""
                TextBox2.Text = ""
                TextBox2.Enabled = False
                RumusCariItem()
                TextBox1.Focus()
            End If

        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Label9.Text = "Rp" Or TxtBayar.Text = "" Or TxtKembali.Text = "" Then
            MsgBox("Transaksi Tidak Ada, Silahkan Lakukan Transaksi Terlebih Dahulu...!")
        ElseIf MessageBox.Show("Ingin Mencetak Nota ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
            'If Label9.Text = "Rp" Or TxtBayar.Text = "" Or TxtKembali.Text = "" Then
            '    MsgBox("Transaksi Tidak Ada, Silahkan Lakukan Transaksi Terlebih Dahulu...!")
            'Else
            Dim SimpanJual As String = "Insert Into TBL_Jual Values ('" & LBLNoJual.Text & "','" & LBLTanggal.Text & "','" & LBLJam.Text & "','" & Label16.Text & "','" & Label9.Text & "','" & MenuUtama.STLabel2.Text & "')"
            CMD = New OleDbCommand(SimpanJual, CONN)
            CMD.ExecuteNonQuery()

            For Baris As Integer = 0 To DataGridView1.Rows.Count - 2
                Dim SimpanDetail As String = "Insert Into TBL_DetailJual Values('" & LBLNoJual.Text & "','" & DataGridView1.Rows(Baris).Cells(0).Value & "','" & DataGridView1.Rows(Baris).Cells(1).Value & "','" & DataGridView1.Rows(Baris).Cells(2).Value & "','" & DataGridView1.Rows(Baris).Cells(3).Value & "','" & DataGridView1.Rows(Baris).Cells(4).Value & "','" & LBLTanggal.Text & "')"
                CMD = New OleDbCommand(SimpanDetail, CONN)
                CMD.ExecuteNonQuery()

                CMD = New OleDbCommand("Select * From TBL_Barang Where KodeBarang='" & DataGridView1.Rows(Baris).Cells(0).Value & "'", CONN)
                RD = CMD.ExecuteReader
                RD.Read()
                Dim KurangiStok As String = "Update TBL_Barang Set JumlahBarang='" & RD.Item("JumlahBarang") - DataGridView1.Rows(Baris).Cells(3).Value & "' Where KodeBarang='" & DataGridView1.Rows(Baris).Cells(0).Value & "'"
                CMD = New OleDbCommand(KurangiStok, CONN)
                CMD.ExecuteNonQuery()
            Next
            'Call Koneksi()
            'Dim EditData As String = "Delete From TBL_Nota"
            'CMD = New OleDbCommand(EditData, CONN)
            'CMD.ExecuteNonQuery()
            'Dim EditData2 As String = "Delete From TBL_Nota2"
            'CMD = New OleDbCommand(EditData2, CONN)
            'CMD.ExecuteNonQuery()
            'MsgBox("Transaksi Telah Berhasil Disimpan :) ")

            Dim SimpanJual2 As String = "Insert Into TBL_Nota Values ('" & LBLNoJual.Text & "','" & LBLTanggal.Text & "','" & LBLJam.Text & "','" & MenuUtama.STLabel2.Text & "','" & Label9.Text & "','" & TxtBayar.Text & "','" & TxtKembali.Text & "')"
            CMD = New OleDbCommand(SimpanJual2, CONN)
            CMD.ExecuteNonQuery()

            For Baris As Integer = 0 To DataGridView1.Rows.Count - 2
                Dim SimpanDetail As String = "Insert Into TBL_Nota2 Values('" & LBLNoJual.Text & "','" & DataGridView1.Rows(Baris).Cells(1).Value & "','" & DataGridView1.Rows(Baris).Cells(2).Value & "','" & DataGridView1.Rows(Baris).Cells(3).Value & "','" & DataGridView1.Rows(Baris).Cells(4).Value & "')"
                CMD = New OleDbCommand(SimpanDetail, CONN)
                CMD.ExecuteNonQuery()
            Next
            MsgBox("Transaksi Telah Berhasil Disimpan :) ")
            Call KondisiAwal()
            AxCrystalReport1.ReportFileName = "CtkNota.rpt"
            AxCrystalReport1.WindowState = Crystal.WindowStateConstants.crptMaximized
            AxCrystalReport1.RetrieveDataFiles()
            AxCrystalReport1.Action = 1
            Call Koneksi()
            Dim EditData As String = "Delete From TBL_Nota"
            CMD = New OleDbCommand(EditData, CONN)
            CMD.ExecuteNonQuery()
            Dim EditData2 As String = "Delete From TBL_Nota2"
            CMD = New OleDbCommand(EditData2, CONN)
            CMD.ExecuteNonQuery()
        Else

            If Label9.Text = "Rp" Then
                MsgBox("Transaksi Tidak Ada, Silahkan Lakukan Transaksi Terlebih Dahulu...!")
            ElseIf TxtBayar.Text = "" Then
                MsgBox("Anda Belum Melakukan Pembayaran !")
            Else
                Dim SimpanJual As String = "Insert Into TBL_Jual Values ('" & LBLNoJual.Text & "','" & LBLTanggal.Text & "','" & LBLJam.Text & "','" & Label16.Text & "','" & Label9.Text & "','" & MenuUtama.STLabel2.Text & "')"
                CMD = New OleDbCommand(SimpanJual, CONN)
                CMD.ExecuteNonQuery()

                For Baris As Integer = 0 To DataGridView1.Rows.Count - 2
                    Dim SimpanDetail As String = "Insert Into TBL_DetailJual Values('" & LBLNoJual.Text & "','" & DataGridView1.Rows(Baris).Cells(0).Value & "','" & DataGridView1.Rows(Baris).Cells(1).Value & "','" & DataGridView1.Rows(Baris).Cells(2).Value & "','" & DataGridView1.Rows(Baris).Cells(3).Value & "','" & DataGridView1.Rows(Baris).Cells(4).Value & "','" & LBLTanggal.Text & "')"
                    CMD = New OleDbCommand(SimpanDetail, CONN)
                    CMD.ExecuteNonQuery()

                    CMD = New OleDbCommand("Select * From TBL_Barang Where KodeBarang='" & DataGridView1.Rows(Baris).Cells(0).Value & "'", CONN)
                    RD = CMD.ExecuteReader
                    RD.Read()
                    Dim KurangiStok As String = "Update TBL_Barang Set JumlahBarang='" & RD.Item("JumlahBarang") - DataGridView1.Rows(Baris).Cells(3).Value & "' Where KodeBarang='" & DataGridView1.Rows(Baris).Cells(0).Value & "'"
                    CMD = New OleDbCommand(KurangiStok, CONN)
                    CMD.ExecuteNonQuery()
                Next
                Call Koneksi()
                Dim EditData As String = "Delete From TBL_Nota"
                CMD = New OleDbCommand(EditData, CONN)
                CMD.ExecuteNonQuery()
                Dim EditData2 As String = "Delete From TBL_Nota2"
                CMD = New OleDbCommand(EditData2, CONN)
                CMD.ExecuteNonQuery()
                Call KondisiAwal()
                MsgBox("Transaksi Telah Berhasil Disimpan :) ")
            End If
        End If

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Call Koneksi()
        Dim EditData As String = "Delete From TBL_Nota"
        CMD = New OleDbCommand(EditData, CONN)
        CMD.ExecuteNonQuery()
        Dim EditData2 As String = "Delete From TBL_Nota2"
        CMD = New OleDbCommand(EditData2, CONN)
        CMD.ExecuteNonQuery()
        KondisiAwal()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call Koneksi()
            CMD = New OleDbCommand("Select * From TBL_Barang Where KodeBarang ='" & TextBox1.Text & "'", CONN)
            RD = CMD.ExecuteReader
            RD.Read()
            If Not RD.HasRows Then
                MsgBox("Kode Barang Tidak Ada...!")
            Else
                TextBox1.Text = RD.Item("KodeBarang")
                LBLNamaBarang.Text = RD.Item("NamaBarang")
                LBLHarga.Text = RD.Item("HargaBarang")
                TextBox2.Enabled = True
                TextBox2.Focus()
            End If
        End If
    End Sub

    Private Sub TxtBayar_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtBayar.KeyPress
        If e.KeyChar = Chr(13) Then
            If Val(TxtBayar.Text) < Label9.Text Then
                MsgBox("Duetmu Kurang...!")
                TxtBayar.Text = ""
                TxtKembali.Text = ""
            Else
                TxtKembali.Text = Val(TxtBayar.Text) - Label9.Text
                TxtBayar.Text = Format(Val(TxtBayar.Text), "Rp, ###,###")
                TxtKembali.Text = Format(Val(TxtKembali.Text), "Rp, ###,###")
            End If
        End If
    End Sub

    
    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            Button1.Focus()
        End If
    End Sub

End Class