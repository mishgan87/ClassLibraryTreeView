using ClosedXML.Excel;

namespace ClassLibraryTreeView.Classes.CellStyle
{
    public class CellStyleForClassDark : CellStyleDefault
    {
        public CellStyleForClassDark() : base()
        {
            this.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            this.Font.FontColor = XLColor.Black;
            this.Fill.BackgroundColor = XLColor.LightGray;
        }
    }
}
