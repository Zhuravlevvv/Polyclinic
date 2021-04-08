using BusinessLogic.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.ViewModels
{
    //талон на посещение
    public class VisitViewModels
    {
        public int? Id { get; set; }
        public int ClientId { get; set; }
        public decimal Summ { get; set; }
        public DateTime Date { get; set; }
        public StatusVisit Status { get; set; }
    }
}
