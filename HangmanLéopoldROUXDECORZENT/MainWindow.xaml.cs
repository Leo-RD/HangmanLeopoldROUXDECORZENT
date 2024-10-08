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

        private void runGame()
        {
            // Fonction de jeu
            // Vérifier si la lettre est dans le mot
            // Si la lettre est dans le mot, affichez la lettre
            // Si la lettre n'est pas dans le mot, décrémentez les vies
            // Si les vies sont épuisées, affichez un message de défaite
            // Si le mot est trouvé, affichez un message de victoire

            // Récupérer la lettre saisie par le joueur
            string guessLetter = GuessLetter;

            // Vérifier si la lettre est dans le mot
            if (randomWord.Contains(guessLetter))
            {
                // Afficher la lettre dans la TextBox à la bonne place
                for (int i = 0; i < randomWord.Length; i++)
                {
                    if (randomWord[i] == guessLetter[0])
                    {
                        hiddenWord = hiddenWord.Remove(i, 1).Insert(i, guessLetter);
                    }
                }

                // Afficher le mot mis à jour avec la lettre trouvée
                DisplayHiddenWord(hiddenWord);

                // Vérifier si toutes les lettres ont été devinées
                if (!hiddenWord.Contains('*'))
                {
                    // Afficher un message de victoire
                    MessageBox.Show("Félicitations ! Vous avez deviné le mot !");
                }
            }
            else
            {
                // Décrémenter le nombre de vies
                vies--;

                // Vérifier si les vies sont épuisées
                if (vies == 0)
                {
                    // Afficher un message de défaite
                    MessageBox.Show("Dommage ! Vous avez perdu. Le mot était : " + randomWord);
                }
            }
        }

        private void guess(string guessLetter, string letter)
        {
            // Vérifier si la lettre a déjà été devinée
            if (GuessLetter.Contains(guessLetter))
            {
                // Afficher un message indiquant que la lettre a déjà été devinée
                MessageBox.Show("Vous avez déjà deviné cette lettre !");
            }
            else
            {
                // Ajouter la lettre à la liste des lettres devinées
                GuessLetter += guessLetter;

                // Appeler la fonction runGame pour vérifier si la lettre est dans le mot
                runGame();
            }
        }

        private void BTN_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string btnContent = btn.Content.ToString();
            btn.IsEnabled = false;

            // Appeler la fonction guess avec la lettre choisie par le joueur
            guess(btnContent, btnContent);
        }



       private void BTN_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            String btnContent = btn.Content.ToString();
            btn.IsEnabled = false;
            //Fonction Bouton
        }
        private void DisplayHiddenWord(string hiddenWord)
        {
            TB_Display.Text = hiddenWord;
        }
         


 

        


    }
}
