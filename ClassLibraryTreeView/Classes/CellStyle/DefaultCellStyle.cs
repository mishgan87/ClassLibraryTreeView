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
}
