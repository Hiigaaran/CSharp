using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace YoDigital.Models
{
    public class LoginTrabajador
    {
        public string Usuario { get; set; }
        public string Contraseña { get; set; }

        public bool Login(string username, string password)
        {
            Cloud cliente = new Cloud();

            DataSet usuario = new DataSet();

            usuario = cliente.Ws.Data_SQL_Query("select nombres from personas where rut = " + username, "YODIGITAL_NEW");

            //Verificacion de Usuario
            if (usuario.Tables.Count > 0 && usuario.Tables[0].Rows.Count != 0)
            {
                if (username.Substring(username.Length - 4).Equals(password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public string QueryUsuario(string username)
        {
            Cloud cliente = new Cloud();
            DataSet usuario = new DataSet();
            string nombre = string.Empty;
            usuario = cliente.Ws.Data_SQL_Query("select nombres from personas where rut = " + username, "YODIGITAL_NEW");

            foreach (DataTable dt in usuario.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    nombre = dr[0].ToString();
                }
            }
            return nombre;
        }
    }
}