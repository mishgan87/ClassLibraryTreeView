using ClosedXML.Excel;

namespace ClassLibraryTreeView.Classes.CellStyle
{
    public class CellStyleForClassId : CellStyleDefault
    {
        public CellStyleForClassId() : base()
        {
            this.Fill.BackgroundColor = XLColor.Green;
        }
    }
}
