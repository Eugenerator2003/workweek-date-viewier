using DateViewier.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DateViewier.AndroidApp.Repositories
{
    public class WorkweekInfoPreferencesRepository
        : IInstanceRepistory<WorkweekInfo>
    {
        private static string _workdaysField = "workweek.workdays";
        private static string _daysOffsField = "workweek.dayOffs";

        public WorkweekInfo Load()
        {
            var workdaysCount = Preferences.Get(_daysOffsField, 1);
            var dayOffsCount = Preferences.Get(_workdaysField, 1);
            return new WorkweekInfo(workdaysCount, dayOffsCount);
        }

        public void Save(WorkweekInfo instance)
        {
            Preferences.Set(_workdaysField, instance.WorkdaysCount);
            Preferences.Set(_daysOffsField, instance.DayOffsCount);
        }
    }
}
