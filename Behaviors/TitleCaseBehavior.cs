using System.Globalization;

namespace MauiAppEventos.Behaviors;

public class TitleCaseBehavior : Behavior<Entry>
{
    private bool _isUpdating;

    protected override void OnAttachedTo(Entry entry)
    {
        entry.TextChanged += OnEntryTextChanged;
        base.OnAttachedTo(entry);
    }

    protected override void OnDetachingFrom(Entry entry)
    {
        entry.TextChanged -= OnEntryTextChanged;
        base.OnDetachingFrom(entry);
    }

    private void OnEntryTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (_isUpdating || sender is not Entry entry || string.IsNullOrEmpty(e.NewTextValue))
            return;

        _isUpdating = true;

        int cursorPosition = entry.CursorPosition;
        TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;

        // Converte a primeira letra de cada palavra para maiúscula
        string formattedText = textInfo.ToTitleCase(e.NewTextValue.ToLower());

        if (entry.Text != formattedText)
        {
            entry.Text = formattedText;
            // Mantém o cursor na posição onde o usuário estava digitando
            entry.CursorPosition = Math.Min(cursorPosition, entry.Text.Length);
        }

        _isUpdating = false;
    }
}
