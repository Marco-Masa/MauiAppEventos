namespace MauiAppEventos.Models
{
    public class Evento
    {
        public string Nome { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public int Participantes { get; set; }
        public string Local { get; set; } = string.Empty;
        public double Valor { get; set; }

        // Calcula a Duração do Evento
        public TimeSpan Duracao
        {
            get => DataTermino - DataInicio;
        }

        // Formata a Duração do Evento ajustando o complemento "dia" ou "dias"
        public string DuracaoFormatada
        {
            get => Duracao.Days == 1 ? "1 dia" : $"{Duracao.Days} dias";
        }

        // Calcula o Custo Total
        public double CustoTotal
        {
            get => Valor * Participantes;
        }
    }
}
