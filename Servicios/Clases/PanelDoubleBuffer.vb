Public Class PanelDoubleBuffer
    Inherits Panel
    Public Sub New()
        Me.DoubleBuffered = True
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        UpdateStyles()
    End Sub
End Class
