﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ClosedXML;
using ClosedXML.Excel;
using System.Drawing;
using iTextSharp.text.html.simpleparser;
using System.Data;




namespace FingerprintsModel
{
    public class Export
    {
        public void Exportpdf(Agencyreport Agencyreport, Stream strPdf, string imagepath)
        {
            try
            {
                Document pdfDoc = new Document(PageSize.A4, 30f, 30f, 5f, 10f);
                PdfWriter.GetInstance(pdfDoc, strPdf);
                pdfDoc.Open();
                Image logo = Image.GetInstance(imagepath);
                pdfDoc.Add(logo);
                Chunk chunk = new Chunk("Section B of the PIR as on " + DateTime.Now, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12.0f, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE));
                Paragraph reportHeadline = new Paragraph(chunk);
                reportHeadline.Alignment = Element.ALIGN_CENTER;
                reportHeadline.SpacingBefore = 12.0f;
                pdfDoc.Add(reportHeadline);
                var fontWhiteBold = FontFactory.GetFont("Tahoma", 12, Font.NORMAL, CMYKColor.BLACK);
                var fontSimple = FontFactory.GetFont("Tahoma", 10, Font.NORMAL);
                var fontBold = FontFactory.GetFont("Tahoma", 12, Font.NORMAL);
                PdfPTable _table = new PdfPTable(3);
                _table.SpacingBefore = 20;
                _table.TotalWidth = 1000f;
                PdfPCell cell1 = new PdfPCell();
                cell1.Border = 0;
                _table.AddCell(cell1);
                PdfPCell cell2 = new PdfPCell(new Phrase("(1) # of Head Start or Early Head Start Staff"));
                cell2.BackgroundColor = CMYKColor.LIGHT_GRAY;
                cell2.FixedHeight = 33f;
                _table.AddCell(cell2);
                PdfPCell cell3 = new PdfPCell(new Phrase("(2) # of Contracted Staff"));
                cell3.BackgroundColor = CMYKColor.LIGHT_GRAY;
                _table.AddCell(cell3);
                cell3.FixedHeight = 33f;
                _table.AddCell("Total no of staff member, regardless of the funding source for their salary or no of hours worked.");
                _table.AddCell(Agencyreport.totalhdstarterlyhdstart);
                _table.AddCell(Agencyreport.totalcontracterhdstarterlyhdstart);
                int countheadstart = 0;
                int countcontractor = 0;
                foreach (var item in Agencyreport.Agencystaffreport)
                {
                    countheadstart = countheadstart + Convert.ToInt32(item.totalAssociatedProgram);
                    countcontractor = countcontractor + Convert.ToInt32(item.Contractor);
                }
                _table.AddCell("a. Of these, the number who are current or former Head Start or Early Head Start parents");
                _table.AddCell(Convert.ToString(countheadstart));
                _table.AddCell(Convert.ToString(countcontractor));
                _table.AddCell("b. Of these, the number who left since last year's PIR was reported.");
                _table.AddCell(Agencyreport.terminationdate);
                _table.AddCell(Agencyreport.Contractortotalterminated);
                _table.AddCell("1. Of these, the number who were replaced.");
                _table.AddCell(Agencyreport.totalreplaced);
                _table.AddCell(Agencyreport.totalreplacedcontrator);
                PdfPCell cell4 = new PdfPCell(new Phrase("Program completing the PIR survey for the first time should report the number of staff who left since the program began."));
                cell4.Colspan = 3;
                cell4.BackgroundColor = CMYKColor.LIGHT_GRAY;
                _table.AddCell(cell4);
                pdfDoc.Add(_table);
                pdfDoc.Close();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
           
        }

        public MemoryStream Exportexcel(Agencyreport Agencyreport)
        {
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                XLWorkbook wb = new XLWorkbook();
                var vs = wb.Worksheets.Add("Section B of the PIR");
                vs.Range("A1:C1").Merge().Value = "Section B of the PIR on " + DateTime.Now;
                vs.Cell(2, 1).Value = "";
                vs.Cell(2, 2).Value = "(1) # of Head Start or Early Head Start Staff";
                vs.Cell(2, 3).Value = "(2) # or Contracted Staff";
                vs.Cell(3, 1).Value = "Total no of staff member, regardless of the funding source for their salary or no of hours worked.";
                vs.Cell(3, 2).Value = Agencyreport.totalhdstarterlyhdstart;
                vs.Cell(3, 3).Value = Agencyreport.totalcontracterhdstarterlyhdstart;
                int row = 4;
                int countheadstart = 0;
                int countcontractor = 0;
                foreach (var item in Agencyreport.Agencystaffreport)
                {
                    countheadstart = countheadstart + Convert.ToInt32(item.totalAssociatedProgram);
                    countcontractor = countcontractor + Convert.ToInt32(item.Contractor);

                }
                vs.Cell(row, 1).Value = "a. Of these,the number who are current or former Head Start or Early Head Start parents";
                vs.Cell(row, 2).Value = Convert.ToString(countheadstart);
                vs.Cell(row, 3).Value = Convert.ToString(countcontractor);
                row++;
                vs.Cell(row, 1).Value = "b. Of these, the number who left since last year's PIR was reported.";
                vs.Cell(row, 2).Value = Agencyreport.terminationdate;
                vs.Cell(row, 3).Value = Agencyreport.Contractortotalterminated;
                row++;
                vs.Cell(row, 1).Value = "1. Of these, the number who were replaced.";
                vs.Cell(row, 2).Value = Agencyreport.totalreplaced;
                vs.Cell(row, 3).Value = Agencyreport.totalreplacedcontrator;
                vs.Range("A7:C7").Merge().Value = "Program completing the PIR survey for the first time should report the number of staff who left since the program began.";
               
                wb.SaveAs(memoryStream);
                //memoryStream.WriteTo(strExcel);
                
            }
            catch (Exception ex)
            {
                clsError.WriteException( ex);
            }
            return memoryStream;
        }

        public void Exportpdf(FPA obj, Stream strPdf, string imagepath)
        {
            try
            {
                Document pdfDoc = new Document(PageSize.A4, 30f, 30f, 5f, 10f);
                PdfWriter.GetInstance(pdfDoc, strPdf);
                pdfDoc.Open();
                Image logo = Image.GetInstance(imagepath);
                pdfDoc.Add(logo);
                Chunk chunk = new Chunk("Family Partnership Agreement ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14.0f, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE, BaseColor.BLUE));
                Paragraph reportHeadline = new Paragraph(chunk);
                reportHeadline.Alignment = Element.ALIGN_CENTER;
                reportHeadline.SpacingBefore = 20.0f; reportHeadline.SpacingAfter = 8.0f;
                pdfDoc.Add(reportHeadline);
                var fontWhiteBold = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, CMYKColor.BLACK);
                var fontSimple = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, Font.NORMAL);
                var fontBold = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL);
                iTextSharp.text.Font f = new iTextSharp.text.Font(Font.FontFamily.TIMES_ROMAN, 10);

                iTextSharp.text.Font f2 = new iTextSharp.text.Font(FontFactory.GetFont(FontFactory.TIMES_BOLD, 10));
                iTextSharp.text.Font f3 = new iTextSharp.text.Font(FontFactory.GetFont(FontFactory.TIMES_BOLD, 12));

                PdfPTable _table = new PdfPTable(3);
                _table.SpacingAfter = 5.0f;
                _table.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell p1 = new PdfPCell(new Phrase(2, ("Goal "), f));
                p1.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p1);
                PdfPCell p2 = new PdfPCell(new Phrase(2, (":"), f)); p2.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p2);
                PdfPCell p3 = new PdfPCell(new Phrase(2, (obj.Goal), f)); p3.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p3);
                PdfPCell p11 = new PdfPCell(new Phrase(2, ("Start Date "), f)); p11.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p11);
                PdfPCell p12 = new PdfPCell(new Phrase(2, (":"), f)); p12.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p12);
                PdfPCell p13 = new PdfPCell(new Phrase(2, (obj.GoalDate), f)); p13.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p13);
                PdfPCell p21 = new PdfPCell(new Phrase(2, ("FEO "), f)); p21.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p21);
                PdfPCell p22 = new PdfPCell(new Phrase(2, (":"), f)); p22.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p22);
                PdfPCell p23 = new PdfPCell(new Phrase(2, (obj.CategoryDesc), f)); p23.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p23);
                PdfPCell p31 = new PdfPCell(new Phrase(2, ("Status "), f)); p31.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p31);
                PdfPCell p32 = new PdfPCell(new Phrase(2, (":"), f)); p32.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p32);
                PdfPCell p33 = new PdfPCell(new Phrase(2, obj.GoalStatus == 0 ? "Open" : obj.GoalStatus == 1 ? "Complete" : obj.GoalStatus == 2 ? "Abandoned" : "Refused to do a FPA", f)); p33.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p33);
                PdfPCell p41 = new PdfPCell(new Phrase(2, ("Completion Date "), f)); p41.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p41);
                PdfPCell p42 = new PdfPCell(new Phrase(2, (":"), f)); p42.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p42);
                PdfPCell p43 = new PdfPCell(new Phrase(2, (obj.CompletionDate), f)); p43.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p43);
                PdfPCell p51 = new PdfPCell(new Phrase(2, ("Assigned To "), f)); p51.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p51);
                PdfPCell p52 = new PdfPCell(new Phrase(2, (":"), f)); p52.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p52);
                string Assignedto = obj.GoalFor == 1 && obj.IsSingleParent ? obj.ParentName1 :
                    obj.GoalFor == 3 ? obj.ParentName1 + ", " + obj.ParentName2 :
                    obj.GoalFor == 1 ? obj.ParentName1 :
                    obj.ParentName2;
                PdfPCell p53 = new PdfPCell(new Phrase(2, Assignedto, f)); p53.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p53);
                Chunk chunk2 = new Chunk("Goal Details ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12.0f, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE));
                Paragraph goal = new Paragraph(chunk2);
                goal.Alignment = Element.ALIGN_CENTER;
                goal.SpacingBefore = 5.0f; goal.SpacingAfter = 5.0f;
                pdfDoc.Add(goal);
                pdfDoc.Add(_table);
                Chunk chunk3 = new Chunk(" Goal Steps ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11.0f, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE));
                Paragraph steps = new Paragraph(chunk3);
                steps.Alignment = Element.ALIGN_CENTER;
                steps.SpacingBefore = 5.0f; steps.SpacingAfter = 5.0f;
                pdfDoc.Add(steps);
                PdfPTable _table2 = new PdfPTable(4);
                _table2.SpacingBefore = 5.0f;
                PdfPCell sp1 = new PdfPCell(new Phrase(2, "Description ", f2));
                _table2.AddCell(sp1);
                PdfPCell sp2 = new PdfPCell(new Phrase(2, "Date of Completion", f2));
                _table2.AddCell(sp2);
                PdfPCell sp3 = new PdfPCell(new Phrase(2, "Status", f2));
                _table2.AddCell(sp3);
                PdfPCell sp4 = new PdfPCell(new Phrase(2, "Reminder for", f2));
                _table2.AddCell(sp4);

                foreach (var item in obj.GoalSteps)
                {
                    PdfPCell sp11 = new PdfPCell(new Phrase(2, item.Description, f));
                    _table2.AddCell(sp11);
                    PdfPCell sp21 = new PdfPCell(new Phrase(2, item.StepsCompletionDate, f));
                    _table2.AddCell(sp21);
                    PdfPCell sp31 = new PdfPCell(new Phrase(2, item.Status == 0 ? "Open" : item.Status == 1 ? "Complete" : "Abandoned", f));
                    _table2.AddCell(sp31);
                    PdfPCell sp41 = new PdfPCell(new Phrase(2, item.Reminderdays.ToString(), f));
                    _table2.AddCell(sp41);

                }

                pdfDoc.Add(_table2);
                if (!string.IsNullOrEmpty(obj.SignatureData))
                {
                    PdfPTable _tableim = new PdfPTable(1);
                    _tableim.SpacingBefore = 20f;
                    _table.DefaultCell.Border = Rectangle.NO_BORDER;
                    string theSource = obj.SignatureData.Replace("data:image/png;base64,", "");
                    var src = theSource;
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(Convert.FromBase64String(src));
                    image.ScaleAbsolute(100f, 20f);
                    iTextSharp.text.pdf.PdfPCell imgCell1 = new iTextSharp.text.pdf.PdfPCell();
                    imgCell1.Border = Rectangle.NO_BORDER;
                    imgCell1.AddElement(new Chunk(image, 0, 0));
                    _tableim.AddCell(imgCell1);
                    PdfPCell cign = new PdfPCell(new Phrase("Signature:"));
                    cign.Border = Rectangle.NO_BORDER;
                    _tableim.AddCell(cign);
                    pdfDoc.Add(_tableim);

                }
                pdfDoc.Close();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                
            }
            
        }

        public void ExportPdf2(FPA obj, Stream PDFData, string imagepath)
        {

            // Document document = new Document(PageSize.LETTER, 50, 50, 80, 50);
            Document pdfDoc = new Document(PageSize.A4, 50, 50, 160, 50);
            PdfWriter PDFWriter = PdfWriter.GetInstance(pdfDoc, PDFData);
            PDFWriter.ViewerPreferences = PdfWriter.PageModeUseOutlines;
            // Our custom Header and Footer is done using Event Handler
            TwoColumnHeaderFooter PageEventHandler = new TwoColumnHeaderFooter();
            PDFWriter.PageEvent = PageEventHandler;
            // Define the page header
            PageEventHandler.Title = "Family Partnership Agreement";

            PageEventHandler.HeaderFont = FontFactory.GetFont(BaseFont.COURIER_BOLD, 14, Font.BOLD);
            Image logo;
            if (!string.IsNullOrEmpty(obj.AgencyLogo))
            {
                string theSource = obj.AgencyLogo.Replace("data:image/png;base64,", "");
                var src = theSource;
                logo = iTextSharp.text.Image.GetInstance(Convert.FromBase64String(src));
            }
            else
            {
                logo = Image.GetInstance(imagepath);
            }
            logo.ScaleAbsolute(120f, 100f);
            PageEventHandler.HeaderLeft = logo;

            //PageEventHandler.HeaderRight = "1";
            pdfDoc.Open();
            try
            {

                AddOutline(PDFWriter, "Group ", pdfDoc.PageSize.Height);
                var fontWhiteBold = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12, Font.NORMAL, CMYKColor.BLACK);
                var fontSimple = FontFactory.GetFont(FontFactory.TIMES_BOLD, 10, Font.NORMAL);
                var fontBold = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12, Font.NORMAL);
                iTextSharp.text.Font f3 = new iTextSharp.text.Font(FontFactory.GetFont(FontFactory.TIMES_BOLD, 11));
                iTextSharp.text.Font f4 = new iTextSharp.text.Font(Font.FontFamily.TIMES_ROMAN, 11);
                iTextSharp.text.Font f2 = new iTextSharp.text.Font(FontFactory.GetFont(FontFactory.TIMES_BOLD, 10));
                iTextSharp.text.Font f = new iTextSharp.text.Font(FontFactory.GetFont(FontFactory.TIMES_BOLD, 12));

                PdfPTable _table = new PdfPTable(3);
                _table.SpacingAfter = 5.0f;
                _table.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell p1 = new PdfPCell(new Phrase(2, ("Goal "), f4));
                p1.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p1);
                PdfPCell p2 = new PdfPCell(new Phrase(2, (":"), f)); p2.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p2);
                PdfPCell p3 = new PdfPCell(new Phrase(2, (obj.Goal), f)); p3.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p3);
                PdfPCell p11 = new PdfPCell(new Phrase(2, ("Start Date "), f4)); p11.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p11);
                PdfPCell p12 = new PdfPCell(new Phrase(2, (":"), f)); p12.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p12);
                PdfPCell p13 = new PdfPCell(new Phrase(2, (obj.GoalDate), f4)); p13.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p13);
                //PdfPCell p21 = new PdfPCell(new Phrase(2, ("FEO "), f)); p21.Border = PdfPCell.NO_BORDER;
                //_table.AddCell(p21);
                //PdfPCell p22 = new PdfPCell(new Phrase(2, (":"), f)); p22.Border = PdfPCell.NO_BORDER;
                //_table.AddCell(p22);
                //PdfPCell p23 = new PdfPCell(new Phrase(2, (obj.CategoryDesc), f)); p23.Border = PdfPCell.NO_BORDER;
                //_table.AddCell(p23);
                PdfPCell p31 = new PdfPCell(new Phrase(2, ("Status "), f4)); p31.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p31);
                PdfPCell p32 = new PdfPCell(new Phrase(2, (":"), f)); p32.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p32);
                PdfPCell p33 = new PdfPCell(new Phrase(2, obj.GoalStatus == 0 ? "Open" : obj.GoalStatus == 1 ? "Complete" : obj.GoalStatus == 2 ? "Abandoned" : "Refused to do a FPA", f4)); p33.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p33);
                PdfPCell p41 = new PdfPCell(new Phrase(2, ("Completion Date "), f4)); p41.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p41);
                PdfPCell p42 = new PdfPCell(new Phrase(2, (":"), f)); p42.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p42);
                PdfPCell p43 = new PdfPCell(new Phrase(2, (obj.CompletionDate), f3)); p43.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p43);
                PdfPCell p51 = new PdfPCell(new Phrase(2, ("Assigned Parent/Guardian "), f4)); p51.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p51);
                PdfPCell p52 = new PdfPCell(new Phrase(2, (":"), f)); p52.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p52);
                string Assignedto = obj.IsSingleParent ? obj.ParentName1 :
                    obj.GoalFor == 3 ? obj.ParentName1 + ", " + obj.ParentName2 :
                    obj.GoalFor == 1 ? obj.ParentName1 :
                    obj.ParentName2;
                PdfPCell p53 = new PdfPCell(new Phrase(2, Assignedto, f4)); p53.Border = PdfPCell.NO_BORDER;
                _table.AddCell(p53);
                Chunk chunk2 = new Chunk("Goal Details ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 13.0f, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE));
                Paragraph goal = new Paragraph(chunk2);
                goal.Alignment = Element.ALIGN_CENTER;
                goal.SpacingBefore = 5.0f; goal.SpacingAfter = 5.0f;
                pdfDoc.Add(goal);
                pdfDoc.Add(_table);
                Chunk chunk3 = new Chunk(" Goal Steps ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12.0f, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE));
                Paragraph steps = new Paragraph(chunk3);
                steps.Alignment = Element.ALIGN_CENTER;
                steps.SpacingBefore = 5.0f; steps.SpacingAfter = 5.0f;
                pdfDoc.Add(steps);
                PdfPTable _table2 = new PdfPTable(4);
                _table2.SpacingBefore = 5.0f;
                PdfPCell sp1 = new PdfPCell(new Phrase(2, "Description ", f3));
                _table2.AddCell(sp1);
                PdfPCell sp2 = new PdfPCell(new Phrase(2, "Date of Completion", f3));
                _table2.AddCell(sp2);
                PdfPCell sp3 = new PdfPCell(new Phrase(2, "Status", f3));
                _table2.AddCell(sp3);
                PdfPCell sp4 = new PdfPCell(new Phrase(2, "Actual Complition Date", f3));
                _table2.AddCell(sp4);

                foreach (var item in obj.GoalSteps)
                {
                    PdfPCell sp11 = new PdfPCell(new Phrase(2, item.Description, f4));
                    _table2.AddCell(sp11);
                    PdfPCell sp21 = new PdfPCell(new Phrase(2, item.StepsCompletionDate, f4));
                    _table2.AddCell(sp21);
                    PdfPCell sp31 = new PdfPCell(new Phrase(2, item.Status == 0 ? "Open" : item.Status == 1 ? "Complete" : "Abandoned", f4));
                    _table2.AddCell(sp31);
                    PdfPCell sp41;
                    if (!string.IsNullOrEmpty(item.ActualCompletionDate))
                    {
                        sp41 = new PdfPCell(new Phrase(2, item.ActualCompletionDate.ToString(), f4));
                    }
                    else
                    {
                        sp41 = new PdfPCell(new Phrase(2, "", f));
                    }
                    _table2.AddCell(sp41);

                }

                pdfDoc.Add(_table2);
                if (!string.IsNullOrEmpty(obj.SignatureData))
                {
                    PdfPTable _tableim = new PdfPTable(1);
                    _tableim.SpacingBefore = 20f;
                    _table.DefaultCell.Border = Rectangle.NO_BORDER;
                    string theSource = obj.SignatureData.Replace("data:image/png;base64,", "");
                    var src = theSource;
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(Convert.FromBase64String(src));
                    image.ScaleAbsolute(100f, 20f);
                    iTextSharp.text.pdf.PdfPCell imgCell1 = new iTextSharp.text.pdf.PdfPCell();
                    imgCell1.Border = Rectangle.NO_BORDER;
                    imgCell1.AddElement(new Chunk(image, 0, 0));
                    _tableim.AddCell(imgCell1);
                    PdfPCell cign = new PdfPCell(new Phrase("Signature:"));
                    cign.Border = Rectangle.NO_BORDER;
                    _tableim.AddCell(cign);
                    pdfDoc.Add(_tableim);

                }
                pdfDoc.Close();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);

            }
        }
        public void AddOutline(PdfWriter writer, string Title, float Position)
        {
            PdfDestination destination = new PdfDestination(PdfDestination.FITH, Position);
            PdfOutline outline = new PdfOutline(writer.DirectContent.RootOutline, destination, Title);
            writer.DirectContent.AddOutline(outline, "Name = " + Title);
        }

        public MemoryStream ExportExcelScreeningMatrix( ScreeningMatrix ScreeningMatrix   )
        {
             MemoryStream memoryStream = new MemoryStream();
            try
            {
                List<List<string>> screening = ScreeningMatrix.Screenings;
                XLWorkbook wb = new XLWorkbook();
                var vs = wb.Worksheets.Add("Missing Screening report");
                vs.Range("B1:H1").Merge().Value = "Missing Screening Collection report for "+ ScreeningMatrix.ClientsClassroom[0].CenterName   +" on " +DateTime.Now.ToString("MM/dd/yyyy");
                vs.Range("B1:H1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                vs.Range("B1:H1").Style.Font.SetBold(true);

               // int Reportcolumn = 2;
                int ReportRow = 3;
               //    for (var i = 1; i < screening[0].Count; i++)
               //    {
               //        vs.Cell(3, Reportcolumn).Value = screening[0][i];
               //        vs.Cell(3, Reportcolumn).Style.Font.SetBold(true);
               //        Reportcolumn++;
               //    }
               //ReportRow = 5;
               //for (var i = 1; i < screening.Count; i++)
               //{
               //    Reportcolumn = 2;
               //    for (var j = 1; j < screening[i].Count; j++)
               //    {
               //        if (j == 1)
               //        {
               //            vs.Cell(ReportRow, Reportcolumn).Value = screening[i][j];
               //            IXLColumn _column = vs.Cell(ReportRow, Reportcolumn).WorksheetColumn();
               //            _column.Width =20;
               //        }
               //        else if (screening[i][j]=="M")
               //        {
               //            vs.Cell(ReportRow, Reportcolumn).Value = screening[i][j];
               //            vs.Cell(ReportRow, Reportcolumn).AsRange().Style.Font.FontColor = XLColor.FromHtml("#295b8f");
               //            IXLColumn _column = vs.Cell(ReportRow, Reportcolumn).WorksheetColumn();
               //            _column.Width = 10;
               //        }
               //        else if (screening[i][j].Contains("ScreeningDate"))
               //        {
               //            vs.Cell(ReportRow, Reportcolumn).Value = screening[i][j].Replace("ScreeningDate","");
               //            vs.Cell(ReportRow, Reportcolumn).Style.NumberFormat.Format = "mm/dd/yyyy";
               //            vs.Cell(ReportRow, Reportcolumn).AsRange().Style.Font.FontColor = XLColor.FromArgb(0, 132, 209);
               //            IXLColumn _column = vs.Cell(ReportRow, Reportcolumn).WorksheetColumn();
               //            _column.Width = 10;
               //        }
               //        else if (screening[i][j].Contains("ExpiringDate"))
               //        {
               //            vs.Cell(ReportRow, Reportcolumn).Value = screening[i][j].Replace("ExpiringDate", "");
               //            vs.Cell(ReportRow, Reportcolumn).Style.NumberFormat.Format = "mm/dd/yyyy";
               //            vs.Cell(ReportRow, Reportcolumn).AsRange().Style.Font.FontColor = XLColor.Orange;
               //            IXLColumn _column = vs.Cell(ReportRow, Reportcolumn).WorksheetColumn();
               //            _column.Width = 10;
               //        }
               //        else if (screening[i][j].Contains("ExpiredDate"))
               //        {
               //            vs.Cell(ReportRow, Reportcolumn).Value = "X";
               //            vs.Cell(ReportRow, Reportcolumn).AsRange().Style.Font.FontColor = XLColor.Red;
               //            vs.Cell(ReportRow, Reportcolumn).Comment.AddText(screening[i][j].Replace("ExpiredDate",""));
               //            IXLColumn _column = vs.Cell(ReportRow, Reportcolumn).WorksheetColumn();
               //            _column.Width = 10;
               //        }
               //        else if (screening[i][j] != "")
               //        {
               //            vs.Cell(ReportRow, Reportcolumn).Value = screening[i][j];
               //            IXLColumn _column = vs.Cell(ReportRow, Reportcolumn).WorksheetColumn();
               //            _column.Width = 10;
               //        }
               //        else
               //        {
               //            vs.Cell(ReportRow, Reportcolumn).Value = "REFUSED";
               //            IXLColumn _column = vs.Cell(ReportRow, Reportcolumn).WorksheetColumn();
               //            _column.Width = 10;
               //        }
               //        Reportcolumn++;
               //    }
               //    ReportRow++;
               //}
               //ReportRow++;
               //int Status = 0;
               //int missingorexpired = 0;
               //int completed = 0; 
               //for (var j = 0; j < 4; j++) {
               //    ReportRow++;
               //    Reportcolumn = 2;
               //    Status = 0;
               //    for (var i = 0; i < screening[0].Count -1 ; i++)
               //    {
               //        if (i == 0)
               //        {
               //            if (j == 0)
               //            {
               //                vs.Cell(ReportRow, 2).Value = "Complete";
               //            }
               //            if (j == 1)
               //            {
               //                vs.Cell(ReportRow, 2).Value = "Missing";
               //                vs.Cell(ReportRow, 2).AsRange().Style.Font.FontColor = XLColor.FromHtml("#295b8f");
               //            }
               //            if (j == 2)
               //            {
               //                vs.Cell(ReportRow, 2).Value = "Expired";
               //                vs.Cell(ReportRow, 2).AsRange().Style.Font.FontColor = XLColor.Red;
               //            }
               //            if (j == 3)
               //            {
               //                vs.Cell(ReportRow, 2).Value = "Expiring";
               //                vs.Cell(ReportRow, 2).AsRange().Style.Font.FontColor = XLColor.Orange;
               //            }
               //        }
               //        else
               //        {
               //            var count = 0;
               //            if (j == 0)
               //            {
               //                for (var k = 1; k < screening.Count; k++)
               //                {   
               //                    if (screening[k][i + 1].Contains("ScreeningDate"))
               //                        count = count + 1;
               //                }
               //                vs.Cell(ReportRow, Reportcolumn).Value = count;
               //                Status = Status + count;
               //                completed = completed + count;
               //            }
               //            count = 0;
               //            if (j == 1)
               //            {
               //                for (var k = 1; k < screening.Count; k++)
               //                {
               //                    if (screening[k][i + 1] == "M")
               //                        count = count + 1;
               //                }
               //                vs.Cell(ReportRow, Reportcolumn).Value = count;
               //                Status = Status + count;
               //                missingorexpired = missingorexpired + count;
               //            }
               //            count = 0;
               //            if (j == 2)
               //            {
               //                for (var k = 1; k < screening.Count; k++)
               //                {
               //                    if (screening[k][i + 1].Contains("ExpiredDate"))
               //                        count = count + 1;
               //                }
               //                vs.Cell(ReportRow, Reportcolumn).Value = count;
               //                Status = Status + count;
               //                missingorexpired = missingorexpired + count;
               //            }
               //            count = 0;
               //            if (j == 3)
               //            {
               //                for (var k = 1; k < screening.Count; k++)
               //                {
               //                    if (screening[k][i + 1].Contains("ExpiringDate"))
               //                        count = count + 1;
               //                }
               //                vs.Cell(ReportRow, Reportcolumn).Value = count;
               //                Status = Status + count;
               //            }
               //            count = 0;
               //        }
               //        Reportcolumn++;
               //    }
               //    vs.Cell(ReportRow, Reportcolumn + 1).Value = Status;
               //}
               //vs.Cell(ReportRow + 2, 2).Value = "Total missing or expired records";
               //vs.Cell(ReportRow + 2, 2).Style.Font.SetBold(true);
               //vs.Cell(ReportRow + 2, 3).Value = missingorexpired;
               //vs.Cell(ReportRow + 2, 4).Value = "Total completed screening";
               //vs.Cell(ReportRow + 2, 4).Style.Font.SetBold(true);
               //vs.Cell(ReportRow + 2, 5).Value = completed;
               //ReportRow = ReportRow + 4;



               vs.Cell(ReportRow, 2).Value="Client Name";
               vs.Cell(ReportRow, 2).Style.Font.SetBold(true);
               vs.Cell(ReportRow, 3).Value = "Classroom";
               IXLColumn _column1 = vs.Cell(ReportRow, 3).WorksheetColumn();
               _column1.Width = 15;
               vs.Cell(ReportRow, 3).Style.Font.SetBold(true);
               vs.Cell(ReportRow, 4).Value = "Date";
               vs.Cell(ReportRow, 4).Style.Font.SetBold(true);
               vs.Cell(ReportRow, 5).Value = "Status";
               vs.Cell(ReportRow, 5).Style.Font.SetBold(true);
               vs.Cell(ReportRow, 6).Value = "Notes";
               vs.Cell(ReportRow, 6).Style.Font.SetBold(true);
               ReportRow = ReportRow + 2;
               foreach(string clientid in ScreeningMatrix.ClientsClassroom.Select(P => P.Eclientid).Distinct() )
               {
                   foreach (var item in ScreeningMatrix.ClientsClassroom.Where( P=>P.Eclientid == clientid))
                   {
                       vs.Cell(ReportRow, 2).Value = item.Name;
                       vs.Cell(ReportRow, 3).Value = item.ClassroomName;
                       //vs.Cell(ReportRow, 4).Value ="Date";
                       //vs.Cell(ReportRow, 5).Value ="Status";
                       //vs.Cell(ReportRow, 6).Value = "Notes";

                   }
                   ReportRow = ReportRow + 2;
                   foreach (var item in ScreeningMatrix.ClientsClassroom.Where(P => P.Eclientid == clientid))
                   {
                       vs.Cell(ReportRow, 3).Value = item.ScreeningName;
                       ReportRow++;
                   }
                   ReportRow++;
               }
                wb.SaveAs(memoryStream);
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            return memoryStream;
        }
    }

    public class TwoColumnHeaderFooter : PdfPageEventHelper
    {
        // This is the contentbyte object of the writer
        PdfContentByte cb;
        // we will put the final number of pages in a template
        PdfTemplate template;
        // this is the BaseFont we are going to use for the header / footer
        BaseFont bf = null;
        // This keeps track of the creation time
        DateTime PrintTime = DateTime.Now;
        #region Properties
        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        private Image _HeaderLeft;
        public Image HeaderLeft
        {
            get { return _HeaderLeft; }
            set { _HeaderLeft = value; }
        }
        private string _HeaderRight;
        public string HeaderRight
        {
            get { return _HeaderRight; }
            set { _HeaderRight = value; }
        }
        private Font _HeaderFont;
        public Font HeaderFont
        {
            get { return _HeaderFont; }
            set { _HeaderFont = value; }
        }
        private Font _FooterFont;
        public Font FooterFont
        {
            get { return _FooterFont; }
            set { _FooterFont = value; }
        }
        #endregion
        // we override the onOpenDocument method
        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                template = cb.CreateTemplate(50, 50);
            }
            catch (DocumentException de)
            {
            }
            catch (System.IO.IOException ioe)
            {
            }
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);
            Rectangle pageSize = document.PageSize;
            if (Title != string.Empty)
            {
                cb.BeginText();
                cb.SetFontAndSize(bf, 15);
                cb.SetRGBColorFill(50, 50, 200);
                cb.SetTextMatrix(pageSize.GetLeft(210), pageSize.GetTop(40));
                cb.ShowText(Title);
                cb.EndText();
            }
            if ( HeaderRight != string.Empty)
            {
                PdfPTable HeaderTable = new PdfPTable(2);
                HeaderTable.DefaultCell.Border = Rectangle.NO_BORDER;
                HeaderTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                HeaderTable.TotalWidth = pageSize.Width - 80;
                HeaderTable.SetWidthPercentage(new float[] { 45, 45 }, pageSize);

                PdfPCell HeaderLeftCell = new PdfPCell(HeaderLeft);
                HeaderLeftCell.Padding = 5;
                HeaderLeftCell.Border = Rectangle.NO_BORDER;
                HeaderLeftCell.PaddingBottom = 8;
                HeaderLeftCell.BorderWidthRight = 0;
                HeaderTable.AddCell(HeaderLeftCell);
                PdfPCell HeaderRightCell = new PdfPCell(new Phrase(8, HeaderRight, HeaderFont));
                HeaderRightCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                HeaderRightCell.Padding = 5;
                HeaderRightCell.PaddingBottom = 8;
                HeaderRightCell.Border = Rectangle.NO_BORDER;
                HeaderRightCell.BorderWidthLeft = 0;
                HeaderTable.AddCell(HeaderRightCell);
                cb.SetRGBColorFill(0, 0, 0);
                HeaderTable.WriteSelectedRows(0, -1, pageSize.GetLeft(40), pageSize.GetTop(50), cb);
            }
        }
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            Rectangle pageSize = document.PageSize;
            //show page no in right side of the footer
            //String text = "";
            //float len = bf.GetWidthPoint(text, 8);
            
            //cb.SetRGBColorFill(100, 100, 100);
            //cb.BeginText();
            //cb.SetFontAndSize(bf, 8);
            //cb.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetBottom(30));
            ////cb.ShowText(text);
            //cb.EndText();
            //cb.AddTemplate(template, pageSize.GetLeft(40) + len, pageSize.GetBottom(30));

            cb.BeginText();
            cb.SetFontAndSize(bf, 8);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER,
                "powered by: GEFingerPrints™  Copyright 2016, 2017 " ,
                pageSize.GetLeft(315),
                pageSize.GetBottom(30), 0);
            cb.EndText();
        }
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
            template.BeginText();
            template.SetFontAndSize(bf, 8);
            template.SetTextMatrix(0, 0);
            template.ShowText("");
            template.EndText();
        }
    }
}
