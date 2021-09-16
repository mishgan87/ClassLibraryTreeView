using ClosedXML.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryTreeView.Classes
{
    class GridCell : IXLCell
    {
        public bool Active { get; set; }

        public IXLAddress Address => throw new NotImplementedException();

        public object CachedValue => throw new NotImplementedException();

        public IXLComment Comment => throw new NotImplementedException();

        public IXLRange CurrentRegion => throw new NotImplementedException();

        public XLDataType DataType { get; set; }

        public IXLDataValidation DataValidation => throw new NotImplementedException();

        public string FormulaA1 { get; set; }
        public string FormulaR1C1 { get; set; }
        public IXLRangeAddress FormulaReference { get; set; }

        public bool HasArrayFormula => throw new NotImplementedException();

        public bool HasComment => throw new NotImplementedException();

        public bool HasDataValidation => throw new NotImplementedException();

        public bool HasFormula => throw new NotImplementedException();

        public bool HasHyperlink => throw new NotImplementedException();

        public bool HasRichText => throw new NotImplementedException();

        public bool HasSparkline => throw new NotImplementedException();

        public XLHyperlink Hyperlink { get; set; }

        public bool NeedsRecalculation => throw new NotImplementedException();

        public IXLDataValidation NewDataValidation => throw new NotImplementedException();

        public IXLRichText RichText => throw new NotImplementedException();

        public bool ShareString { get; set; }

        public IXLSparkline Sparkline => throw new NotImplementedException();

        public IXLStyle Style { get; set; }
        public object Value { get; set; }

        public IXLWorksheet Worksheet => throw new NotImplementedException();

        public IXLConditionalFormat AddConditionalFormat()
        {
            throw new NotImplementedException();
        }

        public IXLCell AddToNamed(string rangeName)
        {
            throw new NotImplementedException();
        }

        public IXLCell AddToNamed(string rangeName, XLScope scope)
        {
            throw new NotImplementedException();
        }

        public IXLCell AddToNamed(string rangeName, XLScope scope, string comment)
        {
            throw new NotImplementedException();
        }

        public IXLRange AsRange()
        {
            throw new NotImplementedException();
        }

        public IXLCell CellAbove()
        {
            throw new NotImplementedException();
        }

        public IXLCell CellAbove(int step)
        {
            throw new NotImplementedException();
        }

        public IXLCell CellBelow()
        {
            throw new NotImplementedException();
        }

        public IXLCell CellBelow(int step)
        {
            throw new NotImplementedException();
        }

        public IXLCell CellLeft()
        {
            throw new NotImplementedException();
        }

        public IXLCell CellLeft(int step)
        {
            throw new NotImplementedException();
        }

        public IXLCell CellRight()
        {
            throw new NotImplementedException();
        }

        public IXLCell CellRight(int step)
        {
            throw new NotImplementedException();
        }

        public IXLCell Clear(XLClearOptions clearOptions = XLClearOptions.All)
        {
            throw new NotImplementedException();
        }

        public IXLCell CopyFrom(IXLCell otherCell)
        {
            throw new NotImplementedException();
        }

        public IXLCell CopyFrom(string otherCell)
        {
            throw new NotImplementedException();
        }

        public IXLCell CopyTo(IXLCell target)
        {
            throw new NotImplementedException();
        }

        public IXLCell CopyTo(string target)
        {
            throw new NotImplementedException();
        }

        public void Delete(XLShiftDeletedCells shiftDeleteCells)
        {
            throw new NotImplementedException();
        }

        public bool GetBoolean()
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime()
        {
            throw new NotImplementedException();
        }

        public double GetDouble()
        {
            throw new NotImplementedException();
        }

        public string GetFormattedString()
        {
            throw new NotImplementedException();
        }

        public XLHyperlink GetHyperlink()
        {
            throw new NotImplementedException();
        }

        public string GetString()
        {
            throw new NotImplementedException();
        }

        public TimeSpan GetTimeSpan()
        {
            throw new NotImplementedException();
        }

        public T GetValue<T>()
        {
            throw new NotImplementedException();
        }

        public IXLCells InsertCellsAbove(int numberOfRows)
        {
            throw new NotImplementedException();
        }

        public IXLCells InsertCellsAfter(int numberOfColumns)
        {
            throw new NotImplementedException();
        }

        public IXLCells InsertCellsBefore(int numberOfColumns)
        {
            throw new NotImplementedException();
        }

        public IXLCells InsertCellsBelow(int numberOfRows)
        {
            throw new NotImplementedException();
        }

        public IXLRange InsertData(IEnumerable data)
        {
            throw new NotImplementedException();
        }

        public IXLRange InsertData(IEnumerable data, bool transpose)
        {
            throw new NotImplementedException();
        }

        public IXLRange InsertData(DataTable dataTable)
        {
            throw new NotImplementedException();
        }

        public IXLTable InsertTable<T>(IEnumerable<T> data)
        {
            throw new NotImplementedException();
        }

        public IXLTable InsertTable<T>(IEnumerable<T> data, bool createTable)
        {
            throw new NotImplementedException();
        }

        public IXLTable InsertTable<T>(IEnumerable<T> data, string tableName)
        {
            throw new NotImplementedException();
        }

        public IXLTable InsertTable<T>(IEnumerable<T> data, string tableName, bool createTable)
        {
            throw new NotImplementedException();
        }

        public IXLTable InsertTable(DataTable data)
        {
            throw new NotImplementedException();
        }

        public IXLTable InsertTable(DataTable data, bool createTable)
        {
            throw new NotImplementedException();
        }

        public IXLTable InsertTable(DataTable data, string tableName)
        {
            throw new NotImplementedException();
        }

        public IXLTable InsertTable(DataTable data, string tableName, bool createTable)
        {
            throw new NotImplementedException();
        }

        public void InvalidateFormula()
        {
            throw new NotImplementedException();
        }

        public bool IsEmpty()
        {
            throw new NotImplementedException();
        }

        public bool IsEmpty(bool includeFormats)
        {
            throw new NotImplementedException();
        }

        public bool IsEmpty(XLCellsUsedOptions options)
        {
            throw new NotImplementedException();
        }

        public bool IsMerged()
        {
            throw new NotImplementedException();
        }

        public IXLRange MergedRange()
        {
            throw new NotImplementedException();
        }

        public void Select()
        {
            throw new NotImplementedException();
        }

        public IXLCell SetActive(bool value = true)
        {
            throw new NotImplementedException();
        }

        public IXLCell SetDataType(XLDataType dataType)
        {
            throw new NotImplementedException();
        }

        public IXLDataValidation SetDataValidation()
        {
            throw new NotImplementedException();
        }

        public IXLCell SetFormulaA1(string formula)
        {
            throw new NotImplementedException();
        }

        public IXLCell SetFormulaR1C1(string formula)
        {
            throw new NotImplementedException();
        }

        public IXLCell SetValue<T>(T value)
        {
            throw new NotImplementedException();
        }

        public XLTableCellType TableCellType()
        {
            throw new NotImplementedException();
        }

        public string ToString(string format)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue<T>(out T value)
        {
            throw new NotImplementedException();
        }

        public IXLColumn WorksheetColumn()
        {
            throw new NotImplementedException();
        }

        public IXLRow WorksheetRow()
        {
            throw new NotImplementedException();
        }
    }
}
