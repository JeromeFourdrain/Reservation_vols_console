using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_vols.CRUD
{
    internal class AirportDB : ConnectionDB
    {

        public void Insert(Airport airport)
        {
            //COde qui insère un aéroport dans la DB
        }

        public Airport GetById(int id)
        {
            //Code qui va récupérer un aéroport dans la DB
            return null;
        }

        public List<Airport> GetAll()
        {
            //Code qui va récupérer tous les aéroports
            return null;
        }

        public void Update(Airport airport)
        {

        }

        public void Delete(Airport airport)
        {

        }


    }
}
