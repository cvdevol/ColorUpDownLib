Imports System.Collections.ObjectModel
Imports System.ComponentModel

''' <summary>
''' An extended ObservableCollection to allow continuous bidirectional cycling through the items.
''' Going forward, When the last item in the Collection is reached, the next item will be the first Item in the Collection.
''' Similarly, going backward, when the first Item in the collection is reached, the next Item will be the first Item in the Collection.
''' </summary>
''' <typeparam name="T">Type of data to be put in the Collection</typeparam>
Public Class CircularObservableCollection(Of T)
    Inherits ObservableCollection(Of T)

    Private Property Index As Integer

    Public Sub New()
        MyBase.New
    End Sub

    ''' <summary>
    ''' Moves to the next Item in the Collection
    ''' </summary>
    ''' <returns>The next Item in the collection</returns>
    Public Function NextElement() As T
        If Me.Count = 0 Then Return Nothing
        If Me.Count = 1 Then Return Me.Item(0)
        Index += 1
        If Index >= Me.Count Then
            Index = 0
        End If
        Return Me.Item(Index)
    End Function

    ''' <summary>
    ''' Moves to the previous Item in the Collection
    ''' </summary>
    ''' <returns>The previous Item in the collection</returns>
    Public Function PreviousElement() As T
        If Me.Count = 0 Then Return Nothing
        If Me.Count = 1 Then Return Me.Item(0)
        Index -= 1
        If Index < 0 Then
            Index = Me.Count - 1
        End If
        Return Me.Item(Index)

    End Function


End Class
