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
    public partial class Trabajadores : Form
    {
        int iduss;
        public Trabajadores(int idusuario)
        {
            InitializeComponent();
            iduss = idusuario;
        }

        private void Trabajadores_Load(object sender, EventArgs e)
        {
            busqueda();
        }
        private void busqueda()
        {
            String busqueda = "select * from trabajadores;";
            MySqlCommand comando = new MySqlCommand(busqueda, basededatos.ObtenerConexion());
            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
            MyAdapter.SelectCommand = comando;
            DataTable dTable = new DataTable();
            MyAdapter.Fill(dTable);
            dg_consulta.DataSource = dTable;
            dg_consulta.Columns[0].HeaderText = "ID TRABAJADOR";
            dg_consulta.Columns[1].HeaderText = "NOMBRE";
            dg_consulta.Columns[2].HeaderText = "CARGO";
            dg_consulta.Columns[3].HeaderText = "RH";
            dg_consulta.Columns[4].HeaderText = "EPS";
            dg_consulta.Columns[5].HeaderText = "ARL";
            dg_consulta.Columns[6].HeaderText = "TELEFONO";
            dg_consulta.Columns[7].HeaderText = "TELEFONO EMERGENCIA";
            dg_consulta.Columns[8].HeaderText = "EMAIL";       
        }

        private void bt_nuevo_Click(object sender, EventArgs e)
        {
            this.txt_nom.Text = "";
            this.txt_car.Text = "";
            this.textRH.Text = "";
            this.textEPS.Text = "";
            this.txtARL.Text = "";
            this.txt_tel.Text = "";
            this.txt_telE.Text = "";
            this.txt_email.Text = "";
            cb_idTra.Focus();

        }

        private void Bt_Ingresar_Click(object sender, EventArgs e)
        {
            try
            {
                //  string MyConnection2 = "server=mysql.freehostia.com; database=qymsas_bd; Uid=qymsas_bd; pwd=qym3103369882;";
                String id_trabajador = Convert.ToString(cb_idTra.SelectedItem);
                string Query = "INSERT INTO trabajadores (id_trabajador,nombre_trabajador,cargo_trabajador,RH,EPS,ARL,telefono,tel_emergencia,email) values('" + id_trabajador + "','" + this.txt_nom.Text + "','" + this.txt_car.Text + "','" + this.textRH.Text + "','" + this.textEPS.Text + "','" + this.txtARL.Text + "','" + this.txt_tel.Text + "','" + this.txt_telE.Text + "','" + this.txt_email.Text + "');";
              //  MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, basededatos.ObtenerConexion());
                MySqlDataReader MyReader2;
             //   MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("Se guardado el registro", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                while (MyReader2.Read())
                {
                }
              //  MyConn2.Close();
                busqueda();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void bt_eliminar_Click(object sender, EventArgs e)
        {
            String id = dg_consulta.Rows[dg_consulta.CurrentRow.Index].Cells[0].Value.ToString();
            if (MessageBox.Show("¿Realmente Desea Eliminar el registro '" + id + "' ?", "Eliminar Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string Borrar = "delete from trabajadores where id_trabajador = '" + id + "';";
                MySqlCommand ComandBorrar = new MySqlCommand(Borrar, basededatos.ObtenerConexion());
                ComandBorrar.ExecuteReader();
                busqueda();
            }

        }

        private void busAce_TextChanged(object sender, EventArgs e)
        {
            if (busAce.Text != "")
                dg_consulta.DataSource = basededatos.ConsultaGeneral("SELECT * FROM `trabajadores` WHERE `nombre_trabajador` LIKE '%" + busAce.Text + "%'");
            else
                dg_consulta.DataSource = basededatos.ConsultaGeneral("SELECT * FROM `trabajadores` ");
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

        private void exportar_Click(object sender, EventArgs e)
        {
            exportExcel exc = new exportExcel();
            exc.exportaraexcel(dg_consulta);
        }
    }
}
