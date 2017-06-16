Public Class frmInventarioCodigosPrev
    Dim ImpDoc As ImprimirDocumento
    Dim Op As dbOpciones
    Public IdInventario As Integer
    Public Codigo As String
    Public CualCodigo As Byte
    Private Sub frmInventarioCodigosPrev_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ImpDoc = New ImprimirDocumento
        Op = New dbOpciones(MySqlcon)
        cmbCodigoBarras1.Items.Add(New EnumDescriptor(Of TiposDocumentos)(TiposDocumentos.CodigoBarras))
        cmbCodigoBarras1.Items.Add(New EnumDescriptor(Of TiposDocumentos)(TiposDocumentos.CodigoBarras2))
        cmbCodigoBarras1.Items.Add(New EnumDescriptor(Of TiposDocumentos)(TiposDocumentos.CodigoBarras3))
        cmbCodigoBarras1.Items.Add(New EnumDescriptor(Of TiposDocumentos)(TiposDocumentos.CodigoBarras4))
        cmbCodigoBarras1.SelectedIndex = 0
        Label6.Text += " " + Codigo
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub btnCodigoBarras1_Click(sender As Object, e As EventArgs) Handles btnCodigoBarras1.Click
        

        Dim printDialog1 As New PrintDialog()
        If printDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Imprimir(cmbCodigoBarras1.SelectedItem.Value, printDialog1.PrinterSettings.PrinterName)
        End If
        printDialog1.Dispose()
    End Sub
    Private Sub Imprimir(Doc As Integer, Impresora As String)
        Try
            ImpDoc.IdSucursal = GlobalIdSucursalDefault
            ImpDoc.TipoDocumento = Doc
            ImpDoc.TipoDocumentoT = Doc + 1000
            ImpDoc.TipoDocumentoImp = Doc
            ImpDoc.TipoRuta = dbSucursalesArchivos.TipoRutas.OtrosPDF
            ImpDoc.Inicializar()
            LlenaNodosImpresion()
            PrintDocument1.PrinterSettings.PrinterName = Impresora
            PrintDocument1.DocumentName = "CB"
            PrintDocument1.Print()
        Catch ex As Exception
            MsgBox("Error al imprimir: " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub LlenaNodosImpresion()
        ImpDoc.ImpND.Clear()
        Dim IP As New dbInventarioPrecios(MySqlcon)
        Dim i As New dbInventario(IdInventario, MySqlcon)
        Dim code As iTextSharp.text.pdf.Barcode128 = New iTextSharp.text.pdf.Barcode128
        If CualCodigo = 0 Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "codigo", i.Clave, 0), "codigo")
            code.Code = i.Clave
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "codigo", i.Clave2, 0), "codigo")
            code.Code = i.Clave2
        End If
        ImpDoc.ImpND.Add(New NodoImpresionN("", "nombreArticulo", i.Nombre, 0), "nombreArticulo")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "precio", Format(IP.DaPrecioListaUno(IdInventario), "$#,###,##0.00"), 0), "precio")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "campo1", txtCodigo2.Text, 0), "campo1")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "campo2", TextBox1.Text, 0), "campo2")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "campo3", TextBox2.Text, 0), "campo3")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "campo4", TextBox3.Text, 0), "campo4")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "campo5", TextBox4.Text, 0), "campo5")
        Dim image As Image = code.CreateDrawingImage(Color.Black, Color.White)
        ImpDoc.CodigoBidimensional = New Bitmap(image)

    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        ImpDoc.DibujaPaginaN(e.Graphics)
        e.HasMorePages = ImpDoc.MasPaginas
    End Sub
End Class