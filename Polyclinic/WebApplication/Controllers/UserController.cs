using BusinessLogic.BindingModel;
using Database.Implements;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using BusinessLogic.Interfaces;
using BusinessLogic.Enum;

namespace WebApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly IUsers user;
        public UserController(IUsers user)
        {
            this.user = user;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel client, string Status)
        {
            var clientView = user.GetElement(new UsersBindingModel
            {
                FIO = client.FIO,
                Password = client.Password,
                Status = StatusUser.Клиент
            });
            if (Status == "Сотрудник")
            {
                clientView = user.GetElement(new UsersBindingModel
                {
                    FIO = client.FIO,
                    Password = client.Password,
                    Status = BusinessLogic.Enum.StatusUser.Сотрудник
                });
            }
            if (clientView == null)
            {

                ModelState.AddModelError("", "Вы ввели неверный пароль, либо пользователь не найден");
                return View(client);
            }
            Program.User = clientView;

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Registration(RegistrationModel client, string Status)
        {
            if (String.IsNullOrEmpty(client.FIO))
            {
                ModelState.AddModelError("", "Введите логин");
                return View(client);
            }
            if (client.FIO.Length > 50 ||
           client.FIO.Length < 3)
            {
                ModelState.AddModelError("", $"Длина логина должна быть от {3} до {50} символов");
                return View(client);
            }
            var status = StatusUser.Клиент;
            if (Status == "Сотрудник")
                status = StatusUser.Сотрудник;
            var existClient = user.GetElement(new UsersBindingModel
            {
                FIO = client.FIO,
                Status = status
            });
            if (existClient != null)
            {
                ModelState.AddModelError("", "Данный логин уже занят");
                return View(client);
            }
            if (String.IsNullOrEmpty(client.Email))
            {
                ModelState.AddModelError("", "Введите электронную почту");
                return View(client);
            }
            existClient = user.GetElement(new UsersBindingModel
            {
                Email = client.Email
            });
            if (existClient != null)
            {
                ModelState.AddModelError("", "Данный E-Mail уже занят");
                return View(client);
            }
            if (!Regex.IsMatch(client.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                ModelState.AddModelError("", "Email введен некорректно");
                return View(client);
            }
            if (client.Password.Length > 6 ||
            client.Password.Length < 10)
            {
                ModelState.AddModelError("", $"Длина пароля должна быть от {6} до {10} символов");
                return View(client);
            }

            if (String.IsNullOrEmpty(client.Password))
            {
                ModelState.AddModelError("", "Введите пароль");
                return View(client);
            }
            if (String.IsNullOrEmpty(client.PhoneNumber))
            {
                ModelState.AddModelError("", "Введите номер телефона");
                return View(client);
            }

            user.Insert(new UsersBindingModel
            {
                FIO = client.FIO,
                Status = status,
                Password = client.Password,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber
            });
            ModelState.AddModelError("", "Вы успешно зарегистрированы");
            return View("Registration", client);
        }
    }
}
