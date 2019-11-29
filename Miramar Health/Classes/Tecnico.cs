using System;
using Miramar_Health.Classes;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Miramar_Health.Classes
{
    public class Tecnico
    {
        /*------------------ Propriedades da classe Tecnico -------------------*/
        public int Id_Tecnico { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Coren { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Situacao { get; set; }
        public string Celular { get; set; } 
        public Categoria Categoria { get; set; }
        
        Banco db;
        /*------------------ Métodos Construtores da classe Tecnico -------------*/
        public Tecnico(int id, string nome, string cpf,  string coren, string email, string senha, bool situacao, string celular, Categoria categoria)
        {
            Id_Tecnico = id;
            Nome = nome;
            Cpf = cpf;
            Coren = coren;
            Email = email;
            Senha = senha;
            Situacao = situacao;
            Celular = celular;
            Categoria = categoria;
            
        }
        public Tecnico()
        {
        }
        /*------------------- Métodos da classe Tecnico -------------------------*/

        /*------------------- Inseririndo Usuarios -------------------*/
        public void InserirTecnico(string nome, string cpf, string coren, string email, string senha, bool situacao, string celular, Categoria categoria)
        {
            db = new Banco();
            var comm = db.Conectar();
            try
            {
                if (comm.Connection.State == ConnectionState.Open)
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.CommandText = "sp_insert_tecnico";
                    comm.Parameters.Add("nome", MySqlDbType.VarChar).Value = nome;
                    comm.Parameters.Add("cpf", MySqlDbType.VarChar).Value = cpf;
                    comm.Parameters.Add("coren", MySqlDbType.VarChar).Value = coren;
                    comm.Parameters.Add("email", MySqlDbType.VarChar).Value = email;
                    comm.Parameters.Add("senha", MySqlDbType.VarChar).Value = senha;
                    comm.Parameters.Add("situacao", MySqlDbType.VarChar).Value = situacao;
                    comm.Parameters.Add("celular", MySqlDbType.VarChar).Value = celular;
                    comm.Parameters.Add("id_categoria", MySqlDbType.Int32).Value = categoria.Id_Categoria;
                    Id_Tecnico = Convert.ToInt32(comm.ExecuteScalar());
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
        /*Método de alterar Tecnico utilizando procedure*/
        public bool AlterarTecnico( int id, string nome, string email, string senha)
        {
            db = new Banco();
            var comm = db.Conectar();
            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "sp_update_tecnico";
                comm.Parameters.Add("id", MySqlDbType.Int32).Value = id;
                comm.Parameters.Add("nome", MySqlDbType.VarChar).Value = nome;
                comm.Parameters.Add("email", MySqlDbType.VarChar).Value = email;
                comm.Parameters.Add("senha", MySqlDbType.VarChar).Value = senha;
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
        public bool LogarTecnico(string _email, string _senha)
        {
            db = new Banco();
            var comm = db.Conectar();
            try
            {
                comm.CommandText = "select * from tecnico where email = '" + _email + "' and senha = '" + _senha + "'";
                var dr = comm.ExecuteReader();
                
                
                    if (dr.Read())
                    {
                        Id_Tecnico = dr.GetInt32(0);
                        Nome = dr.GetString(1);
                        Email = dr.GetString(4);
                        Senha = dr.GetString(5);
                        Situacao = dr.GetBoolean(6);
                        return true;
                    }
                    else
                    {
                        return false;
                    }

          
                
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

    }
}