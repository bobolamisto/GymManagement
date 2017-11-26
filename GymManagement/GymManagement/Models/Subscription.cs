using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GymManagement.Models
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public int Validity { get; set; } // in days

        //abonamentul corespunde unui pachet/ unei oferte
        public int PackageId { get; set; }
        public virtual Package Package { get; set; }
    }
}