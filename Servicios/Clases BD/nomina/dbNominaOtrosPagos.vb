Imports MySql.Data.MySqlClient
Public Class dbNominaOtrosPagos
    Private comm As New MySqlCommand
    Public idPago As Integer
    Public tipoPago As Byte
    Public clave As String
    Public concepto As String
    Public importe As Double
    Public idTrabajador As Integer
    Public subsidio As Double
    Public saldoAFavor As Double
    Public anhos As Double
    Public remanente As Double

    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
    End Sub

    Public Sub New(ByVal idPago As Integer, ByVal conexion As MySqlConnection)
        Me.idPago = idPago
        comm.Connection = conexion
        llenaDatos()
    End Sub

    Private Sub llenaDatos()
        comm.CommandText = "select * from tblnominaotrospagos where idpago=" + idPago.ToString + ";"
        Dim dr As MySqlDataReader = comm.ExecuteReader
        While dr.Read()
            tipoPago = dr("tipopago")
            clave = dr("clave")
            concepto = dr("concepto")
            importe = dr("importe")
            idTrabajador = dr("idnomina")
            subsidio = dr("subsidio")
            saldoAFavor = dr("saldoafavor")
            anhos = dr("anhos")
            remanente = dr("remanente")
        End While
        dr.Close()
    End Sub

    Public Function agregar(ByVal tipoPago As Byte, ByVal clave As String, ByVal concepto As String, ByVal importe As Double, ByVal idTrabajador As Integer, ByVal subsidio As Double, ByVal saldoAFavor As Double, ByVal anhos As Double, ByVal remanente As Double) As Boolean
        comm.CommandText = "insert into tblnominaotrospagos(tipopago,clave,concepto,importe,idnomina,subsidio,saldoafavor,anhos,remanente)values(" + tipoPago.ToString + ",'" + Replace(clave, "'", "''") + "','" + concepto + "'," + importe.ToString + "," + idTrabajador.ToString + "," + subsidio.ToString + "," + saldoAFavor.ToString + "," + anhos.ToString + "," + remanente.ToString + ");"

        comm.ExecuteNonQuery()
        Return True
    End Function

    Public Sub modificar(ByVal idpago As Integer, ByVal tipopago As Byte, ByVal clave As String, ByVal concepto As String, ByVal importe As Double, ByVal idTrabajador As Integer, ByVal subsidio As Double, ByVal saldoAFavor As Double, ByVal anhos As Double, ByVal remanente As Double)
        comm.CommandText = "update tblnominaotrospagos set tipopago=" + tipopago.ToString + ", clave='" + Replace(clave, "'", "''") + "', concepto='" + concepto + "', importe=" + importe.ToString + ", subsidio=" + subsidio.ToString + ", saldoafavor=" + saldoAFavor.ToString + ", anhos=" + anhos.ToString + ", remanente=" + remanente.ToString + " where idpago=" + idpago.ToString + ";"
        comm.ExecuteNonQuery()
    End Sub

    Public Sub eliminar(ByVal idpago As Integer)
        comm.CommandText = "delete from tblnominaotrospagos where idpago=" + idpago.ToString + ";"
        comm.ExecuteNonQuery()

    End Sub

    Public Function buscar(ByVal idPago As Integer) As Boolean
        comm.CommandText = "select idpago from tblnominaotrospagos where idpago=" + idPago.ToString + ";"
        Dim i As Integer = comm.ExecuteScalar
        If i > 0 Then
            Me.idPago = i
            llenaDatos()
            Return True
        End If
        Return False
    End Function

    Public Function vistaPagos(ByVal idTrabajador As Integer) As DataView
        comm.CommandText = "select idpago,clave,concepto,importe from tblnominaotrospagos where idnomina=" + idTrabajador.ToString + ";"
        Dim da As New MySqlDataAdapter(comm)
        Dim ds As New DataSet
        da.Fill(ds, "pagos")
        Return ds.Tables("pagos").DefaultView
    End Function

    Public Function consultaPagos(ByVal idNomina As Integer) As MySqlDataReader
        comm.CommandText = "select * from tblnominaotrospagos where idnomina=" + idNomina.ToString + ";"
        Return comm.ExecuteReader
    End Function
End Class
