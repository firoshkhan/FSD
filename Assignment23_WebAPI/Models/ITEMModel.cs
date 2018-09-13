using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment23_WebAPI.Models
{
    public class ITEMModel
    {
        [Key]
        public string ITCODE { get; set; }
        public string ITDESC { get; set; }       
        public double ITRATE { get; set; }
    }
}