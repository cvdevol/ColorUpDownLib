Public Class SelectedItemChangedEventArgs
    Inherits EventArgs

    Public Property Item As ColorItem

    Public Sub New(ByVal ci As ColorItem)
        Item = ci
    End Sub

End Class
