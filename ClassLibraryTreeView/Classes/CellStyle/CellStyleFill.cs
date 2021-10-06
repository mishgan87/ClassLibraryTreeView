using ClosedXML.Excel;

namespace ClassLibraryTreeView.Classes.CellStyle
{
    public partial class CellStyleDefault
    {
        public class CellStyleFill : IXLFill
        {
            public CellStyleFill(IXLStyle parent)
            {
                BackgroundColor = XLColor.White;
                PatternColor = XLColor.White;
                PatternType = XLFillPatternValues.Solid;
                Parent = parent;
            }
            public XLColor BackgroundColor { get; set; }
            public XLColor PatternColor { get; set; }
            public XLFillPatternValues PatternType { get; set; }
            public IXLStyle Parent { get; set; }

            public bool Equals(IXLFill other)
            {
                if (!BackgroundColor.Equals(other.BackgroundColor)
                    || !PatternColor.Equals(other.PatternColor)
                    || !PatternType.Equals(other.PatternType))
                {
                    return false;
                }
                return true;
            }

            public IXLStyle SetBackgroundColor(XLColor value)
            {
                BackgroundColor = value;
                return Parent;
            }

            public IXLStyle SetPatternColor(XLColor value)
            {
                PatternColor = value;
                return Parent;
            }

            public IXLStyle SetPatternType(XLFillPatternValues value)
            {
                PatternType = value;
                return Parent;
            }
        }
    }
}
