using System;
using System.Collections.Generic;
using System.Text;

namespace DateViewier.Models
{
    public class WorkdayRescheduleChecker
    {
        public List<DateOnly> PublicHolidays { get; }

        public WorkdayRescheduleChecker()
        {
            PublicHolidays = new List<DateOnly>();
        }

        public bool MayRescheduleBaseWorkweekDay(DateOnly workdayDate)
        {
            DateOnly previousDate = workdayDate.AddDays(-1);
            DateOnly nextDate = workdayDate.AddDays(1);
            bool may = MayResheduleBetween(previousDate, nextDate) || MayResheduleBetween(nextDate, previousDate);
            return may;
        }

        private bool MayResheduleBetween(DateOnly date1, DateOnly date2)
        {
            return PublicHolidays.Contains(date1) &&
                                (date2.DayOfWeek == DayOfWeek.Saturday || date2.DayOfWeek == DayOfWeek.Sunday);
        }
    }
}
