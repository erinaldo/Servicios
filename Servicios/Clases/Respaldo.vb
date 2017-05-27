Public Class Respaldo
    Public Sub backup(ByVal servidor As String, ByVal filename As String, ByVal dbname As String, dbPass As String)
        Dim mysql As String = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\MySQL AB\MYSQL Server 5.5", "Location", 0)
        If mysql = "" Then mysql = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\MySQL AB\MYSQL Server 5.5", "Location", 0)
        If mysql = "" Then mysql = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\MySQL AB\MYSQL Server 5.1", "Location", 0)
        If mysql = "" Then mysql = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\MySQL AB\MYSQL Server 5.1", "Location", 0)

        Dim p As New Process
        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        p.StartInfo.FileName = mysql + "bin\mysqldump.exe"
        p.StartInfo.Arguments = "-a -R -h" + servidor + " -uroot -p" + dbPass + " " + dbname + " -r """ + filename + """"
        p.Start()
        p.WaitForExit()
    End Sub

    Public Sub restore(ByVal filename As String, ByVal servidor As String, ByVal dbname As String, dbPass As String)
        Dim mysql As String = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\MySQL AB\MYSQL Server 5.5", "Location", 0)
        If mysql = "" Then mysql = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\MySQL AB\MYSQL Server 5.5", "Location", 0)
        If mysql = "" Then mysql = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\MySQL AB\MYSQL Server 5.1", "Location", 0)
        If mysql = "" Then mysql = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\MySQL AB\MYSQL Server 5.1", "Location", 0)

        Dim postdata As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes( _
        "echo off" + vbNewLine + _
        "cls" + vbNewLine + vbNewLine + _
        "set mysql_admin=""" + mysql + "bin\mysqladmin.exe""" + vbNewLine + _
        "set mysql_exe=""" + mysql + "bin\mysql.exe""" + vbNewLine + _
        "set dbname=" + dbname + vbNewLine + _
        "set respaldo=""" + filename + """" + vbNewLine + vbNewLine + _
        "echo Dropping database %dbname%..." + vbNewLine + _
        "%mysql_admin% -hlocalhost -u root -p" + dbPass + " drop %dbname%" + vbNewLine + vbNewLine + _
        "echo Creating database %dbname%..." + vbNewLine + _
        "%mysql_admin% -hlocalhost -uroot -p" + dbPass + " create %dbname%" + vbNewLine + _
        "echo Database %dbname% created" + vbNewLine + vbNewLine + _
        "echo Restoring information..." + vbNewLine + _
        "%mysql_exe% -hlocalhost -uroot -p" + dbPass + " %dbname% < %respaldo%" + vbNewLine + _
        "echo Restore proccess finished" + vbNewLine + vbNewLine + "pause" + vbNewLine + _
        "del ""C:\windows\temp\restaurar " + dbname + ".bat""")
        Try
retry:
            IO.File.Create("C:\windows\temp\Restaurar " + dbname + ".bat").Write(postdata, 0, postdata.Length)

            GC.Collect()
        Catch ex As System.ComponentModel.Win32Exception
            If ex.ErrorCode = -2147467259 Then GoTo retry
        End Try
    End Sub
End Class
