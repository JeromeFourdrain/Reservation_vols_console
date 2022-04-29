using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_vols.CRUD
{
    internal class FlightDB : ConnectionDB
    {
        public void Insert(Flight flight)
        {
            //COde qui insère un aéroport dans la DB

        }

        public Flight GetById(int id)
        {
            //Code qui va récupérer un aéroport dans la DB
            return null;
        }

        public List<Flight> GetAll()
        {
            //Code qui va récupérer tous les aéroports
            return null;
        }

        public void Update(Flight flight)
        {

        }

        public void Delete(Flight flight)
        {

        }
    }
}
