using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HangmanLéopoldROUXDECORZENT
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string GuessLetter;
        private int vies = 7;
        private string randomWord;
        private DispatcherTimer gameTimer; 
        private int timeLeft = 60; // Temps restant pour le jeu

        private MediaPlayer playMedia = new MediaPlayer(); // instance du media player

       

        public MainWindow()
        {
            InitializeComponent();
            setupGame();
            InitializeMediaPlayer();
            InitializeTimer(); // Appelle la fonction InitializeTimer
        }

        //Fonction pour intialiser le media player
        private void InitializeMediaPlayer()
        {
            try
            {
                var uri = new Uri("../../Ressources/Sound/False.mp3", UriKind.Relative);
                playMedia.Open(uri);
                playMedia.Volume = 100; // Set the volume to 100% (you can adjust this value to your liking)
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing media player: " + ex.Message);
            }
        }

        public TextBox HiddenWordTextBox { get; private set; }

        //Fonction pour initialiser le jeu
        private void setupGame()
        {
            string filePath = "../../Ressources/mots5lettres.txt"; // Chemin du fichier .txt
            List<string> list = new List<string>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    list.Add(line.Trim().ToLower());
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Le fichier 'mots.txt' n'a pas été trouvé.");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
                return;
            }

            Random rnd = new Random();
            int randomIndex = rnd.Next(0, list.Count);
            randomWord = list[randomIndex].ToUpper();
            int wordLength = randomWord.Length;
            string hiddenWord = new string('*', wordLength);
            DisplayHiddenWord(hiddenWord);
            UpdateLifeCounter(); // Appelle la fonction UpdateLifeCounter
        }

        //Fonction pour lancer le jeu
        public void runGame()
        {
            if (GuessLetter != null)
            {
                if (randomWord.Contains(GuessLetter.ToUpper()))
                {
                    for (int i = 0; i < randomWord.Length; i++)
                    {
                        if (randomWord[i].ToString().ToUpper() == GuessLetter.ToUpper())
                        {
                            if (TB_Display != null)
                            {
                                TB_Display.Text = TB_Display.Text.Remove(i, 1).Insert(i, GuessLetter.ToUpper());
                            }
                        }
                    }
                }
                else
                {
                    vies--;
                    UpdateLifeCounter(); // Appelle la fonction UpdateLifeCounter
                    PlaySound(); // Joue le son lorsque c'est faux
                }

                if (vies == 1)
                {
                    MessageBox.Show("Défaite !");
                    PlaySound(); // Joue le son lorsque c'est perdu
                    EndOfGame(); // Appelle la fonction EndOfGame -> ferme l'application
                }

                if (TB_Display != null && !TB_Display.Text.Contains("*"))
                {
                    MessageBox.Show("Victoire !");
                    PlaySound(); // Joue le son lorsque c'est gagné
                    EndOfGame(); // Appelle la fonction EndOfGame -> ferme l'application
                }
            }
            DisplayHiddenWord(TB_Display.Text);
        }

        //Fonction pour gérer les boutons 
        private void BTN_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string btnContent = btn.Content.ToString();
            btn.IsEnabled = false;
            GuessLetter = btn.Content.ToString();
            runGame();
        }

        //Fonction pour jouer le son
        private void PlaySound()
        {
            playMedia.Stop(); // Arrete le media player s'il est dejà en marche
            playMedia.Play(); // Joue le son
        }

        //Fonction pour afficher le mot caché
        private void DisplayHiddenWord(string hiddenWord)
        {
            TB_Display.Text = hiddenWord;
        }

        //Fonction pour gérer les vies & les images
        private void UpdateLifeCounter()
        {
            // Mettre à jour l'image en fonction des vies restantes
            string imagePath = $"../../Ressources/images/{8 - vies}.png"; // 1.png pour 7 vies, 2.png pour 6 vies, etc.
            lifeImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
        }

        //Fonction pour initialiser le timer
        private void InitializeTimer()
        {
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromSeconds(1);
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
        }

        //Fonction pour gérer l'événement Tick du timer
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            UpdateTimerDisplay();

            if (timeLeft <= 0)
            {
                gameTimer.Stop();
                MessageBox.Show("Temps écoulé ! Défaite !");
                PlaySound(); // Joue le son lorsque le temps est écoulé
            }
        }

        //Fonction pour mettre à jour l'affichage du timer
        private void UpdateTimerDisplay()
        {
            TimerTextBlock.Text = $"Temps restant : {timeLeft} secondes";
        }

        //Fonction pour gérer la fin du jeu
        private void EndOfGame()
        { 
            MessageBox.Show("Fin du jeu ! L'application va se  fermer");
            Application.Current.Shutdown();
        }
    }
}
