using System.Collections.Generic;

namespace TddDemo
{
    public class ExcelFile
    {
        public readonly List<Row> _rows;
        public bool IsProcessed { get; set; }
        public bool IsSaved { get; set; }

        public ExcelFile(List<Row> rows)
        {
            _rows = rows;
            IsProcessed = false;
            IsSaved = false;
        }

        public void SaveExcelFile(ExcelFile _file)
        {
            IsSaved = true;
        }

        public static bool ExcelValidRow(Row x)
        {
            return x.Cells.Count == 2;
        }
    }
    
}