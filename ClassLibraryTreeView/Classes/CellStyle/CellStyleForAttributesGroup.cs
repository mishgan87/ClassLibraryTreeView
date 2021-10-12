using ClosedXML.Excel;

namespace ClassLibraryTreeView.Classes.CellStyle
{
    public class CellStyleForAttributesGroup : CellStyleDefault
    {
        public CellStyleForAttributesGroup() : base()
        {
            this.Fill.BackgroundColor = XLColor.DarkBlue;
            this.Border.OutsideBorder = XLBorderStyleValues.Thin;
            this.Border.BottomBorderColor = XLColor.White;
            this.Border.TopBorderColor = XLColor.White;
            this.Border.LeftBorderColor = XLColor.White;
            this.Border.RightBorderColor = XLColor.White;
            this.Font.FontColor = XLColor.White;
        }
    }
}
