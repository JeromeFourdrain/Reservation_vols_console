using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_vols
{
    internal class Passenger : Client
    {
        public Passenger(string FirstName, string LastName, string Address, DateTime Birthdate, string PhoneNumber) : base(FirstName, LastName, Address, Birthdate, PhoneNumber)
        {
        }

        public int PassengerId { get; set; }

    }
}
