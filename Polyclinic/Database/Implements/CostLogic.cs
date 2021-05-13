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
        private Cost CreateModel(CostBindingModel model, Cost cost, Database context)
        {
            cost.Name = model.Name;
            return cost;
        }
        public void Update(CostBindingModel model)
        {
            using (var context = new Database())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var order = context.Costs.FirstOrDefault(rec => rec.Id == model.Id);
                        if (order == null)
                        {
                            throw new Exception("Заказ не найдена");
                        }
                        CreateModel(model, order, context);
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
        public void Insert(CostBindingModel model)
        {
            using (var context = new Database())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Cost cost = new Cost();
                        context.Costs.Add(cost);
                        CreateModel(model, cost, context);
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
        public CostViewModels GetElement(CostBindingModel model)//не используется
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new Database())
            {
                var component = context.Costs
                .FirstOrDefault(rec => rec.Name == model.Name ||
               rec.Id == model.Id);
                return component != null ?
                new CostViewModels
                {
                    Id = component.Id,
                    Name = component.Name
                } :
               null;
            }
        }
        public List<CostViewModels> GetFullList()
        {
            using (var context = new Database())
            {
                return context.Costs
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
