using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;
using System.Data;


namespace Miramar_Health
{
    public class Banco
    {
        MySqlConnectionStringBuilder b = new MySqlConnectionStringBuilder();
        public Banco()
        { }
        public MySqlCommand Conectar()
        {
            MySqlCommand comm;
            b.Server = "127.0.0.1";
            b.UserID = "root";
            b.Database = "enfermagemdb";
            b.Port = 3306;
            b.Password = "usbw";
            try
            {
                MySqlConnection cn = new MySqlConnection(b.ToString());
                cn.Open();
                comm = new MySqlCommand
                {
                    Connection = cn
                };
                return comm;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }

        }
    }
}