using ClosedXML.Excel;

namespace ClassLibraryTreeView.Classes.CellStyle
{
    public class CellStyleForPresenceOptional : CellStyleDefault
    {
        public CellStyleForPresenceOptional() : base()
        {
            this.Fill.BackgroundColor = XLColor.AppleGreen;
        }
    }
}
