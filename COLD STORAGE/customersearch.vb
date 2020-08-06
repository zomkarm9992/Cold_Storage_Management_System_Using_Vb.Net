Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Public Class customersearch
    Dim con As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=F:\COLD STORAGE\COLD STORAGE\customer.mdf;Integrated Security=True;User Instance=True")
    Dim mycom1 As SqlCommand
    Dim da1 As SqlDataAdapter
    Dim ds1 As DataSet

    Private Sub customersearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim rpdoc As New ReportDocument
        rpdoc.Load("F:\COLD STORAGE\COLD STORAGE\CustomerCrystalReport1.rpt")
        con.open()
        Dim str As String
        str = "select * from customerTable"

        ds1 = New DataSet
        da1 = New SqlDataAdapter(str, con)
        da1.Fill(ds1, "customerTable")
        rpdoc.SetDataSource(ds1.Tables("customerTable"))
        CrystalReportViewer1.ReportSource = rpdoc
        CrystalReportViewer1.Refresh()

        con.close()


    End Sub

    Private Sub CrystalReportViewer1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CrystalReportViewer1.Load

    End Sub
End Class