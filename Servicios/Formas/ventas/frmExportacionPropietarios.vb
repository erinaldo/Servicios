Public Class frmExportacionPropietarios
    Dim propietarios As dbComplementoPropietarios
    Public complemento As String


    Public Sub New(ByVal idcomplemento As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        complemento = idcomplemento

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim frm As New frmExportacionPaises()
        frm.ShowDialog()
        Dim codigo = frm.codigo
        If codigo = "" Then
            MsgBox("Debe seleccionar un país o introducir el código.")
            txt_clave.BackColor = Color.Red
        Else
            txt_clave.BackColor = Color.White
            txt_clave.Text = codigo
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btn_cancelar.Click
        Me.Close()
    End Sub

    Private Sub frmExportacionPropietarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception
        End Try
        propietarios = New dbComplementoPropietarios(MySqlcon)
        dgv_propietarios.DataSource = propietarios.lista(complemento)
        dgv_propietarios.Columns(0).Width = 200
        dgv_propietarios.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        propietarios.guardar(txt_numId.Text, txt_clave.Text, complemento)
        dgv_propietarios.DataSource = propietarios.lista(complemento)
        dgv_propietarios.Columns(0).Width = 200
        dgv_propietarios.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Public Function validador() As Boolean
        Dim respuesta As Boolean = False
        If txt_numId.Text <> "" Then
            If txt_clave.Text <> "" Then
                respuesta = True
            End If
        End If
        Return respuesta
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If propietarios.eliminar(complemento) Then
            MsgBox("Propietarios eliminados.")
        End If
    End Sub
End Class