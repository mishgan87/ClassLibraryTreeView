using ClosedXML.Excel;

namespace ClassLibraryTreeView.Classes.CellStyle
{
    public partial class CellStyleDefault
    {
        public class CellStyleNumberFormat : IXLNumberFormat
        {
            public CellStyleNumberFormat(IXLStyle parent)
            {
                NumberFormatId = 0;
                Format = "";
                Parent = parent;
            }
            public int NumberFormatId { get; set; }
            public string Format { get; set; }
            public IXLStyle Parent { get; set; }
            public bool Equals(IXLNumberFormatBase other)
            {
                if (NumberFormatId != other.NumberFormatId || !Format.Equals(other.Format))
                {
                    return false;
                }
                return true;
            }

            IXLStyle IXLNumberFormat.SetFormat(string value)
            {
                throw new System.NotImplementedException();
            }

            IXLStyle IXLNumberFormat.SetNumberFormatId(int value)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
