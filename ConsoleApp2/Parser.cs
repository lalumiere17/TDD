using System.Linq;

namespace TddDemo
{
    public class Parser
    {
        public Parser(IAlertPublisher publisher)
        {
            Publisher = publisher;
        }

        public IAlertPublisher Publisher { get; }

        public int ExcelParse(ExcelFile excelFile)
        {
            for (var i = 0; i < excelFile._rows.Count(x => ExcelFile.ExcelValidRow(x) == false); i++)
                Publisher.PublishAlert();

            if (excelFile._rows.Count(x => ExcelFile.ExcelValidRow(x) == true) == excelFile._rows.Count())
            {
                excelFile.IsProcessed = true;
                excelFile.IsSaved = true;
            }
            return excelFile._rows.Count(x => ExcelFile.ExcelValidRow(x) == true);
        }

        public int CsvParse(CsvFile csvFile)
        {
            for (var i = 0; i < csvFile._rows.Count(x => CsvFile.CsvValidRow(x) == false); i++)
                Publisher.PublishAlert();

            if (csvFile._rows.Count(x => CsvFile.CsvValidRow(x) == true) == csvFile._rows.Count())
            {
                csvFile.IsProcessed = true;
                csvFile.IsSaved = true;
            }
            return csvFile._rows.Count(x => CsvFile.CsvValidRow(x) == true);
        }

    }
}