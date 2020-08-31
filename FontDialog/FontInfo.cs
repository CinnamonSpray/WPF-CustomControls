using System.Windows.Media;

namespace CustomControls
{
    public class FontInfo
    {
        public FontFamily Family { get; set; }
        public double Size { get; set; }

        public FontInfo() { }

        public FontInfo(FontFamily family, double size)
        {
            Family = family;
            Size = size;
        }
    }
}
