using System;
using Miramar_Health.Classes;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Miramar_Health.Classes
{
    public class TipodeFerida
    {
        public int Id_Ferida { get; set; }
        public string Nome_Ferida { get; set; }
        Banco db;


        public TipodeFerida()
        { }

        public TipodeFerida(int id_Ferida, string nome_Ferida)
        {
            Id_Ferida = id_Ferida;
            Nome_Ferida = nome_Ferida;
        }
        public TipodeFerida( string nome_Ferida)
        {
           
            Nome_Ferida = nome_Ferida;
        }

        //Gerar uma Lista de Tipo de Ferida
        public List<TipodeFerida> GerarLista()
        {
            List<TipodeFerida> lista = new List<TipodeFerida>();
            db = new Banco();
            var comm = db.Conectar();
            try
            {
                comm.CommandText = "select * from tipo_ferida";
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    TipodeFerida tp = new TipodeFerida()
                    {
                        Id_Ferida = dr.GetInt32(0),
                        Nome_Ferida = dr.GetString(1)
                    };
                    lista.Add(tp);
                }
                comm.Connection.Close();
                return lista;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }

    }
}