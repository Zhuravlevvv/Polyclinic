using BusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.BindingModel
{
  public class ReportBindingModel
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<int> Selected { get; set; }
        public UsersViewModels User { get; set; }
    }
}
