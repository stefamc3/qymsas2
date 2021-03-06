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
    public partial class ReciboDeCaja : Form
    {
        int iduss;
        public ReciboDeCaja(int idusuario)
        {
            InitializeComponent();
            iduss = idusuario;
        }

        private void solonumeros(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }



        private void bt_nuevo_Click_1(object sender, EventArgs e)
        {
            limpia();
        }
        private void limpia()
        {
            this.txt_valor.Text = "";
            this.txt_descripcionc.Text = "";
            this.txt_Pagado.Text = "";
            cb_idMaquina.Focus();
        }
        private void Bt_Ingresar_Click_1(object sender, EventArgs e)
        {
            try
            {
                //string MyConnection2 = "server=mysql.freehostia.com; database=qymsas_bd; Uid=qymsas_bd; pwd=qym3103369882;";
                String fecha = "" + dt_fecha.Value.Year + "/" + dt_fecha.Value.Month + "/" + dt_fecha.Value.Day;
                String id_maquina = Convert.ToString(cb_idMaquina.SelectedItem);
                string Query = "INSERT INTO recibo_de_caja (fecha,pagado_a,descripcion,valor,reciboC_id_maquina) values('" + fecha + "','" + this.txt_Pagado.Text + "','" + this.txt_descripcionc.Text + "','" + this.txt_valor.Text + "','" + id_maquina + "');";
              //MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, basededatos.ObtenerConexion());
                MySqlDataReader MyReader2;
              //MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("Se guardado el registro", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                while (MyReader2.Read())
                {
                }
              //  MyConn2.Close();
                busqueda();
                limpia();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
            Menu men = new Menu(iduss);
            men.Show();
        }

        private void pictureBox6_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
            Login log = new Login();
            log.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ReciboDeCaja_Load(object sender, EventArgs e)
        {
            busqueda();
        }
        private void busqueda()
        {
            String busqueda = "select * from recibo_de_caja;";
            MySqlCommand comando = new MySqlCommand(busqueda, basededatos.ObtenerConexion());
            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
            MyAdapter.SelectCommand = comando;
            DataTable dTable = new DataTable();
            MyAdapter.Fill(dTable);
            dg_consulta.DataSource = dTable;
            dg_consulta.Columns[0].HeaderText = "ID RECIBO DE CAJA";
            dg_consulta.Columns[1].HeaderText = "FECHA";
            dg_consulta.Columns[2].HeaderText = "PAGADO A";
            dg_consulta.Columns[3].HeaderText = "DESCRIPCIÓN";
            dg_consulta.Columns[4].HeaderText = "VALOR";
            dg_consulta.Columns[5].HeaderText = "MAQUINA";
        }

        private void bt_eliminar_Click(object sender, EventArgs e)
        {
            String id = dg_consulta.Rows[dg_consulta.CurrentRow.Index].Cells[0].Value.ToString();
            if (MessageBox.Show("¿Realmente Desea Eliminar el registro '" + id + "' ?", "Eliminar Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string Borrar = "delete from recibo_de_caja where id_reciboC = '" + id + "';";
                MySqlCommand ComandBorrar = new MySqlCommand(Borrar, basededatos.ObtenerConexion());
                ComandBorrar.ExecuteReader();
                busqueda();
            }
        }

        private void busRc_TextChanged(object sender, EventArgs e)
        {
            if (busRc.Text != "")
                dg_consulta.DataSource = basededatos.ConsultaGeneral("SELECT * FROM `recibo_de_caja` WHERE `reciboC_id_maquina` LIKE '%" + busRc.Text + "%'");
            else
                dg_consulta.DataSource = basededatos.ConsultaGeneral("SELECT * FROM `recibo_de_caja` ");
        }

        private void exportar_Click(object sender, EventArgs e)
        {
            exportExcel exc = new exportExcel();
            exc.exportaraexcel(dg_consulta);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int modifica = basededatos.ModificaReciboC(txt_descripcionc.Text, dg_consulta.Rows[dg_consulta.CurrentRow.Index].Cells[0].Value.ToString());
            if (modifica > 0)
            {
                MessageBox.Show("Se ha modificado el registro", "Modificado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                busqueda();
                limpia();
            }
            else
            {
                MessageBox.Show("No ha modificado el registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dg_consulta_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dg_consulta.SelectedRows.Count > 0)
            {
                txt_descripcionc.Text = dg_consulta.CurrentRow.Cells["descripcion"].Value.ToString();
                txt_Pagado.Text = dg_consulta.CurrentRow.Cells["pagado_a"].Value.ToString();
                txt_valor.Text = dg_consulta.CurrentRow.Cells["valor"].Value.ToString();
            }
        }
    }
}
    
