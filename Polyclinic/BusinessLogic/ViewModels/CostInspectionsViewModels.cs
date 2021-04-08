using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.ViewModels
{
    public class CostInspectionsViewModels
    {
        //стоимость обследования
        public int? Id { get; set; }
        public int InspectionId { get; set; }
        public int CostId { get; set; }
        public decimal Cena { get; set; }
    }
}
