using System;
using System.Collections.Generic;
using System.Text;

namespace DateViewier.Models.Tests
{
    [TestClass]
    public class WorkdayRescheduleCheckerTests
    {
        private WorkdayRescheduleChecker checker = null!;

        [TestInitialize()]
        public void Initialize()
        {
            checker = new WorkdayRescheduleChecker();
            checker.PublicHolidays.Add(new DateOnly(DateTime.Now.Year, 1, 1));
            checker.PublicHolidays.Add(new DateOnly(DateTime.Now.Year, 1, 2));
            checker.PublicHolidays.Add(new DateOnly(DateTime.Now.Year, 1, 7));
            checker.PublicHolidays.Add(new DateOnly(DateTime.Now.Year, 3, 8));
            checker.PublicHolidays.Add(new DateOnly(DateTime.Now.Year, 5, 9));
            checker.PublicHolidays.Add(new DateOnly(DateTime.Now.Year, 7, 3));
            checker.PublicHolidays.Add(new DateOnly(DateTime.Now.Year, 11, 7));
            checker.PublicHolidays.Add(new DateOnly(DateTime.Now.Year, 12, 25));
        }

        [TestMethod]
        [DataRow("04-07-2025", true)]
        [DataRow("26-12-2025", true)]
        [DataRow("22-04-2026", false)]
        [DataRow("07-05-2026", false)]
        public void MayRescheduleBaseWorkweekDayTest(string dateString, bool expectedResult)
        {
            var date = DateOnly.Parse(dateString);
            var result = checker.MayRescheduleBaseWorkweekDay(date);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        [DataRow("25-12-2027", true)]
        [DataRow("09-05-2021", true)]
        [DataRow("07-01-2026", true)]
        [DataRow("09-01-2021", false)]
        [DataRow("10-10-2021", false)]
        public void IsHolidayTests(string dateString, bool isHolidayExpected)
        {
            var date = DateOnly.Parse(dateString);
            Assert.AreEqual(isHolidayExpected, checker.IsHoliday(date));
        }
    }
}
