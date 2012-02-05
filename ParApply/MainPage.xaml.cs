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
            _myLocation = new GeoCoordinate(60.3929932744419 , 5.32415240610052);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var sted = _norge.FindClosestSted(_myLocation);
            _yrService.GetYrData(sted, UpdateUI);
        }

        private void UpdateUI(Result<IEnumerable<YrData>> yrResult)
        {
            var useParaply = _paraplyService.ShouldUseParaply(yrResult);
            
            // bitmaps must be instantiated on UI thread.
            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => SetImage(useParaply));
        }

        private void SetImage(UseParaply useParaply)
        {
            switch (useParaply)
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