using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class InspectionModel
    {
        public string Name { get; set; }
        public Dictionary<int, decimal> Cena { get; set; }
    }
}
