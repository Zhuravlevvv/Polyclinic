using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models
{
  public  class Inspection
    {
        public int? Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }

  
        public User User { get; set; }
        [ForeignKey("InspectionId")]
        public virtual List<CostInspection> CostInspection { get; set; }

        [ForeignKey("InspectionId")]
        public virtual List<InspectionDoctor> InspectionDoctor { get; set; }

    }
}
