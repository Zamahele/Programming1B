Imports System.IO
Imports System.Reflection

Public Class FileHelper
  Public Sub CreateAndSaveTextFile(ByVal fileName As String, ByVal content As String, ByVal filePath As String)

    Using sw As StreamWriter = New StreamWriter(filePath, True)
      sw.WriteLine(content)
    End Using
  End Sub
End Class
