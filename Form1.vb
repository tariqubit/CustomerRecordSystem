Imports System.Data.Entity
Public Class Form1

    Private db As CustomerRecordSystem.NorthwindEntities
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        db = New CustomerRecordSystem.NorthwindEntities
        db.Customers.Load()
        CustomerBindingSource.DataSource = db.Customers.Local
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        CustomerBindingSource.AddNew()

    End Sub

    Private Sub Btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        If MessageBox.Show("Are You Sure ToWant To Delete This Records ?", "Message", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = DialogResult.Yes Then
            CustomerBindingSource.RemoveCurrent()
        End If
    End Sub

    Private Sub Btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        db.SaveChanges()
        MessageBox.Show("Your Data Has Been Successfully Saved !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Dim changed = db.ChangeTracker.Entries().Where(Function(x) x.State <> EntityState.Unchanged).ToList()
        For Each obj In changed
            Select Case obj.State
                Case EntityState.Modified
                    obj.CurrentValues.SetValues(obj.OriginalValues)
                    obj.State = EntityState.Unchanged
                Case EntityState.Added
                    obj.State = EntityState.Detached
                Case EntityState.Deleted
                    obj.State = EntityState.Unchanged
            End Select
        Next
        CustomerBindingSource.ResetBindings(False)

    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        db.Dispose()
    End Sub
End Class
