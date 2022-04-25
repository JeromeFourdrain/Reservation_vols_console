// See https://aka.ms/new-console-template for more information


using Reservation_vols; //on récupère le nom du namespace qui englobe les classes dont on a besoin


int choix = 0;
bool verif = false;
bool quit = false;

Compagnie_Aerienne compagnie = new Compagnie_Aerienne("Brussel Airlines"); //Appel du constructeur de la classe COmpagnie a"érienne
List<Airport> airports = new List<Airport>();
List<Client> clients = new List<Client>();
List<Flight> flights = new List<Flight>();  

do
{
    Console.WriteLine("Faites votre choix :"); 
    Console.WriteLine("1) Ajouter un aéroport \n 2) Afficher les aéroports \n 3) Ajouter un client \n 4) Afficher les clients \n 5) AJouter un vol \n 6) Afficher les vols");
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
    Console.WriteLine();
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

    