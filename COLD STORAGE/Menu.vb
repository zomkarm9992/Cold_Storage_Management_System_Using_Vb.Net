Imports System.Data
Imports System.Data.SqlClient
Imports System.Speech
Imports System.Speech.Synthesis
Imports System.Speech.Recognition
Imports System.Threading
Imports System.Globalization
Public Class Menu
    Dim con As SqlConnection
    Dim mycom As SqlCommand

    Dim gram As Grammar
    Public Event SpeechRecognized As  _
     EventHandler(Of SpeechRecognizedEventArgs)
    Public Event SpeechRecognitionRejected As  _
     EventHandler(Of SpeechRecognitionRejectedEventArgs)
    Dim recog As New SpeechRecognitionEngine

    Dim wordlist As String() = New String() {"Hy", "Who are you", "customer", "close", "in-word", "out-word", "billmaster", "crystalreport", "G-mail-login", "voicehelp", "in-form"}

    Dim p As New SpeechSynthesizer
    Dim speechon As Boolean





    Private Sub btnctmr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnctmr.Click
        If speechon = True Then
            recog.RecognizeAsyncStop()
        End If
        Dim customer As New customer
        customer.Show()
        Me.Hide()
    End Sub

    Private Sub btninword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btninword.Click
        If speechon = True Then
            recog.RecognizeAsyncStop()
        End If
        Dim Inward As New Inward
        Inward.Show()
        Me.Hide()
    End Sub

    Private Sub btnoutword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnoutword.Click
        If speechon = True Then
            recog.RecognizeAsyncStop()
        End If
        Dim Outward As New Outward
        Outward.Show()
        Me.Hide()
    End Sub

    Private Sub btnbill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbill.Click
        If speechon = True Then
            recog.RecognizeAsyncStop()
        End If
        Dim BillMaster As New BillMaster
        BillMaster.Show()
        Me.Hide()
    End Sub

    Private Sub Menu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Timer1.Start()
        Me.ToolStripStatusLabel1.Text = DateAndTime.Now

        Dim words As New Choices(wordlist)
        gram = New Grammar(New GrammarBuilder(words))
        recog.LoadGrammar(gram)
        AddHandler recog.SpeechRecognitionRejected, AddressOf recognizer_SpeechRecognitionRejected
        AddHandler recog.SpeechRecognized, AddressOf recognizer_SpeechRecognized
        recog.SetInputToDefaultAudioDevice()
        recog.RecognizeAsync(RecognizeMode.Multiple)
        speechon = True

  

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If speechon = True Then
            recog.RecognizeAsyncStop()
        End If
        Dim Login As New Login
        Login.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If speechon = True Then
            recog.RecognizeAsyncStop()
        End If
        Dim ob As New Search
        ob.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim a As New About
        a.Show()
        recog.RecognizeAsyncStop()
        Me.Hide()

    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint
        btnctmr.Focus()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If speechon = True Then
            recog.RecognizeAsyncStop()
        End If
        voicedemo.Show()
        Me.Hide()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click

        Dim a As New GmailLogin
        a.Show()
        recog.RecognizeAsyncStop()
        Me.Hide()


    End Sub


    Private Sub recognizer_SpeechRecognized(ByVal sender As Object, ByVal e As SpeechRecognizedEventArgs)
       
        If (e.Result.Text = "Hy") Then
            p.SpeakAsync("Hello Sir Welcome to the Software")

        ElseIf (e.Result.Text = "Who are you") Then
            p.SpeakAsync("I'm Anna ,a virtual assitant created for your support")

        ElseIf (e.Result.Text = "customer") Then
            btnctmr_Click(sender, e)

        ElseIf (e.Result.Text = "in-word") Then
            btninword_Click(sender, e)

        ElseIf (e.Result.Text = "out-word") Then
            btnoutword_Click(sender, e)

        ElseIf (e.Result.Text = "billmaster") Then
            btnbill_Click(sender, e)

        ElseIf (e.Result.Text = "crystalreport") Then
            Button1_Click_1(sender, e)

        ElseIf (e.Result.Text = "in-form") Then
            Button2_Click(sender, e)

        ElseIf (e.Result.Text = "voicehelp") Then
            Button4_Click(sender, e)

        ElseIf (e.Result.Text = "G-mail-login") Then
            Button5_Click(sender, e)

        ElseIf (e.Result.Text = "close") Then
            Button3_Click(sender, e)

        End If

    End Sub


    Private Sub recognizer_SpeechRecognitionRejected(ByVal sender As Object, ByVal e As SpeechRecognitionRejectedEventArgs)
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class
