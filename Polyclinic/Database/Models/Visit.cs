using BusinessLogic.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models
{
   public class Visit
    {
        public int? Id { get; set; }
        public int ClientId { get; set; }
        [Required]
        public decimal Summ { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public StatusVisit Status { get; set; }
        [ForeignKey("VisitId")]
        public virtual List<InspectionDoctor> InspectionDoctor { get; set; }
        [ForeignKey("VisitId")]
        public virtual List<Payment> Payment { get; set; }
        public User User { get; set; }
    }
}
