using BusinessLogic.BindingModel;
using BusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface ICost
    {
        List<CostViewModels> Read(CostBindingModel model);
        void CreateOrUpdate(CostBindingModel model);
    }
}
