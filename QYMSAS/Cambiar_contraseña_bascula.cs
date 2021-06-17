using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace QYMSAS
{
    public partial class Cambiar_contraseña_bascula : Form
    {
        int iduss;
        public Cambiar_contraseña_bascula(int idusuario)
        {
            InitializeComponent();
            iduss = idusuario;
        }

        private void Cambiar_contraseña_bascula_Load(object sender, EventArgs e)
        {
            Lus.Text = basededatos.ConsultanombresUsuario(iduss);

            busqueda();
        }
        private void busqueda()
        {
            String busqueda = "SELECT * FROM usuarios WHERE tipo = 2;";
            MySqlCommand comando = new MySqlCommand(busqueda, basededatos.ObtenerConexion());
            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
            MyAdapter.SelectCommand = comando;
            DataTable dTable = new DataTable();
            MyAdapter.Fill(dTable);
            dg_consulta.DataSource = dTable;
            dg_consulta.Columns[0].HeaderText = "ID USUARIO";
            dg_consulta.Columns[1].HeaderText = "NOMBRE";
            dg_consulta.Columns[2].HeaderText = "APELLIDO";
            dg_consulta.Columns[3].HeaderText = "USUARIO";
            dg_consulta.Columns[4].HeaderText = "CONTRASEÑA";
            dg_consulta.Columns[5].HeaderText = "EMAIL";
            dg_consulta.Columns[6].HeaderText = "TIPO";
            dg_consulta.Columns[7].HeaderText = "ESTADO";
            dg_consulta.Columns[8].HeaderText = "ID TRABAJADOR";
        }

        private void Bt_Ingresar_Click_1(object sender, EventArgs e)
        {
            int modifica = basededatos.ModificaContraseña(txt_contraseña.Text, dg_consulta.Rows[dg_consulta.CurrentRow.Index].Cells[0].Value.ToString());
            if (modifica > 0)
            {
                MessageBox.Show("Se ha modificado el registro", "Modificado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("No ha modificado el registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String id = dg_consulta.Rows[dg_consulta.CurrentRow.Index].Cells[0].Value.ToString();
            if (MessageBox.Show("¿Realmente Desea Eliminar el registro '" + id + "' ?", "Eliminar Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string Borrar = "delete from usuarios where id_us = '" + id + "';";
                MySqlCommand ComandBorrar = new MySqlCommand(Borrar, basededatos.ObtenerConexion());
                ComandBorrar.ExecuteReader();
                busqueda();

            }
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
            bascula log = new bascula(iduss);
            log.Show();
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
