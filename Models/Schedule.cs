using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaClub.Models
{
    public class Schedule
    {
        public int Id { get; set; }

        public int ContractTimeMinutes { get; set; }

        public DateTime Date { get; set; }

        public Boolean IsFullDayAbsence { get; set; }

        public String Name { get; set; }

        public Guid PersonId { get; set; }

        public List<Activity> Projection { get; set; }
    }
}
