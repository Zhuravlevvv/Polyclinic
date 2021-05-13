using BusinessLogic.BindingModel;
using BusinessLogic.HelperModels;
using BusinessLogic.Interfaces;
using BusinessLogic.ViewModels;
using Database.Implements;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class DoctorController : Controller
    {
        private readonly ICost cost;
        private readonly IInspections inspections;
        public DoctorController(ICost cost, IInspections inspections)
        {
            this.cost = cost;
            this.inspections = inspections;
        }

        public IActionResult Cost()
        {
            if (TempData["ErrorLack"] != null)
            {
                ModelState.AddModelError("", TempData["ErrorLack"].ToString());
            }
            ViewBag.Cost = cost.GetFullList();

            return View();
        }

        public IActionResult Inspection()
        {
            var inspection = inspections.GetFilteredList(new InspectionsBindingModel { UserId = (int)Program.User.Id });
            Dictionary<int, decimal> cena = new Dictionary<int, decimal>();
            foreach (var i in inspection)
            {
                cena.Add((int)i.Id,
                    i.costInspections.Sum(x => x.Value));
            }
            ViewBag.Cena = cena;
            ViewBag.In = inspection;
            return View();
        }

        public ActionResult AddCost(string _name)
        {
            if (_name == null)
            {
                TempData["ErrorLack"] = "Вы не ввели название";
                return RedirectToAction("Cost");
            }
            var diagnosis = cost.GetElement(new CostBindingModel { Name = _name });
            if (diagnosis != null)
            {
                TempData["ErrorLack"] = "Такая затрата уже есть в базе данных";
                return RedirectToAction("Cost");
            }
            cost.Insert(new CostBindingModel
            {
                Name = _name
            });
            return RedirectToAction("Cost");
        }

        public IActionResult AddInspection(int id)
        {
            ViewBag.Id = id;
            var Name = inspections.GetElement(new InspectionsBindingModel { Id = id });
            if (Name != null)
            {
                ViewBag.Name = Name.Name;
            }
            else
            {
                ViewBag.Name = "";
            }
            if (TempData["ErrorLack"] != null)
            {
                ModelState.AddModelError("", TempData["ErrorLack"].ToString());
            }
            ViewBag.Cost = cost.GetFullList();
            return View();
        }

        public ActionResult DeleteCost(int id)
        {
            cost.Delete(new CostBindingModel { Id = id });
            return RedirectToAction("Cost");
        }
        public ActionResult DeleteIC(int id, int id1)
        {
            var Id = inspections.GetElement(new CostInspectionsBindingModel { InspectionId = id1, CostId = id });//считываем элемент затраты по обследованию
            inspections.DeleteCost(new CostInspectionsBindingModel { Id = Id.Id });
            if (inspections.GetElement(new InspectionsBindingModel { Id = id1 }).costInspections.Count != 0)//проверяем остались ли у обследования еще затраты
            {
                return RedirectToAction("InspectionCost", new { id = id1 });
            }
            else
            {
                var inspection = inspections.GetFilteredList(new InspectionsBindingModel { UserId = (int)Program.User.Id });
                Dictionary<int, decimal> cena = new Dictionary<int, decimal>();
                foreach (var i in inspection)
                {
                    cena.Add((int)i.Id,
                        i.costInspections.Sum(x => x.Value));
                }
                ViewBag.Cena = cena;
                ViewBag.In = inspection;
                return RedirectToAction("Inspection");
            }
        }
        public ActionResult Delete(int id)
        {
            inspections.Delete(new InspectionsBindingModel { Id = id });
            return RedirectToAction("Inspection");
        }
        public IActionResult UpdateCost(int id)
        {
            ViewBag.Id = id;
            var Name = cost.GetElement(new CostBindingModel { Id = id });
            ViewBag.Name = Name.Name;
            if (TempData["ErrorLack"] != null)
            {
                ModelState.AddModelError("", TempData["ErrorLack"].ToString());
            }
            return View();
        }
        [HttpPost]
        public ActionResult UpdateCost(InspectionModel model, int id)
        {
            if (model.Name == null || model.Name == "")
            {
                ModelState.AddModelError("", "Вы не ввели название");
                ViewBag.Id = id;
                return View("UpdateCost", model);
            }
            var Cost = cost.GetElement(new CostBindingModel { Name = model.Name });
            if (Cost != null && Cost.Id != id)
            {
                ModelState.AddModelError("", "У вас уже есть Затратa с таким название");
                ViewBag.Id = id;
                return View("UpdateCost", model);
            }
            cost.Update(new CostBindingModel
            {
                Id = id,
                Name = model.Name

            });
            ViewBag.Cost = cost.GetFullList();
            return View("Cost");
        }
        [HttpPost]
        public ActionResult AddInspection(InspectionModel model, int id)
        {
            foreach (var q in model.Cena)//проверяем кооектность данных в полях
            {
                try
                {
                    var b = Convert.ToDecimal(q.Value);
                    if (b < 0)
                    {
                        ModelState.AddModelError("", "Цена это положительное десятичное число");

                        ViewBag.Id = id;
                        ViewBag.Cost = cost.GetFullList().OrderBy(u => u.Name);
                        return View("AddInspection", model);

                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Цена это положительное десятичное число");

                    ViewBag.Id = id;
                    ViewBag.Cost = cost.GetFullList().OrderBy(u => u.Name);
                    return View("AddInspection", model);
                }
            }
            Dictionary<int, decimal> icost = new Dictionary<int, decimal> { };
            if (id == 0)
            {
                if (model.Name == null || model.Name == "")
                {
                    ModelState.AddModelError("", "Вы не ввели название");
                    ViewBag.Cost = cost.GetFullList().OrderBy(u => u.Name);
                    return View("AddInspection", model);
                }


                var inspection = inspections.GetElement(new InspectionsBindingModel { Name = model.Name, UserId = (int)Program.User.Id });

                if (inspection != null)
                {
                    ModelState.AddModelError("", "У вас уже есть  обследование с таким название");
                    ViewBag.Cost = cost.GetFullList().OrderBy(u => u.Name);
                    return View("AddInspection", model);
                }
                foreach (var i in model.Cena)
                {
                    icost.Add(i.Key, Convert.ToDecimal(i.Value));
                }
                inspections.Insert(new InspectionsBindingModel
                {
                    Name = model.Name,
                    UserId = (int)Program.User.Id,
                    costInspections = icost
                });
            }
            else
            {
                foreach (var i in model.Cena)
                {
                    icost.Add(i.Key, Convert.ToDecimal(i.Value));
                }
                inspections.Update(new InspectionsBindingModel
                {
                    Id = id,
                    Name = model.Name,
                    UserId = (int)Program.User.Id,
                    costInspections = icost
                });
            }
            var inspection1 = inspections.GetFilteredList(new InspectionsBindingModel { UserId = (int)Program.User.Id });
            Dictionary<int, decimal> cena = new Dictionary<int, decimal>();
            foreach (var i in inspection1)
            {
                cena.Add((int)i.Id,
                    inspections.GetElement(new InspectionsBindingModel { Id = (int)i.Id }).costInspections.Sum(x => x.Value));
            }
            ViewBag.Cena = cena;
            ViewBag.Cost = cost.GetFullList();
            ViewBag.In = inspections.GetFilteredList(new InspectionsBindingModel { UserId = (int)Program.User.Id });
            return RedirectToAction("Inspection");
        }
        public IActionResult InspectionCost(int id)
        {
            ViewBag.Id = id;
            ViewBag.name = inspections.GetElement(new InspectionsBindingModel { Id = id }).Name;
            ViewBag.Name = inspections.GetElement(new InspectionsBindingModel { Id = id });
            ViewBag.Cost = cost.GetFullList();
            return View();
        }

        public IActionResult SendReport()
        {
            SaveToPdf.CreateDoc(new Info
            {
                FileName = $"C:\\data\\ReportInspection{DateTime.Now.Year}.pdf",
                Title = $"Список обследований и затрат по ним сотрудника {Program.User.FIO}",
                Costs = cost.GetFullList(),
                Inspections = inspections.GetFilteredList(new InspectionsBindingModel { UserId = (int)Program.User.Id })
            });
            SaveToExel.CreateDoc(new Info
            {
                FileName = $"C:\\data\\ReportInspection{DateTime.Now.Year}.xlsx",
                Title = $"Список обследований и затрат по ним сотрудника {Program.User.FIO}",
                Costs = cost.GetFullList(),
                Inspections = inspections.GetFilteredList(new InspectionsBindingModel { UserId = (int)Program.User.Id })
            });
            SaveToWord.CreateDoc(new Info
            {
                FileName = $"C:\\data\\ReportInspection{DateTime.Now.Year}.docx",
                Title = $"Список обследований и затрат по ним сотрудника {Program.User.FIO}",
                Costs = cost.GetFullList(),
                Inspections = inspections.GetFilteredList(new InspectionsBindingModel { UserId = (int)Program.User.Id })
            });
            MailAddress from = new MailAddress("zhuravlev1337.73@yandex.ru", "Отчет!");
            MailAddress to = new MailAddress(Program.User.Email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = $"Список обследований и затрат по ним сотрудника {Program.User.FIO}";
            m.Attachments.Add(new Attachment($"C:\\data\\ReportInspection{DateTime.Now.Year}.pdf"));
            m.Attachments.Add(new Attachment($"C:\\data\\ReportInspection{DateTime.Now.Year}.docx"));
            m.Attachments.Add(new Attachment($"C:\\data\\ReportInspection{DateTime.Now.Year}.xlsx"));
            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
            smtp.Credentials = new NetworkCredential("zhuravlev1337.73@yandex.ru", "zhura1337228");
            smtp.EnableSsl = true;
            smtp.Send(m);
            return RedirectToAction("Inspection");
        }
    }
}
