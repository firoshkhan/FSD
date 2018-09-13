using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assignment23_WebAPI.Models
{
    public class PODETAILModel
    {
        [Key, Column(Order = 0)]
        public string PONO { get; set; }
        [Key, Column(Order = 1)]
        public string ITCODE { get; set; }
        public int QTY { get; set; }

        [ForeignKey("PONO")]
        public virtual POMASTERModel POMASTERModels { get; set; }
        [ForeignKey("ITCODE")]
        public virtual ITEMModel ITEMModels { get; set; }

        //  public POMASTERModel Standard { get; set; }

    }
}