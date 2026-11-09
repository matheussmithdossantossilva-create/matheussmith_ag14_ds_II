namespace AppHotel2.Models
{
    public class Reserva
    {
        public Hospede Hospede { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
        public int QuantidadeHospedes { get; set; }

        public Reserva(
            Hospede hospede,
            DateTime entrada,
            DateTime saida,
            int quantidade)
        {
            Hospede = hospede;
            DataEntrada = entrada;
            DataSaida = saida;
            QuantidadeHospedes = quantidade;
        }
    }
}