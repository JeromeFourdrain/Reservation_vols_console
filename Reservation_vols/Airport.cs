using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_vols
{
    internal class Airport
    {
        public string Name { get; private set; }
        public string Address { get; private set; }

        public Airport(string Name, string Address)
        {
            this.Name = Name;
            this.Address = Address;
        }

        public override string ToString()
        {
            return String.Format("Name : {0} Addresse : {1}", this.Name, this.Address);
        }
    }
}
