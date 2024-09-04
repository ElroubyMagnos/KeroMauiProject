using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//using Windows.Networking;

namespace KeroFruits.Model.Usine.People
{
    public class Client
    {
        //Champs de classe
        private int _id;
        private string _lastName;
        private string _firstName;
        private bool _gender;
        private string _email;
        private string _mobilePhoneNumber;
        //Champs Static



        #region Constructeurs

        public Client(int id, string lastName, string firstName = "prenom", bool gender = true, string email = "", string mobilePhoneNumber = "")
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            Gender = gender;
            Email = email;
            MobilePhoneNumber = mobilePhoneNumber;

        }

        public Client()
        {

        }




        public int Id { get => _id; set => _id = value; }
       
        public string LastName
        {
            get => _lastName;
            set
            {
                if (CheckEntryName(value))
                {
                    _lastName = value;

                }
            }
        }

       
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (CheckEntryName(value))
                {
                    _firstName = value;
                }
            }
        }
       
        public bool Gender { get => _gender; set => _gender = value; }
       
        public string Email
        {
            get => _email;
            set
            {
                if (CheckEMail(value))
                {
                    _email = value;
                }
            }
        }

        public string MobilePhoneNumber
        {
            get => _mobilePhoneNumber;
            set
            {
                if (CheckMobilePhoneNumber(value))
                {
                    _mobilePhoneNumber = value;
                }
            }
        }

        public static bool CheckEntryName(string name)
        {
            //name = "P- Henri";
            //si le nom débute ou termine par un espace ou un tiret
            if (name.StartsWith(" ") || name.StartsWith("-") || name.EndsWith(" ") || name.EndsWith("-"))
            {
                //MessageBox.Show($"La saisie {name} ne peut commencer ou se terminer par un espace ou un tiret", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            //si le nom contient au moins un double espace.
            if (name.Contains(" ") || name.Contains("--"))
            {
                //MessageBox.Show($"La saisie {name} comporte au moins un double espace ou tiret non autorisé", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }


       
        public static bool CheckEMail(string tryEmail)
        {
            return tryEmail.Contains("@") && tryEmail.Contains(".");
        }


        private static bool CheckMobilePhoneNumber(string tryPhone)
        {
            if (tryPhone.Length != 10)
                return false;

            return int.TryParse(tryPhone, out int x);
        }
    }

}

#endregion
