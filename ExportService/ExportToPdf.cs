using System.Collections.Generic;
using System.IO;
using HaenggiModel.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace HaenggiModel.ExportService
{
    public static class ExportToPdf
    {
        private static ResultsMessures results;
        private static MessureInformation patientInfo;
        private static iTextSharp.text.Font fontTitle = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 20, BaseColor.WHITE);
        private static iTextSharp.text.Font fontTableHeader = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 18, BaseColor.WHITE);
        private static iTextSharp.text.Font fontTableRow = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK);

        private static BaseColor lightblue2 = new BaseColor(120, 164, 216);
        private static BaseColor darkBlue = new BaseColor(44, 93, 152);
        private static BaseColor lightblue = new BaseColor(142, 180, 227);

        public static Stream Export(ResultsMessures result, MessureInformation patientInformation, string fileName)
        {
            results = result;
            patientInfo = patientInformation;

            using (var doc = new Document(PageSize.A4))
            {
                MemoryStream pdfStream = new MemoryStream();
                doc.AddCreationDate();
                doc.AddHeader("Header", Properties.Resources.PdfHeader);
                doc.AddTitle(Properties.Resources.PdfTitle);
                doc.AddAuthor(Properties.Resources.PdfAuthor);

                using (var writer = PdfWriter.GetInstance(doc, new FileStream(fileName, FileMode.Create)))
                {
                    doc.Open();

                    doc.SetMargins(10, 10, (float)1.5, (float)1.5);

                    SetTitle(doc);

                    SetPatientData(doc);

                    SetDentalBoneDiscrepancy(doc);

                    SetMoyers(doc);

                    SetTanaka(doc);

                    SetBolton(doc);

                    SetPont(doc);

                    doc.Close();

                    return pdfStream;
                }
            }
        }

        private static void SetPatientData(Document doc)
        {
            var itemsToDisplay = new Dictionary<string, string>();
            itemsToDisplay.Add(Properties.Resources.Patient_Name, patientInfo.PatientName.ToString());
            itemsToDisplay.Add(Properties.Resources.Patient_HcNumber, patientInfo.HcNumber.ToString());
            itemsToDisplay.Add(Properties.Resources.Patient_Professional, patientInfo.UserName.ToString());
            itemsToDisplay.Add(Properties.Resources.Patient_Date, patientInfo.DateMessure.ToShortDateString());

            SetTable(doc, Properties.Resources.Patient_Title, itemsToDisplay);
        }

        private static void SetDentalBoneDiscrepancy(Document doc)
        {
            var itemsToDisplay = new Dictionary<string, string>();

            itemsToDisplay.Add(Properties.Resources.Higher, results.DentalBoneDiscrepancy.Superior.ToString());
            itemsToDisplay.Add(Properties.Resources.Lower, results.DentalBoneDiscrepancy.Inferior.ToString());
            itemsToDisplay.Add(Properties.Resources.DentalBone_HigherAntero, results.DentalBoneDiscrepancy.SuperiorAntero.ToString());
            itemsToDisplay.Add(Properties.Resources.DentalBone_LowerAntero, results.DentalBoneDiscrepancy.InferiorAntero.ToString());
            itemsToDisplay.Add(Properties.Resources.DentalBone_LowerIncisives, results.DentalBoneDiscrepancy.InferiorIncisives.ToString());

            SetTable(doc, Properties.Resources.DentalBone_Title, itemsToDisplay);
        }

        private static void SetMoyers(Document doc)
        {
            var itemsToDisplay = new Dictionary<string, string>();

            itemsToDisplay.Add(Properties.Resources.HigherRigth, results.Moyers.RightSuperior.ToString());
            itemsToDisplay.Add(Properties.Resources.HigherLeft, results.Moyers.LeftSuperior.ToString());
            itemsToDisplay.Add(Properties.Resources.LowerRigth, results.Moyers.RightInferior.ToString());
            itemsToDisplay.Add(Properties.Resources.LowerLeft, results.Moyers.LeftInferior.ToString());

            SetTable(doc, Properties.Resources.Moyers_Title, itemsToDisplay);
        }

        private static void SetTanaka(Document doc)
        {
            var itemsToDisplay = new Dictionary<string, string>();

            itemsToDisplay.Add(Properties.Resources.Higher, results.Tanaka.Superior.ToString());
            itemsToDisplay.Add(Properties.Resources.Lower, results.Tanaka.Inferior.ToString());

            SetTable(doc, Properties.Resources.Tanaka_Title, itemsToDisplay);
        }

        private static void SetBolton(Document doc)
        {
            var itemsToDisplay = new Dictionary<string, string>();

            itemsToDisplay.Add(string.Format("{0} - {1}", Properties.Resources.Bolton_Total, GetBoltonExcessLabel(results.BoltonTotal.IsSuperiorExcess)), results.BoltonTotal.SuperiorExcess.ToString());
            itemsToDisplay.Add(string.Format("{0} - {1}", Properties.Resources.Bolton_Antero, GetBoltonExcessLabel(results.BoltonTotal.IsSuperiorExcess)), results.BoltonPreviousRelation.SuperiorExcess.ToString());

            SetTable(doc, Properties.Resources.Bolton_Title, itemsToDisplay);
        }

        private static void SetPont(Document doc)
        {
            var itemsToDisplay = new Dictionary<string, string>();

            itemsToDisplay.Add(Properties.Resources.Pont_14To24, results.Pont.Pont14To24.ToString());
            itemsToDisplay.Add(Properties.Resources.Pont_16To26, results.Pont.Pont16To26.ToString());
            itemsToDisplay.Add(Properties.Resources.Pont_LongArch, results.Pont.ArchLong.ToString());

            SetTable(doc, Properties.Resources.Pont_Title, itemsToDisplay);
        }

        private static void SetTitle(Document doc)
        {
            // Set Logo
            System.Drawing.Image bitmap = Resources.ImagesResources.Logo_Inverso_black;
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(bitmap, BaseColor.WHITE);
            image.ScalePercent(26f);
            image.Alignment = Element.ALIGN_RIGHT;
            
            doc.Add(image);

            // Set Title
            PdfPCell cell = new PdfPCell(new Phrase(Properties.Resources.Title, fontTitle))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BackgroundColor = darkBlue,
                FixedHeight = 30f,
                BorderWidth = (float)0,
                Padding = (float)5,
            };

            var table = new PdfPTable(1);
            table.WidthPercentage = 100;
            table.AddCell(cell);
            doc.Add(table);

            // Empty rows after title
            doc.Add(new Paragraph(" "));
        }

        private static void SetTable(Document doc, string title, Dictionary<string, string> rowItems)
        {
            var table = new iTextSharp.text.pdf.PdfPTable(2);
            table.WidthPercentage = 100;

            float[] widths = new float[] { 2f, 1f };
            table.SetWidths(widths);

            // table title
            var titleCell = new PdfPCell(new Phrase(title, fontTableHeader))
            {
                Colspan = 2,
                MinimumHeight = 28f,
                BackgroundColor = lightblue,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };

            table.AddCell(titleCell);
            
            // table rows
            var itemsCount = 0;

            foreach (var item in rowItems)
            {
                var color = itemsCount % 2 == 0
                        ? BaseColor.WHITE : lightblue;

                SetRowCell(table, item.Key, Element.ALIGN_LEFT, color);

                SetRowCell(table, string.IsNullOrEmpty(item.Value) ? "-" : item.Value, Element.ALIGN_RIGHT, color);

                itemsCount++;
            }
          
            doc.Add(table);

            // Empty rows after title
            doc.Add(new Paragraph(" "));
        }

        private static void SetRowCell(PdfPTable table, string text, int alignment, BaseColor backgroundColor)
        {
            var rowCell = new PdfPCell(new Phrase(text, fontTableRow))
            {
                MinimumHeight = 20f,
                BackgroundColor = backgroundColor,
                HorizontalAlignment = alignment,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };
            
            table.AddCell(rowCell);
        }

        private static string GetBoltonExcessLabel(bool isSuperior)
        {
            return isSuperior ? Properties.Resources.HigherExcess : Properties.Resources.LowerExcess;
        }
    }
}
