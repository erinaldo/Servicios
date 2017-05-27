Public Class frmGastosSalarioEmpleados
    Dim I As New dbAltaEmpleados(MySqlcon)
    Dim checked As Boolean = False
    Dim cargando As Boolean = True
    Dim llenado As Boolean = False
    Dim idGasto As Integer
    Dim idSueldo As Integer
    Dim IdClas1 As Integer
    Dim idClas2 As Integer
    Dim idClas3 As Integer
    Public Sub New(ByVal pidGasto As Integer, pIdClas As Integer, pidClas2 As Integer, pIdClas3 As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        idGasto = pidGasto
        IdClas1 = pIdClas
        If IdClas1 < 0 Then IdClas1 = 0
        idClas2 = pidClas2
        If idClas2 < 0 Then idClas2 = 0
        idClas3 = pIdClas3
        If idClas3 < 0 Then idClas3 = 0

    End Sub
    Private Sub frmGastosSalarioEmpleados_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        'conprobar si tiene suelgo...
        idSueldo = I.buscarSueldos(idGasto)
        cargando = True
        Chck_separar.Checked = True
            If idSueldo = 0 Then
                llenarTabla()
            Else
                llenarTabla()
                cargarDatos()
                btnGaurdar.Text = "Modificar"
            Eliminar.Enabled = True
            End If

            cargando = False
    End Sub
    Private Sub llenarTabla()
        Try


            dgvEmpleados.DataSource = I.consultaPago()
            dgvEmpleados.Columns(0).HeaderText = "X"
            sacarTotal()
            checked = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub dgvEmpleados_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEmpleados.CellClick
        If e.RowIndex = -1 Then

            If e.ColumnIndex = 0 Then
                Dim nfilas As Integer
                nfilas = dgvEmpleados.RowCount()

                If checked = True Then
                    For i As Integer = 0 To nfilas - 1
                        dgvEmpleados.Rows(i).Cells(0).Value = 1

                    Next
                    dgvEmpleados.Columns(0).HeaderText = "X"
                    checked = False

                Else
                    For i As Integer = 0 To nfilas - 1
                        dgvEmpleados.Rows(i).Cells(0).Value = 0
                    Next
                    dgvEmpleados.Columns(0).HeaderText = " "
                    checked = True
                End If

            End If
        End If
        If e.ColumnIndex = 0 And e.RowIndex > -1 Then

            If dgvEmpleados.Rows(e.RowIndex).Cells(0).Value.ToString = "1" Then

                dgvEmpleados.Rows(e.RowIndex).Cells(0).Value = 0

            ElseIf dgvEmpleados.Rows(e.RowIndex).Cells(0).Value.ToString = "0" Then

                dgvEmpleados.Rows(e.RowIndex).Cells(0).Value = 1

            End If
        End If
        sacarTotal()
    End Sub
    Private Sub calculosSueldo(ByVal pDias As Integer)
        llenado = True
        Dim nfilas As Integer
        Dim sueldo As Double = 0
        nfilas = dgvEmpleados.RowCount()
        For i As Integer = 0 To nfilas - 1
            sueldo = Double.Parse(dgvEmpleados.Rows(i).Cells(3).Value.ToString())
            dgvEmpleados.Rows(i).Cells(4).Value = pDias
            dgvEmpleados.Rows(i).Cells(5).Value = sueldo * pDias

        Next
        sacarTotal()
        llenado = False
    End Sub
    Private Sub cargarDatos()
        Dim tabla As DataTable
        tabla = I.consultaSueldo(idGasto)
        Dim nfilas As Integer
        Dim nFilasTabla As Integer
        nFilasTabla = tabla.Rows.Count
        nfilas = dgvEmpleados.RowCount()


        For i As Integer = 0 To nfilas - 1
            dgvEmpleados.Rows(i).Cells(0).Value = 0
            For j As Integer = 0 To nFilasTabla - 1
                If dgvEmpleados.Rows(i).Cells(1).Value.ToString = tabla.Rows(j)(2).ToString() Then
                    dgvEmpleados.Rows(i).Cells(0).Value = 1
                    'sueldo
                    dgvEmpleados.Rows(i).Cells(3).Value = tabla.Rows(j)(3).ToString()
                    'dias
                    dgvEmpleados.Rows(i).Cells(4).Value = tabla.Rows(j)(4).ToString()
                    'Total
                    dgvEmpleados.Rows(i).Cells(5).Value = tabla.Rows(j)(5).ToString()
                End If
            Next


        Next
        sacarTotal()
    End Sub
    Private Sub nmrDiasAPagar_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nmrDiasAPagar.ValueChanged
        If cargando = False Then
            calculosSueldo(nmrDiasAPagar.Value)
        End If

    End Sub
    Private Sub calculosSueldoModificado()
        Dim nFilas As Integer
        Dim sueldo As Double = 0
        Dim pDias As Double


        nfilas = dgvEmpleados.RowCount()
        For i As Integer = 0 To nfilas - 1
            If dgvEmpleados.Rows(i).Cells(3).Value.ToString = "" Then
                dgvEmpleados.Rows(i).Cells(3).Value = 0
            End If
            If dgvEmpleados.Rows(i).Cells(4).Value.ToString = "" Then
                dgvEmpleados.Rows(i).Cells(4).Value = 0
            Else
                If Not (IsNumeric(dgvEmpleados.Rows(i).Cells(4).Value)) Then
                    dgvEmpleados.Rows(i).Cells(4).Value = 0
                End If
            End If
            sueldo = Double.Parse(dgvEmpleados.Rows(i).Cells(3).Value.ToString())
            pDias = dgvEmpleados.Rows(i).Cells(4).Value
            dgvEmpleados.Rows(i).Cells(5).Value = sueldo * pDias

        Next
        sacarTotal()
    End Sub
    Private Sub dgvEmpleados_CurrentCellDirtyStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvEmpleados.CurrentCellDirtyStateChanged
        If llenado = False Then

            calculosSueldoModificado()
        End If

    End Sub
    Private Sub sacarTotal()
        Dim total As Double = 0
        Dim nfilas As Integer
        Dim aux As String
        nfilas = dgvEmpleados.RowCount()

        For i As Integer = 0 To nfilas - 1
            aux = dgvEmpleados.Rows(i).Cells(0).Value.ToString
            If dgvEmpleados.Rows(i).Cells(0).Value.ToString = "1" Then
                total += Double.Parse(dgvEmpleados.Rows(i).Cells(5).Value.ToString())
            End If

        Next
        lblTotal.Text = total.ToString("C2")
    End Sub
    Private Sub dgvEmpleados_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgvEmpleados.KeyPress

    End Sub
    Private Sub dgvEmpleados_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvEmpleados.CurrentCellChanged


    End Sub
    Private Sub dgvEmpleados_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvEmpleados.EditingControlShowing
        ' referencia a la celda  
        Dim validar As TextBox = CType(e.Control, TextBox)

        ' agregar el controlador de eventos para el KeyPress  
        AddHandler validar.KeyPress, AddressOf validar_Keypress
    End Sub
    Private Sub validar_Keypress( _
        ByVal sender As Object, _
        ByVal e As System.Windows.Forms.KeyPressEventArgs)

        ' obtener indice de la columna  
        Dim columna As Integer = dgvEmpleados.CurrentCell.ColumnIndex

        ' verificar columna actual  
        If columna = 3 Or columna = 4 Or columna = 5 Then
            Dim caracter As Char = e.KeyChar

            ' referencia a la celda  
            Dim txt As TextBox = CType(sender, TextBox)

            ' comprobar si es un número con isNumber, si es el backspace, si el caracter  
            ' es el separador decimal, y que no contiene ya el separador  
            If (Char.IsNumber(caracter)) Or _
               (caracter = ChrW(Keys.Back)) Or _
               (caracter = ".") And _
               (txt.Text.Contains(".") = False) Then


                e.Handled = False
            Else
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub btnGaurdar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGaurdar.Click
        Dim nfilas As Integer
        Dim guardados As Integer = 0
        nfilas = dgvEmpleados.RowCount()
        If btnGaurdar.Text = "Guardar" Then
            For j As Integer = 0 To nfilas - 1
                If dgvEmpleados.Rows(j).Cells(0).Value.ToString = "1" Then
                    I.guardarSueldo(idGasto, dgvEmpleados.Rows(j).Cells(1).Value.ToString(), dgvEmpleados.Rows(j).Cells(3).Value.ToString(), dgvEmpleados.Rows(j).Cells(4).Value.ToString(), dgvEmpleados.Rows(j).Cells(5).Value.ToString())
                    guardados += 1
                End If
            Next
            If guardados = 0 Then
                MsgBox("No se a seleccionado ningun empleado. Favor de seleccionar al menos uno.", MsgBoxStyle.OkOnly, "Pull System Soft")
            Else
                If Chck_separar.Checked = False Then
                    I.GuardarSalario(idGasto, "Salario del " + Date.Now.ToString("dd/MM/yyyy"), lblTotal.Text, IdClas1, idClas2, idClas3)
                ElseIf Chck_separar.Checked = True Then
                    For j As Integer = 0 To nfilas - 1
                        If dgvEmpleados.Rows(j).Cells(0).Value.ToString = "1" Then
                            I.GuardarSalario(idGasto, "Salario de: " + dgvEmpleados.Rows(j).Cells(2).Value.ToString, dgvEmpleados.Rows(j).Cells(5).Value, IdClas1, idClas2, idClas3)
                        End If
                    Next
                End If
                btnGaurdar.Text = "Modificar"
                Eliminar.Enabled = True
                PopUp("Guadardo", 90)
            End If
        Else
            guardados = 0
            For j As Integer = 0 To nfilas - 1
                If dgvEmpleados.Rows(j).Cells(0).Value.ToString = "1" Then
                    guardados += 1
                End If
            Next
            If guardados = 0 Then
                MsgBox("No se a seleccionado ningun empleado. Favor de seleccionar al menos uno o elimine el concepto.", MsgBoxStyle.OkOnly, "Pull System Soft")
            Else
                I.EliminarSueldo(idGasto)
                I.EliminarSalario(idGasto)
                For j As Integer = 0 To nfilas - 1
                    If dgvEmpleados.Rows(j).Cells(0).Value.ToString = "1" Then
                        I.guardarSueldo(idGasto, dgvEmpleados.Rows(j).Cells(1).Value.ToString(), dgvEmpleados.Rows(j).Cells(3).Value.ToString(), dgvEmpleados.Rows(j).Cells(4).Value.ToString(), dgvEmpleados.Rows(j).Cells(5).Value.ToString())
                        'guardados += 1
                    End If
                Next
                If Chck_separar.Checked = False Then
                    I.GuardarSalario(idGasto, "Salario del " + Date.Now.ToString("dd/MM/yyyy"), lblTotal.Text, IdClas1, idClas2, idClas3)
                ElseIf Chck_separar.Checked = True Then
                    For j As Integer = 0 To nfilas - 1
                        If dgvEmpleados.Rows(j).Cells(0).Value.ToString = "1" Then
                            I.GuardarSalario(idGasto, "Salario de: " + dgvEmpleados.Rows(j).Cells(2).Value.ToString, dgvEmpleados.Rows(j).Cells(5).Value, IdClas1, idClas2, idClas3)
                        End If
                    Next
                End If
                btnGaurdar.Text = "Modificar"
                Eliminar.Enabled = True
                PopUp("Modificar", 90)
                End If
        End If
    End Sub
    Private Sub Eliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Eliminar.Click
        I.EliminarSalario(idGasto)
        I.EliminarSueldo(idGasto)
        PopUp("Eliminado", 90)
        llenarTabla()
        btnGaurdar.Text = "Guardar"
        Eliminar.Enabled = False

    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class