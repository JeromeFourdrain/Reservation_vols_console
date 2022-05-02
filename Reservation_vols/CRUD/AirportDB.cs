using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Reservation_vols.CRUD
{
    internal class AirportDB : ConnectionDB
    {

        public void Insert(Airport airport)
        {
            using (NpgsqlConnection c = new NpgsqlConnection(ConnectionString))
            {
                using (NpgsqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO airports (name, address) VALUES (@name,@address) RETURNING airportid";

                    NpgsqlParameter Pname = new NpgsqlParameter()
                    {
                        ParameterName = "name",
                        Value = airport.Name
                    };
                    NpgsqlParameter Paddress = new NpgsqlParameter()
                    {
                        ParameterName = "address",
                        Value = airport.Address
                    };

                    cmd.Parameters.Add(Pname);
                    cmd.Parameters.Add(Paddress);   

                    c.Open();
                    airport.AirportId = (int)cmd.ExecuteScalar(); //On récupère une valeur (id) donc on utilise ExecuteScalar
                    //cmd.ExecuteNonQuery(); Renvoie le nombre de lignes modifiées
                }
                
            }

        }

        public Airport GetById(int id)
        {
            Airport airport = null;
            using (NpgsqlConnection c = new NpgsqlConnection(ConnectionString))
            {
                using(NpgsqlCommand cmd = c.CreateCommand())
                {
                    
                    cmd.CommandText = "SELECT airportid, name, address FROM airports WHERE airportid = @airportid";

                    c.Open();

                    NpgsqlParameter p = new NpgsqlParameter
                    {
                        ParameterName="airportid",
                        Value = id
                    };
                    cmd.Parameters.Add(p);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();

                        airport = new Airport((int)reader[0], (string)reader[1], (string)reader[2]);

                    }

                }
                
            }
            return airport;
        }

        public List<Airport> GetAll()
        {
            List<Airport> airports = new List<Airport>();
            
            using(NpgsqlConnection c = new NpgsqlConnection(ConnectionString))
            {
                using(NpgsqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT airportid, name, address FROM airports";
                    c.Open();

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Airport airport = new Airport((int)reader[0], (string)reader[1], (string)reader[2]);
                            airports.Add(airport);
                        }
                    }
                }
            }


            return airports;
        }

        public void Update(Airport airport)
        {

        }

        public void Delete(Airport airport)
        {

        }


    }
}
