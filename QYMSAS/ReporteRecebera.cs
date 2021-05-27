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
    public partial class ReporteRecebera : Form
    {
        int iduss;
        public ReporteRecebera(int idusuario)
        {
            InitializeComponent();
            iduss = idusuario;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Dispose();
            FacturaC_Acopio creus = new FacturaC_Acopio(iduss);
            creus.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Nomina_Acopio creus = new Nomina_Acopio(iduss);
            creus.Show();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Selección log = new Selección(iduss);
            log.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Menu_Acopio_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Produccion_Acopio men = new Produccion_Acopio(iduss);
            men.Show();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
            Resumen_Acopio men = new Resumen_Acopio(iduss);
            men.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Facturacion_Acopio men = new Facturacion_Acopio(iduss);
            men.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
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
            string fullFilePath = folder + "frecebera.xlsx";
            var workbook = new XLWorkbook(fullFilePath); // load the existing Excel file
            var worksheet = workbook.Worksheets.Worksheet(1);
            worksheet.Cell("I7").SetValue(FechaDesde.Value.Date.ToShortDateString());
            worksheet.Cell("L7").SetValue(FechaHasta.Value.Date.ToShortDateString());
            worksheet.Cell("K14").SetValue(basededatos.sumametrosremision(fechainicial,fechafinal));
            worksheet.Cell("K15").SetValue(basededatos.sumaporplaca(fechainicial,fechafinal));
            worksheet.Cell("K17").SetValue(basededatos.sumamegastosoperativos(fechainicial,fechafinal));
            worksheet.Cell("K19").SetValue(basededatos.sumamegastosadministrativos(fechainicial,fechafinal));
            workbook.Save();
            string documentoexcel = Path.GetFullPath(fullFilePath);
            string documentopdf = Path.GetFullPath(folder + "reporterecebera.pdf");
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
                catch (System.Exception ex)
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
    }
}
