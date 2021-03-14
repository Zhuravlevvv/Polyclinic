using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            ViewBag.Cost = cost.Read(null);

            return View();
        }

        public IActionResult Inspection()
        {
            var inspection = inspections.Read(new InspectionsBindingModel { UserId= (int)Program.User.Id});
            Dictionary<int, decimal> cena = new Dictionary<int, decimal>();
            foreach (var i in inspection)
            {
               cena.Add((int)i.Id,
                   inspections.Read(new CostInspectionsBindingModel { InspectionId = (int)i.Id }).ToList().Sum(x => x.Cena));
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
            var diagnosis = cost.Read(new CostBindingModel { Name = _name });
            if (diagnosis.Count > 0)
            {
                TempData["ErrorLack"] = "Такая затрата уже есть в базе данных";
                return RedirectToAction("Cost");
            }
            cost.CreateOrUpdate(new CostBindingModel
            {

                Name = _name
            });
            return RedirectToAction("Cost");
        }

        public IActionResult AddInspection()
        {
            if (TempData["ErrorLack"] != null)
            {
                ModelState.AddModelError("", TempData["ErrorLack"].ToString());
            }
            ViewBag.Cost = cost.Read(null).OrderBy(u => u.Name);
            return View();
        }
        [HttpPost]
        public ActionResult AddInspection( InspectionModel model)
        {

            if (model.Name == null)
            {
                ModelState.AddModelError("", "Вы не ввели название");
                ViewBag.Cost = cost.Read(null).OrderBy(u => u.Name);
                return View("AddInspection", model);
            }
            var inspection = inspections.Read(new InspectionsBindingModel {Name=model.Name, UserId= (int)Program.User.Id });
            if (inspection.Count!=0)
            {
                ModelState.AddModelError("", "У вас уже есть  обследование с таким название");
                ViewBag.Cost = cost.Read(null).OrderBy(u => u.Name);
                return View("AddInspection", model);
            }
            inspections.CreateOrUpdate(new InspectionsBindingModel { 
            Name=model.Name,
            UserId= (int)Program.User.Id

            });
            var id = inspections.Read(new InspectionsBindingModel { Name = model.Name, UserId = (int)Program.User.Id })[0].Id;
            foreach (var i in model.Cena)
            {
                inspections.CreateOrUpdate(new CostInspectionsBindingModel
                {
                    Cena = i.Value,
                    InspectionId = (int)id,
                    CostId = i.Key

                });
            }
            var inspection1 = inspections.Read(new InspectionsBindingModel { UserId = (int)Program.User.Id });
            Dictionary<int, decimal> cena = new Dictionary<int, decimal>();
            foreach (var i in inspection1)
            {
                cena.Add((int)i.Id,
                    inspections.Read(new CostInspectionsBindingModel { InspectionId = (int)i.Id }).ToList().Sum(x => x.Cena));
            }
            ViewBag.Cena = cena;
            ViewBag.In = inspection1;
            return RedirectToAction("Inspection");
        }
        public IActionResult InspectionCost(int id)
        {
           var ss= inspections.Read(new InspectionsBindingModel { Id = id })[0].Name;
            var       s2= cost.Read(null); var s3= inspections.Read(new CostInspectionsBindingModel { InspectionId = id });
            ViewBag.Name = inspections.Read(new InspectionsBindingModel {Id=id })[0].Name;
            ViewBag.Cost = cost.Read(null);
            ViewBag.In = inspections.Read(new CostInspectionsBindingModel {InspectionId=id });
            return View();
        }
        



    }
}
