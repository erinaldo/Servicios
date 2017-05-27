Imports System
Imports System.Threading
Imports System.Net.Sockets
Imports System.IO
Imports System.Text
Public Class frmRestauranteTeclado
    Private shift As Boolean = False
    Private proceso As String
    Dim procID As Integer
    Private cliente As Socket
    Private datos(500) As Byte
    Private cajaTexto As TextBox
    Private configuracion As New dbRestauranteConfiguracion(1, MySqlcon)


    Public Sub New(ByRef cajaTexto As TextBox)

        InitializeComponent()
        Me.cajaTexto = cajaTexto
        Visible = False
        'Me.Location = New Point(Me.MdiParent.Width / 4, Me.MdiParent.Height / 2)
        'Me.Location = New Point(Me.Parent.Width / 4, Me.Parent.Height / 2)
        'Me.proceso = proceso
        'cliente = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        'cliente.Connect("localhost", 9000)
    End Sub

    Private Sub activaApp()
        procID = Shell(proceso, AppWinStyle.NormalFocus)
        AppActivate(procID)
    End Sub
    Private Sub frmRestauranteTeclado_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = Color.FromArgb(configuracion.colorVentanas)
        Me.Location = New Point(Me.MdiParent.Width / 4, Me.MdiParent.Height / 2)
        tecladoActivo = True
        Visible = True
    End Sub

    Private Sub Button40_Click(sender As Object, e As EventArgs) Handles btnShift.Click
        If shift = False Then
            shift = True
            cambiaLetras()
        Else
            shift = False
            cambiaLetras()
        End If
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles btnQ.Click, btnA.Click, btnB.Click, btnC.Click, btnD.Click, btnE.Click, btnF.Click, btnG.Click, btnH.Click, btnI.Click, btnJ.Click, btnK.Click, btnL.Click, btnM.Click, btnN.Click, btnNh.Click, btnO.Click, btnP.Click, btnQ.Click, btnR.Click, btnS.Click, btnT.Click, btnU.Click, btnV.Click, btnW.Click, btnX.Click, btnY.Click, btnZ.Click, btn0.Click, btn1.Click, btn2.Click, btn3.Click, btn4.Click, btn5.Click, btn6.Click, btn7.Click, btn8.Click, btn9.Click
        'activaApp()
        Dim b As Button = DirectCast(sender, Button)
        cajaTexto.Text = b.Text
    End Sub

    'Private Sub Button21_Click(sender As Object, e As EventArgs) Handles btnW.Click
    '    If shift Then
    '        cajaTexto.Text += "W"

    '    Else
    '        cajaTexto.Text += "w"

    '    End If
    'End Sub

    'Private Sub Button13_Click(sender As Object, e As EventArgs) Handles btnE.Click
    '    If shift Then
    '        cajaTexto.Text += "E"

    '    Else
    '        cajaTexto.Text += "e"

    '    End If
    'End Sub

    'Private Sub Button14_Click(sender As Object, e As EventArgs) Handles btnR.Click
    '    If shift Then
    '        cajaTexto.Text += "R"

    '    Else
    '        cajaTexto.Text += "r"

    '    End If
    'End Sub

    'Private Sub Button15_Click(sender As Object, e As EventArgs) Handles btnT.Click
    '    If shift Then
    '        My.Computer.Keyboard.SendKeys("T", True)
    '    Else
    '        My.Computer.Keyboard.SendKeys("t", True)
    '    End If
    'End Sub

    'Private Sub Button16_Click(sender As Object, e As EventArgs) Handles btnY.Click
    '    If shift Then
    '        cajaTexto.Text += "Y"

    '    Else
    '        cajaTexto.Text += "y"

    '    End If
    'End Sub

    'Private Sub Button17_Click(sender As Object, e As EventArgs) Handles btnU.Click
    '    If shift Then
    '        cajaTexto.Text += "U"

    '    Else
    '        cajaTexto.Text += "u"

    '    End If
    'End Sub

    'Private Sub Button19_Click(sender As Object, e As EventArgs) Handles btnI.Click
    '    If shift Then
    '        cajaTexto.Text += "I"

    '    Else
    '        cajaTexto.Text += "i"

    '    End If
    'End Sub

    'Private Sub Button18_Click(sender As Object, e As EventArgs) Handles btnO.Click
    '    If shift Then
    '        cajaTexto.Text += "O"

    '    Else
    '        cajaTexto.Text += "o"

    '    End If
    'End Sub

    'Private Sub Button20_Click(sender As Object, e As EventArgs) Handles btnP.Click
    '    If shift Then
    '        cajaTexto.Text += "P"

    '    Else
    '        cajaTexto.Text += "p"

    '    End If
    'End Sub

    'Private Sub Button23_Click(sender As Object, e As EventArgs) Handles btnA.Click
    '    If shift Then
    '        cajaTexto.Text += "A"

    '    Else
    '        cajaTexto.Text += "a"

    '    End If
    'End Sub

    'Private Sub Button22_Click(sender As Object, e As EventArgs) Handles btnS.Click
    '    If shift Then
    '        cajaTexto.Text += "S"

    '    Else
    '        cajaTexto.Text += "s"

    '    End If
    'End Sub

    'Private Sub Button24_Click(sender As Object, e As EventArgs) Handles btnD.Click
    '    If shift Then
    '        cajaTexto.Text += "D"

    '    Else
    '        cajaTexto.Text += "d"

    '    End If
    'End Sub

    'Private Sub Button25_Click(sender As Object, e As EventArgs) Handles btnF.Click
    '    If shift Then
    '        cajaTexto.Text += "F"

    '    Else
    '        cajaTexto.Text += "f"

    '    End If
    'End Sub

    'Private Sub Button26_Click(sender As Object, e As EventArgs) Handles btnG.Click
    '    If shift Then
    '        cajaTexto.Text += "G"

    '    Else
    '        cajaTexto.Text += "g"

    '    End If
    'End Sub

    'Private Sub Button27_Click(sender As Object, e As EventArgs) Handles btnH.Click
    '    If shift Then
    '        cajaTexto.Text += "H"

    '    Else
    '        cajaTexto.Text += "h"

    '    End If
    'End Sub

    'Private Sub Button28_Click(sender As Object, e As EventArgs) Handles btnJ.Click
    '    If shift Then
    '        cajaTexto.Text += "J"

    '    Else
    '        cajaTexto.Text += "j"

    '    End If
    'End Sub

    'Private Sub Button29_Click(sender As Object, e As EventArgs) Handles btnK.Click
    '    If shift Then
    '        cajaTexto.Text += "K"

    '    Else
    '        cajaTexto.Text += "k"

    '    End If
    'End Sub

    'Private Sub Button31_Click(sender As Object, e As EventArgs) Handles btnL.Click
    '    If shift Then
    '        cajaTexto.Text += "L"

    '    Else
    '        cajaTexto.Text += "l"

    '    End If
    'End Sub

    'Private Sub Button30_Click(sender As Object, e As EventArgs) Handles btnNh.Click
    '    If shift Then
    '        cajaTexto.Text += "Ñ"

    '    Else
    '        cajaTexto.Text += "ñ"

    '    End If
    'End Sub

    'Private Sub Button33_Click(sender As Object, e As EventArgs) Handles btnZ.Click
    '    If shift Then
    '        cajaTexto.Text += "Z"

    '    Else
    '        cajaTexto.Text += "z"

    '    End If
    'End Sub

    'Private Sub Button34_Click(sender As Object, e As EventArgs) Handles btnX.Click
    '    If shift Then
    '        cajaTexto.Text += "X"

    '    Else
    '        cajaTexto.Text += "x"

    '    End If
    'End Sub

    'Private Sub Button32_Click(sender As Object, e As EventArgs) Handles btnC.Click
    '    If shift Then
    '        cajaTexto.Text += "C"

    '    Else
    '        cajaTexto.Text += "c"

    '    End If
    'End Sub

    'Private Sub Button35_Click(sender As Object, e As EventArgs) Handles btnV.Click
    '    If shift Then
    '        cajaTexto.Text += "V"

    '    Else
    '        cajaTexto.Text += "v"

    '    End If
    'End Sub

    'Private Sub Button36_Click(sender As Object, e As EventArgs) Handles btnB.Click
    '    If shift Then
    '        cajaTexto.Text += "B"

    '    Else
    '        cajaTexto.Text += "b"

    '    End If
    'End Sub

    'Private Sub Button38_Click(sender As Object, e As EventArgs) Handles btnN.Click
    '    If shift Then
    '        cajaTexto.Text += "N"

    '    Else
    '        cajaTexto.Text += "n"

    '    End If
    'End Sub

    'Private Sub Button37_Click(sender As Object, e As EventArgs) Handles btnM.Click
    '    If shift Then
    '        cajaTexto.Text += "M"

    '    Else
    '        cajaTexto.Text += "m"

    '    End If
    'End Sub

    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btn1.Click
    '    cajaTexto.Text += "1"
    'End Sub

    'Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btn2.Click
    '    cajaTexto.Text += "2"
    'End Sub

    'Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btn3.Click
    '    cajaTexto.Text += "3"
    'End Sub

    'Private Sub Button4_Click(sender As Object, e As EventArgs) Handles btn4.Click
    '    cajaTexto.Text += "4"
    'End Sub

    'Private Sub Button5_Click(sender As Object, e As EventArgs) Handles btn5.Click
    '    cajaTexto.Text += "5"
    'End Sub

    'Private Sub Button6_Click(sender As Object, e As EventArgs) Handles btn6.Click
    '    cajaTexto.Text += "6"
    'End Sub

    'Private Sub Button7_Click(sender As Object, e As EventArgs) Handles btn7.Click
    '    cajaTexto.Text += "7"
    'End Sub

    'Private Sub Button8_Click(sender As Object, e As EventArgs) Handles btn8.Click
    '    cajaTexto.Text += "8"
    'End Sub

    'Private Sub Button9_Click(sender As Object, e As EventArgs) Handles btn9.Click
    '    cajaTexto.Text += "9"
    'End Sub

    'Private Sub Button10_Click(sender As Object, e As EventArgs) Handles btn0.Click
    '    cajaTexto.Text += "0"
    'End Sub

    Private Sub Button39_Click(sender As Object, e As EventArgs) Handles btnEspacio.Click
        cajaTexto.Text += " "
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim c As Char
        c = "8"
        Dim s As String = cajaTexto.Text
        's = s.Substring(0, s.Length - 1)
        'cajaTexto.Text = s
        cajaTexto.Text += c
    End Sub

    Private Sub cambiaLetras()
        If shift = False Then
            For Each c As Control In Me.Controls
                c.Text = c.Text.ToUpper
            Next
            shift = True
        Else
            For Each c As Control In Me.Controls
                c.Text = c.Text.ToLower
            Next
            shift = False
        End If

    End Sub

    Private Sub btnEnter_Click(sender As Object, e As EventArgs) Handles btnEnter.Click
        cajaTexto.Text += Keys.Enter
    End Sub

    Private Sub frmRestauranteTeclado_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        tecladoActivo = False
    End Sub

    Private Sub btnShift2_Click(sender As Object, e As EventArgs) Handles btnShift2.Click

    End Sub
End Class