using System;
using System.Collections.Generic;
using System.Text;

namespace DateViewier.AndroidApp.ViewModels
{
    public class DayViewModel
    {
        public enum DayType
        {
            Workday,
            DayOff
        }

        public DateOnly Date { get; }

        public DayType Type { get; }

        public DayViewModel(DateOnly date, DayType type)
        {
            Date = date;
            Type = type;
        }
    }
}
