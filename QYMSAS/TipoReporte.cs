using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QYMSAS
{
    public partial class TipoReporte : Form
    {
        int iduss;
        public TipoReporte(int idusuario)
        {
            InitializeComponent();
            iduss = idusuario;
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
            ReporteMaquina creus = new ReporteMaquina(iduss);
            creus.Show();
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
            ReporteRecebera report = new ReporteRecebera(iduss);
            report.Show();
            //Report creus = new Report(iduss);
            //creus.Show();
        }

        private void pictureBox10_Click_1(object sender, EventArgs e)
        {
            int id = basededatos.ObtenerTipoUS(iduss);
            if (id == 1)
            {
            this.Dispose();
            Selección sel = new Selección(iduss);
            sel.Show();
            }else if (id == 3)
            {
                this.Dispose();
                Menu_Gerente men = new Menu_Gerente(iduss);
                men.Show();
            }
                
        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            ReporteAcopioPro sel = new ReporteAcopioPro(iduss);
            sel.Show();
        }
    }
}
