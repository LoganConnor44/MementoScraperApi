using System;
using System.Collections.Generic;
using System.Linq;
using MementoScraperApi.Database;
using MementoScraperApi.Exceptions;

namespace MementoScraperApi.Models {
    public class CronDetailService : ICronDetailService {
        private DataContext _context;

        public CronDetailService(DataContext context) {
            this._context = context;
        }

        public IEnumerable<CronDetail> GetAll() {
            return this._context.CronDetails;
        }

        public List<CronDetail> GetMementosBy(int userId) {
            return this._context.CronDetails.Where(x => x.UserId == userId).ToList();
        }

        public CronDetail Create(CronDetail cron) {
            this._context.CronDetails.Add(cron);
            this._context.SaveChanges();
            return cron;
        }

        public void Update(CronDetail cron) {
            this._context.CronDetails.Update(cron);
            this._context.SaveChanges();
        }

        public void Delete(int id) {
            var cron = this._context.CronDetails.Find(id);
            if (cron != null) {
                this._context.CronDetails.Remove(cron);
                this._context.SaveChanges();
            }
        }
    }
}