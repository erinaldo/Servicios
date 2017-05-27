Public Class dbContabilidadMascarasDetalles
    Public ID As Integer
    Public idVariable As Integer
    Public idCuenta As Integer
    Public Cargo As Byte
    Public Abono As Byte
    Public CuentaSTR As String
    Public IdMascara As Integer
    Public Negativo As Byte
    Public Beneficiario As Byte
    Public Modulo As Byte
    Public InUUIDs As Byte
    Public InPagoInfo As Byte
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        idVariable = 0
        idCuenta = 0
        Cargo = 0
        Abono = 0
        IdMascara = 0
        Negativo = 0
        Beneficiario = 0
        Modulo = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Public Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblcontabilidadmascarasd where iddetalle=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            idVariable = DReader("idvariable")
            idCuenta = DReader("idcuenta")
            Cargo = DReader("cargo")
            Abono = DReader("abono")
            IdMascara = DReader("idmascara")
            Negativo = DReader("negativo")
            Beneficiario = DReader("beneficiario")
            Modulo = DReader("modulo")
            InPagoInfo = DReader("inpagoinfo")
            InUUIDs = DReader("inuuids")
        End If
        DReader.Close()
        'Inventario = New dbInventario(Idinventario, Comm.Connection)
        'Moneda = New dbMonedas(IdMoneda, Comm.Connection)
    End Sub
    Public Sub Guardar(pVariable As Integer, pIdCuenta As Integer, pCargo As Byte, pAbono As Byte, pIdMascara As Integer, pNegativo As Byte, pBeneficiario As Byte, pModulo As Byte, pInUuids As Byte, pInPagoInfo As Byte)
        Comm.CommandText = "insert into tblcontabilidadmascarasd(idvariable,idcuenta,cargo,abono,idmascara,negativo,beneficiario,modulo,inpagoinfo,inuuids) values(" + pVariable.ToString + "," + pIdCuenta.ToString + "," + pCargo.ToString + "," + pAbono.ToString + "," + pIdMascara.ToString + "," + pNegativo.ToString + "," + pBeneficiario.ToString + "," + pModulo.ToString + "," + pInPagoInfo.ToString + "," + pInUuids.ToString + ")"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(pIdDetalle As Integer, pVariable As Integer, pIdCuenta As Integer, pCargo As Byte, pAbono As Byte, pNegativo As Byte, pBeneficiario As Byte, pInUuids As Byte, pInPagoInfo As Byte)
        Comm.CommandText = "update tblcontabilidadmascarasd set idvariable=" + pVariable.ToString + ",idcuenta=" + pIdCuenta.ToString + ",cargo=" + pCargo.ToString + ",abono=" + pAbono.ToString + ",negativo=" + pNegativo.ToString + ",beneficiario=" + pBeneficiario.ToString + ",inpagoinfo=" + pInPagoInfo.ToString + ",inuuids=" + pInUuids.ToString + " where iddetalle=" + pIdDetalle.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblcontabilidadmascarasd where iddetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdMascara As Integer, pN1 As Integer, pN2 As Integer, pN3 As Integer, pN4 As Integer, pN5 As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select iddetalle,cv.descripcion as Variable,case cv.modulo when 0 then 'Ventas' when 1 then 'Compras' when 2 then 'Ventas Dev.' when 3 then 'Compras Dev.' when 4 then 'Depósitos' when 5 then 'Pagos' when 6 then 'N. CR. Ventas' when 7 then 'N. CR. Compras' when 8 then 'NC Ventas' when 9 then 'NC Compras' when 10 then 'Nómina' end as Modulo,if(cm.idcuenta>0,(select concat(LPAD(c.cuenta," + pN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where cm.idcuenta=c.idccontable),case cm.idcuenta when -1 then 'COMODÍN PERCEPCIONES' when -2 then 'COMODÍN DEDUCCIONES' when -3 then 'COMODÍN CUENTAS BANCOS' when -4 then 'COMODÍN CLIENTES' when -5 then 'COMODÍN PROVEEDORES' WHEN -6 THEN 'COMODÍN CLIENTES' when -7 then 'COMODÍN PROVEEDORES' when -8 then 'COMODÍN TRABAJADORES' end) as Cuenta,if(cm.idcuenta>0,(select descripcion from tblccontables where cm.idcuenta=tblccontables.idccontable),'COMODIN'),if(cm.cargo=0,'',if(cm.negativo=0,'1','-1')) as Cargo,if(cm.abono=0,'',if(cm.negativo=0,'1','-1')) as Abono," +
            "if(cm.beneficiario=1,'B','') bene,if(cm.inpagoinfo=1,'X','') pago,if(cm.inuuids=1,'X','') uuids" +
            " from tblcontabilidadmascarasd as cm inner join tblcontabilidadvariables as cv on cm.idvariable=cv.idvariable where idmascara=" + pIdMascara.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcontabilidadmd")
        Return DS.Tables("tblcontabilidadmd").DefaultView
    End Function
    Public Function ConsultaReader(ByVal pidMascara As Integer, pN1 As Integer, pN2 As Integer, pN3 As Integer, pN4 As Integer, pN5 As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select iddetalle,cv.codigo,cm.idcuenta,cm.abono,cm.cargo,cm.negativo,cm.beneficiario,cm.inpagoinfo,cm.inuuids,cm.modulo,if(cm.idcuenta>0,(select concat(LPAD(c.cuenta," + pN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where cm.idcuenta=c.idccontable),'') as Cuenta,ifnull((select descripcion from tblccontables where tblccontables.idccontable=cm.idcuenta),'') as descripcion from tblcontabilidadmascarasd as cm  inner join tblcontabilidadvariables as cv on cm.idvariable=cv.idvariable where idmascara=" + pidMascara.ToString
        Return Comm.ExecuteReader
    End Function
    Public Function ModulosUsados(ByVal pidMascara As Integer) As Collection
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim modulos As New Collection
        Comm.CommandText = "select distinct modulo from tblcontabilidadmascarasd where idmascara=" + pidMascara.ToString
        DR = Comm.ExecuteReader
        While DR.Read
            modulos.Add(DR("modulo"))
        End While
        DR.Close()
        Return modulos
    End Function
End Class
