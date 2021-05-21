using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.ViewModels
{
    public class ReportDetailViewModel
    {
        public string Name { get; set; }
        public List<Tuple<string, decimal>> Details { get; set; }
    }
}
