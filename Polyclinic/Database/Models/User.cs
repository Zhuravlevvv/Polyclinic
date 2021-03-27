using BusinessLogic.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models
{
    public class User
    {
        public int? Id { get; set; }
        [Required]
        public string FIO { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public StatusUser Status { get; set; }
        [ForeignKey("UserId")]
        public virtual List<Visit> Visit { get; set; }
        [ForeignKey("UserId")]
        public virtual List<Payment> Payment { get; set; }
        [ForeignKey("UserId")]
        public virtual List<Inspection> Inspection { get; set; }

    }
}
