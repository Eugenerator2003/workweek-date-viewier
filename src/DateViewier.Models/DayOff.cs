using System;
using System.Collections.Generic;
using System.Text;

namespace DateViewier.Models
{
    public class DayOff : Day
    {
        public DayOff(DateOnly date) : base(date)
        {
        }
        public override string ToString()
        {
            return $"Dayoff {Date:dd.MM.yyyy}";
        }
    }
}
