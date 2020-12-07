Imports Microsoft.Office.Interop.Excel
Imports System.IO
Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Scelta Excel
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.ShowDialog.OK Then
            ListBox2.Items.Add(OpenFileDialog1.FileName)
            If OpenFileDialog1.ShowDialog.Cancel Then

            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Aggiunta colonne
        ListBox1.Items.Add(TextBox4.Text)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Rimozione colonna selezionata da listbox
        ListBox1.Items.Remove(ListBox1.SelectedItem)
    End Sub

    Public Sub Code_Search()
        'Reset progressbar
        ProgressBar1.Value = 0
        ProgressBar1.Maximum = ListBox2.Items.Count

        'reset conteggio modifiche
        Label7.Text = 0

        'Variazione di + excel
        For Each Lexcel As String In ListBox2.Items
            'Apertura excel
            Dim Excel As Object
            Dim Book As Object
            Dim Sheet As Object
            Excel = CreateObject("Excel.Application")
            Book = Excel.Workbooks.Open(Lexcel)
            Sheet = Book.Worksheets(1)
            If Book.ReadOnly Then
                GoTo Salta
            End If
            'Ricerca codice da colonne selezionate
            For Each Column As String In ListBox1.Items
                Dim Riga As String = 6
                Dim Cod1 As String = TextBox2.Text
                Dim Cod As String = TextBox3.Text

Start:
                Riga += 1
                Dim change As String = Column + Riga
                Dim Cod2 As String = Sheet.range(change).value

                'Se cella vuota fine colonna
                If Sheet.range(change).value = "" Then
                    GoTo Finecolonna

                    'Se valore cella = a valore allora sostituisci
                ElseIf Cod2 = Cod1 Then
                    Sheet.range(change).value = Cod
                    Label7.Text += 1
                    GoTo Start

                    'Se valore cella diverso da valore allora riparti cambiando riga
                ElseIf Sheet.range(change).value <> TextBox2.Text Then
                    GoTo Start
                End If
Finecolonna:
            Next
            'Chiusura e salvataggio
            Excel.DisplayAlerts = False
            Book.Save()
                Book.Close()
                Excel.Quit

                ProgressBar1.Value += 1
Salta:
        Next
        MsgBox("Sono stati cambiati " + Label7.Text + " codici", MsgBoxStyle.Information, "MEC")
        MsgBox("Ora verranno chiusi tutti i processi di excel, SALVA!", MsgBoxStyle.Information, "MEC")
task:

        'Chiusura processi excel
        Try
            Process.Start("taskkill", "/im excel.exe /f")
        Catch ex As Exception
            GoTo task
        End Try

        'Riavvio app
        System.Windows.Forms.Application.Restart()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Code_Search()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ListBox2.Items.Remove(ListBox2.SelectedItem)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        FolderBrowserDialog1.ShowDialog()
        Dim path1 As String
        If FolderBrowserDialog1.ShowDialog.OK Then
            path1 = FolderBrowserDialog1.SelectedPath
        End If
        For Each EX As String In My.Computer.FileSystem.GetFiles(Path1)
            ListBox2.Items.Add(EX)
        Next
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
