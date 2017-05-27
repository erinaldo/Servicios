Imports MySql.Data.MySqlClient
Public Class frmNominaDatosN

    Private idTrabajador As Integer
    Private subcontratacion As New dbnominasubcontratacion(MySqlcon)
    Private nuevo As Boolean = True
    Private nuevoPago As Boolean = True
    Private nuevaSubcontratacion As Boolean = True

    Public Sub New(ByVal pidTrabajador As Integer)

        ' This call is required by the designer.
        InitializeComponent()
        Me.idTrabajador = pidTrabajador
    End Sub
    Private Sub frmNominaDatosN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        muestraSubcontrataciones()
    End Sub
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub
    Private Sub btnRfc_Click(sender As Object, e As EventArgs) Handles btnRfc.Click
        Try
            If txtRfc.Text <> "" And txtPorcentaje.Text <> "" Then
                If nuevaSubcontratacion Then
                    subcontratacion.agregar(idTrabajador, txtRfc.Text, CDbl(txtPorcentaje.Text))
                Else
                    subcontratacion.modificar(subcontratacion.idSubcontratacion, idTrabajador, txtRfc.Text, CDbl(txtPorcentaje.Text))
                End If
                nuevaSubcontratacion = True
                txtRfc.Text = ""
                txtPorcentaje.Text = ""
                muestraSubcontrataciones()
                txtRfc.Focus()
            Else
                MsgBox("Debe llenar ambos campos", MsgBoxStyle.Information, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        
    End Sub

    Private Sub muestraSubcontrataciones()
        dgvRFC.DataSource = subcontratacion.vista(idTrabajador)
        dgvRFC.Columns(0).Visible = False
        dgvRFC.Columns(1).HeaderText = "RFC"
        dgvRFC.Columns(2).HeaderText = "%"
        dgvRFC.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
    End Sub
    Private Sub dgvRFC_Click(sender As Object, e As EventArgs) Handles dgvRFC.Click
        Try
            Dim id As Integer = CInt(dgvRFC.CurrentRow.Cells(0).Value.ToString)
            nuevaSubcontratacion = False
            muestraRFC(id)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub muestraRFC(ByVal id As Integer)
        subcontratacion.buscar(id)
        txtRfc.Text = subcontratacion.rfclaboral
        txtPorcentaje.Text = subcontratacion.porcentaje
        txtRfc.Focus()
    End Sub

    Private Sub dgvRFC_DoubleClick(sender As Object, e As EventArgs) Handles dgvRFC.DoubleClick
        Try
            If MsgBox("¿Desea eliminar este RFC?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim id As Integer = dgvRFC.CurrentRow.Cells(0).Value
                subcontratacion.eliminar(id)
                nuevaSubcontratacion = True
                txtRfc.Text = ""
                txtPorcentaje.Text = ""
                txtRfc.Focus()
                muestraSubcontrataciones()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        nuevaSubcontratacion = True
        txtRfc.Text = ""
        txtPorcentaje.Text = ""
        txtRfc.Focus()
    End Sub
End Class