Imports MySql.Data.MySqlClient

Public Class frmSemillasConfiguracion
    '  Dim conexion As MySqlConnection
    '   Dim ruta As String = "Data Source=localhost;Database=db_services;Uid=root;Password=masterdb"
    Dim comm As New MySqlCommand
    Dim existe As Boolean
    Dim idProducto As Integer
    Public boleta As dbSemillasBoleta
    Dim boletas As dbSemillasBoleta
    Enum tipo
        producto = 0
        boleta = 1
    End Enum
    Dim op As tipo
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection, ByVal pIdProducto As Integer)

        InitializeComponent()
        'Conexion = New MySqlConnection(ruta)
        'conexion.Open()
        ' comm = New MySqlCommand
        comm.Connection = Conexion
        idProducto = pIdProducto
        'existe = False
        'obtenerConfiguracionGuardada(idProducto)

    End Sub

    Public Sub New(ByVal conexion As MySql.Data.MySqlClient.MySqlConnection, ByVal boleta As dbSemillasBoleta)
        InitializeComponent()
        Me.boleta = boleta
        comm.Connection = conexion
        boletas = New dbSemillasBoleta(conexion)
        llenaDatosBoleta()
        op = tipo.boleta
        existe = True
        Me.Width = 297
    End Sub

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        InitializeComponent()
        'conexion = New MySqlConnection(ruta)
        'conexion.Open()
        'comm = New MySqlCommand
        comm.Connection = Conexion
    End Sub
    Public Sub guardarCambios()
        If op = tipo.boleta Then
            boleta.porcentajeHumedad = Double.Parse(txtHumedad.Text)
            boleta.porcentajeImpurezas = Double.Parse(txtImpurezas.Text)
            boleta.porcentajeGranoD = Double.Parse(txtGranoDanado.Text)
            boleta.porcentajeGranoQ = Double.Parse(txtGranoQuebrado.Text)
        Else
            If existe Then
                comm.CommandText = "update tblsemillasconfiguracion set humedad=" + txtHumedad.Text.ToString() + ", impurezas=" + txtImpurezas.Text.ToString() + ", granoDanado=" + txtGranoDanado.Text.ToString() + ", granoQuebrado=" + txtGranoQuebrado.Text.ToString() + ", editar=false,"
                comm.CommandText += "castigoH=" + txtCastigoH.Text.ToString() + ", castigoI=" + txtCastigoI.Text.ToString() + ", castigoQ=" + txtCastigoQ.Text.ToString() + ", castigoD=" + txtCastigoD.Text.ToString() + " where idProducto=" + idProducto.ToString()
            Else
                comm.CommandText = "insert into tblsemillasconfiguracion(humedad,impurezas,granoDanado,granoQuebrado,editar,idProducto,castigoH,castigoI,castigoQ,castigoD) values(" + txtHumedad.Text.ToString() + "," + txtImpurezas.Text.ToString() + "," + txtGranoDanado.Text.ToString() + "," + txtGranoQuebrado.Text.ToString() + ",false," + idProducto.ToString() + ","
                comm.CommandText += txtCastigoH.Text.ToString() + "," + txtCastigoI.Text.ToString() + "," + txtCastigoQ.Text.ToString() + "," + txtCastigoD.Text.ToString() + ");"
            End If
            Try
                comm.ExecuteNonQuery()
                MsgBox("configuración guardada")
            Catch ex As Exception
                MsgBox("no se pudo guardar la configuración: " + ex.ToString)
            End Try
        End If
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        guardarCambios()
        Dispose()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Dispose()
    End Sub

    Private Sub obtenerConfiguracionGuardada(ByVal id As Integer)
        Dim dr As MySqlDataReader
        comm.CommandText = "select * from tblsemillasconfiguracion where idProducto=" + id.ToString + ";"
        dr = comm.ExecuteReader
        If dr.HasRows = False Then
            existe = False
        Else
            existe = True
            While dr.Read()
                txtHumedad.Text = dr.GetDouble("humedad")
                txtImpurezas.Text = dr.GetDouble("impurezas")
                txtGranoDanado.Text = dr.GetDouble("granoDanado")
                txtGranoQuebrado.Text = dr.GetDouble("granoQuebrado")
                txtCastigoH.Text = dr("castigoH")
                txtCastigoI.Text = dr("castigoI")
                txtCastigoQ.Text = dr("castigoQ")
                txtCastigoD.Text = dr("castigoD")
                'checkFecha.Checked = dr.GetBoolean(5)
            End While
        End If
        dr.Close()

    End Sub

    Private Sub frmSemillasConfiguracion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        obtenerConfiguracionGuardada(idProducto)
    End Sub

    Private Sub llenaDatosBoleta()
        txtHumedad.Text = boleta.porcentajeHumedad
        txtImpurezas.Text = boleta.porcentajeImpurezas
        txtGranoDanado.Text = boleta.porcentajeGranoD
        txtGranoQuebrado.Text = boleta.porcentajeGranoQ
    End Sub

End Class