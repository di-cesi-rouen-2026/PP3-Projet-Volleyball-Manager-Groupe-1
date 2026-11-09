Console.WriteLine("Variation du match");
Console.WriteLine("Vous êtes sur le point de lancer un match");

while (true)
{
    Console.WriteLine("Êtes-vous prêts ? (oui / non)");
    string? reponse = Console.ReadLine()?.Trim().ToLower();

    if (reponse == "oui")
    {
        break;
    }
    else if (reponse == "non")
    {
        Console.WriteLine("Bah pourquoi tu lances le jeu alors ? Allez à la niche");
        return;
    }
    else
    {
        Console.WriteLine("Mais ce n'est pas une réponse ça");
    }
}

Console.Clear();
Console.WriteLine("Début dans :");
for (int i = 3; i >= 1; i--)
{
    Console.WriteLine($"{i}...");
    Thread.Sleep(2000);
}
Console.Clear();

// Stats Joueur
int att = 70;
int def = 70;
int pass = 100;

// Stats IA
int attad = 70;
int defad = 70;
int passad = 100;

// Logique Score
int point = 0;
int pointad = 0;

// Gestion du service (true = Joueur sert, false = IA sert)
bool serviceJoueur = true;

// Phase de match (jusqu'à 21 points)
while (point < 21 && pointad < 21)
{
    bool echangeEnCours = false;
    bool tourAdversaire = false;

    if (serviceJoueur)
    {
        Console.WriteLine("--- SERVICE JOUEUR ---");
        int tossatt = Random.Shared.Next(1, 101);

        if (tossatt > att)
        {
            Console.WriteLine("Service de Merde ! Faute directe.");
            pointad++;
            serviceJoueur = false; // L'adversaire récupère le service
            Thread.Sleep(1000);
            Console.Clear();
        }
        else
        {
            Console.WriteLine("Service Réussi !");
            echangeEnCours = true;
            tourAdversaire = true; // L'adversaire doit réceptionner
            Thread.Sleep(1000);
            Console.Clear();
        }
    }
    else
    {
        Console.WriteLine("--- SERVICE ADVERSAIRE ---");
        int tossatt = Random.Shared.Next(1, 101);

        if (tossatt > attad)
        {
            Console.WriteLine("L'adversaire rate son service ! Faute directe.");
            point++;
            serviceJoueur = true; // Le joueur récupère le service
        }
        else
        {
            Console.WriteLine("L'adversaire réussit son service !");
            echangeEnCours = true;
            tourAdversaire = false; // Le joueur doit réceptionner
        }
    }

    Thread.Sleep(1000);
    Console.Clear();

    // Échange de balle
    while (echangeEnCours)
    {
        if (tourAdversaire)
        {
            // 1. Réception / Contrôle Adversaire
            int tossdef = Random.Shared.Next(1, 101);
            if (tossdef > defad)
            {
                Console.WriteLine("L'adversaire a raté son contrôle !");
                point++;
                serviceJoueur = true;
                Thread.Sleep(1000);
                Console.Clear();   
                break;
            }

            Console.WriteLine("L'adversaire réussit son contrôle.");
            Thread.Sleep(1000);
            Console.Clear();   

            // 2. Passes Adversaire
            bool passesReussies = true;
            for (int i = 1; i <= 3; i++)
            {
                int tosspass = Random.Shared.Next(1, 101);
                if (tosspass <= passad)
                {
                    Console.WriteLine($"Adversaire : passe {i} réussie");
                    Thread.Sleep(1000);
                }
                else
                {
                    Console.WriteLine("Adversaire : Passe foirée !");
                    passesReussies = false;
                    point++;
                    serviceJoueur = true;
                    Thread.Sleep(1000);
                    Console.Clear();   
                    break;
                }
            }
            Console.Clear();   
            if (!passesReussies)
            {
                break;
            }

            // 3. Frappe Adversaire
            int tossatt = Random.Shared.Next(1, 101);
            if (tossatt > attad)
            {
                Console.WriteLine("L'adversaire a raté sa frappe ! (Hors-limites)");
                point++;
                serviceJoueur = true;
                Thread.Sleep(1000);
                Console.Clear();   
                break;
            }

            Console.WriteLine("L'adversaire tente une frappe et réussit !");
            Thread.Sleep(1000);
            Console.Clear();   

            // La balle passe au joueur
            tourAdversaire = false;
        }
        else
        {
            // 1. Réception Joueur
            int tossdef = Random.Shared.Next(1, 101);
            if (tossdef > def)
            {
                Console.WriteLine("Vous avez raté la réception !");
                pointad++;
                serviceJoueur = false;
                Thread.Sleep(1000);
                Console.Clear();   
                break;
            }

            Console.WriteLine("Réception réussie par votre équipe !");
            Thread.Sleep(1000);
            Console.Clear();   

            // 2. Passes Joueur
            bool passesReussies = true;
            for (int i = 1; i <= 3; i++)
            {
                int tosspass = Random.Shared.Next(1, 101);
                if (tosspass <= pass)
                {
                    Console.WriteLine($"Votre équipe : passe {i} réussie");
                    Thread.Sleep(1000);
                }
                else
                {
                    Console.WriteLine("Votre équipe : Passe foirée !");
                    passesReussies = false;
                    pointad++;
                    serviceJoueur = false;
                    Thread.Sleep(1000);
                    Console.Clear();   
                    break;
                }
            }
            Console.Clear();   
            if (!passesReussies)
            {
                break;
            }

            // 3. Frappe Joueur
            int tossatt = Random.Shared.Next(1, 101);
            if (tossatt > att)
            {
                Console.WriteLine("Votre équipe a raté sa frappe ! (Hors-limites)");
                pointad++;
                serviceJoueur = false;
                Thread.Sleep(1000);
                Console.Clear();   
                break;
            }

            Console.WriteLine("Attaque réussie par votre équipe !");
            Thread.Sleep(1000);
            Console.Clear();   

            // La balle à l'adversaire
            tourAdversaire = true;
        }
    }

    // Affichage  du score 
    Thread.Sleep(1000);
    Console.WriteLine($"\n--- SCORE : Joueur {point} - {pointad} IA ---\n");
    Thread.Sleep(2000);
    Console.Clear();
}

// Fin du match
if (point >= 21)
{
    Console.WriteLine($"Vous avez giflé votre adversaire {point} à {pointad} !");
}
else
{
    Console.WriteLine($"Vous êtes catastrophique, vous avez perdu {point} à {pointad}.");
}