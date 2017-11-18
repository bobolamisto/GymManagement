using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GymManagement.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Number of seats")]
        public int NumberOfSeats { get; set; }

        //planificarile cursurilor care se tin in aceasta sala
        public virtual ICollection<Scheduler> Schedulers { get; set; }

        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}