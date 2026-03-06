using DateViewier.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DateViewier.AndroidApp.Repositories
{
    public class WorkdayRescheduleCheckerMemoryRepository : IOnlyLoadInstanceRepositoy<WorkdayRescheduleChecker>
    {
        private WorkdayRescheduleChecker _instance = new WorkdayRescheduleChecker();

        public WorkdayRescheduleCheckerMemoryRepository()
        {
            _instance.PublicHolidays.Add(new DateOnly(DateTime.Now.Year, 1, 1));
            _instance.PublicHolidays.Add(new DateOnly(DateTime.Now.Year, 1, 2));
            _instance.PublicHolidays.Add(new DateOnly(DateTime.Now.Year, 1, 7));
            _instance.PublicHolidays.Add(new DateOnly(DateTime.Now.Year, 3, 8));
            _instance.PublicHolidays.Add(new DateOnly(DateTime.Now.Year, 5, 9));
            _instance.PublicHolidays.Add(new DateOnly(DateTime.Now.Year, 7, 3)); 
            _instance.PublicHolidays.Add(new DateOnly(DateTime.Now.Year, 11, 7));
            _instance.PublicHolidays.Add(new DateOnly(DateTime.Now.Year, 12, 25));
        }

        public WorkdayRescheduleChecker Load()
        {
            var checker = new WorkdayRescheduleChecker();
            foreach(var item in _instance.PublicHolidays)
            {
                checker.PublicHolidays.Add(item);
            }
            return checker;
        }
    }
}
