using System.Globalization;

namespace MauiAppEventos.Behaviors;

public class CurrencyBehavior : Behavior<Entry>
{
    private bool _isUpdating;

    protected override void OnAttachedTo(Entry entry)
    {
        entry.TextChanged += OnEntryTextChanged;
        entry.Keyboard = Keyboard.Numeric;
        base.OnAttachedTo(entry);
    }

    protected override void OnDetachingFrom(Entry entry)
    {
        entry.TextChanged -= OnEntryTextChanged;
        base.OnDetachingFrom(entry);
    }

    private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (_isUpdating || sender is not Entry entry)
            return;

        _isUpdating = true;

        // Remove tudo que não for dígito
        string digits = new string(e.NewTextValue?.Where(char.IsDigit).ToArray() ?? Array.Empty<char>());

        if (string.IsNullOrEmpty(digits))
        {
            entry.Text = "R$ 0,00";
        }
        else
        {
            // Converte para decimal dividindo por 100 para obter os centavos
            decimal value = decimal.Parse(digits) / 100m;
            entry.Text = value.ToString("C2", new CultureInfo("pt-BR"));
        }

        // Mantém o cursor sempre no final do texto
        entry.CursorPosition = entry.Text.Length;
        _isUpdating = false;
    }
}
