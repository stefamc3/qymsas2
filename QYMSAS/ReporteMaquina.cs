using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QYMSAS
{
    public partial class ReporteMaquina : Form
    {
        int iduss;
        public ReporteMaquina(int idusuario)
        {
            InitializeComponent();
            iduss = idusuario;
        }
      
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            this.Dispose();
            TipoReporte log = new TipoReporte(iduss);
            log.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Menu_Acopio_Load(object sender, EventArgs e)
        {

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            String desde = "" + FechaDesde.Value.Year + "/" + FechaDesde.Value.Month + "/" + FechaDesde.Value.Day;
            String hasta = "" + FechaHasta.Value.Year + "/" + FechaHasta.Value.Month + "/" + FechaHasta.Value.Day;
            actualizaexcel(desde,hasta);
        }

        private void actualizaexcel(String fechainicial, String fechafinal)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string folder = path + "/temp/";

            string fullFilePath = folder + "FMAQUINAS.xlsx";
            var workbook = new XLWorkbook(fullFilePath); // load the existing Excel file
            var worksheet = workbook.Worksheets.Worksheet(1);
              worksheet.Cell("F6").SetValue(FechaDesde.Value.Date.ToShortDateString());
              worksheet.Cell("L6").SetValue(FechaHasta.Value.Date.ToShortDateString());
              worksheet.Cell("D9").SetValue(basededatos.sumaaceitef1(fechainicial,fechafinal));
              worksheet.Cell("D10").SetValue(basededatos.sumaaceitef2(fechainicial,fechafinal));
              worksheet.Cell("D11").SetValue(basededatos.sumaaceiter1(fechainicial,fechafinal));
              worksheet.Cell("D12").SetValue(basededatos.sumaaceiter2(fechainicial,fechafinal));
              worksheet.Cell("D13").SetValue(basededatos.sumaaceiter3(fechainicial,fechafinal));
              worksheet.Cell("D14").SetValue(basededatos.sumaaceiter4(fechainicial,fechafinal));
              worksheet.Cell("D15").SetValue(basededatos.sumaaceiter5(fechainicial,fechafinal));
              worksheet.Cell("D16").SetValue(basededatos.sumaaceiter6(fechainicial,fechafinal));
              worksheet.Cell("D17").SetValue(basededatos.sumaaceiteSZN(fechainicial,fechafinal));
              worksheet.Cell("E9").SetValue(basededatos.sumacombustiblef1(fechainicial,fechafinal));
              worksheet.Cell("E10").SetValue(basededatos.sumacombustiblef2(fechainicial,fechafinal));
              worksheet.Cell("E11").SetValue(basededatos.sumacombustibler1(fechainicial,fechafinal));
              worksheet.Cell("E12").SetValue(basededatos.sumacombustibler2(fechainicial,fechafinal));
              worksheet.Cell("E13").SetValue(basededatos.sumacombustibler3(fechainicial,fechafinal));
              worksheet.Cell("E14").SetValue(basededatos.sumacombustibler4(fechainicial,fechafinal));
              worksheet.Cell("E15").SetValue(basededatos.sumacombustibler5(fechainicial,fechafinal));
              worksheet.Cell("E16").SetValue(basededatos.sumacombustibler6(fechainicial,fechafinal));
              worksheet.Cell("E17").SetValue(basededatos.sumacombustibleSZN(fechainicial,fechafinal));
              worksheet.Cell("F9").SetValue(basededatos.sumagalonesf1(fechainicial, fechafinal));
              worksheet.Cell("F10").SetValue(basededatos.sumagalonesf2(fechainicial, fechafinal));
              worksheet.Cell("F11").SetValue(basededatos.sumagalonesr1(fechainicial, fechafinal));
              worksheet.Cell("F12").SetValue(basededatos.sumagalonesr2(fechainicial, fechafinal));
              worksheet.Cell("F13").SetValue(basededatos.sumagalonesr3(fechainicial, fechafinal));
              worksheet.Cell("F14").SetValue(basededatos.sumagalonesr4(fechainicial, fechafinal));
              worksheet.Cell("F15").SetValue(basededatos.sumagalonesr5(fechainicial, fechafinal));
              worksheet.Cell("F16").SetValue(basededatos.sumagalonesr6(fechainicial, fechafinal));
              worksheet.Cell("F17").SetValue(basededatos.sumagalonesSZN(fechainicial, fechafinal));
              worksheet.Cell("G9").SetValue(basededatos.sumahorasf1(fechainicial, fechafinal));
              worksheet.Cell("G10").SetValue(basededatos.sumahorasf2(fechainicial, fechafinal));
              worksheet.Cell("G11").SetValue(basededatos.sumahorasr1(fechainicial, fechafinal));
              worksheet.Cell("G12").SetValue(basededatos.sumahorasr2(fechainicial, fechafinal));
              worksheet.Cell("G13").SetValue(basededatos.sumahorasr3(fechainicial, fechafinal));
              worksheet.Cell("G14").SetValue(basededatos.sumahorasr4(fechainicial, fechafinal));
              worksheet.Cell("G15").SetValue(basededatos.sumahorasr5(fechainicial, fechafinal));
              worksheet.Cell("G16").SetValue(basededatos.sumahorasr6(fechainicial, fechafinal));
              worksheet.Cell("G17").SetValue(basededatos.sumahorasSZN(fechainicial, fechafinal));
              worksheet.Cell("H9").SetValue(basededatos.sumametros3f1(fechainicial, fechafinal));
              worksheet.Cell("H10").SetValue(basededatos.sumametros3f2(fechainicial, fechafinal));
              worksheet.Cell("H11").SetValue(basededatos.sumametros3r1(fechainicial, fechafinal));
              worksheet.Cell("H12").SetValue(basededatos.sumametros3r2(fechainicial, fechafinal));
              worksheet.Cell("H13").SetValue(basededatos.sumametros3r3(fechainicial, fechafinal));
              worksheet.Cell("H14").SetValue(basededatos.sumametros3r4(fechainicial, fechafinal));
              worksheet.Cell("H15").SetValue(basededatos.sumametros3r5(fechainicial, fechafinal));
              worksheet.Cell("H16").SetValue(basededatos.sumametros3r6(fechainicial, fechafinal));
              worksheet.Cell("H17").SetValue(basededatos.sumametros3SZN(fechainicial, fechafinal));
              worksheet.Cell("I9").SetValue(basededatos.sumacuentas_cobrof1(fechainicial, fechafinal));
              worksheet.Cell("I10").SetValue(basededatos.sumacuentas_cobrof2(fechainicial, fechafinal));
              worksheet.Cell("I11").SetValue(basededatos.sumacuentas_cobror1(fechainicial, fechafinal));
              worksheet.Cell("I12").SetValue(basededatos.sumacuentas_cobror2(fechainicial, fechafinal));
              worksheet.Cell("I13").SetValue(basededatos.sumacuentas_cobror3(fechainicial, fechafinal));
              worksheet.Cell("I14").SetValue(basededatos.sumacuentas_cobror4(fechainicial, fechafinal));
              worksheet.Cell("I15").SetValue(basededatos.sumacuentas_cobror5(fechainicial, fechafinal));
              worksheet.Cell("I16").SetValue(basededatos.sumacuentas_cobror6(fechainicial, fechafinal));
              worksheet.Cell("I17").SetValue(basededatos.sumacuentas_cobroSZN(fechainicial, fechafinal));
              worksheet.Cell("J9").SetValue(basededatos.sumarecibo_cajaf1(fechainicial, fechafinal));
              worksheet.Cell("J10").SetValue(basededatos.sumarecibo_cajaf2(fechainicial, fechafinal));
              worksheet.Cell("J11").SetValue(basededatos.sumarecibo_cajar1(fechainicial, fechafinal));
              worksheet.Cell("J12").SetValue(basededatos.sumarecibo_cajar2(fechainicial, fechafinal));
              worksheet.Cell("J13").SetValue(basededatos.sumarecibo_cajar3(fechainicial, fechafinal));
              worksheet.Cell("J14").SetValue(basededatos.sumarecibo_cajar4(fechainicial, fechafinal));
              worksheet.Cell("J15").SetValue(basededatos.sumarecibo_cajar5(fechainicial, fechafinal));
              worksheet.Cell("J16").SetValue(basededatos.sumarecibo_cajar6(fechainicial, fechafinal));
              worksheet.Cell("J17").SetValue(basededatos.sumarecibo_cajaSZN(fechainicial, fechafinal));
              worksheet.Cell("K9").SetValue(basededatos.sumafacturasf1(fechainicial, fechafinal));
              worksheet.Cell("K10").SetValue(basededatos.sumafacturasf2(fechainicial, fechafinal));
              worksheet.Cell("K11").SetValue(basededatos.sumafacturasr1(fechainicial, fechafinal));
              worksheet.Cell("K12").SetValue(basededatos.sumafacturasr2(fechainicial, fechafinal));
              worksheet.Cell("K13").SetValue(basededatos.sumafacturasr3(fechainicial, fechafinal));
              worksheet.Cell("K14").SetValue(basededatos.sumafacturasr4(fechainicial, fechafinal));
              worksheet.Cell("K15").SetValue(basededatos.sumafacturasr5(fechainicial, fechafinal));
              worksheet.Cell("K16").SetValue(basededatos.sumafacturasr6(fechainicial, fechafinal));
              worksheet.Cell("K17").SetValue(basededatos.sumafacturasSZN(fechainicial, fechafinal));
              worksheet.Cell("L9").SetValue(basededatos.sumaviaticosf1(fechainicial, fechafinal));
              worksheet.Cell("L10").SetValue(basededatos.sumaviaticosf2(fechainicial, fechafinal));
              worksheet.Cell("L11").SetValue(basededatos.sumaviaticosr1(fechainicial, fechafinal));
              worksheet.Cell("L12").SetValue(basededatos.sumaviaticosr2(fechainicial, fechafinal));
              worksheet.Cell("L13").SetValue(basededatos.sumaviaticosr3(fechainicial, fechafinal));
              worksheet.Cell("L14").SetValue(basededatos.sumaviaticosr4(fechainicial, fechafinal));
              worksheet.Cell("L15").SetValue(basededatos.sumaviaticosr5(fechainicial, fechafinal));
              worksheet.Cell("L16").SetValue(basededatos.sumaviaticosr6(fechainicial, fechafinal));
              worksheet.Cell("L17").SetValue(basededatos.sumaviaticosSNZ(fechainicial, fechafinal));
            workbook.Save();
            string documentoexcel = Path.GetFullPath(fullFilePath);
            string documentopdf = Path.GetFullPath(folder + "reportemaquinaria.pdf");
            ExportWorkbookToPdf(documentoexcel, documentopdf);
        }

        public bool ExportWorkbookToPdf(string workbookPath, string outputPath)
        {
            var exportSuccessful = true;
            try
            {

                // If either required string is null or empty, stop and bail out
                if (string.IsNullOrEmpty(workbookPath) || string.IsNullOrEmpty(outputPath))
                {
                    return false;
                }

                // Create COM Objects
                Microsoft.Office.Interop.Excel.Application excelApplication;
                Microsoft.Office.Interop.Excel.Workbook excelWorkbook;

                // Create new instance of Excel
                excelApplication = new Microsoft.Office.Interop.Excel.Application();

                // Make the process invisible to the user
                excelApplication.ScreenUpdating = false;

                // Make the process silent
                excelApplication.DisplayAlerts = false;

                // Open the workbook that you wish to export to PDF
                excelWorkbook = excelApplication.Workbooks.Open(workbookPath);

                // If the workbook failed to open, stop, clean up, and bail out
                if (excelWorkbook == null)
                {
                    excelApplication.Quit();

                    excelApplication = null;
                    excelWorkbook = null;

                    return false;
                }


                try
                {
                    // Call Excel's native export function (valid in Office 2007 and Office 2010, AFAIK)
                    excelWorkbook.ExportAsFixedFormat(Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF, outputPath);
                }
                catch (System.Exception)
                {
                    // Mark the export as failed for the return value...
                    exportSuccessful = false;

                    // Do something with any exceptions here, if you wish...
                    // MessageBox.Show...        
                }
                finally
                {
                    // Close the workbook, quit the Excel, and clean up regardless of the results...
                    excelWorkbook.Close();
                    excelApplication.Quit();

                    excelApplication = null;
                    excelWorkbook = null;
                }

                // You can use the following method to automatically open the PDF after export if you wish
                // Make sure that the file actually exists first...
                if (System.IO.File.Exists(outputPath))
                {
                    System.Diagnostics.Process.Start(outputPath);
                }
            }
            catch
            {
                exportSuccessful = false;
                System.Diagnostics.Process.Start(workbookPath);
            }

            return exportSuccessful;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }
    }
}
