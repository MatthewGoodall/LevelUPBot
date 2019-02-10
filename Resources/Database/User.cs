using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LevelUP.Resources.Database
{
    class User
    {
        [Key]
        public ulong UserID { get; set; }
        public int Stones { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
    }
}
