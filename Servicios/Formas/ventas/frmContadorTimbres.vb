Public Class frmContadorTimbres

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub frmContadorTimbres_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            Dim O As New dbOpciones(MySqlcon)
            Dim CVentas As Integer
            Dim CNotasdeCargo As Integer
            Dim CNotasdeCredito As Integer
            Dim CDevoluciones As Integer
            Dim CcVentas As Integer
            Dim CcNotasdeCargo As Integer
            Dim CcNotasdeCredito As Integer
            Dim CcDevoluciones As Integer
            Dim CNomnas As Integer
            Dim CCNominas As Integer
            Dim CValidados As Integer
            Dim Mysqlcom As New MySql.Data.MySqlClient.MySqlCommand
            Mysqlcom.Connection = MySqlcon
            Mysqlcom.CommandText = "select count(idventa) from tblventastimbrado where uuid<>'**No Timbrado**' and  uuid<>''"
            CVentas = Mysqlcom.ExecuteScalar
            Mysqlcom.CommandText = "select count(idcargo) from tblnotasdecargotimbrado where uuid<>'**No Timbrado**' and  uuid<>''"
            CNotasdeCargo = Mysqlcom.ExecuteScalar
            Mysqlcom.CommandText = "select count(idnota) from tblnotasdecreditotimbrado where uuid<>'**No Timbrado**' and  uuid<>''"
            CNotasdeCredito = Mysqlcom.ExecuteScalar
            Mysqlcom.CommandText = "select count(iddevolucion) from tbldevolucionestimbrado where uuid<>'**No Timbrado**' and  uuid<>''"
            CDevoluciones = Mysqlcom.ExecuteScalar
            Mysqlcom.CommandText = "select count(idnomina) from tblnominastimbrado where uuid<>'**No Timbrado**' and  uuid<>''"
            CNomnas = Mysqlcom.ExecuteScalar

            Mysqlcom.CommandText = "select count(tblventastimbrado.idventa) from tblventastimbrado inner join tblventas on tblventastimbrado.idventa=tblventas.idventa where uuid<>'**No Timbrado**' and  uuid<>'' and tblventas.estado=4 and tblventas.fecha>='2014/01/01' and tblventas.fecha<='2016/02/15'"
            CcVentas = Mysqlcom.ExecuteScalar
            Mysqlcom.CommandText = "select count(tblnotasdecargotimbrado.idcargo) from tblnotasdecargotimbrado inner join tblnotasdecargo on tblnotasdecargotimbrado.idcargo=tblnotasdecargo.idcargo where uuid<>'**No Timbrado**' and  uuid<>'' and tblnotasdecargo.estado=4 and tblnotasdecargo.fecha>='2014/01/01' and tblnotasdecargo.fecha<='2016/02/15'"
            CcNotasdeCargo = Mysqlcom.ExecuteScalar
            Mysqlcom.CommandText = "select count(tblnotasdecreditotimbrado.idnota) from tblnotasdecreditotimbrado inner join tblnotasdecredito on tblnotasdecreditotimbrado.idnota=tblnotasdecredito.idnota where uuid<>'**No Timbrado**' and  uuid<>'' and tblnotasdecredito.estado=4 and tblnotasdecredito.fecha>='2014/01/01' and tblnotasdecredito.fecha<='2016/02/15'"
            CcNotasdeCredito = Mysqlcom.ExecuteScalar
            Mysqlcom.CommandText = "select count(tbldevolucionestimbrado.iddevolucion) from tbldevolucionestimbrado inner join tbldevoluciones on tbldevolucionestimbrado.iddevolucion=tbldevoluciones.iddevolucion where uuid<>'**No Timbrado**' and  uuid<>'' and tbldevoluciones.estado=4 and tbldevoluciones.fecha>='2014/01/01' and tbldevoluciones.fecha<='2016/02/15'"
            CcDevoluciones = Mysqlcom.ExecuteScalar
            Mysqlcom.CommandText = "select count(tblnominastimbrado.idnomina) from tblnominastimbrado inner join tblnominas on tblnominastimbrado.idnomina=tblnominas.idnomina where uuid<>'**No Timbrado**' and  uuid<>'' and tblnominas.estado=4 and tblnominas.fecha>='2014/01/01' and tblnominas.fecha<='2016/02/15'"
            CCNominas = Mysqlcom.ExecuteScalar

            Mysqlcom.CommandText = "select count(uuid) from tblxmlvalidados"
            CValidados = Mysqlcom.ExecuteScalar

            Label1.Text += CVentas.ToString + " Canceladas: " + CcVentas.ToString
            Label2.Text += CNotasdeCargo.ToString + " Canceladas: " + CcNotasdeCargo.ToString
            Label3.Text += CNotasdeCredito.ToString + " Canceladas: " + CcNotasdeCredito.ToString
            Label4.Text += CDevoluciones.ToString + " Canceladas: " + CcDevoluciones.ToString
            Label6.Text += CNomnas.ToString + " Canceladas: " + CCNominas.ToString
            Label7.Text += CValidados.ToString

            Label5.Text += CStr(CVentas + CNotasdeCargo + CNotasdeCredito + CDevoluciones + CcVentas + CcNotasdeCargo + CcNotasdeCredito + CcDevoluciones + CNomnas + CCNominas + CValidados)
            Label8.Text += CStr(O.Timbres - (CVentas + CNotasdeCargo + CNotasdeCredito + CDevoluciones + CcVentas + CcNotasdeCargo + CcNotasdeCredito + CcDevoluciones + CNomnas + CCNominas + CValidados))
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
End Class