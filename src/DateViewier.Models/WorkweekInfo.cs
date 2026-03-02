using System;
using System.Collections.Generic;
using System.Text;

namespace DateViewier.Models
{
    public class WorkweekInfo
    {
        public int WorkdaysCount { get; }

        public int DayOffsCount { get; }

        public int WorkweekLength => WorkdaysCount + DayOffsCount;

        public WorkweekInfo(int workdaysCount, int dayOffsCount)
        {
            WorkdaysCount = workdaysCount;
            DayOffsCount = dayOffsCount;
        }
    }
}
