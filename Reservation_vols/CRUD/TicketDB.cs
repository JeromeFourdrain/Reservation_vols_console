using Npgsql;
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
            using (NpgsqlConnection c = new NpgsqlConnection(ConnectionString))
            {
                using (NpgsqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = ($"INSERT INTO t_ticket (ticket_isconfirmed, ticket_flight_id, ticket_passenger_id, ticket_client_id) VALUES (@iscon, {ticket.Flight.FlightId}, {ticket.Passenger.ClientId}, {ticket.Client.ClientId}) RETURNING ticket_id");

                    NpgsqlParameter Pisconfirmed = new NpgsqlParameter()
                    {
                        ParameterName = "iscon",
                        Value = ticket.IsConfirmed
                    };

                    cmd.Parameters.Add(Pisconfirmed);
                    ticket.TicketId = (int)cmd.ExecuteScalar();
                }
            }
        }
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
