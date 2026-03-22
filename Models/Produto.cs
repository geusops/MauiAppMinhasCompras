using SQLite;

namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {
        String _descricao;
        double _quantidade = 1;
        double _preco = 1;


        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao
        {
            get => _descricao;
            set
            {
                if (value == null)
                {
                    throw new Exception("Favor, preencha a descrição");
                }
                _descricao = value;

            }
        }
        public double Quantidade
        {
            get => _quantidade;
            set
            { 
                if (value >= 1)
                {
                    _quantidade = value;
                }
            }
        }
        public double Preco
        {
            get => _preco;
            set
            {
                if (value >= 1)
                {
                    _preco = value;

                }
            }
        }
        public double Total { get => Quantidade * Preco; }
    }
}
