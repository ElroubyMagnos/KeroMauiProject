using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeroFruits.Model.Usine.Payment
{
    public class TicketsCollection : ObservableCollection<Ticket>
    {

        public TicketsCollection() { }

        public new void AddTicket(Ticket ti)
        {
            if (!this.Any(TicketInTheCollection => TicketInTheCollection.IdSerial == ti.IdSerial))
            {
                this.Add(ti);
            }
            else
            {
            } //id product is already in the collection and will not be added.
        }
    }
}
