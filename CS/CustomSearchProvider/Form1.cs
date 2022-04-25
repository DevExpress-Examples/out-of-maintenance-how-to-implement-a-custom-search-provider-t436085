using DevExpress.XtraMap;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomSearchProvider {
    public partial class Form1 : Form {
        InformationLayer Layer { get { return (InformationLayer)mapControl1.Layers[1]; } }
        public Form1() {
            InitializeComponent();
            SearchProvider provider = new SearchProvider();
            Layer.DataProvider = provider;
        }
    }
    public class SearchProvider : InformationDataProviderBase, ISearchPanelRequestSender {
        protected new SearchData Data { get { return (SearchData)base.Data; } }
        public IEnumerable<LocationInformation> Addresses { get { return Data.Addresses; } }
        protected override IInformationData CreateData() {
            return new SearchData();
        }
        public void SearchByString(string keyword) {
            Data.Search(keyword);
        }
    }
    public class SearchData : IInformationData {
        readonly List<LocationInformation> addresses = new List<LocationInformation>();
        public IEnumerable<LocationInformation> Addresses { get { return addresses; } }
        public event EventHandler<RequestCompletedEventArgs> OnDataResponse;
        readonly TaskScheduler scheduler;
        public SearchData() {
            scheduler = TaskScheduler.FromCurrentSynchronizationContext();
        }
        RequestCompletedEventArgs CreateEventArgs() {
            MapItem[] items = new MapItem[addresses.Count];
            for (int i = 0; i < items.Length; i++)
                items[i] = new MapCallout() { Location = addresses[i].Location, Text = addresses[i].Address.FormattedAddress };
            return new RequestCompletedEventArgs(items, null, false, null);
        }
        protected void RaiseChanged() {
            if (OnDataResponse != null)
                OnDataResponse(this, CreateEventArgs());
        }
        public void Search(string keyword) {
            Task.Factory.StartNew(async () => {
                Random rnd = new Random(DateTime.Now.Millisecond);
                addresses.Clear();
                int length = keyword.Length;
                for (int i = 0; i < length; i++) {
                    LocationInformation info = new LocationInformation();
                    info.Address = new Address(keyword + " " + i.ToString());
                    info.Location = new GeoPoint(rnd.Next(180) - 90, rnd.Next(360) - 180);
                    addresses.Add(info);
                }
                await Task.Delay(500);
                RaiseChanged();
            }, CancellationToken.None, TaskCreationOptions.LongRunning | TaskCreationOptions.DenyChildAttach, scheduler);
        }
    }
    public class Address : AddressBase {
        public Address(string address) {
            this.FormattedAddress = address;
        }
    }
}
