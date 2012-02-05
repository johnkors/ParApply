using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace ParApply
{
    public partial class MainPage : PhoneApplicationPage
    {
        private GeoCoordinateWatcher _geoLocationService;
        private YrService _yrService;
        private ParaplyService _paraplyService;
        
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            _yrService = new YrService();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var location = _geoLocationService.GetLocation();
            var yrdata = _yrService.GetYrData(location);
            var useParaply = _paraplyService.ShouldUseParaply(yrdata);
            if(useParaply)
            {
                
            }
            else
            {
                
            }
        }
    }

    internal class ParaplyService
    {
        public bool ShouldUseParaply(YrData yrdata)
        {
            
        }
    }

    public class YrService 
    {
        public YrData GetYrData(Location location)
        {
            
        }
    }

    public class YrData
    {
    }
}