using System;
using Miramar_Health.Classes;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Miramar_Health.Classes
{
    public class Paciente
    {
        public int Id_paciente { get; set; }
        public string Nome { get; set; }
        public string Local_ferida { get; set; }
        public string Descricao_inicial_ferida { get; set; }
        public DateTime Data_cadastro { get; set; }
        public Endereco End { get; set; }
        Banco db= new Banco();


        public Paciente ()
        { }

        public Paciente(int id, string nome, string local_ferida, string descricao_ferida, DateTime data_cadastro)
        {
            Id_paciente = id;
            Nome = nome;
            Local_ferida = local_ferida;
            Descricao_inicial_ferida = descricao_ferida;
            Data_cadastro = data_cadastro;
        }
        public void InserirPaciente(string nome, string local_ferida, string descricao_ferida, DateTime data_cadastro)
        {
            db = new Banco();
            var comm = db.Conectar();
            try
            {
                if (comm.Connection.State == ConnectionState.Open)
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.CommandText = "sp_inserir_paciente";
                    comm.Parameters.Add("nome", MySqlDbType.VarChar).Value = nome;
                    comm.Parameters.Add("local_ferida", MySqlDbType.VarChar).Value = local_ferida;
                    comm.Parameters.Add("descricao_inicial_ferida", MySqlDbType.VarChar).Value = descricao_ferida;
                    comm.Parameters.Add("data_cadastro", MySqlDbType.DateTime).Value = data_cadastro;
                    Id_paciente = Convert.ToInt32(comm.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                if (comm.Connection.State == ConnectionState.Closed)
                    comm.Connection.Close();
            }
        }
        public bool AlterarPaciente(int id, string nome, Endereco endereco)
        {
            var comm = db.Conectar();
            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "sp_update_paciente";
                comm.Parameters.Add("id", MySqlDbType.Int32).Value = id;
                comm.Parameters.Add("nome", MySqlDbType.VarChar).Value = nome;
                comm.Parameters.Add("id_end", MySqlDbType.Int32).Value = endereco;
                comm.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
            finally
            {
                comm.Connection.Close();
            }
        }
        public void ConsultarPaciente(string nome)
        {
            var comm = db.Conectar();
            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "select * from Pacientes where nome =" + nome;
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Id_paciente = dr.GetInt32(0);
                    Nome = dr.GetString(1);
                    Local_ferida = dr.GetString(2);
                    Descricao_inicial_ferida = dr.GetString(3);
                    Data_cadastro = dr.GetDateTime(4);
                    End.Id_endereco = dr.GetInt32(5);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                comm.Connection.Close();
            }
        }
        public List<Paciente> GerarLista()
        {
            List<Paciente> lista = new List<Paciente>();
            var comm = db.Conectar();
            try
            {
                comm.CommandText = "select * from paciente";
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Paciente p = new Paciente
                    {
                        Id_paciente = dr.GetInt32(0),
                        Nome = dr.GetString(1),
                        Local_ferida = dr.GetString(2),
                        Descricao_inicial_ferida = dr.GetString(3),
                        Data_cadastro = dr.GetDateTime(4)
                    };

                    lista.Add(p);
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