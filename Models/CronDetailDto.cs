using System;

namespace MementoScraperApi.Models {
    public class CronDetailDto {
        public int UserId { get; set; }
        public string Frequency { get; set; }
        public string Hashtag { get; set; }
        public bool Facebook { get; set; }
        public bool Twitter { get; set; }
        public bool Instagram { get; set; }
        public DateTime Creation {get; set; }
    }
}