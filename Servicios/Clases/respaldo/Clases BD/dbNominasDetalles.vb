Public Class dbNominasDetalles
    Public ID As Integer
    Public NuevoConcepto As Boolean
    Public Tipo As Byte
    Public IdNomina As Integer
    Public TipoPercepcionDeduccion As Integer
    Public Clave As String
    Public Concepto As String
    Public ImporteGravado As Double
    Public ImporteExento As Double

    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Tipo = 0
        IdNomina = 0
        TipoPercepcionDeduccion = 0 '0 Percepcion 1 Deduccion
        Clave = ""
        Concepto = ""
        ImporteExento = 0
        ImporteGravado = 0

        NuevoConcepto = False
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Public Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblnominadetalles where iddetalle=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Tipo = DReader("tipo")
            IdNomina = DReader("idnomina")
            TipoPercepcionDeduccion = DReader("tipodetalle")
            Clave = DReader("clave")
            Concepto = DReader("concepto")
            ImporteGravado = DReader("importegravado")
            ImporteExento = DReader("importeexento")
        End If
        DReader.Close()
        'If Idinventario > 1 Then Inventario = New dbInventario(Idinventario, Comm.Connection)
        'If IdVariante > 1 Then Producto = New dbProductosVariantes(IdVariante, Comm.Connection)
        'Moneda = New dbMonedas(IdMoneda, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIdNomina As Integer, ByVal pTipo As Byte, ByVal pTipoDetalle As Integer, ByVal pClave As String, ByVal pConcepto As String, ByVal pImpGravado As Double, ByVal pImpExento As Double)
        
        NuevoConcepto = True
        Comm.CommandText = "insert into tblnominadetalles(idnomina,tipo,tipodetalle,clave,concepto,importegravado,importeexento) values(" + pIdNomina.ToString + "," + pTipo.ToString + "," + pTipoDetalle.ToString + ",'" + Replace(pClave, "'", "''") + "','" + Replace(pConcepto, "'", "''") + "'," + pImpGravado.ToString + "," + pImpExento.ToString + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select ifnull((select max(iddetalle) from tblnominadetalles),0)"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pTipo As Byte, ByVal pTipoDetalle As Integer, ByVal pClave As String, ByVal pConcepto As String, ByVal pImpGravado As Double, ByVal pImpExento As Double)
        ID = pID
        Comm.CommandText = "update tblnominadetalles set tipo=" + pTipo.ToString + ",tipodetalle=" + pTipoDetalle.ToString + ",clave='" + Replace(pClave, "'", "''") + "',concepto='" + Replace(pConcepto, "'", "''") + "',importegravado=" + pImpGravado.ToString + ",importeexento=" + pImpExento.ToString + " where iddetalle=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblnominadetalles where iddetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdVenta As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvi.iddetalle,if(tvi.tipodetalle=0,'Percepción','Deducción') as tipod,tvi.tipo,tvi.clave,tvi.concepto,tvi.importegravado,tvi.importeexento from tblnominadetalles as tvi where tvi.idnomina=" + pIdVenta.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblnominadetalles")
        Return DS.Tables("tblnominadetalles").DefaultView
    End Function
    Public Function ConsultaReader(ByVal pIdVenta As Integer, ByVal pTipoDetalle As Byte) As MySql.Data.MySqlClient.MySqlDataReader
        If pTipoDetalle <> 3 Then
            Comm.CommandText = "select tvi.iddetalle,if(tvi.tipodetalle=0,'Percepción','Deducción') as tipod,tvi.clave,if(tvi.tipodetalle=0,(select descripcion from tblpercepciones where clave=tvi.tipo),(select descripcion from tbldeducciones where clave=tvi.tipo)) as tipod,tvi.concepto,tvi.importegravado,tvi.importeexento,tvi.tipo from tblnominadetalles as tvi where tvi.idnomina=" + pIdVenta.ToString + " and tvi.tipodetalle=" + pTipoDetalle.ToString
        Else
            Comm.CommandText = "select tvi.iddetalle,if(tvi.tipodetalle=0,'Percepción','Deducción') as tipod,tvi.clave,if(tvi.tipodetalle=0,(select descripcion from tblpercepciones where clave=tvi.tipo),(select descripcion from tbldeducciones where clave=tvi.tipo)) as tipod,tvi.concepto,tvi.importegravado,tvi.importeexento,tvi.tipo from tblnominadetalles as tvi where tvi.idnomina=" + pIdVenta.ToString
        End If
        Return Comm.ExecuteReader
    End Function
End Class
