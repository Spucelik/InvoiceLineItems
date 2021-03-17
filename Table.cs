using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceLines
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Style
    {
        public string name { get; set; }
        public double confidence { get; set; }
    }

    public class Appearance
    {
        public Style style { get; set; }
    }

    public class Word
    {
        public List<int> boundingBox { get; set; }
        public string text { get; set; }
        public double confidence { get; set; }
    }

    public class Line
    {
        public List<int> boundingBox { get; set; }
        public string text { get; set; }
        public Appearance appearance { get; set; }
        public List<Word> words { get; set; }
    }

    public class ReadResult
    {
        public int page { get; set; }
        public double angle { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string unit { get; set; }
        public List<Line> lines { get; set; }
    }

    public class Cell
    {
        public int rowIndex { get; set; }
        public int columnIndex { get; set; }
        public string text { get; set; }
        public List<int> boundingBox { get; set; }
        public List<string> elements { get; set; }
    }

    public class Table
    {
        public int rows { get; set; }
        public int columns { get; set; }
        public List<Cell> cells { get; set; }
        public List<int> boundingBox { get; set; }
    }

    public class PageResult
    {
        public int page { get; set; }
        public List<Table> tables { get; set; }
    }

    public class AnalyzeResult
    {
        public string version { get; set; }
        public List<ReadResult> readResults { get; set; }
        public List<PageResult> pageResults { get; set; }
    }

    public class Root
    {
        public string status { get; set; }
        public DateTime createdDateTime { get; set; }
        public DateTime lastUpdatedDateTime { get; set; }
        public AnalyzeResult analyzeResult { get; set; }
    }

    public class JSONOutput
    {
        public string Quantity { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public string UnitPrice { get; set; }   
        public string Discount { get; set; }
        public string Total { get; set; }  
    }

    public class JSONOutputHelper
    {
        public int UnitColumnIndex { get; set; }
        public int QuantityColumnIndex { get; set; }
        public int DescriptionColumnIndex { get; set; }

    }
}
