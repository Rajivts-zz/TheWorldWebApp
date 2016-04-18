using System;
using System.ComponentModel.DataAnnotations;

namespace TheWorld.ViewModel
{
    public class StopViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The Name field is required and hence cannot be empty")]
        [StringLength(255, MinimumLength = 5)]
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        [Required]
        public DateTime Arrival { get; set; }
    }
}