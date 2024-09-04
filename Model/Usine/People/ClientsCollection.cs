using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeroFruits.Model.Usine.People
{
    public class ClientsCollection : ObservableCollection<Client>
    {

        public ClientsCollection() { }

        public bool AddClient(Client cl)
        {
            var Selected = this.Where(x => x.FirstName == cl.FirstName && x.LastName == cl.LastName).Count();
            
            if (Selected > 0)
            {
                return false;
            }

            this.Add(cl);

            return true;
        }


        public bool RemoveClient(Client cl)
        {
            if (this.Any(clientInTheCollection => clientInTheCollection.Id == cl.Id || (clientInTheCollection.LastName == cl.LastName && clientInTheCollection.FirstName == cl.FirstName)))
            {
                this.Remove(cl);
                return true;

            }
            else
            {
                //if StaffMember not in the collection 
                return false;
            }
        }

        public int GetNextId()
        {
            if (this != null && this.Count > 1)
            {
                return this.Max(sm => sm.Id) + 1;
            }
            else
            {
                return 1;
            }
        }

    }


}
