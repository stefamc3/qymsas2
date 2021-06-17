using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QYMSAS
{
    public partial class estadisticas : Form
    {
        int iduss;
        public estadisticas(int idusuario)
        {
            InitializeComponent();
            iduss = idusuario;
        }

        private void estadisticas_Load(object sender, EventArgs e)
        {
            String año = DateTime.Now.Year.ToString();
            
            string[] series = { "Enero", "Febrero", "Marzo", "Abril" , "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre","Noviembre", "Diciembre" };
            int[] monto = new int[12]; ;
            chart1.Titles.Add("VENTAS DE RECEBO MENSUAL");
            monto[0] = basededatos.sumaporplaca(""+año+"/01/01",""+año+"/01/31");
            monto[1] = basededatos.sumaporplaca("" + año + "/02/01", "" + año + "/02/28");
            monto[2] = basededatos.sumaporplaca("" + año + "/03/01", "" + año + "/03/31");
            monto[3] = basededatos.sumaporplaca("" + año + "/04/01", "" + año + "/04/30");
            monto[4] = basededatos.sumaporplaca("" + año + "/05/01", "" + año + "/05/31");
            monto[5] = basededatos.sumaporplaca("" + año + "/06/01", "" + año + "/06/30");
            monto[6] = basededatos.sumaporplaca("" + año + "/07/01", "" + año + "/07/31");
            monto[7] = basededatos.sumaporplaca(""+año+"/08/01", ""+año+"/08/31");
            monto[8] = basededatos.sumaporplaca(""+año+"/09/01", ""+año+"/09/30");
            monto[9] = basededatos.sumaporplaca(""+año+"/10/01", ""+año+"/10/31");
            monto[10] = basededatos.sumaporplaca(""+año+"/11/01", ""+año+"/11/30");
            monto[11] = basededatos.sumaporplaca(""+año+"/12/01", ""+año+"/12/31");

            chart1.Palette = ChartColorPalette.Pastel;

            for (int i = 0; i < series.Length; i++)
            {
                Series serie = chart1.Series.Add(series[i]);
                serie.Label = monto[i].ToString();
                serie.Points.Add(monto[i]);
            }            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            int id = basededatos.ObtenerTipoUS(iduss);
            if (id == 1)
            {
                this.Dispose();
                Selección sel = new Selección(iduss);
                sel.Show();
            }
            else if (id == 3)
            {
                this.Dispose();
                Menu_Gerente men = new Menu_Gerente(iduss);
                men.Show();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bt_nuevo_Click(object sender, EventArgs e)
        {
            String año = txt_año.Text;

            string[] series = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            int[] monto = new int[12]; ;
            chart1.Titles.Clear();
            chart1.Titles.Add("VENTAS DE RECEBO MENSUAL");
            monto[0] = basededatos.sumaporplaca("" + año + "/01/01", "" + año + "/01/31");
            monto[1] = basededatos.sumaporplaca(""+año+"/02/01", ""+año+"/02/28");
            monto[2] = basededatos.sumaporplaca(""+año+"/03/01", ""+año+"/03/31");
            monto[3] = basededatos.sumaporplaca(""+año+"/04/01", ""+año+"/04/30");
            monto[4] = basededatos.sumaporplaca(""+año+"/05/01", ""+año+"/05/31");
            monto[5] = basededatos.sumaporplaca(""+año+"/06/01", ""+año+"/06/30");
            monto[6] = basededatos.sumaporplaca(""+año+"/07/01", ""+año+"/07/31");
            monto[7] = basededatos.sumaporplaca(""+año+"/08/01", ""+año+"/08/31");
            monto[8] = basededatos.sumaporplaca(""+año+"/09/01", ""+año+"/09/30");
            monto[9] = basededatos.sumaporplaca(""+año+"/10/01", ""+año+"/10/31");
            monto[10] = basededatos.sumaporplaca(""+año+"/11/01", ""+año+"/11/30");
            monto[11] = basededatos.sumaporplaca(""+año+"/12/01", ""+año+"/12/31");

            chart1.Palette = ChartColorPalette.Pastel;
            chart1.Series.Clear();
            for (int i = 0; i < series.Length; i++)
            {
                Series serie = chart1.Series.Add(series[i]);
                serie.Label = monto[i].ToString();
                serie.Points.Add(monto[i]);
            }
        }
    }
}
