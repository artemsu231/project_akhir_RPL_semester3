Imports System.Data.OleDb
Public Class MasterSupplier
    Sub KondisiAwal()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        Button1.Text = "Input"
        Button2.Text = "Edit"
        Button3.Text = "Hapus"
        Button4.Text = "Tutup"
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
        AturGrid()
        MunculGrid()
    End Sub

    Sub MunculGrid()
        Call Koneksi()
        DA = New OleDbDataAdapter("Select * From TBL_Supplier", CONN)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "TBL_Supplier")
        DataGridView1.DataSource = (DS.Tables("TBL_Supplier"))
    End Sub

    Sub AturGrid()
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.RowsDefaultCellStyle.BackColor = Color.Cyan
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
    End Sub

    Private Sub MasterSupplier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call KondisiAwal()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Button1.Text = "Input" Then
            Button1.Text = "Simpan"
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Text = "Batal"
            TextBox1.Focus()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
                MsgBox("Pastikan Data Diisi Lengkap...!")
            Else
                Call Koneksi()
                Dim SimpanData As String = "insert into TBL_Supplier values ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "')"
                CMD = New OleDbCommand(SimpanData, CONN)
                CMD.ExecuteNonQuery()
                MsgBox("Data Berhasil Di Tambahkan")
                Call KondisiAwal()
            End If
        End If
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call Koneksi()
            CMD = New OleDbCommand("Select * From TBL_Supplier Where KodeSupplier ='" & TextBox1.Text & "'", CONN)
            RD = CMD.ExecuteReader
            RD.Read()
            If RD.HasRows Then
                TextBox2.Text = RD.Item("NamaSupplier")
                TextBox3.Text = RD.Item("AlamatSupplier")
                TextBox4.Text = RD.Item("TelpSupplier")
            Else
                MsgBox("Data Tidak Ada ...!")
            End If
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
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
                MsgBox("Pastikan Data Diisi Lengkap...!")
            Else
                Call Koneksi()
                Dim EditData As String = "Update TBL_SUPPLIER set NamaSupplier='" & TextBox2.Text & "',AlamatSupplier='" & TextBox3.Text & "',TelpSupplier='" & TextBox4.Text & "' where KodeSupplier='" & TextBox1.Text & "'"
                CMD = New OleDbCommand(EditData, CONN)
                CMD.ExecuteNonQuery()
                MsgBox("Data Berhasil Di Update")
                Call KondisiAwal()
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
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
                MsgBox("Pastikan Data Diisi Lengkap...!")
            Else
                Call Koneksi()
                Dim EditData As String = "Delete From TBL_SUPPLIER where KodeSupplier='" & TextBox1.Text & "'"
                CMD = New OleDbCommand(EditData, CONN)
                CMD.ExecuteNonQuery()
                MsgBox("Data Berhasil Di Hapus")
                Call KondisiAwal()
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        TextBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        TextBox2.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        TextBox3.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        TextBox4.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
    End Sub
End Class