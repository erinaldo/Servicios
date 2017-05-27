Public Class frmInventarioConfiguracionConceptos
    Private idsSucursales As New elemento
    Private idsMerma As New elemento
    Private idsDevoluciones As New elemento
    Private idsObsequios As New elemento
    Private idsConceptos As New elemento
    Private idsCargas As New elemento
    Private idsClientes As New elemento
    Private idsPlanta As New elemento
    Private idsMermas2 As New elemento
    Private idsObsequios2 As New elemento
    Private idsBuenas As New elemento
    Private idSucursal As Integer
    Private idMerma As Integer
    Private idDev As Integer
    Private idObsequio As Integer
    Private relaciones As dbRelacionesConceptos
    Private Sub frmInventarioConfiguracionConceptos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        relaciones = New dbRelacionesConceptos(MySqlcon)
        LlenaCombos("tblsucursales", comboSucursales, "nombre", "nombreN", "idsucursal", idsSucursales)
        'LlenaCombos("tblinventarioconceptos", comboMermas, "nombre", "nombret", "idconcepto", idsMerma, "idsucursal=" + idsSucursales.Valor(comboSucursales.SelectedIndex).ToString)
        'LlenaCombos("tblinventarioconceptos", ComboObsequios, "nombre", "nombret", "idconcepto", idsObsequios, "idsucursal=" + idsSucursales.Valor(comboSucursales.SelectedIndex).ToString)
        'LlenaCombos("tblinventarioconceptos", ComboDevolucion, "nombre", "nombret", "idconcepto", idsDevoluciones, "idsucursal=" + idsSucursales.Valor(comboSucursales.SelectedIndex).ToString)

        llenarCombos()
        llenaDatos()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        guardar()
    End Sub

    Private Sub guardar()
        idSucursal = idsSucursales.Valor(comboSucursales.SelectedIndex)
        If relaciones.buscarTipo(idSucursal, dbRelacionesConceptos.tipoConcepto.mermas) Then
            relaciones.modificar(relaciones.idRelacion, idsMerma.Valor(comboMermas.SelectedIndex), dbRelacionesConceptos.tipoConcepto.mermas, idSucursal)
        Else
            relaciones.agregar(idsMerma.Valor(comboMermas.SelectedIndex), dbRelacionesConceptos.tipoConcepto.mermas, idSucursal)
        End If
        If relaciones.buscarTipo(idSucursal, dbRelacionesConceptos.tipoConcepto.obsequios) Then
            relaciones.modificar(relaciones.idRelacion, idsObsequios.Valor(ComboObsequios.SelectedIndex), dbRelacionesConceptos.tipoConcepto.obsequios, idSucursal)
        Else
            relaciones.agregar(idsObsequios.Valor(ComboObsequios.SelectedIndex), dbRelacionesConceptos.tipoConcepto.obsequios, idSucursal)
        End If
        If relaciones.buscarTipo(idSucursal, dbRelacionesConceptos.tipoConcepto.devPlanta) Then
            relaciones.modificar(relaciones.idRelacion, idsDevoluciones.Valor(ComboDevolucion.SelectedIndex), dbRelacionesConceptos.tipoConcepto.devPlanta, idSucursal)
        Else
            relaciones.agregar(idsDevoluciones.Valor(ComboDevolucion.SelectedIndex), dbRelacionesConceptos.tipoConcepto.devPlanta, idSucursal)
        End If
        'If relaciones.buscarTipo(idSucursal, dbRelacionesConceptos.tipoConcepto.carga) Then
        '    relaciones.modificar(relaciones.idRelacion, idsCargas.Valor(comboCarga.SelectedIndex), dbRelacionesConceptos.tipoConcepto.carga, idSucursal)
        'Else
        '    relaciones.agregar(idsCargas.Valor(comboCarga.SelectedIndex), dbRelacionesConceptos.tipoConcepto.carga, idSucursal)
        'End If
        If relaciones.buscarTipo(idSucursal, dbRelacionesConceptos.tipoConcepto.dvCliente) Then
            relaciones.modificar(relaciones.idRelacion, idsClientes.Valor(comboCliente.SelectedIndex), dbRelacionesConceptos.tipoConcepto.dvCliente, idSucursal)
        Else
            relaciones.agregar(idsClientes.Valor(comboCliente.SelectedIndex), dbRelacionesConceptos.tipoConcepto.dvCliente, idSucursal)
        End If
        If relaciones.buscarTipo(idSucursal, dbRelacionesConceptos.tipoConcepto.mermas2) Then
            relaciones.modificar(relaciones.idRelacion, idsMermas2.Valor(comboMermas2.SelectedIndex), dbRelacionesConceptos.tipoConcepto.mermas2, idSucursal)
        Else
            relaciones.agregar(idsMermas2.Valor(comboMermas2.SelectedIndex), dbRelacionesConceptos.tipoConcepto.mermas2, idSucursal)
        End If
        If relaciones.buscarTipo(idSucursal, dbRelacionesConceptos.tipoConcepto.obsequios2) Then
            relaciones.modificar(relaciones.idRelacion, idsObsequios2.Valor(comboObsequios2.SelectedIndex), dbRelacionesConceptos.tipoConcepto.obsequios2, idSucursal)
        Else
            relaciones.agregar(idsObsequios2.Valor(comboObsequios2.SelectedIndex), dbRelacionesConceptos.tipoConcepto.obsequios2, idSucursal)
        End If
        If relaciones.buscarTipo(idSucursal, dbRelacionesConceptos.tipoConcepto.enviosPlanta) Then
            relaciones.modificar(relaciones.idRelacion, idsPlanta.Valor(comboPlanta.SelectedIndex), dbRelacionesConceptos.tipoConcepto.enviosPlanta, idSucursal)
        Else
            relaciones.agregar(idsPlanta.Valor(comboPlanta.SelectedIndex), dbRelacionesConceptos.tipoConcepto.enviosPlanta, idSucursal)
        End If
        If relaciones.buscarTipo(idSucursal, dbRelacionesConceptos.tipoConcepto.buenas) Then
            relaciones.modificar(relaciones.idRelacion, idsBuenas.Valor(comboBuenas.SelectedIndex), dbRelacionesConceptos.tipoConcepto.buenas, idSucursal)
        Else
            relaciones.agregar(idsBuenas.Valor(comboBuenas.SelectedIndex), dbRelacionesConceptos.tipoConcepto.buenas, idSucursal)
        End If
        PopUp("Guardado", 60)
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub llenaDatos()
        If relaciones.buscarTipo(idsSucursales.Valor(comboSucursales.SelectedIndex), dbRelacionesConceptos.tipoConcepto.mermas) Then
            comboMermas.SelectedIndex = idsMerma.Busca(relaciones.idConcepto)
        End If
        If relaciones.buscarTipo(idsSucursales.Valor(comboSucursales.SelectedIndex), dbRelacionesConceptos.tipoConcepto.obsequios) Then
            ComboObsequios.SelectedIndex = idsObsequios.Busca(relaciones.idConcepto)
        End If
        If relaciones.buscarTipo(idsSucursales.Valor(comboSucursales.SelectedIndex), dbRelacionesConceptos.tipoConcepto.devPlanta) Then
            ComboDevolucion.SelectedIndex = idsDevoluciones.Busca(relaciones.idConcepto)
        End If
        If relaciones.buscarTipo(idsSucursales.Valor(comboSucursales.SelectedIndex), dbRelacionesConceptos.tipoConcepto.mermas2) Then
            comboMermas2.SelectedIndex = idsMermas2.Busca(relaciones.idConcepto)
        End If
        If relaciones.buscarTipo(idsSucursales.Valor(comboSucursales.SelectedIndex), dbRelacionesConceptos.tipoConcepto.obsequios2) Then
            comboObsequios2.SelectedIndex = idsObsequios2.Busca(relaciones.idConcepto)
        End If
        If relaciones.buscarTipo(idsSucursales.Valor(comboSucursales.SelectedIndex), dbRelacionesConceptos.tipoConcepto.enviosPlanta) Then
            comboPlanta.SelectedIndex = idsPlanta.Busca(relaciones.idConcepto)
        End If
        If relaciones.buscarTipo(idsSucursales.Valor(comboSucursales.SelectedIndex), dbRelacionesConceptos.tipoConcepto.buenas) Then
            comboBuenas.SelectedIndex = idsBuenas.Busca(relaciones.idConcepto)
        End If
        If relaciones.buscarTipo(idsSucursales.Valor(comboSucursales.SelectedIndex), dbRelacionesConceptos.tipoConcepto.dvCliente) Then
            comboCliente.SelectedIndex = idsClientes.Busca(relaciones.idConcepto)
        End If
        'If relaciones.buscarTipo(idsSucursales.Valor(comboSucursales.SelectedIndex), dbRelacionesConceptos.tipoConcepto.carga) Then
        '    comboCarga.SelectedIndex = idsCargas.Busca(relaciones.idConcepto)
        'End If
    End Sub

    Private Sub llenarCombos()
        LlenaCombos("tblinventarioconceptos", comboMermas, "nombre", "nombret", "idconcepto", idsMerma, "idsucursal=" + idsSucursales.Valor(comboSucursales.SelectedIndex).ToString)
        LlenaCombos("tblinventarioconceptos", ComboObsequios, "nombre", "nombret", "idconcepto", idsObsequios, "idsucursal=" + idsSucursales.Valor(comboSucursales.SelectedIndex).ToString)
        LlenaCombos("tblinventarioconceptos", ComboDevolucion, "nombre", "nombret", "idconcepto", idsDevoluciones, "idsucursal=" + idsSucursales.Valor(comboSucursales.SelectedIndex).ToString)
        'LlenaCombos("tblinventarioconceptos", comboCarga, "nombre", "nombret", "idconcepto", idsCargas, "idsucursal=" + idsSucursales.Valor(comboSucursales.SelectedIndex).ToString + " and tipo=" + CInt(dbInventarioConceptos.Tipos.Traspaso).ToString())
        LlenaCombos("tblinventarioconceptos", comboCliente, "nombre", "nombret", "idconcepto", idsClientes, "idsucursal=" + idsSucursales.Valor(comboSucursales.SelectedIndex).ToString)
        LlenaCombos("tblinventarioconceptos", comboPlanta, "nombre", "nombret", "idconcepto", idsPlanta, "idsucursal=" + idsSucursales.Valor(comboSucursales.SelectedIndex).ToString)
        LlenaCombos("tblinventarioconceptos", comboMermas2, "nombre", "nombret", "idconcepto", idsMermas2, "idsucursal=" + idsSucursales.Valor(comboSucursales.SelectedIndex).ToString)
        LlenaCombos("tblinventarioconceptos", comboObsequios2, "nombre", "nombret", "idconcepto", idsObsequios2, "idsucursal=" + idsSucursales.Valor(comboSucursales.SelectedIndex).ToString)
        LlenaCombos("tblinventarioconceptos", comboBuenas, "nombre", "nombret", "idconcepto", idsBuenas, "idsucursal=" + idsSucursales.Valor(comboSucursales.SelectedIndex).ToString)


        'LlenaCombos("tblinventarioconceptos", comboMermas, "nombre", "nombret", "idconcepto", idsMerma, "idsucursal=" + idsSucursales.Valor(comboSucursales.SelectedIndex).ToString + " and tipo=" + CInt(dbInventarioConceptos.Tipos.Traspaso).ToString())
        'LlenaCombos("tblinventarioconceptos", ComboObsequios, "nombre", "nombret", "idconcepto", idsObsequios, "idsucursal=" + idsSucursales.Valor(comboSucursales.SelectedIndex).ToString + " and tipo=" + CInt(dbInventarioConceptos.Tipos.Salida).ToString())
        'LlenaCombos("tblinventarioconceptos", ComboDevolucion, "nombre", "nombret", "idconcepto", idsDevoluciones, "idsucursal=" + idsSucursales.Valor(comboSucursales.SelectedIndex).ToString + " and tipo=" + CInt(dbInventarioConceptos.Tipos.Traspaso).ToString())
        ''LlenaCombos("tblinventarioconceptos", comboCarga, "nombre", "nombret", "idconcepto", idsCargas, "idsucursal=" + idsSucursales.Valor(comboSucursales.SelectedIndex).ToString + " and tipo=" + CInt(dbInventarioConceptos.Tipos.Traspaso).ToString())
        'LlenaCombos("tblinventarioconceptos", comboCliente, "nombre", "nombret", "idconcepto", idsClientes, "idsucursal=" + idsSucursales.Valor(comboSucursales.SelectedIndex).ToString + " and tipo=" + CInt(dbInventarioConceptos.Tipos.Entrada).ToString())
        'LlenaCombos("tblinventarioconceptos", comboPlanta, "nombre", "nombret", "idconcepto", idsPlanta, "idsucursal=" + idsSucursales.Valor(comboSucursales.SelectedIndex).ToString + " and tipo=" + CInt(dbInventarioConceptos.Tipos.Entrada).ToString())
        'LlenaCombos("tblinventarioconceptos", comboMermas2, "nombre", "nombret", "idconcepto", idsMermas2, "idsucursal=" + idsSucursales.Valor(comboSucursales.SelectedIndex).ToString + " and tipo=" + CInt(dbInventarioConceptos.Tipos.Traspaso).ToString())
        'LlenaCombos("tblinventarioconceptos", comboObsequios2, "nombre", "nombret", "idconcepto", idsObsequios2, "idsucursal=" + idsSucursales.Valor(comboSucursales.SelectedIndex).ToString + " and tipo=" + CInt(dbInventarioConceptos.Tipos.Salida).ToString())
        'LlenaCombos("tblinventarioconceptos", comboBuenas, "nombre", "nombret", "idconcepto", idsBuenas, "idsucursal=" + idsSucursales.Valor(comboSucursales.SelectedIndex).ToString + " and tipo=" + CInt(dbInventarioConceptos.Tipos.Salida).ToString())
    End Sub

    Private Sub comboSucursales_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboSucursales.SelectedIndexChanged
        llenarCombos()
        llenaDatos()
    End Sub
End Class