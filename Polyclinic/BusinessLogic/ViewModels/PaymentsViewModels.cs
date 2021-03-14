using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.ViewModels
{
    public class PaymentsViewModels
    {
        public int? Id { get; set; }
        public int ClientId { get; set; }
        public int VisitId { get; set; }
        public decimal SumPaument { get; set; }
        public DateTime DatePayment { get; set; }
    }
}
