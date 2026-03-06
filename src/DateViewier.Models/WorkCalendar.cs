using System;
using System.Collections.Generic;
using System.Text;

namespace DateViewier.Models
{
    public class WorkCalendar
    {
        private struct ScheduleInfo
        {
            public DateOnly Start;
            public DateOnly End;
            public int Weeks => (int)Math.Ceiling((double)(End.DayNumber - Start.DayNumber + 1) / 7);
        }

        public Workday FirstWorkday { get; set; }

        public WorkweekInfo WorkweekInfo { get; set; }

        public WorkCalendar(Workday firstWorkday, WorkweekInfo workweekInfo)
        {
            ArgumentNullException.ThrowIfNull(firstWorkday);
            ArgumentNullException.ThrowIfNull(workweekInfo);
            FirstWorkday = firstWorkday;
            WorkweekInfo = workweekInfo;
        }

        public Day[,] GetMonthlySchedule(DateOnly monthDate)
        {
            DateOnly correctMonthDate = new DateOnly(monthDate.Year, monthDate.Month, 1);
            Day firstMonthDay = new Day(correctMonthDate);
            Day lastMonthDay = new Day(correctMonthDate.AddMonths(1).AddDays(-1));
            return GetSchedule(firstMonthDay, lastMonthDay);
        }

        public Day[,] GetYearlySchedule(DateOnly yearDate)
        {
            DateOnly correctMonthDate = new DateOnly(yearDate.Year, 1, 1);
            Day firstYearDay = new Day(correctMonthDate);
            Day lastYearDay = new Day(correctMonthDate.AddYears(1).AddDays(-1));
            return GetSchedule(firstYearDay, lastYearDay);
        }

        private Day[,] GetSchedule(Day startDay, Day endDate)
        {
            if (startDay.Date > endDate.Date)
                throw new ArgumentException("Start day date can not be greather than last day date");
            ScheduleInfo context = GetScheduleContext(startDay, endDate);
            Day[] days = new Day[context.Weeks * 7];
            (DateOnly firstWorkday, int firstWorkdayPosition) = GetFirstWorkdayForDays(context.Start, context.End);
            return TransformDaysToTable(FillWorkweeksByInfo(days, firstWorkday, firstWorkdayPosition));
        }

        private Day[] FillWorkweeksByInfo(Day[] days, DateOnly firstWorkdayDate, int firstWorkdayIndex)
        {
            ArgumentNullException.ThrowIfNull(days);
            ArgumentOutOfRangeException.ThrowIfNegative(firstWorkdayIndex);
            int backIndex = firstWorkdayIndex - 1;
            int backDayNumber = firstWorkdayDate.DayNumber - 1;
            while (backIndex >= 0)
            {
                for(int i = 0; i < WorkweekInfo.DayOffsCount && backIndex >= 0; i++, backIndex --, backDayNumber--)
                {
                    days[backIndex] = new DayOff(DateOnly.FromDayNumber(backDayNumber));
                }
                for(int i = 0; i < WorkweekInfo.WorkdaysCount && backIndex >= 0; i++, backIndex --, backDayNumber--)
                {
                    days[backIndex] = new Workday(DateOnly.FromDayNumber(backDayNumber));
                }
            }
            int dayNumber = firstWorkdayDate.DayNumber;
            while (firstWorkdayIndex < days.Length)
            {
                for(int i = 0; i < WorkweekInfo.WorkdaysCount && firstWorkdayIndex < days.Length; i++, firstWorkdayIndex++, dayNumber++)
                {
                    days[firstWorkdayIndex] = new Workday(DateOnly.FromDayNumber(dayNumber));
                }
                for(int i = 0; i < WorkweekInfo.DayOffsCount && firstWorkdayIndex < days.Length; i++, firstWorkdayIndex++, dayNumber++)
                {
                    days[firstWorkdayIndex] = new DayOff(DateOnly.FromDayNumber(dayNumber));
                }
            }
            return days;
        }

        private Day[,] TransformDaysToTable(Day[] days)
        {
            ArgumentNullException.ThrowIfNull(days);
            if (days.Length % 7 != 0)
            {
                throw new ArgumentException("Can't divide range of days with no divisible by 7 into weeks");
            }
            int rows = days.Length / 7;
            Day[,] table = new Day[rows, 7];
            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < 7; j++)
                {
                    table[i, j] = days[i * 7 + j];
                }
            }
            return table;
        }

        private ScheduleInfo GetScheduleContext(Day startDay, Day endDay)
        {
            if (startDay.Date > endDay.Date)
                throw new ArgumentException("Start day date can not be greather than end day date");
            int days = endDay.Date.DayNumber - startDay.Date.DayNumber + 1;
            ScheduleInfo scheduleContext = new ScheduleInfo();
            scheduleContext.Start = startDay.Date;
            scheduleContext.End = endDay.Date;
            if (startDay.WeekPosition != 0)
            {
                scheduleContext.Start = startDay.Date.AddDays(-startDay.WeekPosition);
            }
            if (endDay.WeekPosition != 6)
            {
                scheduleContext.End = endDay.Date.AddDays(6 - endDay.WeekPosition);
            }

            return scheduleContext;
        }

        private (DateOnly, int) GetFirstWorkdayForDays(DateOnly startDate, DateOnly endDate)
        {
            if (startDate > endDate)
                throw new ArgumentException("Start date can not be greather than end date");
            int index = 0;
            for (; startDate < endDate; startDate = startDate.AddDays(1), index++)
            {
                if (IsFirstWorkday(startDate))
                {
                    break;
                }
            }
            return (startDate, index);
        }

        private bool IsFirstWorkday(DateOnly date)
        {
            int difference = Math.Abs(date.DayNumber - FirstWorkday.Date.DayNumber);
            int posistion = difference % WorkweekInfo.WorkweekLength;
            if (FirstWorkday.Date > date && posistion != 0)
            {
                posistion = WorkweekInfo.WorkweekLength - posistion;
            }
            return posistion == 0;
        }
    }
}
