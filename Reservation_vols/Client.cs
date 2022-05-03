using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_vols
{
    internal class Client
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Address { get; set; }

        public DateTime BirthDate { get; set; }

        public string PhoneNumber { get; set; }


        public Client(string FirstName, string LastName, string Address, DateTime Birthdate, string PhoneNumber, int ClientId = 0)
        {
            this.ClientId = ClientId;  
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Address = Address;
            this.BirthDate = Birthdate;
            this.PhoneNumber = PhoneNumber;
        }

        public override string ToString()
        {
            return String.Format($"Nom : {LastName} Prénom : {FirstName} \n Adresse : {Address} Date de naissance : {BirthDate} Numéro de téléphone : {PhoneNumber} ");
        }


    }
}
