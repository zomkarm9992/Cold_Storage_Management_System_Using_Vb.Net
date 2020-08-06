Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Public Class Inward_Report
    Dim con As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=F:\COLD STORAGE\COLD STORAGE\customer.mdf;Integrated Security=True;User Instance=True")
    Dim cmd As SqlCommand
    Dim ds As DataSet
    Dim da As SqlDataAdapter

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim rpdoc As New ReportDocument
        rpdoc.Load("F:\COLD STORAGE\COLD STORAGE\InwardBillReport.rpt")
        con.Open()
        Dim str As String
        str = "select inwTable.Name,inwTable.InwNo,inwTable.InwDate,inwTable.Id,inwTable.LocationId,inwTable.LotNo,inwTable.BoxQuantity,inwTable.BoxWeight,inwTable.Rate,inwTable.LUcharges from inwTable where inwTable.InwNo='" & TextBox1.Text & "'"

        ds = New DataSet
        da = New SqlDataAdapter(str, con)
        da.Fill(ds, "inwTable")

        rpdoc.SetDataSource(ds.Tables("inwTable"))
        CrystalReportViewer1.ReportSource = rpdoc
        CrystalReportViewer1.Refresh()
        con.Close()

    End Sub
End Class