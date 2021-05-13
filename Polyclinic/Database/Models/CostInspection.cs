using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Models
{
    public class CostInspection
    {
        public int? Id { get; set; }
        public int InspectionId { get; set; }
        public int CostId { get; set; }
        [Required]
        public decimal Cena { get; set; }
        public Cost Cost { get; set; }
        public Inspection Inspection { get; set; }
    }
}
