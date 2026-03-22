using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txt_descricao.Text))
            {
                await DisplayAlert("Ops", "A descrição não pode estar vazia", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(txt_quantidade.Text))
            {
                await DisplayAlert("Ops","A quantidade deve ser ao menos 1","OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(txt_preco.Text))
            {
                await DisplayAlert("Ops", "O preço deve ser maior que 0", "OK");
                return;
            }

            Produto p = new Produto
            {
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text)
            };
            await App.Db.Insert(p);
            await DisplayAlert("Sucesso!", "Registro Inserido!", "OK");
            //Apos inserir, voltar pra tela inicial
            Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}