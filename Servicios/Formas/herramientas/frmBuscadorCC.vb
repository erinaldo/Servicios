Imports System.Text.RegularExpressions
Public Class frmBuscadorCC
    Dim TipoB As Integer
    Public ID As Integer
    'Public contador As Integer
    Public Cuenta As String
    Public descripcion As String
    Public Nivel As String
    Public Tipo As String
    Dim P As New dbContabilidadClasificacion(MySqlcon)
    Dim llenando As Boolean = False
    Dim negrita As New Font("Arial", 9, FontStyle.Bold)
    Dim filtroCuenta As String
    Dim SoloUltimoNivel As Boolean
    Public Sub New(PCuenta As String, pSoloUNivel As Boolean)

        ' This call is required by the designer.
        InitializeComponent()
        filtroCuenta = PCuenta
        SoloUltimoNivel = pSoloUNivel
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub frmBuscadorCC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.DialogResult = Windows.Forms.DialogResult.Cancel


        llenando = True
        If filtroCuenta <> "" Then
            '
            txtBuscar.Text = filtroCuenta
            txtBuscar.Select(txtBuscar.Text.Length, txtBuscar.Text.Length)
        End If
        'cmbBuscarNivel.SelectedIndex = 0
        llenando = False
        FiltroTodos()
        If DataGridView1.RowCount > 0 Then DataGridView1.CurrentCell = DataGridView1.Item(1, 0)
        txtBuscar.Focus()
    End Sub
    Private Sub Consulta()

        FiltroTodos()

    End Sub
    Private Sub FiltroTodos()
        If llenando = False Then

            Try
                Dim PrimerCeldaRow As Integer = -1
                'Dim cuenta As String = txtBuscar.Text
                Dim Palabras() As String
                Dim txtAbuscar As String = ""
                Dim txtAbuscar2 As String = ""
                Palabras = txtBuscar.Text.Split(Chr(32))
                For Each palabra As String In Palabras
                    If Regex.IsMatch(palabra, "[A-Z]|[a-z]") Then
                        txtAbuscar += " " + palabra
                    Else
                        txtAbuscar2 += " " + palabra
                    End If
                Next

                If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex

                'If Regex.IsMatch(cuenta, "[A-Z]|[a-z]") Then
                'DataGridView1.DataSource = P.filtro("", 0, cuenta)
                'Else
                DataGridView1.DataSource = P.Consulta(txtAbuscar2.Trim, 0, txtAbuscar.Trim, True, False)
                'End If

                DataGridView1.Columns(0).HeaderText = "id"
                DataGridView1.Columns(1).HeaderText = "Cuenta"
                DataGridView1.Columns(2).HeaderText = "Descripción"
                DataGridView1.Columns(3).HeaderText = "Nivel"
                DataGridView1.Columns(4).HeaderText = "Tipo"
                DataGridView1.Columns(5).HeaderText = "Naturaleza"
                DataGridView1.Columns(6).HeaderText = "Agrupador SAT"
                DataGridView1.Columns(0).Visible = False
                DataGridView1.Columns(3).Visible = False
                DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                DataGridView1.Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DataGridView1.Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DataGridView1.Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DataGridView1.Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DataGridView1.Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DataGridView1.Columns(5).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DataGridView1.Columns(6).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

                If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try
            '    End If
        End If
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        RegresaValor()
    End Sub
    Private Sub RegresaValor()
        Dim P As New dbCContables(MySqlcon)
        Try
            If DataGridView1.RowCount > 0 Then
                If DataGridView1.RowCount = 1 Then
                    Nivel = DataGridView1.Item(3, 0).Value
                    ID = DataGridView1.Item(0, 0).Value
                    If SoloUltimoNivel Then
                        If P.UtlimoNivel(ID, Nivel) = 0 Then
                            Cuenta = P.Cuenta(ID, Nivel)
                            descripcion = P.D1
                            Me.DialogResult = Windows.Forms.DialogResult.OK
                        Else
                            MsgBox("Debe seleccionar cuentas de último nivel.", MsgBoxStyle.Critical, GlobalNombreApp)
                        End If
                    Else
                        Cuenta = P.Cuenta(ID, Nivel)
                        descripcion = P.D1
                        Me.DialogResult = Windows.Forms.DialogResult.OK
                    End If
                Else
                    Nivel = DataGridView1.Item(3, DataGridView1.CurrentCell.RowIndex).Value
                    ID = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
                    Cuenta = DataGridView1.Item(1, DataGridView1.CurrentCell.RowIndex).Value
                    If SoloUltimoNivel Then
                        If P.UtlimoNivel(ID, Nivel) = 0 Then
                            Cuenta = P.Cuenta(ID, Nivel)
                            descripcion = P.D1
                            Me.DialogResult = Windows.Forms.DialogResult.OK
                        Else
                            MsgBox("Debe seleccionar cuentas de último nivel.", MsgBoxStyle.Critical, GlobalNombreApp)
                        End If
                    Else
                        Cuenta = P.Cuenta(ID, Nivel)
                        descripcion = P.D1
                        Me.DialogResult = Windows.Forms.DialogResult.OK
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub RegresaValorEnter()
        Dim P As New dbCContables(MySqlcon)
        Try
            If DataGridView1.RowCount > 0 Then
                If DataGridView1.RowCount = 1 Then
                    Nivel = DataGridView1.Item(3, 0).Value
                    ID = DataGridView1.Item(0, 0).Value
                    If P.UtlimoNivel(ID, Nivel) = 0 Then
                        Cuenta = P.Cuenta(ID, Nivel)
                        descripcion = P.D1
                        Me.DialogResult = Windows.Forms.DialogResult.OK
                    Else
                        MsgBox("Debe seleccionar cuentas de último nivel.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If

                Else
                    Nivel = DataGridView1.Item(3, DataGridView1.CurrentCell.RowIndex - 1).Value
                    ID = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex - 1).Value
                    Cuenta = DataGridView1.Item(1, DataGridView1.CurrentCell.RowIndex - 1).Value
                    If P.UtlimoNivel(ID, Nivel) = 0 Then
                        Cuenta = P.Cuenta(ID, Nivel)
                        descripcion = P.D1
                        Me.DialogResult = Windows.Forms.DialogResult.OK
                    Else
                        MsgBox("Debe seleccionar cuentas de último nivel.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If

                End If





            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        ID = 0
    End Sub


    Private Sub txtBuscarCuenta_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub


    Private Sub txtBuscarDescripicion_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            'cmbBuscarNivel.Focus()
        End If
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        RegresaValor()
    End Sub


    Private Sub DataGridView1_RowsAdded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles DataGridView1.RowsAdded
        'Dim nivel1 As String
        'Dim numfilas As Integer
        'numfilas = DataGridView1.RowCount 'cuenta las filas del DataGrid
        'numfilas = numfilas - 1
        'For i As Integer = 0 To numfilas
        '    nivel1 = Convert.ToString(DataGridView1.Item(3, DataGridView1.Rows(i).Index).Value)

        '    If nivel1 = "1" Then
        '        DataGridView1.Rows(i).DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Bold)
        '    Else
        '        DataGridView1.Rows(i).DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
        '    End If

        'Next
    End Sub

    Private Sub DataGridView1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DataGridView1.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            RegresaValorEnter()
        End If
    End Sub

  

    Private Sub txtBuscarCuenta_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        FiltroTodos()

    End Sub


    Private Sub cmbBuscarNivel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        FiltroTodos()
    End Sub

    Private Sub cmbBuscarNivel_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            DataGridView1.Focus()
        End If

    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If DataGridView1.Item(3, e.RowIndex).Value.ToString = "1" Then
            e.CellStyle.Font = negrita
        End If
    End Sub

    Private Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        FiltroTodos()
    End Sub

    Private Sub txtBuscar_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBuscar.KeyDown
        If e.KeyCode = Keys.Down Then
            If DataGridView1.RowCount > 1 Then
                If DataGridView1.CurrentRow.Index < DataGridView1.RowCount - 1 Then DataGridView1.CurrentCell = DataGridView1.Item(2, DataGridView1.CurrentCell.RowIndex + 1)
            End If
        End If
        If e.KeyCode = Keys.Up Then
            If DataGridView1.RowCount > 1 Then
                If DataGridView1.CurrentRow.Index > 0 Then DataGridView1.CurrentCell = DataGridView1.Item(2, DataGridView1.CurrentCell.RowIndex - 1)
            End If
        End If
    End Sub
End Class