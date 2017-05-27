Public Class dbOpcionesOc
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public HoraInicioOc As String
    Public HoraFinOc As String
    Public HoraInicioOc2 As String
    Public HoraFinOc2 As String
    Public SerieOc As String
    Public FolioOc As Integer
    Public ActivarOc As Byte
    Public OcultarOc As Byte
    Public Documento As Byte
    Public IdSucursal As Integer
    Public SeriesOc As String
    Public SeriesAnt As String
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Comm.Connection = Conexion
        HoraFinOc = "00:00"
        HoraInicioOc = "00:00"
        HoraFinOc2 = "00:00"
        HoraInicioOc2 = "00:00"
        SerieOc = ""
        SeriesAnt = ""
        FolioOc = 1
        ActivarOc = 0
        OcultarOc = 0
        Documento = 0
        IdSucursal = 0
    End Sub
    Public Sub LlenaDatos(ByVal pDocumento As Byte, ByVal pIdSucursal As Integer)
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblopcionesoc where documento=" + pDocumento.ToString + " and idsucursal=" + pIdSucursal.ToString
        DR = Comm.ExecuteReader
        If DR.Read Then
            HoraInicioOc = DR("horainicio")
            HoraFinOc = DR("horafin")
            HoraInicioOc2 = DR("horainicio2")
            HoraFinOc2 = DR("horafin2")
            SerieOc = DR("serieoc")
            FolioOc = DR("foliooc")
            ActivarOc = DR("activaroc")
            OcultarOc = DR("ocultaroc")
            Documento = DR("documento")
            IdSucursal = DR("idsucursal")
            SeriesAnt = DR("seriesant")
        End If
        DR.Close()
        Dim splistr() As String
        Comm.CommandText = "select * from tblopcionesoc where documento=" + pDocumento.ToString
        DR = Comm.ExecuteReader
        SeriesOc = ""
        While DR.Read
            SeriesOc += "-<>'" + Replace(DR("serieoc"), "'", "''") + "'"
            splistr = DR("seriesant").Split(",")
            For Each s As String In splistr
                If s <> "" Then SeriesOc += "-<>'" + Replace(s, "'", "''") + "'"
            Next
        End While
        DR.Close()
        
    End Sub
    Public Sub GuardaDatos()
        Dim Hay As Integer
        Comm.CommandText = "select ifnull((select documento from tblopcionesoc where documento=" + Documento.ToString + " and idsucursal=" + IdSucursal.ToString + "),-1)"
        Hay = Comm.ExecuteScalar
        If Hay >= 0 Then
            Comm.CommandText = "update tblopcionesoc set horainicio='" + HoraInicioOc + "',horafin='" + HoraFinOc + "',serieoc='" + Replace(SerieOc, "'", "''") + "',foliooc=" + FolioOc.ToString + ",activaroc=" + ActivarOc.ToString + ",ocultaroc=" + OcultarOc.ToString + ",horainicio2='" + HoraInicioOc2 + "',horafin2='" + HoraFinOc2 + "',seriesant='" + Replace(SeriesAnt, "'", "''") + "' where documento=" + Documento.ToString + " and idsucursal=" + IdSucursal.ToString
        Else
            Comm.CommandText = "insert into tblopcionesoc(documento,horainicio,horafin,activaroc,ocultaroc,serieoc,foliooc,horainicio2,horafin2,idsucursal,seriesant) values(" + Documento.ToString + ",'" + HoraInicioOc + "','" + HoraFinOc + "'," + ActivarOc.ToString + "," + OcultarOc.ToString + ",'" + Replace(SerieOc, "'", "''") + "'," + FolioOc.ToString + ",'" + HoraInicioOc2 + "','" + HoraFinOc2 + "'," + IdSucursal.ToString + ",'" + Replace(SeriesAnt, "'", "''") + "')"
        End If
        Comm.ExecuteNonQuery()
        Comm.CommandText = "update tblopcionesoc set activaroc=" + ActivarOc.ToString + ",ocultaroc=" + OcultarOc.ToString
        Comm.ExecuteNonQuery()
    End Sub
End Class
