using System;
using System.Collections.Generic;
using System.Text;

namespace DateViewier.Models
{
    public class WorkCalendar
    {
        public Workday FirstWorkday { get; set; }

        public WorkweekInfo WorkweekInfo { get; set; }

        public WorkCalendar(Workday firstWorkday, WorkweekInfo workweekInfo)
        {
            FirstWorkday = firstWorkday;
            WorkweekInfo = workweekInfo;
        }

        public Day[,] GetMonthlySchedule(DateOnly monthDate)
        {
            DateOnly correctMonthDate = new DateOnly(monthDate.Year, monthDate.Month, 1);
            Day firstMonthDay = new Day(correctMonthDate);
            Day lastMonthDay = new Day(correctMonthDate.AddMonths(1).AddDays(-1));
            DateOnly firstDay = firstMonthDay.Date;
            DateOnly lastDay = lastMonthDay.Date;
            int tableRows = 4;
            if (firstMonthDay.WeekPosition != 0)
            {
                tableRows++;
                firstDay = firstDay.AddDays(-firstMonthDay.WeekPosition);
            }
            if (lastMonthDay.WeekPosition != 6)
            {
                tableRows++;
                lastDay = lastDay.AddDays(6 - lastMonthDay.WeekPosition);
            }
            Day[] days = new Day[tableRows * 7];
            (DateOnly firstWorkday, int firstWorkdayPosition) = GetFirstWorkdayForDays(firstDay, lastDay);
            return TransformDaysToTable(FillWorkweeksByInfo(days, firstWorkday, firstWorkdayPosition));
        }

        public Day[,] GetYearlySchedule(DateOnly yearDate)
        {
            DateOnly correctMonthDate = new DateOnly(yearDate.Year, 1, 1);
            Day firstMonthDay = new Day(correctMonthDate);
            Day lastMonthDay = new Day(correctMonthDate.AddYears(1).AddDays(-1));
            DateOnly firstDay = firstMonthDay.Date;
            DateOnly lastDay = lastMonthDay.Date;
            int tableRows = 52;
            if (firstMonthDay.WeekPosition != 0)
            {
                tableRows++;
                firstDay = firstDay.AddDays(-firstMonthDay.WeekPosition);
            }
            if (lastMonthDay.WeekPosition != 6)
            {
                tableRows++;
                lastDay = lastDay.AddDays(6 - lastMonthDay.WeekPosition);
            }
            Day[] days = new Day[tableRows * 7];
            (DateOnly firstWorkday, int firstWorkdayPosition) = GetFirstWorkdayForDays(firstDay, lastDay);
            return TransformDaysToTable(FillWorkweeksByInfo(days, firstWorkday, firstWorkdayPosition));
        }

        private Day[] FillWorkweeksByInfo(Day[] days, DateOnly firstWorkdayDate, int firstWorkdayIndex)
        {
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

        private (DateOnly, int) GetFirstWorkdayForDays(DateOnly startDate, DateOnly endDate)
        {
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
