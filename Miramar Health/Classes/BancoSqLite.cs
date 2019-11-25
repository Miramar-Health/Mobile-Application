using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace Miramar_Health.Classes
{
    public class BancoSqLite
    {
        readonly SQLiteAsyncConnection banco;
        public BancoSqLite(string dbPath)
        {
            banco = new SQLiteAsyncConnection(dbPath);
            banco.CreateTableAsync<SessaoTecnico>();
        }
        public Task<List<SessaoTecnico>> ObterListaSessao()
        {
            return banco.Table<SessaoTecnico>().ToListAsync();
        }
        public Task<List<SessaoTecnico>> ObterSessaoAberta()
        {
            return banco.QueryAsync<SessaoTecnico>("SELECT * FROM SessaoTecnico WHERE Sessao = 1");
        }
        public Task<List<SessaoTecnico>> ObterId(string nome)
        {
            return banco.QueryAsync<SessaoTecnico>("SELECT * FROM SessaoTecnico WHERE Tecnico = " + nome + "");
        }
        public Task<SessaoTecnico> ObterTecnico(string nome)
        {
            return banco.Table<SessaoTecnico>().Where(i => i.Tecnico == nome).FirstOrDefaultAsync();
        }
        public Task<int> InserirSessao(SessaoTecnico sessao)
        {
            return banco.InsertAsync(sessao);
        }
        public Task<int> AtualizarSessao(SessaoTecnico sessao)
        {
            return banco.UpdateAsync(sessao);
        }
        public Task<int> DeletarSessao(SessaoTecnico sessao)
        {
            return banco.DeleteAsync(sessao);
        }
        public Task<int> SalvarSessaoTecnico(SessaoTecnico item)
        {
            if (item.Id != 0)
            {
                return banco.UpdateAsync(item);
            }
            else
            {
                return banco.InsertAsync(item);
            }
        }

    }
}