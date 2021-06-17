using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QYMSAS
{
    public partial class bascula : Form
    {
        int iduss;
        public bascula(int idusuario)
        {
            InitializeComponent();
            iduss = idusuario;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Remisiones1 Rem = new Remisiones1(iduss);
            Rem.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Login log = new Login();
            log.Show();
        }

        private void bascula_Load(object sender, EventArgs e)
        {
            Lus.Text = basededatos.ConsultanombresUsuario(iduss);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Cambiar_contraseña_bascula log = new Cambiar_contraseña_bascula(iduss);
            log.Show();
        }
    }
}
