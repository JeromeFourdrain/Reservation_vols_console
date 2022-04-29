// See https://aka.ms/new-console-template for more information


using Reservation_vols; //on récupère le nom du namespace qui englobe les classes dont on a besoin
using Npgsql;
using Reservation_vols.CRUD;


int choix = 0;
bool verif = false;
bool quit = false;

Compagnie_Aerienne compagnie = new Compagnie_Aerienne("Brussel Airlines"); //Appel du constructeur de la classe COmpagnie a"érienne
List<Airport> airports = new List<Airport>();
List<Client> clients = new List<Client>();
List<Flight> flights = new List<Flight>();
List<Passenger> passengers = new List<Passenger>();
List<Ticket> tickets = new List<Ticket>();

Random rnd = new Random(); //Objet qui va générer des nombres aléatoires
//GenerateTestDatas();



//ClientDB test = new ClientDB();

//clients.AddRange(test.GetAll());

//foreach(Client client in clients)
//{
//    test.Insert(client);
//}

do
{
    Console.WriteLine("Faites votre choix :"); 
    Console.WriteLine("1) Ajouter un aéroport \n 2) Afficher les aéroports \n 3) Ajouter un client \n 4) Afficher les clients \n 5) Ajouter un vol \n 6) Afficher les vols \n 7) Ajouter une réservation \n 8) Afficher les réservations");
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

void AddTicket()
{

    int choix = 0;
    int choix2 = 0;
    string confirmation = "false";
    bool otherpassenger = false;

    Passenger passenger = null;

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

            passenger = new Passenger(firstname, lastname, address, birthdate, phonenumber);
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
    Passenger passenger = new Passenger("Marsilius", "Routhier", "Place Léopold 220 3020 Herent", Convert.ToDateTime("10/03/97"), "0494 51 65 83");
    passengers.Add(passenger);
    passenger = new Passenger("Thiery", "Vachon", "Booischotseweg 398 5370 Jeneffe", Convert.ToDateTime("25/12/55"), "0496 39 81 70");
    passengers.Add(passenger);
    passenger = new Passenger("Gaston", "Mainville", "Rue de Sy 204 5370 Flostoy", Convert.ToDateTime("29/10/97"), "0482 68 67 26");
    passengers.Add(passenger);
    passenger = new Passenger("Charmaine", "Arcouet", "Avenue Emile Vandervelde 197 7623 Rongy", Convert.ToDateTime("14/12/68"), "0498 46 51 07");
    passengers.Add(passenger);
    passenger = new Passenger("Mirabelle", "Caouette", "Rue de Baras 220 3272 Testelt", Convert.ToDateTime("13/06/61"), "0487 99 43 57");
    passengers.Add(passenger);
    passenger = new Passenger("Grégoire", "Couture", "Rue du Château 262 1357 Linsmeau", Convert.ToDateTime("27/09/82"), "0483 32 99 15");
    passengers.Add(passenger);
    passenger = new Passenger("Laurence", "Bélair", "Rue Fosse Piron 283 4780 Sankt Vith", Convert.ToDateTime("22/07/62"), "0488 42 10 48");
    passengers.Add(passenger);
    passenger = new Passenger("Corinne", "Sarrazin", "Herentalsebaan 154 1140 Brussel", Convert.ToDateTime("14/12/79"), "0472 25 66 43");
    passengers.Add(passenger);
    passenger = new Passenger("Babette", "Bourgouin", "Rue du Stade 323 5570 Beauraing", Convert.ToDateTime("11/01/80"), "0475 55 02 85");
    passengers.Add(passenger);

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

    List<Passenger> passengerstmp = new List<Passenger>(passengers);

    for (int i = 0; i < rnd.Next(1,passengers.Count()); i++)
    {
        Passenger passenger = passengerstmp[rnd.Next(passengerstmp.Count())];
        Ticket ticket = new Ticket(flight, passenger, client);
        tickets.Add(ticket);
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
}

DateTime GenerateDate()
{
    DateTime end = new DateTime(2024,1,1);
    int range = (end - DateTime.Today).Days;
    return  DateTime.Today.AddDays(rnd.Next(range));
}
    