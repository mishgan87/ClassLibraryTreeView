using ClosedXML.Excel;

namespace ClassLibraryTreeView.Classes.CellStyle
{
    public class CellStyleForDiscipline : CellStyleDefault
    {
        public CellStyleForDiscipline() : base()
        {
            this.Fill.BackgroundColor = XLColor.Yellow;
        }
    }
}
