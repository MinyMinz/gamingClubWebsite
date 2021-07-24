using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gaming_Club.Models
{
    public class Market
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SalesGUID { get; set; }
        public string ItemName { get; set; }
        [ForeignKey("Games")]
        public Guid GameGUID { get; set; }
        public virtual Games Games { get; set; }
        public string ItemDescription { get; set; }
        public int Quantity { get; set;}
        public double PPU { get; set;}
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> ListedDate { get; set; }
    }
}