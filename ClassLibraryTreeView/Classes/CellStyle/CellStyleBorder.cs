using ClosedXML.Excel;

namespace ClassLibraryTreeView.Classes.CellStyle
{
    public partial class CellStyleDefault
    {
        public class CellStyleBorder : IXLBorder
        {
            public CellStyleBorder(IXLStyle parent)
            {
                OutsideBorder = XLBorderStyleValues.Thin;
                OutsideBorderColor = XLColor.Black;
                InsideBorder = XLBorderStyleValues.Thin;
                InsideBorderColor = XLColor.Black;
                LeftBorder = XLBorderStyleValues.Thin;
                LeftBorderColor = XLColor.Black;
                RightBorder = XLBorderStyleValues.Thin;
                RightBorderColor = XLColor.Black;
                TopBorder = XLBorderStyleValues.Thin;
                TopBorderColor = XLColor.Black;
                BottomBorder = XLBorderStyleValues.Thin;
                BottomBorderColor = XLColor.Black;
                DiagonalUp = false;
                DiagonalDown = false;
                DiagonalBorder = XLBorderStyleValues.Thin;
                DiagonalBorderColor = XLColor.Black;
                Parent = parent;
            }

            public XLBorderStyleValues OutsideBorder { get; set; }
            public XLColor OutsideBorderColor { get; set; }
            public XLBorderStyleValues InsideBorder { get; set; }
            public XLColor InsideBorderColor { get; set; }
            public XLBorderStyleValues LeftBorder { get; set; }
            public XLColor LeftBorderColor { get; set; }
            public XLBorderStyleValues RightBorder { get; set; }
            public XLColor RightBorderColor { get; set; }
            public XLBorderStyleValues TopBorder { get; set; }
            public XLColor TopBorderColor { get; set; }
            public XLBorderStyleValues BottomBorder { get; set; }
            public XLColor BottomBorderColor { get; set; }
            public bool DiagonalUp { get; set; }
            public bool DiagonalDown { get; set; }
            public XLBorderStyleValues DiagonalBorder { get; set; }
            public XLColor DiagonalBorderColor { get; set; }
            IXLStyle Parent { get; set; }

            public bool Equals(IXLBorder other)
            {
                
                if (!this.Equals(other)
                    /*
                    OutsideBorder != other.OutsideBorder
                    || !OutsideBorderColor.Equals(other.OutsideBorderColor)
                    || !InsideBorder.Equals(other.InsideBorder)
                    || !InsideBorderColor.Equals(other)
                    || !LeftBorder.Equals(other)
                    || !LeftBorderColor.Equals(other)
                    || !RightBorder.Equals(other)
                    || !RightBorderColor.Equals(other)
                    || !TopBorder.Equals(other)
                    || !TopBorderColor.Equals(other)
                    || !BottomBorder.Equals(other)
                    || !BottomBorderColor.Equals(other)
                    || !DiagonalUp.Equals(other)
                    || !DiagonalDown.Equals(other)
                    || !DiagonalBorder.Equals(other)
                    || !DiagonalBorderColor.Equals(other)*/)
                {
                    return false;
                }
                
                return true;
            }

            public IXLStyle SetBottomBorder(XLBorderStyleValues value)
            {
                BottomBorder = value;
                return Parent;
            }

            public IXLStyle SetBottomBorderColor(XLColor value)
            {
                BottomBorderColor = value;
                return Parent;
            }

            public IXLStyle SetDiagonalBorder(XLBorderStyleValues value)
            {
                DiagonalBorder = value;
                return Parent;
            }

            public IXLStyle SetDiagonalBorderColor(XLColor value)
            {
                DiagonalBorderColor = value;
                return Parent;
            }

            public IXLStyle SetDiagonalDown()
            {
                DiagonalDown = true;
                return Parent;
            }

            public IXLStyle SetDiagonalDown(bool value)
            {
                DiagonalDown = value;
                return Parent;
            }

            public IXLStyle SetDiagonalUp()
            {
                DiagonalUp = true;
                return Parent;
            }

            public IXLStyle SetDiagonalUp(bool value)
            {
                DiagonalUp = value;
                return Parent;
            }

            public IXLStyle SetInsideBorder(XLBorderStyleValues value)
            {
                InsideBorder = value;
                return Parent;
            }

            public IXLStyle SetInsideBorderColor(XLColor value)
            {
                InsideBorderColor = value;
                return Parent;
            }

            public IXLStyle SetLeftBorder(XLBorderStyleValues value)
            {
                LeftBorder = value;
                return Parent;
            }

            public IXLStyle SetLeftBorderColor(XLColor value)
            {
                LeftBorderColor = value;
                return Parent;
            }

            public IXLStyle SetOutsideBorder(XLBorderStyleValues value)
            {
                OutsideBorder = value;
                return Parent;
            }

            public IXLStyle SetOutsideBorderColor(XLColor value)
            {
                OutsideBorderColor = value;
                return Parent;
            }

            public IXLStyle SetRightBorder(XLBorderStyleValues value)
            {
                RightBorder = value;
                return Parent;
            }

            public IXLStyle SetRightBorderColor(XLColor value)
            {
                RightBorderColor = value;
                return Parent;
            }

            public IXLStyle SetTopBorder(XLBorderStyleValues value)
            {
                TopBorder = value;
                return Parent;
            }

            public IXLStyle SetTopBorderColor(XLColor value)
            {
                TopBorderColor = value;
                return Parent;
            }
        }
    }
}
