using System;

namespace MementoScraperApi.Models {
    public class CronDetail {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string Frequency { get; set; }
        public string Hashtag { get; set; }
        public bool Facebook { get; set; }
        public bool Twitter { get; set; }
        public bool Instagram { get; set; }

        /// <summary>
        /// Modification Time Stamp.
        /// </summary>
        /// <value></value>
        private DateTime _modified;
        public DateTime Modification {
            get {
                return _modified;
            }
            set {
                _modified = DateTime.UtcNow;
            }
        }
        /// <summary>
        /// Creation Time Stamp.
        /// </summary>
        /// <value></value>
        private DateTime _creation;
        public DateTime Creation {
            get {
                return _creation;
            }
            set {
                _creation = DateTime.UtcNow;
            }
        }
    }
}