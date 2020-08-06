Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Public Class BillReport
    Dim con As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=F:\COLD STORAGE\COLD STORAGE\customer.mdf;Integrated Security=True;User Instance=True")
    Dim cmd As SqlCommand
    Dim ds As New DataSet
    Dim da As SqlDataAdapter
    Private Sub BillReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim rpdoc As New Bill_Report
        Dim query As String = "SELECT BillMaster.PartyName,BillMaster.BillNo,BillMaster.BillDate,outwardTable.OutNo,outwardTable.OutDate,outwardTable.LotNoo,outwardTable.BoxQuant,outwardTable.BoxWeigh,BillMaster.Months,BillMaster.Total FROM BillMaster LEFT JOIN outwardTable ON (BillMaster.Id=outwardTable.Id) WHERE BillMaster.Id='" & TextBox1.Text & "'"
        con.Open()
        cmd = New SqlCommand()
        cmd.CommandText = query
        cmd.Connection = con
        da = New SqlDataAdapter(query, con)
        da.Fill(ds)
        rpdoc.Database.Tables("BillMaster").SetDataSource(ds.Tables(0))
        rpdoc.Database.Tables("outwardTable").SetDataSource(ds.Tables(0))
        CrystalReportViewer1.ReportSource = rpdoc
        CrystalReportViewer1.Refresh()
        con.Close()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class