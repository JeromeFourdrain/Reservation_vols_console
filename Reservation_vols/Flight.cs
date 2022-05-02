using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_vols
{
    internal class Flight
    {
        public int FlightId { get; set; }

        public bool IsOpen { get; private set; }
        public DateTime Date_Departure { get; private set; }

        public DateTime Date_Arrival { get; private set; }

        public Airport Departure_Airport { get; private set; }

        public Airport Arrival_Airport { get; set; }

        public Flight(DateTime departure, DateTime arrival, Airport Departure_Airport, Airport Arrival_Airport, int flightid = 0)
        {
            this.FlightId = flightid;
            IsOpen = true;
            this.Date_Arrival = arrival;
            this.Date_Departure = departure;
            this.Departure_Airport = Departure_Airport;
            this.Arrival_Airport = Arrival_Airport;
        }

        public override string ToString()
        {
            return String.Format($"Date/heure de départ : {Date_Departure} \n" +
                $"Aéroport de départ : {Departure_Airport.Name} \n" +
                $"Date/heure d'arrivée : {Date_Arrival} \n" +
                $"Aéroport d'arrivée : {Arrival_Airport.Name}");
        }

    }
}
