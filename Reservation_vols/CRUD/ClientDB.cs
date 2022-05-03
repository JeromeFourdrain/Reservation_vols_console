using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Reservation_vols.CRUD
{
    internal class ClientDB : ConnectionDB
    {
        public void Insert(Client client)
        {
            using (NpgsqlConnection c = new NpgsqlConnection(ConnectionString))
            {
                using (NpgsqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO clients (firstname, lastname, address, birthdate, phonenumber) values (@firstname, @lastname, @address, @birthdate, @phonenumber) RETURNING clientid;";

                    NpgsqlParameter Pfirstname = new NpgsqlParameter()
                    {
                        ParameterName = "firstname",
                        Value = client.FirstName
                    };
                    NpgsqlParameter Plastname = new NpgsqlParameter()
                    {
                        ParameterName = "lastname",
                        Value = client.LastName
                    };
                    NpgsqlParameter Paddress = new NpgsqlParameter()
                    {
                        ParameterName = "address",
                        Value = client.Address
                    };
                    NpgsqlParameter Pbirthdate = new NpgsqlParameter()
                    {
                        ParameterName = "birthdate",
                        Value = client.BirthDate
                    };
                    NpgsqlParameter Pphonenumber = new NpgsqlParameter()
                    {
                        ParameterName = "phonenumber",
                        Value = client.PhoneNumber
                    };

                    cmd.Parameters.Add(Pfirstname);
                    cmd.Parameters.Add(Plastname);
                    cmd.Parameters.Add(Paddress);
                    cmd.Parameters.Add(Pbirthdate);
                    cmd.Parameters.Add(Pphonenumber);

                    c.Open();
                    client.ClientId = (int)cmd.ExecuteScalar();
                }
            }
        }

        public Client GetById(int id)
        {
            Client client = null;
            using (NpgsqlConnection c = new NpgsqlConnection(ConnectionString))
            {
                using (NpgsqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT clientId, firstname, lastname, address, birthdate, phonenumber FROM clients WHERE id = " + id;

                    c.Open();

                    using(NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int clientId = (int)reader[0];
                            string? firstname = reader[1].ToString();
                            string? lastname = reader[2].ToString();
                            string? address = reader[3].ToString();
                            DateTime birthdate = (DateTime) reader[4];
                            string? phonenumber = reader[5].ToString();
                            client = new Client(firstname, lastname, address, birthdate, phonenumber, clientId);
                        }
                    }
                }
            }

            return client;
        }

        public List<Client> GetAll()
        {
            List<Client> clients = new List<Client>();
            Client client = null;
            using (NpgsqlConnection c = new NpgsqlConnection(ConnectionString))
            {
                using (NpgsqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT clientId, firstname, lastname, address, birthdate, phonenumber FROM clients";

                    c.Open();
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int clientId = (int)reader[0];
                            string? firstname = (string)reader[1];
                            string? lastname = (string)reader[2];
                            string? address = (string)reader[3];
                            DateTime birthdate = (DateTime)reader[4];
                            string? phonenumber = (string)reader[5];
                            client = new Client(firstname, lastname, address, birthdate, phonenumber, clientId);
                            clients.Add(client);
                        }
                    }
                }
            }

            return clients;
        }

        public void Update(Client client)
        {
            using(NpgsqlConnection c = new NpgsqlConnection(ConnectionString))
            {
                using(NpgsqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = $"UPDATE clients SET firstname = @firstname, lastname = @lastname, address = @address, birthdate = @birthdate, phonenumber = @phonenumber WHERE clientid = {client.ClientId} ";

                    NpgsqlParameter Pfirstname = new NpgsqlParameter()
                    {
                        ParameterName = "firstname",
                        Value = client.FirstName
                    };
                    NpgsqlParameter Plastname = new NpgsqlParameter()
                    {
                        ParameterName = "lastname",
                        Value = client.LastName
                    };
                    NpgsqlParameter Paddress = new NpgsqlParameter()
                    {
                        ParameterName = "address",
                        Value = client.Address
                    };
                    NpgsqlParameter Pbirthdate = new NpgsqlParameter()
                    {
                        ParameterName = "birthdate",
                        Value = client.BirthDate
                    };
                    NpgsqlParameter Pphonenumber = new NpgsqlParameter()
                    {
                        ParameterName = "phonenumber",
                        Value = client.PhoneNumber
                    };

                    cmd.Parameters.Add(Pfirstname);
                    cmd.Parameters.Add(Plastname);
                    cmd.Parameters.Add(Paddress);
                    cmd.Parameters.Add(Pbirthdate);
                    cmd.Parameters.Add(Pphonenumber);

                    c.Open();
                    int rows = cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(Client client)
        {

        }
    }
}
