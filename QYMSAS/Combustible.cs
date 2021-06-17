﻿
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
    public partial class Combustible : Form
    {
        int iduss;
        public Combustible(int idusuario)
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Menu men = new Menu(iduss);
            men.Show();
        }

        private void bt_nuevo_Click(object sender, EventArgs e)
        {
            limpia();
        }
        private void limpia()
        {
            this.txt_valor.Text = "";
            Cbid_maquina.Focus();
            this.dt_fecha.Text = DateTime.Now.ToString();
            this.txt_idFac.Text = "";
            this.txt_cantidad.Text = "";
            this.txt_descripcionf.Text = "";
            this.Cbid_maquina.Text = "";
          //  this.cb_item.Text = "";
          //  cb_item.Focus();
        }


        private void Bt_Ingresar_Click(object sender, EventArgs e)
        {
            try
            {
                // string MyConnection2 = "server=mysql.freehostia.com; database=qymsas_bd; Uid=qymsas_bd; pwd=qym3103369882;"; 
                String fecha = "" + dt_fecha.Value.Year + "/" + dt_fecha.Value.Month + "/" + dt_fecha.Value.Day;
                String id_maquina = Convert.ToString(Cbid_maquina.SelectedItem);
                //String item = Convert.ToString(cb_item.SelectedItem);
                string Query = "INSERT INTO discriminacion (fecha,cantidad,descripcion,valor,maquinas_id_maquina,facturacion_id_facturacion,item) VALUES ('" + fecha + "','" + this.txt_cantidad.Text + "','" + this.txt_descripcionf.Text + "','" + this.txt_valor.Text + "','" + id_maquina + "', '" + this.txt_idFac.Text + "','" +"COMBUSTIBLE"+ "');";
                // MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Combustible_Load(object sender, EventArgs e)
        {
            busqueda();
        }
        private void busqueda()
        {
            String busqueda = "SELECT * FROM discriminacion where item = 'COM';";
            MySqlCommand comando = new MySqlCommand(busqueda, basededatos.ObtenerConexion());
            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
            MyAdapter.SelectCommand = comando;
            DataTable dTable = new DataTable();
            MyAdapter.Fill(dTable);
            dg_consulta.DataSource = dTable;
            dg_consulta.Columns[0].HeaderText = "ID ACEITES";
            dg_consulta.Columns[1].HeaderText = "FECHA";
            dg_consulta.Columns[2].HeaderText = "CANTIDAD";
            dg_consulta.Columns[3].HeaderText = "DESCRIPCIÓN";
            dg_consulta.Columns[4].HeaderText = "VALOR";
            dg_consulta.Columns[5].HeaderText = "MAQUINA";
            dg_consulta.Columns[6].HeaderText = "FACTURA";
            dg_consulta.Columns[7].HeaderText = "ITEM";
        }

        private void bt_eliminar_Click(object sender, EventArgs e)
        {
            String id = dg_consulta.Rows[dg_consulta.CurrentRow.Index].Cells[0].Value.ToString();
            if (MessageBox.Show("¿Realmente Desea Eliminar el registro '" + id + "' ?", "Eliminar Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string Borrar = "delete from discriminacion where id_discriminacion = '" + id + "';";
                MySqlCommand ComandBorrar = new MySqlCommand(Borrar, basededatos.ObtenerConexion());
                ComandBorrar.ExecuteReader();
                busqueda();
            }
        }

        private void busCom_TextChanged(object sender, EventArgs e)
        {
            if (busCom.Text != "")
                dg_consulta.DataSource = basededatos.ConsultaGeneral("SELECT * FROM `discriminacion` WHERE `id_maquina` LIKE '%" + busCom.Text + "%'");
            else
                dg_consulta.DataSource = basededatos.ConsultaGeneral("SELECT * FROM `discriminacion` ");
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void exportar_Click(object sender, EventArgs e)
        {
            exportExcel exc = new exportExcel();
            exc.exportaraexcel(dg_consulta);
        }

        private void btn_modificar_Click(object sender, EventArgs e)
        {
            int modifica = basededatos.ModificaDiscriminacion(txt_descripcionf.Text, txt_cantidad.Text, txt_valor.Text, Cbid_maquina.Text, txt_idFac.Text, dg_consulta.Rows[dg_consulta.CurrentRow.Index].Cells[0].Value.ToString());
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
    }
}
