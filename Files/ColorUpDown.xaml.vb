Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Reflection


''' <summary>
''' Color types, either System.Windows.Media.Colors ("Web" Colors), System.Windows.SystemColors (Windows System Colors), or both combined
''' </summary>
Public Enum ColorType
    WebColors
    SystemColors
    AllColors
End Enum

Public Class ColorUpDown
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Sub NotifyPropertyChanged(ByVal info As String)
        Select Case info
            Case "DisplayType"
                Select Case DisplayType
                    Case ColorType.AllColors
                        SelectItem(AllColorItems.Item(0).Brush)
                    Case ColorType.SystemColors
                        SelectItem(SystemColorItems.Item(0).Brush)
                    Case ColorType.WebColors
                        SelectItem(ColorItems.Item(0).Brush)
                End Select
            Case "SelectedIndex"
                SelectIndex(SelectedIndex)
            Case "SelectedBrush"
                SelectItem(SelectedBrush)

        End Select

    End Sub

    ''' <summary>
    ''' The Brush currently showing.
    ''' </summary>
    Public Shared SelectedBrushProperty As DependencyProperty = DependencyProperty.Register("SelectedBrush", GetType(Brush), GetType(ColorUpDown))
    Public Property SelectedBrush() As Brush
        Get
            Return CType(GetValue(SelectedBrushProperty), Brush)
        End Get
        Set(value As Brush)
            SetValue(SelectedBrushProperty, value)
            NotifyPropertyChanged("SelectedBrush")
        End Set
    End Property

    ''' <summary>
    ''' The Color currently showing.
    ''' </summary>
    Public Shared SelectedColorProperty As DependencyProperty = DependencyProperty.Register("SelectedColor", GetType(Color), GetType(ColorUpDown))
    Public Property SelectedColor() As Color
        Get
            Return CType(GetValue(SelectedColorProperty), Color)
        End Get
        Set(value As Color)
            SetValue(SelectedColorProperty, value)

        End Set
    End Property

    Public Shared SelectedColorNameProperty As DependencyProperty = DependencyProperty.Register("SelectedColorName", GetType(String), GetType(ColorUpDown))
    Public Property SelectedColorName() As String
        Get
            Return CType(GetValue(SelectedColorNameProperty), String)
        End Get
        Set(value As String)
            SetValue(SelectedColorNameProperty, value)
        End Set
    End Property

    Public Shared SelectedIndexProperty As DependencyProperty = DependencyProperty.Register("SelectedIndex", GetType(Integer), GetType(ColorUpDown))
    Public Property SelectedIndex() As Integer
        Get
            Return CType(GetValue(SelectedIndexProperty), Integer)
        End Get
        Set(value As Integer)
            SetValue(SelectedIndexProperty, value)
            NotifyPropertyChanged("SelectedIndex")
        End Set
    End Property

    ''' <summary>
    ''' List of all Web Colors (System.Windows.Media.Colors)
    ''' </summary>
    Public Shared ColorItemsProperty As DependencyProperty = DependencyProperty.Register("ColorItems", GetType(CircularObservableCollection(Of ColorItem)), GetType(ColorUpDown))
    Public Property ColorItems As CircularObservableCollection(Of ColorItem)
        Get
            Return GetValue(ColorItemsProperty)
        End Get
        Set(value As CircularObservableCollection(Of ColorItem))
            SetValue(ColorItemsProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' List of all System Colors (System.Windows.SystemColors)
    ''' </summary>
    Public Shared SystemColorItemsProperty As DependencyProperty = DependencyProperty.Register("SystemColorItems", GetType(CircularObservableCollection(Of ColorItem)), GetType(ColorUpDown))
    Public Property SystemColorItems As CircularObservableCollection(Of ColorItem)
        Get
            Return GetValue(SystemColorItemsProperty)
        End Get
        Set(value As CircularObservableCollection(Of ColorItem))
            SetValue(SystemColorItemsProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' List of all colors.
    ''' </summary>
    Public Shared AllColorItemsProperty As DependencyProperty = DependencyProperty.Register("AllColorItems", GetType(CircularObservableCollection(Of ColorItem)), GetType(ColorUpDown))
    Public Property AllColorItems As CircularObservableCollection(Of ColorItem)
        Get
            Return GetValue(AllColorItemsProperty)
        End Get
        Set(value As CircularObservableCollection(Of ColorItem))
            SetValue(AllColorItemsProperty, value)
        End Set
    End Property

    ''' <summary>
    '''  Choice of color types, either System.Windows.Media.Colors ("Web" Colors), System.Windows.SystemColors (Windows System Colors), or both combined
    ''' </summary>
    Public Shared DisplayTypeProperty As DependencyProperty = DependencyProperty.Register("DisplayType", GetType(ColorType), GetType(ColorUpDown))
    Public Property DisplayType() As ColorType
        Get
            Return GetValue(DisplayTypeProperty)
        End Get
        Set(value As ColorType)
            SetValue(DisplayTypeProperty, value)
            NotifyPropertyChanged("DisplayType")
        End Set
    End Property

    Public Delegate Sub SelectedItemChangedEventHandler(sender As Object, e As SelectedItemChangedEventArgs)
    Public Event SelectedItemChanged As SelectedItemChangedEventHandler

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.DataContext = Me
        LoadColors()
        LoadSystemColors()
        LoadAllColors()
        UI.Child = AllColorItems.Item(0)

    End Sub

    Private Sub LoadColors()
        ColorItems = New CircularObservableCollection(Of ColorItem)
        Dim colType As Type = GetType(System.Windows.Media.Colors)
        Dim i As Integer
        For Each prop As PropertyInfo In colType.GetProperties()
            If prop.PropertyType Is GetType(System.Windows.Media.Color) Then
                Dim c As Color = prop.GetValue(prop)
                ColorItems.Add(New ColorItem(c, prop.Name) With {.Index = i, .Tag = i})
                i += 1
            End If
        Next
    End Sub

    Private Sub LoadSystemColors()
        SystemColorItems = New CircularObservableCollection(Of ColorItem)
        Dim colType = GetType(System.Windows.SystemColors)
        Dim i As Integer
        For Each prop As PropertyInfo In colType.GetProperties()
            If prop.PropertyType Is GetType(System.Windows.Media.Color) Then
                Dim c As Color = prop.GetValue(prop)
                SystemColorItems.Add(New ColorItem(c, prop.Name) With {.Index = i, .Tag = i})
                i += 1
            End If
        Next
    End Sub

    Private Sub LoadAllColors()
        AllColorItems = New CircularObservableCollection(Of ColorItem)
        Dim colType As Type = GetType(System.Windows.Media.Colors)
        Dim i As Integer
        For Each prop As PropertyInfo In colType.GetProperties()
            If prop.PropertyType Is GetType(System.Windows.Media.Color) Then
                Dim c As Color = prop.GetValue(prop)
                AllColorItems.Add(New ColorItem(c, prop.Name) With {.Index = i, .Tag = i})
                i += 1
            End If
        Next
        colType = GetType(System.Windows.SystemColors)
        For Each prop As PropertyInfo In colType.GetProperties()
            If prop.PropertyType Is GetType(System.Windows.Media.Color) Then
                Dim c As Color = prop.GetValue(prop)
                AllColorItems.Add(New ColorItem(c, prop.Name) With {.Index = i, .Tag = i})
                i += 1
            End If
        Next
    End Sub

    Public Sub SelectItem(ByVal br As SolidColorBrush)
        Dim q = From ci As ColorItem In AllColorItems
                Where ci.Brush.Color = br.Color
                Select ci

        If q.Count > 0 Then
            UI.Child = q(0)
            RaiseEvent SelectedItemChanged(Me, New SelectedItemChangedEventArgs(q(0)))
        End If

    End Sub

    Public Sub SelectIndex(ByVal i As Integer)
        Dim q = From ci As ColorItem In AllColorItems
                Where ci.Index = i
                Select ci

        If q.Count > 0 Then
            UI.Child = q(0)
            RaiseEvent SelectedItemChanged(Me, New SelectedItemChangedEventArgs(q(0)))
        End If

    End Sub




    Private Sub TB_PreviewMouseWheel(sender As Object, e As MouseWheelEventArgs)
        If e.Delta > 0 Then
            PreviousColor()
        ElseIf e.Delta < 0 Then
            NextColor()
        End If
    End Sub

    Private Sub NextColor()
        Select Case DisplayType
            Case ColorType.WebColors
                UI.Child = ColorItems.NextElement
            Case ColorType.SystemColors
                UI.Child = SystemColorItems.NextElement
            Case ColorType.AllColors
                UI.Child = AllColorItems.NextElement
        End Select
        Dim ci As ColorItem = CType(UI.Child, ColorItem)
        If ci Is Nothing Then Return
        SelectedBrush = ci.Brush
        SelectedColor = ci.Color
        SelectedColorName = ci.ColorName
        RaiseEvent SelectedItemChanged(Me, New SelectedItemChangedEventArgs(ci))
    End Sub

    Private Sub PreviousColor()
        Select Case DisplayType
            Case ColorType.WebColors
                UI.Child = ColorItems.PreviousElement
            Case ColorType.SystemColors
                UI.Child = SystemColorItems.PreviousElement
            Case ColorType.AllColors
                UI.Child = AllColorItems.PreviousElement
        End Select
        Dim ci As ColorItem = CType(UI.Child, ColorItem)
        If ci Is Nothing Then Return
        SelectedBrush = ci.Brush
        SelectedColor = ci.Color
        SelectedColorName = ci.ColorName
        RaiseEvent SelectedItemChanged(Me, New SelectedItemChangedEventArgs(ci))
    End Sub



    Private Sub btnDown_Click(sender As Object, e As RoutedEventArgs)
        NextColor()
    End Sub

    Private Sub btnUp_Click(sender As Object, e As RoutedEventArgs)
        PreviousColor()
    End Sub
End Class
