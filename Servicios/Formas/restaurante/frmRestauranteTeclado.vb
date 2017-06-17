Imports System
Imports System.Threading
Imports System.Net.Sockets
Imports System.IO
Imports System.Text
Public NotInheritable Class frmRestauranteTeclado
    Private shift As Boolean = False
    Private proceso As String
    Dim procID As Integer
    Private cliente As Socket
    Private datos(500) As Byte
    Private cajaTexto As TextBox
    Private configuracion As New dbRestauranteConfiguracion(1, MySqlcon)
    Private Shared _Instancia As frmRestauranteTeclado
    Public Shared Function Instanciar(txtPass) As frmRestauranteTeclado
        If _Instancia Is Nothing Then _Instancia = New frmRestauranteTeclado(txtPass)
        Return _Instancia
    End Function

    Private Sub New(ByRef cajaTexto As TextBox)

        InitializeComponent()
        Me.cajaTexto = cajaTexto
        Visible = False
        Me.Location = New Point(Screen.PrimaryScreen.Bounds.Width / 2 - Me.Width / 2, Screen.PrimaryScreen.Bounds.Height - Me.Height)
        
    End Sub

    Private Sub activaApp()
        procID = Shell(proceso, AppWinStyle.NormalFocus)
        AppActivate(procID)
    End Sub
    Private Sub frmRestauranteTeclado_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = Color.FromArgb(configuracion.colorVentanas)
        'Me.Location = New Point(Me.MdiParent.Width / 4, Me.MdiParent.Height / 2)
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

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles btnA.Click, btnB.Click, btnC.Click, btnD.Click, btnE.Click, btnF.Click, btnG.Click, btnH.Click, btnI.Click, btnJ.Click, btnK.Click, btnL.Click, btnM.Click, btnN.Click, btnNh.Click, btnO.Click, btnP.Click, btnQ.Click, btnR.Click, btnS.Click, btnT.Click, btnU.Click, btnV.Click, btnW.Click, btnX.Click, btnY.Click, btnZ.Click, btn0.Click, btn1.Click, btn2.Click, btn3.Click, btn4.Click, btn5.Click, btn6.Click, btn7.Click, btn8.Click, btn9.Click
        'activaApp()
        Dim b As Button = DirectCast(sender, Button)
        cajaTexto.Text += b.Text
    End Sub

    

    Private Sub Button39_Click(sender As Object, e As EventArgs) Handles btnEspacio.Click
        cajaTexto.Text += " "
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If cajaTexto.Text > 0 Then
            cajaTexto.Text = cajaTexto.Text.Substring(0, cajaTexto.Text.Length - 1)
        End If
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
        cajaTexto.FindForm.AcceptButton.PerformClick()
    End Sub

    Private Sub frmRestauranteTeclado_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        _Instancia = Nothing
    End Sub

End Class