using DateViewier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateViewier.Models.Tests
{
    [TestClass]
    public class DayTests
    {
        [TestMethod]
        [DataRow("02.03.2026", 0)]
        [DataRow("03.03.2026", 1)]
        [DataRow("04.03.2026", 2)]
        [DataRow("05.03.2026", 3)]
        [DataRow("06.03.2026", 4)]
        [DataRow("07.03.2026", 5)]
        [DataRow("08.03.2026", 6)]
        public void InitializeTest(string dateString, int position)
        {
            DateOnly date = DateOnly.Parse(dateString);
            Day day = new Day(date);
            Assert.AreEqual(date, day.Date);
            Assert.AreEqual(position, day.WeekPosition);
        }
    }
}
