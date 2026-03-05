using System;
using System.Collections.Generic;
using System.Text;

namespace DateViewier.Models.Tests
{
    [TestClass]
    public class WorkCalendarTests
    {
        [TestMethod]
        public void InitializeTest()
        {
            Workday workday = new Workday(new DateOnly(2026, 3, 2));
            WorkweekInfo info = new WorkweekInfo(5, 2);
            WorkCalendar workCalendar = new WorkCalendar(workday, info);
            Assert.AreEqual(workday, workCalendar.FirstWorkday);
            Assert.AreEqual(info, workCalendar.WorkweekInfo);
        }

        [TestMethod]
        public void GetMonthlyScheduleBaseWorkweekTest()
        {
            Workday workday = new Workday(new DateOnly(2026, 3, 2));
            WorkweekInfo info = new WorkweekInfo(5, 2);
            WorkCalendar workCalendar = new WorkCalendar(workday, info);
            Day[,] schedule = workCalendar.GetMonthlySchedule(new DateOnly(2026, 3, 2));
            Assert.AreEqual(6, schedule.GetLength(0));
            Assert.AreEqual(7, schedule.GetLength(1));
            DateOnly baseDate = new DateOnly(2026, 2, 23);
            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 7; j++)
                {
                    Assert.AreEqual(baseDate, schedule[i, j].Date);
                    baseDate = baseDate.AddDays(1);
                    if (j == 5 || j == 6)
                    {
                        Assert.IsInstanceOfType<DayOff>(schedule[i, j]);
                    }
                    else
                    {
                        Assert.IsInstanceOfType<Workday>(schedule[i, j]);
                    }
                }
            }
        }

        [TestMethod]
        public void GetYearlyScheduleBaseWorkweekTest()
        {
            Workday workday = new Workday(new DateOnly(2026, 1, 5));
            WorkweekInfo info = new WorkweekInfo(5, 2);
            WorkCalendar workCalendar = new WorkCalendar(workday, info);
            Day[,] schedule = workCalendar.GetYearlySchedule(new DateOnly(2026, 1, 1));
            Assert.AreEqual(53, schedule.GetLength(0));
            Assert.AreEqual(7, schedule.GetLength(1));
            DateOnly baseDate = new DateOnly(2025, 12, 29);
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Assert.AreEqual(baseDate, schedule[i, j].Date);
                    baseDate = baseDate.AddDays(1);
                    if (j == 5 || j == 6)
                    {
                        Assert.IsInstanceOfType<DayOff>(schedule[i, j]);
                    }
                    else
                    {
                        Assert.IsInstanceOfType<Workday>(schedule[i, j]);
                    }
                }
            }
        }

        [TestMethod]
        public void GetMonthlyScheduleTwoTwoTest()
        {
            Workday workday = new Workday(new DateOnly(2026, 3, 2));
            WorkweekInfo info = new WorkweekInfo(2, 2);
            WorkCalendar workCalendar = new WorkCalendar(workday, info);
            Day[,] schedule = workCalendar.GetMonthlySchedule(new DateOnly(2026, 3, 2));
            Assert.AreEqual(6, schedule.GetLength(0));
            Assert.AreEqual(7, schedule.GetLength(1));
            DateOnly baseDate = new DateOnly(2026, 2, 23);
            int daysCount = 1;
            bool workDays = true;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Assert.AreEqual(baseDate, schedule[i, j].Date);
                    baseDate = baseDate.AddDays(1);
                    if (workDays)
                    {
                        Assert.IsInstanceOfType<Workday>(schedule[i, j]);
                    }
                    else
                    {
                        Assert.IsInstanceOfType<DayOff>(schedule[i, j]);
                    }
                    if (++daysCount == 2)
                    {
                        daysCount = 0;
                        workDays = !workDays;
                    }
                }
            }
        }

        [TestMethod]
        public void GetYearlyScheduleTwoTwoTest()
        {
            Workday workday = new Workday(new DateOnly(2026, 1, 1));
            WorkweekInfo info = new WorkweekInfo(2, 2);
            WorkCalendar workCalendar = new WorkCalendar(workday, info);
            Day[,] schedule = workCalendar.GetYearlySchedule(new DateOnly(2026, 1, 1));
            Assert.AreEqual(53, schedule.GetLength(0));
            Assert.AreEqual(7, schedule.GetLength(1));
            DateOnly baseDate = new DateOnly(2025, 12, 29);
            int daysCount = 1;
            bool workDays = true;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Assert.AreEqual(baseDate, schedule[i, j].Date);
                    baseDate = baseDate.AddDays(1);
                    if (workDays)
                    {
                        Assert.IsInstanceOfType<Workday>(schedule[i, j]);
                    }
                    else
                    {
                        Assert.IsInstanceOfType<DayOff>(schedule[i, j]);
                    }
                    if (++daysCount == 2)
                    {
                        daysCount = 0;
                        workDays = !workDays;
                    }
                }
            }
        }
    }
}
