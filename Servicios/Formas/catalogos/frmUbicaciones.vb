﻿Public Class frmUbicaciones
    Private idalmacen As Integer
    Public Sub New(idalmacen As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.idalmacen = idalmacen
    End Sub

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        If MsgBox("¿Generar ubicaciones?", vbOKCancel) = Windows.Forms.DialogResult.OK Then
            Dim db As New dbAlmacenes(idalmacen, MySqlcon)
            For Each r1 As DataGridViewRow In dgvDimension1.Rows
                If r1.Cells(0).Value IsNot Nothing Then
                    For Each r2 As DataGridViewRow In dgvDimension2.Rows
                        If r2.Cells(0).Value IsNot Nothing Then
                            For Each r3 As DataGridViewRow In dgvDimension3.Rows
                                If r3.Cells(0).Value IsNot Nothing Then
                                    db.AgregarUbicacion(idalmacen, String.Concat(r1.Cells(0).Value, r2.Cells(0).Value, +r3.Cells(0).Value))
                                End If
                            Next
                        End If
                    Next
                End If
            Next
            MsgBox("Ubicaciones generadas.")
        End If
    End Sub
End Class