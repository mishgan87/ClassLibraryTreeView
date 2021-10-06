using ClosedXML.Excel;

namespace ClassLibraryTreeView.Classes.CellStyle
{
    public partial class CellStyleDefault
    {
        public class CellStyleProtection : IXLProtection
        {
            public CellStyleProtection(IXLStyle parent)
            {
                Locked = false;
                Hidden = false;
                Parent = parent;
            }
            public bool Locked { get; set; }
            public bool Hidden { get; set; }
            public IXLStyle Parent { get; set; }

            public bool Equals(IXLProtection other)
            {
                if (Locked != other.Locked || Hidden != other.Hidden)
                {
                    return false;
                }
                return true;
            }

            public IXLStyle SetHidden()
            {
                Hidden = true;
                return Parent;
            }

            public IXLStyle SetHidden(bool value)
            {
                Hidden = value;
                return Parent;
            }

            public IXLStyle SetLocked()
            {
                Locked = true;
                return Parent;
            }

            public IXLStyle SetLocked(bool value)
            {
                Locked = value;
                return Parent;
            }
        }
    }
}
