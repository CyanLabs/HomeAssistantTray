Imports CefSharp.WinForms

Public Class Form1
    Dim URL As String, Lovelace As String = "/lovelace", x As Integer, y As Integer, chromeBrowser As ChromiumWebBrowser

    Private Sub ntfyMain_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ntfyMain.MouseDown
        If e.Button = MouseButtons.Left Then
            Me.Opacity = 1
            Me.Activate()
        End If
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Opacity = 0
        If Environment.GetCommandLineArgs.Count > 1 Then
            For Each arg In Environment.GetCommandLineArgs
                If arg.ToLower.Contains("/resetscreen") Then My.Settings.Monitor = 0
            Next
        End If

        If My.Settings.URL = "" Then Settings.Show()

        x = Screen.AllScreens(My.Settings.Monitor).WorkingArea.Width - (Me.Width)
        y = Screen.AllScreens(My.Settings.Monitor).WorkingArea.Height - (Me.Height)
        Location = Screen.AllScreens(My.Settings.Monitor).Bounds.Location + New Point(x, y)

        Dim chromeSettings As CefSettings = New CefSettings()
        chromeSettings.CachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\CEF"
        CefSharp.Cef.Initialize(chromeSettings)


        If My.Settings.Lovelace = True Then
            chromeBrowser = New ChromiumWebBrowser(My.Settings.URL & Lovelace)
        Else
            chromeBrowser = New ChromiumWebBrowser(My.Settings.URL)
        End If
        Me.Controls.Add(chromeBrowser)
        chromeBrowser.Dock = DockStyle.Fill
    End Sub

    Private Sub ReloadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReloadToolStripMenuItem.Click
        If My.Settings.Lovelace = True Then
            chromeBrowser.Load(My.Settings.URL & Lovelace)
        Else
            chromeBrowser.Load(My.Settings.URL)
        End If
    End Sub

    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
        Settings.Show()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        CefSharp.Cef.Shutdown()
    End Sub

    Private Sub Form1__Deactivate(sender As Object, e As EventArgs) Handles MyBase.Deactivate
        Me.Opacity = 0
    End Sub
End Class