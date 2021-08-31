Imports System.Reflection
Imports ColorUpDownLib
Class MainWindow

    Public Property Fore As ColorItem
    Public Property Back As ColorItem

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        rbAllBack.IsChecked = True
        rbAllFore.IsChecked = True
        cbFore.SelectItem(Brushes.Black)
        cbBack.SelectItem(Brushes.White)
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub rbWebFore_Checked(sender As Object, e As RoutedEventArgs)
        cbFore.DisplayType = ColorType.WebColors

    End Sub

    Private Sub rbSysFore_Checked(sender As Object, e As RoutedEventArgs)
        cbFore.DisplayType = ColorType.SystemColors
    End Sub

    Private Sub rbAllFore_Checked(sender As Object, e As RoutedEventArgs)
        cbFore.DisplayType = ColorType.AllColors
    End Sub

    Private Sub rbWebBack_Checked(sender As Object, e As RoutedEventArgs)
        cbBack.DisplayType = ColorType.WebColors

    End Sub

    Private Sub rbSysBack_Checked(sender As Object, e As RoutedEventArgs)
        cbBack.DisplayType = ColorType.SystemColors
    End Sub

    Private Sub rbAllBack_Checked(sender As Object, e As RoutedEventArgs)
        cbBack.DisplayType = ColorType.AllColors
    End Sub

    Private Sub cbBack_SelectedItemChanged(sender As Object, e As SelectedItemChangedEventArgs)
        Back = e.Item
        If Fore Is Nothing Then Return
        TBBack.Text = "Background " & e.Item.Brush.ToString & " " & e.Item.ColorName
        TBBack1.Background = e.Item.Brush
        TBBack1.Text = TBBack.Text
        TBBack1.Foreground = GetComplement(TBBack1.Background)
    End Sub

    Private Sub cbFore_SelectedItemChanged(sender As Object, e As SelectedItemChangedEventArgs)
        Fore = e.Item
        If Back Is Nothing Then Return
        TB.Text = "Foreground " & e.Item.Brush.ToString & " " & e.Item.ColorName
        TB1.Background = e.Item.Brush
        TB1.Text = TB.Text
        TB1.Foreground = GetComplement(TB1.Background)
    End Sub

#Region "ToolBarEx Loaded"



    Public Sub RemoveToolbarHandleAndOverflow(ByVal TB As ToolBarEx)

        Dim overflowPanel = TryCast(TB.Template.FindName("PART_ToolBarOverflowPanel", TB), FrameworkElement)
        If overflowPanel IsNot Nothing Then
            overflowPanel.Opacity = 0
            overflowPanel.Visibility = Visibility.Hidden
        End If

        Dim overflowGrid = TryCast(TB.Template.FindName("OverflowGrid", TB), FrameworkElement)
        If overflowGrid IsNot Nothing Then
            overflowGrid.Opacity = 0

            overflowGrid.Visibility = Visibility.Hidden
        End If

        Dim mainPanelBorder = TryCast(TB.Template.FindName("MainPanelBorder", TB), FrameworkElement)
        If mainPanelBorder IsNot Nothing Then
            mainPanelBorder.Margin = New Thickness(0)
        End If
    End Sub

    Private Sub ToolBarEx_Loaded(sender As Object, e As RoutedEventArgs)
        Dim TB As ToolBarEx = CType(sender, ToolBar)
        RemoveToolbarHandleAndOverflow(TB)
    End Sub


#End Region

    Private Function GetComplement(ByVal br As SolidColorBrush) As SolidColorBrush
        Dim a, r, g, b As Byte
        a = br.Color.A
        r = br.Color.R Xor &H80
        g = br.Color.G Xor &H80
        b = br.Color.B Xor &H80
        Dim clr As New Color()
        clr.A = a
        clr.R = r
        clr.G = g
        clr.B = b
        Return New SolidColorBrush(clr)
    End Function

End Class
