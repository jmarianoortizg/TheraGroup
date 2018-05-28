using GrupoThera.BusinessModel.Contracts.Cotizacion;
using GrupoThera.BusinessModel.Contracts.General;
using GrupoThera.BusinessModel.Contracts.OT;
using GrupoThera.WebUI.Utils;
using HiQPdf;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GrupoThera.WebUI.Controllers
{
    public class DocumentsController : Controller
    {
        #region Fields

        private ICatalogService _catalogService;
        private IOTPreliminarService _otPreliminarService;
        private ICotizacionService _cotizacionService;



        private static string CoreRoute = System.AppDomain.CurrentDomain.BaseDirectory;

        #endregion Fields

        #region Constructor

        public DocumentsController(ICatalogService catalogService,
                                    ICotizacionService cotizacionService,
                                    IOTPreliminarService otPreliminarService)
        {
            _catalogService = catalogService;
            _otPreliminarService = otPreliminarService;
            _cotizacionService = cotizacionService;
        }

        #endregion Constructor

        #region Methods

        public ActionResult TestDocuments()
        {
            return View();
        }

        #region WelfareDocuments

        public ActionResult CotizacionPreliminarWelfare(int documentId)
        {
            var document = _cotizacionService.getPreliminarById(documentId);
            document.Prepartidas = _cotizacionService.getAllPrePartidasByPreliminar(documentId);
            var idDocument = generateIdDocument(document.preliminaresId, document.noDoc, document.noDocInt);

            var pdfDocument = createDocument("Welfare", "Cotizacion Preliminar", documentId.ToString());
            byte[] pdfBuffer = pdfDocument.ConvertHtmlToMemory(StdClassWeb.RenderToString(PartialView("~/Views/Documents/CotizacionPreliminarWelfare.cshtml", document), HttpContext), "");
            return WatherMarkDocument(pdfBuffer, "CotPreWelfare" + idDocument);
        }

        public ActionResult CotizacionServicioWelfare(int documentId)
        {
            var document = _otPreliminarService.getOTPreliminarById(documentId);
            document.OTPrePartidas = _otPreliminarService.getAllPrePartidasByOTPreliminar(documentId);
            var idDocument = generateIdDocument(document.preliminaresId, document.noDoc, document.noDocInt);

            var pdfDocument = createDocument("Welfare", "Cotizacion Servicio", "43");
            byte[] pdfBuffer = pdfDocument.ConvertHtmlToMemory(StdClassWeb.RenderToString(PartialView("~/Views/Documents/CotizacionServicioWelfare.cshtml", document), HttpContext), "");
            return WatherMarkDocument(pdfBuffer, "OTPreWelfare" + idDocument);
        }

        public ActionResult OrdenTrabajoPreliminarWelfare(int documentId)
        {
            var document = _cotizacionService.getPreliminarById(documentId);
            var idDocument = generateIdDocument(document.preliminaresId, document.noDoc, document.noDocInt);

            var pdfDocument = createDocument("Welfare", "Orden Trabajo Preliminar", "43");
            byte[] pdfBuffer = pdfDocument.ConvertHtmlToMemory(StdClassWeb.RenderToString(PartialView("~/Views/Documents/OrdenTrabajoPreliminarWelfare.cshtml", document), HttpContext), "");
            return WatherMarkDocument(pdfBuffer, "OrdenTrabajoPreliminarWelfareTest");
        }

        public ActionResult OrdenTrabajoServicioWelfare(int documentId)
        {
            var document = _cotizacionService.getPreliminarById(documentId);
            var idDocument = generateIdDocument(document.preliminaresId, document.noDoc, document.noDocInt);

            var pdfDocument = createDocument("Welfare", "Orden Trabajo Servicio", "43");
            byte[] pdfBuffer = pdfDocument.ConvertHtmlToMemory(StdClassWeb.RenderToString(PartialView("~/Views/Documents/OrdenTrabajoServicioWelfare.cshtml", document), HttpContext), "");
            return WatherMarkDocument(pdfBuffer, "OrdenTrabajoServicioWelfareTest");
        }
        #endregion WelfareDocuments

        #region PiaDocuments
        public ActionResult CotizacionPreliminarPia(int documentId)
        {
            var document = _cotizacionService.getPreliminarById(documentId);
            var idDocument = generateIdDocument(document.preliminaresId, document.noDoc, document.noDocInt);
            var pdfDocument = createDocument("Pia", "Cotizacion Preliminar", documentId.ToString());
            byte[] pdfBuffer = pdfDocument.ConvertHtmlToMemory(StdClassWeb.RenderToString(PartialView("~/Views/Documents/CotizacionPreliminarPia.cshtml", document), HttpContext), "");
            return WatherMarkDocument(pdfBuffer, "CotPrePia" + idDocument);
        }

        public ActionResult CotizacionServicioPia()
        {
            var pdfDocument = createDocument("Pia", "Cotizacion Servicio", "43");
            byte[] pdfBuffer = pdfDocument.ConvertHtmlToMemory(StdClassWeb.RenderToString(PartialView("~/Views/Documents/CotizacionServicioPia.cshtml"), HttpContext), "");
            return WatherMarkDocument(pdfBuffer, "CotizacionServicioPiaTest");
        }

        public ActionResult OrdenTrabajoPreliminarPia()
        {
            var pdfDocument = createDocument("Pia", "Orden Trabajo Preliminar", "43");
            byte[] pdfBuffer = pdfDocument.ConvertHtmlToMemory(StdClassWeb.RenderToString(PartialView("~/Views/Documents/OrdenTrabajoPreliminarPia.cshtml"), HttpContext), "");
            return WatherMarkDocument(pdfBuffer, "OrdenTrabajoPreliminarPiaTest");
        }

        public ActionResult OrdenTrabajoServicioPia()
        {
            var pdfDocument = createDocument("Pia", "Orden Trabajo Servicio", "43");
            byte[] pdfBuffer = pdfDocument.ConvertHtmlToMemory(StdClassWeb.RenderToString(PartialView("~/Views/Documents/OrdenTrabajoServicioPia.cshtml"), HttpContext), "");
            return WatherMarkDocument(pdfBuffer, "OrdenTrabajoServicioPiaTest");
        }
        #endregion PiaDocuments


        private HtmlToPdf createDocument(string company, string kindDocument, string numberDoc)
        {
            HtmlToPdf htmlToPdfConverter = new HtmlToPdf();
            htmlToPdfConverter.Document.FitPageWidth = true;

            SetHeader(htmlToPdfConverter.Document, company);
            SetFooter(htmlToPdfConverter.Document, company, kindDocument, numberDoc);

            return htmlToPdfConverter;
        }

        private FileResult WatherMarkDocument(byte[] pdfBuffer, string namePDF, bool whatermark = true)
        {
            if (!whatermark)
                return new FileContentResult(pdfBuffer, "application/pdf");

            FileResult fileResult = null;
            string cheatImage = CoreRoute + @"Content\assets\img\logo\WatherMark2.png";
            using (Stream inputPdfStream = new MemoryStream(pdfBuffer))
            using (Stream inputImageStream = new FileStream(cheatImage, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (MemoryStream outputPdfStream = new MemoryStream())
            {
                var reader = new PdfReader(inputPdfStream);
                var stamper = new PdfStamper(reader, outputPdfStream);
                PdfContentByte pdfContentByte = null;
                int c = reader.NumberOfPages;
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(inputImageStream);

                for (int i = 1; i <= c; i++)
                {
                    pdfContentByte = stamper.GetOverContent(i);
                    image.SetAbsolutePosition(222, 743);
                    pdfContentByte.AddImage(image);
                }
                stamper.Close();
                fileResult = new FileContentResult(outputPdfStream.GetBuffer(), "application/pdf");
            }
            fileResult.FileDownloadName = namePDF + ".pdf";
            return fileResult;
        }

        private void SetHeader(PdfDocumentControl htmlToPdfDocument, string company)
        {
            htmlToPdfDocument.Header.Enabled = true;
            htmlToPdfDocument.Header.Height = 75;
            float pdfPageWidth =
                htmlToPdfDocument.PageOrientation == PdfPageOrientation.Portrait ?
                        htmlToPdfDocument.PageSize.Width : htmlToPdfDocument.PageSize.Height;
            float headerWidth = pdfPageWidth - htmlToPdfDocument.Margins.Left
                        - htmlToPdfDocument.Margins.Right;
            float headerHeight = htmlToPdfDocument.Header.Height;

            htmlToPdfDocument.Header.BackgroundColor = System.Drawing.Color.White;

            string headerImageFile = null;
            HiQPdf.PdfImage logoHeaderImage = null;

            string headerImageFile2 = null;
            HiQPdf.PdfImage logoHeaderImage2 = null;

            if (company.Equals("Welfare"))
            {
                headerImageFile = Server.MapPath("~") + @"\Content\assets\img\logo\logoOriginal.jpg";
                logoHeaderImage = new HiQPdf.PdfImage(190, 17, 90, System.Drawing.Image.FromFile(headerImageFile));

                headerImageFile2 = Server.MapPath("~") + @"\Content\assets\img\logo\logo" + company + ".jpg";
                logoHeaderImage2 = new HiQPdf.PdfImage(290, 17, 120, System.Drawing.Image.FromFile(headerImageFile2));
            }
            else
            {

                headerImageFile = Server.MapPath("~") + @"\Content\assets\img\logo\logoOriginal.jpg";
                logoHeaderImage = new HiQPdf.PdfImage(210, 17, 90, System.Drawing.Image.FromFile(headerImageFile));

                headerImageFile2 = Server.MapPath("~") + @"\Content\assets\img\logo\logo" + company + ".jpg";
                logoHeaderImage2 = new HiQPdf.PdfImage(310, 17, 60, System.Drawing.Image.FromFile(headerImageFile2));
            }

            htmlToPdfDocument.Header.Layout(logoHeaderImage);
            htmlToPdfDocument.Header.Layout(logoHeaderImage2);

            HiQPdf.PdfRectangle borderRectangle = new HiQPdf.PdfRectangle(1, 1, headerWidth - 2, headerHeight - 2);
            borderRectangle.ForeColor = System.Drawing.Color.White;
            htmlToPdfDocument.Header.Layout(borderRectangle);
        }

        private void SetFooter(PdfDocumentControl htmlToPdfDocument, string company, string tipoDocumento, string number)
        {
            // enable footer display
            htmlToPdfDocument.Footer.Enabled = true;

            // set footer height
            htmlToPdfDocument.Footer.Height = 20;

            // set footer background color
            htmlToPdfDocument.Footer.BackgroundColor = System.Drawing.Color.White;

            float pdfPageWidth =
                    htmlToPdfDocument.PageOrientation == PdfPageOrientation.Portrait ?
                    htmlToPdfDocument.PageSize.Width : htmlToPdfDocument.PageSize.Height;

            float footerWidth = pdfPageWidth - htmlToPdfDocument.Margins.Left - htmlToPdfDocument.Margins.Right;
            float footerHeight = htmlToPdfDocument.Footer.Height;

            var copyrigth = "";
            if (company.Equals("Welfare"))
                copyrigth = "Copyright © Welfare Ecologia Industrial S.A.de C.V. | " + tipoDocumento + "#" + number + " Pagina {CrtPage} de {PageCount}";
            else
                copyrigth = "Copyright © PIA Presicion Instrumental Aplicada 2013 | " + tipoDocumento + "#" + number + " Pagina {CrtPage} de {PageCount}";

            System.Drawing.Font pageNumberingFont =
                       new System.Drawing.Font(new System.Drawing.FontFamily("Verdana"),
                                   8, System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point);
            PdfText pageNumberingText = new PdfText(5, footerHeight - 12, copyrigth
                    , pageNumberingFont);
            pageNumberingText.HorizontalAlign = PdfTextHAlign.Center;
            pageNumberingText.EmbedSystemFont = true;
            pageNumberingText.ForeColor = System.Drawing.ColorTranslator.FromHtml("#5D6975"); 
            htmlToPdfDocument.Footer.Layout(pageNumberingText);

            HiQPdf.PdfRectangle borderRectangle = new HiQPdf.PdfRectangle(1, 1, footerWidth - 2, footerHeight - 2);
            borderRectangle.LineStyle.LineWidth = 0.5f;
            borderRectangle.ForeColor = System.Drawing.Color.White;
            htmlToPdfDocument.Footer.Layout(borderRectangle);
        }

        private string generateIdDocument(long idDocument, long noDoc, long noDocInt)
        {
            var idItem = "";
            if (noDoc != 0)
            {
                idItem = noDoc.ToString() + "-" + noDocInt.ToString();
            }
            else
            {
                idItem = idDocument.ToString();
            }
            return idItem;
        }

        public ActionResult ShowTestDocumentView(string documentSelected)
        {
            if (documentSelected.Equals("CotizacionPreliminarWF"))
                return PartialView("~/Views/Documents/CotizacionPreliminarWelfare.cshtml");
            else if (documentSelected.Equals("CotizacionServicioWF"))
                return PartialView("~/Views/Documents/CotizacionServicioWelfare.cshtml");
            else if (documentSelected.Equals("OrdenTrabajoPreliminarWF"))
                return PartialView("~/Views/Documents/OrdenTrabajoPreliminarWelfare.cshtml");
            else if (documentSelected.Equals("OrdenTrabajoServicioWF"))
                return PartialView("~/Views/Documents/OrdenTrabajoServicioWelfare.cshtml");

            else if (documentSelected.Equals("CotizacionPreliminarPIA"))
                return PartialView("~/Views/Documents/CotizacionPreliminarPia.cshtml");
            else if (documentSelected.Equals("CotizacionServicioPIA"))
                return PartialView("~/Views/Documents/CotizacionServicioPia.cshtml");
            else if (documentSelected.Equals("OrdenTrabajoPreliminarPIA"))
                return PartialView("~/Views/Documents/OrdenTrabajoPreliminarPia.cshtml");
            else if (documentSelected.Equals("OrdenTrabajoServicioPIA"))
                return PartialView("~/Views/Documents/OrdenTrabajoServicioPia.cshtml");

            return null;
        }

        #endregion Methods
    }
}