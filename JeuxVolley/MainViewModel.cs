using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MatchWpf
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private int _scoreJoueur;
        private int _scoreIA;
        private string _logsText = "Appuyez sur 'Lancer le match' pour démarrer.\n";
        private string _boutonTexte = "Lancer le match";
        private bool _canStartMatch = true;

        // Stats Joueur
        private readonly int _att = 70;
        private readonly int _def = 70;
        private readonly int _pass = 100;

        // Stats IA
        private readonly int _attAd = 70;
        private readonly int _defAd = 70;
        private readonly int _passAd = 100;

        public int ScoreJoueur
        {
            get => _scoreJoueur;
            set { _scoreJoueur = value; OnPropertyChanged(); }
        }

        public int ScoreIA
        {
            get => _scoreIA;
            set { _scoreIA = value; OnPropertyChanged(); }
        }

        public string LogsText
        {
            get => _logsText;
            set { _logsText = value; OnPropertyChanged(); }
        }

        public string BoutonTexte
        {
            get => _boutonTexte;
            set { _boutonTexte = value; OnPropertyChanged(); }
        }

        public bool CanStartMatch
        {
            get => _canStartMatch;
            set { _canStartMatch = value; OnPropertyChanged(); }
        }

        public ICommand DemarrerMatchCommand { get; set; }

        public MainViewModel()
        {
            DemarrerMatchCommand = new RelayCommand(async () => await DemarrerMatchAsync());
        }

        private async Task DemarrerMatchAsync()
        {
            CanStartMatch = false;
            ScoreJoueur = 0;
            ScoreIA = 0;
            LogsText = "";

            Log("Début du match dans :");
            for (int i = 3; i >= 1; i--)
            {
                Log($"{i}...");
                await Task.Delay(1000);
            }

            LogsText = "";
            bool serviceJoueur = true;

            // Boucle principale du match
            while (ScoreJoueur < 21 && ScoreIA < 21)
            {
                bool echangeEnCours = false;
                bool tourAdversaire = false;

                if (serviceJoueur)
                {
                    Log("--- SERVICE JOUEUR ---");
                    int tossatt = Random.Shared.Next(1, 101);

                    if (tossatt > _att)
                    {
                        Log("Service de Merde ! Faute directe.");
                        ScoreIA++;
                        serviceJoueur = false;
                    }
                    else
                    {
                        Log("Service Réussi !");
                        echangeEnCours = true;
                        tourAdversaire = true;
                    }
                }
                else
                {
                    Log("--- SERVICE ADVERSAIRE ---");
                    int tossatt = Random.Shared.Next(1, 101);

                    if (tossatt > _attAd)
                    {
                        Log("L'adversaire rate son service ! Faute directe.");
                        ScoreJoueur++;
                        serviceJoueur = true;
                    }
                    else
                    {
                        Log("L'adversaire réussit son service !");
                        echangeEnCours = true;
                        tourAdversaire = false;
                    }
                }

                await Task.Delay(1000);

                // Boucle de l'échange
                while (echangeEnCours)
                {
                    if (tourAdversaire)
                    {
                        // 1. Contrôle IA
                        int tossdef = Random.Shared.Next(1, 101);
                        if (tossdef > _defAd)
                        {
                            Log("L'adversaire a raté son contrôle !");
                            ScoreJoueur++;
                            serviceJoueur = true;
                            break;
                        }
                        Log("L'adversaire réussit son contrôle.");
                        await Task.Delay(800);

                        // 2. Passes IA
                        bool passesOK = true;
                        for (int i = 1; i <= 3; i++)
                        {
                            int tosspass = Random.Shared.Next(1, 101);
                            if (tosspass <= _passAd)
                            {
                                Log($"Adversaire : passe {i} réussie");
                                await Task.Delay(600);
                            }
                            else
                            {
                                Log("Adversaire : Passe foirée !");
                                passesOK = false;
                                ScoreJoueur++;
                                serviceJoueur = true;
                                break;
                            }
                        }
                        if (!passesOK) break;

                        // 3. Frappe IA
                        int tossatt = Random.Shared.Next(1, 101);
                        if (tossatt > _attAd)
                        {
                            Log("L'adversaire a raté sa frappe ! (Hors-limites)");
                            ScoreJoueur++;
                            serviceJoueur = true;
                            break;
                        }
                        Log("L'adversaire tente une frappe et réussit !");
                        await Task.Delay(800);

                        tourAdversaire = false;
                    }
                    else
                    {
                        // 1. Contrôle Joueur
                        int tossdef = Random.Shared.Next(1, 101);
                        if (tossdef > _def)
                        {
                            Log("Vous avez raté la réception !");
                            ScoreIA++;
                            serviceJoueur = false;
                            break;
                        }
                        Log("Réception réussie par votre équipe !");
                        await Task.Delay(800);

                        // 2. Passes Joueur
                        bool passesOK = true;
                        for (int i = 1; i <= 3; i++)
                        {
                            int tosspass = Random.Shared.Next(1, 101);
                            if (tosspass <= _pass)
                            {
                                Log($"Votre équipe : passe {i} réussie");
                                await Task.Delay(600);
                            }
                            else
                            {
                                Log("Votre équipe : Passe foirée !");
                                passesOK = false;
                                ScoreIA++;
                                serviceJoueur = false;
                                break;
                            }
                        }
                        if (!passesOK) break;

                        // 3. Frappe Joueur
                        int tossatt = Random.Shared.Next(1, 101);
                        if (tossatt > _att)
                        {
                            Log("Votre équipe a raté sa frappe ! (Hors-limites)");
                            ScoreIA++;
                            serviceJoueur = false;
                            break;
                        }
                        Log("Attaque réussie par votre équipe !");
                        await Task.Delay(800);

                        tourAdversaire = true;
                    }
                }

                await Task.Delay(1200);
                Log($"\n=== SCORE : Joueur {ScoreJoueur} - {ScoreIA} IA ===\n");
                await Task.Delay(1500);
            }

            // Fin du match
            if (ScoreJoueur >= 21)
                Log($"\nVICTOIRE ! Vous avez giflé votre adversaire {ScoreJoueur} à {ScoreIA} !");
            else
                Log($"\nDÉFAITE ! Vous avez perdu {ScoreJoueur} à {ScoreIA}.");

            BoutonTexte = "Rejouer";
            CanStartMatch = true;
        }

        private void Log(string message)
        {
            LogsText += message + "\n";
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    // Helper ICommand simple
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        public RelayCommand(Action execute) => _execute = execute;
        public bool CanExecute(object? parameter) => true;
        public void Execute(object? parameter) => _execute();
        public event EventHandler? CanExecuteChanged { add { } remove { } }
    }
}