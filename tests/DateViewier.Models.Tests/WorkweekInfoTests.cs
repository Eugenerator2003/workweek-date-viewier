using System;
using System.Collections.Generic;
using System.Text;

namespace DateViewier.Models.Tests
{
    [TestClass]
    public class WorkweekInfoTests
    {
        [TestMethod]
        [DataRow(2, 2)]
        [DataRow(5, 2)]
        public void InitializeTest(int workdays, int dayoffs)
        {
            WorkweekInfo info = new WorkweekInfo(workdays, dayoffs);
            Assert.AreEqual(workdays, info.WorkdaysCount);
            Assert.AreEqual(dayoffs, info.DayOffsCount);
        }

        [TestMethod]
        [DataRow(2, 2)]
        [DataRow(5, 2)]
        public void GetWorkweekLenghtTest(int workdays, int dayoffs)
        {
            WorkweekInfo info = new WorkweekInfo(workdays, dayoffs);
            Assert.AreEqual(workdays + dayoffs, info.WorkweekLength);
        }
    }
}
