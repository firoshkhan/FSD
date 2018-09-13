using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment23_WebAPI.Models
{
    public class SUPPLIERModel
    {
        [Key]
        public string SUPLNo { get; set; }
        public string SUPLName { get; set; }
        public string SUPLAddr { get; set; }

    }
}