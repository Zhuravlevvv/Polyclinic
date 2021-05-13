using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.ViewModels
{
    public class InspectionsViewModels
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public Dictionary<int, decimal> costInspections { get; set; }// costid, cena
    }
}
