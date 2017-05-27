Imports System.Windows.Forms

Public Class frmCatalogos
    Dim IdsClas As New elemento
    Dim IdsClas2 As New elemento
    Dim IdsClas3 As New elemento
    Dim ConsultaOn As Boolean
    Dim EncontroClas As Boolean = True
    Dim IdsVendedoresZonaC As New elemento

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmCatalogos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Try
                Me.Icon = GlobalIcono
            Catch ex As Exception

            End Try
            LlenaCombos("tblinventarioclasificaciones", ComboBox3, "nombre", "nombret", "idclasificacion", IdsClas, "idclasificacion>1", "Todas")
            LlenaCombos("tblzona", cmbZonacliente, "zona", "zonat", "idZona", IdsVendedoresZonaC, , "Todos")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.SelectedIndex > 0 Then
            Dim IdClas As Integer
            IdClas = IdsClas.Valor(ComboBox3.SelectedIndex)
            Dim IC2 As New dbInventarioClasificaciones(IdClas, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
            ConsultaOn = False
            TextBox10.Text = IC2.Codigo
            ConsultaOn = True
            LlenaCombos("tblinventarioclasificaciones2", ComboBox6, "nombre", "nombret", "idclasificacion", IdsClas2, "idclasificacion>1 and idnivelsuperior=" + IC2.ID.ToString, "Todas")
            HabilitaClase2(True)
        Else
            HabilitaClase2(False)
            HabilitaClase3(False)
            ComboBox6.SelectedIndex = -1
            ConsultaOn = False
            TextBox10.Text = ""
            ConsultaOn = True
        End If
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox6.SelectedIndexChanged
        If ComboBox6.SelectedIndex > 0 Then
            Dim IdClas As Integer
            IdClas = IdsClas2.Valor(ComboBox6.SelectedIndex)
            Dim IC2 As New dbInventarioClasificaciones(IdClas, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
            ConsultaOn = False
            TextBox12.Text = IC2.Codigo
            ConsultaOn = True
            LlenaCombos("tblinventarioclasificaciones3", ComboBox7, "nombre", "nombret", "idclasificacion", IdsClas3, "idclasificacion>1 and idnivelsuperior=" + IC2.ID.ToString, "Todas")
            HabilitaClase3(True)
        Else
            HabilitaClase3(False)
            ComboBox7.SelectedIndex = -1
            ConsultaOn = False
            TextBox12.Text = ""
            ConsultaOn = True
        End If
    End Sub

    Private Sub ComboBox7_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox7.SelectedIndexChanged
        If ComboBox7.SelectedIndex > 0 Then
            Dim IdClas As Integer
            IdClas = IdsClas3.Valor(ComboBox7.SelectedIndex)
            Dim IC2 As New dbInventarioClasificaciones(IdClas, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel3)
            ConsultaOn = False
            TextBox13.Text = IC2.Codigo
            ConsultaOn = True
        Else
            ConsultaOn = False
            TextBox13.Text = ""
            ConsultaOn = True
        End If
    End Sub

    Private Sub TextBox10_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox10.TextChanged
        If ConsultaOn Then
            Dim IC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
            If IC.BuscaClasificacion(TextBox10.Text) Then
                EncontroClas = True
                ComboBox3.SelectedIndex = IdsClas.Busca(IC.ID)
            Else
                EncontroClas = False
            End If
        End If
    End Sub

    Private Sub TextBox12_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox12.TextChanged
        If ConsultaOn Then
            Dim IC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
            If IC.BuscaClasificacion(TextBox12.Text, IdsClas.Valor(ComboBox3.SelectedIndex)) Then
                EncontroClas = True
                ComboBox6.SelectedIndex = IdsClas2.Busca(IC.ID)
            Else
                EncontroClas = False
            End If
        End If
    End Sub

    Private Sub TextBox13_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox13.TextChanged
        If ConsultaOn Then
            Dim IC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel3)
            If IC.BuscaClasificacion(TextBox13.Text, IdsClas2.Valor(ComboBox6.SelectedIndex)) Then
                EncontroClas = True
                ComboBox7.SelectedIndex = IdsClas3.Busca(IC.ID)
            Else
                EncontroClas = False
            End If
        End If
    End Sub

    Private Sub HabilitaClase2(ByVal Habilita As Boolean)
        TextBox12.Enabled = Habilita
        ComboBox6.Enabled = Habilita
    End Sub

    Private Sub HabilitaClase3(ByVal Habilita As Boolean)
        TextBox13.Enabled = Habilita
        ComboBox7.Enabled = Habilita
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim idClas As Integer
        Dim idClas2 As Integer
        Dim idClas3 As Integer
        Dim idClas5 As Integer

        If cmbZonacliente.SelectedIndex <= 0 Then
            idClas5 = 0
        Else
            idClas5 = IdsVendedoresZonaC.Valor(cmbZonacliente.SelectedIndex)
        End If

        If ComboBox3.SelectedIndex <= 0 Then
            idClas = 0
        Else
            idClas = IdsClas.Valor(ComboBox3.SelectedIndex)
        End If
        If ComboBox6.SelectedIndex <= 0 Then
            idClas2 = 0
        Else
            idClas2 = IdsClas2.Valor(ComboBox6.SelectedIndex)
        End If
        If ComboBox7.SelectedIndex <= 0 Then
            idClas3 = 0
        Else
            idClas3 = IdsClas3.Valor(ComboBox7.SelectedIndex)
        End If
        Try
            Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
            If rdbAlmacenes.Checked Then
                Dim db As New dbAlmacenes(MySqlcon)
                Dim rep As New repCatalogoAlmacenes
                rep.SetDataSource(db.reporte)
                rep.SetParameterValue("Encabezado", S.Nombre)
                Dim RV As New frmReportes(rep, False)
                RV.Show()
            End If
            If rdbArticulos.Checked Then
                Dim db As New dbInventario(MySqlcon)
                Dim rep As New repCatalogoArticulos
                rep.SetDataSource(db.ReporteInventario(0, 0, idClas, idClas2, idClas3, 0, False, False, False, False, 0, False, CheckBox2.Checked))
                rep.SetParameterValue("Encabezado", S.Nombre)
                rep.SetParameterValue("Sucursal", "")
                rep.SetParameterValue("Almacen", "")
                If CheckBox2.Checked Then
                    rep.SetParameterValue("filtros", "Solo descontinuados")
                Else
                    rep.SetParameterValue("filtros", "")
                End If
                Dim RV As New frmReportes(rep, False)
                RV.Show()
            End If
            'If rdbProductos.Checked Then
            '    Dim db As New dbProductos(MySqlcon)
            '    Dim rep As New repCatalogoProductos
            '    rep.SetDataSource(db.reporte(0))
            '    rep.SetParameterValue("Encabezado", S.Nombre)
            '    Dim RV As New frmReportes(rep, False)
            '    RV.Show()
            'End If
            If rdbClientes.Checked Then
                Dim db As New dbClientes(MySqlcon)
                Dim rep As New repCatalogoClientes
                rep.SetDataSource(db.Reporte2(idClas5, TextBox1.Text, CheckBox1.Checked))
                rep.SetParameterValue("Encabezado", S.Nombre)
                rep.SetParameterValue("concredito", CheckBox1.Checked)
                Dim RV As New frmReportes(rep, False)
                RV.Show()
            End If
            If rdbClientesDirectorio.Checked Then
                Dim db As New dbClientes(MySqlcon)
                Dim rep As New repDirectorioClientes
                rep.SetDataSource(db.Reporte2(idClas5, TextBox1.Text, CheckBox1.Checked))
                rep.SetParameterValue("Encabezado", S.Nombre)
                Dim RV As New frmReportes(rep, False)
                RV.Show()
            End If
            If rdbProveedores.Checked Then
                Dim db As New dbproveedores(MySqlcon)
                Dim rep As New repCatalogoProveedores
                rep.SetDataSource(db.Reporte(0))
                rep.SetParameterValue("Encabezado", S.Nombre)
                Dim RV As New frmReportes(rep, False)
                RV.Show()
            End If
            If rdbProveedoresDirectorio.Checked Then
                Dim db As New dbproveedores(MySqlcon)
                Dim rep As New repDirectorioProveedores
                rep.SetDataSource(db.Reporte(0))
                rep.SetParameterValue("Encabezado", S.Nombre)
                Dim RV As New frmReportes(rep, False)
                RV.Show()
            End If
            If rdbSucursales.Checked Then
                Dim rep As New repCatalogoSucursales
                Dim db As New dbSucursales(MySqlcon)
                rep.SetDataSource(db.Consulta)
                rep.SetParameterValue("Encabezado", S.Nombre)
                Dim RV As New frmReportes(rep, False)
                RV.Show()
            End If
            If rdbVendedores.Checked Then
                Dim db As New dbVendedores(MySqlcon)
                Dim rep As New repCatalogoVendedores
                rep.SetDataSource(db.Consulta(idClas5))
                rep.SetParameterValue("Encabezado", S.Nombre)
                Dim RV As New frmReportes(rep, False)
                RV.Show()
            End If
            'If rdbClasificacionesProductos.Checked Then
            '    Dim db As New dbProductosClasificaciones(MySqlcon)
            '    Dim rep As New repCatalogoProductosClasif
            '    rep.SetDataSource(db.Consulta)
            '    rep.SetParameterValue("Encabezado", S.Nombre)
            '    Dim RV As New frmReportes(rep, False)
            '    RV.Show()
            'End If
            If rdbClasificacionesArticulos.Checked Then
                Dim db As New dbInventarioClasificaciones(MySqlcon, 0)
                Dim rep As New repCatalogoArticulosClasif
                rep.SetDataSource(db.Reporte)
                rep.SetParameterValue("Encabezado", S.Nombre)
                Dim RV As New frmReportes(rep, False)
                RV.Show()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub rdbClientesDirectorio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbClientesDirectorio.CheckedChanged
        If rdbClientesDirectorio.Checked = True Then
            lblZonaCliente.Visible = True
            cmbZonacliente.Visible = True
        Else
            lblZonaCliente.Visible = False
            cmbZonacliente.Visible = False
        End If
    End Sub

    Private Sub rdbClientes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbClientes.CheckedChanged
        If rdbClientes.Checked = True Then
            lblZonaCliente.Visible = True
            cmbZonacliente.Visible = True
        Else
            lblZonaCliente.Visible = False
            cmbZonacliente.Visible = False
        End If
    End Sub

    Private Sub rdbVendedores_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbVendedores.CheckedChanged
        If rdbVendedores.Checked = True Then
            '  lblZonaCliente.Visible = True
            lblZonaVendedor.Visible = True
            cmbZonacliente.Visible = True
        Else
            ' lblZonaCliente.Visible = False
            lblZonaVendedor.Visible = False
            cmbZonacliente.Visible = False
        End If
    End Sub
End Class
