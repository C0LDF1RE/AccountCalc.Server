using System;
using System.ComponentModel.DataAnnotations;

namespace AccountCalc.Server
{
    public class AccountModel
    {
        [Key]
        public int id { get; set; }
        public DateTime createTime { get; set; }
        public long timestamp => (long)(createTime - new DateTime(1970, 1, 1)).TotalSeconds;
        public string genre { get; set; }
        public decimal account { get; set; }
        public string category { get; set; }
        public string statement { get; set; }
    }
}
