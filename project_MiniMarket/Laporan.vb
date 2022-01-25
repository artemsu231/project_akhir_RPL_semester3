Public Class Laporan

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        AxCrystalReport1.SelectionFormula = "{TBL_DetailTerima.tglterima} in date ('" & DTP1.Value & "') to date ('" & DTP2.Value & "')"
        AxCrystalReport1.ReportFileName = "DetailTerimaPeriodik.rpt"
        AxCrystalReport1.WindowState = Crystal.WindowStateConstants.crptMaximized
        AxCrystalReport1.RetrieveDataFiles()
        AxCrystalReport1.Action = 1
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        AxCrystalReport2.SelectionFormula = "{TBL_DetailJual.tgljual} in date ('" & DTP1.Value & "') to date ('" & DTP2.Value & "')"
        AxCrystalReport2.ReportFileName = "DetailJualPeriodik.rpt"
        AxCrystalReport2.WindowState = Crystal.WindowStateConstants.crptMaximized
        AxCrystalReport2.RetrieveDataFiles()
        AxCrystalReport2.Action = 1
    End Sub
End Class