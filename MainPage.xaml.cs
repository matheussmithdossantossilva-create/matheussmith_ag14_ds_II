using AppHotel2.Models;

namespace AppHotel2
{
    public partial class MainPage : ContentPage
    {
        private List<Reserva> reservas = new List<Reserva>();

        private int quantidadeHospedes = 1;

        public MainPage()
        {
            InitializeComponent();

            EntradaPicker.Date = DateTime.Today;
            SaidaPicker.Date = DateTime.Today.AddDays(1);
        }

        // AUMENTAR HÓSPEDES

        private void AumentarHospedes_Clicked(object sender, EventArgs e)
        {
            if (quantidadeHospedes < 10)
            {
                quantidadeHospedes++;
                QuantidadeLabel.Text = quantidadeHospedes.ToString();
            }
        }

        // DIMINUIR HÓSPEDES

        private void DiminuirHospedes_Clicked(object sender, EventArgs e)
        {
            if (quantidadeHospedes > 1)
            {
                quantidadeHospedes--;
                QuantidadeLabel.Text = quantidadeHospedes.ToString();
            }
        }

        // CADASTRAR RESERVA

        private async void CadastrarReserva_Clicked(object sender, EventArgs e)
        {
            string nome = NomeEntry.Text ?? "";
            string telefone = TelefoneEntry.Text ?? "";
            string email = EmailEntry.Text ?? "";

            if (string.IsNullOrWhiteSpace(nome) ||
                string.IsNullOrWhiteSpace(telefone) ||
                string.IsNullOrWhiteSpace(email))
            {
                await DisplayAlert(
                    "Atenção",
                    "Preencha todos os campos!",
                    "OK");

                return;
            }

            // CORREÇÃO DO DATETIME?

            DateTime entrada = EntradaPicker.Date.GetValueOrDefault(DateTime.Today);

            DateTime saida = SaidaPicker.Date.GetValueOrDefault(
                DateTime.Today.AddDays(1));

            if (entrada.Date < DateTime.Today)
            {
                await DisplayAlert(
                    "Erro",
                    "A entrada não pode ser anterior a hoje!",
                    "OK");

                return;
            }

            if (saida.Date <= entrada.Date)
            {
                await DisplayAlert(
                    "Erro",
                    "A saída deve ser posterior à entrada!",
                    "OK");

                return;
            }

            Hospede hospede = new Hospede(
                nome,
                telefone,
                email);

            Reserva reserva = new Reserva(
                hospede,
                entrada,
                saida,
                quantidadeHospedes);

            reservas.Add(reserva);

            AtualizarLista();

            await DisplayAlert(
                "Sucesso",
                "Reserva cadastrada com sucesso!",
                "OK");

            LimparCampos();
        }

        // ATUALIZAR LISTA

        private void AtualizarLista()
        {
            ListaReservas.Children.Clear();

            foreach (Reserva reserva in reservas)
            {
                Frame cartao = new Frame
                {
                    BackgroundColor = Colors.White,
                    BorderColor = Color.FromArgb("#D6A84F"),
                    CornerRadius = 12,
                    Padding = 15,
                    HasShadow = true
                };

                VerticalStackLayout conteudo = new VerticalStackLayout
                {
                    Spacing = 7
                };

                conteudo.Children.Add(new Label
                {
                    Text = reserva.Hospede.Nome,
                    FontSize = 20,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.FromArgb("#123B65")
                });

                conteudo.Children.Add(new Label
                {
                    Text = "Telefone: " + reserva.Hospede.Telefone,
                    TextColor = Colors.Black
                });

                conteudo.Children.Add(new Label
                {
                    Text = "E-mail: " + reserva.Hospede.Email,
                    TextColor = Colors.Black
                });

                conteudo.Children.Add(new Label
                {
                    Text = "Quantidade de hóspedes: " +
                           reserva.QuantidadeHospedes,
                    TextColor = Colors.Black
                });

                conteudo.Children.Add(new Label
                {
                    Text = "Entrada: " +
                           reserva.DataEntrada.ToString("dd/MM/yyyy"),
                    TextColor = Colors.Black
                });

                conteudo.Children.Add(new Label
                {
                    Text = "Saída: " +
                           reserva.DataSaida.ToString("dd/MM/yyyy"),
                    TextColor = Colors.Black
                });

                cartao.Content = conteudo;

                ListaReservas.Children.Add(cartao);
            }
        }

        // MÉTODO PARA LIMPAR OS CAMPOS

        private void LimparCampos()
        {
            NomeEntry.Text = string.Empty;
            TelefoneEntry.Text = string.Empty;
            EmailEntry.Text = string.Empty;

            EntradaPicker.Date = DateTime.Today;
            SaidaPicker.Date = DateTime.Today.AddDays(1);

            quantidadeHospedes = 1;
            QuantidadeLabel.Text = "1";
        }

        // BOTÃO LIMPAR

        private void LimparCampos_Clicked(object sender, EventArgs e)
        {
            LimparCampos();
        }
    }
}