using MauiAppMinhasCompras.Models;
using SQLite;


namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper    {
        readonly SQLiteAsyncConnection _conn;
        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }
         public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }

        public Task<List<Produto>> Update(Produto p)
        {
            string sql = "UPDATE Produto SET Descrição=?, Quantidade =?, Preço=? WHERE Id=?";

            return _conn.QueryAsync<Produto>(
                sql, p.Descrição, p.Quantidade, p.Preço, p.Id                
                );
                    }

       public Task<int> Delete(Produto produto) 
        {
           // return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
           // return _conn.Table<Produto>().DeleteAsync(Produto.id);
           // return Delete(ItemSelecionado);
          return _conn.DeleteAsync(produto);

        }

        public Task<int> DeleteById(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(p => p.Id == id);
        }

        public Task<int> DeleteAsync(Produto itemSelecionado) 
        {
            return Delete(itemSelecionado);

        }


        public Task<List<Produto>> GetAll() 
        {
            return _conn.Table<Produto>().ToListAsync();   
        }

        public Task<List<Produto>> Search(string q) 
        {
            string sql = "SELECT * FROM produto WHERE Descrição LIKE '%" + q + "%'" ;

            return _conn.QueryAsync<Produto>(sql);
        }

        
    }
   
}
