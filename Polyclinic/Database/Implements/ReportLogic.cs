using BusinessLogic.HelperModels;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Implements
{
    public class ReportLogic
    {
        public static void CreateDoc(PdfInfo info)
        {
            Document document = new Document();
            DefineStyles(document);
            Section section = document.AddSection();
            Paragraph paragraph = section.AddParagraph(info.Title);
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Style = "NormalTitle";
            var doctorTable = document.LastSection.AddTable();
            List<string> headerWidths = new List<string> { "4cm", "4cm", "4cm" };
            foreach (var elem in headerWidths)
            {
                doctorTable.AddColumn(elem);
            }
            if (info.Inspections != null)
            {
                CreateRow(new PdfRowParameters
                {
                    Table = doctorTable,
                    Texts = new List<string> { "Обследование", "Затраты", "Цена" },
                    Style = "NormalTitle",
                    ParagraphAlignment = ParagraphAlignment.Center
                });
                foreach (var inspection in info.Inspections)
                {

                    CreateRow(new PdfRowParameters
                    {
                        Table = doctorTable,
                        Texts = new List<string> { inspection.Name, "", "" },
                        Style = "NormalTitle",
                        ParagraphAlignment = ParagraphAlignment.Center
                    });

                    foreach (var ci in info.CostInspections.Where(rec => rec.InspectionId == inspection.Id))
                    {
                        if (ci.Cena > 0)
                        {
                            var cost = info.Costs.Where(rec => rec.Id == ci.CostId).FirstOrDefault();

                            CreateRow(new PdfRowParameters
                            {
                                Table = doctorTable,
                                Texts = new List<string> { "", cost.Name, ci.Cena.ToString() },
                                Style = "Normal",
                                ParagraphAlignment = ParagraphAlignment.Left
                            });
                        }
                    }
                    if (info.CostInspections.Where(rec => rec.InspectionId == inspection.Id).Sum(x => x.Cena) > 0)
                        CreateRow(new PdfRowParameters
                        {
                            Table = doctorTable,
                            Texts = new List<string> { "", "Итого:", info.CostInspections.Where(rec => rec.InspectionId == inspection.Id).Sum(x => x.Cena).ToString() },
                            Style = "Normal",
                            ParagraphAlignment = ParagraphAlignment.Left
                        });

                }
            }
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            renderer.RenderDocument();
            renderer.PdfDocument.Save(info.FileName);
        }
        private static void DefineStyles(Document document)
        {
            Style style = document.Styles["Normal"];
            style.Font.Name = "Times New Roman";
            style.Font.Size = 14;
            style = document.Styles.AddStyle("NormalTitle", "Normal");
            style.Font.Bold = true;
        }
        private static void CreateRow(PdfRowParameters rowParameters)
        {
            Row row = rowParameters.Table.AddRow();
            for (int i = 0; i < rowParameters.Texts.Count; ++i)
            {
                FillCell(new PdfCellParameters
                {
                    Cell = row.Cells[i],
                    Text = rowParameters.Texts[i],
                    Style = rowParameters.Style,
                    BorderWidth = 0.5,
                    ParagraphAlignment = rowParameters.ParagraphAlignment
                });
            }
        }
        private static void FillCell(PdfCellParameters cellParameters)
        {
            cellParameters.Cell.AddParagraph(cellParameters.Text);
            if (!string.IsNullOrEmpty(cellParameters.Style))
            {
                cellParameters.Cell.Style = cellParameters.Style;
            }
            cellParameters.Cell.Borders.Left.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Right.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Top.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Bottom.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Format.Alignment = cellParameters.ParagraphAlignment;
            cellParameters.Cell.VerticalAlignment = VerticalAlignment.Center;
        }
    }
}
