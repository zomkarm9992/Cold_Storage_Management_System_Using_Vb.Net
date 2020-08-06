Imports System.Data
Imports System.Data.SqlClient
Imports System.Speech
Imports System.Speech.Synthesis
Imports System.Speech.Recognition
Imports System.Threading
Imports System.Globalization
Public Class BillMaster
    Dim con As SqlConnection
    Dim mycom As SqlCommand
    Dim mycom1 As SqlCommand
    Dim dt As New DataTable
    Dim maxid As Object
    Dim strid As String
    Dim intid As Integer
    Dim a As String
    Dim da As SqlDataAdapter
    Dim dr1 As SqlDataReader
    Dim dr2 As SqlDataReader
    Dim dbuilder As SqlCommandBuilder
    Dim ds As DataSet
    Dim p As New SpeechSynthesizer
    Dim gram As Grammar
    Public Event SpeechRecognized As  _
     EventHandler(Of SpeechRecognizedEventArgs)
    Public Event SpeechRecognitionRejected As  _
     EventHandler(Of SpeechRecognitionRejectedEventArgs)
    Dim recog As New SpeechRecognitionEngine

    Dim wordlist As String() = New String() {"Hy", "Who are you", "new-record", "save-record", "go-to-menu", "log-out", "close"}

    Dim speechon As Boolean = False



    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub BillMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Label9.Text = DateTime.Now.ToString("hh:mm dddd,dd MMMM yyyy")
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
        TextBox12.Visible = False
        TextBox16.Visible = False
        TextBox22.Visible = False
        TextBox24.Visible = False
        TextBox38.Visible = False
        TextBox40.Visible = False
        TextBox46.Visible = False
        TextBox48.Visible = False
        TextBox30.Visible = False
        TextBox32.Visible = False
        TextBox54.Visible = False
        TextBox56.Visible = False
        TextBox62.Visible = False
        TextBox64.Visible = False
        TextBox70.Visible = False
        TextBox72.Visible = False
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

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If speechon = True Then
            recog.RecognizeAsyncStop()
        End If

        Dim Menu As New Menu
        Menu.Show()
        Me.Hide()
    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub


    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            con = New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=F:\COLD STORAGE\COLD STORAGE\customer.mdf;Integrated Security=True;User Instance=True")
            con.Open()
            mycom = New SqlCommand("select * from BillMaster", con)
            mycom.Connection = con
            mycom.CommandText = "select MAX(BillNo) from BillMaster"
            maxid = mycom.ExecuteScalar
            If maxid Is DBNull.Value Then
                intid = 1
                TextBox1.Text = intid
            Else
                strid = CType(maxid, String)
                intid = CType(strid, String)
                intid = intid + 1
                TextBox1.Text = intid
            End If
            TextBox73.Focus()
            con.Close()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click


        con = New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=F:\COLD STORAGE\COLD STORAGE\customer.mdf;Integrated Security=True;User Instance=True")

        If Me.DataGridView1.Rows.Count = 1 Then
            If TextBox1.Text = "" Or TextBox73.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox7.Text = "" Or TextBox12.Text = "" Or TextBox16.Text = "" Then
                MsgBox("Enter all the fields")
            Else
                con.Open()

                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox12.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                MsgBox("New Record Inserted Successfully.")

                con.Close()
            End If


        ElseIf Me.DataGridView1.Rows.Count = 2 Then
            If TextBox1.Text = "" Or TextBox73.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox7.Text = "" Or TextBox12.Text = "" Or TextBox16.Text = "" Or TextBox22.Text = "" Or TextBox24.Text = "" Then
                MsgBox("Enter all the fields")
            Else

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox12.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox22.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()
                MsgBox("New Record Inserted Successfully.")
            End If

        ElseIf Me.DataGridView1.Rows.Count = 3 Then
            If TextBox1.Text = "" Or TextBox73.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox7.Text = "" Or TextBox12.Text = "" Or TextBox16.Text = "" Or TextBox22.Text = "" Or TextBox24.Text = "" Or TextBox38.Text = "" Or TextBox40.Text = "" Then
                MsgBox("Enter all the fields")
            Else

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox12.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox22.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                MsgBox("New Record Inserted Successfully.")
                con.Close()
            End If

        ElseIf Me.DataGridView1.Rows.Count = 4 Then
            If TextBox1.Text = "" Or TextBox73.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox7.Text = "" Or TextBox12.Text = "" Or TextBox16.Text = "" Or TextBox22.Text = "" Or TextBox24.Text = "" Or TextBox38.Text = "" Or TextBox40.Text = "" Or TextBox46.Text = "" Or TextBox48.Text = "" Then
                MsgBox("Enter all the fields")
            Else

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox12.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox22.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox46.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                MsgBox("New Record Inserted Successfully.")
                con.Close()
            End If

        ElseIf Me.DataGridView1.Rows.Count = 5 Then
            If TextBox1.Text = "" Or TextBox73.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox7.Text = "" Or TextBox12.Text = "" Or TextBox16.Text = "" Or TextBox22.Text = "" Or TextBox24.Text = "" Or TextBox38.Text = "" Or TextBox40.Text = "" Or TextBox46.Text = "" Or TextBox48.Text = "" Or TextBox30.Text = "" Or TextBox32.Text = "" Then
                MsgBox("Enter all the fields")
            Else

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox12.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox22.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox46.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox30.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                MsgBox("New Record Inserted Successfully.")
                con.Close()
            End If

        ElseIf Me.DataGridView1.Rows.Count = 6 Then
            If TextBox1.Text = "" Or TextBox73.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox7.Text = "" Or TextBox12.Text = "" Or TextBox16.Text = "" Or TextBox22.Text = "" Or TextBox24.Text = "" Or TextBox38.Text = "" Or TextBox40.Text = "" Or TextBox46.Text = "" Or TextBox48.Text = "" Or TextBox30.Text = "" Or TextBox32.Text = "" Or TextBox54.Text = "" Or TextBox56.Text = "" Then
                MsgBox("Enter all the fields")
            Else
                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox12.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox22.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox46.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox30.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox54.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                MsgBox("New Record Inserted Successfully.")
                con.Close()
            End If


        ElseIf Me.DataGridView1.Rows.Count = 7 Then
            If TextBox1.Text = "" Or TextBox73.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox7.Text = "" Or TextBox12.Text = "" Or TextBox16.Text = "" Or TextBox22.Text = "" Or TextBox24.Text = "" Or TextBox38.Text = "" Or TextBox40.Text = "" Or TextBox46.Text = "" Or TextBox48.Text = "" Or TextBox30.Text = "" Or TextBox32.Text = "" Or TextBox54.Text = "" Or TextBox56.Text = "" Or TextBox62.Text = "" Or TextBox64.Text = "" Then
                MsgBox("Enter all the fields")
            Else
                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox12.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox22.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox46.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox30.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox54.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox62.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                MsgBox("New Record Inserted Successfully.")
                con.Close()
            End If

        ElseIf Me.DataGridView1.Rows.Count = 8 Then
            If TextBox1.Text = "" Or TextBox73.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox7.Text = "" Or TextBox12.Text = "" Or TextBox16.Text = "" Or TextBox22.Text = "" Or TextBox24.Text = "" Or TextBox38.Text = "" Or TextBox40.Text = "" Or TextBox46.Text = "" Or TextBox48.Text = "" Or TextBox30.Text = "" Or TextBox32.Text = "" Or TextBox54.Text = "" Or TextBox56.Text = "" Or TextBox62.Text = "" Or TextBox64.Text = "" Or TextBox70.Text = "" Or TextBox72.Text = "" Then
                MsgBox("Enter all the fields")
            Else
                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox12.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox22.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox38.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox46.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox30.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox54.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox62.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                con.Close()

                con.Open()
                mycom = New SqlCommand("insert into BillMaster([Id],[BillNo],[PartyName],[BillDate],[OutNo],[Months],[rent],[lucharge],[Total]) values ('" & TextBox73.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox4.Text & "','" & TextBox70.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')", con)
                mycom.ExecuteNonQuery()
                MsgBox("New Record Inserted Successfully.")
                con.Close()
            End If

        End If



    End Sub


    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click

        con = New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=F:\COLD STORAGE\COLD STORAGE\customer.mdf;Integrated Security=True;User Instance=True")


        con.Open()
        mycom = New SqlCommand("select OutNo from outwardTable where Id='" & TextBox73.Text & "'", con)
        TextBox4.Text = CStr(mycom.ExecuteScalar())
        con.Close()

        Dim str1 As String
        str1 = " SELECT InwNo FROM inwTable WHERE Id='" & TextBox73.Text & "'"
        con.Open()
        mycom = New SqlCommand(str1, con)
        da = New SqlDataAdapter(mycom)
        ds = New DataSet
        da.Fill(ds, "inwTable")
        con.Close()
        DataGridView1.DataSource = ds
        DataGridView1.DataMember = "inwTable"

        Dim str2 As String
        str2 = " SELECT LotNoo,BoxQuant,BoxWeigh FROM outwardTable WHERE Id='" & TextBox73.Text & "'"
        con.Open()
        mycom = New SqlCommand(str2, con)
        da = New SqlDataAdapter(mycom)
        ds = New DataSet
        da.Fill(ds, "outwardTable")
        con.Close()
        DataGridView2.DataSource = ds
        DataGridView2.DataMember = "outwardTable"

        Dim str3 As String
        str3 = " SELECT InwDate,Rate FROM inwTable WHERE Id='" & TextBox73.Text & "'"
        con.Open()
        mycom = New SqlCommand(str3, con)
        da = New SqlDataAdapter(mycom)
        ds = New DataSet
        da.Fill(ds, "inwTable")
        con.Close()
        DataGridView3.DataSource = ds
        DataGridView3.DataMember = "inwTable"



        If Me.DataGridView2.Rows.Count = 1 Then
            TextBox12.Visible = True
            TextBox16.Visible = True

        ElseIf Me.DataGridView2.Rows.Count = 2 Then
            TextBox12.Visible = True
            TextBox16.Visible = True
            TextBox22.Visible = True
            TextBox24.Visible = True



        ElseIf Me.DataGridView2.Rows.Count = 3 Then
            TextBox12.Visible = True
            TextBox16.Visible = True
            TextBox22.Visible = True
            TextBox24.Visible = True
            TextBox38.Visible = True
            TextBox40.Visible = True





        ElseIf Me.DataGridView2.Rows.Count = 4 Then
            TextBox12.Visible = True
            TextBox16.Visible = True
            TextBox22.Visible = True
            TextBox24.Visible = True
            TextBox38.Visible = True
            TextBox40.Visible = True
            TextBox46.Visible = True
            TextBox48.Visible = True



        ElseIf Me.DataGridView2.Rows.Count = 5 Then
            TextBox12.Visible = True
            TextBox16.Visible = True
            TextBox22.Visible = True
            TextBox24.Visible = True
            TextBox38.Visible = True
            TextBox40.Visible = True
            TextBox46.Visible = True
            TextBox48.Visible = True
            TextBox30.Visible = True
            TextBox32.Visible = True



        ElseIf Me.DataGridView2.Rows.Count = 6 Then
            TextBox12.Visible = True
            TextBox16.Visible = True
            TextBox22.Visible = True
            TextBox24.Visible = True
            TextBox38.Visible = True
            TextBox40.Visible = True
            TextBox46.Visible = True
            TextBox48.Visible = True
            TextBox30.Visible = True
            TextBox32.Visible = True
            TextBox54.Visible = True
            TextBox56.Visible = True



        ElseIf Me.DataGridView2.Rows.Count = 7 Then
            TextBox12.Visible = True
            TextBox16.Visible = True
            TextBox22.Visible = True
            TextBox24.Visible = True
            TextBox38.Visible = True
            TextBox40.Visible = True
            TextBox46.Visible = True
            TextBox48.Visible = True
            TextBox30.Visible = True
            TextBox32.Visible = True
            TextBox54.Visible = True
            TextBox56.Visible = True
            TextBox62.Visible = True
            TextBox64.Visible = True


        ElseIf Me.DataGridView2.Rows.Count = 8 Then
            TextBox12.Visible = True
            TextBox16.Visible = True
            TextBox22.Visible = True
            TextBox24.Visible = True
            TextBox38.Visible = True
            TextBox40.Visible = True
            TextBox46.Visible = True
            TextBox48.Visible = True
            TextBox30.Visible = True
            TextBox32.Visible = True
            TextBox54.Visible = True
            TextBox56.Visible = True
            TextBox62.Visible = True
            TextBox64.Visible = True
            TextBox70.Visible = True
            TextBox72.Visible = True


        End If



    End Sub



    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged



    End Sub

    Private Sub TextBox12_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox12.TextChanged
        Try

            If Me.DataGridView2.Rows.Count = 1 Then

                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 2 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 3 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 4 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 5 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 6 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()
                TextBox56.Text = (Double.Parse(DataGridView2.Rows(5).Cells(2).Value) * Double.Parse(DataGridView3.Rows(5).Cells(1).Value) * Double.Parse(TextBox54.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 7 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()
                TextBox56.Text = (Double.Parse(DataGridView2.Rows(5).Cells(2).Value) * Double.Parse(DataGridView3.Rows(5).Cells(1).Value) * Double.Parse(TextBox54.Text)).ToString()
                TextBox64.Text = (Double.Parse(DataGridView2.Rows(6).Cells(2).Value) * Double.Parse(DataGridView3.Rows(6).Cells(1).Value) * Double.Parse(TextBox62.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 8 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()
                TextBox56.Text = (Double.Parse(DataGridView2.Rows(5).Cells(2).Value) * Double.Parse(DataGridView3.Rows(5).Cells(1).Value) * Double.Parse(TextBox54.Text)).ToString()
                TextBox64.Text = (Double.Parse(DataGridView2.Rows(6).Cells(2).Value) * Double.Parse(DataGridView3.Rows(6).Cells(1).Value) * Double.Parse(TextBox62.Text)).ToString()
                TextBox72.Text = (Double.Parse(DataGridView2.Rows(7).Cells(2).Value) * Double.Parse(DataGridView3.Rows(7).Cells(1).Value) * Double.Parse(TextBox70.Text)).ToString()

            Else
                MsgBox("Invalid Prompt")
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub TextBox7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged

    End Sub

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged

        If TextBox6.Text = "" Then
            TextBox6.Focus()
        Else
            TextBox7.Text = (Double.Parse(TextBox5.Text) + Double.Parse(TextBox6.Text)).ToString()
        End If


    End Sub

    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click

    End Sub

    Private Sub Display()
        If TextBox73.Text = "" Then
            TextBox73.Focus()
            TextBox2.Clear()
        Else
            Dim con As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=F:\COLD STORAGE\COLD STORAGE\customer.mdf;Integrated Security=True;User Instance=True")
            con.Open()
            da = New SqlDataAdapter("select Name from customerTable where Id='" & TextBox73.Text & "'", con)
            dt = New DataTable
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                TextBox2.Text = dt.Rows(0).Item("Name").ToString()
            End If
        End If
    End Sub

    Private Sub TextBox73_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox73.TextChanged
        Display()
    End Sub



    Private Sub TextBox38_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox38.TextChanged
        Try

            If Me.DataGridView2.Rows.Count = 1 Then

                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 2 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 3 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 4 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 5 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 6 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()
                TextBox56.Text = (Double.Parse(DataGridView2.Rows(5).Cells(2).Value) * Double.Parse(DataGridView3.Rows(5).Cells(1).Value) * Double.Parse(TextBox54.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 7 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()
                TextBox56.Text = (Double.Parse(DataGridView2.Rows(5).Cells(2).Value) * Double.Parse(DataGridView3.Rows(5).Cells(1).Value) * Double.Parse(TextBox54.Text)).ToString()
                TextBox64.Text = (Double.Parse(DataGridView2.Rows(6).Cells(2).Value) * Double.Parse(DataGridView3.Rows(6).Cells(1).Value) * Double.Parse(TextBox62.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 8 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()
                TextBox56.Text = (Double.Parse(DataGridView2.Rows(5).Cells(2).Value) * Double.Parse(DataGridView3.Rows(5).Cells(1).Value) * Double.Parse(TextBox54.Text)).ToString()
                TextBox64.Text = (Double.Parse(DataGridView2.Rows(6).Cells(2).Value) * Double.Parse(DataGridView3.Rows(6).Cells(1).Value) * Double.Parse(TextBox62.Text)).ToString()
                TextBox72.Text = (Double.Parse(DataGridView2.Rows(7).Cells(2).Value) * Double.Parse(DataGridView3.Rows(7).Cells(1).Value) * Double.Parse(TextBox70.Text)).ToString()

            Else
                MsgBox("Invalid Prompt")
            End If
        Catch ex As Exception
        End Try

    End Sub


    Private Sub TextBox46_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox46.TextChanged
        Try

            If Me.DataGridView2.Rows.Count = 1 Then

                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 2 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 3 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 4 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 5 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 6 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()
                TextBox56.Text = (Double.Parse(DataGridView2.Rows(5).Cells(2).Value) * Double.Parse(DataGridView3.Rows(5).Cells(1).Value) * Double.Parse(TextBox54.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 7 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()
                TextBox56.Text = (Double.Parse(DataGridView2.Rows(5).Cells(2).Value) * Double.Parse(DataGridView3.Rows(5).Cells(1).Value) * Double.Parse(TextBox54.Text)).ToString()
                TextBox64.Text = (Double.Parse(DataGridView2.Rows(6).Cells(2).Value) * Double.Parse(DataGridView3.Rows(6).Cells(1).Value) * Double.Parse(TextBox62.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 8 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()
                TextBox56.Text = (Double.Parse(DataGridView2.Rows(5).Cells(2).Value) * Double.Parse(DataGridView3.Rows(5).Cells(1).Value) * Double.Parse(TextBox54.Text)).ToString()
                TextBox64.Text = (Double.Parse(DataGridView2.Rows(6).Cells(2).Value) * Double.Parse(DataGridView3.Rows(6).Cells(1).Value) * Double.Parse(TextBox62.Text)).ToString()
                TextBox72.Text = (Double.Parse(DataGridView2.Rows(7).Cells(2).Value) * Double.Parse(DataGridView3.Rows(7).Cells(1).Value) * Double.Parse(TextBox70.Text)).ToString()

            Else
                MsgBox("Invalid Prompt")
            End If
        Catch ex As Exception
        End Try

    End Sub


    Private Sub TextBox30_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox30.TextChanged
        Try

            If Me.DataGridView2.Rows.Count = 1 Then

                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 2 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 3 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 4 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 5 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 6 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()
                TextBox56.Text = (Double.Parse(DataGridView2.Rows(5).Cells(2).Value) * Double.Parse(DataGridView3.Rows(5).Cells(1).Value) * Double.Parse(TextBox54.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 7 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()
                TextBox56.Text = (Double.Parse(DataGridView2.Rows(5).Cells(2).Value) * Double.Parse(DataGridView3.Rows(5).Cells(1).Value) * Double.Parse(TextBox54.Text)).ToString()
                TextBox64.Text = (Double.Parse(DataGridView2.Rows(6).Cells(2).Value) * Double.Parse(DataGridView3.Rows(6).Cells(1).Value) * Double.Parse(TextBox62.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 8 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()
                TextBox56.Text = (Double.Parse(DataGridView2.Rows(5).Cells(2).Value) * Double.Parse(DataGridView3.Rows(5).Cells(1).Value) * Double.Parse(TextBox54.Text)).ToString()
                TextBox64.Text = (Double.Parse(DataGridView2.Rows(6).Cells(2).Value) * Double.Parse(DataGridView3.Rows(6).Cells(1).Value) * Double.Parse(TextBox62.Text)).ToString()
                TextBox72.Text = (Double.Parse(DataGridView2.Rows(7).Cells(2).Value) * Double.Parse(DataGridView3.Rows(7).Cells(1).Value) * Double.Parse(TextBox70.Text)).ToString()

            Else
                MsgBox("Invalid Prompt")
            End If
        Catch ex As Exception
        End Try

    End Sub


    Private Sub TextBox54_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox54.TextChanged
        Try

            If Me.DataGridView2.Rows.Count = 1 Then

                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 2 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 3 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 4 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 5 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 6 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()
                TextBox56.Text = (Double.Parse(DataGridView2.Rows(5).Cells(2).Value) * Double.Parse(DataGridView3.Rows(5).Cells(1).Value) * Double.Parse(TextBox54.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 7 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()
                TextBox56.Text = (Double.Parse(DataGridView2.Rows(5).Cells(2).Value) * Double.Parse(DataGridView3.Rows(5).Cells(1).Value) * Double.Parse(TextBox54.Text)).ToString()
                TextBox64.Text = (Double.Parse(DataGridView2.Rows(6).Cells(2).Value) * Double.Parse(DataGridView3.Rows(6).Cells(1).Value) * Double.Parse(TextBox62.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 8 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()
                TextBox56.Text = (Double.Parse(DataGridView2.Rows(5).Cells(2).Value) * Double.Parse(DataGridView3.Rows(5).Cells(1).Value) * Double.Parse(TextBox54.Text)).ToString()
                TextBox64.Text = (Double.Parse(DataGridView2.Rows(6).Cells(2).Value) * Double.Parse(DataGridView3.Rows(6).Cells(1).Value) * Double.Parse(TextBox62.Text)).ToString()
                TextBox72.Text = (Double.Parse(DataGridView2.Rows(7).Cells(2).Value) * Double.Parse(DataGridView3.Rows(7).Cells(1).Value) * Double.Parse(TextBox70.Text)).ToString()

            Else
                MsgBox("Invalid Prompt")
            End If
        Catch ex As Exception
        End Try

    End Sub


    Private Sub TextBox62_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox62.TextChanged
        Try

            If Me.DataGridView2.Rows.Count = 1 Then

                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 2 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 3 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 4 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 5 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 6 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()
                TextBox56.Text = (Double.Parse(DataGridView2.Rows(5).Cells(2).Value) * Double.Parse(DataGridView3.Rows(5).Cells(1).Value) * Double.Parse(TextBox54.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 7 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()
                TextBox56.Text = (Double.Parse(DataGridView2.Rows(5).Cells(2).Value) * Double.Parse(DataGridView3.Rows(5).Cells(1).Value) * Double.Parse(TextBox54.Text)).ToString()
                TextBox64.Text = (Double.Parse(DataGridView2.Rows(6).Cells(2).Value) * Double.Parse(DataGridView3.Rows(6).Cells(1).Value) * Double.Parse(TextBox62.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 8 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()
                TextBox56.Text = (Double.Parse(DataGridView2.Rows(5).Cells(2).Value) * Double.Parse(DataGridView3.Rows(5).Cells(1).Value) * Double.Parse(TextBox54.Text)).ToString()
                TextBox64.Text = (Double.Parse(DataGridView2.Rows(6).Cells(2).Value) * Double.Parse(DataGridView3.Rows(6).Cells(1).Value) * Double.Parse(TextBox62.Text)).ToString()
                TextBox72.Text = (Double.Parse(DataGridView2.Rows(7).Cells(2).Value) * Double.Parse(DataGridView3.Rows(7).Cells(1).Value) * Double.Parse(TextBox70.Text)).ToString()

            Else
                MsgBox("Invalid Prompt")
            End If
        Catch ex As Exception
        End Try

    End Sub


    Private Sub TextBox70_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox70.TextChanged
        Try

            If Me.DataGridView2.Rows.Count = 1 Then

                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 2 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 3 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 4 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 5 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 6 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()
                TextBox56.Text = (Double.Parse(DataGridView2.Rows(5).Cells(2).Value) * Double.Parse(DataGridView3.Rows(5).Cells(1).Value) * Double.Parse(TextBox54.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 7 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()
                TextBox56.Text = (Double.Parse(DataGridView2.Rows(5).Cells(2).Value) * Double.Parse(DataGridView3.Rows(5).Cells(1).Value) * Double.Parse(TextBox54.Text)).ToString()
                TextBox64.Text = (Double.Parse(DataGridView2.Rows(6).Cells(2).Value) * Double.Parse(DataGridView3.Rows(6).Cells(1).Value) * Double.Parse(TextBox62.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 8 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()
                TextBox56.Text = (Double.Parse(DataGridView2.Rows(5).Cells(2).Value) * Double.Parse(DataGridView3.Rows(5).Cells(1).Value) * Double.Parse(TextBox54.Text)).ToString()
                TextBox64.Text = (Double.Parse(DataGridView2.Rows(6).Cells(2).Value) * Double.Parse(DataGridView3.Rows(6).Cells(1).Value) * Double.Parse(TextBox62.Text)).ToString()
                TextBox72.Text = (Double.Parse(DataGridView2.Rows(7).Cells(2).Value) * Double.Parse(DataGridView3.Rows(7).Cells(1).Value) * Double.Parse(TextBox70.Text)).ToString()

            Else
                MsgBox("Invalid Prompt")
            End If
        Catch ex As Exception
        End Try

    End Sub


    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick


    End Sub

    Private Sub DataGridView2_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub

    Private Sub Panel4_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel4.Paint

    End Sub

    Private Sub TextBox16_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox16.TextChanged
        Try
            If Me.DataGridView2.Rows.Count = 1 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 2 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 3 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 4 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 5 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 6 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text) + Double.Parse(TextBox56.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 7 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text) + Double.Parse(TextBox56.Text) + Double.Parse(TextBox64.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 8 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text) + Double.Parse(TextBox56.Text) + Double.Parse(TextBox64.Text) + Double.Parse(TextBox72.Text)).ToString()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub TextBox24_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox24.TextChanged
        Try
            If Me.DataGridView2.Rows.Count = 1 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 2 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 3 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 4 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 5 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 6 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text) + Double.Parse(TextBox56.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 7 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text) + Double.Parse(TextBox56.Text) + Double.Parse(TextBox64.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 8 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text) + Double.Parse(TextBox56.Text) + Double.Parse(TextBox64.Text) + Double.Parse(TextBox72.Text)).ToString()
            End If
        Catch ex As Exception

        End Try



    End Sub

    Private Sub TextBox40_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox40.TextChanged
        Try
            If Me.DataGridView2.Rows.Count = 1 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 2 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 3 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 4 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 5 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 6 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text) + Double.Parse(TextBox56.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 7 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text) + Double.Parse(TextBox56.Text) + Double.Parse(TextBox64.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 8 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text) + Double.Parse(TextBox56.Text) + Double.Parse(TextBox64.Text) + Double.Parse(TextBox72.Text)).ToString()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox48_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox48.TextChanged
        Try
            If Me.DataGridView2.Rows.Count = 1 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 2 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 3 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 4 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 5 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 6 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text) + Double.Parse(TextBox56.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 7 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text) + Double.Parse(TextBox56.Text) + Double.Parse(TextBox64.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 8 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text) + Double.Parse(TextBox56.Text) + Double.Parse(TextBox64.Text) + Double.Parse(TextBox72.Text)).ToString()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox32_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox32.TextChanged
        Try
            If Me.DataGridView2.Rows.Count = 1 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 2 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 3 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 4 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 5 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 6 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text) + Double.Parse(TextBox56.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 7 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text) + Double.Parse(TextBox56.Text) + Double.Parse(TextBox64.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 8 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text) + Double.Parse(TextBox56.Text) + Double.Parse(TextBox64.Text) + Double.Parse(TextBox72.Text)).ToString()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox56_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox56.TextChanged
        Try
            If Me.DataGridView2.Rows.Count = 1 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 2 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 3 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 4 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 5 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 6 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text) + Double.Parse(TextBox56.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 7 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text) + Double.Parse(TextBox56.Text) + Double.Parse(TextBox64.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 8 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text) + Double.Parse(TextBox56.Text) + Double.Parse(TextBox64.Text) + Double.Parse(TextBox72.Text)).ToString()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox64_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox64.TextChanged
        Try
            If Me.DataGridView2.Rows.Count = 1 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 2 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 3 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 4 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 5 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 6 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text) + Double.Parse(TextBox56.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 7 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text) + Double.Parse(TextBox56.Text) + Double.Parse(TextBox64.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 8 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text) + Double.Parse(TextBox56.Text) + Double.Parse(TextBox64.Text) + Double.Parse(TextBox72.Text)).ToString()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox72_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox72.TextChanged
        Try
            If Me.DataGridView2.Rows.Count = 1 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 2 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 3 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 4 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 5 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 6 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text) + Double.Parse(TextBox56.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 7 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text) + Double.Parse(TextBox56.Text) + Double.Parse(TextBox64.Text)).ToString()
            ElseIf Me.DataGridView2.Rows.Count = 8 Then
                TextBox5.Text = (Double.Parse(TextBox16.Text) + Double.Parse(TextBox24.Text) + Double.Parse(TextBox40.Text) + Double.Parse(TextBox48.Text) + Double.Parse(TextBox32.Text) + Double.Parse(TextBox56.Text) + Double.Parse(TextBox64.Text) + Double.Parse(TextBox72.Text)).ToString()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox22_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox22.TextChanged
        Try

            If Me.DataGridView2.Rows.Count = 1 Then

                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 2 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 3 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 4 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 5 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 6 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()
                TextBox56.Text = (Double.Parse(DataGridView2.Rows(5).Cells(2).Value) * Double.Parse(DataGridView3.Rows(5).Cells(1).Value) * Double.Parse(TextBox54.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 7 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()
                TextBox56.Text = (Double.Parse(DataGridView2.Rows(5).Cells(2).Value) * Double.Parse(DataGridView3.Rows(5).Cells(1).Value) * Double.Parse(TextBox54.Text)).ToString()
                TextBox64.Text = (Double.Parse(DataGridView2.Rows(6).Cells(2).Value) * Double.Parse(DataGridView3.Rows(6).Cells(1).Value) * Double.Parse(TextBox62.Text)).ToString()

            ElseIf Me.DataGridView2.Rows.Count = 8 Then
                TextBox16.Text = (Double.Parse(DataGridView2.Rows(0).Cells(2).Value) * Double.Parse(DataGridView3.Rows(0).Cells(1).Value) * Double.Parse(TextBox12.Text)).ToString()
                TextBox24.Text = (Double.Parse(DataGridView2.Rows(1).Cells(2).Value) * Double.Parse(DataGridView3.Rows(1).Cells(1).Value) * Double.Parse(TextBox22.Text)).ToString()
                TextBox40.Text = (Double.Parse(DataGridView2.Rows(2).Cells(2).Value) * Double.Parse(DataGridView3.Rows(2).Cells(1).Value) * Double.Parse(TextBox38.Text)).ToString()
                TextBox48.Text = (Double.Parse(DataGridView2.Rows(3).Cells(2).Value) * Double.Parse(DataGridView3.Rows(3).Cells(1).Value) * Double.Parse(TextBox46.Text)).ToString()
                TextBox32.Text = (Double.Parse(DataGridView2.Rows(4).Cells(2).Value) * Double.Parse(DataGridView3.Rows(4).Cells(1).Value) * Double.Parse(TextBox30.Text)).ToString()
                TextBox56.Text = (Double.Parse(DataGridView2.Rows(5).Cells(2).Value) * Double.Parse(DataGridView3.Rows(5).Cells(1).Value) * Double.Parse(TextBox54.Text)).ToString()
                TextBox64.Text = (Double.Parse(DataGridView2.Rows(6).Cells(2).Value) * Double.Parse(DataGridView3.Rows(6).Cells(1).Value) * Double.Parse(TextBox62.Text)).ToString()
                TextBox72.Text = (Double.Parse(DataGridView2.Rows(7).Cells(2).Value) * Double.Parse(DataGridView3.Rows(7).Cells(1).Value) * Double.Parse(TextBox70.Text)).ToString()

            Else
                MsgBox("Invalid Prompt")
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button7_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        TextBox12.Visible = False
        TextBox16.Visible = False
        TextBox22.Visible = False
        TextBox24.Visible = False
        TextBox38.Visible = False
        TextBox40.Visible = False
        TextBox46.Visible = False
        TextBox48.Visible = False
        TextBox30.Visible = False
        TextBox32.Visible = False
        TextBox54.Visible = False
        TextBox56.Visible = False
        TextBox62.Visible = False
        TextBox64.Visible = False
        TextBox70.Visible = False
        TextBox72.Visible = False

        If DataGridView2.Rows.Count = 1 Then
            TextBox1.Clear()

            TextBox73.Clear()
            TextBox2.Clear()
            TextBox4.Clear()
            TextBox5.Clear()
            TextBox6.Clear()
            TextBox7.Clear()
            TextBox12.Clear()
            TextBox16.Clear()
            DataGridView1.DataSource = DBNull.Value
            DataGridView2.DataSource = DBNull.Value
            DataGridView3.DataSource = DBNull.Value
        ElseIf DataGridView2.Rows.Count = 2 Then
            TextBox1.Clear()

            TextBox73.Clear()
            TextBox2.Clear()
            TextBox4.Clear()
            TextBox5.Clear()
            TextBox6.Clear()
            TextBox7.Clear()
            TextBox12.Clear()
            TextBox16.Clear()
            TextBox22.Clear()
            TextBox24.Clear()
            DataGridView1.DataSource = DBNull.Value
            DataGridView2.DataSource = DBNull.Value
            DataGridView3.DataSource = DBNull.Value
        ElseIf DataGridView2.Rows.Count = 3 Then
            TextBox1.Clear()
            TextBox73.Clear()
            TextBox2.Clear()
            TextBox4.Clear()
            TextBox5.Clear()
            TextBox6.Clear()
            TextBox7.Clear()
            TextBox12.Clear()
            TextBox16.Clear()
            TextBox22.Clear()
            TextBox24.Clear()
            TextBox38.Clear()
            TextBox40.Clear()
            DataGridView1.DataSource = DBNull.Value
            DataGridView2.DataSource = DBNull.Value
            DataGridView3.DataSource = DBNull.Value
        ElseIf DataGridView2.Rows.Count = 4 Then
            TextBox1.Clear()
            TextBox73.Clear()
            TextBox2.Clear()
            TextBox4.Clear()
            TextBox5.Clear()
            TextBox6.Clear()
            TextBox7.Clear()
            TextBox12.Clear()
            TextBox16.Clear()
            TextBox22.Clear()
            TextBox24.Clear()
            TextBox38.Clear()
            TextBox40.Clear()
            TextBox46.Clear()
            TextBox48.Clear()
            DataGridView1.DataSource = DBNull.Value
            DataGridView2.DataSource = DBNull.Value
            DataGridView3.DataSource = DBNull.Value
        ElseIf DataGridView2.Rows.Count = 5 Then
            TextBox1.Clear()
            TextBox73.Clear()
            TextBox2.Clear()
            TextBox4.Clear()
            TextBox5.Clear()
            TextBox6.Clear()
            TextBox7.Clear()
            TextBox12.Clear()
            TextBox16.Clear()
            TextBox22.Clear()
            TextBox24.Clear()
            TextBox38.Clear()
            TextBox40.Clear()
            TextBox46.Clear()
            TextBox48.Clear()
            TextBox30.Clear()
            TextBox32.Clear()
            DataGridView1.DataSource = DBNull.Value
            DataGridView2.DataSource = DBNull.Value
            DataGridView3.DataSource = DBNull.Value
        ElseIf DataGridView2.Rows.Count = 6 Then
            TextBox1.Clear()
            TextBox73.Clear()
            TextBox2.Clear()
            TextBox4.Clear()
            TextBox5.Clear()
            TextBox6.Clear()
            TextBox7.Clear()
            TextBox12.Clear()
            TextBox16.Clear()
            TextBox22.Clear()
            TextBox24.Clear()
            TextBox38.Clear()
            TextBox40.Clear()
            TextBox46.Clear()
            TextBox48.Clear()
            TextBox30.Clear()
            TextBox32.Clear()
            TextBox54.Clear()
            TextBox56.Clear()
            DataGridView1.DataSource = DBNull.Value
            DataGridView2.DataSource = DBNull.Value
            DataGridView3.DataSource = DBNull.Value
        ElseIf DataGridView2.Rows.Count = 7 Then
            TextBox1.Clear()
            TextBox73.Clear()
            TextBox2.Clear()
            TextBox4.Clear()
            TextBox5.Clear()
            TextBox6.Clear()
            TextBox7.Clear()
            TextBox12.Clear()
            TextBox16.Clear()
            TextBox22.Clear()
            TextBox24.Clear()
            TextBox38.Clear()
            TextBox40.Clear()
            TextBox46.Clear()
            TextBox48.Clear()
            TextBox30.Clear()
            TextBox32.Clear()
            TextBox54.Clear()
            TextBox56.Clear()
            TextBox62.Clear()
            TextBox64.Clear()
            DataGridView1.DataSource = DBNull.Value
            DataGridView2.DataSource = DBNull.Value
            DataGridView3.DataSource = DBNull.Value
        ElseIf DataGridView2.Rows.Count = 8 Then
            TextBox1.Clear()
            TextBox73.Clear()
            TextBox2.Clear()
            TextBox4.Clear()
            TextBox5.Clear()
            TextBox6.Clear()
            TextBox7.Clear()
            TextBox12.Clear()
            TextBox16.Clear()
            TextBox22.Clear()
            TextBox24.Clear()
            TextBox38.Clear()
            TextBox40.Clear()
            TextBox46.Clear()
            TextBox48.Clear()
            TextBox30.Clear()
            TextBox32.Clear()
            TextBox54.Clear()
            TextBox56.Clear()
            TextBox62.Clear()
            TextBox64.Clear()
            TextBox70.Clear()
            TextBox72.Clear()
            DataGridView1.DataSource = DBNull.Value
            DataGridView2.DataSource = DBNull.Value
            DataGridView3.DataSource = DBNull.Value
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click

        Dim A As New BillReport
        A.Show()
    End Sub

    Private Sub recognizer_SpeechRecognized(ByVal sender As Object, ByVal e As SpeechRecognizedEventArgs)
        If (e.Result.Text = "Hy") Then
            p.SpeakAsync("Hello Sir Welcome to the Software")
        ElseIf (e.Result.Text = "Who are you") Then
            p.SpeakAsync("I'm Anna ,a virtual assitant created for your support")
        ElseIf (e.Result.Text = "print-bill") Then
            Button6_Click(sender, e)
        ElseIf (e.Result.Text = "save-record") Then
            Button1_Click(sender, e)
        ElseIf (e.Result.Text = "clear") Then
            Button7_Click_1(sender, e)
        ElseIf (e.Result.Text = "log-out") Then
            Button3_Click(sender, e)
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