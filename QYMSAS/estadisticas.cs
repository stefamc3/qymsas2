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
            string[] series = { "Enero", "Febrero", "Marzo", "Abril" , "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre","Noviembre", "Diciembre" };
            int[] monto = new int[12]; ;
            chart1.Titles.Add("VENTAS DE RECEBO MENSUAL");
            monto[0] = basededatos.sumaporplaca("2021/01/01","2021/01/31");
            monto[1] = basededatos.sumaporplaca("2021/02/01", "2021/02/28");
            monto[2] = basededatos.sumaporplaca("2021/03/01", "2021/03/31");
            monto[3] = basededatos.sumaporplaca("2021/04/01", "2021/04/30");
            monto[4] = basededatos.sumaporplaca("2021/05/01", "2021/05/31");
            monto[5] = basededatos.sumaporplaca("2021/06/01", "2021/06/30");
            monto[6] = basededatos.sumaporplaca("2021/07/01", "2021/07/31");
            monto[7] = basededatos.sumaporplaca("2021/08/01", "2021/08/31");
            monto[8] = basededatos.sumaporplaca("2021/09/01", "2021/09/30");
            monto[9] = basededatos.sumaporplaca("2021/10/01", "2021/10/31");
            monto[10] = basededatos.sumaporplaca("2021/11/01", "2021/11/30");
            monto[11] = basededatos.sumaporplaca("2021/12/01", "2021/12/31");

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
            this.Dispose();
            Selección men = new Selección(iduss);
            men.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
