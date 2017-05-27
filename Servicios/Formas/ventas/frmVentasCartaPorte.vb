Public Class frmVentasCartaPorte
    Public IdEquipo As Integer
    Dim Hay As Boolean
    Dim ClaveAnterior As String
    Dim IdVenta As Integer
    Dim Estado As Byte
    Public Sub New(ByVal pIdVenta As Integer, ByVal pEstado As Byte)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        IdVenta = pIdVenta
        Estado = pEstado
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Private Sub Nuevo()
        Try

            'ConsultaOn = False
            Dim CP As New dbVentasCartaPorte(IdVenta, MySqlcon)
            If CP.Origen = "Nohay" Then
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                TextBox5.Text = ""
                TextBox7.Text = ""
                TextBox8.Text = ""
                TextBox9.Text = ""
                TextBox10.Text = ""
                TextBox6.Text = ""
                DateTimePicker1.Value = Date.Now
                DateTimePicker2.Value = Date.Now
                'TextBox1.BackColor = Color.FromKnownColor(KnownColor.Window)
                'TextBox6.BackColor = Color.FromKnownColor(KnownColor.Window)
                'Button1.Text = "Guardar"
                Button2.Enabled = False
                'ConsultaOn = True
                Hay = False
            Else
                Hay = True
                TextBox1.Text = CP.Origen
                TextBox2.Text = CP.Chofer
                TextBox3.Text = CP.Mercancia
                TextBox4.Text = CP.Matricula
                TextBox5.Text = CP.Peso
                TextBox7.Text = CP.Destino
                TextBox6.Text = CP.ValorUnitario
                TextBox8.Text = CP.ValorDeclarado
                TextBox9.Text = CP.Pedimento
                TextBox10.Text = CP.Referencia
                IdEquipo = CP.ID
                DateTimePicker1.Value = CDate(CP.Fecha)
                DateTimePicker2.Value = CDate(CP.FechaPedimento)
                'TextBox1.BackColor = Color.FromKnownColor(KnownColor.Window)
                'TextBox6.BackColor = Color.FromKnownColor(KnownColor.Window)
                'Button1.Text = "Guardar"
                'Button2.Enabled = False
                If Estado = Estados.Guardada Then Button2.Enabled = False
                'ConsultaOn = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim P As New dbVentasCartaPorte(MySqlcon)
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""
            If TextBox1.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar el origen."
                'TextBox1.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If TextBox7.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar el destino."
                'TextBox1.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If TextBox3.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar la mercancia."
                'TextBox1.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If Estado = Estados.Guardada Or Estado = Estados.Cancelada Then
                NoErrores = False
                MensajeError += vbCrLf + " El documento esta guardado no se puede modificar."
                '    TextBox6.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If NoErrores Then
                If Hay = False Then
                    If MsgBox("Al guardar estos datos, el documento se convierte a tipo ""traslado"". ¿Desea continuar?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        P.Guardar(TextBox1.Text, TextBox7.Text, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, Format(DateTimePicker1.Value, "yyyy/MM/dd"), IdVenta, TextBox6.Text, TextBox8.Text, TextBox10.Text, TextBox9.Text, Format(DateTimePicker2.Value, "yyyy/MM/dd"))
                    End If
                    'PopUp("Guardado", 90)
                    'Nuevo()
                Else
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        P.Modificar(IdEquipo, TextBox1.Text, TextBox7.Text, TextBox2.Text, TextBox4.Text, TextBox3.Text, TextBox5.Text, Format(DateTimePicker1.Value, "yyyy/MM/dd"), IdVenta, TextBox6.Text, TextBox8.Text, TextBox10.Text, TextBox9.Text, Format(DateTimePicker2.Value, "yyyy/MM/dd"))
                        'PopUp("Modificado", 90)
                        'Nuevo()
                    End If
                End If
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Else
                MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) Then
                Dim P As New dbVentasCartaPorte(MySqlcon)
                P.Eliminar(IdEquipo)
                PopUp("Eliminado", 90)
                Hay = False
                Nuevo()
                TextBox1.Focus()
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar este cliente debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MsgBox(exm.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub frmClientesEquipos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Nuevo()
    End Sub
End Class