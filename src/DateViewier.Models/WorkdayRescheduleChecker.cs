using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DateViewier.Models
{
    public class WorkdayRescheduleChecker
    {
        private class DateOnlyComparer : IEqualityComparer<DateOnly>
        {
            public bool Equals(DateOnly x, DateOnly y)
            {
                return x.DayOfYear == y.DayOfYear;
            }

            public int GetHashCode(DateOnly obj)
            {
                return obj.DayOfYear.GetHashCode();
            }
        }

        public List<DateOnly> PublicHolidays { get; }

        public WorkdayRescheduleChecker()
        {
            PublicHolidays = new List<DateOnly>();
        }

        public bool IsHoliday(DateOnly date)
        {
            var comparer = new DateOnlyComparer();
            return PublicHolidays.Contains(date, comparer);
        }

        public bool MayRescheduleBaseWorkweekDay(DateOnly workdayDate)
        {
            DateOnly previousDate = workdayDate.AddDays(-1);
            DateOnly nextDate = workdayDate.AddDays(1);
            bool may = MayResheduleBetween(previousDate, nextDate);
            return may;
        }

        private bool MayResheduleBetween(DateOnly date1, DateOnly date2)
        {
            var comparer = new DateOnlyComparer();
            return (PublicHolidays.Contains(date1, comparer) &&
                                (date2.DayOfWeek == DayOfWeek.Saturday || date2.DayOfWeek == DayOfWeek.Sunday)) ||
                   (PublicHolidays.Contains(date2, comparer) &&
                                (date1.DayOfWeek == DayOfWeek.Saturday || date1.DayOfWeek == DayOfWeek.Sunday));
        }
    }
}
