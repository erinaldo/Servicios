Public Class elemento
    Public Valor() As Long
    Public TotalDatos As Long
    Public Sub New()
        TotalDatos = 0
    End Sub
    Public Sub Agregar(ByVal NuevoValor As Long)
        TotalDatos = TotalDatos + 1
        ReDim Preserve Valor(TotalDatos)
        Valor(TotalDatos - 1) = NuevoValor
    End Sub
    Public Sub Limpiar()
        ReDim Valor(-1)
        TotalDatos = 0
    End Sub
    Public Function Busca(ByVal ValorABuscar As Long) As Long
        Dim RecorreArreglo As Long
        RecorreArreglo = 0
        While RecorreArreglo < TotalDatos
            If Valor(RecorreArreglo) = ValorABuscar Then
                Return RecorreArreglo
            End If
            RecorreArreglo += 1
        End While
        Return 0
    End Function
    Public Function Busca(ByVal ValorABuscar As Long, ByVal NoEncontro As Boolean) As Long
        Dim RecorreArreglo As Long
        RecorreArreglo = 0
        While RecorreArreglo < TotalDatos
            If Valor(RecorreArreglo) = ValorABuscar Then
                Return RecorreArreglo
            End If
            RecorreArreglo += 1
        End While
        If NoEncontro Then
            Return -1
        Else
            Return 0
        End If
    End Function
End Class
