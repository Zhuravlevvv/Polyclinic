using BusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.HelperModels
{
    public class Info
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportDetailViewModel>InspectionsCost{ get; set; }
    }
}
