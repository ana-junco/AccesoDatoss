using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Clases
{
    public static class Persona
    {
        private static string CadenaConex= @"server=DESKTOP-0IJTAIU\SQLEXPRESS; database=TDI2021; Integrated Security=true";

        public static int Insertar(string cedula, string apellidos, string nombres, DateTime fechaNacimiento, double peso)
        {
            SqlConnection conexion = new SqlConnection(CadenaConex);
            string sql = "insert into personas(Cedula, Apellidos, Nombres, FechaNacimiento, Peso) ";
            sql += "values (@Cedula, @Apellidos, @Nombres, @FechaNacimiento, @Peso)";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.Add(new SqlParameter("@Cedula", cedula));
            comando.Parameters.Add(new SqlParameter("@Apellidos", apellidos));
            comando.Parameters.Add(new SqlParameter("@Nombres", nombres));
            comando.Parameters.Add(new SqlParameter("@FechaNacimiento", fechaNacimiento));
            comando.Parameters.Add(new SqlParameter("@Peso", peso));
            conexion.Open();
            int res = comando.ExecuteNonQuery();
            conexion.Close();

            return res;
        }

        public static int Borrar(string cedula)
        {
            SqlConnection conexion = new SqlConnection(CadenaConex);
            string eliminar = "DELETE FROM personas WHERE cedula = @cedula";
            SqlCommand comando = new SqlCommand(eliminar, conexion);
            //3.1 configurar el parametro @cedula
            comando.Parameters.Add(new SqlParameter("@cedula", cedula));
            //3.2 abrir la conexion 
            conexion.Open();
            //3.3 Insertar el registro en la Base de datos
            int res = comando.ExecuteNonQuery();
            //4 Cerrar la conexion
            conexion.Close();
            return res;
        }

        public static DataTable getpersona()
        {
            SqlConnection conexion = new SqlConnection(CadenaConex);
            string sql = "";
            sql = "select cedula as Cédulas, upper(apellido+ ' ' + nombre) as [Nombres Completos], fechadenacimiento as [Fechas de nacimiento], peso as Peso ";
            sql += "from personas order by apellidos, nombres";
            SqlCommand comando = new SqlCommand(sql, conexion);
            SqlDataAdapter ad1 = new SqlDataAdapter(comando);

            //pasar los datos del adaptador a un datatable
            DataTable dt = new DataTable();
            ad1.Fill(dt);
            return dt;
        }

    }

}

