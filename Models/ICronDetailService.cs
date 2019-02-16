using System;
using System.Collections.Generic;
using System.Linq;

namespace MementoScraperApi.Models {
    public interface ICronDetailService {
        IEnumerable<CronDetail> GetAll();
        CronDetail Create(CronDetail cron);
        void Update(CronDetail cron);
        void Delete(int id);
        List<CronDetail> GetMementosBy(int userId);
    }
}