Imports System.Data.OleDb
Public Class MasterBarang
    Sub KondisiAwal()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        Label6.Text = ""
        Button1.Text = "Input"
        Button2.Text = "Edit"
        Button3.Text = "Hapus"
        Button4.Text = "Tutup"
        ComboBox1.Text = "Silahkan Pilih"
        ComboBox2.Text = "Silahkan Pilih"
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
        AturGrid()
        MunculGrid()
        MunculKodeKategori()
        MunculSatuan()
        NomorOtomatis()
    End Sub

    Sub MunculGrid()
        Call Koneksi()
        DA = New OleDbDataAdapter("Select * From TBL_Barang", CONN)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "TBL_Barang")
        DataGridView1.DataSource = (DS.Tables("TBL_Barang"))
    End Sub

    Sub AturGrid()
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.RowsDefaultCellStyle.BackColor = Color.Cyan
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
    End Sub

    Private Sub MasterBarang_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call KondisiAwal()
    End Sub


    Sub MunculKodeKategori()
        Call Koneksi()
        CMD = New OleDbCommand("Select * From TBL_Kategori", CONN)
        RD = CMD.ExecuteReader
        ComboBox1.Items.Clear()
        Do While RD.Read
            ComboBox1.Items.Add(RD.Item(0))
        Loop
    End Sub

    Sub MunculSatuan()
        Call Koneksi()
        CMD = New OleDbCommand("Select Distinct SatuanBarang From TBL_Barang", CONN)
        ComboBox2.Items.Clear()
        RD = CMD.ExecuteReader
        Do While RD.Read
            ComboBox2.Items.Add(RD.Item("SatuanBarang"))
        Loop
    End Sub


    Sub NomorOtomatis()
        Call Koneksi()
        'CMD = New OleDbCommand("Select * From TBL_Barang Where KodeBarang in (Select max(KodeBarang) From TBL_Barang)", CONN)
        'Dim UrutKode As String
        'Dim Hitung As Long
        'RD = CMD.ExecuteReader
        'RD.Read()
        'If Not RD.HasRows Then
        '    UrutKode = "BRG" + Format(Now, "yyMMdd") + "001"
        'Else
        '    Hitung = Microsoft.VisualBasic.Right(RD.GetString(0), 9) + 1
        '    UrutKode = "BRG" + Format(Now, "yyMMdd") + Microsoft.VisualBasic.Right("000" & Hitung, 3)
        'End If
        'TextBox1.Text = UrutKode
        CMD = New OleDbCommand("Select * From TBL_Barang order by KodeBarang desc", CONN)
        RD = CMD.ExecuteReader
        RD.Read()
        If Not RD.HasRows Then
            TextBox1.Text = "BRG" + "001"
        Else
            TextBox1.Text = Val(Microsoft.VisualBasic.Mid(RD.Item("KodeBarang").ToString, 4, 3)) + 1
            If Len(TextBox1.Text) = 1 Then
                TextBox1.Text = "BRG00" & TextBox1.Text & ""
            ElseIf Len(TextBox1.Text) = 2 Then
                TextBox1.Text = "BRG0" & TextBox1.Text & ""
            End If
        End If

    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Button1.Text = "Input" Then
            Button1.Text = "Simpan"
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Text = "Batal"
            TextBox1.Focus()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or ComboBox1.Text = "" Or ComboBox2.Text = "" Then
                MsgBox("Pastikan Data Diisi Lengkap...!")
            Else
                Call Koneksi()
                Dim SimpanData As String = "insert into TBL_Barang values ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & ComboBox1.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & ComboBox2.Text & "')"
                CMD = New OleDbCommand(SimpanData, CONN)
                CMD.ExecuteNonQuery()
                MsgBox("Data Berhasil Di Tambahkan")
                Call KondisiAwal()
            End If
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Call Koneksi()
        CMD = New OleDbCommand("Select * From TBL_Kategori Where KodeKategori ='" & ComboBox1.Text & "'", CONN)
            RD = CMD.ExecuteReader
            RD.Read()
        If RD.HasRows Then
            Label6.Text = RD.Item("NamaKategori")
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If Button4.Text = "Batal" Then
            Call KondisiAwal()
        Else
            Me.Close()
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Button2.Text = "Edit" Then
            Button2.Text = "Simpan"
            Button1.Enabled = False
            Button3.Enabled = False
            Button4.Text = "Batal"
            TextBox1.Focus()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or ComboBox1.Text = "" Or ComboBox2.Text = "" Then
                MsgBox("Pastikan Data Diisi Lengkap...!")
            Else
                Call Koneksi()
                Dim EditData As String = "Update TBL_BARANG set NamaBarang='" & TextBox2.Text & "',KodeKategori='" & ComboBox1.Text & "',HargaBarang='" & TextBox3.Text & "',JumlahBarang='" & TextBox4.Text & "',SatuanBarang='" & ComboBox2.Text & "' where KodeBarang='" & TextBox1.Text & "'"
                CMD = New OleDbCommand(EditData, CONN)
                CMD.ExecuteNonQuery()
                MsgBox("Data Berhasil Di Update")
                Call KondisiAwal()
            End If
        End If
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call Koneksi()
            CMD = New OleDbCommand("Select * From TBL_Barang Where KodeBarang ='" & TextBox1.Text & "'", CONN)
            RD = CMD.ExecuteReader
            RD.Read()
            If RD.HasRows Then
                TextBox2.Text = RD.Item("NamaBarang")
                TextBox3.Text = RD.Item("HargaBarang")
                TextBox4.Text = RD.Item("JumlahBarang")
                ComboBox2.Text = RD.Item("SatuanBarang")
                ComboBox1.Text = RD.Item("KodeKategori")
            Else
                MsgBox("Data Tidak Ada ...!")
            End If
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If Button3.Text = "Hapus" Then
            Button3.Text = "Hapus Data"
            Button1.Enabled = False
            Button2.Enabled = False
            Button4.Text = "Batal"
            TextBox1.Focus()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or ComboBox1.Text = "" Or ComboBox2.Text = "" Then
                MsgBox("Pastikan Data Diisi Lengkap...!")
            Else
                Call Koneksi()
                Dim EditData As String = "Delete From TBL_Barang where KodeBarang='" & TextBox1.Text & "'"
                CMD = New OleDbCommand(EditData, CONN)
                CMD.ExecuteNonQuery()
                MsgBox("Data Berhasil Di Hapus")
                Call KondisiAwal()
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        TextBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        TextBox2.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        ComboBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        TextBox3.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        TextBox4.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
        ComboBox2.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value
    End Sub
End Class