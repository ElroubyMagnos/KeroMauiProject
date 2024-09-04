using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeroFruits.Model.Usine.Payment
{
    public class FacturesCollection :  ObservableCollection<Facture>
    {
        public FacturesCollection() { }

        public void AddFacture(Facture f)
        {
            if (this.Count == 0 || !this.Any(factureInTheCollection => factureInTheCollection.VatNumber == f.VatNumber))
            {
                this.Add(f);
            }
            else
            {
                // Facture with the same VAT number already exists in the collection and will not be added.
                Console.WriteLine("Facture with the same VAT number already exists.");
            }
        }

        public void RemoveFacture(Facture f)
        {
            if (this.Contains(f))
            {
                this.Remove(f);
            }
            else
            {
                // Facture not found in the collection.
                Console.WriteLine("Facture not found in the collection.");
            }
        }
    }
}
