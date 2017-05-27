Public Class frmServiciosConsulta
    Dim ConsultaOn As Boolean = True
    Dim IdsEstado As New elemento
    Private Sub frmServiciosConsulta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ConsultaOn = False
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        chkTiempoReal.Checked = GlobalConsultaTiempoReal
        LlenaCombos("tblserviciosestados", ComboBox1, "estado", "estadoc", "idEstado", IdsEstado, , "Todos")
        'ComboBox1.Items.Add("Todos")
        ComboBox2.Items.Add("Todos")
        'ComboBox1.Items.Add("En Taller")
        'ComboBox1.Items.Add("Listo")
        ComboBox2.Items.Add("Abierto")
        ComboBox2.Items.Add("Cerrado")
        ComboBox1.SelectedIndex = 0
        ComboBox2.SelectedIndex = 0
        Dim mes As Integer
        mes = Integer.Parse(Date.Now.Month.ToString())
        If mes <> 1 Then
            mes = mes - 1
        End If
        DateTimePicker2.Value = CDate(Format(Date.Now, "yyyy") + "/" + mes.ToString() + "/01")
        DateTimePicker3.Value = Date.Now
        ConsultaOn = True
        If chkTiempoReal.Checked Then consultaTodos()
        TextBox1.Focus()
    End Sub

    Private Sub AbreDetalles()
        Dim F As New frmServiciosDetalles(DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value, DGServicios.Item(7, DGServicios.CurrentCell.RowIndex).Value, 0)
        F.ShowDialog()
        F.Dispose()
        If F.DialogResult = Windows.Forms.DialogResult.OK Then
            consultaEspecifica()
            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If chkTiempoReal.Checked = True Then
            consultaEspecifica()
        End If

    End Sub

    Private Sub DateTimePicker2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker2.ValueChanged
        If chkTiempoReal.Checked = True Then consultaEspecifica()
    End Sub

    Private Sub DateTimePicker3_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker3.ValueChanged
        If chkTiempoReal.Checked = True Then consultaEspecifica()
    End Sub

    Private Sub DGServicios_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellDoubleClick
        If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServiciosDetallesVer, PermisosN.Secciones.Servicios) = True) Then
            AbreDetalles()
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If chkTiempoReal.Checked = True Then consultaEspecifica()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If chkTiempoReal.Checked = True Then consultaEspecifica()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If chkTiempoReal.Checked = True Then consultaEspecifica()
    End Sub

    Private Sub DGServicios_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellContentClick

    End Sub
    '------------
    Public Sub consultaTodos()
        Try
            If ConsultaOn Then
                Dim S As New dbServicios(MySqlcon)
                Dim Estado As Byte
                Dim Cerrado As Byte
                Dim estado1 As Integer
                Dim estado2 As String
                If ComboBox1.SelectedIndex = 0 Then
                    Estado = 200
                Else
                    Estado = ComboBox1.SelectedIndex - 1
                End If
                If ComboBox2.SelectedIndex = 0 Then
                    Cerrado = 200
                Else
                    Cerrado = ComboBox2.SelectedIndex - 1
                End If
                Dim TablaFull As New DataTable
                Dim Tabla As New DataTable
                TablaFull.Columns.Add("ID")
                TablaFull.Columns.Add("Folio")
                TablaFull.Columns.Add("Fecha")
                TablaFull.Columns.Add("Cliente")
                TablaFull.Columns.Add("Detalles")
                TablaFull.Columns.Add("Estado")
                TablaFull.Columns.Add("Estado2")
                TablaFull.Columns.Add("idEquipo")

                Tabla = S.ConsultaTodo(Format(DateTimePicker2.Value, "yyyy/MM/dd"), Format(DateTimePicker3.Value, "yyyy/MM/dd")).ToTable
                For i As Integer = 0 To Tabla.Rows.Count() - 1
                    Dim dr As DataRow

                    dr = TablaFull.NewRow()
                    dr("ID") = Tabla.Rows(i)(0).ToString
                    dr("Folio") = Tabla.Rows(i)(1).ToString
                    dr("Fecha") = Tabla.Rows(i)(2).ToString
                    dr("Cliente") = Tabla.Rows(i)(4).ToString
                    dr("Detalles") = Tabla.Rows(i)(3).ToString

                    'If Tabla.Rows(i)(5).ToString = "0" Then
                    '    estado1 = "En Taller"
                    'Else
                    estado1 = Integer.Parse(Tabla.Rows(i)(5).ToString)
                    'End If
                    dr("Estado") = S.buscarEstado(estado1)
                    If Tabla.Rows(i)(6).ToString = "0" Then
                        estado2 = "Abierto"
                    Else
                        estado2 = "Cerrado"
                    End If
                    dr("Estado2") = estado2
                    TablaFull.Rows.Add(dr)
                    dr("idEquipo") = Tabla.Rows(i)(7).ToString()
                Next

                DGServicios.DataSource = TablaFull
                DGServicios.Columns(0).Visible = False
                DGServicios.Columns(1).HeaderText = "Folio"
                DGServicios.Columns(2).HeaderText = "Fecha"
                DGServicios.Columns(3).HeaderText = "Detalles"
                DGServicios.Columns(4).HeaderText = "Cliente"
                DGServicios.Columns(5).HeaderText = "Estado"
                DGServicios.Columns(6).HeaderText = "Estatus"
                DGServicios.Columns(7).Visible = False
                DGServicios.Columns(1).Width = 40
                DGServicios.Columns(2).Width = 80
                DGServicios.Columns(3).Width = 180
                DGServicios.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                DGServicios.ClearSelection()

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Public Sub consultaEspecifica()
        Try
            If ConsultaOn Then
                Dim S As New dbServicios(MySqlcon)
                ' Dim Estado As Byte
                Dim Cerrado As Byte
                Dim estado1 As Integer
                Dim estado2 As String
                Dim x As Integer
                Dim y As Integer
                'Dim idEvento1 As Integer
                'If ComboBox1.SelectedIndex = 0 Then
                '    Estado = 200
                'Else
                '    Estado = ComboBox1.SelectedIndex - 1
                'End If
                If ComboBox1.SelectedIndex <= 0 Then
                    x = 100
                Else
                    x = IdsEstado.Valor(ComboBox1.SelectedIndex)
                End If
                If ComboBox2.SelectedIndex = 0 Then
                    Cerrado = 200
                Else
                    Cerrado = ComboBox2.SelectedIndex - 1
                End If
                Dim TablaFull As New DataTable
                Dim Tabla As New DataTable
                TablaFull.Columns.Add("ID")
                TablaFull.Columns.Add("Folio")
                TablaFull.Columns.Add("Fecha")
                TablaFull.Columns.Add("Cliente")
                TablaFull.Columns.Add("Detalles")
                TablaFull.Columns.Add("Estado")
                TablaFull.Columns.Add("Estado2")
                TablaFull.Columns.Add("idEquipo")
                'If ComboBox1.Text = "Todos" Then
                '    x = 100
                'Else
                '    x = ComboBox1.SelectedIndex - 1
                'End If
                If ComboBox2.Text = "Todos" Then
                    y = 100
                Else
                    y = ComboBox2.SelectedIndex - 1
                End If
                Tabla = S.ConsultaEspecifica(Format(DateTimePicker2.Value, "yyyy/MM/dd"), Format(DateTimePicker3.Value, "yyyy/MM/dd"), TextBox1.Text, x, y).ToTable
                For i As Integer = 0 To Tabla.Rows.Count() - 1
                    Dim dr As DataRow

                    dr = TablaFull.NewRow()
                    dr("ID") = Tabla.Rows(i)(0).ToString
                    dr("Folio") = Tabla.Rows(i)(1).ToString
                    dr("Fecha") = Tabla.Rows(i)(2).ToString
                    dr("Cliente") = Tabla.Rows(i)(4).ToString
                    dr("Detalles") = Tabla.Rows(i)(3).ToString

                    'If Tabla.Rows(i)(5).ToString = "0" Then
                    '    estado1 = "En Taller"
                    'Else
                    estado1 = Integer.Parse(Tabla.Rows(i)(5).ToString)
                    'End If

                    If estado1 = 0 Then
                        dr("Estado") = "Ninguno"
                    Else
                        dr("Estado") = S.buscarEstado(estado1)
                    End If

                    If Tabla.Rows(i)(6).ToString = "0" Then
                        estado2 = "Abierto"
                    Else
                        estado2 = "Cerrado"
                    End If
                    dr("Estado2") = estado2
                    TablaFull.Rows.Add(dr)
                    dr("idEquipo") = Tabla.Rows(i)(7).ToString
                Next

                DGServicios.DataSource = TablaFull
                DGServicios.Columns(0).Visible = False
                DGServicios.Columns(1).HeaderText = "Folio"
                DGServicios.Columns(2).HeaderText = "Fecha"
                DGServicios.Columns(3).HeaderText = "Detalles"
                DGServicios.Columns(4).HeaderText = "Cliente"
                DGServicios.Columns(5).HeaderText = "Estado"
                DGServicios.Columns(6).HeaderText = "Estatus"
                DGServicios.Columns(7).Visible = False
                DGServicios.Columns(1).Width = 40
                DGServicios.Columns(2).Width = 80
                DGServicios.Columns(3).Width = 180
                DGServicios.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                DGServicios.ClearSelection()

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub btnSeleccionar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeleccionar.Click
        consultaEspecifica()
    End Sub
End Class