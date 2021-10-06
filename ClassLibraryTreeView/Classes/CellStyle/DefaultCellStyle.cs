using ClosedXML.Excel;

namespace ClassLibraryTreeView.Classes.CellStyle
{
    public partial class CellStyleDefault : IXLStyle
    {
        public CellStyleDefault()
        {
            this.Alignment = new CellStyleAlignment(this);
            this.Border = new CellStyleBorder(this);
            // this.DateFormat = new CellStyleNumberFormat(this);
            this.Fill = new CellStyleFill(this);
            this.Font = new CellStyleFont(this);
            this.IncludeQuotePrefix = true;
            this.NumberFormat = new CellStyleNumberFormat(this);
            this.Protection = new CellStyleProtection(this);
        }
        public IXLAlignment Alignment { get; set; }
        public IXLBorder Border { get; set; }
        public IXLNumberFormat DateFormat => null;
        public IXLFill Fill { get; set; }
        public IXLFont Font { get; set; }
        public bool IncludeQuotePrefix { get; set; }
        public IXLNumberFormat NumberFormat { get; set; }
        public IXLProtection Protection { get; set; }

        public bool Equals(IXLStyle other)
        {
            if (!other.Alignment.Equals(this.Alignment)
                || !other.Border.Equals(this.Border)
                || !other.DateFormat.Equals(this.DateFormat)
                || !other.Fill.Equals(this.Fill)
                || !other.Font.Equals(this.Font)
                || !other.IncludeQuotePrefix.Equals(this.IncludeQuotePrefix)
                || !other.NumberFormat.Equals(this.NumberFormat)
                || !other.Protection.Equals(this.Protection))
            {
                return false;
            }
            return true;
        }

        public IXLStyle SetIncludeQuotePrefix(bool includeQuotePrefix = true)
        {
            IncludeQuotePrefix = includeQuotePrefix;
            return this;
        }
    }

    public class CellStyleForClass : CellStyleDefault
    {
        public CellStyleForClass() : base()
        {
            this.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            this.Font.FontColor = XLColor.Black;
        }
    }
    public class CellStyleForClassId : CellStyleDefault
    {
        public CellStyleForClassId() : base()
        {
            this.Fill.BackgroundColor = XLColor.Green;
        }
    }
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
    public class CellStyleForDiscipline : CellStyleDefault
    {
        public CellStyleForDiscipline() : base()
        {
            this.Fill.BackgroundColor = XLColor.Yellow;
        }
    }
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
    public class CellStyleForPresenceUnselect : CellStyleDefault
    {
        public CellStyleForPresenceUnselect() : base()
        {
            this.Fill.BackgroundColor = XLColor.AntiFlashWhite;
        }
    }
    public class CellStyleForPresenceNonApplicable : CellStyleDefault
    {
        public CellStyleForPresenceNonApplicable() : base()
        {
        }
    }
    public class CellStyleForPresenceOptional : CellStyleDefault
    {
        public CellStyleForPresenceOptional() : base()
        {
            this.Fill.BackgroundColor = XLColor.AppleGreen;
        }
    }
    public class CellStyleForPresencePreffered : CellStyleDefault
    {
        public CellStyleForPresencePreffered() : base()
        {
            this.Fill.BackgroundColor = XLColor.BrightGreen;
        }
    }
    public class CellStyleForPresenceRequired : CellStyleDefault
    {
        public CellStyleForPresenceRequired() : base()
        {
            this.Fill.BackgroundColor = XLColor.DarkGreen;
            this.Font.FontColor = XLColor.White;
        }
    }
}
