Public Class dbImpresionesN
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Documento As Integer
    Public IdSucursal As Integer
    Public Y As Integer
    Public YL As Integer
    Public RG As Integer
    Public Alt As Integer
    Public Modo As Byte
    Public Ancho As Integer
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Documento = 0
        IdSucursal = 0
        Modo = 0
        Ancho = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub DaZonaDetalles(ByVal pidDocumento As Integer, ByVal pIdSucursal As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblimpresiones where documento=" + pidDocumento.ToString + " and idsucursal=" + pIdSucursal.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read Then
            Y = DReader("y")
            YL = DReader("yl")
            RG = DReader("rg")
            Alt = DReader("alt")
            Modo = DReader("modo")
            Ancho = DReader("ancho")
            Documento = pidDocumento
            IdSucursal = pIdSucursal
        End If
        DReader.Close()
    End Sub
    Public Sub ActualizaZonaDetalles(ByVal pDocumento As Integer, ByVal pidSucursal As Integer, ByVal pY As Integer, ByVal pYL As Integer, ByVal prg As Integer, ByVal pAlt As Integer, ByVal pModo As Byte, ByVal pAncho As Integer, ByVal pPermiso As Boolean)
        If pPermiso Then
            Comm.CommandText = "update tblimpresiones set y=" + pY.ToString + ",yl=" + pYL.ToString + ",rg=" + prg.ToString + ",alt=" + pAlt.ToString + ",modo=" + pModo.ToString + ",ancho=" + pAncho.ToString + " where documento=" + pDocumento.ToString + " and idsucursal=" + pidSucursal.ToString
            Comm.ExecuteNonQuery()
        End If
    End Sub
    Public Sub GuardaZonaDetalles(ByVal pidsucursal As Integer)
        '    Public Enum TiposDocumentos
        '    Venta = 0
        '    VentaCotizacion = 1
        '    VentaPedido = 2
        '    VentaRemision = 3
        '    VentaDevolucion = 4
        '    VentaNotadeCredito = 5
        '    VentaNotadeCargo = 6
        '    Compra = 7
        '    CompraCotizacion = 8
        '    CompraPedido = 9
        '    CompraRemision = 10
        '    CompraDevolucion = 11
        '    CompraNotadeCredito = 12
        '    CompraNotadeCargo = 13
        'End Enum
        Comm.CommandText = "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,0," + pidsucursal.ToString + ",66,4,0,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,1," + pidsucursal.ToString + ",66,4,0,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,2," + pidsucursal.ToString + ",66,4,0,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,3," + pidsucursal.ToString + ",66,4,0,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,4," + pidsucursal.ToString + ",66,4,0,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,5," + pidsucursal.ToString + ",66,4,0,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,6," + pidsucursal.ToString + ",66,4,0,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,7," + pidsucursal.ToString + ",66,4,0,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,8," + pidsucursal.ToString + ",66,4,0,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,9," + pidsucursal.ToString + ",66,4,0,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,10," + pidsucursal.ToString + ",66,4,0,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,11," + pidsucursal.ToString + ",66,4,0,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,12," + pidsucursal.ToString + ",66,4,0,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,13," + pidsucursal.ToString + ",66,4,0,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,14," + pidsucursal.ToString + ",66,4,0,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,15," + pidsucursal.ToString + ",66,4,0,864);"

        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,16," + pidsucursal.ToString + ",66,4,1,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,17," + pidsucursal.ToString + ",66,4,1,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,18," + pidsucursal.ToString + ",66,4,1,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,19," + pidsucursal.ToString + ",66,4,1,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,20," + pidsucursal.ToString + ",66,4,1,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,21," + pidsucursal.ToString + ",66,4,1,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,22," + pidsucursal.ToString + ",66,4,1,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,23," + pidsucursal.ToString + ",66,4,1,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,24," + pidsucursal.ToString + ",66,4,1,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,25," + pidsucursal.ToString + ",66,4,1,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,26," + pidsucursal.ToString + ",66,4,1,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,27," + pidsucursal.ToString + ",66,4,1,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,28," + pidsucursal.ToString + ",66,4,1,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,29," + pidsucursal.ToString + ",66,4,1,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,30," + pidsucursal.ToString + ",66,4,1,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,31," + pidsucursal.ToString + ",66,4,1,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,32," + pidsucursal.ToString + ",66,4,0,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,33," + pidsucursal.ToString + ",66,4,0,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,48," + pidsucursal.ToString + ",66,4,1,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,49," + pidsucursal.ToString + ",66,4,1,864);"
        Comm.CommandText += "insert into tblimpresiones(y,yl,documento,idsucursal,rg,alt,modo,ancho) values(500,100,50," + pidsucursal.ToString + ",66,4,1,864);"
        Comm.ExecuteNonQuery()

        Comm.CommandText = "insert into tblimpresionesnodos(x,y,texto,datapropertyname,xl,yl,fuente,fuentesize,fuentestilo,alineacion,tipo,tipodato,visible,documento,idsucursal,tiponodo,conetiqueta,nombre,renglon,clasificacion) " + _
        "select x,y,texto,datapropertyname,xl,yl,fuente,fuentesize,fuentestilo,alineacion,tipo,tipodato,visible,documento," + pidsucursal.ToString + ",tiponodo,conetiqueta,nombre,renglon,clasificacion from tblimpresionesnodos where idsucursal=1"
        Comm.ExecuteNonQuery()
    End Sub

    Public Function DaNodos(ByVal pDocumento As Integer, ByVal pIdSucursal As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        ''verifica si existen impresiones igual o mayores a 1000
        'Comm.CommandText = "select count(*) from tblimpresionesnodos where documento>=1000;"
        'If Comm.ExecuteScalar = 0 Then
        '    Try
        '        'si no existen les aumenta 1000-16 del 16 al 31 y del 48 al 63
        '        Comm.Transaction = Comm.Connection.BeginTransaction
        '        Comm.CommandText = "update tblimpresiones set documento = documento+1000-16 where documento >= 16 and documento <= 31; update tblimpresiones set documento = documento+1000-16 where documento >= 48 and documento <= 63; update tblimpresionesnodos set documento = documento+1000-16 where documento >= 16 and documento <= 31; update tblimpresionesnodos set documento = documento+1000-16 where documento >= 48 and documento <= 63;"
        '        Comm.ExecuteNonQuery()
        '        Comm.Transaction.Commit()
        '    Catch
        '        Comm.Transaction.Rollback()
        '    End Try
        'End If
        Comm.CommandText = "select * from tblimpresionesnodos where documento=" + pDocumento.ToString + " and idsucursal=" + pIdSucursal.ToString + " order by clasificacion,nombre"
        Return Comm.ExecuteReader
    End Function
    Public Function GuardaNodo(ByVal pNodo As NodoImpresionN) As String
        Dim FS As Integer
        Dim SR As String
        Select Case pNodo.Fuente.Style
            Case FontStyle.Bold
                FS = 1
            Case FontStyle.Italic
                FS = 2
            Case FontStyle.Regular
                FS = 0
            Case FontStyle.Strikeout
                FS = 8
            Case FontStyle.Underline
                FS = 4
        End Select
        Comm.CommandText = "insert into tblimpresionesnodos(x,y,texto,datapropertyname,xl,yl,fuente,fuentesize,fuentestilo,alineacion,tipo,tipodato,visible,documento,idsucursal,tiponodo,conetiqueta,nombre,renglon,clasificacion) " + _
        "values(" + pNodo.X.ToString + "," + pNodo.Y.ToString + ",'" + Replace(pNodo.Texto, "'", "''") + "','" + pNodo.DataPropertyName + "'," + pNodo.XL.ToString + "," + pNodo.YL.ToString + ",'" + pNodo.Fuente.Name + "'," + pNodo.Fuente.Size.ToString + "," + FS.ToString + "," + pNodo.Alineacion.ToString + "," + pNodo.Tipo.ToString + "," + pNodo.TipoDato.ToString + "," + pNodo.Visible.ToString + "," + pNodo.Documento.ToString + "," + pNodo.IdSucursal.ToString + "," + pNodo.TipoNodo.ToString + "," + pNodo.ConEtiqueta.ToString + ",'" + Replace(pNodo.Nombre, "'", "''") + "'," + pNodo.Renglon.ToString + "," + pNodo.Clasificacion.ToString + ")"
        Comm.ExecuteNonQuery()
        SR = Comm.CommandText
        Comm.CommandText = "select max(id) from tblimpresionesnodos"
        ID = Comm.ExecuteScalar
        Return SR
    End Function
    Public Sub ActualizaNodo(ByVal pNodo As NodoImpresionN)
        Dim FS As Integer
        Select Case pNodo.Fuente.Style
            Case FontStyle.Bold
                FS = 1
            Case FontStyle.Italic
                FS = 2
            Case FontStyle.Regular
                FS = 0
            Case FontStyle.Strikeout
                FS = 8
            Case FontStyle.Underline
                FS = 4
        End Select
        Comm.CommandText = "update tblimpresionesnodos set " + _
        "x=" + pNodo.X.ToString + ",y=" + pNodo.Y.ToString + ",xl=" + pNodo.XL.ToString + ",yl=" + pNodo.YL.ToString + ",fuente='" + pNodo.Fuente.Name + "',fuentesize=" + pNodo.Fuente.Size.ToString + ",fuentestilo=" + FS.ToString + ",alineacion=" + pNodo.Alineacion.ToString + ",tipo=" + pNodo.Tipo.ToString + ",visible=" + pNodo.Visible.ToString + ",conetiqueta=" + pNodo.ConEtiqueta.ToString + ",texto='" + Replace(pNodo.Texto, "'", "''") + "',renglon=" + pNodo.Renglon.ToString + ",clasificacion=" + pNodo.Clasificacion.ToString + " where id=" + pNodo.id.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminaNodo(ByVal pid As Integer)
        Comm.CommandText = "delete from tblimpresionesnodos where id=" + pid.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Function PasaATexto(ByVal pDocumento As Integer, ByVal pIdSucursal As Integer) As String
        Dim Script As String = ""
        Comm.CommandText = "select * from tblimpresionesnodos where documento=" + pDocumento.ToString + " and idsucursal=" + pIdSucursal.ToString
        Dim dReader As MySql.Data.MySqlClient.MySqlDataReader
        dReader = Comm.ExecuteReader
        Script = "delete from tblimpresionesnodos where documento=" + pDocumento.ToString + " and idsucursal=" + pIdSucursal.ToString + ";" + vbCrLf
        While dReader.Read
            Script += "insert into tblimpresionesnodos(x,y,texto,datapropertyname,xl,yl,fuente,fuentesize,fuentestilo,alineacion,tipo,tipodato,visible,documento,idsucursal,tiponodo,conetiqueta,nombre,renglon,clasificacion) " + _
            "values(" + dReader("x").ToString + "," + dReader("y").ToString + ",'" + dReader("texto") + "','" + dReader("datapropertyname") + "'," + dReader("xl").ToString + "," + dReader("yl").ToString + ",'" + dReader("fuente") + "'," + dReader("fuentesize").ToString + "," + dReader("fuentestilo").ToString + _
            "," + dReader("alineacion").ToString + "," + dReader("tipo").ToString + "," + dReader("tipodato").ToString + "," + dReader("visible").ToString + "," + dReader("documento").ToString + "," + dReader("idsucursal").ToString + "," + dReader("tiponodo").ToString + "," + dReader("conetiqueta").ToString + ",'" + dReader("nombre") + "'," + dReader("renglon").ToString + "," + dReader("clasificacion").ToString + ");" + vbCrLf
        End While
        dReader.Close()
        Return Script
    End Function
    Public Sub CopiaFormato(ByVal pIdSucursalOrigen As Integer, ByVal pIdsucursalDestino As Integer, ByVal PDocumento As Integer)
        Comm.CommandText = "delete from tblimpresionesnodos where documento=" + PDocumento.ToString + " and idsucursal=" + pIdsucursalDestino.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblimpresionesnodos(x,y,texto,datapropertyname,xl,yl,fuente,fuentesize,fuentestilo,alineacion,tipo,tipodato,visible,documento,idsucursal,tiponodo,conetiqueta,nombre,renglon,clasificacion) select x,y,texto,datapropertyname,xl,yl,fuente,fuentesize,fuentestilo,alineacion,tipo,tipodato,visible,documento," + pIdsucursalDestino.ToString + ",tiponodo,conetiqueta,nombre,renglon,clasificacion from tblimpresionesnodos where documento=" + PDocumento.ToString + " and idsucursal=" + pIdSucursalOrigen.ToString
        Comm.ExecuteNonQuery()
    End Sub
End Class
