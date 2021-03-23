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
        public void CreateOrUpdate(UsersBindingModel model)
        {
            using (var context = new Database())
            {
                User element = model.Id.HasValue ? null : new User();
                if (model.Id.HasValue)
                {
                    element = context.Users.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new User();
                    context.Users.Add(element);
                }
                element.Email = model.Email;
                element.FIO = model.FIO;
                element.PhoneNumber = model.PhoneNumber;
                element.Status = model.Status;
                element.Password = model.Password;
                context.SaveChanges();
            }
        }



        public List<UsersViewModels> Read(UsersBindingModel model)
        {
            using (var context = new Database())
            {
                return context.Users
                 .Where(rec => model == null
                   || rec.Id == model.Id
                   || (rec.FIO == model.FIO && (rec.Password == model.Password || model.Password == null) && rec.Status == model.Status)

                   || rec.Email == model.Email)
               .Select(rec => new UsersViewModels
               {
                   Id = rec.Id,
                   FIO = rec.FIO,
                   Status = rec.Status,
                   Email = rec.Email,
                   Password = rec.Password,
                   PhoneNumber = rec.PhoneNumber


               })
                .ToList();
            }
        }
    }
}
