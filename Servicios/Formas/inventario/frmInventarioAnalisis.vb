Public Class frmInventarioAnalisis

    Public IdCompra As Integer
    Dim IdInventario As Integer
    Dim IdAlmacenes As New elemento
    Dim ConsultaOn As Boolean = True
    Dim Modo As Integer
    Dim ColorRojo As Color = Color.FromArgb(255, 200, 200)
    Dim ColorMorado As Color = Color.FromArgb(240, 180, 240)
    Dim ColorAzul As Color = Color.FromArgb(200, 200, 255)
    Dim ColorAmarillo As Color = Color.FromArgb(255, 255, 192)
    Dim ColorVerde As Color = Color.FromArgb(200, 255, 200)
    Dim ColorNaranja As Color = Color.FromArgb(250, 250, 145)
    Dim IdsSucursales As New elemento
    Dim Ids As New Collection
    Public Sub New(ByVal pIdInventario As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        IdInventario = pIdInventario

    End Sub
    Private Sub frmComprasConsulta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ConsultaOn = False
        LlenaCombos("tblsucursales", ComboBox1, "nombre", "nombret", "idsucursal", IdsSucursales, , "Todas")
        'LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdAlmacenes, "idalmacen<>1", "Todos")
        DateTimePicker2.Value = Date.Now
        DateTimePicker3.Value = Date.Now
        'chkTiempoReal.Checked = ConsultaTiempoRealG
        ConsultaOn = True
        If IdInventario <> 0 Then
        End If
    End Sub
    Private Sub Consulta()
        Try
            'If ConsultaOn Then
            ' If IdInventario <> 0 Then
            Dim Fu As New Font("Arial", 8)
            Dim S As New dbInventario(MySqlcon)
            Dim DR As MySql.Data.MySqlClient.MySqlDataReader
            DR = S.ConsultaIds(0, 0, 0, IdInventario, True)
            Ids.Clear()
            DGServicios.Rows.Clear()
            While DR.Read
                Ids.Add(New NodoImpresionN(DR("idinventario"), 0, 0, 0, 0, DR("nombre"), "", Fu, NodoImpresionN.Alineaciones.Izquierda, 0, 0, 0, 0, 0, 0, 0, DR("clave"), 0, 0))
            End While
            DR.Close()
            Dim FechaActual As String
            Dim Cantidad As Double
            Label4.Text = "Procesando 1 de " + Ids.Count.ToString
            ProgressBar1.Maximum = Ids.Count
            ProgressBar1.Value = ProgressBar1.Minimum
            ConsultaOn = True
            Button3.Enabled = False
            Button1.Enabled = True
            Application.DoEvents()
            For Each N As NodoImpresionN In Ids
                FechaActual = Format(DateTimePicker2.Value, "yyyy/MM/dd")
                While FechaActual <= Format(DateTimePicker3.Value, "yyyy/MM/dd")
                    Cantidad = S.DaInventarioAFecha(N.id, FechaActual, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdAlmacenes.Valor(ComboBox8.SelectedIndex))
                    If Cantidad < 0 Then
                        DGServicios.Rows.Add(N.id, N.Nombre, N.Texto, FechaActual, Cantidad)
                    End If
                    Label4.Text = "Procesando " + ProgressBar1.Value.ToString + " de " + Ids.Count.ToString + " Fecha: " + FechaActual
                    FechaActual = Format(DateAdd(DateInterval.Day, 1, CDate(FechaActual)), "yyyy/MM/dd")
                    Application.DoEvents()
                    If ConsultaOn = False Then Exit While
                End While
                'Label4.Text = "Procesando " + ProgressBar1.Value.ToString + " de " + Ids.Count.ToString
                ProgressBar1.Value += 1
                Application.DoEvents()
                If ConsultaOn = False Then Exit For
            Next
            Label4.Text = "Proceso terminado."
            If ConsultaOn = False Then Label4.Text = "Proceso detenido."
            Button3.Enabled = True
            Button1.Enabled = False
            'End If
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub CalculaSaldos(ByVal pSaldoInicial As Double)
        Dim C As Integer = 0
        Dim Cc As New dbClientes(MySqlcon)
        pSaldoInicial = Cc.DaSaldoAFecha(IdInventario, Format(DateTimePicker2.Value, "yyyy/MM/dd"))
        While C < DGServicios.RowCount
            pSaldoInicial = pSaldoInicial + DGServicios.Item(6, C).Value - DGServicios.Item(7, C).Value
            DGServicios.Item(8, C).Value = pSaldoInicial
            C += 1
        End While

    End Sub

    
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim op As New dbOpciones(MySqlcon)
        If op.BusquedaporClases = 0 Then
            Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Articulo, 0, False, False, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                TextBox2.Text = B.Inventario.Nombre
                IdInventario = B.Inventario.ID
                ConsultaOn = False
                TextBox1.Text = B.Inventario.Clave
                ConsultaOn = True
            End If
            B.Dispose()
        Else
            Dim B As New frmBuscadorClases(frmBuscador.TipoDeBusqueda.Articulo, 0, False, False, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                TextBox2.Text = B.Inventario.Nombre
                IdInventario = B.Inventario.ID
                ConsultaOn = False
                TextBox1.Text = B.Inventario.Clave
                ConsultaOn = True
            End If
            B.Dispose()
        End If
    End Sub
    Private Sub BuscaInventario()
        Try
            If ConsultaOn Then
                Dim c As New dbInventario(MySqlcon)
                If c.BuscaArticulo(TextBox1.Text, 1) Then
                    TextBox2.Text = c.Nombre
                    IdInventario = c.ID
                    'Consulta()
                Else
                    IdInventario = 0
                    TextBox2.Text = ""
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        BuscaInventario()
    End Sub

   


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub



    Private Sub DGServicios_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellContentClick

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'If IdInventario <> 0 Then
        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        'Dim C As New dbClientes(MySqlcon)
        'Dim O As New dbOpciones(MySqlcon)
        Dim Su As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        Rep = New repInventarioMovimientos
        Dim S As New dbInventario(MySqlcon)
        Rep.SetDataSource(S.ReporteAnalisis(IdAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, Format(DateTimePicker2.Value, "yyyy/MM/dd"), Format(DateTimePicker3.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex)))
        Rep.SetParameterValue("Empresa", Su.Nombre)
        Rep.SetParameterValue("Fechas", "Del " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker3.Value, "yyyy/MM/dd") + " Almance: " + ComboBox8.Text)
        'Rep.SetParameterValue("dcliente", TextBox1.Text + " " + TextBox2.Text)
        'Rep.SetParameterValue("fechas", "Del " + Format(DateTimePicker2.Value, "dd/MM/yyyy") + " al " + Format(DateTimePicker3.Value, "dd/MM/yyyy"))
        Dim RV As New frmReportes(Rep, False)
        RV.Show()
        ' End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Consulta()
    End Sub

  
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox1.SelectedIndex).ToString, "Todos")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ConsultaOn = False
    End Sub
End Class