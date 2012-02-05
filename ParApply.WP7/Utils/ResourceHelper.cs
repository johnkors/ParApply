using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ParApply
{
    public class ResourceHelper
    {
        private static string NoregFilePath = "Contents/noreg.txt";
        private static string UseUmbrellaImagePath = "Contents/UseUmbrella.png";
        private static string DontUseUmbrellaImagePath = "Contents/DontUseUmbrella.png";
        private static string QuestionMarkPath = "Contents/QuestionMark.png";

        public static Stream Noreg()
        {
            var streamResourceInfo = App.GetResourceStream(GetUri(NoregFilePath));
            return streamResourceInfo.Stream;
        }

        public static ImageSource UseUmbrella()
        {
            return Bitmap(UseUmbrellaImagePath);
        }

        public static ImageSource DontUseUmbrella()
        {
            return Bitmap(DontUseUmbrellaImagePath);
        }

        public static ImageSource QuestionMark()
        {
            return Bitmap(QuestionMarkPath);
        }

        private static ImageSource Bitmap(string useUmbrellaImagePath)
        {
            return new BitmapImage(GetUri(useUmbrellaImagePath));
        }
        
        private static Uri GetUri(string path)
        {
            return new Uri(path, UriKind.Relative);
        }

        
    }
}
