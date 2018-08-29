using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SpongeBot.Resources.Database
{
    public class Clam
    {
        [Key]
        public ulong UserID { get; set; }
        public int Amount { get; set; }
    }
}
