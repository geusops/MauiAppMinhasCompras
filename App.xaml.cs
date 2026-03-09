using MauiAppMinhasCompras.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        //criacao de um campo privado
        static SQLiteDatabaseHelper _db;

        //criacao de uma propriedade publica
        public static SQLiteDatabaseHelper Db
        {
            
            get {
                if (_db == null)
                {
                    string path = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "banco_sqlite_compra_db3");
                    _db = new SQLiteDatabaseHelper(path);
                }
                return _db; } 
        }
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }//close class
}//close namespace