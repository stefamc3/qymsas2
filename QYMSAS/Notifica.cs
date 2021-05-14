using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QYMSAS
{
    public partial class Notifica : Form
    {
        public Notifica()
        {
            InitializeComponent();
            
        }

        private async void Notifica_Load(object sender, EventArgs e)
        {
            await notificahoras();   
        }

        private async Task notificahoras()
        {
            var task = new Task(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    sumahoras();
                    Thread.Sleep(5000);
                }

            });
            task.Start();
        }

        int m1 = 0;
        int m2 = 0;
        int m3 = 0;
        int m4 = 0;
        int m5 = 0;
        int m6 = 0;
        int m7 = 0;
        int m8 = 0;
        int m9 = 0;
        int m10 = 0;
        public int sumahoras()
        {
            int suma = 0;
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("select sum(horas.Num_Horas), horas.id_maquina , maquinas.nombre from horas, maquinas where horas.id_maquina=maquinas.id_maquina group by horas.id_maquina"), basededatos.ObtenerConexion());
                MySqlDataReader _reader = comando.ExecuteReader();
                Int64 Suma = 0;
                Int32 nmaquina=0;
                while (_reader.Read())
                {
                    Suma = _reader.GetInt32(0);
                    if (Suma > 500)
                    {
                        nmaquina = _reader.GetInt32(1);
                        if (nmaquina == 1)
                        {
                            if(m1 <= 1)
                            {
                                MessageBox.Show("La maquina: " + _reader.GetString(2) + " acaba de llegar a las 500 horas");
                            }
                            m1++;
                        } else if (nmaquina == 2)
                        {
                            if (m2 <= 1)
                            {
                                MessageBox.Show("La maquina: " + _reader.GetString(2) + " acaba de llegar a las 500 horas");
                            }
                            m2++;
                        }
                        else if (nmaquina == 3)
                        {
                            if (m3 <= 1)
                            {
                                MessageBox.Show("La maquina: " + _reader.GetString(2) + " acaba de llegar a las 500 horas");
                            }
                            m3++;
                        }
                        else if (nmaquina == 4)
                        {
                            if (m4 <= 1)
                            {
                                MessageBox.Show("La maquina: " + _reader.GetString(2) + " acaba de llegar a las 500 horas");
                            }
                            m4++;
                        }
                        else if (nmaquina == 5)
                        {
                            if (m5 <= 1)
                            {
                                MessageBox.Show("La maquina: " + _reader.GetString(2) + " acaba de llegar a las 500 horas");
                            }
                            m5++;
                        }                       
                        else if (nmaquina == 6)
                        {
                            if (m6 <= 1)
                            {
                                MessageBox.Show("La maquina: " + _reader.GetString(2) + " acaba de llegar a las 500 horas");
                            }
                            m6++;
                        }
                        else if (nmaquina == 7)
                        {
                            if (m7 <= 1)
                            {
                                MessageBox.Show("La maquina: " + _reader.GetString(2) + " acaba de llegar a las 500 horas");
                            }
                            m7++;
                        }
                        else if (nmaquina == 8)
                        {
                            if (m8 <= 1)
                            {
                                MessageBox.Show("La maquina: " + _reader.GetString(2) + " acaba de llegar a las 500 horas");
                            }
                            m8++;
                        }
                        else if (nmaquina == 9)
                        {
                            if (m9 <= 1)
                            {
                                MessageBox.Show("La maquina: " + _reader.GetString(2) + " acaba de llegar a las 500 horas");
                            }
                            m9++;
                        }
                        else if (nmaquina == 10)
                        {
                            if (m10 <= 1)
                            {
                                MessageBox.Show("La maquina: " + _reader.GetString(2) + " acaba de llegar a las 500 horas");
                            }
                            m10++;
                        }



                    }
                }
                suma = (Int32)Suma;
            }
            catch
            {
                suma = 0;
            }
            return suma;
        }

        //private void mensajenotifica(String maquinades)
        //{
        //    MessageBox.Show("La maquina: " + maquinades + " acaba de llegar a las 500 horas");
        //}
    }
}
