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
                    cmd.CommandText = ($"INSERT INTO tickets (isconfirmed, flight_id, passenger_id, client_id) VALUES (@iscon, {ticket.Flight.FlightId}, {ticket.Passenger.ClientId}, {ticket.Client.ClientId}) RETURNING ticketid");

                    NpgsqlParameter Pisconfirmed = new NpgsqlParameter()
                    {
                        ParameterName = "iscon",
                        Value = ticket.IsConfirmed
                    };
                    cmd.Parameters.Add(Pisconfirmed);

                    c.Open();
                    ticket.TicketId = (int)cmd.ExecuteScalar();
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
            List<Ticket> tickets = new List<Ticket>();

            using (NpgsqlConnection c = new NpgsqlConnection(ConnectionString))
            {
                using (NpgsqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM vw_tickets;";

                    c.Open();
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AirportDB db = new AirportDB();
                            Airport airport_departure = db.GetById((int)reader[16]);
                            Airport airport_arrival = db.GetById((int)reader[17]);
                            Flight flight = new Flight((DateTime)reader[14], (DateTime)reader[15], airport_departure, airport_arrival, (int)reader[12]);
                            Client client = new Client((string)reader[1], (string)reader[2], (string)reader[3], (DateTime)reader[4], (string)reader[5], (int)reader[0]);
                            Client passenger = new Client((string)reader[7], (string)reader[8], (string)reader[9], (DateTime)reader[10], (string)reader[11], (int)reader[6]);
                            Ticket ticket = new Ticket(flight,passenger,client);
                            tickets.Add(ticket);
                        }
                    }
                }
                return tickets;
            }
        }

        public void Update(Ticket ticket)
        {

        }

        public void Delete(Ticket ticket)
        {

        }
    }
}
