using System;
using System.Collections.Generic;
using System.Text;

namespace DateViewier.Models.Tests
{
    [TestClass]
    public class WorkdayRescheduleCheckerTests
    {
        [TestMethod]
        [DataRow("28-04-2025", "29-04-2025", true)]
        [DataRow("04-07-2025", "03-07-2025", true)]
        [DataRow("26-12-2025", "25-12-2025", true)]
        [DataRow("20-04-2026", "21-04-2026", true)]
        [DataRow("22-04-2026", "21-04-2026", false)]
        [DataRow("07-05-2026", "09-05-2026", false)]
        public void MayRescheduleBaseWorkweekDayTest(string dateString, string nearestHolidayDateString, bool expectedResult)
        {
            var date = DateOnly.Parse(dateString);
            var holidayDate = DateOnly.Parse(nearestHolidayDateString);
            var checker = new WorkdayRescheduleChecker();
            checker.PublicHolidays.Add(holidayDate);
            var result = checker.MayRescheduleBaseWorkweekDay(date);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
