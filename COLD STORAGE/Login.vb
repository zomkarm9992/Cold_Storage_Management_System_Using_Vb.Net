Imports System.Data
Imports System.Data.SqlClient
Imports System.Speech
Imports System.Speech.Synthesis
Imports System.Speech.Recognition
Imports System.Threading
Imports System.Globalization
Public Class Login
    Dim con As SqlConnection
    Dim mycom As SqlCommand


    Dim gram As Grammar
    Public Event SpeechRecognized As  _
     EventHandler(Of SpeechRecognizedEventArgs)
    Public Event SpeechRecognitionRejected As  _
     EventHandler(Of SpeechRecognitionRejectedEventArgs)
    Dim recog As New SpeechRecognitionEngine

    Dim wordlist As String() = New String() {"Hy", "Enter", "Who are you", "login", "close", "forget"}

    Dim p As New SpeechSynthesizer
    Dim speechon As Boolean

    Private Sub Login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        TextBox1.Focus()
        TextBox2.UseSystemPasswordChar = True
        Dim words As New Choices(wordlist)
        gram = New Grammar(New GrammarBuilder(words))
        recog.LoadGrammar(gram)
        AddHandler recog.SpeechRecognitionRejected, AddressOf recognizer_SpeechRecognitionRejected
        AddHandler recog.SpeechRecognized, AddressOf recognizer_SpeechRecognized
        recog.SetInputToDefaultAudioDevice()

        recog.RecognizeAsync(RecognizeMode.Multiple)
        speechon = True
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim menu As New Loading
        Try
            con = New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=F:\COLD STORAGE\COLD STORAGE\customer.mdf;Integrated Security=True;User Instance=True")
            con.Open()
            mycom = New SqlCommand("SELECT * FROM RegTable WHERE (Username='" & TextBox1.Text & "') And (Password='" & TextBox2.Text & "')", con)
            Dim sdr As SqlDataReader = mycom.ExecuteReader()
            If (sdr.Read() = True) Then
                If speechon = True Then
                    recog.RecognizeAsyncStop()
                End If
                MessageBox.Show("Access Granted!")
                menu.Show()
                Me.Hide()
            Else
                MessageBox.Show("Invalid Username or password!")
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox1.Focus()
            End If
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Dim answer As Integer
        answer = MsgBox("Are you Sure you want to exit?", vbQuestion + vbYesNo, "Warning")
        If answer = vbYes Then
            If speechon = True Then
                recog.RecognizeAsyncStop()
            End If
            Me.Hide()
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ProgressBar1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If speechon = True Then
            recog.RecognizeAsyncStop()
        End If
        Dim a As New forget
        a.Show()
    End Sub

    Private Sub recognizer_SpeechRecognized(ByVal sender As Object, ByVal e As SpeechRecognizedEventArgs)
        If (e.Result.Text = "Hy") Then
            p.SpeakAsync("Hello Sir Welcome to the Software")
        ElseIf (e.Result.Text = "Who are you") Then
            p.SpeakAsync("I'm Anna ,a virtual assitant created for your support")
        ElseIf (e.Result.Text = "Enter") Then
            Button1_Click(sender, e)
        ElseIf (e.Result.Text = "forget") Then
            Button2_Click(sender, e)
        ElseIf (e.Result.Text = "close") Then
            cmdExit_Click(sender, e)
        End If
    End Sub


    Private Sub recognizer_SpeechRecognitionRejected(ByVal sender As Object, ByVal e As SpeechRecognitionRejectedEventArgs)
        Try

        Catch ex As Exception

        End Try
    End Sub





End Class