Public Class Encriptador
    Dim key() As Byte = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24}
    Dim keyiv() As Byte = {1, 2, 3, 4, 5, 6, 7, 8}
    Public NombreX509 As String
    Public EmitidoX509 As String
    Public FechaValidacionx509 As String
    Public FechaVencimientox509 As String
    Public Seriex509 As String
    Public Certificado64 As String
    Public RFCX09 As String
    Public Sub New()

    End Sub
    Public Function Encriptar3DES(ByVal Texto As String) As Byte()
        Dim Encoder As New System.Text.UTF8Encoding
        Dim Bytes() As Byte = Encoder.GetBytes(Texto)
        Dim tdesprovider As New Security.Cryptography.TripleDESCryptoServiceProvider
        Dim ICrypTransform As Security.Cryptography.ICryptoTransform = tdesprovider.CreateEncryptor(key, keyiv)
        Dim Mstream As New IO.MemoryStream
        Dim Cstream As New Security.Cryptography.CryptoStream(Mstream, ICrypTransform, Security.Cryptography.CryptoStreamMode.Write)
        Cstream.Write(Bytes, 0, Bytes.Length)
        Cstream.FlushFinalBlock()
        Mstream.Position = 0
        Dim resultado(Mstream.Length - 1) As Byte
        Mstream.Read(resultado, 0, Mstream.Length)
        Cstream.Close()
        Return resultado
    End Function
    Public Function Desencriptar3DES(ByVal bytes() As Byte) As String
        Dim Encoder As New System.Text.UTF8Encoding
        Dim tdesprovider As New Security.Cryptography.TripleDESCryptoServiceProvider
        Dim ICrypTransform As Security.Cryptography.ICryptoTransform = tdesprovider.CreateDecryptor(key, keyiv)
        Dim Mstream As New IO.MemoryStream
        Dim Cstream As New Security.Cryptography.CryptoStream(Mstream, ICrypTransform, Security.Cryptography.CryptoStreamMode.Write)
        Cstream.Write(bytes, 0, bytes.Length)
        Cstream.FlushFinalBlock()
        Mstream.Position = 0
        Dim resultado(Mstream.Length - 1) As Byte
        Mstream.Read(resultado, 0, Mstream.Length)
        Cstream.Close()
        Return Encoder.GetString(resultado)
    End Function
    Public Function EncriptarMD5(ByVal Texto As String) As String
        Dim Encoder As New System.Text.UTF8Encoding
        Dim Bytes() As Byte = Encoder.GetBytes(Texto)
        Dim Bytes2() As Byte
        Dim Str As String = ""
        Dim md5 As New Security.Cryptography.MD5CryptoServiceProvider
        md5.ComputeHash(Bytes)
        Bytes2 = md5.Hash
        For Each b As Byte In Bytes2
            Str += b.ToString("x2")
        Next
        Return Str
        'Return Convert.ToBase64String(Bytes2)
    End Function
    Public Function EncriptarMD5Bytes(ByVal Texto As String) As Byte()
        Dim Encoder As New System.Text.UTF8Encoding
        Dim Bytes() As Byte = Encoder.GetBytes(Texto)
        Dim Bytes2() As Byte
        Dim md5 As New Security.Cryptography.MD5CryptoServiceProvider
        md5.ComputeHash(Bytes)
        Bytes2 = md5.Hash
        Return Bytes2
    End Function

    Public Sub crearXMLclaves(ByVal ficPruebas As String)
        Dim rsa As New Security.Cryptography.RSACryptoServiceProvider()
        Dim xmlKey As String = rsa.ToXmlString(True)
        GuardaArchivoTexto(ficPruebas, xmlKey, System.Text.Encoding.Default)
    End Sub

    Public Sub GuardaArchivoTexto(ByVal Archivo As String, ByVal CadenaAGuardar As String, ByVal Formato As System.Text.Encoding)
        Dim dirPruebas As String = IO.Path.GetDirectoryName(Archivo)
        Using sw As New IO.StreamWriter(Archivo, False, Formato)
            sw.WriteLine(CadenaAGuardar)
            sw.Close()
        End Using
    End Sub
    Public Function LeeArchivoTexto(ByVal Archivo As String, Optional ByVal Enc As Byte = 0) As String
        Dim s As String = ""
        If IO.File.Exists(Archivo) Then
            If Enc = 0 Then
                Using sr As New IO.StreamReader(Archivo, System.Text.Encoding.UTF8)
                    s = sr.ReadToEnd
                    sr.Close()
                End Using
            Else
                Using sr As New IO.StreamReader(Archivo, System.Text.Encoding.Default)
                    s = sr.ReadToEnd
                    sr.Close()
                End Using
            End If

        End If
        Return s
    End Function
    Public Function LeeArchivo(ByVal fileName As String) As Byte()
        Dim f As New IO.FileStream(fileName, IO.FileMode.Open, IO.FileAccess.Read)
        Dim size As Integer = Fix(f.Length)
        Dim data(size) As Byte
        size = f.Read(data, 0, size)
        f.Close()
        Return data
    End Function
    Public Sub GuardaArchivo(ByVal FileName As String, ByVal Datos As Byte())
        Dim f As New IO.FileStream(FileName, IO.FileMode.Create)
        Dim size As Integer = Fix(Datos.Length)
        Dim data(size) As Byte
        f.Write(Datos, 0, size)
        f.Close()
    End Sub

    Public Function EncriptarRSA(ByVal texto As String, ByVal xmlKeys As String) As Byte()
        Dim rsa As New Security.Cryptography.RSACryptoServiceProvider()
        rsa.FromXmlString(xmlKeys)
        Dim datosEnc As Byte() = rsa.Encrypt(System.Text.Encoding.Default.GetBytes(texto), False)
        Return datosEnc
    End Function

    Public Function DesencrimptarRSA(ByVal datosEnc As Byte(), ByVal xmlKeys As String) As String
        Dim rsa As New Security.Cryptography.RSACryptoServiceProvider()
        rsa.FromXmlString(xmlKeys)
        Dim datos As Byte() = rsa.Decrypt(datosEnc, False)
        Dim res As String = System.Text.Encoding.Default.GetString(datos)
        Return res
    End Function

    Public Sub Leex509(ByVal StrArchivo As String)
        Dim x509 As New Security.Cryptography.X509Certificates.X509Certificate2()
        Dim rawData As Byte() = LeeArchivo(StrArchivo)
        x509.Import(rawData)
        NombreX509 = ObtenerNombre(x509.Subject, "O=")
        EmitidoX509 = ObtenerNombre(x509.Issuer, "O=")
        Seriex509 = QuitaImparStr(x509.SerialNumber)
        FechaValidacionx509 = Format(x509.NotBefore, "yyyy/MM/dd")
        FechaVencimientox509 = Format(x509.NotAfter, "yyyy/MM/dd")
        Certificado64 = Convert.ToBase64String(x509.GetRawCertData)
        RFCX09 = ObtenerNombre(x509.Subject, "OID.2.5.4.45=")
    End Sub

    
    Private Function ObtenerNombre(ByVal Str As String, ByVal pParte As String) As String
        Dim Nombre As String = ""
        Dim Pos As Integer = 0
        Dim Listo As Boolean = False
        Pos = Str.IndexOf(pParte) + pParte.Length
        Try
            While Not Listo
                If Str.Substring(Pos, 1) = "," Then
                    Listo = True
                Else
                    Nombre += Str.Substring(Pos, 1)
                End If

                Pos += 1
            End While
        Catch ex As Exception

        End Try
        Return Nombre
    End Function
    Private Function QuitaImparStr(ByVal Str As String) As String
        Dim Resultado As String = ""
        Dim Pos As Integer = 0
        Dim Par As Boolean = False
        While Pos < Str.Length
            If Par Then
                Resultado += Str.Substring(Pos, 1)
                Par = False
            Else
                Par = True
            End If
            Pos += 1
        End While
        Return Resultado
    End Function
    'Public Function GeneraSellov(ByVal pCadenaOriginal As String, ByVal pArchivoKey As String, ByVal pPasswordKey As String) As String
    '    GuardaArchivoTexto("cadena.txt", pCadenaOriginal, System.Text.Encoding.Default)
    '    Dim Comando As String
    '    Comando = """C:\OpenSSL-Win32\bin\openssl"" pkcs8 -inform DER -in """ + pArchivoKey + """ -passin pass:" + pPasswordKey + " -out """ + pArchivoKey + ".pem"""
    '    Shell(Comando, AppWinStyle.Hide, True)
    '    Comando = "@echo off" + vbCrLf
    '    Comando += """C:\OpenSSL-Win32\bin\openssl"" dgst -md5 -sign """ + pArchivoKey + ".pem"" -out """ + Application.StartupPath + "\sello.txt"" """ + Application.StartupPath + "\cadena.txt""" + vbCrLf
    '    Comando += """C:\OpenSSL-Win32\bin\openssl"" enc -base64 -in """ + Application.StartupPath + "\sello.txt"" -out """ + Application.StartupPath + "\sello64.txt"""
    '    GuardaArchivoTexto("comandomanfleis.bat", Comando, System.Text.Encoding.UTF8)
    '    Shell("comandomanfleis.bat", AppWinStyle.Hide, True)
    '    Return LeeArchivoTexto(Application.StartupPath + "\sello64.txt")
    'End Function

    Public Function GeneraSelloB(ByVal pCadenaOriginal As String, ByVal pArchivoKey As String, ByVal pPasswordKey As String)
        GuardaArchivoTexto(Application.StartupPath + "\md5.txt", EncriptarMD5(pCadenaOriginal), System.Text.Encoding.UTF8)
        Dim Comando As String
        Comando = """C:\OpenSSL-Win32\bin\openssl"" pkcs8 -inform DER -in """ + pArchivoKey + """ -passin pass:" + pPasswordKey + " -out """ + pArchivoKey + ".pem"""
        Shell(Comando, AppWinStyle.Hide, True)
        Comando = "@echo off" + vbCrLf
        Comando += """C:\OpenSSL-Win32\bin\openssl"" dgst -sign """ + pArchivoKey + ".pem"" """ + Application.StartupPath + "\md5.txt"" -out """ + Application.StartupPath + "\sellob.txt"""
        Comando += """C:\OpenSSL-Win32\bin\openssl"" enc -base64 -in """ + Application.StartupPath + "\sellob.txt"" -out sello64.txt"
        'openssl enc -base64 -in file.txt -out file.txt.enc

        'Comando += """C:\OpenSSL-Win32\bin\openssl"" dgst -sign """ + pArchivoKey + ".pem"" """ + Application.StartupPath + "\md5.txt"" | ""C:\OpenSSL-Win32\bin\openssl"" enc -base64 -A > """ + Application.StartupPath + "\sellob.txt"""
        GuardaArchivoTexto("comandomanfleis.bat", Comando, System.Text.Encoding.UTF8)
        Shell("comandomanfleis.bat", AppWinStyle.Hide, True)
        Return LeeArchivoTexto(Application.StartupPath + "\sello64.txt")
    End Function
    'Public Function GeneraSelloc(ByVal pCadenaOriginal As String, ByVal pArchivoKey As String, ByVal pPasswordKey As String) As String
    '    Dim Encoder As New System.Text.UTF8Encoding
    '    Dim Bytes() As Byte = Encoder.GetBytes(EncriptarMD5(pCadenaOriginal))
    '    GuardaArchivo(Application.StartupPath + "\md5.txt", Bytes)
    '    'GuardaArchivoTexto(Application.StartupPath + "\cadena.txt", pCadenaOriginal, utf8WithoutBom)
    '    Dim Comando As String
    '    Comando = """C:\OpenSSL-Win32\bin\openssl"" pkcs8 -inform DER -in """ + pArchivoKey + """ -passin pass:" + pPasswordKey + " -out """ + pArchivoKey + ".pem"""
    '    Shell(Comando, AppWinStyle.Hide, True)
    '    Comando = "@echo off" + vbCrLf
    '    'Comando += """C:\OpenSSL-Win32\bin\openssl"" dgst -md5 """ + Application.StartupPath + "\cadena.txt"" > """ + Application.StartupPath + "\md5.txt""" + vbCrLf
    '    Comando += """C:\OpenSSL-Win32\bin\openssl"" dgst -sign """ + pArchivoKey + ".pem"" """ + Application.StartupPath + "\md5.txt"" | ""C:\OpenSSL-Win32\bin\openssl"" enc -base64 -A > """ + Application.StartupPath + "\sellob.txt"""
    '    GuardaArchivoTexto("comandomanfleis.bat", Comando, System.Text.Encoding.UTF8)
    '    Shell("comandomanfleis.bat", AppWinStyle.Hide, True)
    '    Return LeeArchivoTexto(Application.StartupPath + "\sellob.txt")
    'End Function
    Public Function CreaPFX(ByVal pArchivoKey As String, ByVal pPasswordKey As String, ByVal pArchivoCer As String) As Boolean
        Try
            Dim Comando As String
            Comando = """C:\OpenSSL-Win32\bin\openssl"" pkcs8 -inform DER -in """ + pArchivoKey + """ -passin pass:" + pPasswordKey + " -out """ + pArchivoKey + ".pem"""
            Shell(Comando, AppWinStyle.Hide, True)
            Comando = """C:\OpenSSL-Win32\bin\openssl"" x509 -inform der -in """ + pArchivoCer + """ -out """ + pArchivoCer + ".pem"""
            Shell(Comando, AppWinStyle.Hide, True)
            Comando = """C:\OpenSSL-Win32\bin\openssl"" pkcs12 -password pass: -export -in """ + pArchivoCer + ".pem"" -inkey """ + pArchivoKey + ".pem"" -passin pass:" + pPasswordKey + " -out """ + pArchivoCer + ".pfx"""
            Shell(Comando, AppWinStyle.Hide, True)
            Return True
        Catch ex As Exception
            'MsgBox(ex.Message + " Es posible que el password del archivo .key sea incorrecto.", MsgBoxStyle.Critical, NombreApp)
            Return False
        End Try

    End Function
    Public Function GeneraSello(ByVal pCadenaOriginal As String, ByVal pArchivoCer As String, ByVal pYear As String, dos56 As Boolean) As String
        Try
            Dim objCert As New Security.Cryptography.X509Certificates.X509Certificate2(pArchivoCer + ".pfx", "")
            Dim lRSA As Security.Cryptography.RSACryptoServiceProvider = objCert.PrivateKey
            Dim bytesFirmados As Byte()
            If pYear = "2010" Then
                Dim lhasher As New Security.Cryptography.MD5CryptoServiceProvider()
                bytesFirmados = lRSA.SignData(System.Text.Encoding.UTF8.GetBytes(pCadenaOriginal), lhasher)
            Else
                If dos56 = False Then
                    Dim lhasher As New Security.Cryptography.SHA1CryptoServiceProvider()
                    bytesFirmados = lRSA.SignData(System.Text.Encoding.UTF8.GetBytes(pCadenaOriginal), lhasher)
                Else
                    'Dim lhasher256 As New Security.Cryptography.SHA256CryptoServiceProvider
                    'Dim rsaClear = New Security.Cryptography.RSACryptoServiceProvider
                    '// Export RSA parameters from 'rsa' and import them into 'rsaClear'
                    'rsaClear.ImportParameters(lRSA.ExportParameters(True))
                    'bytesFirmados = rsaClear.SignData(System.Text.Encoding.UTF8.GetBytes(pCadenaOriginal), System.Security.Cryptography.CryptoConfig.MapNameToOID("SHA256"))

                    'Dim privateCert As New Security.Cryptography.X509Certificates.X509Certificate2(archivoPFX, clavePFX, X509KeyStorageFlags.Exportable)
                    Dim privateCert As New Security.Cryptography.X509Certificates.X509Certificate2(pArchivoCer + ".pfx", "")
                    Dim privateKey As Security.Cryptography.RSACryptoServiceProvider = DirectCast(privateCert.PrivateKey, Security.Cryptography.RSACryptoServiceProvider)
                    Dim privateKey1 As New Security.Cryptography.RSACryptoServiceProvider()
                    'privateKey1.ImportParameters(privateKey.ExportParameters(True))
                    bytesFirmados = privateKey1.SignData(System.Text.Encoding.UTF8.GetBytes(pCadenaOriginal), "SHA256")

                    'Dim sello256 As String = Convert.ToBase64String(signature)


                End If
                End If
            Return Convert.ToBase64String(bytesFirmados)
        Catch ex As Exception
            MsgBox(ex.Message + " Es posible que el password del archivo .key sea incorrecto.", MsgBoxStyle.Critical, GlobalNombreApp)
            Return ""
        End Try
    End Function
    'Public Function GeneraSelloN(pArchivoCer As String, pPassKey As String) As String
    '    Dim AKey As New System.IO.FileStream(pArchivoCer, IO.FileMode.Open)
    '    Dim CKey As Org.BouncyCastle.Crypto.AsymmetricKeyParameter
    '    CKey = Org.BouncyCastle.Security.PrivateKeyFactory.DecryptKey(pPassKey.ToCharArray, AKey)
    '    AKey.Close()

    '    Dim Sellador As Org.BouncyCastle.Crypto.Signers.RsaDigestSigner
    '    Sellador = Org.BouncyCastle.Security.SignerUtilities.GetSigner("SHA1WithRSA")

    '    Sellador.Init(True, CKey)
    '    Sellador.BlockUpdate(Cadbytes, 0, Cadbytes.Length)
    '    Dim Sello As Byte() = Sellador.GenerateSignature()
    '    Debug.Print(Convert.ToBase64String(Sello))

    'End Function


End Class
