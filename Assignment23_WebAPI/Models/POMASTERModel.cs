using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assignment23_WebAPI.Models
{
    public class POMASTERModel
    {
        [Key]
        public string PONo { get; set; }
        public string PODate{ get; set; }
     

        // Foreign key 
        [Display(Name = "SuplNo")]
        public string SuplNo { get; set; }

        [ForeignKey("SUPLNo")]
        public virtual SUPPLIERModel SUPPLIERModels { get; set; }

      //  public ICollection<PODETAILModel> POdetals { get; set; }

    }
}