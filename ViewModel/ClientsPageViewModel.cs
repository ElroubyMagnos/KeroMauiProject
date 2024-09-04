using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeroFruits.Model.Usine.People;
using KeroFruits.Utilities.DataAccess;
using KeroFruits.Utilities.Interfaces;

namespace KeroFruits.ViewModel
{
    public partial class ClientsPageViewModel : BaseViewModel
    {
        public ClientsPageViewModel(DataAccess dataAccessService, IAlertService alertService) : base(alertService)
        {
            SQLDataAccess = new DataAccessSqlFile();
            dataAccess = dataAccessService;
            Clients = SQLDataAccess.GetAllClients(); //get user's collection datas from chosen DataAccessSource (excel, csv, json...).                                             
            NewSelection = new Client(0, "Kiro");
        }

     

        private DataAccess dataAccess;





      
        public ClientsCollection Clients { get; set; }

        /// <summary>
        /// Staff Member selected in the listview
        /// </summary>
        [ObservableProperty]
        private Client clientSelection;

        [ObservableProperty]
        private Client newSelection;
        /// <summary>
        /// Staff Member who will be displayed in the popup screen
        /// </summary>      // PAS AJOUTé A LA CLIENTS VIEW MODEL 


        /// </summary>
        [ObservableProperty]
        private bool isNewClientrAction; // pas ajouté A LA CLIENT 


        [RelayCommand()]
        public void SaveDatas()
        {
            if (SQLDataAccess.UpdateAllClients(Clients))
            {
                alertService.ShowAlert("Sauvegarde", "Les données staff members ont bien été sauvegardées");
            }
            else
            {
                alertService.ShowAlert("Sauvegarde erreur", "Une erreur est survenue lors de la sauvegarde");
            };
        }

        /// <summary>
        /// Command binded to the "Add new Member" button in the pop up display
        /// </summary>
        [RelayCommand()]
        public void AddNewClient()
        {
            if (Clients.AddClient(NewSelection))
            {

                alertService.ShowAlert("Ajout", "Le nouveau membre a bien été ajouté");
            }
            else
            {
                alertService.ShowAlert("Ajout erreur", "Une erreur est survenue lors de l'ajout");
            };
            //reset the property for a future choice.
        }

        public DateTime Today { get; } = DateTime.Now;
        public string TodayDate => Today.Date.ToString("d-M-yyyy");
    }
}
