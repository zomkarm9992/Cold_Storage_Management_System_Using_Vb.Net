Imports System.Speech
Imports System.Speech.Synthesis
Imports System.Speech.Recognition
Imports System.Threading
Imports System.Globalization
Public Class Startup



    Dim gram As Grammar
    Public Event SpeechRecognized As  _
     EventHandler(Of SpeechRecognizedEventArgs)
    Public Event SpeechRecognitionRejected As  _
     EventHandler(Of SpeechRecognitionRejectedEventArgs)
    Dim recog As New SpeechRecognitionEngine

    Dim wordlist As String() = New String() {"Hy", "Enter", "Who are you"}



    Dim p As New SpeechSynthesizer()
    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Startup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
      
        Dim words As New Choices(wordlist)
        gram = New Grammar(New GrammarBuilder(words))
        recog.LoadGrammar(gram)
        AddHandler recog.SpeechRecognitionRejected, AddressOf recognizer_SpeechRecognitionRejected
        AddHandler recog.SpeechRecognized, AddressOf recognizer_SpeechRecognized
        recog.SetInputToDefaultAudioDevice()

        recog.RecognizeAsync(RecognizeMode.Multiple)




    End Sub
    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        recog.RecognizeAsyncStop()
        Dim go As New Login
        go.Show()
        Me.Hide()
    End Sub



    Private Sub recognizer_SpeechRecognized(ByVal sender As Object, ByVal e As SpeechRecognizedEventArgs)
        If (e.Result.Text = "Hy") Then
            p.SpeakAsync("Hello Sir Welcome to the Software")
        ElseIf (e.Result.Text = "Who are you") Then
            p.SpeakAsync("I'm Anna ,a virtual assitant created for your support")
        ElseIf (e.Result.Text = "Enter") Then
            Button1_Click_1(sender, e)
            
        End If
    End Sub


    Private Sub recognizer_SpeechRecognitionRejected(ByVal sender As Object, ByVal e As SpeechRecognitionRejectedEventArgs)
        Try

        Catch ex As Exception

        End Try
    End Sub



End Class