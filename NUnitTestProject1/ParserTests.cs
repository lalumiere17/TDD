using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TddDemo;

namespace TddDemoTests
{
    public class ParserTests
    {
        [SetUp]
        public void Setup()
        {
            publisher = new Mock<IAlertPublisher>();
            parser = new Parser(publisher.Object);
        }

        public Mock<IAlertPublisher> publisher { get; set; }
        public Parser parser { get; set; }

        [Test]
        public void Return2ParsedRowsCount()
        {
            var count = parser.ExcelParse(new ExcelFile(new List<Row>
            {
                CreateValidExcelRow(),
                CreateValidExcelRow()
            }));

            Assert.AreEqual(2, count);
        }
        

        [Test]
        public void Return3ParsedRowsCount()
        {
            var count = parser.ExcelParse(new ExcelFile(new List<Row>
                {
                    CreateValidExcelRow(),
                    CreateValidExcelRow(),
                    CreateValidExcelRow()
                })
            );

            Assert.AreEqual(3, count);
        }



        [Test]
        public void Return2_IfOneRowIsInvalidFor3RowsFile()
        {
            var count = parser.ExcelParse(new ExcelFile(new List<Row>
                {
                    CreateValidExcelRow(),
                    CreateValidExcelRow(),
                    new Row(new List<Cell>
                    {
                        new Cell()
                    })
                })
            );

            Assert.AreEqual(2, count);
        }

        [Test]
        public void PublishAlert_IfOneRowIsInvalid()
        {

            var count = new Parser(publisher.Object).ExcelParse(new ExcelFile(new List<Row>
                {
                    CreateValidExcelRow(),
                    new Row(new List<Cell>
                    {
                        new Cell()
                    })
                })
            );

            publisher.Verify(x=>x.PublishAlert(), Times.Once);
        }

        [Test]
        public void DoNotPublishAlert_IfAllRowsAreValid()
        {

            var count = new Parser(publisher.Object).ExcelParse(new ExcelFile(new List<Row>
                {
                    CreateValidExcelRow(),
                    CreateValidExcelRow()
                })
            );

            publisher.Verify(x=>x.PublishAlert(), Times.Never);
        }

        //Если все строки файла распарсились, он помечается
        //обработанным и сохраняется в хранилище

        [Test]
        public void MakeAsProcessed_IfAllRowsWereParsed()
        {
            var file = new ExcelFile(new List<Row>
                {
                    CreateValidExcelRow(),
                    CreateValidExcelRow(),
                    CreateValidExcelRow()
                });
            var count = parser.ExcelParse(file);

            Assert.AreEqual(true, file.IsProcessed);
        }

        [Test]
        public void Save_IfAllRowsWereParsed()
        {
            var file = new ExcelFile(new List<Row>
                {
                    CreateValidExcelRow(),
                    CreateValidExcelRow(),
                    CreateValidExcelRow()
                });
            var count = parser.ExcelParse(file);

            Assert.AreEqual(true, file.IsSaved);
        }

        //Пользователь загружает csv-файл, в котором на один столбец
        //больше, чем в xlsx


        [Test]
        public void IfCsvFile_CorrectResult()
        {
            var file = new CsvFile(new List<Row>
            {
                CreateValidCsvRow(),
                CreateValidCsvRow(),
                CreateValidCsvRow()
            });
            var count = parser.CsvParse(file);
            Assert.AreEqual(3, count);
        }
        
        private static Row CreateValidExcelRow()
        {
            return new Row(new List<Cell>
            {
                new Cell(),
                new Cell()
            });
        }
        private static Row CreateValidCsvRow()
        {
            return new Row(new List<Cell>
            {
                new Cell(),
                new Cell(),
                new Cell()
            });
        }
    }
}