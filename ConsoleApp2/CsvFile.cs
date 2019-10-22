using System;
using System.Collections.Generic;
using System.Text;

namespace TddDemo
{
    public class CsvFile
    {
        public readonly List<Row> _rows;
        public bool IsProcessed { get; set; }
        public bool IsSaved { get; set; }

        public CsvFile(List<Row> rows)
        {
            _rows = rows;
            IsProcessed = false;
            IsSaved = false;
        }

        public void SaveExcelFile(ExcelFile _file)
        {
            IsSaved = true;
        }

        public static bool CsvValidRow(Row x)
        {
            return x.Cells.Count == 3;
        }
    }
}