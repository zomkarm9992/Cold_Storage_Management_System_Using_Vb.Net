Imports System.Data
Imports System.Data.SqlClient
Public Class forget
    Dim con As SqlConnection
    Dim mycom As SqlCommand
    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim register As New Register
        con = New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=F:\COLD STORAGE\COLD STORAGE\customer.mdf;Integrated Security=True;User Instance=True")
        con.Open()
        mycom = New SqlCommand("SELECT * FROM RegTable WHERE (Que1='" & TextBox4.Text & "') And (Que2='" & TextBox5.Text & "')", con)
        Dim sdr As SqlDataReader = mycom.ExecuteReader()
        If (sdr.Read() = True And TextBox5.Text.Length = 14) Then
            con.Close()
            MessageBox.Show("Acess Granted.....")
            con = New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=F:\COLD STORAGE\COLD STORAGE\customer.mdf;Integrated Security=True;User Instance=True")
            con.Open()
            mycom = New SqlCommand("TRUNCATE TABLE RegTable", con)
            mycom.ExecuteNonQuery()
            con.Close()
            register.Show()
            Me.Hide()
        Else
            MessageBox.Show("Enter Correct Hobby or Aadhar card Number!")
            TextBox4.Focus()
        End If

    End Sub

    Private Sub forget_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.KeyChar = ""
        End If
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged

    End Sub
End Class