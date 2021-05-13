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
    public class UserLogic : IUsers
    {
        private User CreateModel(UsersBindingModel model, User user, Database context)
        {
            user.Email = model.Email;
            user.FIO = model.FIO;
            user.PhoneNumber = model.PhoneNumber;
            user.Status = model.Status;
            user.Password = model.Password;
            return user;
        }

        public void Insert(UsersBindingModel model)
        {
            using (var context = new Database())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        User cost = new User();
                        context.Users.Add(cost);
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
        public UsersViewModels GetElement(UsersBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new Database())
            {
                var component = context.Users
                .FirstOrDefault(rec => model == null
                   || rec.Id == model.Id
                   || (rec.FIO == model.FIO && rec.Password == model.Password && rec.Status == model.Status));
                return component != null ?
                new UsersViewModels
                {
                    Id = component.Id,
                    FIO = component.FIO,
                    Status = component.Status,
                    Email = component.Email,
                    Password = component.Password,
                    PhoneNumber = component.PhoneNumber
                } :
               null;
            }
        }
    }
}
