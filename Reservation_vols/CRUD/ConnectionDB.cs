using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_vols.CRUD
{
    internal abstract class ConnectionDB //abstract
    {
        protected const string ConnectionString = "Server=127.0.0.1;Port=5432;Database=Reservation_vols;User Id=postgres;Password=b6364b55ba47e8edbbfe4e2bf01e2256;";

    }
}
