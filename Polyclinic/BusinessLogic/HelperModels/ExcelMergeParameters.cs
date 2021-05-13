using DocumentFormat.OpenXml.Spreadsheet;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;

namespace BusinessLogic.HelperModels
{
    public class ExcelMergeParameters
    {
      public Worksheet Worksheet { get; set; }
        public string CellFromName { get; set; }
        public string CellToName { get; set; }
        public string Merge => $"{CellFromName}:{CellToName}";
    }
}
