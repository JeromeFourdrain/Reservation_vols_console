using Npgsql;

namespace Reservation_vols.CRUD
{
    internal class FlightDB : ConnectionDB
    {
        public void Insert(Flight flight)
        {
            using (NpgsqlConnection c = new NpgsqlConnection(ConnectionString*))
            {
                using (NpgsqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = ($"INSERT INTO t_flight (flight_airport_departure, flight_airport_arrival, flight_isopen, flight_datedeparture, flight_datearrival) VALUES ({flight.Departure_Airport.AirportId}, {flight.Arrival_Airport.AirportId}, @isopen, @datedep, @datearr) RETURNING flight_id");

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
            Flight flight = null;
            using (NpgsqlConnection c = new NpgsqlConnection(ConnectionString))
            {
                using (NpgsqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT flightid, isopen, date_departure, date_arrival," +
                                      "airport_departure_id, dep.name AS departure_name, dep.address AS departure_address," +
                                      "airport_arrival_id, arr.name AS arrival_name, arr.address AS arrival_address" +
                                      "FROM flights" +
                                      "JOIN airports AS dep ON airport_departure_id = dep.airportid" +
                                      "JOIN airports AS arr ON airport_arrival_id = arr.airportid";

                    c.Open();
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            
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

        }
    }
}
