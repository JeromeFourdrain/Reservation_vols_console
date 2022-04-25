using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_vols
{
    internal class Compagnie_Aerienne
    {
        public string Name { get; private set; } //Nom des propriétés en anglais et première lettre majuscule
        //private set = on ne peut pas modifier le Name ailleurs que dans la classe donc il faut passer par le constructeur

        public List<Flight> Company_Flights { get; set; }
        public Compagnie_Aerienne(string Name)
        {
            this.Name = Name;
        }
    }
}
