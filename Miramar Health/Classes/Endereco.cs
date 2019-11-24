using System;
using Miramar_Health.Classes;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;


namespace Miramar_Health.Classes
{
    public class Endereco
    {
        public int Id_endereco { get; set; }
        public Paciente Id_paciente { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }


        public Endereco() 
        { }


        public Endereco(int id, Paciente id_paciente, string rua, string bairro, string cidade, string estado)
        {
            Id_endereco = id;
            Id_paciente = id_paciente;
            Rua = rua;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
        }

        public void CadastarEndereco(Paciente id_paciente, string rua, string bairro, string cidade, string estado)
        {
            var db = new Banco();
            var comm = db.Conectar();
            try
            {
                if (comm.Connection.State == ConnectionState.Open)
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.CommandText = "sp_inserir_endereco";
                    comm.Parameters.Add("id_paciente", MySqlDbType.Int32).Value = id_paciente.Id_paciente;
                    comm.Parameters.Add("rua", MySqlDbType.VarChar).Value = rua;
                    comm.Parameters.Add("bairro", MySqlDbType.VarChar).Value = bairro;
                    comm.Parameters.Add("cidade", MySqlDbType.VarChar).Value = cidade;
                    comm.Parameters.Add("estado", MySqlDbType.VarChar).Value = estado;
                    Id_endereco = Convert.ToInt32(comm.ExecuteScalar());
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
    }
  
}