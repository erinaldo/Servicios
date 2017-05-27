<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSemillasReportes
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.DSsemillas = New System.Data.DataSet()
        Me.tblSemillas = New System.Data.DataTable()
        Me.folio = New System.Data.DataColumn()
        Me.fecha = New System.Data.DataColumn()
        Me.producto = New System.Data.DataColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtProductor = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtProducto = New System.Windows.Forms.TextBox()
        Me.btnProductor = New System.Windows.Forms.Button()
        Me.btnProducto = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpDesde = New System.Windows.Forms.DateTimePicker()
        Me.dtpHasta = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtClaveProductor = New System.Windows.Forms.TextBox()
        Me.txtClaveProducto = New System.Windows.Forms.TextBox()
        Me.btnGenerar = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        CType(Me.DSsemillas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tblSemillas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DSsemillas
        '
        Me.DSsemillas.DataSetName = "DSsemillas"
        Me.DSsemillas.Tables.AddRange(New System.Data.DataTable() {Me.tblSemillas})
        '
        'tblSemillas
        '
        Me.tblSemillas.Columns.AddRange(New System.Data.DataColumn() {Me.folio, Me.fecha, Me.producto})
        Me.tblSemillas.TableName = "tblSemillas"
        '
        'folio
        '
        Me.folio.ColumnName = "folio"
        '
        'fecha
        '
        Me.fecha.ColumnName = "fecha"
        '
        'producto
        '
        Me.producto.ColumnName = "producto"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(275, 64)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Productor:"
        '
        'txtProductor
        '
        Me.txtProductor.Enabled = False
        Me.txtProductor.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProductor.Location = New System.Drawing.Point(437, 61)
        Me.txtProductor.Name = "txtProductor"
        Me.txtProductor.Size = New System.Drawing.Size(285, 22)
        Me.txtProductor.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(281, 96)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Producto:"
        '
        'txtProducto
        '
        Me.txtProducto.Enabled = False
        Me.txtProducto.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProducto.Location = New System.Drawing.Point(437, 93)
        Me.txtProducto.Name = "txtProducto"
        Me.txtProducto.Size = New System.Drawing.Size(285, 22)
        Me.txtProducto.TabIndex = 4
        '
        'btnProductor
        '
        Me.btnProductor.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProductor.Location = New System.Drawing.Point(728, 59)
        Me.btnProductor.Name = "btnProductor"
        Me.btnProductor.Size = New System.Drawing.Size(40, 23)
        Me.btnProductor.TabIndex = 5
        Me.btnProductor.Text = "..."
        Me.btnProductor.UseVisualStyleBackColor = True
        '
        'btnProducto
        '
        Me.btnProducto.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProducto.Location = New System.Drawing.Point(729, 91)
        Me.btnProducto.Name = "btnProducto"
        Me.btnProducto.Size = New System.Drawing.Size(39, 23)
        Me.btnProducto.TabIndex = 6
        Me.btnProducto.Text = "..."
        Me.btnProducto.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(274, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 16)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Desde:"
        '
        'dtpDesde
        '
        Me.dtpDesde.CustomFormat = "yyyy/MM/dd"
        Me.dtpDesde.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDesde.Location = New System.Drawing.Point(329, 17)
        Me.dtpDesde.Name = "dtpDesde"
        Me.dtpDesde.Size = New System.Drawing.Size(89, 22)
        Me.dtpDesde.TabIndex = 8
        '
        'dtpHasta
        '
        Me.dtpHasta.CustomFormat = "yyyy/MM/dd"
        Me.dtpHasta.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpHasta.Location = New System.Drawing.Point(484, 16)
        Me.dtpHasta.Name = "dtpHasta"
        Me.dtpHasta.Size = New System.Drawing.Size(89, 22)
        Me.dtpHasta.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(434, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 16)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Hasta:"
        '
        'txtClaveProductor
        '
        Me.txtClaveProductor.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClaveProductor.Location = New System.Drawing.Point(353, 61)
        Me.txtClaveProductor.Name = "txtClaveProductor"
        Me.txtClaveProductor.Size = New System.Drawing.Size(77, 22)
        Me.txtClaveProductor.TabIndex = 11
        '
        'txtClaveProducto
        '
        Me.txtClaveProducto.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClaveProducto.Location = New System.Drawing.Point(353, 93)
        Me.txtClaveProducto.Name = "txtClaveProducto"
        Me.txtClaveProducto.Size = New System.Drawing.Size(78, 22)
        Me.txtClaveProducto.TabIndex = 12
        '
        'btnGenerar
        '
        Me.btnGenerar.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerar.Location = New System.Drawing.Point(418, 141)
        Me.btnGenerar.Name = "btnGenerar"
        Me.btnGenerar.Size = New System.Drawing.Size(137, 36)
        Me.btnGenerar.TabIndex = 15
        Me.btnGenerar.Text = "Imprimir"
        Me.btnGenerar.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 16
        Me.ListBox1.Location = New System.Drawing.Point(2, 1)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(266, 196)
        Me.ListBox1.TabIndex = 17
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(691, 16)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox1.TabIndex = 18
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(579, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(106, 16)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "tipo de boletas:"
        '
        'frmSemillasReportes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(816, 198)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.btnGenerar)
        Me.Controls.Add(Me.txtClaveProducto)
        Me.Controls.Add(Me.txtClaveProductor)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dtpHasta)
        Me.Controls.Add(Me.dtpDesde)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnProducto)
        Me.Controls.Add(Me.btnProductor)
        Me.Controls.Add(Me.txtProducto)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtProductor)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSemillasReportes"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reportes"
        CType(Me.DSsemillas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tblSemillas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DSsemillas As System.Data.DataSet
    Friend WithEvents tblSemillas As System.Data.DataTable
    Friend WithEvents folio As System.Data.DataColumn
    Friend WithEvents fecha As System.Data.DataColumn
    Friend WithEvents producto As System.Data.DataColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtProductor As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtProducto As System.Windows.Forms.TextBox
    Friend WithEvents btnProductor As System.Windows.Forms.Button
    Friend WithEvents btnProducto As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpDesde As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpHasta As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtClaveProductor As System.Windows.Forms.TextBox
    Friend WithEvents txtClaveProducto As System.Windows.Forms.TextBox
    Friend WithEvents btnGenerar As System.Windows.Forms.Button
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
