Public Class frmRestauranteAcomodarMesas
    Dim mesa As RestauranteMesa
    Dim mesasController As dbRestauranteMesas
    Dim secciones As dbRestauranteSecciones
    Dim IdsSucursales As New elemento
    Dim listaSecciones As New elemento
    Dim listaMesas As New List(Of RestauranteMesa)
    Dim configuracion As dbRestauranteConfiguracion
    Dim idSucursal As Integer
    Dim idSeccion As Integer
    Dim capacidad As Integer
    Dim seMueve As Boolean = False
    Dim seleccionado As Boolean = False
    Dim numMesa As Integer
    Dim Fuente As Font = New Font("Arial", 10, FontStyle.Regular)

    Public Sub New(pIdSucursal As Integer)

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        mesasController = New dbRestauranteMesas(MySqlcon, pIdSucursal)
        secciones = New dbRestauranteSecciones(MySqlcon)
        configuracion = New dbRestauranteConfiguracion(1, MySqlcon)
        LlenaCombos("tblsucursales", comboSucursal, "nombre", "nombret", "idsucursal", IdsSucursales)
        comboSucursal.SelectedIndex = IdsSucursales.Busca(pIdSucursal)
        Me.BackColor = Color.FromArgb(configuracion.colorVentanas)
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        'Dim HayError As String = ""
        'If txtNumero.Text = "" Then
        '    HayError += "Debe indicar un numero de mesa."
        'Else
        '    If IsNumeric(txtNumero.Text) = False Then
        '        HayError += "El número de mesa debe ser un valor numérico."
        '    End If
        'End If
        'If txtCapacidad.Text = "" Then
        '    HayError += "Debe indicar la capacidad de la mesa."
        'Else
        '    If IsNumeric(txtNumero.Text) = False Then
        '        HayError += "La capacidad de la mesa debe ser un valor numérico."
        '    End If
        'End If

        'If HayError = "" Then
        idSucursal = IdsSucursales.Valor(comboSucursal.SelectedIndex)
        idSeccion = listaSecciones.Valor(comboSeccion.SelectedIndex)
        listaMesas.Add(New RestauranteMesa(listaMesas.Count + 1, idSeccion, EstadosMesas.inicio, 4, idSucursal))
        panelMesas.Refresh()
        'MuestraMesas()
        'Else
        '   MsgBox(HayError, MsgBoxStyle.Information, GlobalNombreApp)
        'End If
    End Sub

    Private Sub comboSucursal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboSucursal.SelectedIndexChanged
        idSucursal = IdsSucursales.Valor(comboSucursal.SelectedIndex)
        LlenaCombos("tblrestaurantesecciones", comboSeccion, "nombre", "nombret", "idseccion", listaSecciones, "idsucursal=" + idSucursal.ToString)
        MuestraMesas()
    End Sub

    Private Sub MuestraMesas()
        listaMesas = mesasController.listaMesas(idSeccion)
        panelMesas.Refresh()
    End Sub

    Private Sub panelMesas_MouseMove(sender As Object, e As MouseEventArgs)
        If seMueve Then
            For Each m As RestauranteMesa In listaMesas
                If m.seleccionado Then
                    m.X = m.X - e.X
                    m.Y = m.Y - e.Y
                    If m.X < 0 Then m.X = 0
                    If m.Y < 0 Then m.Y = 0
                    Exit For
                End If
            Next
            panelMesas.Refresh()
        End If
    End Sub

    Private Sub frmRestauranteAcomodarMesas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        listaMesas = mesasController.listaMesas(idSucursal)
        MuestraMesas()
    End Sub

    Private Sub panelMesas_Paint(sender As Object, e As PaintEventArgs) Handles panelMesas.Paint
        For Each m As RestauranteMesa In listaMesas
            If m.seleccionado Then
                e.Graphics.DrawRectangle(Pens.Red, m.X, m.Y, m.Width, m.Height)
                e.Graphics.DrawString(m.numero.ToString + " - " + m.capacidad.ToString, Fuente, Brushes.Black, m.X + 2, m.Y + 2)
            Else
                e.Graphics.DrawRectangle(Pens.Blue, m.X, m.Y, m.Width, m.Height)
                e.Graphics.DrawString(m.numero.ToString + " - " + m.capacidad.ToString, Fuente, Brushes.Black, m.X + 2, m.Y + 2)
            End If
        Next
    End Sub

    Private Sub panelMesas_MouseDown(sender As Object, e As MouseEventArgs) Handles panelMesas.MouseDown
        If seMueve = False Then
            For Each m As RestauranteMesa In listaMesas
                m.seleccionado = False
            Next
            For Each m As RestauranteMesa In listaMesas
                If e.X > m.X And e.X < (m.X + m.Width) And e.Y > m.Y And e.Y < (m.Y + m.Height) Then
                    m.seleccionado = True
                    txtAlto.Text = m.Height.ToString
                    txtAncho.Text = m.Width.ToString
                    txtNumero.Text = m.numero.ToString
                    txtCapacidad.Text = m.capacidad.ToString
                    Exit For
                End If
            Next
        End If
        seMueve = True
        panelMesas.Refresh()
    End Sub

    Private Sub panelMesas_MouseUp(sender As Object, e As MouseEventArgs) Handles panelMesas.MouseUp
        'If seMueve Then
        '    For Each m As RestauranteMesa In listaMesas
        '        If m.seleccionado = True Then
        '            ' m.seleccionado = False
        '            Exit For
        '        End If
        '    Next
        '    panelMesas.Refresh()
        'End If
        seMueve = False
    End Sub

    Private Sub panelMesas_MouseMove_1(sender As Object, e As MouseEventArgs) Handles panelMesas.MouseMove
        If seMueve Then
            For Each m As RestauranteMesa In listaMesas
                If m.seleccionado Then
                    m.X = m.X + (e.X - m.X)
                    m.Y = m.Y + (e.Y - m.Y)
                    If m.X < 0 Then m.X = 0
                    If m.Y < 0 Then m.Y = 0
                    Exit For
                End If
            Next
            panelMesas.Refresh()
        End If
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        For Each m As RestauranteMesa In listaMesas
            If m.estado = EstadosMesas.inicio Then
                m.estado = EstadosMesas.Libre
                mesasController.agregar(m)
            Else
                mesasController.modificar(m)
            End If
        Next
        Me.Dispose()
    End Sub

    Private Sub txtAlto_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAlto.KeyPress
        If Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
        If e.KeyChar = vbBack Then
            e.Handled = False
        End If
    End Sub

    Private Sub txtAncho_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAncho.KeyPress
        If Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
        If e.KeyChar = vbBack Then
            e.Handled = False
        End If
    End Sub

    Private Sub txtAlto_TextChanged(sender As Object, e As EventArgs) Handles txtAlto.TextChanged
        For Each m As RestauranteMesa In listaMesas
            If m.seleccionado Then
                If txtAlto.Text = "" Then
                    m.Height = 0
                Else
                    m.Height = CInt(txtAlto.Text)
                End If
                Exit For
            End If
        Next
        panelMesas.Refresh()
    End Sub

    Private Sub txtAncho_TextChanged(sender As Object, e As EventArgs) Handles txtAncho.TextChanged
        For Each m As RestauranteMesa In listaMesas
            If m.seleccionado Then
                If txtAncho.Text = "" Then
                    m.Width = 0
                Else
                    m.Width = CInt(txtAncho.Text)
                End If
                Exit For
            End If
        Next
        panelMesas.Refresh()
    End Sub

    Private Sub comboSeccion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboSeccion.SelectedIndexChanged
        idSeccion = listaSecciones.Valor(comboSeccion.SelectedIndex)
        Try
            panelMesas.BackgroundImage = Image.FromFile(secciones.rutaMapa)
        Catch

        End Try
        MuestraMesas()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    
    Private Sub txtNumero_TextChanged(sender As Object, e As EventArgs) Handles txtNumero.TextChanged

    End Sub

    Private Sub txtCapacidad_TextChanged(sender As Object, e As EventArgs) Handles txtCapacidad.TextChanged
        For Each m As RestauranteMesa In listaMesas
            If m.seleccionado Then
                If IsNumeric(txtCapacidad.Text) = False Then
                    m.capacidad = 0
                Else
                    m.capacidad = CInt(txtCapacidad.Text)
                End If
                Exit For
            End If
        Next
        panelMesas.Refresh()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        For Each m As RestauranteMesa In listaMesas
            If m.seleccionado Then
                If MsgBox("¿Eliminar esta mesa?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Try
                        mesasController.eliminar(m.id)
                        MuestraMesas()
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Information, GlobalNombreApp)
                    End Try

                End If
            End If
        Next
    End Sub
End Class