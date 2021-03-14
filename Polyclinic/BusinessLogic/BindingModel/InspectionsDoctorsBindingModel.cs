using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.BindingModel
{
    public class InspectionsDoctorsBindingModel
    {
        public int? Id { get; set; }
        public int Count { get; set; }
        public int VisitId { get; set; }
        public int InspectionId { get; set; }
    }
}
