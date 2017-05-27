Public Class dbEmpeniosConfiguracion

    Public factor As Double
    Public folio As Integer
    Public interes As Double
    Public almacenaje As Double
    Public gasto As Double
    Public ID As Integer
    Public diasProrroga As Double
    Public numRegistro As String
    Public A1a30 As Double
    Public A31a60 As Double
    Public A61a90 As Double
    Public A90mas As Double
    Public B1a30 As Double
    Public B31a60 As Double
    Public B61a90 As Double
    Public B90mas As Double
    Public aumentoPrenda As Double
    Public valorPlata As Double
    Public aumentoPlata As Double

    Public diasProrrogaV As Double
    Public diasProrrogaT As Double
    Public interes1a15V As Double
    Public interes16a30V As Double
    Public interes31a60V As Double
    Public interes1a15T As Double
    Public interes16a30T As Double
    Public interes31a60T As Double
    Public almacenaje1a15V As Double
    Public almacenaje16a30V As Double
    Public almacenaje31a60V As Double
    Public almacenaje1a15T As Double
    Public almacenaje16a30T As Double
    Public almacenaje31a60T As Double
    Public aumentoValorPrendaV As Double
    Public aumentoValorPrendaT As Double
    Public ClaveAutorizacion As String
    Public impresion As Integer
    Public Vis As Byte

    Public Rango1 As Double
    Public Rango2 As Double
    Public Maximo As Double
    Public Criterio As Byte
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
       
        factor = 0
        folio = 0
        interes = 0
        almacenaje = 0
        gasto = 0
        ID = 1
        diasProrroga = 0
        numRegistro = ""
        A1a30 = 0
        A31a60 = 0
        A61a90 = 0
        A90mas = 0
        B1a30 = 0
        B31a60 = 0
        B61a90 = 0
        B90mas = 0
        ClaveAutorizacion = ""
        valorPlata = 0
        aumentoPrenda = 0
        aumentoPlata = 0
        Vis = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Public Sub LlenaDatos()
        ID = 1
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblempeniosconfiguracion"
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
           
            factor = DReader("factor")
            folio = DReader("folio")
            interes = DReader("interes")
            almacenaje = DReader("almacenaje")
            ' gasto = DReader("gasto")
            diasProrroga = DReader("diasProrroga")
            numRegistro = DReader("numRegistro")
            A1a30 = DReader("A1a30")
            A31a60 = DReader("A31a60")
            A61a90 = DReader("A61a90")
            A90mas = DReader("A90mas")
            B1a30 = DReader("B1a30")
            B31a60 = DReader("B31a60")
            B61a90 = DReader("B61a90")
            B90mas = DReader("B90mas")
            aumentoPrenda = DReader("aumentoPrenda")
            valorPlata = DReader("valorPlata")
            aumentoPlata = DReader("aumentoPlata")

            diasProrrogaT = DReader("diasProrrogaT")
            diasProrrogaV = DReader("diasProrrogaV")
            interes1a15V = DReader("interes1a15V")
            interes16a30V = DReader("interes16a30V")
            interes31a60V = DReader("interes31a60V")
            almacenaje1a15V = DReader("almacenaje1a15V")
            almacenaje16a30V = DReader("almacenaje16a30V")
            almacenaje31a60V = DReader("almacenaje31a60V")
            interes1a15T = DReader("interes1a15T")
            interes16a30T = DReader("interes16a30T")
            interes31a60T = DReader("interes31a60T")
            almacenaje1a15T = DReader("almacenaje1a15T")
            almacenaje16a30T = DReader("almacenaje16a30T")
            almacenaje31a60T = DReader("almacenaje31a60T")
            aumentoValorPrendaV = DReader("aumentoValorPrendaV")
            aumentoValorPrendaT = DReader("aumentoValorPrendaT")
            ClaveAutorizacion = DReader("claveautorizacion")
            impresion = DReader("impresion")
            Vis = DReader("vis")
            Rango1 = DReader("rango1")
            Rango2 = DReader("rango2")
            Maximo = DReader("maximo")
            Criterio = DReader("criterio")
        End If
        DReader.Close()
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pfactor As String, ByVal pFolio As Integer, ByVal pinteres As String, ByVal pProrroga As Double, ByVal pNumRegitro As String, ByVal pAlmacenaje As Double, ByVal pA1a30 As Double, ByVal pA31a60 As Double, ByVal pA61a90 As Double, ByVal pA90mas As Double, ByVal pB1a30 As Double, ByVal pB31a60 As Double, ByVal pB61a90 As Double, ByVal pB90mas As Double, ByVal paumentoPrenda As Double, ByVal valorPlata As Double, ByVal aumentoPlata As Double, ByVal pdiasProrrogaV As Double, ByVal pdiasProrrogaT As Double, ByVal pinteres1a15V As Double, ByVal pinteres16a30V As Double, ByVal pinteres31a60V As Double, ByVal pinteres1a15T As Double, ByVal pinteres16a30T As Double, ByVal pinteres31a60T As Double, ByVal palmacenaje1a15V As Double, ByVal palmacenaje16a30V As Double, ByVal palmacenaje31a60V As Double, ByVal palmacenaje1a15T As Double, ByVal palmacenaje16a30T As Double, ByVal palmacenaje31a60T As Double, ByVal paumentoValorPrendaV As Double, ByVal paumentoValorPrendaT As Double, ByVal pClaveAutoriza As String, ByVal pImpresion As Integer, pVis As Byte)
        Comm.CommandText = "update tblempeniosconfiguracion set factor=" + pfactor.ToString + " ,folio=" + pFolio.ToString + " ,interes=" + pinteres.ToString + ",diasProrroga=" + pProrroga.ToString + ",numRegistro='" + Replace(pNumRegitro, "'", "''") + "',almacenaje=" + pAlmacenaje.ToString + ",A1a30=" + pA1a30.ToString + ",A31a60=" + pA31a60.ToString + ",A61a90=" + pA61a90.ToString + ",A90mas=" + pA90mas.ToString + ",B1a30=" + pB1a30.ToString + ",B31a60=" + pB31a60.ToString + ",B61a90=" + pB61a90.ToString + ",B90mas=" + pB90mas.ToString + ",aumentoPrenda=" + paumentoPrenda.ToString + ", valorPlata=" + valorPlata.ToString + " ,aumentoPlata=" + aumentoPlata.ToString + ",diasProrrogaT=" + pdiasProrrogaT.ToString + ", diasProrrogaV=" + pdiasProrrogaV.ToString + ", interes1a15V=" + pinteres1a15V.ToString + ", interes16a30V=" + pinteres16a30V.ToString + ",  interes31a60V=" + pinteres31a60V.ToString + ",  almacenaje1a15V=" + palmacenaje1a15V.ToString + ", almacenaje16a30V=" + palmacenaje16a30V.ToString + ",  almacenaje31a60V=" + palmacenaje31a60V.ToString + ",  interes1a15T=" + pinteres1a15T.ToString + ",  interes16a30T=" + pinteres16a30T.ToString + ",  interes31a60T=" + pinteres31a60T.ToString + ", almacenaje1a15T=" + palmacenaje1a15T.ToString + ", almacenaje16a30T=" + palmacenaje16a30T.ToString + ",  almacenaje31a60T=" + palmacenaje31a60T.ToString + ",  aumentoValorPrendaV=" + paumentoValorPrendaV.ToString + ", aumentoValorPrendaT=" + paumentoValorPrendaT.ToString + ",claveautorizacion='" + Replace(pClaveAutoriza, "'", "''") + "', impresion=" + pImpresion.ToString + ",vis=" + pVis.ToString + ",idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "'"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificaExtras()
        Comm.CommandText = "update tblempeniosconfiguracion set rango1=" + Rango1.ToString + ",rango2=" + Rango2.ToString + ",maximo=" + Maximo.ToString + ",criterio=" + Criterio.ToString + ",idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "'"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificarExtraVer(pVer As Byte)
        Comm.CommandText = "update tblempeniosconfiguracion set vis=" + pVer.ToString + ",idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "'"
        Comm.ExecuteNonQuery()
    End Sub
End Class
