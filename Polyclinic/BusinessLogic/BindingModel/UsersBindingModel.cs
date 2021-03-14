using BusinessLogic.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.BindingModel
{
    public class UsersBindingModel
    {
        public int? Id { get; set; }
        public string FIO { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public StatusUser Status { get; set; }
    }
}
