using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_vols.CRUD
{
    internal class TicketDB : ConnectionDB
    {
        public void Insert(Ticket ticket)
        {
            //COde qui insère un aéroport dans la DB
        }

        public Ticket GetById(int id)
        {
            //Code qui va récupérer un aéroport dans la DB
            return null;
        }

        public List<Ticket> GetAll()
        {
            //Code qui va récupérer tous les aéroports
            return null;
        }

        public void Update(Ticket ticket)
        {

        }

        public void Delete(Ticket ticket)
        {

        }
    }
}
