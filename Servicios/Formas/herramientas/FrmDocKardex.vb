Public Class FrmDocKardex
    Dim IdDocumento As Integer
    Dim Tipo As Byte
    Dim Folio As String
    Dim Clave As String
    Dim ColorRojo As Color = Color.FromArgb(255, 200, 200)
    Dim ColorMorado As Color = Color.FromArgb(240, 180, 240)
    Dim ColorAzul As Color = Color.FromArgb(200, 200, 255)
    Dim ColorAmarillo As Color = Color.FromArgb(255, 255, 192)
    Dim ColorVerde As Color = Color.FromArgb(200, 255, 200)
    Dim ColorNaranja As Color = Color.FromArgb(250, 250, 145)
    Dim ColorDocumento As Color = Color.FromArgb(250, 250, 145)
    Dim Colorremision As Color = Color.FromArgb(190, 210, 190)
    Dim Kardex As dbkardexdocumentos
    Public Sub New(pIdDocumento As Integer, pTipo As Byte, pFolio As String, pClave As String)

        ' This call is required by the designer.
        InitializeComponent()
        IdDocumento = pIdDocumento
        Tipo = pTipo
        Folio = pFolio
        Clave = pClave
        ' Add any initialization after the InitializeComponent() call.
        Kardex = New dbkardexdocumentos(MySqlcon)
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub DGServicios_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGServicios.CellContentClick

    End Sub

    Private Sub DGServicios_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGServicios.CellDoubleClick
        AbreDocumento()
    End Sub

    Private Sub DGServicios_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGServicios.CellFormatting
        On Error Resume Next
        If e.ColumnIndex = 5 Then
            e.Value = Format(e.Value, "#0.00")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
        'If e.ColumnIndex = 5 Then
        '    e.Value = Format(e.Value, "00000")
        'End If
        'If e.ColumnIndex = 4 Then
        '    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'End If
        Select Case DGServicios.Item(1, e.RowIndex).Value
            Case 4
                e.CellStyle.BackColor = ColorVerde
            Case 1 'dev
                e.CellStyle.BackColor = ColorAmarillo
                ' Case 3
                '    e.CellStyle.BackColor = ColorMorado
            Case 0 'pago
                e.CellStyle.BackColor = ColorAzul
            Case 3 'movimientos
                e.CellStyle.BackColor = ColorNaranja
            Case 2 'nota de credito
                e.CellStyle.BackColor = ColorDocumento
                'Case 7
                '   e.CellStyle.BackColor = ColorDocumento
                'Case 8
                '   e.CellStyle.BackColor = Colorremision
        End Select

    End Sub

    Private Sub FrmDocKardex_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Tipo = 0 Then
            Label2.Text = "Movimientos en la factura:"
            Me.Text = "Movimientos en la factura"
        End If
        If Tipo = 1 Then
            Label2.Text = "Movimientos en la remisión:"
            Me.Text = "Movimientos en la remisión"
        End If
        If Tipo = 2 Then
            Label2.Text = "Movimientos en la compra:"
            Me.Text = "Movimientos en la compra"
        End If
        Me.Show()
        Consulta()
    End Sub
    Private Sub Consulta()
        Try
            If Tipo = 0 Then
                DGServicios.DataSource = Kardex.MovimientosFactura(IdDocumento)
                DGServicios2.DataSource = Kardex.MovimientosInvFactura(IdDocumento)
            End If
            If Tipo = 1 Then
                DGServicios.DataSource = Kardex.MovimientosRemision(IdDocumento)
                DGServicios2.DataSource = Kardex.MovimientosInvRemision(IdDocumento)
            End If
            If Tipo = 2 Then
                DGServicios.DataSource = Kardex.MovimientosCompra(IdDocumento)
                DGServicios2.DataSource = Kardex.MovimientosInvCompra(IdDocumento)
            End If

            DGServicios.Columns(0).Visible = False
            DGServicios.Columns(1).Visible = False
            DGServicios.Columns(2).HeaderText = "Fecha"
            DGServicios.Columns(3).HeaderText = "Movimiento"
            DGServicios.Columns(4).HeaderText = "Folio"
            DGServicios.Columns(5).HeaderText = "Importe"
            DGServicios.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DGServicios.ClearSelection()
            'DGServicios.CurrentCell = Nothing
            DGServicios2.Columns(0).HeaderText = "Código"
            DGServicios2.Columns(1).HeaderText = "Descripción"
            DGServicios2.Columns(2).HeaderText = "Cantidad"
            DGServicios2.Columns(3).HeaderText = "Devoluciones"
            DGServicios2.Columns(4).HeaderText = "Salidas"
            DGServicios2.Columns(5).HeaderText = "Diferencia"
            DGServicios2.Columns(2).Width = 75
            DGServicios2.Columns(3).Width = 75
            DGServicios2.Columns(4).Width = 75
            DGServicios2.Columns(5).Width = 75
            DGServicios2.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DGServicios2.ClearSelection()
            'DGServicios2.CurrentCell = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub AbreDocumento()
        '0 pago
        '1 dev
        '2 n cr
        '3 mov
        Try
            Dim Row As Integer = DGServicios.CurrentRow.Index
            If Row >= 0 Then
                If Tipo = 0 Then
                    Select Case DGServicios.Item(1, Row).Value
                        Case 0
                            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PagosVer, PermisosN.Secciones.Ventas) = True Then
                                Dim Fac As New frmVentasPagos(Folio, Clave)
                                Fac.ShowDialog()
                                Fac.Dispose()
                            End If
                        Case 2
                            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.NotasdeCreditoVer, PermisosN.Secciones.Ventas) = True Then
                                Dim NC As New frmNotasdeCredito(DGServicios.Item(0, Row).Value)
                                NC.ShowDialog()
                                NC.Dispose()
                            End If
                        Case 1
                            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.DevolucionesVer, PermisosN.Secciones.Ventas) = True Then
                                Dim Dev As New frmDevoluciones(DGServicios.Item(0, Row).Value, 0, 0, "", 0)
                                Dev.ShowDialog()
                                Dev.Dispose()
                            End If

                        Case 3
                            If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosVer, PermisosN.Secciones.Inventario) = True Then
                                Dim Fac As New frmInventarioMovimientosN(DGServicios.Item(0, Row).Value)
                                Fac.ShowDialog()
                                Fac.Dispose()
                            End If
                    End Select
                End If


                If Tipo = 1 Then
                    Select Case DGServicios.Item(1, Row).Value
                        Case 0
                            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PagosRemVer, PermisosN.Secciones.Ventas) = True Then
                                Dim Fac As New frmVentasPagosRemisiones(Folio, 0, 0, Clave)
                                Fac.ShowDialog()
                                Fac.Dispose()
                            End If
                        Case 1
                            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.DevolucionesVer, PermisosN.Secciones.Ventas) = True Then
                                Dim Dev As New frmDevoluciones(DGServicios.Item(0, Row).Value, 0, 0, "", 0)
                                Dev.ShowDialog()
                                Dev.Dispose()
                            End If

                        Case 3
                            If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosVer, PermisosN.Secciones.Inventario) = True Then
                                Dim Fac As New frmInventarioMovimientosN(DGServicios.Item(0, Row).Value)
                                Fac.ShowDialog()
                                Fac.Dispose()
                            End If
                        Case 4
                            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasVer, PermisosN.Secciones.Ventas) = True Then
                                Dim Fac As New frmVentasN(DGServicios.Item(0, Row).Value, 0, 0, 0)
                                Fac.ShowDialog()
                                Fac.Dispose()
                            End If
                    End Select
                End If

                If Tipo = 2 Then
                    Select Case DGServicios.Item(1, Row).Value
                        Case 0
                            If GlobalPermisos.ChecaPermiso(PermisosN.Compras.PagosVer, PermisosN.Secciones.Compras) = True Then
                                Dim Fac As New frmComprasPagos(Folio, Clave)
                                Fac.ShowDialog()
                                Fac.Dispose()
                            End If
                        Case 2
                            If GlobalPermisos.ChecaPermiso(PermisosN.Compras.NotasdeCreditoVer, PermisosN.Secciones.Compras) = True Then
                                Dim NC As New frmNotasdeCreditoCompras(DGServicios.Item(0, Row).Value)
                                NC.ShowDialog()
                                NC.Dispose()
                            End If
                        Case 1
                            If GlobalPermisos.ChecaPermiso(PermisosN.Compras.DevolucionesVer, PermisosN.Secciones.Compras) = True Then
                                Dim Dev As New frmDevolucionesCompras(DGServicios.Item(0, Row).Value, 0, 0, "", 0)
                                Dev.ShowDialog()
                                Dev.Dispose()
                            End If

                    End Select
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        AbreDocumento()
    End Sub
    Private Sub DGServicios2_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGServicios2.CellFormatting
        If e.ColumnIndex >= 2 And e.ColumnIndex <= 5 Then
            e.Value = Format(e.Value, "#,###,##0.###")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub
End Class