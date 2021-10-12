using ClosedXML.Excel;

namespace ClassLibraryTreeView.Classes.CellStyle
{
    public class CellStyleForPresenceUnselect : CellStyleDefault
    {
        public CellStyleForPresenceUnselect() : base()
        {
            this.Fill.BackgroundColor = XLColor.AntiFlashWhite;
        }
    }
}
