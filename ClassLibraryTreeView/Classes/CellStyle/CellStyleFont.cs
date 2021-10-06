using ClosedXML.Excel;

namespace ClassLibraryTreeView.Classes.CellStyle
{
    public partial class CellStyleDefault
    {
        public class CellStyleFont : IXLFont
        {
            public CellStyleFont(IXLStyle parent)
            {
                Bold = false;
                Italic = false;
                Underline = XLFontUnderlineValues.None;
                Strikethrough = false;
                VerticalAlignment = XLFontVerticalTextAlignmentValues.Baseline;
                Shadow = false;
                FontSize = 10;
                FontColor = XLColor.Black;
                FontName = "Calibri";
                FontFamilyNumbering = XLFontFamilyNumberingValues.NotApplicable;
                FontCharSet = XLFontCharSet.Ansi;
                Parent = parent;
            }
            public bool Bold { get; set; }
            public bool Italic { get; set; }
            public XLFontUnderlineValues Underline { get; set; }
            public bool Strikethrough { get; set; }
            public XLFontVerticalTextAlignmentValues VerticalAlignment { get; set; }
            public bool Shadow { get; set; }
            public double FontSize { get; set; }
            public XLColor FontColor { get; set; }
            public string FontName { get; set; }
            public XLFontFamilyNumberingValues FontFamilyNumbering { get; set; }
            public XLFontCharSet FontCharSet { get; set; }
            public IXLStyle Parent { get; set; }

            public bool Equals(IXLFont other)
            {
                if (Bold != other.Bold
                    || Italic != other.Italic
                    || Underline != other.Underline
                    || Strikethrough != other.Strikethrough
                    || VerticalAlignment != other.VerticalAlignment
                    || Shadow != other.Shadow
                    || FontSize != other.FontSize
                    || FontColor != other.FontColor
                    || !FontName.Equals(other.FontName)
                    || FontFamilyNumbering != other.FontFamilyNumbering
                    || FontCharSet != other.FontCharSet)
                {
                    return false;
                }
                return true;
            }

            public IXLStyle SetBold()
            {
                Bold = true;
                return Parent;
            }

            public IXLStyle SetBold(bool value)
            {
                Bold = value;
                return Parent;
            }

            public IXLStyle SetFontCharSet(XLFontCharSet value)
            {
                FontCharSet = value;
                return Parent;
            }

            public IXLStyle SetFontColor(XLColor value)
            {
                FontColor = value;
                return Parent;
            }

            public IXLStyle SetFontFamilyNumbering(XLFontFamilyNumberingValues value)
            {
                FontFamilyNumbering = value;
                return Parent;
            }

            public IXLStyle SetFontName(string value)
            {
                FontName = value;
                return Parent;
            }

            public IXLStyle SetFontSize(double value)
            {
                FontSize = value;
                return Parent;
            }

            public IXLStyle SetItalic()
            {
                Italic = true;
                return Parent;
            }

            public IXLStyle SetItalic(bool value)
            {
                Italic = value;
                return Parent;
            }

            public IXLStyle SetShadow()
            {
                Shadow = true;
                return Parent;
            }

            public IXLStyle SetShadow(bool value)
            {
                Shadow = value;
                return Parent;
            }

            public IXLStyle SetStrikethrough()
            {
                Strikethrough = true;
                return Parent;
            }

            public IXLStyle SetStrikethrough(bool value)
            {
                Strikethrough = value;
                return Parent;
            }

            public IXLStyle SetUnderline()
            {
                Underline = XLFontUnderlineValues.None;
                return Parent;
            }

            public IXLStyle SetUnderline(XLFontUnderlineValues value)
            {
                Underline = value;
                return Parent;
            }

            public IXLStyle SetVerticalAlignment(XLFontVerticalTextAlignmentValues value)
            {
                VerticalAlignment = value;
                return Parent;
            }
        }
    }
}
