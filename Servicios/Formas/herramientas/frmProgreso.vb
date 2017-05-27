Public Class frmProgreso
    Public maximo As Integer
    Public minimo As Integer
    Public value As Integer
    Public Sub New(ByVal max As Integer, ByVal min As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        maximo = max
        minimo = min
        value = 0
        ProgressBar1.Maximum = maximo
        ProgressBar1.Minimum = minimo
        ProgressBar1.Value = value
    End Sub

    Private Sub Progreso_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        'ProgressBar1.Maximum = maximo
        'ProgressBar1.Minimum = minimo
        'ProgressBar1.Value = value
        ' Label1.Text = "0 %"
    End Sub
    Public Sub incrementarMaximo(ByVal pMaximo As Integer)
        'PMaximo es el aumento a lo que ya tenia
        ProgressBar1.Maximum = maximo + pMaximo

    End Sub
    Public Sub Aumentar(ByVal valor As Integer)
        ProgressBar1.Value = valor
        ' Label1.Text = ((ProgressBar1.Value * 100) / ProgressBar1.Maximum).ToString("0") + " %"
        If valor = maximo Then
            Me.Close()
        End If
    End Sub

End Class