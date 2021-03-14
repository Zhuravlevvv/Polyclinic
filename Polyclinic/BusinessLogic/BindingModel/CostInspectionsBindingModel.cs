using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.BindingModel
{
    public class CostInspectionsBindingModel
    {
        public int? Id { get; set; }
        public int InspectionId { get; set; }
        public int CostId { get; set; }
        public decimal Cena { get; set; }
    }
}
