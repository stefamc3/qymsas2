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
    public partial class SZN114 : Form
    {
        int iduss;
        public SZN114(int idusuario)
        {
            InitializeComponent();
            iduss = idusuario;
        }

        private void SZN114_Load(object sender, EventArgs e)
        {
            busqueda();
        }
        private void busqueda()
        {
            String busqueda = "select * from fletes;";
            MySqlCommand comando = new MySqlCommand(busqueda, basededatos.ObtenerConexion());
            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
            MyAdapter.SelectCommand = comando;
            DataTable dTable = new DataTable();
            MyAdapter.Fill(dTable);
            dg_consulta.DataSource = dTable;
            dg_consulta.Columns[0].HeaderText = "ID FLETES SZN-114";
            dg_consulta.Columns[1].HeaderText = "FECHA";
            dg_consulta.Columns[2].HeaderText = "DESTINO";
            dg_consulta.Columns[3].HeaderText = "VALOR";
            dg_consulta.Columns[4].HeaderText = "ID MAQUINA";

        }

        private void bt_nuevo_Click(object sender, EventArgs e)
        {
            this.txt_valor.Text = "";
            this.txt_destino.Text = "";
         //   Cbid_maquina.Focus();
        }

        private void Bt_Ingresar_Click(object sender, EventArgs e)
        {
            try
            {
                string MyConnection2 = "server=mysql.freehostia.com; database=qymsas_bd; Uid=qymsas_bd; pwd=qym3103369882;";
                String fecha = "" + dt_fecha.Value.Year + "/" + dt_fecha.Value.Month + "/" + dt_fecha.Value.Day;
              //  String id_maquina = Convert.ToString(Cbid_maquina.SelectedItem);
                string Query = "INSERT INTO fletes (fecha,destino,valor,fletes_id_maquina) values('" + fecha + "','" + this.txt_destino.Text + "','" + this.txt_valor.Text + "','" +"9"+ "');";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, basededatos.ObtenerConexion());
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("Se guardado el registro", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                while (MyReader2.Read())
                {
                }
                MyConn2.Close();
                busqueda();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bt_Eliminar_Click(object sender, EventArgs e)
        {
            String id = dg_consulta.Rows[dg_consulta.CurrentRow.Index].Cells[0].Value.ToString();
            if (MessageBox.Show("¿Realmente Desea Eliminar el registro '" + id + "' ?", "Eliminar Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string Borrar = "delete from fletes where id_fletes = '" + id + "';";
                MySqlCommand ComandBorrar = new MySqlCommand(Borrar, basededatos.ObtenerConexion());
                ComandBorrar.ExecuteReader();
                busqueda();
            }
        }

        private void busAce_TextChanged(object sender, EventArgs e)
        {
            if (busAce.Text != "")
                dg_consulta.DataSource = basededatos.ConsultaGeneral("SELECT * FROM `fletes` WHERE `destino` LIKE '%" + busAce.Text + "%'");
            else
                dg_consulta.DataSource = basededatos.ConsultaGeneral("SELECT * FROM `fletes` ");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Maquinaria log = new Maquinaria(iduss);
            log.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void exportar_Click(object sender, EventArgs e)
        {
            exportExcel exc = new exportExcel();
            exc.exportaraexcel(dg_consulta);
        }

        private void btn_modificar_Click(object sender, EventArgs e)
        {
            int modifica = basededatos.ModificaFletes(txt_destino.Text, dg_consulta.Rows[dg_consulta.CurrentRow.Index].Cells[0].Value.ToString());
            if (modifica > 0)
            {
                MessageBox.Show("Se ha modificado el registro", "Modificado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                busqueda();
               // limpia();
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
                txt_destino.Text = dg_consulta.CurrentRow.Cells["destino"].Value.ToString();
                txt_valor.Text = dg_consulta.CurrentRow.Cells["valor"].Value.ToString();
                
            }
        }
    }
}
