using BusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.HelperModels
{
    public class PdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<CostViewModels> Costs { get; set; }
        public List<CostInspectionsViewModels> CostInspections { get; set; }
        public List<InspectionsViewModels> Inspections { get; set; }
    }
}
