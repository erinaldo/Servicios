Public Class frmInput
    Public Enum TipoDatos As Integer
        Texto = 1
        Numeros = 2
    End Enum
    Dim TipodeDato As Integer
    Public Valor As String
    Dim EsObligatorio As Boolean
   
    Public Sub New(ByVal Titulo As String, ByVal TipodeDatos As TipoDatos, Optional ByVal Obligatorio As Boolean = False, Optional ByVal EsPassword As Boolean = False, Optional ByVal Maxsize As Integer = 0, Optional DefaultText As String = "")
        InitializeComponent()
        TipodeDato = TipodeDatos
        Label1.Text = Titulo
        EsObligatorio = Obligatorio
        If EsPassword Then
            TextBox1.PasswordChar = "*"
        End If
        If Maxsize > 0 Then
            TextBox1.MaxLength = Maxsize
        End If
        TextBox1.Text = DefaultText
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If EsObligatorio Then
            If TextBox1.Text = "" Then
                MsgBox("Debe indicar un valor", MsgBoxStyle.OkOnly, "Introducir Datos")
            Else
                If TipodeDato = 1 Then
                    Davalor()
                Else
                    If IsNumeric(TextBox1.Text) Then
                        Davalor()
                    Else
                        MsgBox("Debe indicar un número", MsgBoxStyle.OkOnly, "Introducir Datos")
                    End If
                End If

            End If
        Else
            If TipodeDato = 1 Then
                Davalor()
            Else
                If IsNumeric(TextBox1.Text) Then
                    Davalor()
                Else
                    MsgBox("Debe indicar un número", MsgBoxStyle.OkOnly, "Introducir Datos")
                End If
            End If
        End If

    End Sub
    Private Sub Davalor()
        Valor = TextBox1.Text
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
    Private Sub frmInput_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class