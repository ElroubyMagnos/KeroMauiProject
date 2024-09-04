using KeroFruits.ViewModel;

namespace KeroFruits.View;

public partial class Reservations : ContentPage
{
	public Reservations(ReservationsViewModel reservationspage)
	{
		InitializeComponent();
		BindingContext = reservationspage;
	}
}