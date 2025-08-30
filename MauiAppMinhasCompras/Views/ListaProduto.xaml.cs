using System.Collections.ObjectModel;
using System.Diagnostics;
using MauiAppMinhasCompras.Models;
using MauiAppMinhasCompras.Helpers;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
     


    private SQLiteDatabaseHelper _db;
    ObservableCollection<Produto> Lista = new ObservableCollection<Produto>();

    

    public ListaProduto()
    {
        InitializeComponent();

        Lst_produtos.ItemsSource = Lista;

       // string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "meu_banco.db3");
       // _db = new SQLiteDatabaseHelper(path);

        
    }

    protected async override void OnAppearing()
    {

        List<Produto> tmp = await App.DB.GetAll();
        tmp.ForEach(i => Lista.Add(i));

    }

    private async Task CarregarProdutos()
    {
        Lista.Clear();
        List<Produto> tmp = await _db.GetAll();
        tmp.ForEach(p => Lista.Add(p));
    }

    private async void MenuItem_Remover(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var itemSelecionado = menuItem?.CommandParameter as Produto;

        if (itemSelecionado != null)
        {
            bool confirmar = await DisplayAlert("Remover", $"Deseja remover '{itemSelecionado.Descrição}'?", "Sim", "Não");
            if (confirmar)
            {
                await App.DB.Delete(itemSelecionado);

                var itemNaLista = Lista.FirstOrDefault(p => p.Id == itemSelecionado.Id);
                if (itemNaLista != null)
                {
                    Lista.Remove(itemNaLista);
                }
            }
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        string q = e.NewTextValue;
        Lista.Clear();

        List<Produto> tmp = await App.DB.Search(q);
        tmp.ForEach(i => Lista.Add(i));
    }

    private void ToolbarItem_Somar(object sender, EventArgs e)
    {
        double soma = Lista.Sum(p => p.Total);
        string msg = $"O total é {soma:C}";

        DisplayAlert("Total dos Produtos", msg, "OK");
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
