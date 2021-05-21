using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Models
{
    public class Payment
    {
        public int? Id { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public int VisitId { get; set; }
        [Required]
        public decimal SumPaument { get; set; }
        [Required]
        public DateTime DatePayment { get; set; }
        public User User { get; set; }
        public Visit Visit { get; set; }
    }
}
