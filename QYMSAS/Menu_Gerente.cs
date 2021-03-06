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
    public partial class Menu_Gerente : Form
    {
        int iduss;
        public Menu_Gerente(int idusuario)
        {
            InitializeComponent();
            iduss = idusuario;
        }

        private void Menu_Gerente_Load(object sender, EventArgs e)
        {
            Lus.Text = basededatos.ConsultanombresUsuario(iduss);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Dispose();
            TipoReporte repo = new TipoReporte(iduss);
            repo.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Dispose();
            estadisticas repo = new estadisticas(iduss);
            repo.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            CrearUsuario repo = new CrearUsuario(iduss);
            repo.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Login repo = new Login();
            repo.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
