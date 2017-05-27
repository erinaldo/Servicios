Public Class frmDevolucionesElegirC
    Dim idDevolucion As Integer
    Dim Tabla As New DataTable
    Dim checked As Boolean
    Public IDs As String = ""

    Public Sub New(ByVal pidDevolucion As Integer)

        InitializeComponent()

        idDevolucion = pidDevolucion

    End Sub
    Private Sub frmDevolucionesElegirC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            Dim I As Integer = 0
            Dim S As String = ""
            Dim D As Double = 0
            Tabla.Columns.Add("Id", I.GetType)
            Tabla.Columns.Add("TipoR", S.GetType)
            Tabla.Columns.Add("Extra", S.GetType)
            Tabla.Columns.Add("Cantidad", D.GetType)
            Tabla.Columns.Add("Código", S.GetType)
            Tabla.Columns.Add("Descripción", S.GetType)
            Tabla.Columns.Add("Precio U.", S.GetType)
            Tabla.Columns.Add("Importe", S.GetType)
            Tabla.Columns.Add("Moneda", S.GetType)

            Tabla.Rows.Clear()
            Dim T As MySql.Data.MySqlClient.MySqlDataReader
            Dim CD As New dbDevolucionesComprasDetalles(MySqlcon)
            T = CD.ConsultaReader(idDevolucion)
            While T.Read
                If T("cantidad") <> 0 Then

                    Tabla.Rows.Add(T("iddetalle"), "A", "", T("cantidad"), T("clave"), T("descripcion"), Format(T("precio") / T("cantidad"), "0.00"), Format(T("precio"), "0.00"), T("abreviatura"))

                Else
                    Tabla.Rows.Add(T("iddetalle"), "A", "", T("cantidad"), T("clave"), T("descripcion"), "0.00", Format(T("precio"), "0.00"), T("abreviatura"))
                End If
            End While
            T.Close()

            DGDetalles.DataSource = Tabla
            DGDetalles.Columns(1).Visible = False
            DGDetalles.Columns(2).Visible = False
            DGDetalles.Columns("Extra").Visible = False
            'DGDetalles.Columns(5).ReadOnly = True
            'DGDetalles.Columns(6).ReadOnly = True
            'DGDetalles.Columns(7).ReadOnly = True
            'DGDetalles.Columns(8).ReadOnly = True
            'DGDetalles.Columns(9).ReadOnly = True
            DGDetalles.Columns(6).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            'DGDetalles.Columns(5).Width = 80
            'DGDetalles.Columns(9).Width = 80
            ' DGDetalles.Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
            ' DGDetalles.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
            ' DGDetalles.Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
            'DGDetalles.Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
            'DGDetalles.Columns(5).SortMode = DataGridViewColumnSortMode.NotSortable
            'DGDetalles.Columns(6).SortMode = DataGridViewColumnSortMode.NotSortable
            'DGDetalles.Columns(7).SortMode = DataGridViewColumnSortMode.NotSortable
            'DGDetalles.Columns(8).SortMode = DataGridViewColumnSortMode.NotSortable
            'DGDetalles.Columns(9).SortMode = DataGridViewColumnSortMode.NotSortable

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim nfilas As Integer
        nfilas = DGDetalles.RowCount() - 1


        For i As Integer = 0 To nfilas
            If DGDetalles.Rows(i).Cells(0).Value = 0 Then

                'Que devuleva los IDS que va a eliminar
                IDs = IDs + DGDetalles.Rows(i).Cells("Id").Value.ToString() + ","



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