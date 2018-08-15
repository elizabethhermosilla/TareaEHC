Imports System.Data.Odbc
Module Conexiones
    Public Conexion As OdbcConnection
    Public comando As OdbcCommand
    Public FECHA As Date = Today.Date

    Public Sub AbrirConexion()
        Conexion = New OdbcConnection
        Conexion.ConnectionString = "Dsn=OracleCn;UID=system;PWD=hc;DATABASE=AGENDA;"
        Conexion.Open()
    End Sub

    Public Sub GuardarDatos(ByVal Cadena As String)
        comando = New OdbcCommand(Cadena, Conexion)
        comando.Parameters.Add("?FECHA", OdbcType.DateTime).Value = FECHA
        comando.ExecuteNonQuery()

    End Sub

End Module
