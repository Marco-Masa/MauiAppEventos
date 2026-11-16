using MauiAppEventos.Models;
using System.Globalization;

namespace MauiAppEventos.Views;

public partial class CadastroEvento : ContentPage
{
    public CadastroEvento()
    {
        InitializeComponent();

        DataInicio.MinimumDate = DateTime.Today;
        DataInicio.MaximumDate = DateTime.Today.AddMonths(1);

        DataTermino.MinimumDate = DataInicio.Date.AddDays(1);
        DataTermino.MaximumDate = DataInicio.Date.AddMonths(6);
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Valida Campo Nome - required
            if (string.IsNullOrWhiteSpace(Nome.Text))
            {
                await DisplayAlert("Campo Obrigatório", "Por favor, informe o nome do evento.", "OK");

                Nome.Focus();

                return;
            }

            // Valida Campo Local - required
            if (string.IsNullOrWhiteSpace(Local.Text))
            {
                await DisplayAlert("Campo Obrigatório", "Por favor, informe o local do evento.", "OK");

                Local.Focus();

                return;
            }

            // Valida campo Valor - required e maior que zero
            if (string.IsNullOrWhiteSpace(Valor.Text) ||
                !double.TryParse(Valor.Text, NumberStyles.Currency, new CultureInfo("pt-BR"), out double valorConvertido) ||
                valorConvertido <= 0)
            {
                await DisplayAlert("Campo Obrigatório", "Por favor, informe um valor por participante maior que R$ 0,00.", "OK");

                Valor.Focus();

                return;
            }

            // Objeto Evento
            Evento evento = new Evento
            {
                Nome = Nome.Text.Trim(),
                DataInicio = DataInicio.Date,
                DataTermino = DataTermino.Date,
                Participantes = Convert.ToInt32(Participantes.Value),
                Local = Local.Text.Trim(),
                Valor = valorConvertido
            };

            // Objeto BindingContext
            await Navigation.PushAsync(new ConfirmacaoCadastro()
            {
                BindingContext = evento
            });
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", "Ops! Ocorreu um erro ao cadastrar o evento: " + ex.Message, "OK");
        }
    }

    private void DataInicio_DateSelected(object sender, DateChangedEventArgs e)
    {
        if (sender is DatePicker elemento)
        {
            DateTime dataSelecionadaInicio = elemento.Date;

            DataTermino.MinimumDate = dataSelecionadaInicio.AddDays(1);
            DataTermino.MaximumDate = dataSelecionadaInicio.AddMonths(6);

            // Garante Data Término maior que a Data Minima
            if (DataTermino.Date < DataTermino.MinimumDate)
            {
                DataTermino.Date = DataTermino.MinimumDate;
            }
        }
    }
}