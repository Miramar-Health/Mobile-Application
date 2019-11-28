using System;
using Miramar_Health.Classes;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Miramar_Health.Classes
{
    public class Categoria
    {
        public int Id_Categoria { get; set; }
        public string Categoriaa { get; set; }
        Banco db;




        public Categoria()
        { }
        public Categoria(int id_Categoria, string categoria)
        {
            Id_Categoria = id_Categoria;
            Categoriaa = categoria;
        }

        public Categoria( string categoria)
        {
            Categoriaa = categoria;
        }


        //Gerar uma Lista de Categoria
        public List<Categoria> GerarLista()
        {
            List<Categoria> lista = new List<Categoria>();
            db = new Banco();
            var comm = db.Conectar();
            try
            {
                comm.CommandText = "select * from categoria";
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Categoria c = new Categoria()
                    {
                        Id_Categoria = dr.GetInt32(0),
                        Categoriaa = dr.GetString(1)
                    };
                    lista.Add(c);
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