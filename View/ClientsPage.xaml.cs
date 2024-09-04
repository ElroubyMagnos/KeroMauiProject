using KeroFruits.ViewModel;

namespace KeroFruits.View;

public partial class ClientsPage : ContentPage
{
    public ClientsPage(ClientsPageViewModel clientsPageVM)
    {
        BindingContext = clientsPageVM;
        InitializeComponent();
    }
}