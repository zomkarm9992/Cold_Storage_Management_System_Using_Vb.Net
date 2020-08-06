Imports System.Data
Imports System.Data.SqlClient
Public Class Register
    Dim myconnection As SqlConnection
    Dim mycommand As SqlCommand
    Dim dr As SqlDataReader
    Dim dr1 As SqlDataReader
    Dim ra As Integer

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim log As New Login
            myconnection = New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=F:\COLD STORAGE\COLD STORAGE\customer.mdf;Integrated Security=True;User Instance=True")
            myconnection.Open()

            If (TextBox2.Text <> TextBox3.Text) And ((TextBox5.Text.Length < 14) Or (TextBox5.Text.Length > 14)) Then
                MessageBox.Show("Password is Incorrect Or Check your Aadhar Card Number is Invalid")
            Else
                mycommand = New SqlCommand("INSERT INTO RegTable([Username],[Password],[RePassword],[Que1],[Que2]) VALUES ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "')", myconnection)
                mycommand.ExecuteNonQuery()
                MsgBox("New Record Inserted Successfully.")
                myconnection.Close()
                log.Show()
                Me.Hide()
            End If
        Catch ex As Exception
            MsgBox("Check your Details .")
        End Try
        
    End Sub

    Private Sub Register_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox2.UseSystemPasswordChar = True
        TextBox3.UseSystemPasswordChar = True
    End Sub

    Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.KeyChar = ""
        End If
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged

    End Sub
End Class