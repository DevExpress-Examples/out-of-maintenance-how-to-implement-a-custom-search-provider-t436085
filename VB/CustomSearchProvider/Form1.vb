Imports DevExpress.XtraMap
Imports System
Imports System.Collections.Generic
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms

Namespace CustomSearchProvider
    Partial Public Class Form1
        Inherits Form

        Private ReadOnly Property Layer() As InformationLayer
            Get
                Return CType(mapControl1.Layers(1), InformationLayer)
            End Get
        End Property
        Public Sub New()
            InitializeComponent()
            Dim provider As New SearchProvider()
            Layer.DataProvider = provider
        End Sub
    End Class
    Public Class SearchProvider
        Inherits InformationDataProviderBase
        Implements ISearchPanelRequestSender

        Protected Shadows ReadOnly Property Data() As SearchData
            Get
                Return CType(MyBase.Data, SearchData)
            End Get
        End Property

        Private ReadOnly Property Addresses As IEnumerable(Of LocationInformation) Implements ISearchPanelRequestSender.Addresses
            Get
                Return Data.Addresses
            End Get
        End Property

        Protected Overrides Function CreateData() As IInformationData
            Return New SearchData()
        End Function

        Private Sub SearchByString(keyword As String) Implements ISearchPanelRequestSender.SearchByString
            Data.Search(keyword)
        End Sub
    End Class
    Public Class SearchData
        Implements IInformationData


        Private ReadOnly addresses_Renamed As New List(Of LocationInformation)()

        Public ReadOnly Property Addresses() As IEnumerable(Of LocationInformation)
            Get
                Return addresses_Renamed
            End Get
        End Property
        Private Event OnDataResponse As EventHandler(Of RequestCompletedEventArgs) Implements IInformationData.OnDataResponse
        Private ReadOnly scheduler As TaskScheduler

        Public Sub New()
            scheduler = TaskScheduler.FromCurrentSynchronizationContext()
        End Sub
        Private Function CreateEventArgs() As RequestCompletedEventArgs
            Dim items(addresses_Renamed.Count - 1) As MapItem
            For i As Integer = 0 To items.Length - 1
                items(i) = New MapCallout() With {.Location = addresses_Renamed(i).Location, .Text = addresses_Renamed(i).Address.FormattedAddress}
            Next i
            Return New RequestCompletedEventArgs(items, Nothing, False, Nothing)
        End Function
        Protected Sub RaiseChanged()
            RaiseEvent OnDataResponse(Me, CreateEventArgs())
        End Sub
        Public Sub Search(ByVal keyword As String)
            Call Task.Factory.StartNew(Async Function()
                                           Dim rnd As Random = New Random(Date.Now.Millisecond)
                                           addresses_Renamed.Clear()
                                           Dim length = keyword.Length

                                           For i = 0 To length - 1
                                               Dim info As LocationInformation = New LocationInformation()
                                               info.Address = New Address(keyword & " " & i.ToString())
                                               info.Location = New GeoPoint(rnd.Next(180) - 90, rnd.Next(360) - 180)
                                               addresses_Renamed.Add(info)
                                           Next

                                           Await Task.Delay(500)
                                           RaiseChanged()
                                       End Function, CancellationToken.None, TaskCreationOptions.LongRunning Or TaskCreationOptions.DenyChildAttach, scheduler)
        End Sub

    End Class
    Public Class Address
        Inherits AddressBase

        Public Sub New(ByVal address As String)
            Me.FormattedAddress = address
        End Sub
    End Class
End Namespace
