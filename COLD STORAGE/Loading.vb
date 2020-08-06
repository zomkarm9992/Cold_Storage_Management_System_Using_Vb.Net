Public Class Loading
    Dim MyProgress As Integer
    Private Sub ProgressBar1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProgressBar1.Click

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        ProgressBar1.Value = MyProgress
        If MyProgress < 50 Then MyProgress = MyProgress + 1
        Label6.Text = "Loading" & MyProgress & "%"
        If MyProgress = 50 Then
            Dim menu As New Menu
            menu.Show()
            Me.Dispose()
            Timer1.Enabled = False

        End If
    End Sub

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ProgressBar1.Style = ProgressBarStyle.Marquee
        ProgressBar1.Style = ProgressBarStyle.Continuous
        ProgressBar1.Step = 1
        Timer1.Enabled = True
    End Sub

    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click

    End Sub
End Class