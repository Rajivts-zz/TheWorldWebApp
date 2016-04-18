using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheWorld.ViewModel
{
    public class TripViewModel
    {
        [Required]
        [StringLength(255, MinimumLength = 5)]
        public string Name { get; set; }
        public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public IEnumerable<StopViewModel> Stops { get; set; }
    }
}