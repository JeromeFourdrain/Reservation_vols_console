using Npgsql;

namespace Reservation_vols.CRUD
{
    internal class FlightDB : ConnectionDB
    {
        public void Insert(Flight flight)
        {
            using (NpgsqlConnection c = new NpgsqlConnection(ConnectionString))
            {
                using (NpgsqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = ($"INSERT INTO flights (airport_departure_id, airport_arrival_id, isopen, date_departure, date_arrival) VALUES ({flight.Departure_Airport.AirportId}, {flight.Arrival_Airport.AirportId}, @isopen, @datedep, @datearr) RETURNING flightid");

                    NpgsqlParameter Pisopen = new NpgsqlParameter()
                    {
                        ParameterName = "isopen",
                        Value = flight.IsOpen
                    };
                    NpgsqlParameter Pdatedep = new NpgsqlParameter()
                    {
                        ParameterName = "datedep",
                        Value = flight.Date_Departure
                    };
                    NpgsqlParameter Pdatearr = new NpgsqlParameter()
                    {
                        ParameterName = "datearr",
                        Value = flight.Date_Arrival
                    };

                    cmd.Parameters.Add(Pisopen);
                    cmd.Parameters.Add(Pdatedep);
                    cmd.Parameters.Add(Pdatearr);

                    c.Open();
                    flight.FlightId = (int)cmd.ExecuteScalar();
                }
            }
        }

        public Flight GetById(int id)
        {
            //Code qui va récupérer un aéroport dans la DB
            return null;
        }

        public List<Flight> GetAll()
        {
            List<Flight> flights = new List<Flight>();

            using (NpgsqlConnection c = new NpgsqlConnection(ConnectionString))
            {
                using (NpgsqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT flightid, isopen, date_departure, date_arrival," +
                                      "airport_departure_id, dep.name AS departure_name, dep.address AS departure_address," +
                                      "airport_arrival_id, arr.name AS arrival_name, arr.address AS arrival_address " +
                                      "FROM flights " +
                                      "JOIN airports AS dep ON airport_departure_id = dep.airportid " +
                                      "JOIN airports AS arr ON airport_arrival_id = arr.airportid " +
                                      "WHERE isopen = true ;";

                    c.Open();
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Airport airport_departure = new Airport((int)reader[4], (string)reader[5], (string)reader[6]);
                            Airport airport_arrival = new Airport((int)reader[7], (string)reader[8], (string)reader[9]);
                            Flight flight = new Flight((DateTime)reader[2], (DateTime)reader[3], airport_departure, airport_arrival, (int)reader[0]);
                            flights.Add(flight);
                        }
                    }
                }
            }

            return flights;
        }

        public void Update(Flight flight)
        {

        }

        public void Delete(Flight flight)
        {
            using (NpgsqlConnection c = new NpgsqlConnection(ConnectionString))
            {
                using (NpgsqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = $"UPDATE flights SET isopen = false WHERE flightid = {flight.FlightId};";

                    c.Open();
                    int rows = cmd.ExecuteNonQuery();
                }
            }
        }

        public void ClosePastFlights()
        {
            using (NpgsqlConnection c = new NpgsqlConnection(ConnectionString))
            {
                using(NpgsqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "UPDATE flights SET isopen = false WHERE date_departure<NOW();";

                    c.Open();
                    int rows = cmd.ExecuteNonQuery();
                }
            }

                
        }
    }
}
