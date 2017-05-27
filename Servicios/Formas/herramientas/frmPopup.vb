Public Class frmPopup
    Inherits System.Windows.Forms.Form
    Dim Tiempo As Integer
    Public TiempoMostrando As Integer
    Public TextoAMostrar As String
    Dim Desvanece As Boolean
    Dim Apareciendo As Boolean
    Dim Fuente As New Font("Arial", 18, FontStyle.Bold)
    Dim Brocha As New SolidBrush(Color.Black)
    Dim Rec As Rectangle
#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        '
        'Timer1
        '
        Me.Timer1.Interval = 16
        '
        'frmPopup
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(10, 22)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(224, Byte), CType(224, Byte), CType(224, Byte))
        Me.ClientSize = New System.Drawing.Size(292, 52)
        Me.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmPopup"
        Me.Text = "frmPopup"
        Me.TransparencyKey = System.Drawing.Color.FromArgb(CType(224, Byte), CType(224, Byte), CType(224, Byte))

    End Sub

#End Region

    Private Sub frmPopup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Me.Width = TextoAMostrar.Length * 15 + 25

        Rec = New Rectangle(0, 0, Me.Width, Me.Height)
        Tiempo = 0
        Desvanece = False
        Apareciendo = True
        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Tiempo += 1
        If Apareciendo Then
            If Me.Opacity < 1 Then
                Me.Opacity += 0.1
            Else
                Apareciendo = False
            End If
            Me.Refresh()
        End If
        If Tiempo >= TiempoMostrando And Apareciendo = False Then
            Desvanece = True
        End If
        If Desvanece Then
            Me.Opacity -= 0.1
            Me.Refresh()
            If Me.Opacity <= 0 Then Me.Close()
        End If
    End Sub

    Private Sub frmPopup_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        Try
            e.Graphics.DrawImage(ImagenPopUp, Rec)
            e.Graphics.DrawString(TextoAMostrar, Fuente, Brocha, 10, 10)
        Catch ex As Exception

        End Try
    End Sub
End Class
