Imports MySql.Data.MySqlClient
Public Class generarReporte
    Private command As New MySqlCommand

    Public Sub New(ByVal conexion As mysqlconnection)
        command.Connection = conexion
    End Sub

    Public Sub reporteGeneral()
        Dim ds As New DataSet
        command.CommandText = "select b.folio as folio, b.fecha as fecha, b.humedad as humedad, b.impurezas as impurezas, b.granoquebrado as granoQuebrado," +
            "b.granodanado as granoDanado,b.castigoHumedad as castigoHumedad,b.castigoimpurezas as castigoImpurezas,b.castigoGranoQ as castigoGranoQ, " +
            "b.castigoGranoD as castigoGranoD,b.pesoanalizado as pesoanalizado,b.hora as hora, b.tipoboleta as tipoboleta, prov.nombre as nombreProductor, inv.nombre as nombreProducto, b.peso as peso " +
     "from tblsemillasboletas as b inner join tblproveedores as prov on b.productor=prov.idproveedor inner join tblinventario as inv on b.producto=inv.idinventario "
        Dim da As New MySqlDataAdapter(command)
        da.Fill(ds, "Table")
        ds.WriteXml(CurDir() & "\repGeneral.xml", XmlWriteMode.WriteSchema)
        'Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        'rep = New repSemillasGeneral
        Dim fecha As New Date()
        'rep.SetParameterValue("fecha", fecha.ToString("yyyy/MM/dd"))
        'rep.SetDataSource(ds.Tables("Table"))
        'Dim RV As New frmReportes(rep, False)
        'RV.Show()
    End Sub

    Public Sub reporteProductos(ByVal producto As dbInventario)
        Dim ds As New DataSet
        command.CommandText = "select b.folio as folio, b.fecha as fecha, prov.nombre as nombreProductor, inv.nombre as nombreProducto, b.peso as peso " +
     "from tblsemillasboletas as b inner join tblproveedores as prov on b.productor=prov.idproveedor inner join tblinventario as inv on b.producto=inv.idinventario " +
     "where b.producto=" + producto.ID.ToString() + ";"
        Dim da As New MySqlDataAdapter(command)
        da.Fill(ds, "tblRepSemillasProductos")
        'ds.WriteXml(CurDir() & "\repSemillasProductos.xml", XmlWriteMode.WriteSchema)
        'Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        'rep = New repSemillasProductos
        'rep.SetDataSource(ds.Tables("tblSemillasProductos"))
        'Dim RV As New frmReportes(rep, False)
        'RV.Show()
    End Sub

    Public Sub reporteProductores(ByVal proveedor As dbproveedores)
        Dim ds As New DataSet
        command.CommandText = "select b.folio as folio, b.fecha as fecha, b.humedad, as humedad, b.impurezas as impurezas, b.granoquebrado as granoQuebrado," +
            "b.granodanado as granoDañado,b.castigoHumedad as castigoHumedad,b.castigoimpurezas as castigoImpurezas,b.castigoGranoQ as castigoGranoQ, " +
            "b.castigoGranoD as castigoGranoD,b.pesoanalizado as pesoanalizado,b.hora as hora, b.tipoboleta as tipoboleta, prov.nombre as nombreProductor, inv.nombre as nombreProducto, b.peso as peso " +
     "from tblsemillasboletas as b inner join tblproveedores as prov on b.productor=prov.idproveedor inner join tblinventario as inv on b.producto=inv.idinventario " +
     "where b.productor=" + proveedor.ID.ToString() + ";"
        Dim da As New MySqlDataAdapter(command)
        da.Fill(ds, "tblRepSemillasProductores")
        'ds.WriteXml(CurDir() & "\repSemillasProductos.xml", XmlWriteMode.WriteSchema)
        'Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        'rep = New repSemillasProductos
        'rep.SetDataSource(ds.Tables("tblSemillasProductores"))
        'Dim RV As New frmReportes(rep, False)
        'RV.Show()
    End Sub

    Public Sub reporteFechasRango(ByVal desde As String, ByVal hasta As String)
        Dim ds As New DataSet
        command.CommandText = "select b.folio as folio, b.fecha as fecha, prov.nombre as nombreProductor, inv.nombre as nombreProducto, b.peso as peso " +
     "from tblsemillasboletas as b inner join tblproveedores as prov on b.productor=prov.idproveedor inner join tblinventario as inv on b.producto=inv.idinventario " +
     "where b.fecha>='" + desde + "' and b.fecha<='" + hasta + "';"
        Dim da As New MySqlDataAdapter(command)
        da.Fill(ds, "tblRepSemillasRangoFechas")
        ds.WriteXml(CurDir() & "\repSemillasRangoFechas.xml", XmlWriteMode.WriteSchema)
    End Sub

    Public Sub reportefecha(ByVal fecha As String)
        Dim ds As New DataSet
        command.CommandText = "select b.folio as folio, b.fecha as fecha, prov.nombre as nombreProductor, inv.nombre as nombreProducto, b.peso as peso " +
     "from tblsemillasboletas as b inner join tblproveedores as prov on b.productor=prov.idproveedor inner join tblinventario as inv on b.producto=inv.idinventario " +
     "where b.fecha='" + fecha + "';"
        Dim da As New MySqlDataAdapter(command)
        da.Fill(ds, "tblRepSemillasFecha")
        ds.WriteXml(CurDir() & "\repSemillasFecha.xml", XmlWriteMode.WriteSchema)
    End Sub
End Class
