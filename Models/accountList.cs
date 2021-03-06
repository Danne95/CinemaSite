using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class accountList
    {
        public string type { get; set; }
        [Key] [Column(Order = 0)]
        public string username { get; set; }
        public string password { get; set; }


    }
}