using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using KeroFruits.Model.Usine.Calendar;
using KeroFruits.Model.Usine.Payment;
using KeroFruits.Model.Usine.People;
using KeroFruits.Utilities.DataAccess.Files;
using KeroFruits.Utilities.Interfaces;

namespace KeroFruits.Utilities.DataAccess
{
    public abstract class DataAccess
    {
        private string _accessPath;
        /// <summary>
        /// constructor with just the fileName for AccessPath
        /// </summary>
        /// <param name="filePath"></param>
        public DataAccess()
        {
        }
        /// <summary>
        /// Constructor with fileName only one and authorized file extensions
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="extensions"> Exemple {".xlxs",".json"}</param>
        public DataAccess(string filePath, string[] extensions)
        {
            Extensions = new List<string>(extensions.ToList());
            AccessPath = filePath;
        }
        /// <summary>
        /// Constructor associated with a DatafileManager object, it will contains all datas files informations (path and subject)
        /// </summary>
        /// <param name="dfm"></param>
        public DataAccess(DataFilesManager dfm)
        {
            this.DataFilesManager = dfm;
        }
        public DataFilesManager DataFilesManager { get; set; }
        /// <summary>
        /// AccessPath file to the data source
        /// </summary>
        /// 
        public virtual string AccessPath
        {
            get => _accessPath;
            set
            {
                _accessPath = value;
            }
        }//end AccessPath
        /// <summary>
        /// List of authorized extensions .txt, csv, .Json, .xml ...for the AccessPath file
        /// </summary>
        public List<string> Extensions { get; set; }
        /// <summary>
        /// Continue to check AccessPath even after constructor (in the case of the file may be moved, renamed or deleted)
        /// </summary>
        public bool IsValidAccessPath => CheckAccessPath(AccessPath);



        //public ClientsCollection GetAllClients();


        //public ProductsCollection GetAllProducts();
        //public ReservationsCollection GetAllReservations();
        //public FacturesCollection GetAllFactures();
        //public TicketsCollection GetAllTickets();

        //public ReservationsCollection EnterClientGetReservations(Client client);

        //public bool UpdateAllClients(ClientsCollection clients);

        //public bool UpdateAllReservations(ReservationsCollection clients);

        /// <summary>
        /// Check AccessPath to the data source file. File path must exist and if
        /// extensions are specified, the extension file must match to one of them
        /// </summary>
        /// <returns>true if valid path and extension file</returns>
        public bool CheckAccessPath(string tryPath)
        {
            if (File.Exists(tryPath))
            {
                if (Extensions?.Any() ?? false)
                {
                    string pattern = "";
                    foreach (string ext in Extensions)
                    {
                        pattern += ext + "|";
                    }
                    pattern = pattern.Substring(0, pattern.Length - 1);
                    //check extension file
                    if (!Regex.IsMatch(tryPath, pattern + "$")) //pattern exemple = ".txt|.csv$"
                    {
                        Console.WriteLine($"L'extension du fichier {tryPath} n'est pas valide, extensions attendues : {pattern}", "Erreur de fichier");
                        return false;
                    }
                    return true;
                }
                else
                {
                    return true;
                }
            }

            else
            {
                Console.WriteLine($"Le fichier {tryPath} n'existe pas", "Erreur de fichier");
                return false;
            }
        }

        public bool UpdateAllcLIENTS(ClientsCollection clients)
        {
            return true;
        }

        /// <summary> 
        /// Constructor associated with a DatafileManager object, it will contains all datas 
        ///files informations(path subject)
        /// </summary> 
        /// <param name="dfm"></param> 
        public DataAccess(DataFilesManager dfm, IAlertService alertService)
        {

            this.DataFilesManager = dfm;
            this.alertService = alertService;
        }

        protected IAlertService alertService;
    }//end class DataAccess
}
