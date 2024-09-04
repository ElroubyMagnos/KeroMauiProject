using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KeroFruits.Model.Production
{
    public class Conditioning
    {
        // Champs privés
        private string _name;
        private double _volume;
        private double _unitPrice;


        // Constructeur
        public Conditioning(string name, double volume, double unitPrice)
        {
            Name = name;
            Volume = volume;
            UnitPrice = unitPrice;
        }
        public string Name { get => _name; set => _name = value; }

       
        public double Volume
        {
            get { return _volume; }
            set
            {
                if (value > 0)
                {
                    _volume = value;
                }
                else
                {
                    Console.WriteLine("Le volume doit être supérieur à 0.");
                }
            }
        }

        public double UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                if (value >= 0)
                {
                    _unitPrice = value;
                }
                else
                {
                    Console.WriteLine("Le prix unitaire doit être supérieur ou égal à 0.");
                }
            }
        }

       
    }
}
