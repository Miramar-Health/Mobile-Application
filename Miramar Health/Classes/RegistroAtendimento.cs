using System;
using Miramar_Health.Classes;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;


namespace Miramar_Health.Classes
{
    public class RegistroAtendimento
    {
       
        public int Id_atendimento { get; set; }
        public byte[] Img_ferida { get; set; }
        public string Descricao_ferida { get; set; }
        public DateTime Data_atendimento { get; set; }
        public string Cobertura_realizada { get; set; }
        public Tecnico Tecnico{ get; set; }
        public Paciente Paciente { get; set; }
        public TipodeFerida TipodeFerida { get; set; }
        Banco db;


        public RegistroAtendimento()
        {

        }
        public RegistroAtendimento(int id_atendimento, byte[] img_ferida, string descricao_ferida, DateTime data_atendimento, string cobertura_realizada, Tecnico tecnico, Paciente paciente, TipodeFerida tipoferida)
        {
            Id_atendimento = id_atendimento;
            Img_ferida = img_ferida;
            Descricao_ferida = descricao_ferida;
            Data_atendimento = data_atendimento;
            Cobertura_realizada = cobertura_realizada;
            Tecnico = tecnico;
            Paciente = paciente;
            TipodeFerida = tipoferida;
            
        }

        public RegistroAtendimento( byte[] img_ferida, string descricao_ferida, DateTime data_atendimento, string cobertura_realizada, Tecnico tecnico, Paciente paciente, TipodeFerida tipoferida)
        {
            Img_ferida = img_ferida;
            Descricao_ferida = descricao_ferida;
            Data_atendimento = data_atendimento;
            Cobertura_realizada = cobertura_realizada;
            Tecnico = tecnico;
            Paciente = paciente;
            TipodeFerida = tipoferida;
        }
         // Métdos da Classe

        //Inserir

        public void InserirRegistroAtendimento(byte[] img_ferida, string descricao_ferida, DateTime data_atendimento, string cobertura_realizada, int tecnico, int paciente, int tipoferida)
        {
            db = new Banco();
            var comm = db.Conectar();
            try
            {
                if (comm.Connection.State == ConnectionState.Open)
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.CommandText = "sp_insert_registro_atendimento";
                    comm.Parameters.Add("img_ferida", MySqlDbType.LongBlob).Value = img_ferida;
                    comm.Parameters.Add("descricao_ferida", MySqlDbType.VarChar).Value = descricao_ferida;
                    comm.Parameters.Add("data_atendimento", MySqlDbType.DateTime).Value = data_atendimento;
                    comm.Parameters.Add("cobertura_realizada", MySqlDbType.VarChar).Value = cobertura_realizada;
                    comm.Parameters.Add("id_tecnico", MySqlDbType.Int32).Value = tecnico;
                    comm.Parameters.Add("id_paciente", MySqlDbType.Int32).Value = paciente;
                    comm.Parameters.Add("id_ferida", MySqlDbType.Int32).Value = tipoferida;
                    Id_atendimento = Convert.ToInt32(comm.ExecuteScalar());
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