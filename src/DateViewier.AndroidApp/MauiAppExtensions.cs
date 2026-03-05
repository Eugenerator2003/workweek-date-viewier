using DateViewier.AndroidApp.Repositories;
using DateViewier.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DateViewier.AndroidApp
{
    public static class MauiAppExtensions
    {
        public static void SetTwoTwoWorkweek(this MauiApp app)
        {
            var infoRepository = app.Services.CreateScope()
                                   .ServiceProvider
                                   .GetRequiredService<IInstanceRepistory<WorkweekInfo>>();
            var info = new WorkweekInfo(2, 2);
            infoRepository.Save(info);
        }
    }
}
