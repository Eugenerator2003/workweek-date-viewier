using System;
using System.Collections.Generic;
using System.Text;

namespace DateViewier.Models
{
    public class Workday : Day
    {
        public Workday(DateOnly date) : base(date)
        {
        }

        public override string ToString()
        {
            return $"Workday {Date:dd.MM.yyyy}";
        }
    }
}
