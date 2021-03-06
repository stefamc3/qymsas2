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
    public partial class movimientos : Form
    {
        int iduss;
        public movimientos(int idusuario)
        {
            InitializeComponent();
            iduss = idusuario;
        }

        private void movimientos_Load(object sender, EventArgs e)
        {
            busqueda();
        }
        private void busqueda()
        {
            String busqueda = "select * from movimientos_foton2;";
            MySqlCommand comando = new MySqlCommand(busqueda, basededatos.ObtenerConexion());
            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
            MyAdapter.SelectCommand = comando;
            DataTable dTable = new DataTable();
            MyAdapter.Fill(dTable);
            dg_consulta.DataSource = dTable;
            dg_consulta.Columns[0].HeaderText = "ID MOVIMIENTOS";
            dg_consulta.Columns[1].HeaderText = "FECHA";
            dg_consulta.Columns[2].HeaderText = "PATIO";
            dg_consulta.Columns[3].HeaderText = "N° TRASLADOS";
            dg_consulta.Columns[4].HeaderText = "TONELADA";
            dg_consulta.Columns[5].HeaderText = "PRECIO TONELADA";
            dg_consulta.Columns[6].HeaderText = "PRECIO TOTAL";
            dg_consulta.Columns[7].HeaderText = "MAQUINA";
        }

        private void bt_nuevo_Click(object sender, EventArgs e)
        {
            limpia();
        }
        private void limpia()
        {
            this.txt_Ton.Text = "";
            this.textPatio.Text = "";
            this.textPreTone.Text = "";
            this.textItem.Text = "";
            this.txt_precioT.Text = "";
         //   Cbid_maquina.Focus();
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
        private void Bt_Ingresar_Click(object sender, EventArgs e)
        {
            try
            {
              //  string MyConnection2 = "server=mysql.freehostia.com; database=qymsas_bd; Uid=qymsas_bd; pwd=qym3103369882;";
                String fecha = "" + dt_fecha.Value.Year + "/" + dt_fecha.Value.Month + "/" + dt_fecha.Value.Day;
             //   String id_maquina = Convert.ToString(Cbid_maquina.SelectedItem);
                string Query = "INSERT INTO movimientos_foton2 (fecha,patio,item,tonelda_m,precio_tonelada,precio_total,movimientos_id_maquina) values('" + fecha + "','" + this.textPatio.Text + "','"+ this.textItem.Text + "','" + this.txt_Ton.Text + "','"+ this.textPreTone.Text + "','" + this.txt_precioT.Text + "','" +"8"+ "');";
                //MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, basededatos.ObtenerConexion());
                MySqlDataReader MyReader2;
               // MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("Se guardado el registro", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                while (MyReader2.Read())
                {
                }
               // MyConn2.Close();
                busqueda();
                limpia();
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
                string Borrar = "delete from movimientos_foton2 where id_movimientos = '" + id + "';";
                MySqlCommand ComandBorrar = new MySqlCommand(Borrar, basededatos.ObtenerConexion());
                ComandBorrar.ExecuteReader();
                busqueda();
            }
        }

        private void busAce_TextChanged(object sender, EventArgs e)
        {
            if (busAce.Text != "")
                dg_consulta.DataSource = basededatos.ConsultaGeneral("SELECT * FROM `movimientos_foton2` WHERE `patio` LIKE '%" + busAce.Text + "%'");
            else
                dg_consulta.DataSource = basededatos.ConsultaGeneral("SELECT * FROM `movimientos_foton2` ");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
          this.Dispose();
          Foton2 log = new Foton2(iduss);
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
            int modifica = basededatos.ModificaMovimientos(textPatio.Text, dg_consulta.Rows[dg_consulta.CurrentRow.Index].Cells[0].Value.ToString());
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

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            txt_precioT.Text = Convert.ToString(Convert.ToDecimal(txt_Ton.Text) * Convert.ToDecimal(textPreTone.Text));
        }

        private void dg_consulta_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dg_consulta.SelectedRows.Count > 0)
            {
                txt_precioT.Text = dg_consulta.CurrentRow.Cells["precio_total"].Value.ToString();
                txt_Ton.Text = dg_consulta.CurrentRow.Cells["tonelada_m"].Value.ToString();
                textItem.Text = dg_consulta.CurrentRow.Cells["item"].Value.ToString();
                textPatio.Text = dg_consulta.CurrentRow.Cells["patio"].Value.ToString();
                textPreTone.Text = dg_consulta.CurrentRow.Cells["precio_total"].Value.ToString();
            }
        }
    }
}
