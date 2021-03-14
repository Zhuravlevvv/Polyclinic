using BusinessLogic.BindingModel;
using BusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
        public interface IInspections
        {
            List<InspectionsViewModels> Read(InspectionsBindingModel model);
            List<CostInspectionsViewModels> Read(CostInspectionsBindingModel model);
            void CreateOrUpdate(InspectionsBindingModel model);
        void CreateOrUpdate(CostInspectionsBindingModel model);
        void Delete(InspectionsBindingModel model);
        }
}
