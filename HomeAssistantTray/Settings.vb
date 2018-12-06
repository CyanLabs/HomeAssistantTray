Public Class Settings
    Dim Screens() As System.Windows.Forms.Screen

    Private Sub Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Screens = System.Windows.Forms.Screen.AllScreens
        For Each s As Screen In Screens
            ComboBox1.Items.Add(s.DeviceName.Replace("\\.\DISPLAY", ""))
        Next
        ComboBox1.SelectedIndex = My.Settings.Monitor
    End Sub

    Private Sub Settings_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        My.Settings.Lovelace = CheckBox1.Checked
        Form1.ReloadToolStripMenuItem.PerformClick()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        My.Settings.Monitor = ComboBox1.SelectedIndex
        Dim x As Integer, y As Integer
        x = Screen.AllScreens(My.Settings.Monitor).WorkingArea.Width / 2 - (Me.Width)
        y = Screen.AllScreens(My.Settings.Monitor).WorkingArea.Height / 2 - (Me.Height)
        Location = Screen.AllScreens(My.Settings.Monitor).Bounds.Location + New Point(x, y)
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        My.Settings.Startup = sender.checked
        If sender.checked = True Then
            My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).SetValue(Application.ProductName, Application.ExecutablePath & " -startup")
        Else
            If Not My.Computer.Registry.CurrentUser.GetValue("SOFTWARE\Microsoft\Windows\CurrentVersion\Run") Is Nothing Then My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).DeleteValue(Application.ProductName)
        End If
    End Sub
End Class