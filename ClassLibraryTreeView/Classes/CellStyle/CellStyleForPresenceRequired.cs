using ClosedXML.Excel;

namespace ClassLibraryTreeView.Classes.CellStyle
{
    public class CellStyleForPresenceRequired : CellStyleDefault
    {
        public CellStyleForPresenceRequired() : base()
        {
            this.Fill.BackgroundColor = XLColor.DarkGreen;
            this.Font.FontColor = XLColor.White;
        }
    }
}
