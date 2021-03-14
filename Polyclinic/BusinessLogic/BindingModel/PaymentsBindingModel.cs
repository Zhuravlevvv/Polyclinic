using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.BindingModel
{
    public class PaymentsBindingModel
    {
        public int? Id { get; set; }
        public int ClientId { get; set; }
        public int VisitId { get; set; }
        public  decimal SumPaument { get; set; }
        public DateTime DatePayment { get; set; }
    }
}
