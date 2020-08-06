Imports System.Data
Imports System.Data.SqlClient
Imports System.Speech
Imports System.Speech.Synthesis
Imports System.Speech.Recognition
Imports System.Threading
Imports System.Globalization

Public Class customer
    Dim con As SqlConnection
    Dim mycom As SqlCommand
    Dim dt As New DataTable
    Dim maxid As Object
    Dim strid As String
    Dim intid As Integer
    Dim a As String
    Dim p As New SpeechSynthesizer
    Dim dr As SqlDataReader

    Dim gram As Grammar
    Public Event SpeechRecognized As  _
     EventHandler(Of SpeechRecognizedEventArgs)
    Public Event SpeechRecognitionRejected As  _
     EventHandler(Of SpeechRecognitionRejectedEventArgs)
    Dim recog As New SpeechRecognitionEngine

    Dim wordlist As String() = New String() {"Hy", "Who are you", "new-record", "save-record", "go-to-menu", "log-out", "close"}

    Dim speechon As Boolean = False


    Public Sub customer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label10.Text = DateTime.Now.ToString("hh:mm dddd,dd MMMM yyyy")
        Button1.TabIndex = 0
        Dim words As New Choices(wordlist)
        gram = New Grammar(New GrammarBuilder(words))
        recog.LoadGrammar(gram)
        AddHandler recog.SpeechRecognitionRejected, AddressOf recognizer_SpeechRecognitionRejected
        AddHandler recog.SpeechRecognized, AddressOf recognizer_SpeechRecognized
        recog.SetInputToDefaultAudioDevice()

        recog.RecognizeAsync(RecognizeMode.Multiple)
        speechon = True


    End Sub

   
    

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If speechon = True Then
            recog.RecognizeAsyncStop()
        End If
        Try
            If RadioButton1.Checked Then
                a = RadioButton1.Text.ToString
            ElseIf RadioButton2.Checked Then
                a = RadioButton2.Text.ToString
            Else
                MsgBox("Choose Correct Gender")
            End If
            If TextBox4.Text = "" Or TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
                MsgBox("Fields not Filled.")
            Else
                con = New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=F:\COLD STORAGE\COLD STORAGE\customer.mdf;Integrated Security=True;User Instance=True")
                con.Open()
                mycom = New SqlCommand("insert into customerTable([Id],[Name],[Address],[PhNo],[Gender]) values ('" & TextBox4.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & a & "')", con)
                mycom.ExecuteNonQuery()
                MsgBox("New Record Inserted.")
                con.Dispose()
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
                TextBox4.Clear()
            End If
        Catch ex As Exception
        End Try
        
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If speechon = True Then
            recog.RecognizeAsyncStop()
        End If

        Dim ob As New Menu
        ob.Show()
        Me.Dispose()
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged

    End Sub
    Private Sub MYID()
        Try
            con = New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=F:\COLD STORAGE\COLD STORAGE\customer.mdf;Integrated Security=True;User Instance=True")
            con.Open()
            Dim sql As String = "select Max(Id) from customerTable"
            mycom = New SqlCommand(sql, con)
            dr = mycom.ExecuteReader
            If dr.Read() Then
                If dr.IsDBNull(0) Then
                    Dim number As Integer = 1
                    Dim emplang As String = "UID"
                    Dim append As String = emplang + number.ToString().PadLeft(3, "00")
                    TextBox4.Text = append
                Else
                    Dim empString As String = dr(0).ToString.Substring(0, 3)
                    Dim empID As Integer = dr(0).ToString().Substring(3) + 1
                    Dim append As String = empString + empID.ToString().PadLeft(3, "00")
                    TextBox4.Text = append.ToString
                End If
            End If
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub
    Private Sub Button5_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        MYID()
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If speechon = True Then
            recog.RecognizeAsyncStop()
        End If

        Dim a As New Login
        a.Show()
        Me.Hide()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        If speechon = True Then
            recog.RecognizeAsyncStop()
        End If

        Dim a As New Menu
        a.Show()
        Me.Hide()
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.KeyChar = ""
        End If
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub recognizer_SpeechRecognized(ByVal sender As Object, ByVal e As SpeechRecognizedEventArgs)
        If (e.Result.Text = "Hy") Then
            p.SpeakAsync("Hello Sir Welcome to the Software")
        ElseIf (e.Result.Text = "Who are you") Then
            p.SpeakAsync("I'm Anna ,a virtual assitant created for your support")
        ElseIf (e.Result.Text = "new-record") Then
            Button5_Click_1(sender, e)
        ElseIf (e.Result.Text = "save-record") Then
            Button1_Click(sender, e)
        ElseIf (e.Result.Text = "close") Then
            Button2_Click(sender, e)
        ElseIf (e.Result.Text = "log-out") Then
            Button3_Click(sender, e)
        ElseIf (e.Result.Text = "go-to-menu") Then
            Button6_Click(sender, e)
        End If
    End Sub


    Private Sub recognizer_SpeechRecognitionRejected(ByVal sender As Object, ByVal e As SpeechRecognitionRejectedEventArgs)
        Try

        Catch ex As Exception

        End Try
    End Sub


End Class