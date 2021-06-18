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
    public partial class Facturacion_Acopio : Form
    {
        int iduss;
        public Facturacion_Acopio(int idusuario)
        {
            InitializeComponent();
            iduss = idusuario;
        }

        private void Facturacion_Acopio_Load(object sender, EventArgs e)
        {
            busqueda();
        }
        private void busqueda()
        {
            String busqueda = "SELECT * FROM facturacion WHERE tipo='ACOPIO' and Apartado = 'VENTA';";
            MySqlCommand comando = new MySqlCommand(busqueda, basededatos.ObtenerConexion());
            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
            MyAdapter.SelectCommand = comando;
            DataTable dTable = new DataTable();
            MyAdapter.Fill(dTable);
            dg_consulta.DataSource = dTable;
            dg_consulta.Columns[0].HeaderText = "ID FACTURACION";
            dg_consulta.Columns[1].HeaderText = "FECHA";
            dg_consulta.Columns[2].HeaderText = "No.FACTURA";
            dg_consulta.Columns[3].HeaderText = "DESTINO";
            dg_consulta.Columns[4].HeaderText = "NIT";
            dg_consulta.Columns[5].HeaderText = "DIRECCION";
            dg_consulta.Columns[6].HeaderText = "TELEFONO";
            dg_consulta.Columns[7].HeaderText = "MATERIAL";
            dg_consulta.Columns[8].HeaderText = "METROS";
            dg_consulta.Columns[9].HeaderText = "VALOR UNITARIO";
            dg_consulta.Columns[10].HeaderText = "SUBTOTAL";
            dg_consulta.Columns[11].HeaderText = "IVA %";
            dg_consulta.Columns[12].HeaderText = "IVA";
            dg_consulta.Columns[13].HeaderText = "RETEFUENTE %";
            dg_consulta.Columns[14].HeaderText = "RETEFUENTE";
            dg_consulta.Columns[15].HeaderText = "NETO";
            dg_consulta.Columns[16].HeaderText = "ESTADO";
            dg_consulta.Columns[17].HeaderText = "TIPO";
            dg_consulta.Columns[18].HeaderText = "TIPO DE FACTURA";
            
        }
            private void bt_nuevo_Click(object sender, EventArgs e)
        {
            limpia();
        }
        private void limpia()
        {
            this.txtfac.Text = "";
            this.txtdestino.Text = "";
            this.txtnit.Text = "";
            this.txtdireccion.Text = "";
            this.txttelefono.Text = "";
            this.txtmaterial.Text = "";
            this.txtmetros.Text = "";
            this.txtvaloru.Text = "";
            this.txtsubt.Text = "";
            this.txtivap.Text = "";
            this.txtiva.Text = "";
            this.txtretef.Text = "";
            this.txtretefp.Text = "";
            this.txtneto.Text = "";
            Cbid_estadof.Focus();
           // cbtipo.Focus();

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
               // string MyConnection2 = "server=mysql.freehostia.com; database=qymsas_bd; Uid=qymsas_bd; pwd=qym3103369882;";
                String fecha = "" + dt_fecha.Value.Year + "/" + dt_fecha.Value.Month + "/" + dt_fecha.Value.Day;
                String estado = Convert.ToString(Cbid_estadof.SelectedItem);
              //  String tipo = Convert.ToString(cbtipo.SelectedItem);
              //  String apartado = Convert.ToString(cbApartado.SelectedItem);
                string Query = "INSERT INTO facturacion (Fecha,Factura,señores,nit,direccion,telefono,Descripcion,cantidad,valor_unitario,subtotal,porcentaje_iva,iva,porcenta_Rtefuente,retefuente,neto,estado,tipo,Apartado) values('" + fecha + "','" + this.txtfac.Text + "','" + this.txtdestino.Text + "','" + this.txtnit.Text + "','" + this.txtdireccion.Text + "','" + this.txttelefono.Text + "','" + this.txtmaterial.Text + "','" + this.txtmetros.Text + "','" + this.txtvaloru.Text + "','" + this.txtsubt.Text + "','" + this.txtivap.Text + "','" + this.txtiva.Text + "','" + this.txtretefp.Text + "','" + this.txtretef.Text + "','" + this.txtneto.Text + "','" + estado + "','" +"ACOPIO"+ "','" +"VENTA"+ "';";
              //  MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, basededatos.ObtenerConexion());
                MySqlDataReader MyReader2;
             //   MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("Se guardado el registro", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                while (MyReader2.Read())
                {
                }
             //   MyConn2.Close();
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
                string Borrar = "delete from facturacion where id_facturacion = '" + id + "';";
                MySqlCommand ComandBorrar = new MySqlCommand(Borrar, basededatos.ObtenerConexion());
                ComandBorrar.ExecuteReader();
                busqueda();
            }
        }

        private void btn_exportar_Click(object sender, EventArgs e)
        {
            exportExcel exc = new exportExcel();
            exc.exportaraexcel(dg_consulta);
        }

        private void busFac_TextChanged(object sender, EventArgs e)
        {
            if (busFac.Text != "")
                dg_consulta.DataSource = basededatos.ConsultaGeneral("SELECT * FROM `facturas_de_venta` WHERE `destino` LIKE '%" + busFac.Text + "%'");
            else
                dg_consulta.DataSource = basededatos.ConsultaGeneral("SELECT * FROM `facturas_de_venta` ");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Menu_Acopio men = new Menu_Acopio(iduss);
            men.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            txtsubt.Text = Convert.ToString(Convert.ToDecimal(txtmetros.Text) * Convert.ToDecimal(txtvaloru.Text));
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            txtiva.Text = Convert.ToString(Convert.ToDecimal(txtsubt.Text) * Convert.ToDecimal(txtivap.Text));
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            txtretef.Text = Convert.ToString(Convert.ToDecimal(txtsubt.Text) * Convert.ToDecimal(txtretefp.Text));
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            txtneto.Text = Convert.ToString(Convert.ToDecimal(txtsubt.Text) - Convert.ToDecimal(txtretefp.Text) + Convert.ToDecimal(txtiva.Text));
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void btn_modificar_Click(object sender, EventArgs e)
        {
            int modifica = basededatos.ModificaFacturas(txtmaterial.Text, dg_consulta.Rows[dg_consulta.CurrentRow.Index].Cells[0].Value.ToString());
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
                txtdestino.Text = dg_consulta.CurrentRow.Cells["señores"].Value.ToString();
                txtfac.Text = dg_consulta.CurrentRow.Cells["Factura"].Value.ToString();
                txtdireccion.Text = dg_consulta.CurrentRow.Cells["direccion"].Value.ToString();
                txtiva.Text = dg_consulta.CurrentRow.Cells["iva"].Value.ToString();
                txtivap.Text = dg_consulta.CurrentRow.Cells["porcentaje_iva"].Value.ToString();
                txtmaterial.Text = dg_consulta.CurrentRow.Cells["Descripcion"].Value.ToString();
                txtmetros.Text = dg_consulta.CurrentRow.Cells["cantidad"].Value.ToString();
                txtneto.Text = dg_consulta.CurrentRow.Cells["neto"].Value.ToString();
                txtnit.Text = dg_consulta.CurrentRow.Cells["nit"].Value.ToString();
                txtretef.Text = dg_consulta.CurrentRow.Cells["retefuente"].Value.ToString();
                txtretefp.Text = dg_consulta.CurrentRow.Cells["porcenta_Rtefuente"].Value.ToString();
                txtsubt.Text = dg_consulta.CurrentRow.Cells["subtotal"].Value.ToString();
                txttelefono.Text = dg_consulta.CurrentRow.Cells["telefono"].Value.ToString();
                txtvaloru.Text = dg_consulta.CurrentRow.Cells["valor_unitario"].Value.ToString();
            }
        }
    }
}
