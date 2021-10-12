using ClosedXML.Excel;

namespace ClassLibraryTreeView.Classes.CellStyle
{
    public class CellStyleForPresencePreffered : CellStyleDefault
    {
        public CellStyleForPresencePreffered() : base()
        {
            this.Fill.BackgroundColor = XLColor.BrightGreen;
        }
    }
}
