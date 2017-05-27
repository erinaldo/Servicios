Imports MySql.Data.MySqlClient

Public Class AddendaService

    Dim addendaData As New AddendaData(MySqlcon)

    Public Function guardarArticuloPorAddenda(ByVal articulo As Articulo, ByVal idAddenda As Integer) As Boolean
        Return addendaData.guardarArticuloPorAddenda(articulo, idAddenda)
    End Function

    Public Function guardarAddenda(ByVal addenda As AddendaOxxo, ByVal pIdVenta As Integer) As Boolean
        Return addendaData.guardarAddenda(addenda, pIdVenta)
    End Function

    Public Function obtenerArticulosPorAddenda(ByVal idAddenda As Integer) As MySqlDataAdapter
        Return addendaData.obtenerArticulosPorAddenda(idAddenda)
    End Function

    Public Function actualizarAddenda(ByVal addenda As AddendaOxxo) As Boolean
        Return addendaData.actualizarAddenda(addenda)
    End Function

    Public Function actualizarArticulo(ByVal articulo As Articulo) As Boolean
        Return addendaData.actualizarArticulo(articulo)
    End Function

    Public Function obtenerAddenda(ByVal idAddenda As Integer) As AddendaOxxo
        Dim addenda As New AddendaOxxo()
        addenda = addendaData.obtenerAddendaConArticulos(idAddenda)
        Return addenda
    End Function

End Class
