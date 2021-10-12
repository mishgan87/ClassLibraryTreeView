using ClosedXML.Excel;

namespace ClassLibraryTreeView.Classes.CellStyle
{
    public class CellStyleForAttribute : CellStyleDefault
    {
        public CellStyleForAttribute() : base()
        {
            this.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            this.Alignment.Vertical = XLAlignmentVerticalValues.Bottom;
            this.Alignment.TextRotation = 90;
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
