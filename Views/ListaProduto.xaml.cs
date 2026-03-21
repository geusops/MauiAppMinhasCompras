using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    //criando uma observable collection chamada lista
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    public ListaProduto()
    {
        InitializeComponent();
        //inicializando a lista
        lst_produtos.ItemsSource = lista;
    }

    //criando um metodo progido para popular a tabela sempre que houve uma mudanca de tela
    protected override async void OnAppearing()
    {

        try
        {
            lista.Clear();
            base.OnAppearing();
            //instanciando uma lista de produtos e chamando de tmp e populando com a conexao com bd no metodo getall (que criamos na model)
            List<Produto> tmp = await App.Db.GetAll();
            //populando as 'table roles' com o resultado do getall atravez do foreach
            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        //navegando para a outra pagina
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }

    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        string q = e.NewTextValue;
        //limpando o objeto lista antes de fazer a busca
        lista.Clear();

        try
        {
            List<Produto> tmp = await App.Db.Search(q);
            //populando as 'table roles' com o resultado do getall atravez do foreach
            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        //aqui eu olho todos os items da minha lista e somo todos os do campo Total
        double soma = lista.Sum(i => i.Total);

        string msg = $"O total é {soma:C}";

        DisplayAlert("Total de protudos", msg, "OK");

    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            /*
            var objetoLista = (Produto)((MenuItem)sender).BindingContext;
            await App.Db.Delete(objetoLista.Id);
            //instanciando uma lista de produtos e chamando de tmp e populando com a conexao com bd no metodo getall (que criamos na model)
            List<Produto> tmp = await App.Db.GetAll();
            //populando as 'table roles' com o resultado do getall atravez do foreach
            tmp.ForEach(i => lista.Add(i));
            */
            MenuItem selecionado = sender as MenuItem;

            Produto p = selecionado.BindingContext as Produto;

            bool confirma = await DisplayAlert("Tem certeza?", $"Remover {p.Descricao}?", "Sim", "Não");

            if (confirma)
            {
                await App.Db.Delete(p.Id);
                lista.Remove(p);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }

    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try {
            Produto p = e.SelectedItem as Produto;
            Navigation.PushAsync(new Views.EditarProtudo
            {
                BindingContext = p,
            });
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}