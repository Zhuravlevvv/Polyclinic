using BusinessLogic.BindingModel;
using BusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface ICost
    {
        List<CostViewModels> GetFullList();
        CostViewModels GetElement(CostBindingModel model);
        void Insert(CostBindingModel model);
        void Update(CostBindingModel model);
        void Delete(CostBindingModel model);
    }
}
