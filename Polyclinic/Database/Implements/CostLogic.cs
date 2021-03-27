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
    public class CostLogic : ICost
    {
        public void CreateOrUpdate(CostBindingModel model)
        {
            using (var context = new Database())
            {
                Cost element = model.Id.HasValue ? null : new Cost();
                if (model.Id.HasValue)
                {
                    element = context.Costs.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Cost();
                    context.Costs.Add(element);
                }
                element.Name = model.Name;
                context.SaveChanges();
            }
        }

        public void Delete(CostBindingModel model)
        {
            using (var context = new Database())
            {
                Cost element = context.Costs.FirstOrDefault(rec => rec.Id == model.Id);

                if (element != null)
                {
                    context.Costs.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public List<CostViewModels> Read(CostBindingModel model)
        {
            using (var context = new Database())
            {
                return context.Costs
                 .Where(rec => model == null
                   || rec.Id == model.Id
                   || rec.Name == model.Name)
               .Select(rec => new CostViewModels
               {
                   Id = rec.Id,
                   Name = rec.Name
               })
                .ToList();
            }
        }
    }
}
