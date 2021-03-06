// See https://aka.ms/new-console-template for more information


using Reservation_vols; //on récupère le nom du namespace qui englobe les classes dont on a besoin
using Npgsql;
using Reservation_vols.CRUD;


FlightDB flightDB = new FlightDB();


/// <summary>
/// Airports
/// </summary>
flightDB.ClosePastFlights();



/// <summary>
/// Airports
/// </summary>
List<Airport> airports = new List<Airport>();

AirportDB airportDB = new AirportDB();

airports.AddRange(airportDB.GetAll());

/// <summary>
/// Clients
/// </summary>
List<Client> clients = new List<Client>();

ClientDB clientDB = new ClientDB();

clients.AddRange(clientDB.GetAll());

/// <summary>
/// Flights
/// </summary>
List<Flight> flights = new List<Flight>();

flights.AddRange(flightDB.GetAll());

/// <summary>
/// Tickets
/// </summary>
List<Ticket> tickets = new List<Ticket>();

TicketDB ticketDB = new TicketDB();

tickets.AddRange(ticketDB.GetAll());

List<Passenger> passengers = new List<Passenger>();


int choix = 0;
bool verif = false;
bool quit = false;

Random rnd = new Random(); //Objet qui va générer des nombres aléatoires
//GenerateTestDatas();

do
{
    Console.WriteLine("Faites votre choix :"); 
    Console.WriteLine("1) Ajouter un aéroport \n 2) Afficher les aéroports \n 3) Ajouter un client \n 4) Afficher les clients \n 5) Ajouter un vol \n 6) Afficher les vols \n 7) Ajouter une réservation \n 8) Afficher les réservations \n 9) Supprimer un vol \n 10) Modifier un client ");
    verif = int.TryParse(Console.ReadLine(), out choix);
    switch (choix)
    {
        case 1:
            AddAirport();
            break;
        case 2:
            DisplayAirports();
            break;
        case 3:
            AddClient();
            break;
        case 4:
            DisplayClients();
            break;
        case 5:
            AddFlight();
            break;
        case 6:
            DisplayFlights();
            break;
        case 7:
            AddTicket();
            break;
        case 8:
            DisplayTickets();
            break;
        case 9:
            DeleteFlight();
            break;
        case 10:
            UpdateClient();
            break;
        default:
            break;
    }

    Console.WriteLine("Voulez vous quitter ? (oui/non)");
    string answer = Console.ReadLine().ToLower();


    switch (answer)
    {
        case "oui":
            quit = true;
            break;
        case "o":
            quit = true;
            break;
        default:
            quit = false;
            break;
    }

    Console.Clear();

} while (!quit);




void AddAirport()
{
    Console.WriteLine("Veuillez encoder le nom de l'aéroport :");
    string name = Console.ReadLine();
    Console.WriteLine("Veuillez encoder l'adresse de l'aéroport : "+name);
    string address = Console.ReadLine();
    Airport airport1 = new Airport(name, address);
    airports.Add(airport1);

    AirportDB airportdb = new AirportDB();
    airportdb.Insert(airport1);

    Console.WriteLine("Aéroport correctement ajouté !");

}

void DisplayAirports()
{
    if (airports.Any()) 
    {
        foreach (Airport airport in airports)
        {
            Console.WriteLine(airport);
        }
    }
    else
    {
        Console.WriteLine("Pas d'aéroports encodés !");
    }
    
}

void AddClient()
{
    Console.WriteLine("Veuillez encoder le prénom du client :");
    string firstname = Console.ReadLine();
    Console.WriteLine("Veuillez encoder le nom du client :");
    string lastname = Console.ReadLine();
    Console.WriteLine("Veuillez encoder l'adresse du client : " + firstname + " " + lastname);
    string address = Console.ReadLine();
    Console.WriteLine($"Veuillez encoder la date naissance de {firstname} {lastname} (JJ/MM/AA)"); //TODO : Exception mauvais format de date
    DateTime birthdate = Convert.ToDateTime(Console.ReadLine());
    Console.WriteLine("Veuillez encoder le numéro de téléphone :");
    string phonenumber = Console.ReadLine();

    Client client = new Client(firstname, lastname, address, birthdate, phonenumber);
    clients.Add(client);

    ClientDB clientdb = new ClientDB();
    clientdb.Insert(client);

    Console.WriteLine("Client correctement ajouté !");

}


void DisplayClients()
{
    if (clients.Any())
    {
        foreach (Client client in clients)
        {
            Console.WriteLine(client);
        }
    }
    else
    {
        Console.WriteLine("Pas de clients encodés !");
    }

}

void AddFlight()
{
    if (airports.Count() >= 2)
    {
        int choix;
        bool verif;
        Console.WriteLine($"Veuillez encoder la date et l'heure de départ du vol : (JJ/MM/AA hh:mm)"); //TODO : Exception mauvais format de date
        DateTime Date_Departure = Convert.ToDateTime(Console.ReadLine());
        Airport departure_airport = ChoiceAirport("départ");
        DateTime Date_Arrival = new DateTime();
        do
        {
            Console.WriteLine($"Veuillez encoder la date et l'heure d'arrivée du vol : (JJ/MM/AA hh:mm)"); //TODO : Exception mauvais format de date
            Date_Arrival = Convert.ToDateTime(Console.ReadLine());
        } while (Date_Arrival <=  Date_Departure);

        Airport arrival_airport = ChoiceAirport("arrivée", departure_airport);
        Flight flight = new Flight(Date_Departure, Date_Arrival, departure_airport, arrival_airport);
        flights.Add(flight);
        

        FlightDB flightdb = new FlightDB();
        flightdb.Insert(flight);

        Console.WriteLine("Vol correctement ajouté !");

    }
    else
    {
        Console.WriteLine("Impossible d'ajouter un vol sans deux aéroports différents !!");
    }
}

void DisplayFlights()
{
    foreach (Flight flight in flights)
    {
        Console.WriteLine(flight);
    }

}

Airport ChoiceAirport(string type, Airport NotAvailable = null)
{
    int cpt = 1;
    int choice_anavailable = 0;
    foreach (Airport airport in airports)
    {

        if (airport != NotAvailable) Console.WriteLine($"Vol n° {cpt} :" + airport.ToString());
        else choice_anavailable = cpt;

        cpt++;
    }
    do
    {
        Console.WriteLine($"Quel est le numéro de l'aéroport de {type} :");
        verif = int.TryParse(Console.ReadLine(), out choix);
    } while (!verif || choix < 1 || choix > airports.Count() || choix == choice_anavailable);

    return airports[choix-1];
}

Flight ChoiceFlight(string type)
{
    int cpt = 1;
    int choix;

    foreach (Flight flight in flights)
    {

        Console.WriteLine($"Vol n° {cpt} :" + flight.ToString());
        cpt++;
    }
    do
    {
        Console.WriteLine($"Quel est le numéro de vol {type} :");
        verif = int.TryParse(Console.ReadLine(), out choix);
    } while (!verif || choix < 1 || choix > flights.Count());

    return flights[choix - 1];
}

void AddTicket()
{

    int choix = 0;
    int choix2 = 0;
    string confirmation = "false";
    bool otherpassenger = false;

    Client passenger = null;

    if (flights.Count() >= 1 && clients.Count() >= 1)
    {
        int cpt = 1;
        
        foreach (Client client in clients)
        {
            Console.WriteLine($"Client n° {cpt} " + client.ToString());
            cpt++;
        }
        do
        {
            Console.WriteLine("Quel client passe la réservation ?");
            bool verif = int.TryParse(Console.ReadLine(), out choix);
        } while (!verif || choix < 1 || choix > clients.Count());

        cpt = 1;
        foreach(Flight flight in flights)
        {
            Console.WriteLine($"Vol n° {cpt} " + flight.ToString());
            cpt++;
        }
        do
        {
            Console.WriteLine("Pour quel vol souhaitez vous réserver ?");
            bool verif = int.TryParse(Console.ReadLine(), out choix2);
        } while (!verif || choix2 < 1 || choix2 > flights.Count());

        Console.Clear();

        cpt = 1;

        do
        {
            otherpassenger = false;
            Console.WriteLine("Veuillez encoder le prénom du passager :");
            string firstname = Console.ReadLine();
            Console.WriteLine("Veuillez encoder le nom du passager :");
            string lastname = Console.ReadLine();
            Console.WriteLine("Veuillez encoder l'adresse du passager : " + firstname + " " + lastname);
            string address = Console.ReadLine();
            Console.WriteLine($"Veuillez encoder la date naissance de {firstname} {lastname} (JJ/MM/AA)"); //TODO : Exception mauvais format de date
            DateTime birthdate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Veuillez encoder le numéro de téléphone :");
            string phonenumber = Console.ReadLine();

            passenger = new Client(firstname, lastname, address, birthdate, phonenumber);
            clientDB.Insert(passenger);

            Console.Clear();
            Console.WriteLine("Nouveau ticket : ");
            Console.WriteLine(flights[choix2 - 1]);
            Console.WriteLine(passenger);
            Console.WriteLine("Confirmez vous les informations : (oui/non)");
            confirmation = Console.ReadLine();
            if(confirmation == "oui")
            {
                Ticket ticket = new Ticket(flights[choix2 - 1], passenger, clients[choix - 1]);
                tickets.Add(ticket);
                ticketDB.Insert(ticket);

            }
            Console.WriteLine("Est-ce qu'il y a un autre passager ? (oui/non)");
            confirmation = Console.ReadLine();
            if (confirmation == "oui")
                otherpassenger = true;
        } while (otherpassenger);
        
    }
    else
    {
        Console.WriteLine("Impossible de passer une réservation sans client ou sans vol encodés !!");
    }

}

void DisplayTickets()
{
    foreach(Ticket ticket in tickets)
    {
        Console.WriteLine(ticket);
    }
}

void GenerateTestDatas()
{
    Airport airport1 = new Airport("Brussels South Charleroi Airport", "Charleroi, Belgium" );
    Airport airport2 = new Airport("Aéroport de Bruxelles", "Zaventem");
    Airport airport3 = new Airport("Aéroport de Lille-Lesquin", "Lesquin, France");
    Airport airport4 = new Airport("Aéroport Roissy-Charles-De-Gaulle", "Paris");
    Airport airport5 = new Airport("Aéroport Barcelone-El Prat", "Barcelone, Espagne");
    Airport airport6 = new Airport("Aéroport international du roi Fahd", "Dammam, Arabie Saoudite");
    Airport airport7 = new Airport("Aéroport international de Denver", "Denver, Etats-unis");
    Airport airport8 = new Airport("Aéroport de Rome Fiumicino", "Rome, Latium, Italia");
    airports.Add(airport1);
    airports.Add(airport2);
    airports.Add(airport3);
    airports.Add(airport4);
    airports.Add(airport5);
    airports.Add(airport6);
    airports.Add(airport7);
    airports.Add(airport8);
    foreach(Airport airport in airports)
    {
        airportDB.Insert(airport);
    }
    Client client1 = new Client("Ethan", "Arix","Rue d'Estinnes, La louviere", Convert.ToDateTime("25/03/98"), "0478256423");
    Client client2 = new Client("Paul", "Pirotte", "Rue de Binche, Leval-Trahegnies", Convert.ToDateTime("14/02/91"), "0472254423");
    Client client3 = new Client("Joseph", "Assez", "Rue de Binche, Mont-St-Geneviève", Convert.ToDateTime("14/02/83"), "0468256423");
    Client client4 = new Client("Jérémy", "Lambrecq", "Rue de Leval, Binche", Convert.ToDateTime("28/12/99"), "0458216224");
    Client client5 = new Client("Gavin", "Chaineux", "Rue de loin d'ici, Far far away", Convert.ToDateTime("10/03/91"), "046164223");
    Client client6 = new Client("Anthony", "Paduwat", "Rue de Mons, La louviere", Convert.ToDateTime("14/04/00"), "0488256321");
    Client client7 = new Client("Brandon", "Limbourg", "Rue de La Louvière, Trivières", Convert.ToDateTime("15/02/98"), "0478716423");
    clients.Add(client1);
    clients.Add(client2);
    clients.Add(client3);
    clients.Add(client4);
    clients.Add(client5);
    clients.Add(client6);
    clients.Add(client7);
    
    Client passenger = new Client("Marsilius", "Routhier", "Place Léopold 220 3020 Herent", Convert.ToDateTime("10/03/97"), "0494 51 65 83");
    clients.Add(passenger);
    passenger = new Passenger("Thiery", "Vachon", "Booischotseweg 398 5370 Jeneffe", Convert.ToDateTime("25/12/55"), "0496 39 81 70");
    clients.Add(passenger);
    passenger = new Passenger("Gaston", "Mainville", "Rue de Sy 204 5370 Flostoy", Convert.ToDateTime("29/10/97"), "0482 68 67 26");
    clients.Add(passenger);
    passenger = new Passenger("Charmaine", "Arcouet", "Avenue Emile Vandervelde 197 7623 Rongy", Convert.ToDateTime("14/12/68"), "0498 46 51 07");
    clients.Add(passenger);
    passenger = new Passenger("Mirabelle", "Caouette", "Rue de Baras 220 3272 Testelt", Convert.ToDateTime("13/06/61"), "0487 99 43 57");
    clients.Add(passenger);
    passenger = new Passenger("Grégoire", "Couture", "Rue du Château 262 1357 Linsmeau", Convert.ToDateTime("27/09/82"), "0483 32 99 15");
    clients.Add(passenger);
    passenger = new Passenger("Laurence", "Bélair", "Rue Fosse Piron 283 4780 Sankt Vith", Convert.ToDateTime("22/07/62"), "0488 42 10 48");
    clients.Add(passenger);
    passenger = new Passenger("Corinne", "Sarrazin", "Herentalsebaan 154 1140 Brussel", Convert.ToDateTime("14/12/79"), "0472 25 66 43");
    clients.Add(passenger);
    passenger = new Passenger("Babette", "Bourgouin", "Rue du Stade 323 5570 Beauraing", Convert.ToDateTime("11/01/80"), "0475 55 02 85");
    clients.Add(passenger);

    foreach (Client client in clients)
    {
        clientDB.Insert(client);
    }

    for (int i = 0; i < 5; i++)
    {
        GenerateFlight();
    }

    for (int i = 0; i < 5; i++)
    {
        GenerateTicket();
    }
    


}

void GenerateTicket()
{
    Flight flight = flights[rnd.Next(flights.Count())];

    Client client = clients[rnd.Next(clients.Count())];

    List<Client> passengerstmp = new List<Client>(clients);

    for (int i = 0; i < rnd.Next(1,clients.Count()); i++)
    {
        Client passenger = passengerstmp[rnd.Next(passengerstmp.Count())];
        Ticket ticket = new Ticket(flight, passenger, client);
        tickets.Add(ticket);
        ticketDB.Insert(ticket);
        passengerstmp.Remove(passenger);
    }
    
}

void GenerateFlight()
{
    DateTime date_departure = GenerateDate();
    DateTime date_arrival;

    do
    {
        date_arrival = GenerateDate();
    } while (date_arrival <= date_departure);

    Airport airport_departure = airports[rnd.Next(airports.Count)];
    Airport airport_arrival;

    do
    {
        airport_arrival = airports[rnd.Next(airports.Count)];
    } while (airport_arrival == airport_departure);

    Flight flight = new Flight(date_departure, date_arrival, airport_departure, airport_arrival);
    flights.Add(flight);
    flightDB.Insert(flight);
}

DateTime GenerateDate()
{
    DateTime end = new DateTime(2024,1,1);
    int range = (end - DateTime.Today).Days;
    return  DateTime.Today.AddDays(rnd.Next(range));
}

void DeleteFlight()
{
    Flight flight = ChoiceFlight("à supprimer");

    flightDB.Delete(flight);
    flights.Remove(flight);

}

void UpdateClient()
{
    Client client = null;
    int choixModif;
    bool quit = false;
    bool verif = false;

    client = ChoiceClient();

    do
    {
        Console.WriteLine("Que voulez vous modifier ?: ");
        Console.WriteLine($@"1- Prenom : {client.FirstName}
2- Nom : {client.LastName}
3- Addresse : {client.Address}
4- Date de naissance : {client.BirthDate}
5- Numéro de téléphone : {client.PhoneNumber}
6- Confirmer ");
        do
        {
            verif = int.TryParse(Console.ReadLine(), out choixModif);
        } while (!verif || choixModif < 1 || choixModif > 6);


        switch (choixModif)
        {
            case 1:
                Console.WriteLine("Encodez son nouveau prénom :");
                client.FirstName = Console.ReadLine();
                break;
            case 2:
                Console.WriteLine("Encodez son nouveau nom :");
                client.LastName = Console.ReadLine();
                break;
            case 3:
                Console.WriteLine("Encodez sa nouvelle addresse :");
                client.Address = Console.ReadLine();
                break;
            case 4:
                Console.WriteLine("Encodez sa nouvelle date de naissance :");
                client.BirthDate = Convert.ToDateTime(Console.ReadLine());
                break;
            case 5:
                Console.WriteLine("Encodez son nouveau numéro de téléphone :");
                client.PhoneNumber = Console.ReadLine();
                break;
            case 6:
                quit = true;
                break;
            default:
                break;
        }
        Console.Clear();

    } while (!quit);

    clientDB.Update(client);
}

Client ChoiceClient()
{
    int cpt = 1;
    int choix;


    foreach(Client clienttmp in clients)
    {
        Console.WriteLine($"{cpt} : Prénom : {clienttmp.FirstName} Nom : {clienttmp.LastName}");
        cpt++;
    }
    do {
        Console.WriteLine("Quel client souhaitez vous modifier ?");
        verif = int.TryParse(Console.ReadLine(), out choix);
    } while (!verif || choix < 1 || choix > clients.Count());

    return clients[choix - 1];
}
