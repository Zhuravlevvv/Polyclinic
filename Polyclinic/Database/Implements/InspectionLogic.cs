using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
using BusinessLogic.ViewModels;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Implements
{
    public class InspectionLogic : IInspections
    {
        public void CreateOrUpdate(InspectionsBindingModel model)
        {
            using (var context = new Database())
            {
                Inspection element = model.Id.HasValue ? null : new Inspection();
                if (model.Id.HasValue)
                {
                    element = context.Inspections.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Inspection();
                    context.Inspections.Add(element);
                }
                element.Name = model.Name;
                element.UserId = model.UserId;
                context.SaveChanges();
            }
        }

        public void CreateOrUpdate(CostInspectionsBindingModel model)
        {
            using (var context = new Database())
            {
                CostInspection element = model.Id.HasValue ? null : new CostInspection();
                if (model.CostId != 0 && model.InspectionId != 0)
                {
                    element = context.CostInspections.FirstOrDefault(rec => rec.InspectionId == model.InspectionId && rec.CostId == model.CostId);
                    if (element == null)
                    {
                        element = new CostInspection();
                        context.CostInspections.Add(element);
                    }
                }
                else
                {
                    element = new CostInspection();
                    context.CostInspections.Add(element);
                }
                element.Cena = model.Cena;
                element.CostId = model.CostId;
                element.InspectionId = model.InspectionId;

                context.SaveChanges();
            }
        }

        public void Delete(InspectionsBindingModel model)
        {
            using (var context = new Database())
            {
                Inspection element = context.Inspections.FirstOrDefault(rec => rec.Id == model.Id);

                if (element != null)
                {
                    context.Inspections.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public void DeleteCost(CostInspectionsBindingModel model)
        {
            using (var context = new Database())
            {
                CostInspection element = context.CostInspections.FirstOrDefault(rec => rec.Id == model.Id);

                if (element != null)
                {
                    context.CostInspections.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public List<InspectionsViewModels> Read(InspectionsBindingModel model)
        {
            using (var context = new Database())
            {
                return context.Inspections
                 .Where(rec => model == null
                   || rec.Id == model.Id
                   || ((rec.Name == model.Name || model.Name == null) && rec.UserId == model.UserId))
               .Select(rec => new InspectionsViewModels
               {
                   Id = rec.Id,
                   Name = rec.Name,
                   UserId = rec.UserId
               })
                .ToList();
            }
        }

        public List<CostInspectionsViewModels> ReadCI(CostInspectionsBindingModel model)
        {
            using (var context = new Database())
            {
                return context.CostInspections
                 .Where(rec => model == null
                   || rec.Id == model.Id
                   || rec.InspectionId == model.InspectionId)
               .Select(rec => new CostInspectionsViewModels
               {
                   Id = rec.Id,
                   Cena = rec.Cena,
                   InspectionId = rec.InspectionId,
                   CostId = rec.CostId
               })
                .ToList();
            }
        }
    }
}       

