﻿using System;
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
    public partial class Nomina_Recebera : Form
    {
        int iduss;
        public Nomina_Recebera(int idusuario)
        {
            InitializeComponent();
            iduss = idusuario;
        }
        private void Nomina_Recebera_Load(object sender, EventArgs e)
        {
            busqueda();
        }
        private void busqueda()
        {
            String busqueda = "select * from nomina WHERE tipo='RECEBERA';";
            MySqlCommand comando = new MySqlCommand(busqueda, basededatos.ObtenerConexion());
            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
            MyAdapter.SelectCommand = comando;
            DataTable dTable = new DataTable();
            MyAdapter.Fill(dTable);
            dg_consulta.DataSource = dTable;
            dg_consulta.Columns[0].HeaderText = "ID NOMINA RECEBERA";
            dg_consulta.Columns[1].HeaderText = "FECHA";
            dg_consulta.Columns[2].HeaderText = "NOMBRE";
            dg_consulta.Columns[3].HeaderText = "ID TRABAJADOR";
            dg_consulta.Columns[4].HeaderText = "SALARIO";
            dg_consulta.Columns[5].HeaderText = "AUXILIO DE TRANSPORTE";
            dg_consulta.Columns[6].HeaderText = "PRESTACIONES";
            dg_consulta.Columns[7].HeaderText = "SEGURO";
            dg_consulta.Columns[8].HeaderText = "SEGURIDAD";
            dg_consulta.Columns[9].HeaderText = "BONIFICACIONES";
            dg_consulta.Columns[10].HeaderText = "NETO";
            dg_consulta.Columns[11].HeaderText = "TIPO";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void bt_nuevo_Click(object sender, EventArgs e)
        {
            this.txt_valor.Text = "";
            this.txt_Aux.Text = "";
            this.textBon.Text = "";
            this.textNeto.Text = "";
            this.textprest.Text = "";
            this.textSeg.Text = "";
            this.textSegd.Text = "";
            Cb_idT.Focus();
            cbtipo.Focus();
            cb_nombre.Focus();
        }

        private void Bt_Ingresar_Click(object sender, EventArgs e)
        {
            try
            {
               // string MyConnection2 = "server=mysql.freehostia.com; database=qymsas_bd; Uid=qymsas_bd; pwd=qym3103369882;";
                String fecha = "" + dt_fecha.Value.Year + "/" + dt_fecha.Value.Month + "/" + dt_fecha.Value.Day;
                String identificacion = Convert.ToString(Cb_idT.SelectedItem);
                String tipo = Convert.ToString(cbtipo.SelectedItem);
                String nombre = Convert.ToString(cb_nombre.SelectedItem);
                string Query = "INSERT INTO nomina (fecha,nombre_TR,id_trabajador,salario_rec,auxilio_de_transporte_rec,prestaciones_rec,seguro_rec,seguridad_rec,bonificaciones_rec,neto_rec,tipo) values('" + fecha + "','" + nombre + "','" + identificacion + "','" + this.txt_valor.Text + "','" + this.txt_Aux.Text + "','" + this.textprest.Text + "', '" + this.textSeg.Text + "', '" + this.textSegd.Text + "','" + this.textBon.Text + "','" + this.textNeto.Text + "','" + tipo + "');";
              //  MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, basededatos.ObtenerConexion());
                MySqlDataReader MyReader2;
              //  MyConn2.Open();
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Menu_Recebera log = new Menu_Recebera(iduss);
            log.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void bt_Eliminar_Click(object sender, EventArgs e)
        {
            String id = dg_consulta.Rows[dg_consulta.CurrentRow.Index].Cells[0].Value.ToString();
            if (MessageBox.Show("¿Realmente Desea Eliminar el registro '" + id + "' ?", "Eliminar Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string Borrar = "delete from nomina where id_nomina = '" + id + "';";
                MySqlCommand ComandBorrar = new MySqlCommand(Borrar, basededatos.ObtenerConexion());
                ComandBorrar.ExecuteReader();
                busqueda();
            }
        }

        private void busAce_TextChanged(object sender, EventArgs e)
        {
            if (busAce.Text != "")
                dg_consulta.DataSource = basededatos.ConsultaGeneral("SELECT * FROM `nomina_recebera` WHERE `nombre_TR` LIKE '%" + busAce.Text + "%'");
            else
                dg_consulta.DataSource = basededatos.ConsultaGeneral("SELECT * FROM `nomina_recebera` ");
        }

        private void exportar_Click(object sender, EventArgs e)
        {
            exportExcel exc = new exportExcel();
            exc.exportaraexcel(dg_consulta);
        }

        private void btn_modificar_Click(object sender, EventArgs e)
        {
            int modifica = basededatos.ModificaNomina(textNeto.Text, dg_consulta.Rows[dg_consulta.CurrentRow.Index].Cells[0].Value.ToString());
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
    }
}
