using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaClub.Models
{
    public class Activity
    {
        public int Id { get; set; }

        public string Color { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public int minutes { get; set; }

        public int ScheduleId { get; set; }

        public Schedule Schedule { get; set; }
    }
}
