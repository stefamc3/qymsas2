using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace QYMSAS
{
    class basededatos
    {

        public static MySqlConnection ObtenerConexion()
        {
            MySqlConnection conectar = new MySqlConnection("server=mysql.freehostia.com; database=qymqym_bd; Uid=qymqym_bd; pwd=qymsas3103369882;");
            try
            {
                conectar.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return conectar;
        }

        public static String ConsultaStatusUsuario(String usuario, String contraseña)
        {
            String status = "0";
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("select estado, usuario, contraseña from usuarios where (usuario='{0}' and contraseña= md5('{1}') )", usuario, contraseña), basededatos.ObtenerConexion());
                MySqlDataReader leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    status = leer.GetString(0);
                }
            }
            catch
            {
                MessageBox.Show("Ingreso invalido, verifique el usuario y la contraseña");
                status = "0";
            }
            return status;
        }

        public static Int32 ConsultaTipoUsuario(String usuario, String contraseña)
        {
            Int32 tipo = 0;
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("select tipo, usuario, contraseña from usuarios where (usuario='{0}' and contraseña= md5('{1}') )", usuario, contraseña), basededatos.ObtenerConexion());
                tipo = (Int32)comando.ExecuteScalar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                tipo = 3;
            }
            return tipo;
        }

        public static Int32 ObtenerIDusuario(String usuario, String contraseña)
        {

            Int32 Id = 0;
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("select id_us, usuario, contraseña from usuarios where (usuario='{0}' and contraseña= md5('{1}') )", usuario, contraseña), basededatos.ObtenerConexion());
                Id = (int)comando.ExecuteScalar();

            }
            catch (Exception)
            {
                Id = 0;
            }
            return Id;
        }


        public static int RegistrarUsuario(String nombre, String apellido, String usuario, String contraseña, String email, String tipo)
        {
            int registro;
            try
            {
                String estado = "Habilitado";
                MySqlCommand comando = new MySqlCommand(String.Format("insert into usuarios(nombre,apellido,usuario,contraseña,email,estado,tipo) values ('{0}','{1}','{2}',md5('{3}'),'{4}','{5}','{6}')", nombre, apellido, usuario, contraseña, estado, email, tipo), basededatos.ObtenerConexion());
                registro = comando.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Ingreso invalido, verifique los datos");
                registro = 0;
            }
            return registro;
        }

        public static int Modificarcontraseña(String usuario, String contraseña)
        {
            int registro = 1;
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("update usuarios set contraseña = md5('{0}') where usuario='{1}'", contraseña, usuario), ObtenerConexion());
                registro = comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                registro = 0;
            }
            return registro;
        }

        public static int sumaporplaca(String fechainicial, String fechafinal)
        {
            int suma;
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("select sum(precio) from remisiones where (fecha between '{0}' and '{1}')", fechainicial, fechafinal), basededatos.ObtenerConexion());
                MySqlDataReader _reader = comando.ExecuteReader();
                Int64 Suma = 0;
                while (_reader.Read())
                {
                    Suma = _reader.GetInt32(0);
                }
                suma = (Int32)Suma;
            }
            catch
            {
                suma = 0;
            }
            return suma;
        }

        public static int sumametrosremision(String fechainicial, String fechafinal)
        {
            int suma;
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("select sum(metros) from remisiones where (fecha between '{0}' and '{1}')", fechainicial, fechafinal), basededatos.ObtenerConexion());
                MySqlDataReader _reader = comando.ExecuteReader();
                Int64 Suma = 0;
                while (_reader.Read())
                {
                    Suma = _reader.GetInt32(0);
                }
                suma = (Int32)Suma;
            }
            catch
            {
                suma = 0;
            }
            return suma;
        }

        public static int sumamegastosadministrativos(String fechainicial, String fechafinal)
        {
            int suma;
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("select sum(neto_rec) from nomina where (fecha between '{0}' and '{1}')", fechainicial, fechafinal), basededatos.ObtenerConexion());
                MySqlDataReader _reader = comando.ExecuteReader();
                Int64 Suma = 0;
                while (_reader.Read())
                {
                    Suma = _reader.GetInt32(0);
                }
                suma = (Int32)Suma;
            }
            catch (Exception)
            {

                suma = 0;
            }
            return suma;
        }
        public static int sumamegastosoperativos(String fechainicial, String fechafinal)
        {
            int suma;
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("select sum(neto) from facturacion where (fecha between '{0}' and '{1}')", fechainicial, fechafinal), basededatos.ObtenerConexion());
                MySqlDataReader _reader = comando.ExecuteReader();
                Int64 Suma = 0;
                while (_reader.Read())
                {
                    Suma = _reader.GetInt32(0);
                }
                suma = (Int32)Suma;
            }
            catch
            {
                suma = 0;
            }
            return suma;
        }

        public static DataTable ConsultaGeneral(string Q)
        {
            //MySqlConnection conectar = new MySqlConnection("server=localhost; database=qymsas; Uid=root; pwd=;");
            MySqlDataAdapter DA = new MySqlDataAdapter(Q, ObtenerConexion());
            DataTable TB = new DataTable();
            try
            {
                //conectar.Open();
                DA.Fill(TB);
                //conectar.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                //conectar.Close();
            }
            return TB;


        }

        public static int ModificaDiscriminacion(String descripcion, String iddiscriminacion)
        {
            int registro = 1;
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("update discriminacion set descripcion = '{0}' where id_discriminacion='{1}'", descripcion, iddiscriminacion), ObtenerConexion());
                registro = comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                registro = 0;
            }
            return registro;
        }
        public static int ModificaCombustible(String valor, String idcombustible)
        {
            int registro = 1;
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("update combustible set Valor_Total = '{0}' where id_combustible='{1}'", valor, idcombustible), ObtenerConexion());
                registro = comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                registro = 0;
            }
            return registro;

        }
        public static int ModificaCuentasdeCobro(String descripcion, String idcuentas_de_cobro)
        {
            int registro = 1;
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("update cuentas_de_cobro set Descripcion = '{0}' where id_cuentasC='{1}'", descripcion, idcuentas_de_cobro), ObtenerConexion());
                registro = comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                registro = 0;
            }
            return registro;

        }
        public static int ModificaHoras(String horas, String maquina)
        {
            int registro = 1;
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("update horas set Num_Horas = '{0}' where id_horas ='{1}'", horas, maquina), ObtenerConexion());
                registro = comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                registro = 0;
            }
            return registro;

        }
        public static int ModificaMovimientos(String patio, String id_movimientos)
        {
            int registro = 1;
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("update movimientos_foton2 set patio = '{0}' where id_movimientos ='{1}'", patio, id_movimientos), ObtenerConexion());
                registro = comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                registro = 0;
            }
            return registro;

        }
        public static int ModificaContraseña(String contraseña, String id_us)
        {
            int registro = 1;
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("update usuarios set contraseña = '{0}' where id_us ='{1}'", contraseña, id_us), ObtenerConexion());
                registro = comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                registro = 0;
            }
            return registro;

        }
        public static int ModificaEgreso(String descripcion, String id_comprobante)
        {
            int registro = 1;
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("update greso set descripcion = '{0}' where id_comprobante='{1}'", descripcion, id_comprobante), ObtenerConexion());
                registro = comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                registro = 0;
            }
            return registro;

        }
        public static int ModificaFacturas(String descripcion, String id_facturacion)
        {
            int registro = 1;
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("update facturacion set Descripcion = '{0}' where id_facturacion='{1}'", descripcion, id_facturacion), ObtenerConexion());
                registro = comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                registro = 0;
            }
            return registro;

        }
        public static int ModificaNomina(String descripcion, String id_nomina)
        {
            int registro = 1;
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("update nomina set neto_rec = '{0}' where id_nomina='{1}'", descripcion, id_nomina), ObtenerConexion());
                registro = comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                registro = 0;
            }
            return registro;

        }
        public static int ModificaProduccion(String cliente, String id_produccion_acopio)
        {
            int registro = 1;
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("update produccion_acopio set cliente_A= '{0}' where id_produccion_acopio='{1}'", cliente, id_produccion_acopio), ObtenerConexion());
                registro = comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                registro = 0;
            }
            return registro;

        }
        public static int ModificaReciboC(String pagado_a, String id_reciboC)
        {
            int registro = 1;
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("update recibo_de_caja set pagado_a= '{0}' where id_reciboC='{1}'", pagado_a, id_reciboC), ObtenerConexion());
                registro = comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                registro = 0;
            }
            return registro;
        }
        public static int ModificaRemisiones(String destino, String id_remisiones)
        {
            int registro = 1;
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("update remisiones set destino = '{0}' where id_remisiones ='{1}'", destino, id_remisiones), ObtenerConexion());
                registro = comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                registro = 0;
            }
            return registro;
        }
        public static int ModificaRetro3(String destino, String id_produccion_R3)
        {
            int registro = 1;
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("update produccion_r3 set destino_p = '{0}' where id_produccion_R3 ='{1}'", destino, id_produccion_R3), ObtenerConexion());
                registro = comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                registro = 0;
            }
            return registro;
        }
        public static int ModificaFletes(String destino, String id_fletes)
        {
            int registro = 1;
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("update fletes set destino= '{0}' where id_fletes ='{1}'", destino, id_fletes), ObtenerConexion());
                registro = comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                registro = 0;
            }
            return registro;
        }
        public static int ModificaTrabajadores(String nombre_trabajador, String id_trabajadores)
        {
            int registro = 1;
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("update trabajadores set nombre_trabajor= '{0}' where id_trabajadores ='{1}'", nombre_trabajador, id_trabajadores), ObtenerConexion());
                registro = comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                registro = 0;
            }
            return registro;
        }
        public static int ModificaViaticos(String id_maquina, String id_viaticos)
        {
            int registro = 1;
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("update viaticos set id_maquina= '{0}' where id_viaticos ='{1}'", id_maquina, id_viaticos), ObtenerConexion());
                registro = comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                registro = 0;
            }
            return registro;
        }
        public static String ConsultanombresUsuario(int usuario)
        {
            String status = "0";
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("select concat(nombre,' ' , apellido) from usuarios where id_us='{0}';", usuario), basededatos.ObtenerConexion());
                MySqlDataReader leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    status = leer.GetString(0);
                }
            }
            catch
            {
                status = "0";
            }
            return status;
        }
    }
}
    

