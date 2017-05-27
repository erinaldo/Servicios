Public Class frmDevolucionesElegir
    Dim idDevolucion As Integer
    Dim Tabla As New DataTable
    Dim checked As Boolean
    Public IDs As String = ""
    Public Sub New(ByVal pidDevolucion As Integer)
        InitializeComponent()
        idDevolucion = pidDevolucion

    End Sub
    Private Sub frmDevolucionesElegir_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim I As Integer = 0
        Dim S As String = ""
        Dim D As Double = 0
        Tabla.Columns.Add("Id", I.GetType)
        Tabla.Columns.Add("TipoR", S.GetType)
        Tabla.Columns.Add("Extra", S.GetType)
        Tabla.Columns.Add("Cantidad", D.GetType)
        Tabla.Columns.Add("Uni.", S.GetType)
        Tabla.Columns.Add("Cantidad M.", S.GetType)
        Tabla.Columns.Add("Uni. M.", S.GetType)
        Tabla.Columns.Add("Código", S.GetType)
        Tabla.Columns.Add("Descripción", S.GetType)
        Tabla.Columns.Add("Precio U.", D.GetType)
        Tabla.Columns.Add("Importe", D.GetType)
        Tabla.Columns.Add("Moneda", S.GetType)

        Dim T As MySql.Data.MySqlClient.MySqlDataReader
        Dim CD As New dbDevolucionesDetalles(MySqlcon)
        T = CD.ConsultaReader(idDevolucion)
        While T.Read
            If T("idinventario") > 1 Then
                Tabla.Rows.Add(T("iddetalle"), "A", "", Format(T("cantidad"), "0.00"), T("tipocantidad"), Format(T("cantidadm"), "0.00"), T("tipocantidad2"), T("clave"), T("descripcion"), Format(T("precio") / T("cantidad"), "0.00"), Format(T("precio"), "0.00"), T("abreviatura"))
            Else
                Tabla.Rows.Add(T("iddetalle"), "P", "", Format(T("cantidad"), "0.00"), T("tipocantidad"), Format(T("cantidadm"), "0.00"), T("tipocantidad2"), T("pclave"), T("descripcion"), Format(T("precio") / T("cantidad"), "0.00"), Format(T("precio"), "0.00"), T("abreviatura"))
            End If
        End While
        T.Close()

        DGDetalles.DataSource = Tabla
        DGDetalles.Columns(3).Visible = False
        DGDetalles.Columns(1).Visible = False
        DGDetalles.Columns(2).Visible = False
        DGDetalles.Columns(4).ReadOnly = True
        DGDetalles.Columns(6).ReadOnly = True
        DGDetalles.Columns(7).ReadOnly = True
        DGDetalles.Columns(8).ReadOnly = True
        DGDetalles.Columns(9).ReadOnly = True
        DGDetalles.Columns(10).ReadOnly = True
        DGDetalles.Columns(11).ReadOnly = True
        DGDetalles.Columns(8).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGDetalles.Columns(4).Width = 45
        DGDetalles.Columns(6).Width = 65
        DGDetalles.Columns(11).Width = 50
        DGDetalles.Columns(0).HeaderText = " "
        checked = False
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        'AGREGAR SELECCIONADOS
        Dim nfilas As Integer
        nfilas = DGDetalles.RowCount() - 1


        For i As Integer = 0 To nfilas
            If DGDetalles.Rows(i).Cells(0).Value = 0 Then

                'Que devuleva los IDS que va a eliminar
                IDs = IDs + DGDetalles.Rows(i).Cells(1).Value.ToString() + ","
         


            End If
        Next





        DialogResult = Windows.Forms.DialogResult.OK


    End Sub

    Private Sub DGDetalles_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellClick
        If e.RowIndex = -1 Then
            'btnAgregar.Enabled = False
            'btnAgregarTodos.Enabled = False
            'btnQuitar.Enabled = False
            'Button2.Enabled = False
            If e.ColumnIndex = 0 Then
                Dim nfilas As Integer
                nfilas = DGDetalles.RowCount()


                If checked = True Then
                  
                    For i As Integer = 0 To nfilas - 1
                        DGDetalles.Rows(i).Cells(0).Value = 0
                    Next
                    DGDetalles.Columns(0).HeaderText = " "
                    checked = False

                Else
                    For i As Integer = 0 To nfilas - 1
                        DGDetalles.Rows(i).Cells(0).Value = 1

                    Next
                    DGDetalles.Columns(0).HeaderText = "X"
                    checked = True
                End If

            End If

            'btnAgregar.Enabled = True
            'btnAgregarTodos.Enabled = True
            'btnQuitar.Enabled = True
            'Button2.Enabled = True
        End If
        If e.ColumnIndex = 0 And e.RowIndex > -1 Then
            If DGDetalles.Rows(e.RowIndex).Cells(0).Value = 1 Then

                DGDetalles.Rows(e.RowIndex).Cells(0).Value = 0

            ElseIf DGDetalles.Rows(e.RowIndex).Cells(0).Value = 0 Then

                DGDetalles.Rows(e.RowIndex).Cells(0).Value = 1

            End If
        End If
    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class