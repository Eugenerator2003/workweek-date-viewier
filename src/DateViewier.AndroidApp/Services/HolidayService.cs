using DateViewier.AndroidApp.Repositories;
using DateViewier.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DateViewier.AndroidApp.Services
{
    public class HolidayService
    {
        private IOnlyLoadInstanceRepositoy<WorkdayRescheduleChecker> repository;

        public HolidayService(IOnlyLoadInstanceRepositoy<WorkdayRescheduleChecker> repository)
        {
            this.repository = repository;
        }
        
        public bool MayRescheduleBaseWorkweekDay(DateOnly date)
        {
            return repository.Load().MayRescheduleBaseWorkweekDay(date);
        }

        public bool IsHoliday(DateOnly date)
        {
            return repository.Load().IsHoliday(date);
        }
    }
}
