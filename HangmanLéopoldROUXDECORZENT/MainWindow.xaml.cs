using System;
using System.Collections.Generic;
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

namespace HangmanLéopoldROUXDECORZENT
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            setupGame();
        }

        string GuessLetter;
        int vies = 5;

        public TextBox HiddenWordTextBox { get; private set; }

        private void setupGame()
        {
            //Initialisation du jeu

            List<string> list = new List<string> { "vache", "chien", "guepe", "koala", "lapin", "zebre", "ecran", "canon", "epave", "pomme" };
            Random rnd = new Random();
            int randomIndex = rnd.Next(0, list.Count);
            string randomWord = list[randomIndex];
            // Utilisez le mot aléatoire sélectionné pour continuer le jeu

            int wordLength = randomWord.Length;
            string hiddenWord = new string('*', wordLength);
            // Afficher le mot caché avec des astérisques

            DisplayHiddenWord(hiddenWord);

        }

        public void runGame()
        {
            // Exécutez le jeu
            // Vérifiez si la lettre est dans le mot
            // Si la lettre est dans le mot, affichez la lettre
            // Si la lettre n'est pas dans le mot, décrémentez les vies
            // Si les vies sont épuisées, affichez un message de défaite
            // Si le mot est trouvé, affichez un message de victoire

            if (GuessLetter != null)
            {
                string randomWord = "vache"; // Remplacez "vache" par le mot aléatoire sélectionné

                // Vérifiez si la lettre est dans le mot
                if (randomWord.Contains(GuessLetter.ToUpper()))
                {
                    // Affichez la lettre
                    for (int i = 0; i < randomWord.Length; i++)
                    {
                        if (randomWord[i].ToString().ToUpper() == GuessLetter.ToUpper())
                        {
                            // Remplacez le caractère correspondant dans le mot caché
                            HiddenWordTextBox.Text = HiddenWordTextBox.Text.Remove(i, 1).Insert(i, GuessLetter.ToUpper());
                        }
                    }
                }
                else
                {
                    vies--;
                }

                // Si les vies sont épuisées, affichez un message de défaite
                if (vies == 0)
                {
                    MessageBox.Show("Défaite !");
                }

                // Si le mot est trouvé, affichez un message de victoire
                if (!HiddenWordTextBox.Text.Contains("*"))
                {
                    MessageBox.Show("Victoire !");
                }
            }
        }

        private void BTN_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string btnContent = btn.Content.ToString();
            btn.IsEnabled = false;
        }

        private void DisplayHiddenWord(string hiddenWord)
        {
            TB_Display.Text = hiddenWord;
        }
    }
}
