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
                }
                
            }

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
