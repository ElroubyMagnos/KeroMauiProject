//using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Windows.Networking;

namespace KeroFruits.Model.Usine.Payment
{
    public class Ticket
    {
        private const double MIN_WEIGHT = 1.0;   
        private const double MIN_PRICE = 0.0;    
        private const double MIN_LITRE_QUANTITY = 0.1;

        //Champs de classe
        private int _idSerial;
        private double _totalWeight;
        private double _litreQuantity;
        private double _totalPrice;


        //Constructeur 
        public Ticket(int idSerial, double totalWeight, double litreQuantity, double totalPrice)
        {
            IdSerial = idSerial;
            TotalWeight = totalWeight;
            LitreQuantity = litreQuantity;
            TotalPrice = totalPrice;
            
        }


        public int IdSerial
        {
            get => _idSerial;
            set
            {
                if (CheckNotNegative(value))
                {
                    _idSerial = value;
                }
            }
        }


        public double TotalWeight
        {
            get => _totalWeight;
            set
            {
                if (CheckTotalWeight(value))
                {
                    _totalWeight = value;
                }
                else
                {
                    Console.WriteLine($"Le poids total doit être d'au moins {MIN_WEIGHT} kg.");
                }
            }
        }

        public double LitreQuantity
        {
            get => _litreQuantity;
            set
            {
                if (CheckLitreQuantity(value))
                {
                    _litreQuantity = value;
                }
                else
                {
                    Console.WriteLine($"La quantité en litres doit être d'au moins {MIN_LITRE_QUANTITY}.");
                }
            }
        }

        public double TotalPrice
        {
            get => _totalPrice;
            set
            {
                if (CheckTotalPrice(value))
                {
                    _totalPrice = value;
                }
                else
                {
                    Console.WriteLine($"Le prix total ne doit pas être inférieur à {MIN_PRICE}.");
                }
            }
        }


        public bool CheckNotNegative(double d)
        {
            return d > 0.0;
        }


        // Méthodes de validation
        private bool CheckTotalWeight(double weight)
        {
            return weight >= MIN_WEIGHT;
        }

        private bool CheckLitreQuantity(double quantity)
        {
            return quantity >= MIN_LITRE_QUANTITY;
        }

        private bool CheckTotalPrice(double price)
        {
            return price >= MIN_PRICE;
        }


    }
}

