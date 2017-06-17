Imports MySql.Data.MySqlClient
Public Class dbRestauranteConfiguracion
    Private comm As MySqlCommand
    Public Property idConfig As Integer
    Public Property idInventario1 As Integer
    Public Property idInventario2 As Integer
    Public Property idInventario3 As Integer
    Public Property idInventario4 As Integer
    Public Property idInventario5 As Integer
    Public Property claveProducto1 As String
    Public Property claveProducto2 As String
    Public Property claveProducto3 As String
    Public Property claveProducto4 As String
    Public Property claveProducto5 As String
    Public Property fuente As String
    Public Property tamano As Double
    Public Property colorLibre As String
    Public Property colorOcupado As String
    Public Property colorReservado As String
    Public Property colorSucio As String
    Public Property colorLetraLibre As String
    Public Property colorLetraOcupado As String
    Public Property colorLetraReservado As String
    Public Property colorLetraSucio As String
    Public Property textoLibre As String
    Public Property textoOcupado As String
    Public Property textoReservado As String
    Public Property textoSucio As String
    Public Property clienteDefault As Integer
    Public Property cajaDefault As Integer
    Public Property vendedorDefault As Integer
    Public Property meseroDefault As Integer
    Public Property colorVentanas As String
    Public Property activarTeclado As Boolean
    Public Property horizontal As Integer
    Public Property vertical As Integer

    Public Sub New(idConfig As Integer, conexion As MySqlConnection)
        Me.idConfig = idConfig
        comm = New MySqlCommand
        comm.Connection = conexion
        llenaDatos()
    End Sub

    Public Sub New(ByVal conexion As MySqlConnection)
        comm = New MySqlCommand
        comm.Connection = conexion
        idConfig = -1
    End Sub

    Public Sub agregar(ByVal clave1 As String, ByVal clave2 As String, ByVal clave3 As String, ByVal clave4 As String, ByVal clave5 As String, ByVal fuente As String, ByVal tamano As Double, ByVal colorLibre As String, ByVal colorOcupado As String, ByVal colorReservado As String, colorSucio As String, ByVal colorLetraLibre As String, ByVal colorLetraOcupado As String, ByVal colorLetraReservado As String, colorLetraSucio As String, ByVal textoLibre As String, ByVal textoOcupado As String, ByVal textoReservado As String, textoSucio As String, ByVal clienteDefault As Integer, ByVal cajaDefault As Integer, ByVal vendedorDefault As Integer, ByVal meseroDefault As Integer, ByVal colorVentanas As String, ByVal activarTeclado As Boolean, ByVal horizontal As Integer, ByVal vertical As Integer)
        comm.CommandText = "insert into tblrestauranteconfiguracion(producto1,producto2,producto3,producto4,producto5,fuente,tamanoLetra,colorLibre,colorOcupado,colorReservado,colorSucio,colorLetraLibre,colorLetraOcupado,colorLetraReservado,colorLetraSucio,textoLibre,textoOcupado,textoReservado,textoSucio,clienteDefault,cajaDefault,vendedorDefault,meseroDefault,colorVentanas,activarteclado,horizontal,vertical) "
        comm.CommandText += "values('" + clave1 + "','" + clave2 + "','" + clave3 + "','" + clave4 + "','" + clave5 + "','" + fuente + "'," + tamano.ToString() + ",'" + colorLibre + "','" + colorOcupado + "','" + colorReservado + "','" + colorLetraLibre + "','" + colorLetraReservado + "','" + colorLetraReservado + "','" + textoLibre + "','" + textoOcupado + "','" + textoReservado + "'," + clienteDefault.ToString() + "," + cajaDefault.ToString() + "," + vendedorDefault.ToString() + "," + meseroDefault.ToString + ",'" + colorVentanas + "'," + activarTeclado.ToString() + "," + horizontal.ToString + "," + vertical.ToString + ");"
        Try
            comm.ExecuteNonQuery()
        Catch ex As Exception

        End Try
    End Sub
    Public Function llenaDatos() As Boolean
        comm.CommandText = "select ifnull((select idconfig from tblrestauranteconfiguracion limit 1),0)"
        Dim res = comm.ExecuteScalar
        If res <= 0 Then
            Return False
        End If
        comm.CommandText = "select * from tblrestauranteconfiguracion limit 1"
        Dim dr As MySqlDataReader
        dr = comm.ExecuteReader
        While dr.Read()
            claveProducto1 = dr("producto1")
            claveProducto2 = dr("producto2")
            claveProducto3 = dr("producto3")
            claveProducto4 = dr("producto4")
            claveProducto5 = dr("producto5")
            If Not IsDBNull(dr("fuente")) Then fuente = dr("fuente") Else fuente = ""
            If Not IsDBNull(dr("tamanoLetra")) Then tamano = dr("tamanoLetra") Else tamano = 10
            If Not IsDBNull(dr("colorLibre")) Then colorLibre = dr("colorLibre") Else colorLibre = ""
            If Not IsDBNull(dr("colorOcupado")) Then colorOcupado = dr("colorOcupado") Else colorOcupado = ""
            If Not IsDBNull(dr("colorReservado")) Then colorReservado = dr("colorReservado") Else colorReservado = ""
            If Not IsDBNull(dr("colorSucio")) Then colorSucio = dr("colorSucio") Else colorSucio = ""
            If Not IsDBNull(dr("colorLetraLibre")) Then colorLetraLibre = dr("colorLetraLibre") Else colorLetraLibre = ""
            If Not IsDBNull(dr("colorLetraOcupado")) Then colorLetraOcupado = dr("colorLetraOcupado") Else colorLetraOcupado = ""
            If Not IsDBNull(dr("colorLetraReservado")) Then colorLetraReservado = dr("colorLetraReservado") Else colorLetraReservado = ""
            If Not IsDBNull(dr("colorLetraSucio")) Then colorLetraSucio = dr("colorLetraSucio") Else colorLetraSucio = ""
            If Not IsDBNull(dr("textoLibre")) Then textoLibre = dr("textoLibre") Else textoLibre = ""
            If Not IsDBNull(dr("textoOcupado")) Then textoOcupado = dr("textoOcupado") Else textoOcupado = ""
            If Not IsDBNull(dr("textoReservado")) Then textoReservado = dr("textoReservado") Else textoReservado = ""
            If Not IsDBNull(dr("textoSucio")) Then textoSucio = dr("textoSucio") Else textoSucio = ""
            clienteDefault = dr("clienteDefault")
            cajaDefault = dr("cajaDefault")
            vendedorDefault = dr("vendedorDefault")
            meseroDefault = dr("meseroDefault")
            If Not IsDBNull(dr("colorVentanas")) Then colorVentanas = dr("colorVentanas") Else colorVentanas = ""
            activarTeclado = dr("activarTeclado")
            horizontal = dr("horizontal")
            vertical = dr("vertical")
        End While
        dr.Close()
        Return True
    End Function

    

    Public Sub actualiza(ByVal idConfig As Integer, ByVal clave1 As String, ByVal clave2 As String, ByVal clave3 As String, ByVal clave4 As String, ByVal clave5 As String, ByVal fuente As String, ByVal tamano As Double, ByVal colorLibre As String, ByVal colorOcupado As String, ByVal colorReservado As String, colorSucio As String, ByVal colorLetraLibre As String, ByVal colorLetraOcupado As String, ByVal colorLetraReservado As String, colorLetraSucio As String, ByVal textoLibre As String, ByVal textoOcupado As String, ByVal textoReservado As String, textoSucio As String, ByVal clienteDefault As Integer, ByVal cajaDefault As Integer, ByVal vendedorDefault As Integer, ByVal meseroDefault As Integer, ByVal colorVentanas As String, ByVal activarTeclado As Boolean, ByVal horizontal As Integer, ByVal vertical As Integer)
        comm.CommandText = "update tblrestauranteconfiguracion set "
        If clave1 <> "" Then comm.CommandText += "producto1='" + clave1 + "', "
        If clave2 <> "" Then comm.CommandText += "producto2='" + clave2 + "', "
        If clave3 <> "" Then comm.CommandText += "producto3='" + clave3 + "', "
        If clave4 <> "" Then comm.CommandText += "producto4='" + clave4 + "', "
        If clave5 <> "" Then comm.CommandText += "producto5='" + clave5 + "', "
        comm.CommandText += "fuente='" + fuente + "', tamanoLetra=" + tamano.ToString() + ", colorLetraLibre='" + colorLetraLibre + "', colorLibre='" + colorLibre + "', colorOcupado='" + colorOcupado + "', colorReservado='" + colorReservado + "', colorSucio='" + colorSucio + "', colorLetraLibre='" + colorLetraLibre + "', colorLetraOcupado='" + colorLetraOcupado + "', colorLetraReservado='" + colorLetraReservado + "', colorLetraSucio='" + colorLetraSucio + "', textoLibre='" + textoLibre + "', textoOcupado='" + textoOcupado + "', textoReservado='" + textoReservado + "', textoSucio='" + textoSucio + "', clienteDefault=" + clienteDefault.ToString() + ", cajaDefault=" + cajaDefault.ToString() + ", vendedorDefault=" + vendedorDefault.ToString() + ", meseroDefault=" + meseroDefault.ToString + ", colorVentanas='" + colorVentanas + "', activarTeclado=" + activarTeclado.ToString() + ", horizontal=" + horizontal.ToString + ", vertical=" + vertical.ToString + ""
        'Try
        comm.ExecuteNonQuery()
        'Catch ex As Exception

        'End Try
    End Sub

    Public Function regresaIdConfig() As Integer
        comm.CommandText = "select max(idconfig) from tblrestauranteconfiguracion;"
        Dim res As Integer = comm.ExecuteScalar
        Return res
    End Function
End Class
