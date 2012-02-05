using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace ParApply
{
    public partial class MainPage : PhoneApplicationPage
    {
        private GeoCoordinateWatcher _geoLocationService; // need to implement Get My Location  
        private YrService _yrService;
        private ParaplyService _paraplyService;
        private GeoCoordinate _myLocation;
        private static Noreg _norge;
        private Sted _sted;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            _geoLocationService = new GeoCoordinateWatcher();
            var noregParser = new NoregParser();
            _norge = noregParser.Parse(ResourceHelper.Noreg());
            _paraplyService = new ParaplyService();
            _yrService = new YrService();
            
            // MOCKED, Skøyen.
            _myLocation = new GeoCoordinate(59.931108, 10.685921); 
            
            // bergen loc: 
            //_myLocation = new GeoCoordinate(60.3929932744419 , 5.32415240610052);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _sted = _norge.FindClosestSted(_myLocation);
            _yrService.GetYrData(_sted, UpdateUI);
        }

        private void UpdateUI(Result<IEnumerable<YrData>> yrResult)
        {
            var useParaply = _paraplyService.ShouldUseParaply(yrResult);
            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => SetImage(useParaply));
        }

        private void SetImage(UseParaplyResult useParaply)
        {
            if(!useParaply.HasError())
            {
                StedInfoTextBlock.Text = string.Format("Sted: {0}, Varsel: {1}, Tidsrom: {2}-{3}: ", _sted.Navn, useParaply.YrData.SymbolName, useParaply.YrData.From, useParaply.YrData.To);    
            }
            
            switch (useParaply.Result)
            {
                case UseParaply.Unknown:
                    ParaplyImage.Source = ResourceHelper.QuestionMark();
                    break;
                case UseParaply.Yes:
                    ParaplyImage.Source = ResourceHelper.UseUmbrella();
                    break;
                case UseParaply.No:
                    ParaplyImage.Source = ResourceHelper.DontUseUmbrella();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    }
}