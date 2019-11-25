using SQLite;

namespace Miramar_Health.Classes
{
    public class SessaoTecnico
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Tecnico { get; set; }
        public bool Sessao { get; set; }
        public int Nivel { get; set; }
    }
}