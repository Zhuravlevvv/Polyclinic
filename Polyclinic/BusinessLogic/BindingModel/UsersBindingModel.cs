using BusinessLogic.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace BusinessLogic.BindingModel
{
    [DataContract]
    public class UsersBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        [DisplayName("ФИО:")]
        public string FIO { get; set; }
        [DataMember]
        [DisplayName("Почта:")]
        public string Email { get; set; }
        [DataMember]
        [DisplayName("Пароль:")]
        public string Password { get; set; }
        [DataMember]
        [DisplayName("Номер телефона:")]
        public string PhoneNumber { get; set; }
        [DataMember]
        [DisplayName("Статус:")]
        public StatusUser Status { get; set; }
    }
}
