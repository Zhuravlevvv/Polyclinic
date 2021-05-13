using BusinessLogic.BindingModel;
using BusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface IInspections
    {
        void DeleteCost(CostInspectionsBindingModel model);
        List<InspectionsViewModels> GetFullList();
        InspectionsViewModels GetElement(InspectionsBindingModel model);
        CostInspectionsViewModels GetElement(CostInspectionsBindingModel model);
        void Insert(InspectionsBindingModel model);
        void Update(InspectionsBindingModel model);
        void Delete(InspectionsBindingModel model);
        List<InspectionsViewModels> GetFilteredList(InspectionsBindingModel model);
    }
}
