using DateViewier.AndroidApp.Repositories;
using DateViewier.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DateViewier.AndroidApp.Services
{
    public class WorkweekInfoService
    {
        private IInstanceRepistory<WorkweekInfo> workweekInfoRepository;

        public WorkweekInfoService(IInstanceRepistory<WorkweekInfo> workweekInfoRepository)
        {
            this.workweekInfoRepository = workweekInfoRepository;
        }

        public void SetWorkdays(int workdaysCount)
        {
            var workweekInfo = workweekInfoRepository.Load();
            workweekInfoRepository.Save(new WorkweekInfo(workdaysCount, workweekInfo.DayOffsCount));
        }

        public void SetDayOffs(int daysOffCount)
        {
            var workweekInfo = workweekInfoRepository.Load();
            workweekInfoRepository.Save(new WorkweekInfo(workweekInfo.WorkdaysCount, daysOffCount));
        }
    }
}
