using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.BindingModel
{
    public class InspectionsBindingModel
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public List<int?> Selected { get; set; }
        public Dictionary<int, decimal> costInspections { get; set; }// costid, cena
    }
}
