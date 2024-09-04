//ing Java.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KeroFruits.Model.Usine.Payment
{
    public class Facture : Ticket
    {
        private string _vatNumber;


        public Facture(int idSerial , double totalWeight, double litreQuantity, double totalPrice, string vatNumber)
           : base(idSerial,totalWeight, litreQuantity, totalPrice)

        {
            VatNumber = vatNumber;

        }

        public string VatNumber
        {
            get => _vatNumber;
            set
            {
                if (CheckVatNumber(value))
                {
                    _vatNumber = value;
                }
                else
                {
                    Console.WriteLine("Numéro de TVA invalide.");
                }
            }
        }

        // Méthode de validation
        private bool CheckVatNumber(string number)
        {
            // Vérifie si le numéro est au format BE suivi de 10 chiffres
            if (Regex.IsMatch(number, @"^BE[0-9]{10}$"))
            {
                // Extraire les chiffres pour vérification
                string digits = number.Substring(2); // Enlever le préfixe BE
                long num;
                if (long.TryParse(digits, out num)) // Vérifier que c'est bien un nombre
                {
                    // Calculer le code de contrôle (les deux premiers chiffres après BE)
                    long controlNumber = long.Parse(digits.Substring(0, 2));
                    long companyNumber = long.Parse(digits.Substring(2));

                    // Le numéro d'entreprise est correct si 97 - (le numéro d'entreprise modulo 97) égale le code de contrôle
                    return (97 - (companyNumber % 97)) == controlNumber;
                }
            }
            return false;
        }
    }
}

