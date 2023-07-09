﻿using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodTracker
{
    public partial class ComplementPageViewModel : ViewModelBase
    {
        private DateTime _maximumDisplayedDate;
        public DateTime MaximumDisplayedDate
        {
            get { return _maximumDisplayedDate; }
            set { _maximumDisplayedDate = value; NotifyPropertyChanged(); }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value; NotifyPropertyChanged(); }
        }


        public ComplementPageViewModel(IDataBaseManager dataBaseManager, IPeriodManager periodManager) : base(dataBaseManager, periodManager)
        {
            MaximumDisplayedDate = DateTime.Today;
            SelectedDate = DateTime.Today;
        }

        [RelayCommand]
        public async Task SaveDate()
        { 
            await PeriodManager.SaveDate(SelectedDate);
        }
    }
}