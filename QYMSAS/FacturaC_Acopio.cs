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
    public partial class FacturaC_Acopio : Form
    {
        int iduss;
        public FacturaC_Acopio(int idusuario)
        {
            InitializeComponent();
            iduss = idusuario;
        }
        basededatos bd = new basededatos();
        private void GastosAcopio_Load(object sender, EventArgs e)
        {
            busqueda();
        }

        private void busqueda()
        {
            String busqueda = "select * from facturacion where tipo='ACOPIO' and Apartado='COMPRA';";
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

        private void bt_nuevo_Click_1(object sender, EventArgs e)
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
            //this.txt_idR.Text = "";
            Cbid_estadof.Focus();
            //cbtipo.Focus();
            //cbApartado.Focus();

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

        private void Bt_Ingresar_Click_1(object sender, EventArgs e)
        {
            try
            {
                // string MyConnection2 = "server=mysql.freehostia.com; database=qymsas_bd; Uid=qymsas_bd; pwd=qym3103369882;";
                String fecha = "" + dt_fecha.Value.Year + "/" + dt_fecha.Value.Month + "/" + dt_fecha.Value.Day;
                String estado = Convert.ToString(Cbid_estadof.SelectedItem);
             //   String tipo = Convert.ToString(cbtipo.SelectedItem);
             //   String apartado = Convert.ToString(cbApartado.SelectedItem);
                string Query = "INSERT INTO facturacion (Fecha,Factura,señores,nit,direccion,telefono,Descripcion,cantidad,valor_unitario,subtotal,porcentaje_iva,iva,porcenta_Rtefuente,retefuente,neto,estado,tipo,Apartado) values('" + fecha + "','" + this.txtfac.Text + "','" + this.txtdestino.Text + "','" + this.txtnit.Text + "','" + this.txtdireccion.Text + "','" + this.txttelefono.Text + "','" + this.txtmaterial.Text + "','" + this.txtmetros.Text + "','" + this.txtvaloru.Text + "','" + this.txtsubt.Text + "','" + this.txtivap.Text + "','" + this.txtiva.Text + "','" + this.txtretefp.Text + "','" + this.txtretef.Text + "','" + this.txtneto.Text + "','" + estado + "','" +"ACOPIO"+ "','" +"COMPRA"+ "');";
                //   MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, basededatos.ObtenerConexion());
                MySqlDataReader MyReader2;
                //   MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("Se guardado el registro", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                while (MyReader2.Read())
                {
                }
              //MyConn2.Close();
                busqueda();
                limpia();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bt_Eliminar_Click_1(object sender, EventArgs e)
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

        private void busAce_TextChanged_1(object sender, EventArgs e)
        {
            if (busAce.Text != "")
                dg_consulta.DataSource = basededatos.ConsultaGeneral("SELECT * FROM `facturacion` WHERE `señores` LIKE '%" + busAce.Text + "%'");
            else
                dg_consulta.DataSource = basededatos.ConsultaGeneral("SELECT * FROM `facturacion` ");
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
            Menu_Acopio log = new Menu_Acopio(iduss);
            log.Show();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void exportar_Click(object sender, EventArgs e)
        {
            exportExcel exc = new exportExcel();
            exc.exportaraexcel(dg_consulta);
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

        private void busqueda2()
        {
            String busqueda = "select case when facturacion.estado='PAGO' then 'P' else 'O' end as '1', case when facturacion.nit<999999999 then " +
                "concat('0', facturacion.nit) else facturacion.nit end as '2', month(fecha) as '3', case when facturacion.nit<999999999 then " +
                "concat('0000', facturacion.nit) else concat('000', facturacion.nit) end as '4', case when facturacion.tipo='ACOPIO' then '001' " +
                "end as '5', case when facturacion.apartado='COMPRA' then '0002' end as '6', case when facturacion.apartado='COMPRA' then '000' end as '7', " +
                "case when facturacion.descripcion='RECEBO' then '001' when facturacion.descripcion='CARBON' then '002' when facturacion.descripcion='ALQUILER MAQUINA' " +
                "then '003' when facturacion.descripcion='TRANSPORTE' then '004' when facturacion.descripcion='ACEITES Y FILTROS' then '005' " +
                "when facturacion.descripcion='COMBUSTIBLE' then '006' else '007' end as '8', facturacion.factura as '9', " +
                "facturacion.valor_unitario as '10', facturacion.cantidad as '11', case when facturacion.tipo='ACOPIO' then '0001' end as '12', " +
                "case when facturacion.apartado='COMPRA' then '000' end as '13', facturacion.subtotal as '14', facturacion.iva as '15', facturacion.retefuente as '16', " +
                "facturacion.neto as '17' from facturacion where tipo='ACOPIO' and Apartado = 'COMPRA'";
            MySqlCommand comando = new MySqlCommand(busqueda, basededatos.ObtenerConexion());
            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
            MyAdapter.SelectCommand = comando;
            DataTable dTable = new DataTable();
            MyAdapter.Fill(dTable);
            dg_siigo.DataSource = dTable;
        }
        private void exportar_Click_1(object sender, EventArgs e)
        {
            busqueda2();
            exportExcel exc = new exportExcel();
            exc.exportaraexcelsiigo(dg_siigo);
        }
    }
}


    
