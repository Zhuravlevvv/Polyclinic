using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Models
{
    public  class InspectionDoctor
    {
        public int? Id { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public int VisitId { get; set; }
        [Required]
        public int InspectionId { get; set; }
        [Required]
        public Visit Visit { get; set; }
        public Inspection Inspection { get; set; }
    }
}
