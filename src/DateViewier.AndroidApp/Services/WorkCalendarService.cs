using DateViewier.AndroidApp.Repositories;
using DateViewier.AndroidApp.ViewModels;
using DateViewier.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DateViewier.AndroidApp.Services
{
    public class WorkCalendarService
    {
        IInstanceRepistory<WorkCalendar> workCalendarRepository;

        public WorkCalendarService(IInstanceRepistory<WorkCalendar> workCalendarRepository)
        {
            this.workCalendarRepository = workCalendarRepository;
        }

        public DayViewModel[,] GetScheduleByMonth(DateOnly date)
        {
            return GetFromDays(workCalendarRepository.Load().GetMonthlySchedule(date));
        }

        public DayViewModel[,] GetScheduleByYear(DateOnly date)
        {
            return GetFromDays(workCalendarRepository.Load().GetYearlySchedule(date));
        }

        public DateOnly GetFirstWorkdayDate()
        {
            return workCalendarRepository.Load().FirstWorkday.Date;
        }

        public void SetFirstWorkday(DateOnly date)
        {
            var workCalendar = workCalendarRepository.Load();
            workCalendar.FirstWorkday = new Workday(date);
            workCalendarRepository.Save(workCalendar);
        }

        private DayViewModel[,] GetFromDays(Day[,] days)
        {
            var viewSchedule = new DayViewModel[days.GetLength(0), days.GetLength(1)];
            for (int i = 0; i < days.GetLength(0); i++)
            {
                for (int j = 0; j < days.GetLength(1); j++)
                {
                    if (days[i, j] is Workday)
                    {
                        viewSchedule[i, j] = new DayViewModel(days[i, j].Date, DayViewModel.DayType.Workday);
                    }
                    else if (days[i, j] is DayOff)
                    {
                        viewSchedule[i, j] = new DayViewModel(days[i, j].Date, DayViewModel.DayType.DayOff);
                    }
                }
            }
            return viewSchedule;
        }
    }
}
