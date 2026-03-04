using DateViewier.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DateViewier.AndroidApp.Repositories
{
    public class WorkCalendarPreferencesRepository
        : IInstanceRepistory<WorkCalendar>
    {
        private static string _firstWorkDayField = "workCalendar.firstWorkdayDayDate";

        private IInstanceRepistory<WorkweekInfo> workweekInfoRepository;

        public WorkCalendarPreferencesRepository(IInstanceRepistory<WorkweekInfo> workweekInfoRepository)
        {
            this.workweekInfoRepository = workweekInfoRepository;
        }

        public WorkCalendar Load()
        {
            var workweek = workweekInfoRepository.Load();
            var dateTime = Preferences.Get(_firstWorkDayField, DateTime.Now);
            var date = DateOnly.FromDateTime(dateTime);
            return new WorkCalendar(new Workday(date), workweek);
        }

        public void Save(WorkCalendar instance)
        {
            Preferences.Set(_firstWorkDayField, new DateTime(instance.FirstWorkday.Date, new TimeOnly()));
        }
    }
}
