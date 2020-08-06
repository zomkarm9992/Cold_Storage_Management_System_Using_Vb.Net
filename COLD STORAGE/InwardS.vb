Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Public Class InwardS
    Dim con As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=F:\COLD STORAGE\COLD STORAGE\customer.mdf;Integrated Security=True;User Instance=True")
    Dim mycom1 As New SqlCommand
    Dim da1 As SqlDataAdapter
    Dim ds1 As DataSet
    Dim p(1) As SqlParameter
    

    Private Sub InwardS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim rpdoc As New ReportDocument
        rpdoc.Load("F:\COLD STORAGE\COLD STORAGE\InwardCrystalReport1.rpt")
        con.Open()
        Dim str As String
        str = "SELECT InwDate,Name,Id,InwNo,LocationId,LotNo,BoxQuantity,BoxWeight,Rate,LUcharges FROM inwTable " 'WHERE InwDate BETWEEN '" & TextBox1.Text & "' AND '" & TextBox2.Text & "'
        ds1 = New DataSet
        da1 = New SqlDataAdapter(str, con)
        da1.Fill(ds1, "inwTable")

        rpdoc.SetDataSource(ds1.Tables("inwTable"))
        CrystalReportViewer1.ReportSource = rpdoc
        CrystalReportViewer1.Refresh()
        con.Close()

    End Sub
End Class