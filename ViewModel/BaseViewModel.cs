using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeroFruits.Utilities.DataAccess;
using KeroFruits.Utilities.Interfaces;

namespace KeroFruits.ViewModel
{
    public class BaseViewModel : ObservableObject
    {
        public DataAccessSqlFile SQLDataAccess { get; set; }
        public BaseViewModel(IAlertService alertService)
        {
            
            this.alertService = alertService;
        }

        protected IAlertService alertService;

        public DateTime Today { get; } = DateTime.Now;
        public string TodayDate => Today.Date.ToString("d-M-yyyy");
    }
}
