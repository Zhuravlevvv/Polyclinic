using BusinessLogic.BindingModel;
using BusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface IUsers
    {
        UsersViewModels GetElement(UsersBindingModel model);
        void Insert(UsersBindingModel model);
        void Update(UsersBindingModel model);
        void Delete(UsersBindingModel model);
    }
}
