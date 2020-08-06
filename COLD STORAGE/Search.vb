Imports System.Speech
Imports System.Speech.Synthesis
Imports System.Speech.Recognition
Imports System.Threading
Imports System.Globalization
Public Class Search
    Dim p As New SpeechSynthesizer
    Dim gram As Grammar
    Public Event SpeechRecognized As  _
     EventHandler(Of SpeechRecognizedEventArgs)
    Public Event SpeechRecognitionRejected As  _
     EventHandler(Of SpeechRecognitionRejectedEventArgs)
    Dim recog As New SpeechRecognitionEngine

    Dim wordlist As String() = New String() {"Hy", "Who are you", "customer-report", "in-word-report", "out-word-report", "go-to-menu", "log-out", "close"}

    Dim speechon As Boolean = False

    Private Sub Search_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

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

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim men As New Menu
        men.Show()
        Me.Hide()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim log As New Login
        log.Show()
        Me.Hide()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim a As New InwardS
        a.Show()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim c As New customersearch
        c.Show()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim a As New OutwardReport
        a.Show()
    End Sub
    Private Sub recognizer_SpeechRecognized(ByVal sender As Object, ByVal e As SpeechRecognizedEventArgs)
        If (e.Result.Text = "Hy") Then
            p.SpeakAsync("Hello Sir Welcome to the Software")
        ElseIf (e.Result.Text = "Who are you") Then
            p.SpeakAsync("I'm Anna ,a virtual assitant created for your support")
        ElseIf (e.Result.Text = "customer-report") Then
            Button2_Click(sender, e)
        ElseIf (e.Result.Text = "in-word-report") Then
            Button3_Click(sender, e)
        ElseIf (e.Result.Text = "out-word-report") Then
            Button6_Click(sender, e)
        ElseIf (e.Result.Text = "log-out") Then
            Button5_Click(sender, e)
        ElseIf (e.Result.Text = "go-to-menu") Then
            Button4_Click(sender, e)
        End If
    End Sub


    Private Sub recognizer_SpeechRecognitionRejected(ByVal sender As Object, ByVal e As SpeechRecognitionRejectedEventArgs)
        Try

        Catch ex As Exception

        End Try
    End Sub

End Class