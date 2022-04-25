using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_vols
{
    internal class Client
    {
        public int ClientId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public string Address { get; private set; }

        public DateTime BirthDate { get; private set; }

        public string PhoneNumber { get; private set; }


        public Client(string FirstName, string LastName, string Address, DateTime Birthdate, string PhoneNumber)
        {
            ClientId = 0;   
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Address = Address;
            this.BirthDate = BirthDate;
            this.PhoneNumber = PhoneNumber;
        }

        public override string ToString()
        {
            return String.Format($"Client n° : {ClientId} Nom : {LastName} Prénom : {FirstName} \n Adresse : {Address} Date de naissance : {BirthDate} Numéro de téléphone : {PhoneNumber} ");
        }

        //Test
    }
}
