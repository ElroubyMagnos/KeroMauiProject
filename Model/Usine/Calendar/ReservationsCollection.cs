using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeroFruits.Model.Usine.People;

namespace KeroFruits.Model.Usine.Calendar
{
    public class ReservationsCollection : ObservableCollection<Reservation>
    {

        public ReservationsCollection() { }



        public void AddReservation(Reservation reservation)
        {
            if (!this.Any(r => r.Client == reservation.Client
                               && r.Date == reservation.Date))
            {
                this.Add(reservation);
            }
            else
            {
                // Reservation already exists in the collection and will not be added.
                Console.WriteLine("Reservation with the same client, date, and product already exists in the collection and will not be added.");
            }
        }


        public void DeleteReservation(Client client, DateTime date)
        {
            var reservationToDelete = this.FirstOrDefault(r => r.Client == client && r.Date == date);
            if (reservationToDelete != null)
            {
                this.Remove(reservationToDelete);
            }
            else
            {
                // Reservation not found
                Console.WriteLine("Reservation not found in the collection.");
            }
        }


    }
}
