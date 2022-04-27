using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_vols
{
    internal class Ticket
    {
        public int TicketId { get; private set; }
        public bool IsConfirmed { get; private set; }

        public Flight Flight { get; private set; }

        public Client Client  { get; private set; }

        public Passenger Passenger { get; private set; }

        public Ticket(Flight Flight, Passenger Passenger, Client Client)
        {
            TicketId = 0;

            IsConfirmed = true;

            this.Flight = Flight;

            this.Passenger = Passenger;
            this.Client = Client;   
        }

        public override string ToString()
        {
            return "Vol du ticket :" + this.Flight + "\n Passager : " + this.Passenger + "\n Nom de la réservation : " + this.Client;
        }
    }
}
