using ClosedXML.Excel;

namespace ClassLibraryTreeView.Classes.CellStyle
{
    public class CellStyleForHeader : CellStyleDefault
    {
        public CellStyleForHeader() : base()
        {
            this.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            this.Fill.BackgroundColor = XLColor.Blue;
            this.Border.OutsideBorder = XLBorderStyleValues.Thin;
            this.Border.BottomBorderColor = XLColor.White;
            this.Border.TopBorderColor = XLColor.White;
            this.Border.LeftBorderColor = XLColor.White;
            this.Border.RightBorderColor = XLColor.White;
            this.Font.FontColor = XLColor.White;
        }
    }
}
