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
