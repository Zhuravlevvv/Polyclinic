using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.ViewModels
{
    public class InspectionsDoctorsViewModels
    {
        public int? Id { get; set; }
        public int Count { get; set; }
        public int VisitId { get; set; }
        public int InspectionId { get; set; }
    }
}
