using BusinessLogic.BindingModel;
using BusinessLogic.HelperModels;
using BusinessLogic.Interfaces;
using BusinessLogic.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Database.Implements
{
    public class ReportLogic
    {

        private readonly IInspections _inspection;
        private readonly ICost _cost;
        public ReportLogic(IInspections inspection, ICost cost)
        {
                  _inspection = inspection;
               _cost = cost;
        }

        public List<ReportDetailViewModel> GetDetail(int Id, ReportBindingModel model)
        {
            var engines = _inspection.GetFilteredList(
                new InspectionsBindingModel { UserId = Id, Selected=model.Selected });
            var list = new List<ReportDetailViewModel>();
            foreach (var engine in engines)
            {
                var record = new ReportDetailViewModel
                {
                    Name = engine.Name,
                    Details = new List<Tuple<string, decimal>>(),
                };

                foreach (var detail in engine.costInspections)
                {
                    record.Details.Add(new Tuple<string, decimal>( _cost.GetElement(new CostBindingModel {Id=detail.Key }).Name, detail.Value));
                }

                list.Add(record);
            }
            return list;
        }

        public void SaveDetailsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new Info
            {
                FileName = model.FileName,
                Title = model.Title,

                InspectionsCost = GetDetail(
                      (int)model.User.Id,
                  model)
            });
        
        }

        // Сохранение компонент с указаеним продуктов в файл-Excel

        public void SaveDetailToExcelFile(ReportBindingModel model)
        {
            SaveToExel.CreateDoc(new Info
            {
                FileName = model.FileName,
                Title = model.Title,
                InspectionsCost = GetDetail((int)model.User.Id, model)
            });
        }

        // Сохранение заказов в файл-Pdf

        public void SaveToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new Info
            {
                FileName = model.FileName,
                Title = model.Title,
                InspectionsCost = GetDetail((int)model.User.Id, model)
            });
            Mail(model);
        }
        public void Mail(ReportBindingModel model)
        {
            MailAddress from = new MailAddress("zrider1121@gmail.com", "Отчет!");
            MailAddress to = new MailAddress(model.User.Email);
            MailMessage m = new MailMessage(from, to);
            m.Subject =model.Title;
            m.Attachments.Add(new Attachment(model.FileName));
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("zrider1121@gmail.com", "Zhura1337227");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }

}
