Imports System.Data.OleDb
Public Class Login
    Sub Terbuka()
        MenuUtama.LoginToolStripMenuItem.Enabled = False
        MenuUtama.LogoutToolStripMenuItem.Enabled = True
        MenuUtama.MasterToolStripMenuItem.Enabled = True
        MenuUtama.TransaksiToolStripMenuItem.Enabled = True
        MenuUtama.LaporanToolStripMenuItem.Enabled = True
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Data Belum Lengkap, Silahkan Masukan Data Login dan Password...!")
        Else
            Call Koneksi()
            CMD = New OleDbCommand("Select * From TBL_ADMIN where KodeAdmin ='" & TextBox1.Text & "' and PasswordAdmin ='" & TextBox2.Text & "'", CONN)
            RD = CMD.ExecuteReader
            RD.Read()
            If RD.HasRows Then
                Me.Close()
                Call Terbuka()
                MenuUtama.STLabel2.Text = RD!KodeAdmin
                MenuUtama.STLabel4.Text = RD!NamaAdmin
                MenuUtama.STLabel6.Text = RD!LevelAdmin
                If MenuUtama.STLabel6.Text = "USER" Then
                    MenuUtama.AdminToolStripMenuItem.Visible = False
                Else
                    MenuUtama.AdminToolStripMenuItem.Visible = True
                    MenuUtama.ResetToolStripMenuItem.Visible = True
                End If
            Else
                TextBox1.Focus()
                MsgBox("KodeAdmin atau Password Salah...!")
            End If
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox1.Focus()
    End Sub


    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            If TextBox2.Text = "" Then
                MsgBox("Password Belum Diisi ...!")
            Else
                Button1.Focus()
            End If
        End If
    End Sub

    Private Sub TextBox1_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then

            If TextBox1.Text = "" Then
                MsgBox("Isikan Dengan Benar ...!")
            Else
                TextBox2.Focus()
            End If
        End If
    End Sub
End Class