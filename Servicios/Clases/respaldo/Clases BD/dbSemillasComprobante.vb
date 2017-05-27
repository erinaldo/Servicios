'Imports Microsoft.Office.Interop
Imports MySql.Data.MySqlClient
'Imports System.Data.OleDb
'Imports Microsoft.Office.Interop
Public Class dbSemillasComprobante

#Region "propiedades"
    Public Property id As Integer
    Public Property idCliente As Integer
    Public Property riego As Integer
    Public Property superficie As Double
    Public Property idCompra As Integer
    Public Property numComprobante As String
    Public Property disponeContrato As Boolean
    Public Property socioPersonaMoral As String
    Public Property volumen As Double
    Public Property estado As Integer
    Public Property serie As String
    Public Property folio As String
    Public Property idSucursal As Integer
    Public Property nombreRepresentante As String
    Public Property apellidoP As String
    Public Property apellidoM As String
    Public Fecha As String
    Public NR As String
    Public AP As String
    Public AM As String
#End Region

#Region "constructores"
    Public Sub New(conexion As MySql.Data.MySqlClient.MySqlConnection)
        comm = New MySqlCommand
        comm.Connection = conexion
    End Sub

    Public Sub New(ByVal id As Integer, conexion As MySql.Data.MySqlClient.MySqlConnection)
        Me.id = id
        comm = New MySqlCommand
        comm.Connection = conexion
    End Sub
    Public Sub New(ByVal id As Integer, ByVal idCliente As Integer, ByVal riego As Integer, ByVal superficie As Double, ByVal idFactura As Integer, ByVal numComprobante As String, ByVal disponeContrato As Boolean, ByVal socioPersonaMoral As String, ByVal volumen As Double, conexion As MySql.Data.MySqlClient.MySqlConnection)
        Me.id = id
        Me.idCliente = idCliente
        Me.riego = riego
        Me.superficie = superficie
        Me.idCompra = idFactura
        Me.numComprobante = numComprobante
        Me.disponeContrato = disponeContrato
        Me.socioPersonaMoral = socioPersonaMoral
        Me.volumen = volumen
        comm = New MySqlCommand
        comm.Connection = conexion
    End Sub

    Public Sub New(ByVal idCliente As Integer, ByVal riego As Integer, ByVal superficie As Double, ByVal idFactura As Integer, ByVal numComprobante As String, ByVal disponeContrato As Boolean, ByVal socioPersonaMoral As String, ByVal volumen As Double, conexion As MySql.Data.MySqlClient.MySqlConnection)
        Me.idCliente = idCliente
        Me.riego = riego
        Me.superficie = superficie
        Me.idCompra = idFactura
        Me.numComprobante = numComprobante
        Me.disponeContrato = disponeContrato
        Me.socioPersonaMoral = socioPersonaMoral
        Me.volumen = volumen
        comm = New MySqlCommand
        comm.Connection = conexion
    End Sub

#End Region
    Private comm As MySqlCommand
    Private ruta As String = "C:/"

    Public Sub agregar(ByVal comprobante As dbSemillasComprobante)
        comm.CommandText = "insert into tblsemillascomprobante(idlciente,riego,superficie,idCompra,numcomprobante,disponecontrato,sociopersonamoral,volumen,estado,sucursal,folio,serie,nombreRepresentante,apellidoP,apellidoM,fecha) " +
            "values(" + comprobante.idCliente.ToString() + "," +
                    comprobante.riego.ToString() + "," +
                    comprobante.idCompra.ToString() + "," +
                    "'" + comprobante.numComprobante + "'," +
                    comprobante.disponeContrato.ToString() + "," +
                    "'" + comprobante.socioPersonaMoral + "'," +
                    comprobante.volumen.ToString() + "," +
                    comprobante.estado.ToString() + "," +
                    comprobante.idSucursal.ToString() + "," +
                    "'" + comprobante.folio + "'," +
                    "'" + comprobante.serie + "'" +
                    "'" + comprobante.apellidoP + "'" +
                    "'" + comprobante.apellidoM + "','" + comprobante.Fecha + "');"
        Try
            comm.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("no se puede agregar: " + ex.ToString())
        End Try
    End Sub

    Public Function buscar(ByVal id As Integer) As dbSemillasComprobante
        Dim dr As MySqlDataReader
        Dim c As New dbSemillasComprobante(comm.Connection)
        comm.CommandText = "select * from tblsemillascomprobante where id=" + id.ToString() + ";"
        dr = comm.ExecuteReader
        Try
            If dr.HasRows Then
                While dr.Read()
                    c.id = dr("id")
                    c.idCliente = dr("idcliente")
                    c.riego = dr("riego")
                    c.superficie = dr("superficie")
                    c.idCompra = dr("idCompra")
                    c.numComprobante = dr("numcomprobante")
                    c.disponeContrato = dr("disponecontrato")
                    c.socioPersonaMoral = dr("sociopersonamoral")
                    c.volumen = dr("volumen")
                    If IsDBNull(dr("estado")) Then
                        c.estado = Estados.Inicio
                    Else
                        c.estado = dr("estado")
                    End If
                    If IsDBNull(dr("sucursal")) Then
                        c.idSucursal = GlobalIdSucursalDefault
                    Else
                        c.idSucursal = dr("sucursal")
                    End If
                    If IsDBNull(dr("serie")) Then
                        c.serie = ""
                    Else
                        c.serie = dr("serie")
                    End If
                    If IsDBNull(dr("folio")) Then
                        c.folio = c.numComprobante
                    Else
                        c.folio = dr("folio")
                    End If
                    If IsDBNull(dr("nombreRepresentante")) Then
                        c.nombreRepresentante = ""
                    Else
                        c.nombreRepresentante = dr("nombreRepresentante")
                    End If
                    If IsDBNull(dr("apellidoP")) Then
                        c.apellidoP = ""
                    Else
                        c.apellidoP = dr("apellidoP")
                    End If
                    If IsDBNull(dr("apellidoM")) Then
                        c.apellidoM = ""
                    Else
                        c.apellidoM = dr("apellidoM")
                    End If
                    c.Fecha = dr("fecha")
                End While
                dr.Close()
                Return c
            Else
                dr.Close()
                Return Nothing
            End If
        Catch ex As Exception
            dr.Close()
            Return Nothing
        End Try
    End Function

    Public Sub actualiza(ByVal comprobante As dbSemillasComprobante)
        comm.CommandText = "update tblsemillascomprobante set idcliente=" + comprobante.idCliente.ToString() + "," +
            " riego=" + comprobante.riego.ToString() + "," +
            " superficie=" + comprobante.superficie.ToString() + "," +
            " idcompra=" + comprobante.idCompra.ToString() + "," +
            " numcomprobante='" + comprobante.numComprobante + "'," +
            " disponecontrato=" + comprobante.disponeContrato.ToString() + "," +
            " sociopersonamoral='" + comprobante.socioPersonaMoral + "'," +
            " volumen=" + comprobante.volumen.ToString() + ", estado=" + comprobante.estado.ToString() + ", " +
            "sucursal=" + comprobante.idSucursal.ToString() + ", folio='" + comprobante.folio + "'," +
            "serie='" + comprobante.serie + "'," +
            "nombreRepresentante='" + comprobante.nombreRepresentante + "', " +
            "apellidoP='" + comprobante.apellidoP + "', " +
            "apellidoM='" + comprobante.apellidoM + "',fecha='" + comprobante.Fecha + "' " +
            " where id=" + comprobante.id.ToString() + ";"
        Try
            comm.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("no se puede modificar: " + ex.ToString())
        End Try
    End Sub

    Public Sub eliminar(ByVal id As Integer)
        comm.CommandText = "delete from tblsemillascomprobante where id=" + id.ToString() + ";"
        Try
            comm.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("no se puede eliminar: " + ex.ToString())
        End Try
    End Sub

    Public Function vistaComprobantes() As DataView
        Dim ds As New DataSet
        comm.CommandText = "select d.id as id, d.numcomprobante as folio, cli.nombre as cliente, format(d.volumen,2) as volumen, d.superficie as superficie, case d.estado when 0 then 'INICIO' when 1 then 'SIN GUARDAR' when 2 then 'PENDIENTE' when 3 then 'GUARDADA' when 4 then 'CANCELADA' end as estado from tblsemillascomprobante as d " +
            "inner join tblclientes as cli on d.idcliente=cli.idcliente;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "detalles")
        Return ds.Tables("detalles").DefaultView
    End Function

    Public Function crearComprobante() As Integer
        comm.CommandText = "insert into tblsemillascomprobante(idcliente,riego,superficie,idcompra,numcomprobante,disponecontrato,sociopersonamoral,volumen,estado,sucursal,folio,serie,fecha) values(null,null,null,null,null,null,null,null,0,null,null,null,'" + Date.Now.ToString("yyyy/MM/dd") + "');"
        comm.ExecuteNonQuery()
        comm.CommandText = "select max(id) as id from tblsemillascomprobante;"
        Dim i As Integer = comm.ExecuteScalar
        Return i
    End Function

    Public Function buscarCliente(ByVal clave As String, pFecha1 As String, pFecha2 As String, pFolio As String) As DataView
        Dim ds As New DataSet
        comm.CommandText = "select d.id as id,d.fecha, d.numcomprobante as folio, cli.nombre as cliente, d.volumen as volumen, d.superficie as superficie, case d.estado when 0 then 'INICIO' when 1 then 'SIN GUARDAR' when 2 then 'PENDIENTE' when 3 then 'GUARDADA' when 4 then 'CANCELADA' end as estado from tblsemillascomprobante as d " +
            "inner join tblclientes as cli on d.idcliente=cli.idcliente where d.fecha>='" + pFecha1 + "' and d.fecha<='" + pFecha2 + "'"
        If clave <> "" Then
            comm.CommandText += " and cli.clave like '%" + clave.Replace("'", "''") + "'"
        End If
        If pFolio <> "" Then
            comm.CommandText += " and d.numcomprobante like '%" + folio.Replace("'", "''") + "%'"
        End If
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "detalles")
        Return ds.Tables("detalles").DefaultView
    End Function

    Public Function buscaFolio(ByVal folio As String) As DataView
        Dim ds As New DataSet
        comm.CommandText = "select d.id as id, d.numcomprobante as folio, cli.nombre as cliente, d.volumen as volumen, d.superficie as superficie, d.estado as estado from tblsemillascomprobante as d " +
            "inner join tblclientes as cli on d.idcliente=cli.idcliente where d.numcomprobante like '%" + folio + "%';"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "detalles")
        Return ds.Tables("detalles").DefaultView
    End Function

    Public Sub exportar(Optional ByVal idComprobante As Integer = 0)
        Dim ds As New DataSet
        comm.CommandText = "select cli.nombre as nombre, cli.estado as estado, cli.municipio as municipio, cli.rfc as RFC, prod.nombre as Cultivo, c.riego as Riego," +
"c.superficie as Superficie,(select sum(tvi.cantidad) from tblcomprasdetalles as tvi where idcompra=com.idcompra) as Volumen," +
"(select art.precio from tblinventarioprecios as art where art.idinventario=prod.idinventario and idlista=1)as Precio," +
"sociopersonamoral from tblsemillascomprobante as c inner join tblclientes as cli on c.idcliente=cli.idcliente " +
"inner join tblcompras as com on c.idCompra=com.idCompra inner join tblcomprasdetalles as tvi on com.idcompra=tvi.idcompra " +
"inner join tblinventario as prod on tvi.idinventario=prod.idinventario "
        '"inner join tblinventario as prod on tvi.idinventario=prod.idinventario "
        If idComprobante > 0 Then
            comm.CommandText += " where c.id=" + idComprobante.ToString()
        End If

        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "comprobantes")
        If ExportarDataTableAExcel("Comprobantes", ds.Tables("comprobantes")) Then
            PopUp("Exportado", 30)
        Else
            MsgBox("no se pudo exportar.")
        End If
        'Dim dgv As New DataGridView()
        'dgv.DataSource = ds.Tables("comprobantes").DefaultView
        'exportarAExcel(dgv)
    End Sub

    Shared Function ExportarDataTableAExcel(ByVal Titulo As String, _
                                            ByVal Tabla As DataTable) As Boolean
        Try
            'Creamos las variables
            Dim op As New dbOpciones(MySqlcon)
            Dim suc As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
            Dim exApp As New Microsoft.Office.Interop.Excel.Application
            Dim exLibro As Microsoft.Office.Interop.Excel.Workbook
            Dim exHoja As Microsoft.Office.Interop.Excel.Worksheet
            Dim filaTabla As System.Data.DataRow

            'Añadimos el Libro al programa, y la hoja al libro
            exLibro = exApp.Workbooks.Add
            exHoja = exLibro.Worksheets.Add()
            Dim oSheet = exLibro.Worksheets(1)
            oSheet.Range("E1:J1").Merge()
            oSheet.Range("E1:J1").value = "SECRETARIA DE AGRICULTURA, GANADERIA, DESARROLLO RURAL. PESCA Y ALIMENTACIÓN"
            oSheet.Range("E1").font.bold = True
            'oSheet.Range("E1").WrapText = True
            oSheet.Range("B3:O3").Merge()
            oSheet.Range("B3:O3").value = "FORMATO 3. RELAICÓN  DE COMPRAS DE PRODUCTORES(PERSONA FÍSICA O MORAL)INSCRITOS EN EL PROYECTO 'INCENTIVOS AL PAQUETE TECNOLÓGICO EN CULTIVOS DE OLEAGINOSAS'"
            oSheet.Range("B3").font.bold = True
            'oSheet.Range("D3").font.bold = True
            'oSheet.Range("D3").WrapText = True
            oSheet.Range("A4:E4").Merge()
            oSheet.Range("A4:E4").value = "Nombre de la industria Aceitera Nacional de Alimentos Balanceados:     "
            ' oSheet.Range("A4").WrapText = True
            oSheet.Range("A4").font.bold = True
            oSheet.Range("A5:C5").Merge()
            oSheet.Range("A5:C5").value = "Nombre del representante Legal:    "
            'oSheet.Range("A5").WrapText = True
            oSheet.Range("A5").font.bold = True
            oSheet.Range("A6:C6").Merge()
            oSheet.Range("A6:C6").value = "Nombre del Representante de compra:"
            'oSheet.Range("A6").WrapText = True
            oSheet.Range("A6").font.bold = True
            oSheet.Range("A7").value = "Domicilio:"
            'oSheet.Range("A7").WrapText = True
            oSheet.Range("A7").font.bold = True
            oSheet.Range("A8:C8").Merge()
            oSheet.Range("A8:C8").value = "Producto industrial que elabora:"
            'oSheet.Range("A8").WrapText = True
            oSheet.Range("A8").font.bold = True
            oSheet.Range("A9").value = "RFC:"
            'oSheet.Range("A9").WrapText = True
            oSheet.Range("A9").font.bold = True
            oSheet.Range("A10").value = "Fecha de elaboración:"
            'oSheet.Range("A10").WrapText = True
            oSheet.Range("A10").font.bold = True
            oSheet.Range("D7").value = "Municipio:"
            'oSheet.Range("C7").WrapText = True
            oSheet.Range("D7").font.bold = True
            oSheet.Range("D9").value = "Teléfono:"
            ' oSheet.Range("C9").WrapText = True
            oSheet.Range("D9").font.bold = True
            oSheet.Range("D10").value = "Periodo:"
            'oSheet.Range("C10").WrapText = False
            oSheet.Range("D10").font.bold = True
            oSheet.Range("F7").value = "Estado:"
            'oSheet.Range("E7").WrapText = True
            oSheet.Range("F7").font.bold = True
            oSheet.Range("F9").value = "Correo electrónico:"
            'oSheet.Range("E9").WrapText = True
            oSheet.Range("F9").font.bold = True
            oSheet.Range("F10").value = "No. de Reporte:"
            'oSheet.Range("E10").WrapText = True
            oSheet.Range("F10").font.bold = True
            oSheet.Range("H4:J4").Merge()
            oSheet.Range("H4:J4").value = "Nombre del centro de acopio:"
            'oSheet.Range("G4").WrapText = True
            oSheet.Range("G4").font.bold = True
            oSheet.Range("I7").value = "Georeferencia:"
            'oSheet.Range("G7").WrapText = True
            oSheet.Range("I7").font.bold = True
            oSheet.Range("I9").value = "Página web:"
            'oSheet.Range("G9").WrapText = True
            oSheet.Range("I9").font.bold = True
            oSheet.Range("D13:J13").Merge()
            oSheet.Range("D13:J13").Value = "RELACIÓN DE PRODUCTORES QUE VENDIERON SU PRODUCCIÓN A LA INDUSTRIA NACIONAL"
            oSheet.Range("D13").font.bold = True
            ' ¿Cuantas columnas y cuantas filas?
            Dim NCol As Integer = Tabla.Columns.Count
            Dim NRow As Integer = Tabla.Rows.Count

            'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
            For i As Integer = 1 To NCol
                exHoja.Cells.Item(14, i) = Tabla.Columns(i - 1).Caption
                exHoja.Cells.Item(14, i).Interior.ColorIndex = 16
            Next

            For Fila As Integer = 0 To NRow - 1
                filaTabla = Tabla.Rows(Fila)
                For Col As Integer = 0 To NCol - 1
                    If Col = 8 Then
                        Dim p As String = Format(CDbl(filaTabla(Col).ToString()), op._formatoTotal)
                        exHoja.Cells.Item(Fila + 15, Col + 1) = p
                    Else
                        exHoja.Cells.Item(Fila + 15, Col + 1) = filaTabla(Col)
                        'exHoja.Cells.Item(Fila + 15, Col + 1)
                    End If
                Next
            Next

            'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
            exHoja.Rows.Item(3).Font.Bold = 1
            exHoja.Rows.Item(3).HorizontalAlignment = 3
            exHoja.Columns.AutoFit()

            'Aplicación visible
            exApp.Application.Visible = True

            exHoja = Nothing
            exLibro = Nothing
            exApp = Nothing

            ExportarDataTableAExcel = True
        Catch ex As Exception
            MessageBox.Show(" ERROR : " & ex.Message & " --UtilForm.ExportarDataTableAExcel", "Administrador", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ExportarDataTableAExcel = False
        End Try
    End Function

    Public Function DaNuevoFolio(ByVal pSerie As String) As Integer
        comm.CommandText = "select ifnull((select max(folio) from tblsemillascomprobante where serie='" + Replace(Trim(pSerie), "'", "''") + "'),0)"
        DaNuevoFolio = comm.ExecuteScalar + 1
    End Function

    Public Function checaFolioRepetido(ByVal serie As String, ByVal folio As Integer) As Boolean
        comm.CommandText = "select serie,folio from tblsemillascomprobante where serie='" + serie + "' and folio=" + folio.ToString() + ";"
        Dim dr As MySqlDataReader
        dr = comm.ExecuteReader
        If dr.HasRows() Then
            dr.Close()
            Return True
        Else
            dr.Close()
            Return False
        End If
    End Function
    Public Sub DaultimoRepresentante(pidCliente As Integer)
        comm.CommandText = "select ifnull(nombrerepresentante,'') as np,ifnull(apellidop,'') as ap,ifnull(apellidom,'') as am,ifnull(sociopersonamoral,'') as sm from tblsemillascomprobante where idcliente=" + pidCliente.ToString + " and estado=3 order by id desc limit 1"
        Dim dr As MySqlDataReader
        dr = comm.ExecuteReader
        If dr.Read Then
            NR = dr("np")
            AM = dr("am")
            AP = dr("ap")
            socioPersonaMoral = dr("sm")
        Else
            NR = ""
            AM = ""
            AP = ""
            socioPersonaMoral = ""
        End If
        dr.Close()
    End Sub
    Public Sub reporteComprobante(ByVal idComprobante As Integer)
        Dim da As MySqlDataAdapter
        Dim ds As New DataSet
        Dim idC As Integer = 0
        Dim idS As Integer = 0
        Dim idCom As Integer = 0
        Dim dr As MySqlDataReader
        comm.CommandText = "select * from tblsemillascomprobante where id=" + idComprobante.ToString()
        dr = comm.ExecuteReader
        While dr.Read()
            idC = dr("idCliente")
            idS = dr("sucursal")
            idCom = dr("idcompra")
        End While
        dr.Close()
        comm.CommandText = "select c.id,c.idcliente,c.riego,c.superficie,c.idcompra,c.numcomprobante,c.disponecontrato,c.sociopersonamoral,c.volumen,c.estado,c.sucursal,lpad(c.folio,4,'0') as folio,c.serie,c.nombreRepresentante,c.apellidoP,c.apellidoM,c.fecha from tblsemillascomprobante as c where "
        comm.CommandText += " c.id=" + idComprobante.ToString()
        da = New MySqlDataAdapter(comm)
        da.Fill(ds, "tblcomprobantes")
        comm.CommandText = "select * from tblsemillascomprobantedetalle "
        comm.CommandText += "where idcomprobante=" + idComprobante.ToString()
        da = New MySqlDataAdapter(comm)
        da.Fill(ds, "tbldetallescomprobantes")
        comm.CommandText = "select * from tblclientes as c "
        comm.CommandText += "where c.idcliente=" + idC.ToString()
        da = New MySqlDataAdapter(comm)
        da.Fill(ds, "tblCliente")
        comm.CommandText = "select * from tblsucursales where idsucursal=" + idS.ToString()
        da = New MySqlDataAdapter(comm)
        da.Fill(ds, "tblSucursal")
        comm.CommandText = "select * from tblcompras as c where idcompra=" + idCom.ToString() + ";"
        da = New MySqlDataAdapter(comm)
        da.Fill(ds, "compra")
        ds.WriteXmlSchema(CurDir() & "\repComprobanteImpresion1.xml")
        Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rep = New repSemillasComprobanteImpresion
        rep.SetDataSource(ds)
        Dim RV As New frmReportes(rep, False)
        RV.Show()
    End Sub
    Public Sub exportarVarios(ByVal desde As String, ByVal hasta As String)
        Dim ds As New DataSet
        comm.CommandText = "select cli.nombre as nombre, cli.estado as estado, cli.municipio as municipio, cli.rfc as RFC, prod.nombre as Cultivo, c.riego as Riego," +
"c.superficie as Superficie,(select sum(tvi.cantidad) from tblcomprasdetalles as tvi where idcompra=com.idcompra) as Volumen," +
"(select d.precio from tblcomprasdetalles as d where d.idinventario=prod.idinventario limit 1)as Precio,com.referencia as NumFactura, com.fecha as fechaFactura,c.numcomprobante as numero_comprobante, case c.disponecontrato when 1 then 'SI' when 0 then 'NO' end as dispone_contrato," +
"sociopersonamoral from tblsemillascomprobante as c inner join tblclientes as cli on c.idcliente=cli.idcliente " +
"inner join tblcompras as com on c.idCompra=com.idCompra inner join tblcomprasdetalles as tvi on com.idcompra=tvi.idcompra " +
"inner join tblinventario as prod on tvi.idinventario=prod.idinventario where c.fecha>='" + desde + "' and c.fecha<='" + hasta + "';"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "comprobantes")
        If ExportarDataTableAExcel("Comprobantes", ds.Tables("comprobantes")) Then
            PopUp("Exportado", 30)
        Else
            MsgBox("no se pudo exportar.")
        End If
    End Sub
End Class
