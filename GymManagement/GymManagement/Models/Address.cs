using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GymManagement.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        [Display(Name = "Street number")]
        public int StreetNumber { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        //salile de la aceasta adresa
        public virtual ICollection<Room> Rooms { get; set; }
    }
}