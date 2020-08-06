Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Public Class OutwardReport
    Dim con As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=F:\COLD STORAGE\COLD STORAGE\customer.mdf;Integrated Security=True;User Instance=True")
    Dim mycom1 As SqlCommand
    Dim da1 As SqlDataAdapter
    Dim ds1 As DataSet
    Private Sub OutwardReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim rpdoc As New ReportDocument
        ' Dim dfrom As DateTime = DateTimePicker1.Value
        ' Dim dto As DateTime = DateTimePicker2.Value
        rpdoc.Load("F:\COLD STORAGE\COLD STORAGE\OutwardCrystalReport1.rpt")
        con.Open()
        Dim str As String
        str = "select OutDate,Id,Name,OutNo,LotNoo,BoxQuant,BoxWeigh from outwardTable " 'where OutDate between '" & DateTimePicker1.Value & "' and '" & DateTimePicker2.Value & "' 
        ds1 = New DataSet
        mycom1 = New SqlCommand(str, con)
        da1 = New SqlDataAdapter(mycom1)
        da1.Fill(ds1, "outwardTable")
        rpdoc.SetDataSource(ds1.Tables("outwardTable"))
        CrystalReportViewer1.ReportSource = rpdoc
        CrystalReportViewer1.Refresh()
        con.Close()
    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class