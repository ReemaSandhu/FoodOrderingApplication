using System;
using System.ComponentModel.DataAnnotations;

namespace mvcEntities.Entities
{
    public partial class Reservation
    {
        public long ReservationId { get; set; }
       
        public string Name { get; set; }
       
        public int NoOfPeople { get; set; }
        
        public DateTime DateOfReservation { get; set; }
    }
}
