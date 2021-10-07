using ClosedXML.Excel;

namespace ClassLibraryTreeView.Classes.CellStyle
{
    public class CellStyleAlignment : IXLAlignment
    {
        public CellStyleAlignment(IXLStyle parent)
        {
            Horizontal = XLAlignmentHorizontalValues.Center;
            Vertical = XLAlignmentVerticalValues.Center;
            Indent = 0;
            JustifyLastLine = false;
            ReadingOrder = XLAlignmentReadingOrderValues.ContextDependent;
            RelativeIndent = 0;
            ShrinkToFit = true;
            TextRotation = 0;
            WrapText = true;
            TopToBottom = true;
            Parent = parent;
        }
        public XLAlignmentHorizontalValues Horizontal { get; set; }
        public XLAlignmentVerticalValues Vertical { get; set; }
        public int Indent { get; set; }
        public bool JustifyLastLine { get; set; }
        public XLAlignmentReadingOrderValues ReadingOrder { get; set; }
        public int RelativeIndent { get; set; }
        public bool ShrinkToFit { get; set; }
        public int TextRotation { get; set; }
        public bool WrapText { get; set; }
        public bool TopToBottom { get; set; }
        public IXLStyle Parent { get; set; }

        public bool Equals(IXLAlignment other)
        {
            if ( !Horizontal.Equals(other.Horizontal)
                || !Vertical.Equals(other.Vertical)
                || !Indent.Equals(other.Indent)
                || !JustifyLastLine.Equals(other.JustifyLastLine)
                || !ReadingOrder.Equals(other.ReadingOrder)
                || !RelativeIndent.Equals(other.RelativeIndent)
                || !ShrinkToFit.Equals(other.ShrinkToFit)
                || !TextRotation.Equals(other.TextRotation)
                || !WrapText.Equals(other.WrapText)
                || !TopToBottom.Equals(other.TopToBottom))
            {
                return false;
            }
            return true;
        }

        public IXLStyle SetHorizontal(XLAlignmentHorizontalValues value)
        {
            Horizontal = value;
            return Parent;
        }

        public IXLStyle SetIndent(int value)
        {
            Indent = value;
            return Parent;
        }

        public IXLStyle SetJustifyLastLine()
        {
            JustifyLastLine = true;
            return Parent;
        }

        public IXLStyle SetJustifyLastLine(bool value)
        {
            JustifyLastLine = value;
            return Parent;
        }

        public IXLStyle SetReadingOrder(XLAlignmentReadingOrderValues value)
        {
            ReadingOrder = value;
            return Parent;
        }

        public IXLStyle SetRelativeIndent(int value)
        {
            RelativeIndent = value;
            return Parent;
        }

        public IXLStyle SetShrinkToFit()
        {
            ShrinkToFit = true;
            return Parent;
        }

        public IXLStyle SetShrinkToFit(bool value)
        {
            ShrinkToFit = value;
            return Parent;
        }

        public IXLStyle SetTextRotation(int value)
        {
            TextRotation = value;
            return Parent;
        }

        public IXLStyle SetTopToBottom()
        {
            TopToBottom = true;
            return Parent;
        }

        public IXLStyle SetTopToBottom(bool value)
        {
            TopToBottom = value;
            return Parent;
        }

        public IXLStyle SetVertical(XLAlignmentVerticalValues value)
        {
            Vertical = value;
            return Parent;
        }

        public IXLStyle SetWrapText()
        {
            WrapText = true;
            return Parent;
        }

        public IXLStyle SetWrapText(bool value)
        {
            WrapText = value;
            return Parent;
        }
    }
}
