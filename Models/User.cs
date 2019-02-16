using System.Collections.Generic;

namespace MementoScraperApi.Models {
    public class User {
        public int Id { get; set; }
        /// <summary>
        /// A list of CronDetails that are associated with a User.
        /// </summary>
        public List<CronDetail> CronDetails { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}