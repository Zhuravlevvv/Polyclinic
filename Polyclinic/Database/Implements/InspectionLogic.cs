using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
using BusinessLogic.ViewModels;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Implements
{
    public class InspectionLogic : IInspections
    {
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


        public List<InspectionsViewModels> GetFullList()
        {
            using (var context = new Database())
            {
                return context.Inspections
                .Include(rec => rec.CostInspection)
               .ThenInclude(rec => rec.Cost)
               .ToList()
               .Select(rec => new InspectionsViewModels
               {
                   Id = rec.Id,
                   Name = rec.Name,
                   UserId = rec.UserId,
                   costInspections = rec.CostInspection
                .ToDictionary(recPC => (int)recPC.CostId, recPC =>
               (recPC.Cena))
               })
               .ToList();
            }
        }
        public List<InspectionsViewModels> GetFilteredList(InspectionsBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new Database())
            {
                return context.Inspections
                .Include(costi => costi.CostInspection)
                .ThenInclude(cost => cost.Cost)
               .Where(ins => ins.UserId == model.UserId && 
               (model.Selected == null || model.Selected.Contains(ins.Id))
               )
               .ToList()
               .Select(rec => new InspectionsViewModels
               {
                   Id = rec.Id,
                   Name = rec.Name,
                   UserId = rec.UserId,
                   costInspections = rec.CostInspection
                 .ToDictionary(recPC => (int)recPC.CostId, recPC =>
               (recPC.Cena))
               })
               .ToList();
            }
        }
        public InspectionsViewModels GetElement(InspectionsBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new Database())
            {
                var product = context.Inspections
                .Include(rec => rec.CostInspection)
               .ThenInclude(rec => rec.Cost)
               .FirstOrDefault(rec => rec.Id == model.Id ||
                (rec.Name.Contains(model.Name) && rec.UserId == model.UserId)
               );
                return product != null ?
                new InspectionsViewModels
                {
                    Id = product.Id,
                    Name = product.Name,
                    UserId = product.UserId,
                    costInspections = product.CostInspection
                 .ToDictionary(recPC => (int)recPC.CostId, recPC =>
               (recPC.Cena))
                } :
               null;
            }
        }
        public CostInspectionsViewModels GetElement(CostInspectionsBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new Database())
            {
                var product = context.CostInspections
               .FirstOrDefault(rec => rec.Id == model.Id || (rec.CostId == model.CostId && rec.InspectionId == model.InspectionId));
                return product != null ?
                new CostInspectionsViewModels
                {
                    Id = product.Id,
                    Cena = product.Cena,
                    CostId = product.CostId,
                    InspectionId = product.InspectionId
                } :
               null;
            }
        }
        public void Insert(InspectionsBindingModel model)
        {
            using (var context = new Database())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Inspection ins = new Inspection();
                        context.Inspections.Add(ins);
                        CreateModel(model, ins, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Update(InspectionsBindingModel model)
        {
            using (var context = new Database())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Inspections.FirstOrDefault(rec => rec.Id ==
                       model.Id);
                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
                        CreateModel(model, element, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Delete(InspectionsBindingModel model)
        {
            using (var context = new Database())
            {
                Inspection element = context.Inspections.FirstOrDefault(rec => rec.Id ==
               model.Id);
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
        private Inspection CreateModel(InspectionsBindingModel model, Inspection product,
       Database context)
        {
            product.Name = model.Name;
            product.UserId = model.UserId;
            if (model.Id.HasValue)
            {
                var productComponents = context.CostInspections.Where(rec =>
               rec.InspectionId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.CostInspections.RemoveRange(productComponents.Where(rec =>
               !model.costInspections.ContainsKey(rec.CostId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateComponent in productComponents)
                {
                    updateComponent.Cena =
                   model.costInspections[updateComponent.CostId] + updateComponent.Cena;
                    model.costInspections.Remove(updateComponent.CostId);
                }

            }
            context.SaveChanges();
            // добавили новые
            foreach (var pc in model.costInspections)
            {
                if (pc.Value > 0)
                {
                    context.CostInspections.Add(new CostInspection
                    {
                        InspectionId = (int)product.Id,
                        CostId = pc.Key,
                        Cena = pc.Value
                    });
                    context.SaveChanges();
                }
            }

            return product;
        }
    }
}       

