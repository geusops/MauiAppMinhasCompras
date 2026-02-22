using MauiAppMinhasCompras.Models;
using SQLite;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;
        public SQLiteDatabaseHelper(string path) {
            //conexao com o banco de dados (no arquivo de texto) baseado no caminho do arquivo (path)
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }
        public Task<int> Insert(Produto p) {
            return _conn.InsertAsync(p);
        }
        //usando task/list pois é o retorno da queryasyn.
        //Nao entendi pq ele escolheu o query ao inves do update
        public Task<List<Produto>> Update(Produto p) {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE ID=?";
            return _conn.QueryAsync<Produto>(sql, p.Descricao, p.Quantidade, p.preco, p.Id);
        }

        public Task<int> Delete(int id) {
            //expressao lambda para deletar o produto com o id passado como parametro
            //vai deletar o produto que tiver o id igual ao id passado como parametro
            return _conn.Table<Produto>().DeleteAsync(i  => i.Id == id);
        }

        //tbm vai retornar uma lista com os produtos
        public Task<List<Produto>> GetAll() {
            return _conn.Table<Produto>().ToListAsync();
        }
        //buscar o produto que tiver a descricao parecida com a string "q"  passada como parametro
        public Task<List<Produto>> Search(string q){
            //string de consulta no banco
            string sql = "SELECT * FROM Produto WHERE descricao LIKE '%" + q + "%'";
            //fazendo a consulta no bd com a string sql e retornando a lista de produtos encontrados
            return _conn.QueryAsync<Produto>(sql);
        }
    }//close class
}//close namespace
