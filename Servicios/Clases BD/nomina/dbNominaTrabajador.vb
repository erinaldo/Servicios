Imports MySql.Data.MySqlClient
Public Class dbNominaTRabajador
    Private comm As New MySqlCommand
    Public id As Integer
    'Public idNomina As Integer
    Public idNomina As Integer
    'Public tipoNomina As String
    'Public tipoPago As String
    'Public clave As String
    'Public concepto As String
    'Public importe As Double
    ' Public subsidio As Double
    'Public saldoFavor As Double
    'Public anho As Double
    'Public remanente As Double
    'Public origenRecurso As String
    'Public monto As Double
    'Public valorMercado As Double
    'Public precioAlOtorgar As Double
    Public StotalPagado As Double
    Public SanhosServicio As Double
    Public SsueldoMensual As Double
    Public Sacumulable As Double
    Public SnoAcumulable As Double

    Public Jacumulable As Double
    Public JnoAcumulable As Double
    Public JtotalUnaExhibicion As Double
    Public JtotalParcialidad As Double
    Public JmontoDiario As Double
    
    Public HayHatos As Boolean
    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        HayHatos = False
    End Sub

    Public Sub New(ByVal pIdNomina As Integer, ByVal conexion As MySqlConnection)
        idNomina = pIdNomina
        comm.Connection = conexion
        HayHatos = False
        llenaDatos()
    End Sub

    Private Sub llenaDatos()
        comm.CommandText = "select * from tblnominaextra where idnomina=" + idNomina.ToString
        Dim dr As MySqlDataReader = comm.ExecuteReader
        While dr.Read()
            id = dr("id")
            StotalPagado = dr("totalpagado")
            SanhosServicio = dr("anhosservicio")
            SsueldoMensual = dr("sueldomensual")
            Jacumulable = dr("acumulable1")
            JnoAcumulable = dr("noacumulable1")
            JtotalUnaExhibicion = dr("totalunaexhibicion")
            JtotalParcialidad = dr("totalparcialidad")
            JmontoDiario = dr("montodiario")
            Sacumulable = dr("acumulable2")
            SnoAcumulable = dr("noacumulable2")
            HayHatos = True
        End While
        dr.Close()
    End Sub

    Public Sub agregar(ByVal idTrabajador As Integer, ByVal totalPagado As Double, ByVal anhosServicio As Double, ByVal sueldoMensual As Double, ByVal acumulable1 As Double, ByVal noAcumulable1 As Double, ByVal totalUnaExhibicion As Double, ByVal totalParcialidad As Double, ByVal montoDiario As Double, ByVal acumulable2 As Double, ByVal noAcumulable2 As Double)
        comm.CommandText = "insert into tblnominaextra(idnomina,totalpagado,anhosservicio,sueldomensual,acumulable1,noacumulable1,totalunaexhibicion,totalparcialidad,montodiario,acumulable2,noacumulable2)"
        comm.CommandText += "values(" + idTrabajador.ToString + "," + totalPagado.ToString + "," + anhosServicio.ToString + "," + sueldoMensual.ToString + "," + acumulable1.ToString + "," + noAcumulable1.ToString + "," + totalUnaExhibicion.ToString + "," + totalParcialidad.ToString + "," + montoDiario.ToString + "," + acumulable2.ToString + "," + noAcumulable2.ToString + ");"
        comm.ExecuteNonQuery()
    End Sub

    Public Sub modificar(ByVal id As Integer, ByVal pidNomina As Integer, ByVal totalPagado As Double, ByVal anhosServicio As Double, ByVal sueldoMensual As Double, ByVal acumulable1 As Double, ByVal noAcumulable1 As Double, ByVal totalUnaExhibicion As Double, ByVal totalParcialidad As Double, ByVal montoDiario As Double, ByVal acumulable2 As Double, ByVal noAcumulable2 As Double)
        comm.CommandText = "update tblnominaextra set totalpagado=" + totalPagado.ToString + ", anhosservicio=" + anhosServicio.ToString + ", sueldoMensual=" + sueldoMensual.ToString + ", acumulable1=" + noAcumulable1.ToString + ", noacumulable1=" + noAcumulable1.ToString + ", totalunaexhibicion=" + totalUnaExhibicion.ToString + ", totalparcialidad=" + totalParcialidad.ToString + ", montodiario=" + montoDiario.ToString + ", acumulable2=" + noAcumulable2.ToString + ", noacumulable2=" + noAcumulable2.ToString
        comm.CommandText += " where idnomina=" + idNomina.ToString + ";"
        comm.ExecuteNonQuery()
    End Sub

    Public Sub eliminar(ByVal id As Integer)
        comm.CommandText = "delete from tblnominaextra where id=" + id.ToString + ";"
        comm.ExecuteNonQuery()
    End Sub

    'Public Function buscar(ByVal id As Integer) As Boolean
    '    comm.CommandText = "select id from tblnominaextra where id=" + id.ToString
    '    Dim i As Integer = comm.ExecuteScalar
    '    If i > 0 Then
    '        Me.id = i
    '        llenaDatos()
    '        Return True
    '    End If
    '    Return False
    'End Function

    'Public Function buscarTrabajador(ByVal idTrabajador As Integer) As Boolean
    '    comm.CommandText = "select id from tblnominaextra where idTrabajador=" + idTrabajador.ToString
    '    Dim i As Integer = comm.ExecuteScalar
    '    If i > 0 Then
    '        Me.id = i
    '        llenaDatos()
    '        Return True
    '    End If
    '    Return False
    'End Function
End Class
