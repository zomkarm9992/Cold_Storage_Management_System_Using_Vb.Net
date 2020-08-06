Imports System.Data
Imports System.Data.SqlClient
Imports System.Speech
Imports System.Speech.Synthesis
Imports System.Speech.Recognition
Imports System.Threading
Imports System.Globalization
Public Class Outward
    Dim con As SqlConnection
    Dim mycom As SqlCommand
    Dim p As New SpeechSynthesizer
    Dim mycom1 As SqlCommand
    Dim dr As SqlDataReader
    Dim dt As DataTable
    Dim da As SqlDataAdapter
    Dim gram As Grammar
    Public Event SpeechRecognized As  _
     EventHandler(Of SpeechRecognizedEventArgs)
    Public Event SpeechRecognitionRejected As  _
     EventHandler(Of SpeechRecognitionRejectedEventArgs)
    Dim recog As New SpeechRecognitionEngine

    Dim wordlist As String() = New String() {"Hy", "Who are you", "save-record", "go-to-menu", "log-out", "clear"}

    Dim speechon As Boolean = False

    Private Sub Outward_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label1.Text = DateTime.Now.ToString("hh:mm dddd,dd MMMM yyyy")
        TextBox10.Visible = False
        TextBox12.Visible = False
        TextBox13.Visible = False

        TextBox14.Visible = False
        TextBox16.Visible = False
        TextBox17.Visible = False

        TextBox18.Visible = False
        TextBox20.Visible = False
        TextBox21.Visible = False

        TextBox22.Visible = False
        TextBox24.Visible = False
        TextBox25.Visible = False

        TextBox26.Visible = False
        TextBox28.Visible = False
        TextBox29.Visible = False

        TextBox30.Visible = False
        TextBox32.Visible = False
        TextBox33.Visible = False

        TextBox34.Visible = False
        TextBox36.Visible = False
        TextBox37.Visible = False

        Dim words As New Choices(wordlist)
        gram = New Grammar(New GrammarBuilder(words))
        recog.LoadGrammar(gram)
        AddHandler recog.SpeechRecognitionRejected, AddressOf recognizer_SpeechRecognitionRejected
        AddHandler recog.SpeechRecognized, AddressOf recognizer_SpeechRecognized
        recog.SetInputToDefaultAudioDevice()

        recog.RecognizeAsync(RecognizeMode.Multiple)
        speechon = True


    End Sub



    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
   
        Try
            Dim take As String
            take = TextBox9.Text
            If take = 1 Then
                TextBox10.Visible = False
                TextBox12.Visible = False
                TextBox13.Visible = False
            ElseIf take = 2 Then
                TextBox10.Visible = False
                TextBox12.Visible = False
                TextBox13.Visible = False
                TextBox14.Visible = False
                TextBox16.Visible = False
                TextBox17.Visible = False
            ElseIf take = 3 Then
                TextBox10.Visible = False
                TextBox12.Visible = False
                TextBox13.Visible = False
                TextBox14.Visible = False
                TextBox16.Visible = False
                TextBox17.Visible = False
                TextBox18.Visible = False
                TextBox20.Visible = False
                TextBox21.Visible = False
            ElseIf take = 4 Then
                TextBox10.Visible = False
                TextBox12.Visible = False
                TextBox13.Visible = False
                TextBox14.Visible = False
                TextBox16.Visible = False
                TextBox17.Visible = False
                TextBox18.Visible = False
                TextBox20.Visible = False
                TextBox21.Visible = False
                TextBox22.Visible = False
                TextBox24.Visible = False
                TextBox25.Visible = False
            ElseIf take = 5 Then
                TextBox10.Visible = False
                TextBox12.Visible = False
                TextBox13.Visible = False
                TextBox14.Visible = False
                TextBox16.Visible = False
                TextBox17.Visible = False
                TextBox18.Visible = False
                TextBox20.Visible = False
                TextBox21.Visible = False
                TextBox22.Visible = False
                TextBox24.Visible = False
                TextBox25.Visible = False
                TextBox26.Visible = False
                TextBox28.Visible = False
                TextBox29.Visible = False
            ElseIf take = 6 Then
                TextBox10.Visible = False
                TextBox12.Visible = False
                TextBox13.Visible = False
                TextBox14.Visible = False
                TextBox16.Visible = False
                TextBox17.Visible = False
                TextBox18.Visible = False
                TextBox20.Visible = False
                TextBox21.Visible = False
                TextBox22.Visible = False
                TextBox24.Visible = False
                TextBox25.Visible = False
                TextBox26.Visible = False
                TextBox28.Visible = False
                TextBox29.Visible = False
                TextBox30.Visible = False
                TextBox32.Visible = False
                TextBox33.Visible = False
            ElseIf take = 7 Then
                TextBox10.Visible = False
                TextBox12.Visible = False
                TextBox13.Visible = False
                TextBox14.Visible = False
                TextBox16.Visible = False
                TextBox17.Visible = False
                TextBox18.Visible = False
                TextBox20.Visible = False
                TextBox21.Visible = False
                TextBox22.Visible = False
                TextBox24.Visible = False
                TextBox25.Visible = False
                TextBox26.Visible = False
                TextBox28.Visible = False
                TextBox29.Visible = False
                TextBox30.Visible = False
                TextBox32.Visible = False
                TextBox33.Visible = False
                TextBox34.Visible = False
                TextBox36.Visible = False
                TextBox37.Visible = False
            End If
        Catch ex As Exception
        End Try


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
   
        Try
            Dim out As String
            out = TextBox9.Text

            If out = "" Then
                If TextBox38.Text = "" Or TextBox5.Text = "" Or TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
                    MsgBox("Enter All The Fields")
                Else

                    con = New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=F:\COLD STORAGE\COLD STORAGE\customer.mdf;Integrated Security=True;User Instance=True")
                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    MsgBox("New Record Inserted.")
                    con.Close()

                    TextBox38.Clear()
                    TextBox5.Clear()
                    TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox3.Clear()
                    TextBox4.Clear()

                End If




            ElseIf out = 1 Then
                If TextBox38.Text = "" Or TextBox5.Text = "" Or TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox10.Text = "" Or TextBox12.Text = "" Or TextBox13.Text = "" Then
                    MsgBox("Enter All The Fields")
                Else
                    con = New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=F:\COLD STORAGE\COLD STORAGE\customer.mdf;Integrated Security=True;User Instance=True")

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox10.Text & "','" & TextBox12.Text & "','" & TextBox13.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    MsgBox("New Record Inserted.")

                    TextBox38.Clear()
                    TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox3.Clear()
                    TextBox4.Clear()
                    TextBox5.Clear()
                    TextBox10.Clear()
                    TextBox12.Clear()
                    TextBox13.Clear()
                End If

            ElseIf out = 2 Then
                If TextBox38.Text = "" Or TextBox5.Text = "" Or TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox10.Text = "" Or TextBox12.Text = "" Or TextBox13.Text = "" Or TextBox14.Text = "" Or TextBox16.Text = "" Or TextBox17.Text = "" Then
                    MsgBox("Enter All The Fields")
                Else
                    con = New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=F:\COLD STORAGE\COLD STORAGE\customer.mdf;Integrated Security=True;User Instance=True")

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox10.Text & "','" & TextBox12.Text & "','" & TextBox13.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox14.Text & "','" & TextBox16.Text & "','" & TextBox17.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    MsgBox("New Record Inserted.")

                    TextBox38.Clear()
                    TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox3.Clear()
                    TextBox4.Clear()
                    TextBox5.Clear()
                    TextBox10.Clear()
                    TextBox12.Clear()
                    TextBox13.Clear()
                    TextBox14.Clear()
                    TextBox16.Clear()
                    TextBox17.Clear()
                End If

            ElseIf out = 3 Then
                If TextBox38.Text = "" Or TextBox5.Text = "" Or TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox10.Text = "" Or TextBox12.Text = "" Or TextBox13.Text = "" Or TextBox14.Text = "" Or TextBox16.Text = "" Or TextBox17.Text = "" Or TextBox18.Text = "" Or TextBox20.Text = "" Or TextBox21.Text = "" Then
                    MsgBox("Enter All The Fields")
                Else
                    con = New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=F:\COLD STORAGE\COLD STORAGE\customer.mdf;Integrated Security=True;User Instance=True")

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox10.Text & "','" & TextBox12.Text & "','" & TextBox13.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox14.Text & "','" & TextBox16.Text & "','" & TextBox17.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox18.Text & "','" & TextBox20.Text & "','" & TextBox21.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    MsgBox("New Record Inserted.")

                    TextBox38.Clear()
                    TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox3.Clear()
                    TextBox4.Clear()
                    TextBox5.Clear()
                    TextBox10.Clear()
                    TextBox12.Clear()
                    TextBox13.Clear()
                    TextBox14.Clear()
                    TextBox16.Clear()
                    TextBox17.Clear()
                    TextBox18.Clear()
                    TextBox20.Clear()
                    TextBox21.Clear()
                End If

            ElseIf out = 4 Then
                If TextBox38.Text = "" Or TextBox5.Text = "" Or TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox10.Text = "" Or TextBox12.Text = "" Or TextBox13.Text = "" Or TextBox14.Text = "" Or TextBox16.Text = "" Or TextBox17.Text = "" Or TextBox18.Text = "" Or TextBox20.Text = "" Or TextBox21.Text = "" Or TextBox22.Text = "" Or TextBox24.Text = "" Or TextBox25.Text = "" Then
                    MsgBox("Enter All The Fields")
                Else
                    con = New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=F:\COLD STORAGE\COLD STORAGE\customer.mdf;Integrated Security=True;User Instance=True")

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox10.Text & "','" & TextBox12.Text & "','" & TextBox13.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox14.Text & "','" & TextBox16.Text & "','" & TextBox17.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox18.Text & "','" & TextBox20.Text & "','" & TextBox21.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox22.Text & "','" & TextBox24.Text & "','" & TextBox25.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    MsgBox("New Record Inserted.")

                    TextBox38.Clear()
                    TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox3.Clear()
                    TextBox4.Clear()
                    TextBox5.Clear()
                    TextBox10.Clear()
                    TextBox12.Clear()
                    TextBox13.Clear()
                    TextBox14.Clear()
                    TextBox16.Clear()
                    TextBox17.Clear()
                    TextBox18.Clear()
                    TextBox20.Clear()
                    TextBox21.Clear()
                    TextBox22.Clear()
                    TextBox24.Clear()
                    TextBox25.Clear()
                End If

            ElseIf out = 5 Then
                If TextBox38.Text = "" Or TextBox5.Text = "" Or TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox10.Text = "" Or TextBox12.Text = "" Or TextBox13.Text = "" Or TextBox14.Text = "" Or TextBox16.Text = "" Or TextBox17.Text = "" Or TextBox18.Text = "" Or TextBox20.Text = "" Or TextBox21.Text = "" Or TextBox22.Text = "" Or TextBox24.Text = "" Or TextBox25.Text = "" Or TextBox26.Text = "" Or TextBox28.Text = "" Or TextBox29.Text = "" Then
                    MsgBox("Enter All The Fields")
                Else
                    con = New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=F:\COLD STORAGE\COLD STORAGE\customer.mdf;Integrated Security=True;User Instance=True")

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox10.Text & "','" & TextBox12.Text & "','" & TextBox13.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox14.Text & "','" & TextBox16.Text & "','" & TextBox17.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox18.Text & "','" & TextBox20.Text & "','" & TextBox21.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox22.Text & "','" & TextBox24.Text & "','" & TextBox25.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox26.Text & "','" & TextBox28.Text & "','" & TextBox29.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    MsgBox("New Record Inserted.")

                    TextBox38.Clear()
                    TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox3.Clear()
                    TextBox4.Clear()
                    TextBox5.Clear()
                    TextBox10.Clear()
                    TextBox12.Clear()
                    TextBox13.Clear()
                    TextBox14.Clear()
                    TextBox16.Clear()
                    TextBox17.Clear()
                    TextBox18.Clear()
                    TextBox20.Clear()
                    TextBox21.Clear()
                    TextBox22.Clear()
                    TextBox24.Clear()
                    TextBox25.Clear()
                    TextBox26.Clear()
                    TextBox28.Clear()
                    TextBox29.Clear()
                End If

            ElseIf out = 6 Then
                If TextBox38.Text = "" Or TextBox5.Text = "" Or TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox10.Text = "" Or TextBox12.Text = "" Or TextBox13.Text = "" Or TextBox14.Text = "" Or TextBox16.Text = "" Or TextBox17.Text = "" Or TextBox18.Text = "" Or TextBox20.Text = "" Or TextBox21.Text = "" Or TextBox22.Text = "" Or TextBox24.Text = "" Or TextBox25.Text = "" Or TextBox26.Text = "" Or TextBox28.Text = "" Or TextBox29.Text = "" Or TextBox30.Text = "" Or TextBox32.Text = "" Or TextBox33.Text = "" Then
                    MsgBox("Enter All The Fields")
                Else
                    con = New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=F:\COLD STORAGE\COLD STORAGE\customer.mdf;Integrated Security=True;User Instance=True")

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox10.Text & "','" & TextBox12.Text & "','" & TextBox13.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox14.Text & "','" & TextBox16.Text & "','" & TextBox17.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox18.Text & "','" & TextBox20.Text & "','" & TextBox21.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox22.Text & "','" & TextBox24.Text & "','" & TextBox25.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox26.Text & "','" & TextBox28.Text & "','" & TextBox29.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox30.Text & "','" & TextBox32.Text & "','" & TextBox33.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    MsgBox("New Record Inserted.")

                    TextBox38.Clear()
                    TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox3.Clear()
                    TextBox4.Clear()
                    TextBox5.Clear()
                    TextBox10.Clear()
                    TextBox12.Clear()
                    TextBox13.Clear()
                    TextBox14.Clear()
                    TextBox16.Clear()
                    TextBox17.Clear()
                    TextBox18.Clear()
                    TextBox20.Clear()
                    TextBox21.Clear()
                    TextBox22.Clear()
                    TextBox24.Clear()
                    TextBox25.Clear()
                    TextBox26.Clear()
                    TextBox28.Clear()
                    TextBox29.Clear()
                    TextBox30.Clear()
                    TextBox32.Clear()
                    TextBox33.Clear()
                End If

            ElseIf out = 7 Then
                If TextBox38.Text = "" Or TextBox5.Text = "" Or TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox10.Text = "" Or TextBox12.Text = "" Or TextBox13.Text = "" Or TextBox14.Text = "" Or TextBox16.Text = "" Or TextBox17.Text = "" Or TextBox18.Text = "" Or TextBox20.Text = "" Or TextBox21.Text = "" Or TextBox22.Text = "" Or TextBox24.Text = "" Or TextBox25.Text = "" Or TextBox26.Text = "" Or TextBox28.Text = "" Or TextBox29.Text = "" Or TextBox30.Text = "" Or TextBox32.Text = "" Or TextBox33.Text = "" Or TextBox34.Text = "" Or TextBox36.Text = "" Or TextBox37.Text = "" Then
                    MsgBox("Enter All The Fields")
                Else
                    con = New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=F:\COLD STORAGE\COLD STORAGE\customer.mdf;Integrated Security=True;User Instance=True")

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox10.Text & "','" & TextBox12.Text & "','" & TextBox13.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox14.Text & "','" & TextBox16.Text & "','" & TextBox17.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox18.Text & "','" & TextBox20.Text & "','" & TextBox21.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox22.Text & "','" & TextBox24.Text & "','" & TextBox25.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox26.Text & "','" & TextBox28.Text & "','" & TextBox29.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox30.Text & "','" & TextBox32.Text & "','" & TextBox33.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    con.Open()
                    mycom1 = New SqlCommand("insert into outwardTable([Id],[Name],[OutNo],[OutDate],[LotNoo],[BoxQuant],[BoxWeigh]) values ('" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox34.Text & "','" & TextBox36.Text & "','" & TextBox37.Text & "')", con)
                    mycom1.ExecuteNonQuery()
                    con.Close()

                    MsgBox("New Record Inserted.")

                    TextBox38.Clear()
                    TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox3.Clear()
                    TextBox4.Clear()
                    TextBox5.Clear()
                    TextBox10.Clear()
                    TextBox12.Clear()
                    TextBox13.Clear()
                    TextBox14.Clear()
                    TextBox16.Clear()
                    TextBox17.Clear()
                    TextBox18.Clear()
                    TextBox20.Clear()
                    TextBox21.Clear()
                    TextBox22.Clear()
                    TextBox24.Clear()
                    TextBox25.Clear()
                    TextBox26.Clear()
                    TextBox28.Clear()
                    TextBox29.Clear()
                    TextBox30.Clear()
                    TextBox32.Clear()
                    TextBox33.Clear()
                    TextBox34.Clear()
                    TextBox36.Clear()
                    TextBox37.Clear()
                End If

            Else

                MsgBox("ReCheck Fields")
            End If
        Catch ex As Exception
            MsgBox("Something went wrong")
        End Try

    End Sub

    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label8.Click

    End Sub

    Private Sub MYID()
        Try
            con = New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=F:\COLD STORAGE\COLD STORAGE\customer.mdf;Integrated Security=True;User Instance=True")
            con.Open()
            Dim sql As String = "select Max(OutNo) from outwardTable"
            mycom = New SqlCommand(sql, con)
            dr = mycom.ExecuteReader
            If dr.Read() Then
                If dr.IsDBNull(0) Then
                    Dim number As Integer = 1
                    Dim emplang As String = "OWD"
                    Dim append As String = emplang + number.ToString().PadLeft(3, "00")
                    TextBox1.Text = append
                Else
                    Dim empString As String = dr(0).ToString.Substring(0, 3)
                    Dim empID As Integer = dr(0).ToString().Substring(3) + 1
                    Dim append As String = empString + empID.ToString().PadLeft(3, "00")
                    TextBox1.Text = append.ToString
                End If
            End If
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        MYID()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            Dim take As String
            take = TextBox9.Text
            If take = 1 Then
                TextBox10.Visible = True
                TextBox12.Visible = True
                TextBox13.Visible = True
            ElseIf take = 2 Then
                TextBox10.Visible = True
                TextBox12.Visible = True
                TextBox13.Visible = True
                TextBox14.Visible = True
                TextBox16.Visible = True
                TextBox17.Visible = True
            ElseIf take = 3 Then
                TextBox10.Visible = True
                TextBox12.Visible = True
                TextBox13.Visible = True
                TextBox14.Visible = True
                TextBox16.Visible = True
                TextBox17.Visible = True
                TextBox18.Visible = True
                TextBox20.Visible = True
                TextBox21.Visible = True
            ElseIf take = 4 Then
                TextBox10.Visible = True
                TextBox12.Visible = True
                TextBox13.Visible = True
                TextBox14.Visible = True
                TextBox16.Visible = True
                TextBox17.Visible = True
                TextBox18.Visible = True
                TextBox20.Visible = True
                TextBox21.Visible = True
                TextBox22.Visible = True
                TextBox24.Visible = True
                TextBox25.Visible = True
            ElseIf take = 5 Then
                TextBox10.Visible = True
                TextBox12.Visible = True
                TextBox13.Visible = True
                TextBox14.Visible = True
                TextBox16.Visible = True
                TextBox17.Visible = True
                TextBox18.Visible = True
                TextBox20.Visible = True
                TextBox21.Visible = True
                TextBox22.Visible = True
                TextBox24.Visible = True
                TextBox25.Visible = True
                TextBox26.Visible = True
                TextBox28.Visible = True
                TextBox29.Visible = True
            ElseIf take = 6 Then
                TextBox10.Visible = True
                TextBox12.Visible = True
                TextBox13.Visible = True
                TextBox14.Visible = True
                TextBox16.Visible = True
                TextBox17.Visible = True
                TextBox18.Visible = True
                TextBox20.Visible = True
                TextBox21.Visible = True
                TextBox22.Visible = True
                TextBox24.Visible = True
                TextBox25.Visible = True
                TextBox26.Visible = True
                TextBox28.Visible = True
                TextBox29.Visible = True
                TextBox30.Visible = True
                TextBox32.Visible = True
                TextBox33.Visible = True
            ElseIf take = 7 Then
                TextBox10.Visible = True
                TextBox12.Visible = True
                TextBox13.Visible = True
                TextBox14.Visible = True
                TextBox16.Visible = True
                TextBox17.Visible = True
                TextBox18.Visible = True
                TextBox20.Visible = True
                TextBox21.Visible = True
                TextBox22.Visible = True
                TextBox24.Visible = True
                TextBox25.Visible = True
                TextBox26.Visible = True
                TextBox28.Visible = True
                TextBox29.Visible = True
                TextBox30.Visible = True
                TextBox32.Visible = True
                TextBox33.Visible = True
                TextBox34.Visible = True
                TextBox36.Visible = True
                TextBox37.Visible = True
            Else
                MsgBox("Enter Correect Number ")
            End If
        Catch ex As Exception
            MsgBox("Enter Valid Numbers Only")
        End Try
    End Sub
    Private Sub Display()
        If TextBox38.Text = "" Then
            TextBox38.Focus()
            TextBox5.Clear()
        Else
            Dim con As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=F:\COLD STORAGE\COLD STORAGE\customer.mdf;Integrated Security=True;User Instance=True")
            con.Open()
            da = New SqlDataAdapter("select Name from customerTable where Id='" & TextBox38.Text & "'", con)
            dt = New DataTable
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                TextBox5.Text = dt.Rows(0).Item("Name").ToString()
            End If
        End If

    End Sub
    Private Sub TextBox38_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox38.TextChanged
        Display()
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
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


    Private Sub recognizer_SpeechRecognized(ByVal sender As Object, ByVal e As SpeechRecognizedEventArgs)
        If (e.Result.Text = "Hy") Then
            p.SpeakAsync("Hello Sir Welcome to the Software")
        ElseIf (e.Result.Text = "Who are you") Then
            p.SpeakAsync("I'm Anna ,a virtual assitant created for your support")
        ElseIf (e.Result.Text = "save-record") Then
            Button1_Click(sender, e)
        ElseIf (e.Result.Text = "clear") Then
            Button2_Click(sender, e)
        ElseIf (e.Result.Text = "log-out") Then
            Button5_Click(sender, e)
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