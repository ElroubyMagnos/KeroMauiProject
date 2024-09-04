using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeroFruits.Model.Usine.Calendar;
using KeroFruits.Model.Usine.People;
using KeroFruits.Utilities.DataAccess;
using KeroFruits.Utilities.Interfaces;

namespace KeroFruits.ViewModel
{
    public partial class ReservationsViewModel : BaseViewModel
    {
        public ReservationsViewModel(DataAccess dataAccessService, IAlertService alertService) : base(alertService)
        {
            SQLDataAccess = new DataAccessSqlFile();
            dataAccess = dataAccessService;
            Clients = SQLDataAccess.GetAllClients();

            NewreservationSelection = new Reservation(clientSelection, 0, DateTime.Now, DateTime.Now, 0, null, 0);
        }

        private DataAccess dataAccess;

        public ClientsCollection Clients { get; set; }

        [ObservableProperty]
        public ReservationsCollection reservationsCol;

        [ObservableProperty]
        private Client clientSelection;

        [ObservableProperty]
        private Client selectedclientSelection;

        [RelayCommand()]
        private void getReservations()
        {
            if (ClientSelection != null)
            {
                ReservationsCol = SQLDataAccess.EnterClientGetReservations(ClientSelection, Clients);
            }
            else ReservationsCol = null;
        }

        [ObservableProperty]
        private Reservation reservationSelectionCol;

        [ObservableProperty]
        private Reservation newreservationSelection;

        [RelayCommand()]
        public void AddReservation()
        {
            Calculate();


            if (!CalcSuccess)
                return;

            if (ReservationsCol == null)
            {
                alertService.ShowAlert("Error", "Fill Reservation First");
                return;
            }
            
            if (NewreservationSelection != null)
            {
                if (ReservationsCol.FirstOrDefault(x => 
                x.EstimatedDate == NewreservationSelection.EstimatedDate
                &&
                x.Date == NewreservationSelection.Date
                &&
                x.EffectifLitresReceived == NewreservationSelection.EffectifLitresReceived
                ) == null)
                    ReservationsCol.AddReservation(NewreservationSelection);
                else alertService.ShowAlert("Error", "Same Dates Already Exist");
            }
        }

        bool CalcSuccess = false;

        [RelayCommand()]
        public void Calculate()
        {
            if (SelectedclientSelection == null)
            {
                alertService.ShowAlert("Error", "Select Client First");
                CalcSuccess = false;
                return;
            }
            NewreservationSelection = new Reservation(SelectedclientSelection, NewreservationSelection.ID, NewreservationSelection.Date, NewreservationSelection.DeliveryEffectifDate, NewreservationSelection.Quantity, null, SelectedclientSelection.Id);

            CalcSuccess = true;
        }

        
        [RelayCommand()]
        public void SaveReservationsDatas()
        {
            if (SQLDataAccess.UpdateAllReservations(ReservationsCol))
            {
                alertService.ShowAlert("Sauvegarde", "Les données de reservation ont bien été sauvegardées");
            }
            else
            {
                alertService.ShowAlert("Sauvegarde erreur", "Une erreur est survenue lors de la sauvegarde");
            };
        }

        [RelayCommand()]
        public void SaveAll()
        {
            SaveReservationsDatas();
        }
    }
}
