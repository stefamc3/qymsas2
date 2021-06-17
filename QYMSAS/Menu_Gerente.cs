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

    }
}
