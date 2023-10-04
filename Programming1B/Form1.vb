Imports System.Text
Imports System.IO
Imports System.Reflection
Imports System.Linq

Public Class Form1
  Dim filehelper As New FileHelper()
  Dim currentAssembly As Assembly = Assembly.GetExecutingAssembly()
  Dim debugFolderPath As String = Path.GetDirectoryName(currentAssembly.Location)
  Dim filePath As String

  Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
    filehelper.CreateAndSaveTextFile($"{TextBox1.Text}.txt", "", filePath)
  End Sub

  Private Sub btnAddtofile_Click(sender As Object, e As EventArgs) Handles btnAddtofile.Click
    Dim sb As New StringBuilder()
    sb.AppendLine(TextBox2.Text)
    sb.AppendLine(TextBox3.Text)
    sb.AppendLine(TextBox4.Text)
    sb.AppendLine(TextBox5.Text)
    sb.AppendLine(TextBox6.Text)
    sb.AppendLine(TextBox7.Text)

    Dim content As String = sb.ToString()
    filehelper.CreateAndSaveTextFile($"{TextBox1.Text}.txt", content, filePath)
  End Sub

  Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress
    If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
      e.Handled = True
    End If
  End Sub

  Private Sub TextBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox6.KeyPress
    If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
      e.Handled = True
    End If
  End Sub

  Private Sub TextBox7_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox7.KeyPress
    If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
      e.Handled = True
    End If
  End Sub

  Private Sub LoadDataIntoDataGridView(filePath As String)
    ' LINQ
    Dim lines As List(Of String) = File.ReadAllLines(filePath).Where(Function(line) Not String.IsNullOrWhiteSpace(line)).ToList()
    Dim students As New List(Of Student)

    For i As Integer = 0 To lines.Count - 1 Step 6
      If i + 5 < lines.Count Then
        students.Add(New Student() With {
                .StudentNumber = lines(i),
                .Name = lines(i + 1),
                .Surname = lines(i + 2),
                .IndAssignment = lines(i + 3),
                .GroupAssignment = lines(i + 4),
                .Test = lines(i + 5)
            })
      End If
    Next

    ' Bind
    DataGridView1.DataSource = students
  End Sub

  Private Sub btnDisplay_Click(sender As Object, e As EventArgs) Handles btnDisplay.Click
    If String.IsNullOrEmpty(TextBox1.Text.Trim()) Then
      filePath = Path.Combine(debugFolderPath, "grades.txt")
    End If
    LoadDataIntoDataGridView(filePath)
  End Sub

  Private Sub TextBox1_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TextBox1.Validating
    If String.IsNullOrEmpty(TextBox1.Text.Trim()) Then
      ErrorProvider1.SetError(TextBox1, "This field is required.")
    Else
      ErrorProvider1.SetError(TextBox1, "")
    End If
  End Sub

  Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
    filePath = Path.Combine(debugFolderPath, $"{TextBox1?.Text}.txt")
  End Sub
End Class
