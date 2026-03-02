using System;
using System.Collections.Generic;
using System.Text;

namespace DateViewier.Models
{
    public class Day
    {
        public DateOnly Date { get; }

        public int WeekPosition { get; }

        public Day(DateOnly date)
        {
            Date = date;
            WeekPosition = date.DayOfWeek != DayOfWeek.Sunday ? (int)date.DayOfWeek - 1 : 6;
        }
    }
}
