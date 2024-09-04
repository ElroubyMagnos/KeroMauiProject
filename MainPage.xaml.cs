using KeroFruits.View;

namespace KeroFruits
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ClientsPage), typeof(ClientsPage));
            Routing.RegisterRoute(nameof(Reservations), typeof(Reservations));
        }

        private async void Clients_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ClientsPage));
        }

        private async void Reservations_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(Reservations));
        }
    }

}
