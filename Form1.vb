Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Imports System.Security.Permissions

'フォームのCreateParamsプロパティをオーバーライドする
Public Class Form1
    Private Disc1, Disc2 As New PerformanceCounter(),
        icos(3) As Icon

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        <SecurityPermission(SecurityAction.Demand,
        Flags:=SecurityPermissionFlag.UnmanagedCode)>
        Get
            Const WS_EX_TOOLWINDOW As Int32 = &H80
            Const WS_POPUP As Int32 = &H80000000
            Const WS_VISIBLE As Int32 = &H10000000
            Const WS_SYSMENU As Int32 = &H80000
            Const WS_MAXIMIZEBOX As Int32 = &H10000

            Dim cp As System.Windows.Forms.CreateParams
            cp = MyBase.CreateParams
            cp.ExStyle = WS_EX_TOOLWINDOW
            cp.Style = WS_POPUP Or WS_VISIBLE Or
            WS_SYSMENU Or WS_MAXIMIZEBOX
            cp.Height = 0
            cp.Width = 0
            Return cp
        End Get
    End Property

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        '現在のコードを実行しているAssemblyを取得
        Dim myAssembly As System.Reflection.Assembly =
            System.Reflection.Assembly.GetExecutingAssembly()
        '指定されたマニフェストリソースを読み込む
        Dim myIcon As New Icon(myAssembly.GetManifestResourceStream("HddLamp.lamp_00.ico"))
        NotifyIcon1.Icon = myIcon
        NotifyIcon1.BalloonTipText = "HddLamp"
        icos(0) = myIcon
        myIcon = New Icon(myAssembly.GetManifestResourceStream("HddLamp.lamp_01.ico"))
        icos(1) = myIcon
        myIcon = New Icon(myAssembly.GetManifestResourceStream("HddLamp.lamp_02.ico"))
        icos(2) = myIcon
        myIcon = New Icon(myAssembly.GetManifestResourceStream("HddLamp.lamp_03.ico"))
        icos(3) = myIcon
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim p1, p2 As Single
        'Disk1
        Disc1.CategoryName = "LogicalDisk"
        'Disc1.CategoryName = "PhysicalDisk"
        'Disc1.CounterName = "Disk Transfers/sec"
        Disc1.CounterName = "Disk Reads/sec"
        Disc1.InstanceName = "C:"

        p1 = Disc1.NextValue()
        'Label1.Text = p1.ToString()
        Dim ipos = 0
        If p1 > .0 Then
            Label1.BackColor = Color.LightGreen
            ipos += 1
        Else
            Label1.BackColor = Nothing
        End If

        Disc2.CategoryName = "LogicalDisk"
        Disc2.CounterName = "Disk Writes/sec"
        Disc2.InstanceName = "C:"
        p2 = Disc2.NextValue()
        'Label2.Text = p2.ToString()
        If p2 > .0 Then
            Label2.BackColor = Color.Red
            ipos += 2
        Else
            Label2.BackColor = Nothing
        End If
        NotifyIcon1.Icon = icos(ipos)

        'Debug.WriteLine(p1)
    End Sub

End Class

