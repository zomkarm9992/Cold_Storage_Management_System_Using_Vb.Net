Imports System.Data
Imports System.Data.SqlClient
Imports System.Speech
Imports System.Speech.Synthesis
Imports System.Speech.Recognition
Imports System.Threading
Imports System.Globalization
Public Class GmailLogin

    Dim gram As Grammar
    Public Event SpeechRecognized As  _
     EventHandler(Of SpeechRecognizedEventArgs)
    Public Event SpeechRecognitionRejected As  _
     EventHandler(Of SpeechRecognitionRejectedEventArgs)
    Dim recog As New SpeechRecognitionEngine

    Dim wordlist As String() = New String() {"Hy", "Who are you", "proceed", "close"}

    Dim p As New SpeechSynthesizer
    Dim speechon As Boolean = False


    Private Sub GmailLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim words As New Choices(wordlist)
        gram = New Grammar(New GrammarBuilder(words))
        recog.LoadGrammar(gram)
        AddHandler recog.SpeechRecognitionRejected, AddressOf recognizer_SpeechRecognitionRejected
        AddHandler recog.SpeechRecognized, AddressOf recognizer_SpeechRecognized
        recog.SetInputToDefaultAudioDevice()

        recog.RecognizeAsync(RecognizeMode.Multiple)
        speechon = True

    End Sub

    Private Sub login_gmail()
        WebBrowser1.Navigate("https:\\accounts.google.com/ServiceLogin?service=mail&continue=https://mail.google.com/mail/")
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Label3.Visible = True
        WebBrowser1.Navigate("https://accounts.google.com/ServiceLogin?service=mail&continue=https://mail.google.com/mail/")
    End Sub

    Private Sub WebBrowser1_ProgressChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserProgressChangedEventArgs) Handles WebBrowser1.ProgressChanged
        Try
            ProgressBar1.Maximum = e.MaximumProgress
            ProgressBar1.Value = e.CurrentProgress
            Label3.Text = "Loading"
            If ProgressBar1.Value = ProgressBar1.Maximum Then
                Label3.Text = "Done"
                ProgressBar1.Value = ProgressBar1.Maximum
            End If
        Catch ex As Exception
            Label3.Text = "Error Loading"
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        login_gmail()
        Button1.Visible = False
    End Sub

    Private Sub recognizer_SpeechRecognized(ByVal sender As Object, ByVal e As SpeechRecognizedEventArgs)
        If (e.Result.Text = "Hy") Then
            p.SpeakAsync("Hello Sir Welcome to the Software")
        ElseIf (e.Result.Text = "Who are you") Then
            p.SpeakAsync("I'm Anna ,a virtual assitant created for your support")
        ElseIf (e.Result.Text = "close") Then
            If speechon = True Then
                recog.RecognizeAsyncStop()
                Dim a As New Menu
                a.Show()
            End If
            Me.Close()
        ElseIf (e.Result.Text = "proceed") Then
            Button1_Click(sender, e)
        End If
    End Sub


    Private Sub recognizer_SpeechRecognitionRejected(ByVal sender As Object, ByVal e As SpeechRecognitionRejectedEventArgs)
        Try

        Catch ex As Exception

        End Try
    End Sub

End Class