using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Device.Location;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using ParApply.Business;
using ParApply.Services;

namespace ParApply
{
    public partial class MainPage : PhoneApplicationPage
    {
        private GeoCoordinateWatcher _coordinateWatcher;  
        private YrService _yrService;
        private ParaplyService _paraplyService;
        private GeoCoordinate _myLocation;
        private static Noreg _norge;
        private Sted _sted;
        private BackgroundWorker _backgroundWorker;
        private StedParser _norgeParser;
        private bool _isFirstLookup = false;
        private bool _parsingComplete;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            _coordinateWatcher = new GeoCoordinateWatcher();
            _coordinateWatcher.PositionChanged += SaveCurrentPosition;
            _coordinateWatcher.Start(false);
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += ParseNorgeFile;
            _backgroundWorker.RunWorkerCompleted += SetParsingCompleted;
            
            // Stop dreaming about IoC-containers!
            _norgeParser = new StedParser();
            _norge = new Noreg();
            _paraplyService = new ParaplyService();
            var webRequestFactory = new WebRequestFactory();
            _yrService = new YrService(webRequestFactory);
             _backgroundWorker.RunWorkerAsync();
        }

  

        private void ParseNorgeFile(object sender, DoWorkEventArgs e)
        {
            using (var stream = ResourceHelper.Noreg())
            {
                var steder =  _norgeParser.Parse(stream);
                foreach (var sted in steder)
                {
                    _norge.AddSted(sted);
                }
            }
        }

        private void SetParsingCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _parsingComplete = true;
            TryUpdateData();
        }

        private void TryUpdateData()
        {
            if(_parsingComplete && _myLocation != null)
            {

                _myLocation = new GeoCoordinate(59.923861, 10.7579014);
                _sted = _norge.FindClosestSted(_myLocation);
                _yrService.GetYrData(_sted, UpdateUI);
            }
        }


        private void SaveCurrentPosition(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            _isFirstLookup = _myLocation == null;
            _myLocation = e.Position.Location;

            if (_isFirstLookup)
            {   
                _coordinateWatcher.Stop();
                _coordinateWatcher.Dispose();
            }
            TryUpdateData();
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
                StedInfoTextBlock.Text = string.Format("{0}, {1}, {2} ", _sted.Navn, useParaply.YrData.SymbolName, useParaply.YrData.GetPeriode());    
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