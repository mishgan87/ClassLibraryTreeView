using ClosedXML.Excel;

namespace ClassLibraryTreeView.Classes.CellStyle
{
    public class CellStyleForClass : CellStyleDefault
    {
        public CellStyleForClass() : base()
        {
            this.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            this.Font.FontColor = XLColor.Black;
        }
    }
}
