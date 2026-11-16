namespace MauiAppEventos.Views;

public partial class ConfirmacaoCadastro : ContentPage
{
	public ConfirmacaoCadastro()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		try
		{
			await Navigation.PopAsync();

		} catch (Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "OK");
		}
    }
}