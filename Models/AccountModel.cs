using System;
using System.ComponentModel.DataAnnotations;

namespace AccountCalc.Server
{
    public class AccountModel
    {
        [Key]
        public int? uid { get; set; }
        public long? timestamp { get; set; }

        public string? genre { get; set; }

        public decimal? account { get; set; }

        public string? account_state { get; set; }
    }
}
